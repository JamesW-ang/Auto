# COTUI 项目文档索引

> 📚 所有项目文档集中管理，方便查阅

---

## 📖 文档目录

### 🌐 全局变量系统

#### [Gvar 快速参考卡片](./Gvar快速参考卡片.md)
快速查询手册，包含常用API和模式示例
- 快捷访问单例服务
- 用户与权限管理
- 生产统计（自动缓存）
- 设备连接状态
- 常用模式示例

#### [Gvar 使用指南](./Gvar使用指南.md)
完整的全局变量使用文档（1058行）
- 核心层、业务层、扩展层详细说明
- 典型使用场景示例
- 性能分析与最佳实践
- 常见问题解答

#### [迁移指南](./迁移指南.md)
从旧代码迁移到全局变量（540行）
- 4阶段迁移计划
- VS Code批量替换命令
- 验证脚本
- 性能对比分析

#### [应用总结](./应用总结.md)
全局变量应用总结报告（391行）
- 执行情况统计
- 性能提升分析
- 影响范围详情

---

### 🔧 硬件通信框架

#### [Hardware 框架说明](./Hardware框架说明.md)
硬件设备通信架构
- 接口设计（IHardwareDevice）
- Modbus TCP实现（8个功能码）
- 通用TCP设备
- 扩展指南

#### [HardwareTestForm 使用说明](./HardwareTestForm使用说明.md)
测试工具文档（449行）
- 启动方法
- Modbus TCP测试步骤
- 通用TCP测试步骤
- 模拟器配置
- FAQ和日志分析

---

## 🔍 快速查找

### 需要了解全局变量？
1. **新手入门** → [Gvar 快速参考卡片](./Gvar快速参考卡片.md)
2. **详细使用** → [Gvar 使用指南](./Gvar使用指南.md)
3. **代码迁移** → [迁移指南](./迁移指南.md)

### 需要硬件通信？
1. **架构了解** → [Hardware 框架说明](./Hardware框架说明.md)
2. **功能测试** → [HardwareTestForm 使用说明](./HardwareTestForm使用说明.md)

### 查看应用效果？
→ [应用总结](./应用总结.md)

---

## 📊 推荐阅读顺序

### 初次接触项目
1. [应用总结](./应用总结.md) - 了解项目整体情况
2. [Gvar 快速参考卡片](./Gvar快速参考卡片.md) - 快速上手全局变量
3. [Hardware 框架说明](./Hardware框架说明.md) - 了解硬件通信架构

### 深入开发
1. [Gvar 使用指南](./Gvar使用指南.md) - 掌握全局变量所有功能
2. [HardwareTestForm 使用说明](./HardwareTestForm使用说明.md) - 学习硬件测试

### 维护升级
1. [迁移指南](./迁移指南.md) - 了解如何迁移旧代码
2. [应用总结](./应用总结.md) - 查看优化效果

---

## 📈 文档统计

| 类别 | 文档数 | 总行数 |
|------|--------|--------|
| 全局变量系统 | 4 | 2,161 |
| 硬件通信框架 | 2 | 449+ |
| **总计** | **6** | **2,610+** |

---

**项目地址**: https://github.com/JamesW-ang/Auto  
**最后更新**: 2025-11-29

---

<details>
<summary>📝 Gvar 快速参考卡片内容预览</summary>

# Gvar 快速参考卡片 (Quick Reference)

## 🚀 快捷访问单例服务
```csharp
Gvar.Logger.Info("日志消息");              // 日志服务
Gvar.DB.ExecuteQuery("SELECT ...");      // 数据库服务  
Gvar.Config.GetValue("Group", "Key");    // 配置管理器
Gvar.Mqtt.PublishAsync("topic", "msg");  // MQTT服务
```

## 👤 用户与权限
```csharp
Gvar.User = "admin";                     // 设置当前用户
Gvar.CurrentStation = "工站1";           // 设置当前工站

bool isAdmin = Gvar.Permission.IsAdmin;         // 是否管理员（≥5）
bool isOperator = Gvar.Permission.IsOperator;   // 是否操作员（≥3）
Gvar.Permission.ClearCache();                   // 注销时清除缓存
```

## 📊 生产统计（自动缓存5秒）
```csharp
int total = Gvar.Production.TodayTotalCount;     // 今日总产量
int ok = Gvar.Production.TodayOkCount;           // 今日合格数
int ng = Gvar.Production.TodayNgCount;           // 今日不良数
double rate = Gvar.Production.TodayYieldRate;    // 今日良率（%）

Gvar.Production.RefreshCache();                  // 强制刷新缓存
```

## 🌐 设备连接状态
```csharp
Gvar.Communication.IsPlcConnected = true;        // 设置PLC状态
Gvar.Communication.IsVisionConnected = true;     // 设置视觉状态

bool mqttOk = Gvar.Communication.IsMqttConnected;        // 获取MQTT状态
bool allOk = Gvar.Communication.IsAllCriticalDevicesConnected;  // 所有关键设备是否就绪
string summary = Gvar.Communication.GetConnectionSummary();      // 状态摘要
```

## ⚙️ 系统配置
```csharp
Gvar.System.IsDebugMode = true;                  // 启用调试模式
Gvar.System.Version = "1.2.0";                   // 设置版本号
TimeSpan uptime = Gvar.System.Uptime;            // 应用运行时长
```

## 🖥️ UI状态
```csharp
Gvar.UI.MainForm = new MainForm();               // 设置主窗体引用
Gvar.UI.MainForm?.BringToFront();                // 激活主窗体
Gvar.UI.CurrentPageName = "生产监控";             // 设置当前页面名
```

## 💾 临时数据传递
```csharp
// 窗体A：设置数据
Gvar.TempData.Set("ProductId", 12345);
Gvar.TempData.Set("EditMode", true);

// 窗体B：获取数据
int id = Gvar.TempData.Get<int>("ProductId");
bool edit = Gvar.TempData.Get<bool>("EditMode");

// 使用完毕：清除数据
Gvar.TempData.Clear("ProductId");
Gvar.TempData.Clear();  // 清除所有
```

## 📝 常用模式

### 登录流程
```csharp
private void OnLoginSuccess(string username)
{
    Gvar.User = username;
    Gvar.Permission.CurrentUserLevel = Gvar.DB.GetUserPermissionLevel(username);
    UpdateUIPermissions();
}

private void Logout()
{
    Gvar.User = "";
    Gvar.Permission.ClearCache();
}
```

### 仪表盘刷新（缓存优化）
```csharp
private void Timer_Tick(object sender, EventArgs e)
{
    // 每秒调用，但5秒内只查询一次数据库
    lblTotal.Text = Gvar.Production.TodayTotalCount.ToString();
    lblOk.Text = Gvar.Production.TodayOkCount.ToString();
    lblYield.Text = $"{Gvar.Production.TodayYieldRate:F2}%";
}
```

### 插入数据后刷新
```csharp
private void SaveProductionData(ProductionData data)
{
    Gvar.DB.ExecuteNonQuery("INSERT INTO ProductionData ...");
    Gvar.Production.RefreshCache();  // 立即刷新缓存
}
```

### 权限控制
```csharp
private void UpdateButtonPermissions()
{
    btnDelete.Enabled = Gvar.Permission.IsAdmin;
    btnEdit.Enabled = Gvar.Permission.IsOperator;
    btnView.Enabled = !Gvar.Permission.IsViewOnly;
}
```

### 设备连接管理
```csharp
private void OnDeviceConnected(object sender, EventArgs e)
{
    Gvar.Communication.IsPlcConnected = true;
    statusLabel.Text = Gvar.Communication.GetConnectionSummary();
    
    if (Gvar.Communication.IsAllCriticalDevicesConnected)
    {
        btnStart.Enabled = true;
    }
}
```

## ⚡ 性能提示

✅ **推荐**：使用 `Gvar` 快捷属性
```csharp
Gvar.Logger.Info("消息");  // 零开销
```

❌ **不推荐**：重复调用 `GetInstance()`
```csharp
Logger.GetInstance().Info("消息");  // 每次都调用单例查找
```

✅ **推荐**：依赖自动缓存
```csharp
// 仪表盘每秒刷新，但5秒才查询一次数据库
int count = Gvar.Production.TodayTotalCount;
```

✅ **推荐**：数据变更后主动刷新
```csharp
Gvar.DB.ExecuteNonQuery("INSERT ...");
Gvar.Production.RefreshCache();  // 立即刷新
```

## 📦 内存与性能

| 指标 | 数值 |
|------|------|
| 总内存占用 | < 2 KB |
| 性能影响 | < 0.01% |
| 缓存时间 | 5 秒 |
| 性能提升 | 减少 80% 数据库查询 |

---

**详细文档**: 
- [Gvar使用指南.md](./Gvar使用指南.md)
- [迁移指南.md](./迁移指南.md)

**版本**: 1.0.0  
**更新**: 2025-11-29
