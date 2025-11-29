# 硬件设备通信框架

## 架构设计

```
IHardwareDevice (接口)
    ↓
HardwareDeviceBase (抽象基类)
    ├── 连接管理
    ├── 自动重连
    ├── 日志记录
    └── 事件通知
    ↓ 继承
    ├── ModbusTcpDevice (Modbus TCP设备)
    │   ├── 读写线圈
    │   ├── 读写寄存器
    │   └── 适用于：PLC、IO模块、传感器
    │
    └── GenericTcpDevice (通用TCP设备)
        ├── 自定义协议
        ├── 文本/字节收发
        └── 适用于：视觉系统、运动控制卡

独立服务（不继承）：
    ├── TcpSocketService (通用TCP通信)
    └── MqttService (MES报工)
```

## 使用示例

### 1. Modbus TCP设备 (PLC/IO模块)

```csharp
// 创建设备
var plc = new ModbusTcpDevice("PLC-01", "192.168.1.10", 502);

// 订阅事件
plc.ConnectionStatusChanged += (s, connected) => {
    Console.WriteLine($"PLC {(connected ? "已连接" : "断开")}");
};

// 连接
await plc.ConnectAsync();

// 读取保持寄存器 (从站1, 地址0, 读10个)
ushort[] registers = await plc.ReadHoldingRegistersAsync(1, 0, 10);

// 写入单个线圈 (从站1, 地址100, ON)
await plc.WriteSingleCoilAsync(1, 100, true);

// 写入多个寄存器
ushort[] data = { 100, 200, 300 };
await plc.WriteMultipleRegistersAsync(1, 0, data);

// 断开
await plc.DisconnectAsync();
```

### 2. 通用TCP设备 (视觉系统/运动控制卡)

```csharp
// 创建设备
var vision = new GenericTcpDevice("Vision-01", "192.168.1.20", 3000);

// 订阅数据接收事件
vision.TextReceived += (s, text) => {
    Console.WriteLine($"收到: {text}");
};

// 连接
await vision.ConnectAsync();

// 发送命令
await vision.SendTextAsync("TRIGGER\r\n");

// 发送命令并等待响应
string result = await vision.SendCommandAsync("READ\r\n", 3000);

// 断开
await vision.DisconnectAsync();
```

## 测试方法

### 方法1: 使用测试窗体

1. 在VS2022中打开项目
2. 修改 `Program.cs`，临时启动测试窗体：

```csharp
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    
    // 临时测试硬件设备
    Application.Run(new COTUI.Form.HardwareTestForm());
    
    // 正式运行
    // Application.Run(new MainForm());
}
```

3. 运行程序，选择对应的Tab页进行测试

### 方法2: 单元测试

```csharp
[Test]
public async Task TestModbusConnection()
{
    var device = new ModbusTcpDevice("Test", "192.168.1.10", 502);
    bool connected = await device.ConnectAsync();
    Assert.IsTrue(connected);
}
```

### 方法3: 模拟器测试

如果没有真实硬件，可以使用：

- **Modbus**: [ModbusSim](https://sourceforge.net/projects/modrssim/)
- **通用TCP**: `nc -l 3000` (macOS/Linux) 或 [Hercules](https://www.hw-group.com/software/hercules-setup-utility)

## 扩展新设备

### 示例：添加EtherCAT设备

```csharp
public class EtherCATDevice : HardwareDeviceBase
{
    public override string DeviceName => "EtherCAT Master";
    
    protected override async Task<bool> ConnectDeviceAsync()
    {
        // 实现EtherCAT连接逻辑
        // 使用专用库如 TwinCAT ADS, SOEM等
        return await Task.FromResult(true);
    }
    
    protected override async Task DisconnectDeviceAsync()
    {
        // 实现断开逻辑
        await Task.CompletedTask;
    }
    
    // EtherCAT特定方法
    public void ConfigureSlave(int position, byte[] config) { }
    public void StartCyclic() { }
}
```

## 配置文件集成

可以在 `Config.ini` 中添加硬件配置：

```ini
[Hardware]
PLC1_IP=192.168.1.10
PLC1_Port=502
PLC1_SlaveAddr=1

Vision_IP=192.168.1.20
Vision_Port=3000
```

读取配置：

```csharp
var config = ConfigManager.Instance;
string plcIP = config.GetValue("Hardware", "PLC1_IP", "192.168.1.10");
int plcPort = config.GetIntValue("Hardware", "PLC1_Port", 502);

var plc = new ModbusTcpDevice("PLC-1", plcIP, plcPort);
```

## 常见问题

**Q: Modbus读取失败？**
A: 检查从站地址、起始地址、寄存器数量是否正确

**Q: 连接超时？**
A: 确认IP地址和端口，检查网络防火墙设置

**Q: 自动重连不生效？**
A: 设置 `device.EnableAutoReconnect = true`

**Q: 如何查看日志？**
A: 使用Logger.GetInstance()查看控制台输出
