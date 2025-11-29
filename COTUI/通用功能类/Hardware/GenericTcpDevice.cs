using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace COTUI.通用功能类.Hardware
{
    /// <summary>
    /// 通用TCP设备 - 用于自定义协议的硬件设备
    /// 
    /// <para><b>适用场景：</b></para>
    /// - 视觉系统（Cognex、Keyence等）
    /// - 运动控制卡（雷赛、固高等）
    /// - 自定义协议的工业设备
    /// 
    /// <para><b>使用示例：</b></para>
    /// <code>
    /// var vision = new GenericTcpDevice("Vision-01", "192.168.1.20", 3000);
    /// vision.DataReceived += (s, data) => {
    ///     string response = Encoding.ASCII.GetString(data);
    ///     Console.WriteLine($"收到: {response}");
    /// };
    /// 
    /// await vision.ConnectAsync();
    /// await vision.SendTextAsync("TRIGGER\r\n");
    /// </code>
    /// </summary>
    public class GenericTcpDevice : HardwareDeviceBase
    {
        #region 字段

        private readonly string ipAddress;
        private readonly int port;
        private TcpClient tcpClient;
        private NetworkStream stream;
        private readonly byte[] receiveBuffer = new byte[8192];
        private bool isReceiving = false;

        #endregion

        #region 属性

        public override string DeviceName { get; }

        public string IPAddress => ipAddress;
        public int Port => port;

        #endregion

        #region 事件

        /// <summary>
        /// 接收到数据事件
        /// </summary>
        public event EventHandler<byte[]> DataReceived;

        /// <summary>
        /// 接收到文本数据事件
        /// </summary>
        public event EventHandler<string> TextReceived;

        #endregion

        #region 构造函数

        public GenericTcpDevice(string deviceName, string ipAddress, int port)
        {
            this.DeviceName = deviceName;
            this.ipAddress = ipAddress;
            this.port = port;
        }

        #endregion

        #region 连接管理

        protected override async Task<bool> ConnectDeviceAsync()
        {
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(ipAddress, port);
                
                if (tcpClient.Connected)
                {
                    stream = tcpClient.GetStream();
                    StartReceiving();
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"[{DeviceName}] TCP连接失败");
                return false;
            }
        }

        protected override async Task DisconnectDeviceAsync()
        {
            isReceiving = false;
            
            stream?.Close();
            stream?.Dispose();
            tcpClient?.Close();
            tcpClient?.Dispose();
            
            await Task.CompletedTask;
        }

        #endregion

        #region 数据收发

        /// <summary>
        /// 发送字节数据
        /// </summary>
        public async Task<bool> SendBytesAsync(byte[] data)
        {
            if (!isConnected || stream == null)
            {
                OnError("设备未连接");
                return false;
            }

            try
            {
                await stream.WriteAsync(data, 0, data.Length);
                Gvar.Logger.Log($"[{DeviceName}] 发送 {data.Length} 字节");
                return true;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"[{DeviceName}] 发送数据失败");
                OnConnectionLost();
                return false;
            }
        }

        /// <summary>
        /// 发送文本数据
        /// </summary>
        public async Task<bool> SendTextAsync(string text, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.ASCII;
            byte[] data = encoding.GetBytes(text);
            return await SendBytesAsync(data);
        }

        /// <summary>
        /// 发送命令并等待响应（简化版）
        /// </summary>
        public async Task<string> SendCommandAsync(string command, int timeoutMs = 3000)
        {
            string response = null;
            var tcs = new TaskCompletionSource<string>();

            EventHandler<string> handler = (s, data) =>
            {
                tcs.TrySetResult(data);
            };

            try
            {
                TextReceived += handler;
                await SendTextAsync(command);
                
                var timeoutTask = Task.Delay(timeoutMs);
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);
                
                if (completedTask == tcs.Task)
                {
                    response = await tcs.Task;
                }
                else
                {
                    OnError("命令响应超时");
                }
            }
            finally
            {
                TextReceived -= handler;
            }

            return response;
        }

        private async void StartReceiving()
        {
            isReceiving = true;
            
            while (isReceiving && isConnected)
            {
                try
                {
                    if (stream != null && stream.DataAvailable)
                    {
                        int bytesRead = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                        
                        if (bytesRead > 0)
                        {
                            byte[] data = new byte[bytesRead];
                            Array.Copy(receiveBuffer, data, bytesRead);
                            
                            DataReceived?.Invoke(this, data);
                            
                            string text = Encoding.ASCII.GetString(data);
                            TextReceived?.Invoke(this, text);
                            
                            Gvar.Logger.Log($"[{DeviceName}] 接收 {bytesRead} 字节");
                        }
                    }
                    
                    await Task.Delay(10);
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, $"[{DeviceName}] 接收数据异常");
                    OnConnectionLost();
                    break;
                }
            }
        }

        #endregion
    }
}
