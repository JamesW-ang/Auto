using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using COTUI.通用功能类;
using COTUI.数据库;

namespace COTUI
{
    /// <summary>
    /// 全局变量管理类 - 优化版
    /// 提供系统级快捷访问、生产数据、设备状态等功能
    /// </summary>
    public static class Gvar
    {
        #region 核心层：基础变量（零开销快捷访问）

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static string User { get; set; } = "";

        /// <summary>
        /// 当前选择的工站
        /// </summary>
        public static string CurrentStation { get; set; } = "全部工站";

        /// <summary>
        /// 应用程序启动时间
        /// </summary>
        public static DateTime StartupTime { get; } = DateTime.Now;

        /// <summary>
        /// 日志服务（单例快捷访问）
        /// </summary>
        public static Logger Logger => 通用功能类.Logger.GetInstance();

        /// <summary>
        /// 数据库服务（单例快捷访问）
        /// </summary>
        public static DatabaseHelper DB => DatabaseHelper.Instance;

        /// <summary>
        /// 配置管理器（单例快捷访问）
        /// </summary>
        public static ConfigManager Config => ConfigManager.Instance;

        /// <summary>
        /// MQTT服务（单例快捷访问）
        /// </summary>
        public static MqttService Mqtt => MqttService.Instance;

        #endregion

        #region 业务层：生产统计（缓存优化）

        /// <summary>
        /// 生产统计相关数据
        /// </summary>
        public static class Production
        {
            private static int? _todayTotalCount;
            private static DateTime _todayTotalCountCacheTime;

            /// <summary>
            /// 今日总产量（缓存5秒）
            /// </summary>
            public static int TodayTotalCount
            {
                get
                {
                    if (_todayTotalCount == null || (DateTime.Now - _todayTotalCountCacheTime).TotalSeconds > 5)
                    {
                        _todayTotalCount = DB.GetTodayTotalCount();
                        _todayTotalCountCacheTime = DateTime.Now;
                    }
                    return _todayTotalCount.Value;
                }
            }

            private static int? _todayOkCount;
            private static DateTime _todayOkCountCacheTime;

            /// <summary>
            /// 今日合格数（缓存5秒）
            /// </summary>
            public static int TodayOkCount
            {
                get
                {
                    if (_todayOkCount == null || (DateTime.Now - _todayOkCountCacheTime).TotalSeconds > 5)
                    {
                        _todayOkCount = DB.GetTodayOkCount();
                        _todayOkCountCacheTime = DateTime.Now;
                    }
                    return _todayOkCount.Value;
                }
            }

            private static int? _todayNgCount;
            private static DateTime _todayNgCountCacheTime;

            /// <summary>
            /// 今日不良数（缓存5秒）
            /// </summary>
            public static int TodayNgCount
            {
                get
                {
                    if (_todayNgCount == null || (DateTime.Now - _todayNgCountCacheTime).TotalSeconds > 5)
                    {
                        _todayNgCount = DB.GetTodayNgCount();
                        _todayNgCountCacheTime = DateTime.Now;
                    }
                    return _todayNgCount.Value;
                }
            }

            /// <summary>
            /// 今日良率（计算属性，基于缓存数据）
            /// </summary>
            public static double TodayYieldRate
            {
                get
                {
                    int total = TodayTotalCount;
                    return total > 0 ? (double)TodayOkCount / total * 100 : 0;
                }
            }

            /// <summary>
            /// 刷新所有缓存（调用此方法强制重新查询数据库）
            /// </summary>
            public static void RefreshCache()
            {
                _todayTotalCount = null;
                _todayOkCount = null;
                _todayNgCount = null;
            }
        }

        #endregion

        #region 业务层：通信状态（设备管理优化）

        /// <summary>
        /// 通信与设备状态
        /// </summary>
        public static class Communication
        {
            /// <summary>
            /// MQTT连接状态
            /// </summary>
            public static bool IsMqttConnected => Mqtt?.IsConnected ?? false;

            /// <summary>
            /// PLC连接状态
            /// </summary>
            public static bool IsPlcConnected { get; set; }

            /// <summary>
            /// 视觉系统连接状态
            /// </summary>
            public static bool IsVisionConnected { get; set; }

            /// <summary>
            /// 运动控制卡连接状态
            /// </summary>
            public static bool IsMotionControlConnected { get; set; }

            /// <summary>
            /// 扫码枪连接状态
            /// </summary>
            public static bool IsScannerConnected { get; set; }

            /// <summary>
            /// 获取所有设备连接状态摘要
            /// </summary>
            public static string GetConnectionSummary()
            {
                var statuses = new[]
                {
                    $"MQTT: {(IsMqttConnected ? "已连接" : "未连接")}",
                    $"PLC: {(IsPlcConnected ? "已连接" : "未连接")}",
                    $"视觉: {(IsVisionConnected ? "已连接" : "未连接")}",
                    $"运动控制: {(IsMotionControlConnected ? "已连接" : "未连接")}",
                    $"扫码枪: {(IsScannerConnected ? "已连接" : "未连接")}"
                };
                return string.Join(" | ", statuses);
            }

            /// <summary>
            /// 检查是否所有关键设备已连接
            /// </summary>
            public static bool IsAllCriticalDevicesConnected =>
                IsPlcConnected && IsVisionConnected && IsMotionControlConnected;
        }

        #endregion

        #region 业务层：权限管理

        /// <summary>
        /// 权限管理相关
        /// </summary>
        public static class Permission
        {
            private static int? _currentUserLevel;

            /// <summary>
            /// 当前用户权限等级（缓存，登录后设置）
            /// </summary>
            public static int CurrentUserLevel
            {
                get
                {
                    if (_currentUserLevel == null)
                    {
                        _currentUserLevel = DB.GetUserPermissionLevel(User);
                    }
                    return _currentUserLevel.Value;
                }
                set => _currentUserLevel = value;
            }

            /// <summary>
            /// 是否为管理员（权限等级 >= 5）
            /// </summary>
            public static bool IsAdmin => CurrentUserLevel >= 5;

            /// <summary>
            /// 是否为操作员（权限等级 >= 3）
            /// </summary>
            public static bool IsOperator => CurrentUserLevel >= 3;

            /// <summary>
            /// 是否仅查看权限（权限等级 < 3）
            /// </summary>
            public static bool IsViewOnly => CurrentUserLevel < 3;

            /// <summary>
            /// 清除权限缓存（用户注销时调用）
            /// </summary>
            public static void ClearCache()
            {
                _currentUserLevel = null;
            }
        }

        #endregion

        #region 扩展层：系统配置

        /// <summary>
        /// 系统配置相关
        /// </summary>
        public static class System
        {
            /// <summary>
            /// 是否启用调试模式
            /// </summary>
            public static bool IsDebugMode { get; set; } = false;

            /// <summary>
            /// 是否启用自动保存
            /// </summary>
            public static bool IsAutoSaveEnabled { get; set; } = true;

            /// <summary>
            /// 自动保存间隔（秒）
            /// </summary>
            public static int AutoSaveInterval { get; set; } = 300;

            /// <summary>
            /// 数据刷新间隔（毫秒）
            /// </summary>
            public static int DataRefreshInterval { get; set; } = 1000;

            /// <summary>
            /// 应用程序版本
            /// </summary>
            public static string Version { get; set; } = "1.0.0";

            /// <summary>
            /// 应用程序运行时长
            /// </summary>
            public static TimeSpan Uptime => DateTime.Now - StartupTime;
        }

        #endregion

        #region 扩展层：UI状态

        /// <summary>
        /// UI状态管理
        /// </summary>
        public static class UI
        {
            /// <summary>
            /// 主窗体引用（按需设置）
            /// </summary>
            public static Form1 MainForm { get; set; }

            /// <summary>
            /// 当前激活的页面名称
            /// </summary>
            public static string CurrentPageName { get; set; } = "";

            /// <summary>
            /// 是否显示状态栏
            /// </summary>
            public static bool ShowStatusBar { get; set; } = true;

            /// <summary>
            /// 是否显示工具栏
            /// </summary>
            public static bool ShowToolBar { get; set; } = true;
        }

        #endregion

        #region 扩展层：临时数据缓存

        /// <summary>
        /// 临时数据缓存（用于跨窗体传递数据）
        /// </summary>
        public static class TempData
        {
            private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

            /// <summary>
            /// 设置临时数据
            /// </summary>
            public static void Set(string key, object value)
            {
                _cache[key] = value;
            }

            /// <summary>
            /// 获取临时数据
            /// </summary>
            public static T Get<T>(string key, T defaultValue = default(T))
            {
                if (_cache.TryGetValue(key, out object value) && value is T result)
                {
                    return result;
                }
                return defaultValue;
            }

            /// <summary>
            /// 清除临时数据
            /// </summary>
            public static void Clear(string key = null)
            {
                if (key == null)
                    _cache.Clear();
                else
                    _cache.Remove(key);
            }

            /// <summary>
            /// 检查是否存在指定键
            /// </summary>
            public static bool Contains(string key)
            {
                return _cache.ContainsKey(key);
            }
        }

        #endregion

        #region 兼容性：保留旧变量（向后兼容）

        /// <summary>
        /// [已废弃] 使用 Gvar.User 代替
        /// </summary>
        [Obsolete("请使用 Gvar.User 代替")]
        public static string _User
        {
            get => User;
            set => User = value;
        }

        /// <summary>
        /// [已废弃] 使用 Gvar.CurrentStation 代替
        /// </summary>
        [Obsolete("请使用 Gvar.CurrentStation 代替")]
        public static string _CurrentStation
        {
            get => CurrentStation;
            set => CurrentStation = value;
        }

        #endregion
    }
}
