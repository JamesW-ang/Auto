using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace COTUI.通用功能类.Hardware
{
    /// <summary>
    /// Modbus TCP 设备通信类
    /// 
    /// <para><b>功能：</b></para>
    /// - 标准Modbus TCP协议
    /// - 读写线圈/寄存器
    /// - 自动重连
    /// - 适用于：PLC、IO模块、传感器等
    /// 
    /// <para><b>使用示例：</b></para>
    /// <code>
    /// var plc = new ModbusTcpDevice("PLC-01", "192.168.1.10", 502);
    /// await plc.ConnectAsync();
    /// 
    /// // 读取保持寄存器
    /// ushort[] values = await plc.ReadHoldingRegistersAsync(1, 0, 10);
    /// 
    /// // 写入单个线圈
    /// await plc.WriteSingleCoilAsync(1, 100, true);
    /// </code>
    /// </summary>
    public class ModbusTcpDevice : HardwareDeviceBase
    {
        #region 字段

        private readonly string ipAddress;
        private readonly int port;
        private TcpClient tcpClient;
        private NetworkStream stream;
        private ushort transactionId = 0;

        #endregion

        #region 属性

        public override string DeviceName { get; }

        public string IPAddress => ipAddress;
        public int Port => port;

        #endregion

        #region 构造函数

        public ModbusTcpDevice(string deviceName, string ipAddress, int port = 502)
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
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                logger.ErrorException(ex, $"[{DeviceName}] TCP连接失败");
                return false;
            }
        }

        protected override async Task DisconnectDeviceAsync()
        {
            stream?.Close();
            stream?.Dispose();
            tcpClient?.Close();
            tcpClient?.Dispose();
            
            await Task.CompletedTask;
        }

        #endregion

        #region Modbus 功能码实现

        /// <summary>
        /// 读取线圈状态 (功能码 0x01)
        /// </summary>
        /// <param name="slaveAddress">从站地址</param>
        /// <param name="startAddress">起始地址</param>
        /// <param name="count">数量</param>
        public async Task<bool[]> ReadCoilsAsync(byte slaveAddress, ushort startAddress, ushort count)
        {
            byte[] request = BuildModbusRequest(slaveAddress, 0x01, startAddress, count);
            byte[] response = await SendModbusRequestAsync(request);
            
            return ParseCoilsResponse(response, count);
        }

        /// <summary>
        /// 读取离散输入 (功能码 0x02)
        /// </summary>
        public async Task<bool[]> ReadDiscreteInputsAsync(byte slaveAddress, ushort startAddress, ushort count)
        {
            byte[] request = BuildModbusRequest(slaveAddress, 0x02, startAddress, count);
            byte[] response = await SendModbusRequestAsync(request);
            
            return ParseCoilsResponse(response, count);
        }

        /// <summary>
        /// 读取保持寄存器 (功能码 0x03)
        /// </summary>
        public async Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort count)
        {
            byte[] request = BuildModbusRequest(slaveAddress, 0x03, startAddress, count);
            byte[] response = await SendModbusRequestAsync(request);
            
            return ParseRegistersResponse(response);
        }

        /// <summary>
        /// 读取输入寄存器 (功能码 0x04)
        /// </summary>
        public async Task<ushort[]> ReadInputRegistersAsync(byte slaveAddress, ushort startAddress, ushort count)
        {
            byte[] request = BuildModbusRequest(slaveAddress, 0x04, startAddress, count);
            byte[] response = await SendModbusRequestAsync(request);
            
            return ParseRegistersResponse(response);
        }

        /// <summary>
        /// 写入单个线圈 (功能码 0x05)
        /// </summary>
        public async Task<bool> WriteSingleCoilAsync(byte slaveAddress, ushort address, bool value)
        {
            ushort coilValue = value ? (ushort)0xFF00 : (ushort)0x0000;
            byte[] request = BuildModbusRequest(slaveAddress, 0x05, address, coilValue);
            byte[] response = await SendModbusRequestAsync(request);
            
            return response != null && response.Length > 0;
        }

        /// <summary>
        /// 写入单个寄存器 (功能码 0x06)
        /// </summary>
        public async Task<bool> WriteSingleRegisterAsync(byte slaveAddress, ushort address, ushort value)
        {
            byte[] request = BuildModbusRequest(slaveAddress, 0x06, address, value);
            byte[] response = await SendModbusRequestAsync(request);
            
            return response != null && response.Length > 0;
        }

        /// <summary>
        /// 写入多个线圈 (功能码 0x0F)
        /// </summary>
        public async Task<bool> WriteMultipleCoilsAsync(byte slaveAddress, ushort startAddress, bool[] values)
        {
            byte[] request = BuildWriteMultipleCoilsRequest(slaveAddress, startAddress, values);
            byte[] response = await SendModbusRequestAsync(request);
            
            return response != null && response.Length > 0;
        }

        /// <summary>
        /// 写入多个寄存器 (功能码 0x10)
        /// </summary>
        public async Task<bool> WriteMultipleRegistersAsync(byte slaveAddress, ushort startAddress, ushort[] values)
        {
            byte[] request = BuildWriteMultipleRegistersRequest(slaveAddress, startAddress, values);
            byte[] response = await SendModbusRequestAsync(request);
            
            return response != null && response.Length > 0;
        }

        #endregion

        #region Modbus 协议处理

        private byte[] BuildModbusRequest(byte slaveAddress, byte functionCode, ushort address, ushort value)
        {
            byte[] request = new byte[12];
            
            // MBAP Header
            ushort tid = ++transactionId;
            request[0] = (byte)(tid >> 8);
            request[1] = (byte)(tid & 0xFF);
            request[2] = 0x00; // Protocol ID
            request[3] = 0x00;
            request[4] = 0x00; // Length
            request[5] = 0x06;
            
            // PDU
            request[6] = slaveAddress;
            request[7] = functionCode;
            request[8] = (byte)(address >> 8);
            request[9] = (byte)(address & 0xFF);
            request[10] = (byte)(value >> 8);
            request[11] = (byte)(value & 0xFF);
            
            return request;
        }

        private byte[] BuildWriteMultipleCoilsRequest(byte slaveAddress, ushort startAddress, bool[] values)
        {
            int byteCount = (values.Length + 7) / 8;
            byte[] request = new byte[13 + byteCount];
            
            // MBAP Header
            ushort tid = ++transactionId;
            request[0] = (byte)(tid >> 8);
            request[1] = (byte)(tid & 0xFF);
            request[2] = 0x00;
            request[3] = 0x00;
            request[4] = (byte)((7 + byteCount) >> 8);
            request[5] = (byte)((7 + byteCount) & 0xFF);
            
            // PDU
            request[6] = slaveAddress;
            request[7] = 0x0F; // Function code
            request[8] = (byte)(startAddress >> 8);
            request[9] = (byte)(startAddress & 0xFF);
            request[10] = (byte)(values.Length >> 8);
            request[11] = (byte)(values.Length & 0xFF);
            request[12] = (byte)byteCount;
            
            // Coil values
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i])
                {
                    int byteIndex = 13 + (i / 8);
                    int bitIndex = i % 8;
                    request[byteIndex] |= (byte)(1 << bitIndex);
                }
            }
            
            return request;
        }

        private byte[] BuildWriteMultipleRegistersRequest(byte slaveAddress, ushort startAddress, ushort[] values)
        {
            byte[] request = new byte[13 + values.Length * 2];
            
            // MBAP Header
            ushort tid = ++transactionId;
            request[0] = (byte)(tid >> 8);
            request[1] = (byte)(tid & 0xFF);
            request[2] = 0x00;
            request[3] = 0x00;
            request[4] = (byte)((7 + values.Length * 2) >> 8);
            request[5] = (byte)((7 + values.Length * 2) & 0xFF);
            
            // PDU
            request[6] = slaveAddress;
            request[7] = 0x10; // Function code
            request[8] = (byte)(startAddress >> 8);
            request[9] = (byte)(startAddress & 0xFF);
            request[10] = (byte)(values.Length >> 8);
            request[11] = (byte)(values.Length & 0xFF);
            request[12] = (byte)(values.Length * 2);
            
            // Register values
            for (int i = 0; i < values.Length; i++)
            {
                request[13 + i * 2] = (byte)(values[i] >> 8);
                request[14 + i * 2] = (byte)(values[i] & 0xFF);
            }
            
            return request;
        }

        private async Task<byte[]> SendModbusRequestAsync(byte[] request)
        {
            if (!isConnected || stream == null)
            {
                OnError("设备未连接");
                return null;
            }

            try
            {
                await stream.WriteAsync(request, 0, request.Length);
                
                byte[] response = new byte[256];
                int bytesRead = await stream.ReadAsync(response, 0, response.Length);
                
                byte[] result = new byte[bytesRead];
                Array.Copy(response, result, bytesRead);
                
                return result;
            }
            catch (Exception ex)
            {
                logger.ErrorException(ex, $"[{DeviceName}] Modbus请求失败");
                OnConnectionLost();
                return null;
            }
        }

        private bool[] ParseCoilsResponse(byte[] response, ushort count)
        {
            if (response == null || response.Length < 9)
                return null;

            int byteCount = response[8];
            bool[] coils = new bool[count];
            
            for (int i = 0; i < count; i++)
            {
                int byteIndex = 9 + (i / 8);
                int bitIndex = i % 8;
                coils[i] = (response[byteIndex] & (1 << bitIndex)) != 0;
            }
            
            return coils;
        }

        private ushort[] ParseRegistersResponse(byte[] response)
        {
            if (response == null || response.Length < 9)
                return null;

            int byteCount = response[8];
            int registerCount = byteCount / 2;
            ushort[] registers = new ushort[registerCount];
            
            for (int i = 0; i < registerCount; i++)
            {
                registers[i] = (ushort)((response[9 + i * 2] << 8) | response[10 + i * 2]);
            }
            
            return registers;
        }

        #endregion
    }
}
