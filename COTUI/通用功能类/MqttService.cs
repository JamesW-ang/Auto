using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace COTUI.通用功能类
{
    /// <summary>
    /// MQTT服务（集成通信+业务逻辑）
    /// 
    /// <para><b>功能：</b></para>
    /// - MQTT连接/断开/重连
    /// - 消息发布/订阅
    /// - 自动报工（内置业务逻辑）
    /// - 配置管理
    /// </summary>
    public class MqttService
    {
        #region 单例模式
        
        private static readonly Lazy<MqttService> instance = new Lazy<MqttService>();
        public static MqttService Instance => instance.Value;

        #endregion

        #region 字段

        private IMqttClient mqttClient;
        private MqttClientOptions options;
        private bool isConnected = false;
        private bool connected = false;
        
        private readonly ConcurrentDictionary<string, Action<string>> messageHander = new ConcurrentDictionary<string, Action<string>>();
        
        private System.Threading.Timer reconnectTimer;
        private int reconnectAttempts = 0;
        private const int MaxReconnectAttempts = 5;
        private const int ReconnectInterval = 5000;

        // 配置参数（可从配置文件加载）
        private string mqttServer = "192.168.1.100";
        private int mqttPort = 1883;
        private string mqttClientId = "L1-620-004-10000012345";
        private string mqttUsername = "your_username";
        private string mqttPassword = "your_password";
        private string workReportTopic = "factory/workreport"; // 报工主题

        #endregion

        #region 事件

        public event EventHandler<bool> ConnectionStatusChanged;

        #endregion

        #region 构造函数

        public MqttService()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            SetupEventHandlers();
        }

        #endregion

        #region 事件处理

        private void SetupEventHandlers()
        {
            try
            {
                mqttClient.ConnectedAsync += OnConnectedAsync;
                mqttClient.DisconnectedAsync += OnDisconnectedAsync;
                mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
            }
            catch
            {
                // 忽略
            }
        }

        private async Task OnConnectedAsync(MqttClientConnectedEventArgs e)
        {
            await HandleConnectionEstablished();
        }

        private async Task OnDisconnectedAsync(MqttClientDisconnectedEventArgs e)
        {
            await HandleConnectionLost();
        }

        private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            await HandleMessageReceived(e);
        }

        // 统一的事件处理逻辑
        private async Task HandleConnectionEstablished()
        {
            isConnected = true;
            connected = false;
            reconnectAttempts = 0;
            StopReconnectTimer();

            Gvar.Logger.Info("MQTT已连接");
            ConnectionStatusChanged?.Invoke(this, true);
            
            await ResubscribeAllTopics();
        }

        private async Task HandleConnectionLost()
        {
            isConnected = false;
            connected = false;

            // 使用 GetInstance() 方法
            Gvar.Logger.Warn("MQTT已断开连接");
            ConnectionStatusChanged?.Invoke(this, false);

            if (options != null && reconnectAttempts < MaxReconnectAttempts)
            {
                StartReconnectTimer();
            }

            await Task.CompletedTask;
        }

        private async Task HandleMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            var topic = e.ApplicationMessage.Topic;
            var segment = e.ApplicationMessage.PayloadSegment;
            string payload;

            if (segment.Array != null && segment.Count > 0)
            {
                payload = Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
            }
            else
            {
                payload = "[空消息]";
            }

            // 使用 GetInstance() 方法
            Gvar.Logger.Debug($"收到MQTT消息 - 主题: {topic}, 内容: {payload}");

            if (messageHander.TryGetValue(topic, out var handler))
            {
                try
                {
                    handler(payload);
                }
                catch (Exception ex)
                {
                    Gvar.Logger.Error($"处理MQTT消息失败: {topic} - {ex.Message}");
                }
            }

            await Task.CompletedTask;
        }

        #endregion

        #region 连接管理

        public async Task ConnectAsync(string server, int port, string clientId, string username = null, string password = null, string willTopic = null, string willPayload = null)
        {
            var builder = new MqttClientOptionsBuilder()
                .WithTcpServer(server, port)
                .WithClientId(clientId);

            if (!string.IsNullOrEmpty(username))
            {
                builder = builder.WithCredentials(username, password);
            }

            if (!string.IsNullOrEmpty(willTopic))
            {
                builder = builder.WithWillTopic(willTopic)
                    .WithWillPayload(Encoding.UTF8.GetBytes(willPayload ?? "disconnected"))
                    .WithWillQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .WithWillRetain(false);
            }

            options = builder.Build();
            reconnectAttempts = 0;

            try
            {
                Gvar.Logger.Info($"正在连接到 MQTT 服务器 {server}:{port}...");
                connected = true;
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            }
            catch (Exception ex)
            {
                connected = false;
                Gvar.Logger.ErrorException(ex, $"MQTT连接异常: {ex.Message}");
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            StopReconnectTimer();
            reconnectAttempts = 0;

            if (isConnected)
            {
                Gvar.Logger.Info("正在断开MQTT连接...");
                var disconnectOptions = new MqttClientDisconnectOptionsBuilder()
                    .WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection)
                    .Build();
                await mqttClient.DisconnectAsync(disconnectOptions, CancellationToken.None);
            }
        }

        public bool IsConnected => isConnected;

        #endregion

        #region 自动重连

        private void StartReconnectTimer()
        {
            StopReconnectTimer();
            reconnectTimer = new System.Threading.Timer(async (state) => await Reconnect(), null, ReconnectInterval, System.Threading.Timeout.Infinite);
            Gvar.Logger.Info($"MQTT将在 {ReconnectInterval / 1000} 秒后尝试重连");
        }

        private async Task Reconnect()
        {
            if (isConnected || connected || options == null)
                return;

            if (reconnectAttempts >= MaxReconnectAttempts)
            {
                Gvar.Logger.Error($"已达到最大重连尝试次数 ({MaxReconnectAttempts})，停止自动重连");
                return;
            }

            reconnectAttempts++;
            Gvar.Logger.Info($"尝试重连 (第 {reconnectAttempts} 次)");

            try
            {
                connected = true;
                await mqttClient.ConnectAsync(options, CancellationToken.None);
            }
            catch (Exception ex)
            {
                connected = false;
                Gvar.Logger.ErrorException(ex, $"重连失败: {ex.Message}");

                if (reconnectAttempts < MaxReconnectAttempts)
                {
                    StartReconnectTimer();
                }
            }
        }

        public void ResetReconnectAttempts()
        {
            reconnectAttempts = 0;
        }

        private void StopReconnectTimer()
        {
            reconnectTimer?.Dispose();
            reconnectTimer = null;
        }

        #endregion

        #region 发布/订阅

        public async Task SubscribeAsync(string topic, Action<string> messageHandler)
        {
            if (isConnected)
            {
                Gvar.Logger.Info($"正在订阅主题: {topic}");
                var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build();
                await mqttClient.SubscribeAsync(subscribeOptions, CancellationToken.None);
            }
            messageHander[topic] = messageHandler;
        }

        public async Task PublishAsync(string topic, string payload, bool retain = false)
        {
            if (isConnected)
            {
                Gvar.Logger.Info($"正在发布消息 - 主题: {topic}");
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(Encoding.UTF8.GetBytes(payload))
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .WithRetainFlag(retain)
                    .Build();

                await mqttClient.PublishAsync(message, CancellationToken.None);
            }
            else
            {
                throw new InvalidOperationException("MQTT未连接，无法发布消息");
            }
        }

        public async Task UnsubscribeAsync(string topic)
        {
            if (isConnected)
            {
                Gvar.Logger.Info($"正在取消订阅主题: {topic}");
                var unsubscribeOptions = new MqttClientUnsubscribeOptionsBuilder()
                    .WithTopicFilter(topic)
                    .Build();
                await mqttClient.UnsubscribeAsync(unsubscribeOptions, CancellationToken.None);
            }
            messageHander.TryRemove(topic, out _);
        }

        private async Task ResubscribeAllTopics()
        {
            foreach (var topic in messageHander.Keys)
            {
                try
                {
                    var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                        .WithTopicFilter(topic)
                        .Build();
                    await mqttClient.SubscribeAsync(subscribeOptions, CancellationToken.None);
                    Gvar.Logger.Debug($"重新订阅主题: {topic}");
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, $"重新订阅主题失败: {topic} - {ex.Message}");
                }
            }
        }

        #endregion

        #region 业务功能：自动报工（新增）

        /// <summary>
        /// 设置MQTT配置（从外部调用，如配置文件）
        /// </summary>
        public void SetConfiguration(string server, int port, string clientId, string username, string password, string workTopic)
        {
            mqttServer = server;
            mqttPort = port;
            mqttClientId = clientId;
            mqttUsername = username;
            mqttPassword = password;
            workReportTopic = workTopic;
        }

        /// <summary>
        /// 程序启动时自动连接MQTT
        /// </summary>
        public async Task StartAsync()
        {
            try
            {
                Gvar.Logger.Info("正在启动MQTT自动连接...");
                
                // 使用配置的参数连接
                await ConnectAsync(mqttServer, mqttPort, mqttClientId, mqttUsername, mqttPassword);
                
                Gvar.Logger.Info("✅ MQTT自动连接成功");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "MQTT自动连接失败，稍后将自动重试");
                // 不抛出异常，让程序继续运行
            }
        }

        /// <summary>
        /// 上报生产数据到MES（自动报工）
        /// </summary>
        public async Task PublishWorkReportAsync(COTUI.数据库.Models.ProductionDataModel data)
        {
            try
            {
                if (!isConnected)
                {
                    Gvar.Logger.Warn("MQTT未连接，跳过报工");
                    return;
                }

                // 提取资产编号
                string deviceno = mqttClientId.Contains("-") ? 
                    mqttClientId.Substring(mqttClientId.LastIndexOf('-') + 1) : mqttClientId;

                // 构建报工数据（符合波士顿标准）
                var workReport = new
                {
                    msg_id = $"per-{data.ProductSN}-{DateTime.Now:yyyyMMddHHmmss}",
                    deviceno = deviceno,
                    dataType = "WorkReportData",
                    deviceType = "",
                    data = new[]
                    {
                        new
                        {
                            o_createTime = data.ProductionTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            o_machineNo = mqttClientId,
                            o_workStation = data.Station ?? "主工位",
                            o_productSN = data.ProductSN,
                            o_productModel = data.ProductModel ?? "",
                            o_result = data.OverallResult ?? "OK",
                            o_operator = data.Operator ?? Gvar.User ?? "System",
                            o_testTime = data.TestTime,
                            o_cycleTime = data.CycleTime ?? 0,
                            o_batchNo = data.MaterialBatchNo ?? "",
                            o_dimensionX = data.Dimension_X ?? 0,
                            o_dimensionY = data.Dimension_Y ?? 0,
                            o_dimensionZ = data.Dimension_Z ?? 0,
                            o_angle = data.Angle ?? 0,
                            o_gap = data.Gap ?? 0,
                            o_defectCode = data.DefectCode ?? "",
                            o_defectDescription = data.DefectDescription ?? "",
                            timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        }
                    }
                };

                // 序列化为JSON
                string json = JsonConvert.SerializeObject(workReport);

                // 发布到MQTT
                await PublishAsync(workReportTopic, json);

                Gvar.Logger.Info($"✅ 报工成功: {data.ProductSN}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"报工失败: {data.ProductSN}");
            }
        }

        #endregion
    }
}
