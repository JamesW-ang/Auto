using System;
using System.Threading.Tasks;

namespace COTUI.通用功能类.Hardware
{
    /// <summary>
    /// 硬件设备通用接口
    /// </summary>
    public interface IHardwareDevice
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// 设备是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接状态变化事件
        /// </summary>
        event EventHandler<bool> ConnectionStatusChanged;

        /// <summary>
        /// 错误事件
        /// </summary>
        event EventHandler<string> ErrorOccurred;

        /// <summary>
        /// 连接设备
        /// </summary>
        Task<bool> ConnectAsync();

        /// <summary>
        /// 断开设备
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// 初始化设备
        /// </summary>
        Task<bool> InitializeAsync();

        /// <summary>
        /// 重置设备
        /// </summary>
        Task ResetAsync();
    }
}
