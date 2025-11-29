using System;
using System.IO; // ← 添加IO命名空间
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
using COTUI.通用功能类;
using System.Drawing;
using Newtonsoft.Json; // ← 添加Newtonsoft.Json命名空间

namespace COTUI
{
    public partial class MQTTForm : Form
    {
        #region 字段

        // 使用全局变量 Gvar.Logger 访问日志服务
        // 使用全局变量 Gvar.Mqtt 访问 MQTT 服务
        private ConfigManager config; // ← 延迟初始化
        
        // 通讯服务
        private TcpSocketService tcpSocketService;
        
        // MQTT配置（从配置文件读取）
        private string mqttServer;
        private int mqttPort;
        private string mqttClientId;
        private string mqttUsername;
        private string mqttPassword;
        private string mqttTopicPrefix;
        
        // TCP Socket配置（从配置文件读取）
        private string tcpServerHost;
        private int tcpServerPort;
        private bool tcpEnableHeartbeat;
        private int tcpHeartbeatInterval;
        
        // ✅ 常量定义（只保留真正的常量）
        private const int DEFAULT_MQTT_PORT = 1883;
        private const int DEFAULT_TCP_PORT = 8080;
        private const int MIN_PORT = 1;
        private const int MAX_PORT = 65535;

        #endregion

        #region 构造函数

        public MQTTForm()
        {
            InitializeComponent();
            
            // 初始化通讯服务
            InitializeCommunicationServices();
            
            Gvar.Logger.Info("MQTT管理页面创建");
        }
        
        /// <summary>
        /// 初始化通讯服务
        /// </summary>
        private void InitializeCommunicationServices()
        {
            try
            {
                // 初始化TCP Socket服务
                tcpSocketService = new TcpSocketService();
                tcpSocketService.ConnectionStatusChanged += TcpSocket_ConnectionStatusChanged;
                tcpSocketService.TextReceived += TcpSocket_TextReceived;
                tcpSocketService.ErrorOccurred += TcpSocket_ErrorOccurred;
                
                Gvar.Logger.Info("通讯服务初始化完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "初始化通讯服务失败");
            }
        }

        #endregion

        #region 页面加载

        private void MQTTForm_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ 初始化ConfigManager（捕获可能的异常）
                try
                {
                    config = Gvar.Config;
                    
                    // 显示配置文件信息（调试用）
                    Gvar.Logger.Info("=== 配置文件信息 ===");
                    Gvar.Logger.Log(Gvar.Config.GetConfigInfo());
                    Gvar.Logger.Info("====================");
                }
                catch (FileNotFoundException ex)
                {
                    Gvar.Logger.ErrorException(ex, "配置文件不存在");
                    
                    DialogResult result = MessageBox.Show(
                        $"配置文件不存在！\n\n" +
                        $"{ex.Message}\n\n" +
                        $"是否使用默认配置继续？\n\n" +
                        $"点击\"是\"：使用默认配置\n" +
                        $"点击\"否\"：退出程序",
                        "配置文件缺失",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    
                    if (result == DialogResult.No)
                    {
                        this.Close();
                        return;
                    }
                    
                    // 使用默认配置
                    config = null;
                }
                
                // 加载配置
                LoadConfiguration();
                
                // ✅ 验证配置是否加载成功
                if (!ValidateConfiguration())
                {
                    MessageBox.Show(
                        "配置加载失败或配置不完整！\n\n" +
                        "请检查Config.ini文件：\n" +
                        "1. 文件是否存在于程序根目录\n" +
                        "2. [MQTT]分组是否存在\n" +
                        "3. Server、Port、ClientID等配置是否填写\n\n" +
                        "当前配置状态:\n" +
                        $"• 服务器: {(string.IsNullOrEmpty(mqttServer) ? "未配置" : mqttServer)}\n" +
                        $"• 端口: {mqttPort}\n" +
                        $"• 客户端ID: {(string.IsNullOrEmpty(mqttClientId) ? "未配置" : mqttClientId)}\n\n" +
                        "请修改Config.ini后重新启动程序。",
                        "配置警告",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                
                // 显示当前配置
                DisplayConfiguration();
                
                // 订阅MQTT状态变化
                Gvar.Mqtt.ConnectionStatusChanged += MqttService_ConnectionStatusChanged;
                
                // 更新初始状态
                UpdateConnectionStatus(Gvar.Mqtt.IsConnected);
                
                Gvar.Logger.Info("MQTT管理页面加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT管理页面加载失败");
                MessageBox.Show($"页面加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 验证配置是否有效
        /// </summary>
        private bool ValidateConfiguration()
        {
            bool isValid = true;
            
            if (string.IsNullOrWhiteSpace(mqttServer))
            {
                Gvar.Logger.Info("❌ MQTT服务器地址为空");
                isValid = false;
            }
            
            if (mqttPort < MIN_PORT || mqttPort > MAX_PORT)
            {
                Gvar.Logger.Info($"❌ MQTT端口号无效: {mqttPort}（有效范围：{MIN_PORT}-{MAX_PORT}）");
                isValid = false;
            }
            
            if (string.IsNullOrWhiteSpace(mqttClientId))
            {
                Gvar.Logger.Info("❌ MQTT客户端ID为空");
                isValid = false;
            }
            
            if (isValid)
            {
                Gvar.Logger.Info($"✅ 配置验证通过: {mqttServer}:{mqttPort}");
            }
            else
            {
                Gvar.Logger.Info("⚠️ 配置验证失败，请检查Config.ini文件");
            }
            
            return isValid;
        }

        #endregion

        #region 配置管理

        /// <summary>
        /// 加载配置（从配置文件）
        /// </summary>
        private void LoadConfiguration()
        {
            try
            {
                if (config != null)
                {
                    // ✅ 从配置文件读取MQTT配置
                    mqttServer = Gvar.Config.GetValue("MQTT", "Server");
                    mqttPort = Gvar.Config.GetIntValue("MQTT", "Port", DEFAULT_MQTT_PORT);
                    mqttClientId = Gvar.Config.GetValue("MQTT", "ClientID");
                    mqttUsername = Gvar.Config.GetValue("MQTT", "Username");
                    mqttPassword = Gvar.Config.GetValue("MQTT", "Password");
                    mqttTopicPrefix = Gvar.Config.GetValue("MQTT", "TopicPrefix");
                    
                    // ✅ 从配置文件读取TCP Socket配置
                    tcpServerHost = Gvar.Config.GetValue("TcpSocket", "ServerHost", "192.168.1.100");
                    tcpServerPort = Gvar.Config.GetIntValue("TcpSocket", "ServerPort", DEFAULT_TCP_PORT);
                    tcpEnableHeartbeat = Gvar.Config.GetBoolValue("TcpSocket", "EnableHeartbeat", true);
                    tcpHeartbeatInterval = Gvar.Config.GetIntValue("TcpSocket", "HeartbeatInterval", 30000);
                    
                    Gvar.Logger.Info($"✅ 配置已从文件加载");
                    Gvar.Logger.Info($"   MQTT: {mqttServer}:{mqttPort}");
                    Gvar.Logger.Info($"   TCP: {tcpServerHost}:{tcpServerPort}");
                }
                else
                {
                    // ⚠️ 配置文件不可用时，所有值为空/默认值
                    mqttServer = string.Empty;
                    mqttPort = DEFAULT_MQTT_PORT;
                    mqttClientId = string.Empty;
                    mqttUsername = string.Empty;
                    mqttPassword = string.Empty;
                    mqttTopicPrefix = string.Empty;
                    
                    tcpServerHost = "192.168.1.100";
                    tcpServerPort = DEFAULT_TCP_PORT;
                    tcpEnableHeartbeat = true;
                    tcpHeartbeatInterval = 30000;
                    
                    Gvar.Logger.Info("⚠️ 配置文件不可用，使用默认配置");
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载配置失败");
                
                // 发生异常时，设置为空/默认值
                mqttServer = string.Empty;
                mqttPort = DEFAULT_MQTT_PORT;
                mqttClientId = string.Empty;
                mqttUsername = string.Empty;
                mqttPassword = string.Empty;
                mqttTopicPrefix = string.Empty;
                
                tcpServerHost = "192.168.1.100";
                tcpServerPort = DEFAULT_TCP_PORT;
                tcpEnableHeartbeat = true;
                tcpHeartbeatInterval = 30000;
            }
        }

        /// <summary>
        /// 显示当前配置
        /// </summary>
        private void DisplayConfiguration()
        {
            try
            {
                // 显示MQTT配置到UI控件
                txtServer.Text = mqttServer;          // IP
                txtPort.Text = mqttPort.ToString(); // Port
                txtUsername.Text = mqttUsername;        // User
                txtPassword.Text = mqttPassword;        // Password
                
                Gvar.Logger.Info("配置信息已显示到界面");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "显示配置信息失败");
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfiguration()
        {
            try
            {
                // ✅ 验证输入
                if (string.IsNullOrWhiteSpace(txtServer.Text))
                {
                    MessageBox.Show("MQTT服务器地址不能为空！", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtServer.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPort.Text))
                {
                    MessageBox.Show("MQTT端口不能为空！", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPort.Focus();
                    return;
                }

                // ✅ 安全解析端口号
                int port;
                if (!int.TryParse(txtPort.Text.Trim(), out port))
                {
                    MessageBox.Show(
                        $"端口号格式不正确：{txtPort.Text}\n\n请输入有效的数字（如：{DEFAULT_MQTT_PORT}）", 
                        "格式错误", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                    txtPort.Focus();
                    txtPort.SelectAll();
                    return;
                }

                // ✅ 验证端口范围（使用常量）
                if (port < MIN_PORT || port > MAX_PORT)
                {
                    MessageBox.Show(
                        $"端口号超出范围：{port}\n\n有效范围：{MIN_PORT}-{MAX_PORT}\n常用端口：{DEFAULT_MQTT_PORT}", 
                        "范围错误", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                    txtPort.Focus();
                    txtPort.SelectAll();
                    return;
                }
                
                // 从UI控件读取最新配置
                mqttServer = txtServer.Text.Trim();
                mqttPort = port;
                mqttUsername = txtUsername.Text.Trim();
                mqttPassword = txtPassword.Text.Trim();
                
                // 保存到配置文件（如果config可用）
                if (config != null)
                {
                    Gvar.Config.SetValue("MQTT", "Server", mqttServer);
                    Gvar.Config.SetValue("MQTT", "Port", mqttPort);
                    Gvar.Config.SetValue("MQTT", "Username", mqttUsername);
                    Gvar.Config.SetValue("MQTT", "Password", mqttPassword);
                    
                    // 写入文件
                    Gvar.Config.SaveConfig();
                    
                    Gvar.Logger.Info($"✅ MQTT配置已保存: {mqttServer}:{mqttPort}");
                }
                else
                {
                    Gvar.Logger.Info($"⚠️ 配置已更新（仅内存，未保存到文件）");
                }
            }
            catch (FormatException ex)
            {
                Gvar.Logger.ErrorException(ex, "保存MQTT配置失败：格式错误");
                MessageBox.Show(
                    $"配置格式错误！\n\n" +
                    $"端口输入: {txtPort.Text}\n" +
                    $"错误详情: {ex.Message}\n\n" +
                    $"请输入有效的数字端口（如：{DEFAULT_MQTT_PORT}）", 
                    "格式错误", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                txtPort.Focus();
                txtPort.SelectAll();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "保存MQTT配置失败");
                MessageBox.Show($"保存配置失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region MQTT连接控制

        /// <summary>
        /// 连接按钮点击事件
        /// </summary>
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Gvar.Mqtt.IsConnected)
                {
                    MessageBox.Show("MQTT已连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ✅ 先保存配置（会进行验证）
                SaveConfiguration();

                // ✅ 再次验证配置是否有效
                if (string.IsNullOrWhiteSpace(mqttServer))
                {
                    MessageBox.Show(
                        "MQTT服务器地址为空！\n\n" +
                        "请在上方输入框中填写MQTT服务器IP地址。\n\n" +
                        "如果您是首次使用，请检查Config.ini文件中的[MQTT]配置。",
                        "配置错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtServer.Focus();
                    return;
                }

                if (mqttPort < MIN_PORT || mqttPort > MAX_PORT)
                {
                    MessageBox.Show(
                        $"MQTT端口号无效：{mqttPort}\n\n" +
                        $"有效范围：{MIN_PORT}-{MAX_PORT}\n" +
                        $"常用端口：{DEFAULT_MQTT_PORT}\n\n" +
                        "请检查Config.ini文件中的Port配置。",
                        "配置错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtPort.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(mqttClientId))
                {
                    MessageBox.Show(
                        "MQTT客户端ID为空！\n\n" +
                        "请检查Config.ini文件中的[MQTT]分组下的ClientID配置。",
                        "配置错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // 禁用按钮
                btnConnect.Enabled = false;
                
                Cursor = Cursors.WaitCursor;

                Gvar.Logger.Info($"开始连接MQTT: {mqttServer}:{mqttPort} (ClientID: {mqttClientId})");

                // 连接MQTT
                await Gvar.Mqtt.ConnectAsync(
                    mqttServer, 
                    mqttPort, 
                    mqttClientId, 
                    mqttUsername, 
                    mqttPassword
                );

                Gvar.Logger.Info("✅ MQTT连接成功");
                MessageBox.Show(
                    $"MQTT连接成功！\n\n" +
                    $"服务器: {mqttServer}:{mqttPort}\n" +
                    $"客户端ID: {mqttClientId}",
                    "成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (ArgumentNullException ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT连接失败：参数为空");
                MessageBox.Show(
                    $"连接失败：参数为空\n\n" +
                    $"错误详情: {ex.Message}\n\n" +
                    $"当前配置:\n" +
                    $"服务器: {mqttServer ?? "(空)"}\n" +
                    $"端口: {mqttPort}\n" +
                    $"客户端ID: {mqttClientId ?? "(空)"}\n\n" +
                    $"请检查配置文件是否正确",
                    "参数错误", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT连接失败");
                MessageBox.Show(
                    $"连接失败：{ex.Message}\n\n" +
                    $"服务器: {mqttServer ?? "(空)"}:{mqttPort}\n" +
                    $"客户端ID: {mqttClientId ?? "(空)"}\n\n" +
                    $"请检查:\n" +
                    $"1. 服务器地址是否正确\n" +
                    $"2. 网络是否连通\n" +
                    $"3. MQTT服务是否启动",
                    "连接错误", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnConnect.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 断开按钮点击事件
        /// </summary>
        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gvar.Mqtt.IsConnected)
                {
                    MessageBox.Show("MQTT未连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (this.Controls.Find("btnDisconnect", true).Length > 0)
                {
                    ((Button)this.Controls.Find("btnDisconnect", true)[0]).Enabled = false;
                }
                
                Cursor = Cursors.WaitCursor;

                await Gvar.Mqtt.DisconnectAsync();

                Gvar.Logger.Info("MQTT已断开");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT断开失败");
                MessageBox.Show($"断开失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (this.Controls.Find("btnDisconnect", true).Length > 0)
                {
                    ((Button)this.Controls.Find("btnDisconnect", true)[0]).Enabled = true;
                }
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 重连按钮点击事件
        /// </summary>
        private async void btnReconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Controls.Find("btnReconnect", true).Length > 0)
                {
                    ((Button)this.Controls.Find("btnReconnect", true)[0]).Enabled = false;
                }
                
                Cursor = Cursors.WaitCursor;

                // 先断开
                if (Gvar.Mqtt.IsConnected)
                {
                    await Gvar.Mqtt.DisconnectAsync();
                    await Task.Delay(500);
                }

                // 重置重连计数
                Gvar.Mqtt.ResetReconnectAttempts();

                // 重新连接
                await Gvar.Mqtt.ConnectAsync(
                    mqttServer, 
                    mqttPort, 
                    mqttClientId, 
                    mqttUsername, 
                    mqttPassword
                );

                Gvar.Logger.Info("MQTT重连成功");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT重连失败");
                MessageBox.Show($"重连失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (this.Controls.Find("btnReconnect", true).Length > 0)
                {
                    ((Button)this.Controls.Find("btnReconnect", true)[0]).Enabled = true;
                }
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// MQTT连接状态变化事件
        /// </summary>
        private void MqttService_ConnectionStatusChanged(object sender, bool isConnected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateConnectionStatus(isConnected)));
                return;
            }

            UpdateConnectionStatus(isConnected);
        }

        /// <summary>
        /// 更新连接状态显示
        /// </summary>
        private void UpdateConnectionStatus(bool isConnected)
        {
            // 查找状态标签
            var lblStatus = this.Controls.Find("lblStatus", true);
            if (lblStatus.Length > 0 && lblStatus[0] is Label label)
            {
                if (isConnected)
                {
                    label.Text = "✅ 已连接";
                    label.ForeColor = Color.Green;
                }
                else
                {
                    label.Text = "❌ 未连接";
                    label.ForeColor = Color.Red;
                }
            }

            // 更新按钮状态
            var btnConnect = this.Controls.Find("btnConnect", true);
            if (btnConnect.Length > 0 && btnConnect[0] is Button connectBtn)
            {
                connectBtn.Enabled = !isConnected;
            }

            var btnDisconnect = this.Controls.Find("btnDisconnect", true);
            if (btnDisconnect.Length > 0 && btnDisconnect[0] is Button disconnectBtn)
            {
                disconnectBtn.Enabled = isConnected;
            }
        }

        #endregion

        #region 测试发送

        /// <summary>
        /// 测试发送按钮点击事件
        /// </summary>
        private async void btnTestSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Gvar.Mqtt.IsConnected)
                {
                    MessageBox.Show("MQTT未连接，无法发送", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 构建测试数据
                var testData = new
                {
                    msg_id = $"test-{DateTime.Now:yyyyMMddHHmmss}",
                    deviceno = mqttClientId.Contains("-") ? 
                        mqttClientId.Substring(mqttClientId.LastIndexOf('-') + 1) : mqttClientId,
                    dataType = "TestData",
                    deviceType = "",
                    data = new[]
                    {
                        new
                        {
                            testTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            message = "这是一条测试消息",
                            value = 123.45
                        }
                    }
                };
                
                // 序列化为JSON（使用Newtonsoft.Json）
                string json = JsonConvert.SerializeObject(testData, Formatting.Indented);

                // 发送
                string topic = $"{mqttTopicPrefix}/test";
                await Gvar.Mqtt.PublishAsync(topic, json);

                Gvar.Logger.Info($"✅ 测试消息已发送到主题: {topic}");
                MessageBox.Show($"测试消息发送成功\n\n主题: {topic}\n\n内容:\n{json}", "成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "发送测试消息失败");
                MessageBox.Show($"发送失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 窗体关闭

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // 取消订阅事件
                Gvar.Mqtt.ConnectionStatusChanged -= MqttService_ConnectionStatusChanged;
                
                // 释放TCP Socket服务
                if (tcpSocketService != null)
                {
                    tcpSocketService.ConnectionStatusChanged -= TcpSocket_ConnectionStatusChanged;
                    tcpSocketService.TextReceived -= TcpSocket_TextReceived;
                    tcpSocketService.ErrorOccurred -= TcpSocket_ErrorOccurred;
                    tcpSocketService.Dispose();
                }
                
                Gvar.Logger.Info("MQTT管理页面关闭");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "关闭MQTT管理页面失败");
            }

            base.OnFormClosing(e);
        }

        #endregion
        
        #region TCP Socket通讯
        
        /// <summary>
        /// 连接TCP服务器（从界面读取配置或使用INI配置）
        /// </summary>
        private async Task ConnectTcpSocket()
        {
            try
            {
                if (tcpSocketService.IsConnected)
                {
                    MessageBox.Show("TCP已连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 从界面读取配置（如果有对应控件）或使用INI配置
                string host = tcpServerHost;
                int port = tcpServerPort;
                
                Gvar.Logger.Info($"正在连接TCP服务器: {host}:{port}");
                
                // 配置心跳
                tcpSocketService.EnableHeartbeat = tcpEnableHeartbeat;
                tcpSocketService.HeartbeatInterval = tcpHeartbeatInterval;
                
                // 连接
                await tcpSocketService.ConnectAsync(host, port);
                
                Gvar.Logger.Info($"✅ TCP连接成功: {host}:{port}");
                MessageBox.Show($"TCP连接成功！\n\n服务器: {host}:{port}\n心跳: {(tcpEnableHeartbeat ? "启用" : "禁用")}", 
                    "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "TCP连接失败");
                MessageBox.Show($"TCP连接失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 断开TCP连接
        /// </summary>
        private void DisconnectTcpSocket()
        {
            try
            {
                if (!tcpSocketService.IsConnected)
                {
                    MessageBox.Show("TCP未连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                tcpSocketService.DisconnectClient();
                Gvar.Logger.Info("TCP已断开");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "断开TCP失败");
                MessageBox.Show($"断开TCP失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// TCP发送数据
        /// </summary>
        private async Task SendTcpData(string data)
        {
            try
            {
                if (!tcpSocketService.IsConnected)
                {
                    MessageBox.Show("TCP未连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                await tcpSocketService.SendTextAsync(data);
                Gvar.Logger.Info($"TCP已发送: {data}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "TCP发送失败");
                MessageBox.Show($"发送失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// TCP连接状态变化事件
        /// </summary>
        private void TcpSocket_ConnectionStatusChanged(object sender, bool isConnected)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TcpSocket_ConnectionStatusChanged(sender, isConnected)));
                return;
            }
            
            Gvar.Logger.Info($"TCP状态变化: {(isConnected ? "已连接" : "已断开")}");
        }
        
        /// <summary>
        /// TCP接收数据事件
        /// </summary>
        private void TcpSocket_TextReceived(object sender, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TcpSocket_TextReceived(sender, text)));
                return;
            }
            
            Gvar.Logger.Info($"TCP接收: {text}");
        }
        
        /// <summary>
        /// TCP错误事件
        /// </summary>
        private void TcpSocket_ErrorOccurred(object sender, string error)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TcpSocket_ErrorOccurred(sender, error)));
                return;
            }
            
            Gvar.Logger.Info($"TCP错误: {error}");
        }
        
        #endregion
    }
}

