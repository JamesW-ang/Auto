using CCWin;
using COTUI.通用功能类;
using COTUI.数据库.Services;
using COTUI.数据库.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI
{
    public partial class MainPage : Form
    {
        // 使用全局变量 Gvar.Logger 访问日志服务
        private AlarmService alarmService = new AlarmService();
        private ProductionDataService productionService = new ProductionDataService();
        // 使用全局变量 Gvar.Mqtt 访问 MQTT 服务
        
        private const int MAX_LOG_LINES = 100;
        private Queue<string> logQueue = new Queue<string>();

        public MainPage()
        {
            InitializeComponent();
            InitializeLogDisplay();
            InitializeStatusIndicators();
            //  只订阅状态
            SubscribeMqttStatus(); 
        }

        /// <summary>
        /// 初始化日志显示 - 订阅Logger的实时事件
        /// </summary>
        private void InitializeLogDisplay()
        {
            // 配置日志文本框
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Consolas", 9);
            textBox1.WordWrap = false;
            textBox1.ScrollBars = ScrollBars.Both;

            // 关键：订阅Logger的实时日志事件
            Gvar.Logger.OnLogMessage += Logger_OnLogMessage;
            
            // 添加初始日志
            Gvar.Logger.Log(LogLevel.Info, "主页面加载完成");
        }

        /// <summary>
        /// 初始化状态指示器
        /// </summary>
        private void InitializeStatusIndicators()
        {
            try
            {
                // 设置每个指示器的初始状态和事件处理
                
                // 1. 生产模式 - 默认不激活
                statusIndicatorControl1.IsActive = false;
                statusIndicatorControl1.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(LogLevel.Info, $"生产模式: {(e.NewStatus ? "开启" : "关闭")}");
                };

                // 2. 空跑模式
                statusIndicatorControl2.IsActive = false;
                statusIndicatorControl2.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(LogLevel.Info, $"空跑模式: {(e.NewStatus ? "开启" : "关闭")}");
                };

                // 3. 首件模式
                statusIndicatorControl3.IsActive = false;
                statusIndicatorControl3.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(LogLevel.Info, $"首件模式: {(e.NewStatus ? "开启" : "关闭")}");
                };

                // 4. 返工模式
                statusIndicatorControl4.IsActive = false;
                statusIndicatorControl4.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(LogLevel.Info, $"返工模式: {(e.NewStatus ? "开启" : "关闭")}");
                };

                // 5. 安全门闭合 - 默认激活（绿色表示正常）
                statusIndicatorControl5.IsActive = true;
                statusIndicatorControl5.StatusChanged += (s, e) =>
                {
                    if (!e.NewStatus)
                    {
                        Gvar.Logger.Log(LogLevel.Warn, "警告: 安全门打开");
                        AddAlarm("安全门打开", "警告", "ALARM_DOOR");
                    }
                    else
                    {
                        Gvar.Logger.Log(LogLevel.Info, "安全门已闭合");
                    }
                };

                // 6. 机台照明
                statusIndicatorControl6.IsActive = false;
                statusIndicatorControl6.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(LogLevel.Info, $"机台照明: {(e.NewStatus ? "开启" : "关闭")}");
                };

                // 7. MQTT在线
                statusIndicatorControl7.IsActive = false;
                statusIndicatorControl7.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(e.NewStatus ? LogLevel.Info : LogLevel.Error, 
                        $"MQTT连接: {(e.NewStatus ? "在线" : "离线")}");
                };

                // 8. 急停按下 - 红色表示按下，绿色表示正常
                statusIndicatorControl8.IsActive = false;
                statusIndicatorControl8.InactiveColor = Color.LimeGreen; // 正常时绿色
                statusIndicatorControl8.ActiveColor = Color.Red;          // 急停时红色
                statusIndicatorControl8.StatusChanged += (s, e) =>
                {
                    if (e.NewStatus)
                    {
                        Gvar.Logger.Log(LogLevel.Fatal, "紧急停止: 急停按钮被按下");
                        AddAlarm("急停按钮被按下", "严重", "ALARM_ESTOP");
                    }
                    else
                    {
                        Gvar.Logger.Log(LogLevel.Info, "急停按钮已复位");
                    }
                };

                // 9. 正压气源
                statusIndicatorControl9.IsActive = true; // 默认正常
                statusIndicatorControl9.StatusChanged += (s, e) =>
                {
                    if (!e.NewStatus)
                    {
                        Gvar.Logger.Log(LogLevel.Error, "错误: 正压气源异常");
                        AddAlarm("正压气源异常", "严重", "ALARM_AIR_POS");
                    }
                    else
                    {
                        Gvar.Logger.Log(LogLevel.Info, "正压气源正常");
                    }
                };

                // 10. 负压气源
                statusIndicatorControl10.IsActive = true; // 默认正常
                statusIndicatorControl10.StatusChanged += (s, e) =>
                {
                    if (!e.NewStatus)
                    {
                        Gvar.Logger.Log(LogLevel.Error, "错误: 负压气源异常");
                        AddAlarm("负压气源异常", "严重", "ALARM_AIR_NEG");
                    }
                    else
                    {
                        Gvar.Logger.Log(LogLevel.Info, "负压气源正常");
                    }
                };

                // 11. MES连接
                statusIndicatorControl11.IsActive = false;
                statusIndicatorControl11.StatusChanged += (s, e) =>
                {
                    Gvar.Logger.Log(e.NewStatus ? LogLevel.Info : LogLevel.Warn, 
                        $"MES连接: {(e.NewStatus ? "在线" : "离线")}");
                };

                Gvar.Logger.Log(LogLevel.Info, "状态指示器初始化完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "初始化状态指示器失败");
            }
        }

        /// <summary>
        /// Logger事件处理 - 接收所有日志消息
        /// </summary>
        private void Logger_OnLogMessage(object sender, string logLine)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<object, string>(Logger_OnLogMessage), sender, logLine);
                    return;
                }

                // 添加到队列
                logQueue.Enqueue(logLine);

                // 限制日志行数
                while (logQueue.Count > MAX_LOG_LINES)
                {
                    logQueue.Dequeue();
                }

                // 更新显示
                textBox1.Text = string.Join(Environment.NewLine, logQueue);

                // 自动滚动到底部
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
            catch (Exception ex)
            {
                Gvar.Logger.Log(LogLevel.Debug, $"显示日志失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 添加报警日志（同时显示在报警信息表格、日志区域和保存到数据库）
        /// </summary>
        /// <param name="content">报警内容</param>
        /// <param name="level">报警等级</param>
        /// <param name="alarmCode">报警代码</param>
        public void AddAlarm(string content, string level = "警告", string alarmCode = "")
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string, string, string>(AddAlarm), content, level, alarmCode);
                    return;
                }

                // 添加到报警表格
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int rowIndex = dataGridView1.Rows.Add(time, content, level);

                // 根据等级设置颜色
                Color levelColor = GetAlarmColor(level);
                dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = levelColor;

                // 限制报警记录数量
                while (dataGridView1.Rows.Count > 50)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }

                // 💾 异步保存到数据库
                Task.Run(() =>
                {
                    try
                    {
                        var alarm = new AlarmModel
                        {
                            AlarmTime = DateTime.Now,
                            Level = level,
                            Content = content,
                            Station = "主工位",
                            AlarmCode = alarmCode,
                            IsHandled = false
                        };
                        alarmService.AddAlarm(alarm);
                    }
                    catch (Exception ex)
                    {
                        Gvar.Logger.ErrorException(ex, "保存报警到数据库失败");
                    }
                });

                // 使用Logger记录报警（自动触发事件显示）
                LogLevel logLevel = level == "严重" ? LogLevel.Error : 
                                   level == "警告" ? LogLevel.Warn : LogLevel.Info;
                Gvar.Logger.Log(logLevel, $"报警: {content}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.Log(LogLevel.Debug, $"添加报警失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 添加生产数据（同时显示在生产数据表格、保存到数据库、上报到MES）
        /// </summary>
        public void AddProductionData(string ringInfo, double testTime, string result = "OK", string batchNo = "")
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string, double, string, string>(AddProductionData), ringInfo, testTime, result, batchNo);
                    return;
                }

                string time = DateTime.Now.ToString("HH:mm:ss");
                dataGridView2.Rows.Add(time, ringInfo, $"{testTime:F2}ms");

                // 限制生产数据记录数量
                while (dataGridView2.Rows.Count > 100)
                {
                    dataGridView2.Rows.RemoveAt(0);
                }

                // 创建生产数据模型
                var production = new ProductionDataModel
                {
                    ProductionTime = DateTime.Now,
                    Station = "主工位",
                    ProductSN = ringInfo,
                    ProductInfo = ringInfo,
                    TestTime = testTime,
                    OverallResult = result,
                    Result = result,
                    BatchNo = batchNo,
                    MaterialBatchNo = batchNo,
                    Operator = Gvar.User ?? "System"
                };

                // 💾 保存到数据库
                Task.Run(() =>
                {
                    try
                    {
                        productionService.AddProductionData(production);
                    }
                    catch (Exception ex)
                    {
                        Gvar.Logger.ErrorException(ex, "保存生产数据到数据库失败");
                    }
                });

                // 📡 上报到MES（MQTT报工）- 现在只需一行代码！
                Task.Run(async () =>
                {
                    try
                    {
                        await Gvar.Mqtt.PublishWorkReportAsync(production);
                    }
                    catch (Exception ex)
                    {
                        Gvar.Logger.ErrorException(ex, "MQTT报工失败");
                    }
                });

                Gvar.Logger.Log(LogLevel.Debug, $"生产数据: {ringInfo}, 耗时: {testTime:F2}ms, 结果: {result}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.Log(LogLevel.Debug, $"添加生产数据失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 清除日志显示（不清除文件）
        /// </summary>
        public void ClearLogs()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ClearLogs));
                return;
            }

            logQueue.Clear();
            textBox1.Clear();
            Gvar.Logger.Log(LogLevel.Info, "日志显示已清除");
        }

        /// <summary>
        /// 清除报警按钮点击事件
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 清除报警表格
                dataGridView1.Rows.Clear();
                
                // 记录日志
                Gvar.Logger.Log(LogLevel.Info, "报警信息已清除");
                
                // 可选：显示提示
                // MessageBox.Show("报警信息已清除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "清除报警失败");
                MessageBox.Show($"清除报警失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 获取报警级别颜色
        /// </summary>
        private Color GetAlarmColor(string level)
        {
            switch (level)
            {
                case "严重": return Color.Red;
                case "警告": return Color.Orange;
                case "提示": return Color.Blue;
                default: return Color.Black;
            }
        }

        #region 公共方法：状态控制接口

        /// <summary>
        /// 设置生产模式
        /// </summary>
        public void SetProductionMode(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetProductionMode), enabled);
                return;
            }
            statusIndicatorControl1.IsActive = enabled;
        }

        /// <summary>
        /// 设置MQTT连接状态
        /// </summary>
        public void SetMQTTStatus(bool connected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetMQTTStatus), connected);
                return;
            }
            statusIndicatorControl7.IsActive = connected;
        }

        /// <summary>
        /// 设置安全门状态
        /// </summary>
        public void SetSafetyDoorStatus(bool closed)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetSafetyDoorStatus), closed);
                return;
            }
            statusIndicatorControl5.IsActive = closed;
        }

        /// <summary>
        /// 设置气源状态
        /// </summary>
        public void SetAirPressureStatus(bool positiveOk, bool negativeOk)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool, bool>(SetAirPressureStatus), positiveOk, negativeOk);
                return;
            }
            statusIndicatorControl9.IsActive = positiveOk;   // 正压
            statusIndicatorControl10.IsActive = negativeOk;  // 负压
        }

        /// <summary>
        /// 设置急停状态
        /// </summary>
        public void SetEmergencyStopStatus(bool pressed)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetEmergencyStopStatus), pressed);
                return;
            }
            statusIndicatorControl8.IsActive = pressed;
        }

        /// <summary>
        /// 设置MES连接状态
        /// </summary>
        public void SetMESStatus(bool connected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetMESStatus), connected);
                return;
            }
            statusIndicatorControl11.IsActive = connected;
        }

        #endregion

        #region MQTT状态监听（只显示状态，不管理连接）

        /// <summary>
        /// 订阅MQTT状态变化（只监听，不连接）
        /// </summary>
        private void SubscribeMqttStatus()
        {
            try
            {
                // 只订阅状态变化事件
                Gvar.Mqtt.ConnectionStatusChanged += MqttService_ConnectionStatusChanged;
                
                // 初始化状态显示
                SetMQTTStatus(Gvar.Mqtt.IsConnected);
                
                Gvar.Logger.Log(LogLevel.Debug, "已订阅MQTT状态监听");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "订阅MQTT状态失败");
            }
        }
        
        /// <summary>
        /// MQTT连接状态变化事件（只更新UI）
        /// </summary>
        private void MqttService_ConnectionStatusChanged(object sender, bool isConnected)
        {
            // 只更新UI上的MQTT状态指示器
            SetMQTTStatus(isConnected);
            
            if (isConnected)
            {
                Gvar.Logger.Log(LogLevel.Info, "✅ MQTT已连接");
            }
            else
            {
                Gvar.Logger.Log(LogLevel.Error, "❌ MQTT已断开");
            }
        }

        #endregion

        /// <summary>
        /// 模拟系统运行日志（用于测试）
        /// </summary>
        public void SimulateSystemLogs()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                Gvar.Logger.Log(LogLevel.Info, "初始化设备连接...");
                
                await Task.Delay(500);
                SetAirPressureStatus(true, true);
                Gvar.Logger.Log(LogLevel.Debug, "检查气源状态: 正常");
                
                await Task.Delay(500);
                SetSafetyDoorStatus(true);
                Gvar.Logger.Log(LogLevel.Debug, "检查安全门状态: 闭合");
                
                await Task.Delay(500);
                SetMQTTStatus(true);
                Gvar.Logger.Log(LogLevel.Info, "MQTT连接成功");
                
                await Task.Delay(500);
                SetProductionMode(true);
                Gvar.Logger.Log(LogLevel.Info, "系统就绪，进入生产模式");
            });
        }

        /// <summary>
        /// 模拟生产过程（用于测试）
        /// </summary>
        public void SimulateProduction()
        {
            Task.Run(async () =>
            {
                Random rand = new Random();
                string batchNo = $"BATCH{DateTime.Now:yyyyMMdd}001";
                
                for (int i = 0; i < 5; i++)
                {
                    await Task.Delay(2000);
                    
                    string ringInfo = $"Ring-{DateTime.Now:HHmmss}-{i + 1:D3}";
                    double testTime = 150 + rand.NextDouble() * 100;
                    string result = rand.Next(0, 10) > 1 ? "OK" : "NG";
                    
                    AddProductionData(ringInfo, testTime, result, batchNo);
                    
                    // 偶尔产生报警
                    if (rand.Next(0, 10) > 7)
                    {
                        string[] alarms = { "视觉检测异常", "气压不足", "定位偏移" };
                        string[] codes = { "ALARM_VISION", "ALARM_PRESSURE", "ALARM_POSITION" }; // ← 修复
                        int idx = rand.Next(alarms.Length);
                        AddAlarm(alarms[idx], "警告", codes[idx]);
                    }
                }
                
                Gvar.Logger.Log(LogLevel.Info, "生产批次完成");
            });
        }

        /// <summary>
        /// 窗体加载时的额外初始化
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // 可选：启动模拟日志（用于测试）
            // SimulateSystemLogs();
        }

        /// <summary>
        /// 窗体关闭时取消订阅
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // 取消订阅Logger事件
                Gvar.Logger.OnLogMessage -= Logger_OnLogMessage;
                
                // 断开MQTT连接
                Gvar.Mqtt.DisconnectAsync().Wait();
                Gvar.Logger.Log(LogLevel.Info, "MQTT已断开");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "关闭MQTT连接失败");
            }
            
            base.OnFormClosing(e);
        }
    }
}
