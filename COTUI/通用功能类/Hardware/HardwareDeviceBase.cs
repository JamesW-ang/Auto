using System;
using System.Threading;
using System.Threading.Tasks;

namespace COTUI.通用功能类.Hardware
{
    /// <summary>
    /// 硬件设备基类 - 封装通用连接管理逻辑
    /// </summary>
    public abstract class HardwareDeviceBase : IHardwareDevice
    {
        #region 字段

        protected bool isConnected = false;
        protected readonly object lockObject = new object();
        protected Logger logger = Logger.GetInstance();

        // 自动重连
        protected System.Threading.Timer reconnectTimer;
        protected int reconnectAttempts = 0;
        protected const int MaxReconnectAttempts = 5;
        protected const int ReconnectInterval = 3000; // 3秒
        protected bool enableAutoReconnect = true;

        #endregion

        #region 属性

        public abstract string DeviceName { get; }
        
        public bool IsConnected => isConnected;

        public bool EnableAutoReconnect
        {
            get => enableAutoReconnect;
            set => enableAutoReconnect = value;
        }

        #endregion

        #region 事件

        public event EventHandler<bool> ConnectionStatusChanged;
        public event EventHandler<string> ErrorOccurred;

        #endregion

        #region 公共方法

        public async Task<bool> ConnectAsync()
        {
            try
            {
                Gvar.Logger.Log($"[{DeviceName}] 正在连接...");
                
                bool success = await ConnectDeviceAsync();
                
                if (success)
                {
                    isConnected = true;
                    reconnectAttempts = 0;
                    StopReconnectTimer();
                    
                    Gvar.Logger.Log($"[{DeviceName}] 连接成功");
                    ConnectionStatusChanged?.Invoke(this, true);
                }
                else
                {
                    Gvar.Logger.Log($"[{DeviceName}] 连接失败");
                }
                
                return success;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"[{DeviceName}] 连接异常");
                OnError($"连接异常: {ex.Message}");
                return false;
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                StopReconnectTimer();
                
                if (isConnected)
                {
                    Gvar.Logger.Log($"[{DeviceName}] 正在断开连接...");
                    await DisconnectDeviceAsync();
                    
                    isConnected = false;
                    ConnectionStatusChanged?.Invoke(this, false);
                    Gvar.Logger.Log($"[{DeviceName}] 已断开连接");
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"[{DeviceName}] 断开连接异常");
                OnError($"断开连接异常: {ex.Message}");
            }
        }

        public virtual async Task<bool> InitializeAsync()
        {
            Gvar.Logger.Log($"[{DeviceName}] 初始化设备");
            return await Task.FromResult(true);
        }

        public virtual async Task ResetAsync()
        {
            Gvar.Logger.Log($"[{DeviceName}] 重置设备");
            await DisconnectAsync();
            await Task.Delay(1000);
            await ConnectAsync();
        }

        #endregion

        #region 抽象方法（子类实现）

        /// <summary>
        /// 实际的设备连接逻辑
        /// </summary>
        protected abstract Task<bool> ConnectDeviceAsync();

        /// <summary>
        /// 实际的设备断开逻辑
        /// </summary>
        protected abstract Task DisconnectDeviceAsync();

        #endregion

        #region 自动重连

        protected void StartReconnectTimer()
        {
            if (!enableAutoReconnect)
                return;

            StopReconnectTimer();
            reconnectTimer = new System.Threading.Timer(
                async (state) => await AttemptReconnect(),
                null,
                ReconnectInterval,
                Timeout.Infinite
            );
            Gvar.Logger.Log($"[{DeviceName}] 将在 {ReconnectInterval / 1000} 秒后尝试重连");
        }

        protected void StopReconnectTimer()
        {
            reconnectTimer?.Change(Timeout.Infinite, Timeout.Infinite);
            reconnectTimer?.Dispose();
            reconnectTimer = null;
        }

        protected async Task AttemptReconnect()
        {
            if (isConnected)
                return;

            if (reconnectAttempts >= MaxReconnectAttempts)
            {
                Gvar.Logger.Log($"[{DeviceName}] 已达到最大重连次数 ({MaxReconnectAttempts})");
                OnError($"重连失败，已尝试 {MaxReconnectAttempts} 次");
                return;
            }

            reconnectAttempts++;
            Gvar.Logger.Log($"[{DeviceName}] 尝试重连 (第 {reconnectAttempts}/{MaxReconnectAttempts} 次)");

            bool success = await ConnectAsync();
            
            if (!success && enableAutoReconnect)
            {
                StartReconnectTimer();
            }
        }

        protected void OnConnectionLost()
        {
            if (isConnected)
            {
                isConnected = false;
                Gvar.Logger.Log($"[{DeviceName}] 连接丢失");
                ConnectionStatusChanged?.Invoke(this, false);
                
                if (enableAutoReconnect)
                {
                    StartReconnectTimer();
                }
            }
        }

        #endregion

        #region 辅助方法

        protected void OnError(string message)
        {
            Gvar.Logger.Log($"[{DeviceName}] {message}");
            ErrorOccurred?.Invoke(this, message);
        }

        public void ResetReconnectAttempts()
        {
            reconnectAttempts = 0;
        }

        #endregion

        #region 资源释放

        public virtual void Dispose()
        {
            StopReconnectTimer();
            DisconnectAsync().Wait();
        }

        #endregion
    }
}
