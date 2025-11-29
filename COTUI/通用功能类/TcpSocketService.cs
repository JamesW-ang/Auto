using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COTUI.通用功能类
{
    /// <summary>
    /// TCP Socket通讯服务
    /// 
    /// <para><b>功能：</b></para>
    /// - TCP客户端连接/断开/重连
    /// - TCP服务端监听/接受连接
    /// - 数据发送（同步/异步）
    /// - 数据接收（事件驱动）
    /// - 多客户端管理（服务端模式）
    /// - 心跳检测
    /// - 完善的异常处理
    /// 
    /// <para><b>设计原则：</b></para>
    /// - 高内聚：所有TCP Socket通讯逻辑集中在此类
    /// - 低耦合：通过事件和回调与外部交互，不依赖MQTT等业务层
    /// - 单一职责：仅负责TCP通讯，不涉及具体业务逻辑（如MQTT协议）
    /// - 灵活扩展：支持客户端和服务端两种模式
    /// </summary>
    public class TcpSocketService
    {
        #region 枚举

        /// <summary>
        /// TCP工作模式
        /// </summary>
        public enum TcpMode
        {
            /// <summary>客户端模式</summary>
            Client,
            /// <summary>服务端模式</summary>
            Server
        }

        #endregion

        #region 字段

        // 客户端模式
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private bool isConnected = false;
        
        // 服务端模式
        private TcpListener tcpListener;
        private ConcurrentDictionary<string, TcpClient> connectedClients = new ConcurrentDictionary<string, TcpClient>();
        private bool isListening = false;

        // 通用配置
        private TcpMode mode;
        private string remoteHost;
        private int remotePort;
        private int localPort;
        private readonly object lockObject = new object();

        // 接收缓冲区
        private const int BufferSize = 8192;
        private byte[] receiveBuffer = new byte[BufferSize];

        // 自动重连（仅客户端模式）
        private System.Threading.Timer reconnectTimer;
        private int reconnectAttempts = 0;
        private const int MaxReconnectAttempts = 5;
        private const int ReconnectInterval = 5000; // 5秒
        private bool enableAutoReconnect = true;

        // 心跳检测
        private System.Threading.Timer heartbeatTimer;
        private bool enableHeartbeat = false;
        private int heartbeatInterval = 30000; // 30秒
        private byte[] heartbeatData = Encoding.UTF8.GetBytes("HEARTBEAT");

        #endregion

        #region 事件

        /// <summary>
        /// 连接状态变化事件 (仅客户端模式)
        /// </summary>
        public event EventHandler<bool> ConnectionStatusChanged;

        /// <summary>
        /// 客户端连接事件 (仅服务端模式，参数为客户端ID)
        /// </summary>
        public event EventHandler<string> ClientConnected;

        /// <summary>
        /// 客户端断开事件 (仅服务端模式，参数为客户端ID)
        /// </summary>
        public event EventHandler<string> ClientDisconnected;

        /// <summary>
        /// 数据接收事件 (客户端模式: 接收到的数据; 服务端模式: 包含客户端ID和数据的元组)
        /// </summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>
        /// 服务端数据接收事件 (包含客户端ID和数据)
        /// </summary>
        public event EventHandler<(string ClientId, byte[] Data)> ServerDataReceived;

        /// <summary>
        /// 文本接收事件 (使用UTF-8解码)
        /// </summary>
        public event EventHandler<string> TextReceived;

        /// <summary>
        /// 服务端文本接收事件 (包含客户端ID和文本)
        /// </summary>
        public event EventHandler<(string ClientId, string Text)> ServerTextReceived;

        /// <summary>
        /// 错误事件
        /// </summary>
        public event EventHandler<string> ErrorOccurred;

        #endregion

        #region 属性

        /// <summary>
        /// 当前工作模式
        /// </summary>
        public TcpMode Mode => mode;

        /// <summary>
        /// 是否已连接（客户端模式）或正在监听（服务端模式）
        /// </summary>
        public bool IsConnected => mode == TcpMode.Client ? isConnected : isListening;

        /// <summary>
        /// 远程主机地址（客户端模式）
        /// </summary>
        public string RemoteHost => remoteHost;

        /// <summary>
        /// 远程端口（客户端模式）
        /// </summary>
        public int RemotePort => remotePort;

        /// <summary>
        /// 本地监听端口（服务端模式）
        /// </summary>
        public int LocalPort => localPort;

        /// <summary>
        /// 已连接的客户端数量（服务端模式）
        /// </summary>
        public int ClientCount => connectedClients.Count;

        /// <summary>
        /// 是否启用自动重连（仅客户端模式）
        /// </summary>
        public bool EnableAutoReconnect
        {
            get => enableAutoReconnect;
            set => enableAutoReconnect = value;
        }

        /// <summary>
        /// 是否启用心跳检测
        /// </summary>
        public bool EnableHeartbeat
        {
            get => enableHeartbeat;
            set => enableHeartbeat = value;
        }

        /// <summary>
        /// 心跳间隔（毫秒）
        /// </summary>
        public int HeartbeatInterval
        {
            get => heartbeatInterval;
            set => heartbeatInterval = value;
        }

        #endregion

        #region 构造函数

        public TcpSocketService()
        {
        }

        #endregion

        #region 客户端模式 - 连接管理

        /// <summary>
        /// 以客户端模式连接到TCP服务器
        /// </summary>
        /// <param name="host">服务器地址</param>
        /// <param name="port">服务器端口</param>
        public async Task ConnectAsync(string host, int port)
        {
            if (mode == TcpMode.Server)
            {
                throw new InvalidOperationException("当前处于服务端模式，无法作为客户端连接");
            }

            mode = TcpMode.Client;
            remoteHost = host;
            remotePort = port;

            try
            {
                // 关闭旧连接
                DisconnectClient();

                Gvar.Logger.Info(LogLevel.Info, $"正在连接到 TCP 服务器 {host}:{port}...");

                // 创建TCP客户端
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(host, port);

                networkStream = tcpClient.GetStream();
                isConnected = true;
                reconnectAttempts = 0;

                Gvar.Logger.Info(LogLevel.Info, $"✅ TCP 连接成功: {host}:{port}");
                ConnectionStatusChanged?.Invoke(this, true);

                // 启动接收数据
                _ = Task.Run(ReceiveDataAsync);

                // 启动心跳
                if (enableHeartbeat)
                {
                    StartHeartbeat();
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                Gvar.Logger.ErrorException(ex, $"TCP 连接失败: {host}:{port}");
                ErrorOccurred?.Invoke(this, $"连接失败: {ex.Message}");

                // 启动自动重连
                if (enableAutoReconnect)
                {
                    StartReconnectTimer();
                }

                throw;
            }
        }

        /// <summary>
        /// 断开客户端连接
        /// </summary>
        public void DisconnectClient()
        {
            lock (lockObject)
            {
                try
                {
                    StopReconnectTimer();
                    StopHeartbeat();
                    reconnectAttempts = 0;

                    if (networkStream != null)
                    {
                        networkStream.Close();
                        networkStream = null;
                    }

                    if (tcpClient != null)
                    {
                        tcpClient.Close();
                        tcpClient = null;
                    }

                    if (isConnected)
                    {
                        isConnected = false;
                        Gvar.Logger.Info(LogLevel.Info, $"TCP 连接已断开: {remoteHost}:{remotePort}");
                        ConnectionStatusChanged?.Invoke(this, false);
                    }
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, "断开 TCP 连接时发生错误");
                    ErrorOccurred?.Invoke(this, $"断开连接失败: {ex.Message}");
                }
            }
        }

        #endregion

        #region 服务端模式 - 监听管理

        /// <summary>
        /// 以服务端模式启动TCP监听
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="ipAddress">监听地址（默认为任意地址）</param>
        public void StartServer(int port, IPAddress ipAddress = null)
        {
            if (mode == TcpMode.Client)
            {
                throw new InvalidOperationException("当前处于客户端模式，无法启动服务端");
            }

            mode = TcpMode.Server;
            localPort = port;

            try
            {
                // 停止旧监听
                StopServer();

                Gvar.Logger.Info(LogLevel.Info, $"正在启动 TCP 服务端，端口: {port}...");

                // 创建监听器
                IPAddress listenAddress = ipAddress ?? IPAddress.Any;
                tcpListener = new TcpListener(listenAddress, port);
                tcpListener.Start();
                isListening = true;

                Gvar.Logger.Info(LogLevel.Info, $"✅ TCP 服务端已启动: {listenAddress}:{port}");

                // 异步接受客户端连接
                _ = Task.Run(AcceptClientsAsync);
            }
            catch (Exception ex)
            {
                isListening = false;
                Gvar.Logger.ErrorException(ex, $"启动 TCP 服务端失败: {port}");
                ErrorOccurred?.Invoke(this, $"启动服务端失败: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 停止TCP服务端
        /// </summary>
        public void StopServer()
        {
            try
            {
                isListening = false;

                // 断开所有客户端
                foreach (var client in connectedClients.Values)
                {
                    try
                    {
                        client.Close();
                    }
                    catch { }
                }
                connectedClients.Clear();

                // 停止监听
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                    tcpListener = null;
                }

                Gvar.Logger.Info(LogLevel.Info, $"TCP 服务端已停止: {localPort}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "停止 TCP 服务端时发生错误");
                ErrorOccurred?.Invoke(this, $"停止服务端失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 异步接受客户端连接
        /// </summary>
        private async Task AcceptClientsAsync()
        {
            while (isListening && tcpListener != null)
            {
                try
                {
                    TcpClient client = await tcpListener.AcceptTcpClientAsync();
                    string clientId = Guid.NewGuid().ToString();
                    
                    connectedClients[clientId] = client;

                    string clientEndpoint = client.Client.RemoteEndPoint?.ToString() ?? "Unknown";
                    Gvar.Logger.Info(LogLevel.Debug, $"客户端已连接: {clientEndpoint} (ID: {clientId})");
                    ClientConnected?.Invoke(this, clientId);

                    // 异步处理客户端数据
                    _ = Task.Run(() => ReceiveClientDataAsync(clientId, client));
                }
                catch (Exception ex)
                {
                    if (isListening)
                    {
                        Gvar.Logger.ErrorException(ex, "接受客户端连接失败");
                        ErrorOccurred?.Invoke(this, $"接受连接失败: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 断开指定客户端
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        public void DisconnectClient(string clientId)
        {
            if (connectedClients.TryRemove(clientId, out TcpClient client))
            {
                try
                {
                    client.Close();
                    Gvar.Logger.Info(LogLevel.Debug, $"客户端已断开: {clientId}");
                    ClientDisconnected?.Invoke(this, clientId);
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, $"断开客户端失败: {clientId}");
                }
            }
        }

        /// <summary>
        /// 获取所有已连接的客户端ID
        /// </summary>
        public IEnumerable<string> GetConnectedClientIds()
        {
            return connectedClients.Keys;
        }

        #endregion

        #region 数据发送

        /// <summary>
        /// 发送字节数据（客户端模式）
        /// </summary>
        /// <param name="data">要发送的字节数组</param>
        public async Task SendBytesAsync(byte[] data)
        {
            if (mode != TcpMode.Client || !isConnected || networkStream == null)
            {
                throw new InvalidOperationException("TCP 客户端未连接");
            }

            try
            {
                await networkStream.WriteAsync(data, 0, data.Length);
                await networkStream.FlushAsync();
                Gvar.Logger.Debug(LogLevel.Trace, $"TCP 发送 ({data.Length} 字节)");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "TCP 发送数据失败");
                ErrorOccurred?.Invoke(this, $"发送失败: {ex.Message}");
                
                // 发送失败可能是连接断开
                HandleDisconnection();
                throw;
            }
        }

        /// <summary>
        /// 发送文本数据（客户端模式，使用UTF-8编码）
        /// </summary>
        /// <param name="text">要发送的文本</param>
        public async Task SendTextAsync(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            await SendBytesAsync(data);
        }

        /// <summary>
        /// 发送字节数据到指定客户端（服务端模式）
        /// </summary>
        /// <param name="clientId">客户端ID</param>
        /// <param name="data">要发送的字节数组</param>
        public async Task SendBytesToClientAsync(string clientId, byte[] data)
        {
            if (mode != TcpMode.Server)
            {
                throw new InvalidOperationException("当前不是服务端模式");
            }

            if (!connectedClients.TryGetValue(clientId, out TcpClient client))
            {
                throw new InvalidOperationException($"客户端不存在: {clientId}");
            }

            try
            {
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
                Gvar.Logger.Debug(LogLevel.Trace, $"TCP 发送到客户端 {clientId} ({data.Length} 字节)");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"发送数据到客户端失败: {clientId}");
                ErrorOccurred?.Invoke(this, $"发送到客户端失败: {ex.Message}");
                
                // 移除失效客户端
                DisconnectClient(clientId);
                throw;
            }
        }

        /// <summary>
        /// 发送文本数据到指定客户端（服务端模式，使用UTF-8编码）
        /// </summary>
        public async Task SendTextToClientAsync(string clientId, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            await SendBytesToClientAsync(clientId, data);
        }

        /// <summary>
        /// 广播字节数据到所有客户端（服务端模式）
        /// </summary>
        public async Task BroadcastBytesAsync(byte[] data)
        {
            if (mode != TcpMode.Server)
            {
                throw new InvalidOperationException("当前不是服务端模式");
            }

            var tasks = new List<Task>();
            foreach (var clientId in connectedClients.Keys)
            {
                tasks.Add(SendBytesToClientAsync(clientId, data));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 广播文本数据到所有客户端（服务端模式）
        /// </summary>
        public async Task BroadcastTextAsync(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            await BroadcastBytesAsync(data);
        }

        #endregion

        #region 数据接收

        /// <summary>
        /// 异步接收数据（客户端模式）
        /// </summary>
        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[BufferSize];

            while (isConnected && networkStream != null)
            {
                try
                {
                    int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // 连接已关闭
                        Gvar.Logger.Warn(LogLevel.Warn, "TCP 连接已被远程主机关闭");
                        HandleDisconnection();
                        break;
                    }

                    // 提取有效数据
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);

                    // 触发事件
                    DataReceived?.Invoke(this, data);
                    
                    string text = Encoding.UTF8.GetString(data);
                    TextReceived?.Invoke(this, text);

                    Gvar.Logger.Debug(LogLevel.Trace, $"TCP 接收 ({bytesRead} 字节)");
                }
                catch (Exception ex)
                {
                    if (isConnected)
                    {
                        Gvar.Logger.ErrorException(ex, "TCP 接收数据失败");
                        ErrorOccurred?.Invoke(this, $"接收失败: {ex.Message}");
                        HandleDisconnection();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 异步接收客户端数据（服务端模式）
        /// </summary>
        private async Task ReceiveClientDataAsync(string clientId, TcpClient client)
        {
            byte[] buffer = new byte[BufferSize];
            NetworkStream stream = client.GetStream();

            while (connectedClients.ContainsKey(clientId))
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // 客户端断开
                        Gvar.Logger.Info(LogLevel.Info, $"客户端断开连接: {clientId}");
                        DisconnectClient(clientId);
                        break;
                    }

                    // 提取有效数据
                    byte[] data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);

                    // 触发事件
                    ServerDataReceived?.Invoke(this, (clientId, data));
                    
                    string text = Encoding.UTF8.GetString(data);
                    ServerTextReceived?.Invoke(this, (clientId, text));

                    Gvar.Logger.Debug(LogLevel.Debug, $"从客户端 {clientId} 接收 ({bytesRead} 字节)");
                }
                catch (Exception ex)
                {
                    if (connectedClients.ContainsKey(clientId))
                    {
                        Gvar.Logger.ErrorException(ex, $"从客户端接收数据失败: {clientId}");
                        DisconnectClient(clientId);
                    }
                    break;
                }
            }
        }

        #endregion

        #region 自动重连（客户端模式）

        /// <summary>
        /// 处理连接断开
        /// </summary>
        private void HandleDisconnection()
        {
            lock (lockObject)
            {
                if (!isConnected)
                    return;

                DisconnectClient();

                if (enableAutoReconnect && reconnectAttempts < MaxReconnectAttempts)
                {
                    StartReconnectTimer();
                }
            }
        }

        /// <summary>
        /// 启动重连定时器
        /// </summary>
        private void StartReconnectTimer()
        {
            StopReconnectTimer();
            reconnectTimer = new System.Threading.Timer(async (state) => await Reconnect(), null, ReconnectInterval, Timeout.Infinite);
            Gvar.Logger.Info(LogLevel.Info, $"TCP 将在 {ReconnectInterval / 1000} 秒后尝试重连");
        }

        /// <summary>
        /// 停止重连定时器
        /// </summary>
        private void StopReconnectTimer()
        {
            reconnectTimer?.Dispose();
            reconnectTimer = null;
        }

        /// <summary>
        /// 重连逻辑
        /// </summary>
        private async Task Reconnect()
        {
            if (isConnected || string.IsNullOrEmpty(remoteHost))
                return;

            if (reconnectAttempts >= MaxReconnectAttempts)
            {
                Gvar.Logger.Error(LogLevel.Error, $"已达到最大重连尝试次数 ({MaxReconnectAttempts})，停止自动重连");
                return;
            }

            reconnectAttempts++;
            Gvar.Logger.Info(LogLevel.Info, $"尝试重连 TCP 服务器 (第 {reconnectAttempts} 次)");

            try
            {
                await ConnectAsync(remoteHost, remotePort);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"TCP 重连失败: {ex.Message}");

                if (reconnectAttempts < MaxReconnectAttempts)
                {
                    StartReconnectTimer();
                }
            }
        }

        /// <summary>
        /// 重置重连尝试次数
        /// </summary>
        public void ResetReconnectAttempts()
        {
            reconnectAttempts = 0;
        }

        #endregion

        #region 心跳检测

        /// <summary>
        /// 启动心跳检测
        /// </summary>
        private void StartHeartbeat()
        {
            if (!enableHeartbeat)
                return;

            StopHeartbeat();
            heartbeatTimer = new System.Threading.Timer(async (state) => await SendHeartbeat(), null, heartbeatInterval, heartbeatInterval);
            Gvar.Logger.Debug(LogLevel.Debug, $"心跳检测已启动，间隔: {heartbeatInterval}ms");
        }

        /// <summary>
        /// 停止心跳检测
        /// </summary>
        private void StopHeartbeat()
        {
            heartbeatTimer?.Dispose();
            heartbeatTimer = null;
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        private async Task SendHeartbeat()
        {
            try
            {
                if (mode == TcpMode.Client && isConnected)
                {
                    await SendBytesAsync(heartbeatData);
                    Gvar.Logger.Debug(LogLevel.Debug, "心跳包已发送");
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.Debug(LogLevel.Debug, $"发送心跳包失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 设置自定义心跳数据
        /// </summary>
        public void SetHeartbeatData(byte[] data)
        {
            heartbeatData = data;
        }

        /// <summary>
        /// 设置自定义心跳数据（文本）
        /// </summary>
        public void SetHeartbeatData(string text)
        {
            heartbeatData = Encoding.UTF8.GetBytes(text);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            if (mode == TcpMode.Client)
            {
                DisconnectClient();
            }
            else if (mode == TcpMode.Server)
            {
                StopServer();
            }
        }

        #endregion
    }
}
