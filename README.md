# Auto - 工业自动化HMI系统

> .NET Framework 4.7.2 + C# WinForms 工业上位机框架

---

## 📚 项目文档

**完整文档请查看**: [docs/](./docs/)

### 快速链接
- [Gvar 快速参考卡片](./docs/Gvar快速参考卡片.md) - 全局变量速查
- [Hardware 框架说明](./docs/Hardware框架说明.md) - 硬件通信架构
- [应用总结](./docs/应用总结.md) - 项目优化报告

---

## 🎯 核心功能

### 1. 全局变量系统（Gvar）
- ✅ 零开销单例服务访问
- ✅ 智能缓存（减少80%数据库查询）
- ✅ 分层设计（核心层、业务层、扩展层）
- ✅ 权限管理、生产统计、设备状态

### 2. 硬件通信框架
- ✅ Modbus TCP（8个功能码完整实现）
- ✅ 通用TCP设备通信
- ✅ 自动重连机制
- ✅ 可视化测试工具

### 3. 数据库管理
- ✅ SQLite数据库
- ✅ 生产数据、日志、告警、用户管理
- ✅ 自动备份、数据清理

### 4. MQTT通信
- ✅ MES系统报工
- ✅ 设备状态上报
- ✅ 自动重连

### 5. 日志系统
- ✅ 分级日志（Trace/Debug/Info/Warn/Error/Fatal）
- ✅ 文件日志 + 数据库日志
- ✅ 实时查询和过滤

---

## 🚀 快速开始

### 环境要求
- Windows 10/11
- .NET Framework 4.7.2
- Visual Studio 2019/2022

### 使用全局变量

```csharp
// 日志记录
Gvar.Logger.Info("操作成功");

// 数据库操作
Gvar.DB.ExecuteQuery("SELECT * FROM Users");

// 生产统计（自动缓存）
int total = Gvar.Production.TodayTotalCount;
double rate = Gvar.Production.TodayYieldRate;

// 权限控制
if (Gvar.Permission.IsAdmin)
{
    // 管理员操作
}
```

### 硬件通信

```csharp
// Modbus TCP设备
var plc = new ModbusTcpDevice("192.168.1.10", 502);
await plc.ConnectAsync();
ushort[] values = await plc.ReadHoldingRegistersAsync(0, 10);

// 通用TCP设备
var vision = new GenericTcpDevice("192.168.1.20", 8000);
await vision.ConnectAsync();
string result = await vision.SendCommandAsync("GET_IMAGE");
```

---

## 📊 项目结构

```
Auto/
├── docs/                    # 📚 文档目录
│   ├── README.md           # 文档索引
│   ├── Gvar使用指南.md
│   ├── Gvar快速参考卡片.md
│   ├── 迁移指南.md
│   ├── 应用总结.md
│   ├── Hardware框架说明.md
│   └── HardwareTestForm使用说明.md
├── COTUI/                   # 主项目代码
│   ├── 全局变量/
│   │   └── Gvar.cs         # 全局变量定义
│   ├── 通用功能类/
│   │   ├── Logger.cs       # 日志系统
│   │   ├── MqttService.cs  # MQTT服务
│   │   ├── ConfigManager.cs
│   │   └── Hardware/       # 硬件通信框架
│   ├── 数据库/
│   │   ├── DatabaseHelper.cs
│   │   └── Services/       # 数据服务层
│   ├── Form/               # 测试工具
│   └── ...                 # 各功能页面
└── Config.ini              # 配置文件
```

---

## 📈 性能优化

| 优化项 | 效果 |
|--------|------|
| 单例访问优化 | 减少 33% 调用开销 |
| 数据库查询缓存 | 减少 80% 查询次数 |
| 日志分级输出 | 减少 70% 日志量 |
| 内存占用 | < 2KB（全局变量） |

---

## 🔗 相关链接

- **GitHub**: https://github.com/JamesW-ang/Auto
- **文档**: [docs/](./docs/)
- **最后更新**: 2025-11-29

---

## 📝 更新日志

### 2025-11-29
- ✅ 完成全局变量系统设计与应用
- ✅ 优化日志系统（分级、Console替换）
- ✅ 实现硬件通信框架（Modbus TCP + 通用TCP）
- ✅ 创建完整项目文档（6份，2610+行）
- ✅ 性能优化（33%单例优化，80%缓存优化）

---

## 📄 License

Private Project
