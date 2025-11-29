using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COTUI.通用功能类
{
    /// <summary>
    /// 串口通讯服务
    /// 
    /// <para><b>功能：</b></para>
    /// - 串口打开/关闭/重连
    /// - 数据发送（同步/异步）
    /// - 数据接收（事件驱动）
    /// - 自动重连机制
    /// - 完善的异常处理
    /// 
    /// <para><b>设计原则：</b></para>
    /// - 高内聚：所有串口通讯逻辑集中在此类
    /// - 低耦合：通过事件和回调与外部交互
    /// - 单一职责：仅负责串口通讯，不涉及业务逻辑
    /// </summary>
    public class SerialPortService
    {
        #region 字段

        private SerialPort serialPort;
        private bool isConnected = false;
        private readonly object lockObject = new object();
        
        // 接收缓冲区
        private StringBuilder receiveBuffer = new StringBuilder();
        
        // 自动重连
        private System.Threading.Timer reconnectTimer;
        private int reconnectAttempts = 0;
        private const int MaxReconnectAttempts = 5;
        private const int ReconnectInterval = 3000; // 3秒
        
        // 配置参数
        private string portName;
        private int baudRate;
        private Parity parity;
        private int dataBits;
        private StopBits stopBits;
        private bool enableAutoReconnect = true;

        #endregion

        #region 事件

        /// <summary>
        /// 连接状态变化事件 (true=已连接, false=已断开)
        /// </summary>
        public event EventHandler<bool> ConnectionStatusChanged;

        /// <summary>
        /// 数据接收事件 (接收到的原始字节数据)
        /// </summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>
        /// 文本接收事件 (接收到的文本数据，使用UTF-8解码)
        /// </summary>
        public event EventHandler<string> TextReceived;

        /// <summary>
        /// 错误事件
        /// </summary>
        public event EventHandler<string> ErrorOccurred;

        #endregion

        #region 属性

        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnected
        {
            get
            {
                lock (lockObject)
                {
                    return isConnected && serialPort != null && serialPort.IsOpen;
                }
            }
        }

        /// <summary>
        /// 当前端口名
        /// </summary>
        public string PortName => portName;

        /// <summary>
        /// 当前波特率
        /// </summary>
        public int BaudRate => baudRate;

        /// <summary>
        /// 是否启用自动重连
        /// </summary>
        public bool EnableAutoReconnect
        {
            get => enableAutoReconnect;
            set => enableAutoReconnect = value;
        }

        #endregion

        #region 构造函数

        public SerialPortService()
        {
        }

        #endregion

        #region 连接管理

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="portName">端口名 (如 COM1)</param>
        /// <param name="baudRate">波特率 (如 9600)</param>
        /// <param name="parity">校验位 (默认无校验)</param>
        /// <param name="dataBits">数据位 (默认8位)</param>
        /// <param name="stopBits">停止位 (默认1位)</param>
        public void Open(string portName, int baudRate, Parity parity = Parity.None, 
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            lock (lockObject)
            {
                try
                {
                    // 保存配置参数
                    this.portName = portName;
                    this.baudRate = baudRate;
                    this.parity = parity;
                    this.dataBits = dataBits;
                    this.stopBits = stopBits;

                    // 如果已连接，先关闭
                    if (serialPort != null && serialPort.IsOpen)
                    {
                        serialPort.Close();
                    }

                    // 创建并配置串口
                    serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
                    {
                        ReadTimeout = 500,
                        WriteTimeout = 500,
                        Encoding = Encoding.UTF8,
                        ReceivedBytesThreshold = 1 // 接收到1个字节就触发事件
                    };

                    // 注册事件
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.ErrorReceived += SerialPort_ErrorReceived;

                    // 打开串口
                    serialPort.Open();
                    isConnected = true;
                    reconnectAttempts = 0;

                    Logger.GetInstance().Info(LogLevel.Info, $"串口已打开: {portName}, 波特率: {baudRate}");
                    ConnectionStatusChanged?.Invoke(this, true);
                }
                catch (Exception ex)
                {
                    isConnected = false;
                    Logger.GetInstance().ErrorException(ex, $"打开串口失败: {portName}");
                    ErrorOccurred?.Invoke(this, $"打开串口失败: {ex.Message}");
                    
                    // 启动自动重连
                    if (enableAutoReconnect)
                    {
                        StartReconnectTimer();
                    }
                    
                    throw;
                }
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            lock (lockObject)
            {
                try
                {
                    StopReconnectTimer();
                    reconnectAttempts = 0;

                    if (serialPort != null)
                    {
                        // 注销事件
                        serialPort.DataReceived -= SerialPort_DataReceived;
                        serialPort.ErrorReceived -= SerialPort_ErrorReceived;

                        if (serialPort.IsOpen)
                        {
                            serialPort.Close();
                        }

                        serialPort.Dispose();
                        serialPort = null;
                    }

                    isConnected = false;
                    Logger.GetInstance().Info(LogLevel.Info, $"串口已关闭: {portName}");
                    ConnectionStatusChanged?.Invoke(this, false);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().ErrorException(ex, $"关闭串口失败: {portName}");
                    ErrorOccurred?.Invoke(this, $"关闭串口失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 获取系统可用串口列表
        /// </summary>
        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        #endregion

        #region 数据发送

        /// <summary>
        /// 发送字节数据
        /// </summary>
        /// <param name="data">要发送的字节数组</param>
        public void SendBytes(byte[] data)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("串口未连接");
            }

            try
            {
                serialPort.Write(data, 0, data.Length);
                Logger.GetInstance().Debug(LogLevel.Debug, $"串口发送 ({data.Length} 字节): {BitConverter.ToString(data)}");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().ErrorException(ex, "串口发送数据失败");
                ErrorOccurred?.Invoke(this, $"发送失败: {ex.Message}");
                
                // 发送失败可能是连接断开，触发重连
                HandleDisconnection();
                throw;
            }
        }

        /// <summary>
        /// 发送文本数据 (使用UTF-8编码)
        /// </summary>
        /// <param name="text">要发送的文本</param>
        public void SendText(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            SendBytes(data);
        }

        /// <summary>
        /// 发送文本数据并自动添加换行符
        /// </summary>
        /// <param name="text">要发送的文本</param>
        public void SendLine(string text)
        {
            SendText(text + "\r\n");
        }

        /// <summary>
        /// 异步发送字节数据
        /// </summary>
        public async Task SendBytesAsync(byte[] data)
        {
            await Task.Run(() => SendBytes(data));
        }

        /// <summary>
        /// 异步发送文本数据
        /// </summary>
        public async Task SendTextAsync(string text)
        {
            await Task.Run(() => SendText(text));
        }

        #endregion

        #region 数据接收

        /// <summary>
        /// 串口数据接收事件处理
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort == null || !serialPort.IsOpen)
                    return;

                // 读取所有可用数据
                int bytesToRead = serialPort.BytesToRead;
                if (bytesToRead > 0)
                {
                    byte[] buffer = new byte[bytesToRead];
                    int bytesRead = serialPort.Read(buffer, 0, bytesToRead);

                    // 触发字节数据接收事件
                    DataReceived?.Invoke(this, buffer);

                    // 解码为文本并触发文本接收事件
                    string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    receiveBuffer.Append(text);
                    
                    // 如果包含换行符，触发完整行事件
                    ProcessReceivedText();

                    Logger.GetInstance().Debug(LogLevel.Debug, $"串口接收 ({bytesRead} 字节): {text}");
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().ErrorException(ex, "处理串口接收数据失败");
                ErrorOccurred?.Invoke(this, $"接收数据处理失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理接收到的文本（按行分割）
        /// </summary>
        private void ProcessReceivedText()
        {
            string bufferContent = receiveBuffer.ToString();
            
            // 如果包含换行符，提取完整行
            if (bufferContent.Contains("\n"))
            {
                string[] lines = bufferContent.Split(new[] { '\n' }, StringSplitOptions.None);
                
                // 处理完整行
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    string line = lines[i].TrimEnd('\r');
                    if (!string.IsNullOrEmpty(line))
                    {
                        TextReceived?.Invoke(this, line);
                    }
                }
                
                // 保留最后的不完整数据
                receiveBuffer.Clear();
                receiveBuffer.Append(lines[lines.Length - 1]);
            }
            // 如果缓冲区太大（超过1MB），强制输出并清空
            else if (bufferContent.Length > 1024 * 1024)
            {
                TextReceived?.Invoke(this, bufferContent);
                receiveBuffer.Clear();
            }
        }

        /// <summary>
        /// 串口错误事件处理
        /// </summary>
        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            string errorMsg = $"串口错误: {e.EventType}";
            Logger.GetInstance().Error(LogLevel.Error, errorMsg);
            ErrorOccurred?.Invoke(this, errorMsg);
            
            // 某些错误需要重连
            if (e.EventType == SerialError.Frame || e.EventType == SerialError.Overrun)
            {
                HandleDisconnection();
            }
        }

        #endregion

        #region 自动重连

        /// <summary>
        /// 处理连接断开
        /// </summary>
        private void HandleDisconnection()
        {
            lock (lockObject)
            {
                if (!isConnected)
                    return;

                isConnected = false;
                Logger.GetInstance().Warn(LogLevel.Warn, $"串口连接断开: {portName}");
                ConnectionStatusChanged?.Invoke(this, false);

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
            reconnectTimer = new System.Threading.Timer(Reconnect, null, ReconnectInterval, Timeout.Infinite);
            Logger.GetInstance().Info(LogLevel.Info, $"串口将在 {ReconnectInterval / 1000} 秒后尝试重连");
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
        private void Reconnect(object state)
        {
            if (isConnected || string.IsNullOrEmpty(portName))
                return;

            if (reconnectAttempts >= MaxReconnectAttempts)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"已达到最大重连尝试次数 ({MaxReconnectAttempts})，停止自动重连");
                return;
            }

            reconnectAttempts++;
            Logger.GetInstance().Info(LogLevel.Info, $"尝试重连串口 (第 {reconnectAttempts} 次)");

            try
            {
                Open(portName, baudRate, parity, dataBits, stopBits);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().ErrorException(ex, $"串口重连失败: {ex.Message}");

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

        #region 高级功能

        /// <summary>
        /// 发送命令并等待响应
        /// </summary>
        /// <param name="command">要发送的命令</param>
        /// <param name="timeout">超时时间(毫秒)</param>
        /// <returns>接收到的响应</returns>
        public async Task<string> SendCommandAndWaitResponse(string command, int timeout = 5000)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("串口未连接");
            }

            string response = null;
            var responseReceived = new ManualResetEventSlim(false);

            // 临时订阅文本接收事件
            EventHandler<string> handler = (sender, text) =>
            {
                response = text;
                responseReceived.Set();
            };

            try
            {
                TextReceived += handler;
                
                // 发送命令
                SendLine(command);

                // 等待响应
                bool received = responseReceived.Wait(timeout);
                
                if (!received)
                {
                    throw new TimeoutException($"等待响应超时 ({timeout}ms)");
                }

                return response;
            }
            finally
            {
                TextReceived -= handler;
                responseReceived.Dispose();
            }
        }

        /// <summary>
        /// 清空接收缓冲区
        /// </summary>
        public void ClearReceiveBuffer()
        {
            lock (lockObject)
            {
                receiveBuffer.Clear();
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.DiscardInBuffer();
                }
            }
        }

        /// <summary>
        /// 清空发送缓冲区
        /// </summary>
        public void ClearSendBuffer()
        {
            lock (lockObject)
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.DiscardOutBuffer();
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
