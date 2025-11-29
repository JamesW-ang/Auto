using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COTUI.通用功能类
{
    #region//枚举日志等级
    public enum LogLevel
    {
            //轻微
            Trace  = 0,
            Debug = 1,
            Info  = 2,
            Warn = 3,
            Error = 4,
            //严重 
            Fatal = 5,
    }
    #endregion

    /// <summary>
    /// 日志监听器接口
    /// 
    /// <para><b>用途：</b></para>
    /// 实现此接口可以接收并处理日志消息，支持多种日志输出目标：
    /// - 数据库存储（DatabaseLoggerExtension）
    /// - 网络传输（MES系统上报）
    /// - 实时监控（UI显示）
    /// - 文件归档（特殊格式）
    /// 
    /// <para><b>设计原则：</b></para>
    /// 1. 处理应快速完成（建议 &lt; 10ms）
    /// 2. 耗时操作应使用队列异步处理
    /// 3. 异常不应向外传播（内部捕获并记录）
    /// 4. 支持启用/禁用切换
    /// 
    /// <para><b>实现示例：</b></para>
    /// <code>
    /// public class DatabaseLoggerExtension : ILogListener
    /// {
    ///     public string Name => "DatabaseLogger";
    ///     public bool Enabled { get; set; } = true;
    ///     
    ///     public void OnLog(LogLevel level, string logLine)
    ///     {
    ///         if (!Enabled) return;
    ///         
    ///         try
    ///         {
    ///             // 解析日志并写入数据库
    ///             var logModel = ParseLogLine(logLine);
    ///             _logQueue.TryAdd(logModel);
    ///         }
    ///         catch (Exception ex)
    ///         {
    ///             Console.Error.WriteLine($"数据库日志处理失败: {ex.Message}");
    ///         }
    ///     }
    /// }
    /// </code>
    /// </summary>
    public interface ILogListener
    {
        /// <summary>
        /// 监听器名称
        /// 用于调试、日志输出和管理
        /// 
        /// <para>命名建议：</para>
        /// - DatabaseLogger: 数据库日志
        /// - MESLogger: MES系统日志上报
        /// - UILogger: UI实时显示
        /// - NetworkLogger: 网络日志传输
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 是否启用此监听器
        /// 
        /// <para>用途：</para>
        /// - 运行时动态启用/禁用监听器
        /// - 性能调试时临时关闭某些监听器
        /// - 根据配置条件启用特定监听器
        /// 
        /// <para>注意：</para>
        /// 设置为 false 时，Logger 将跳过此监听器的 OnLog 调用
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 处理日志消息
        /// 
        /// <para><b>参数说明：</b></para>
        /// - level: 日志级别（Trace/Debug/Info/Warn/Error/Fatal）
        /// - logLine: CSV格式的完整日志行，包含：
        ///   * 时间戳（yyyy-MM-dd HH:mm:ss.fff）
        ///   * 日志级别
        ///   * 消息内容（已转义CSV特殊字符）
        ///   * 源文件名
        ///   * 方法名和行号
        /// 
        /// <para><b>格式示例：</b></para>
        /// "2024-11-21 19:00:00.123,Info,测试消息,MainPage.cs,btnTest_Click:45"
        /// 
        /// <para><b>性能要求：</b></para>
        /// - 此方法在日志线程中调用
        /// - 应快速完成处理（建议 &lt; 10ms）
        /// - 耗时操作（如网络请求、数据库写入）应使用队列异步处理
        /// 
        /// <para><b>异常处理：</b></para>
        /// - 必须捕获所有异常
        /// - 不要让异常传播到 Logger
        /// - 可以输出到 Console.Error 或其他独立日志
        /// 
        /// <para><b>线程安全：</b></para>
        /// - 可能被多个日志线程调用（虽然当前只有一个）
        /// - 内部状态访问需要线程同步
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="logLine">CSV格式的日志行</param>
        void OnLog(LogLevel level, string logLine);

        /// <summary>
        /// 初始化监听器（可选）
        /// 
        /// <para>用途：</para>
        /// - 创建队列、线程等资源
        /// - 连接数据库、网络等
        /// - 加载配置
        /// 
        /// <para>调用时机：</para>
        /// 在 Logger.RegisterListener() 之后
        /// 或在监听器自己的 Initialize() 方法中
        /// </summary>
        void Initialize();

        /// <summary>
        /// 关闭监听器（可选）
        /// 
        /// <para>用途：</para>
        /// - 刷新缓冲区
        /// - 释放资源
        /// - 关闭连接
        /// 
        /// <para>调用时机：</para>
        /// - 程序退出时
        /// - Logger.UnregisterListener() 时
        /// - 手动调用 Shutdown()
        /// </summary>
        void Shutdown();
    }

    //单例类，sealed关键字防止继承
    public sealed class Logger
    {
        //单例实现
        private static readonly Lazy<Logger> Instance = new Lazy<Logger>(() => new Logger());
        //公共访问点
        public static Logger GetInstance()
        {
            return Instance.Value;
        }
        //生产者线程，日志消息缓冲区
        private readonly BlockingCollection<(LogLevel, string)> logQuene = new BlockingCollection<(LogLevel, string)>();
        //消费者线程,处理日志消息，避免阻塞主线程
        private readonly Thread logThread;
        
        //消息事件声明（保留向后兼容）
        private event EventHandler<string> onLogMessage = delegate { };
        
        /// <summary>
        /// 日志消息事件（向后兼容）
        /// 
        /// <para><b>⚠️ 推荐使用 ILogListener 接口替代事件</b></para>
        /// 
        /// <para><b>保留原因：</b></para>
        /// - 向后兼容现有代码
        /// - 简单场景快速订阅
        /// - UI实时显示等轻量级用途
        /// 
        /// <para><b>事件 vs 接口对比：</b></para>
        /// - 事件：简单、直接，适合UI显示
        /// - 接口：规范、可管理，适合复杂扩展（数据库、MES等）
        /// 
        /// <para><b>使用示例：</b></para>
        /// <code>
        /// // 事件方式（简单场景）
        /// Logger.GetInstance().OnLogMessage += (sender, logLine) => {
        ///     textBox.AppendText(logLine + Environment.NewLine);
        /// };
        /// 
        /// // 接口方式（推荐，复杂场景）
        /// Logger.GetInstance().RegisterListener(new DatabaseLoggerExtension());
        /// </code>
        /// </summary>
        public event EventHandler<string> OnLogMessage
        {
            add { onLogMessage += value; }
            remove { onLogMessage -= value; }
        }

        // 日志监听器集合
        private readonly List<ILogListener> listeners = new List<ILogListener>();
        private readonly object listenerLock = new object();

        private readonly string logDirectory;
        private string currentLogFile;
        private DateTime currentLogDate;
        //线程同步锁
        private readonly object fileLock = new object();
        private bool isRunning = true;
        private long currentFileSize = 0;
        private const long MaxLogFileSize = 10 * 1024 * 1024; // 10 MB
        private const int MaxArchivedFiles = 20; // 最大归档文件数
        private LogLevel minLogLevel = LogLevel.Trace; // 默认最低日志等级
        //私有构造函数，防止外部实例化
        private Logger()
        {
            // 优先使用 E:\logs 目录，如果不可用则使用应用程序目录
            string preferredLogDirectory = @"E:\logs";
            
            try
            {
                // 检查 E 盘是否存在且可访问
                DriveInfo eDrive = new DriveInfo("E");
                if (eDrive.IsReady)
                {
                    logDirectory = preferredLogDirectory;
                    // 确保日志目录存在
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                }
                else
                {
                    // E 盘不可用，使用应用程序目录
                    logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                }
            }
            catch (Exception)
            {
                // 如果访问 E 盘失败，回退到应用程序目录
                logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
            }
            
            currentLogDate = DateTime.Now.Date;
            currentLogFile = GetLogFilePath(currentLogDate);
            logThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true
            };
            logThread.Start();
        }
        public void SetMinLogLevel(LogLevel level)
        {
            minLogLevel = level;
        }
        //定义调用者信息特性，显示调用位置
        public void Log(LogLevel level, string message,
            [CallerMemberName] string memberName = "",// 方法名
            [CallerFilePath] string filePath = "",    // 文件路径
            [CallerLineNumber] int lineNumber = 0    // 行号
            )
        {
            // 过滤低于最低等级的日志
            if (level < minLogLevel)
            {
                return;
            }
            DateTime now = DateTime.Now;
            string logLine = string.Format(CultureInfo.InvariantCulture,
                "{0:yyyy-MM-dd HH:mm:ss.fff},{1},{2},{3},{4}:{5}",
                now, level, EscapeCSV(message), Path.GetFileName(filePath), memberName, lineNumber);
            logQuene.Add((level, logLine));
        }

        public void LogException(LogLevel level, Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (level < minLogLevel)
            {
                return;
            }
            DateTime now = DateTime.Now;
            string fullMessage = string.IsNullOrEmpty(message) ? ex.ToString() : $"{message}\n{ex}";
            string logLine = string.Format(CultureInfo.InvariantCulture,
                "{0:yyyy-MM-dd HH:mm:ss.fff},{1},{2},{3},{4}:{5}",
                now, level, EscapeCSV(fullMessage), Path.GetFileName(filePath), memberName, lineNumber);
            //消费者线程处理日志消息，不影响ui线程性能
            logQuene.Add((level, logLine));
        }

        public void Trace(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Trace, message, memberName, filePath, lineNumber);
        }

        public void Debug(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Debug, message, memberName, filePath, lineNumber);
        }
        public void Info(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Info, message, memberName, filePath, lineNumber);
        }
        public void Warn(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Warn, message, memberName, filePath, lineNumber);
        }
        public void Error(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Error, message, memberName, filePath, lineNumber);
        }
        public void Fatal(LogLevel level, string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Log(LogLevel.Fatal, message, memberName, filePath, lineNumber);
        }
        //详细错误日志
        public void TraceException(Exception ex,string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Trace, ex, message, memberName, filePath, lineNumber);
        }
        public void DebugException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Debug, ex, message, memberName, filePath, lineNumber);
        }
        public void InfoException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Info, ex, message, memberName, filePath, lineNumber);
        }
        public void WarnException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Warn, ex, message, memberName, filePath, lineNumber);
        }
        public void ErrorException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Error, ex, message, memberName, filePath, lineNumber);
        }
        public void FatalException(Exception ex, string message = "",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            LogException(LogLevel.Fatal, ex, message, memberName, filePath, lineNumber);
        }
        //将日志路径设为只读属性
        public string LogDirctory => logDirectory;

        /// <summary>
        /// 注册日志监听器
        /// 
        /// <para><b>用途：</b></para>
        /// 添加一个新的日志监听器到系统中
        /// 
        /// <para><b>使用场景：</b></para>
        /// - 数据库日志：Logger.GetInstance().RegisterListener(new DatabaseLoggerExtension());
        /// - MES上报：Logger.GetInstance().RegisterListener(new MESLoggerExtension());
        /// - 网络日志：Logger.GetInstance().RegisterListener(new NetworkLoggerExtension());
        /// 
        /// <para><b>注意事项：</b></para>
        /// - 同一监听器只能注册一次（根据引用判断）
        /// - 注册后立即生效，开始接收日志
        /// - 建议在程序启动时注册所有监听器
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// // Program.cs 中
        /// var logger = Logger.GetInstance();
        /// 
        /// // 注册数据库日志
        /// Gvar.Logger.RegisterListener(DatabaseLoggerExtension.Instance);
        /// 
        /// // 注册MES日志
        /// Gvar.Logger.RegisterListener(new MESLoggerExtension());
        /// 
        /// // 查看已注册的监听器
        /// var listeners = Gvar.Logger.GetListeners();
        /// Console.WriteLine($"已注册 {listeners.Count} 个监听器");
        /// </code>
        /// </summary>
        /// <param name="listener">日志监听器实例</param>
        /// <returns>是否注册成功（false表示已存在）</returns>
        public bool RegisterListener(ILogListener listener)
        {
            if (listener == null)
            {
                Console.Error.WriteLine("[Logger] 注册失败：监听器为 null");
                return false;
            }

            lock (listenerLock)
            {
                // 检查是否已注册
                if (listeners.Contains(listener))
                {
                    Console.WriteLine($"[Logger] 监听器已存在: {listener.Name}");
                    return false;
                }

                // 添加到集合
                listeners.Add(listener);
                Console.WriteLine($"[Logger] ✅ 注册监听器: {listener.Name} (当前共 {listeners.Count} 个)");
                
                return true;
            }
        }

        /// <summary>
        /// 注销日志监听器
        /// 
        /// <para><b>用途：</b></para>
        /// 从系统中移除一个日志监听器
        /// 
        /// <para><b>使用场景：</b></para>
        /// - 程序退出时清理资源
        /// - 运行时禁用某个监听器
        /// - 重新配置监听器
        /// 
        /// <para><b>注意事项：</b></para>
        /// - 注销后监听器不再接收日志
        /// - 应在注销前调用监听器的 Shutdown() 方法
        /// - 移除的是引用，不影响监听器本身
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// // 注销并关闭
        /// var dbLogger = DatabaseLoggerExtension.Instance;
        /// dbLogger.Shutdown();  // 先关闭
        /// Logger.GetInstance().UnregisterListener(dbLogger);  // 再注销
        /// </code>
        /// </summary>
        /// <param name="listener">要注销的监听器</param>
        /// <returns>是否注销成功（false表示不存在）</returns>
        public bool UnregisterListener(ILogListener listener)
        {
            if (listener == null)
            {
                return false;
            }

            lock (listenerLock)
            {
                bool removed = listeners.Remove(listener);
                if (removed)
                {
                    Console.WriteLine($"[Logger] ❌ 注销监听器: {listener.Name} (剩余 {listeners.Count} 个)");
                }
                return removed;
            }
        }

        /// <summary>
        /// 获取所有已注册的监听器（只读副本）
        /// 
        /// <para><b>用途：</b></para>
        /// - 查看当前活跃的监听器
        /// - 调试和诊断
        /// - 配置界面显示
        /// 
        /// <para><b>返回值：</b></para>
        /// 返回监听器列表的副本，修改不会影响原列表
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// var listeners = Logger.GetInstance().GetListeners();
        /// foreach (var listener in listeners)
        /// {
        ///     Console.WriteLine($"- {listener.Name}: {(listener.Enabled ? "启用" : "禁用")}");
        /// }
        /// </code>
        /// </summary>
        /// <returns>监听器列表的只读副本</returns>
        public List<ILogListener> GetListeners()
        {
            lock (listenerLock)
            {
                return new List<ILogListener>(listeners);
            }
        }

        /// <summary>
        /// 启用/禁用指定名称的监听器
        /// 
        /// <para><b>用途：</b></para>
        /// - 运行时动态控制监听器
        /// - 性能调试时临时禁用
        /// - 根据配置切换监听器
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// // 禁用数据库日志（临时）
        /// Logger.GetInstance().SetListenerEnabled("DatabaseLogger", false);
        /// 
        /// // 重新启用
        /// Logger.GetInstance().SetListenerEnabled("DatabaseLogger", true);
        /// </code>
        /// </summary>
        /// <param name="listenerName">监听器名称</param>
        /// <param name="enabled">是否启用</param>
        /// <returns>是否设置成功</returns>
        public bool SetListenerEnabled(string listenerName, bool enabled)
        {
            lock (listenerLock)
            {
                var listener = listeners.FirstOrDefault(l => l.Name == listenerName);
                if (listener != null)
                {
                    listener.Enabled = enabled;
                    Console.WriteLine($"[Logger] 监听器 {listenerName}: {(enabled ? "✅ 启用" : "❌ 禁用")}");
                    return true;
                }
                return false;
            }
        }
        private void ProcessLogQueue()
        {
            while (isRunning)
            {
                foreach (var (level,message) in logQuene.GetConsumingEnumerable())
                {
                    while (!isRunning) break;
                    // 缓存日志队列
                    try
                    {
                        // 1️⃣ 触发事件（向后兼容）
                        onLogMessage?.Invoke(this, message);
                        
                        // 2️⃣ 通知所有监听器（新机制）
                        lock (listenerLock)
                        {
                            foreach (var listener in listeners)
                            {
                                try
                                {
                                    // 只调用启用的监听器
                                    if (listener.Enabled)
                                    {
                                        listener.OnLog(level, message);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // 监听器异常不应影响日志系统
                                    Console.Error.WriteLine($"[Logger] 监听器 {listener.Name} 处理失败: {ex.Message}");
                                }
                            }
                        }
                        
                        // 3️⃣ 写入文件（核心功能）
                        DateTime now = DateTime.Now;
                        if (now.Date != currentLogDate || currentFileSize >= MaxLogFileSize)
                        {
                            if(now.Date != currentLogDate)
                            {
                                currentLogDate = now.Date;
                            }
                            if (currentFileSize >= MaxLogFileSize)
                            {
                                ArchiveLogFile(currentLogFile);
                            }
                            currentLogFile = GetLogFilePath(currentLogDate);
                            currentFileSize = 0;
                            CleanupOldLogFiles();
                        }
                        WriteLogToFile(message);
                        currentFileSize += Encoding.UTF8.GetByteCount(message + Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"写入日志文件失败: {ex}");
                    }
                }
            }
        }
        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="logLine">写入日志信息</param>
        private void WriteLogToFile(string message)
        {
            lock (fileLock)
            {
                DateTime now = DateTime.Now;
                // 检查是否需要切换日志文件（按天或按大小）
                if (now.Date != currentLogDate || currentFileSize >= MaxLogFileSize)
                {
                    currentLogDate = now.Date;
                    currentLogFile = GetLogFilePath(currentLogDate);
                    currentFileSize = 0;
                    CleanupOldLogFiles();
                }
                // 写入日志文件
                using (var stream = new FileStream(currentLogFile, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(message);
                    currentFileSize += Encoding.UTF8.GetByteCount(message + Environment.NewLine);
                }
            }
        }
        /// <summary>
        /// 获取日志文件路径
        /// </summary>
        /// <param name="date">文件名称</param>
        /// <returns>文件路径</returns>
        private string GetLogFilePath(DateTime date)
        {
            string fileName = $"log_{date:yyyyMMdd}.csv";
            return Path.Combine(logDirectory, fileName);
        }
        /// <summary>
        /// 文件归档
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void ArchiveLogFile(string filePath)
        {
            lock (fileLock)
            {
                var archiveFileName = Path.Combine(logDirectory,
                        $"log_{currentLogDate:yyyyMMdd}_{Process.GetCurrentProcess().Id}.csv");

                if (File.Exists(currentLogFile))
                {
                    File.Move(currentLogFile, archiveFileName);
                }

                CleanupArchivedFiles();
            }
        }
        /// <summary>
        /// 删除多余的归档文件
        /// </summary>
        private void CleanupArchivedFiles()
        {
            try
            {
                var logfiles = Directory.GetFiles(logDirectory, "log_*.csv");
                if (logfiles.Length <= MaxArchivedFiles)
                {
                    return;
                }
                var filesToDelete = logfiles.Select(file => new FileInfo(file))
                                            .OrderByDescending(file => file.CreationTime)
                                            .Skip(MaxArchivedFiles)
                                            .ToList();
                foreach (var file in filesToDelete)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"无法删除: {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"清理归档文件失败: {ex}");
            }
        }
        /// <summary>
        /// 删除旧日志文件，保留最新的MaxArchivedFiles个文件
        /// </summary>
        private void CleanupOldLogFiles()
        {
            var logFiles = Directory.GetFiles(logDirectory, "log_*.csv")
                                    .Select(file => new FileInfo(file))
                                    .OrderByDescending(file => file.CreationTime)
                                    .ToList();
            for (int i = MaxArchivedFiles; i < logFiles.Count; i++)
            {
                try
                {
                    logFiles[i].Delete();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"删除旧日志文件失败: {ex}");
                }
            }
        }
        //导出.csv日志文件
        public string EscapeCSV(string message)
        {
            if (message.Contains(",") || message.Contains("\"") || message.Contains("\n"))
            {
                message = message.Replace("\"", "\"\"");
                return $"\"{message}\"";
            }
            return message;
        }
        /// <summary>
        /// 释放资源，关闭日志线程
        /// 
        /// <para><b>功能：</b></para>
        /// 1. 停止接收新日志
        /// 2. 处理完队列中剩余日志
        /// 3. 关闭所有监听器
        /// 4. 等待日志线程退出
        /// 
        /// <para><b>调用时机：</b></para>
        /// 程序退出时（Program.cs 的 Main 方法结束前）
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// // Program.cs
        /// Application.Run(new Form1());
        /// 
        /// // 程序退出时
        /// Logger.GetInstance().Log(LogLevel.Info, "程序退出");
        /// Logger.GetInstance().Shutdown();  // ← 确保所有日志都写入
        /// </code>
        /// </summary>
        public void Shutdown()
        {
            Console.WriteLine("[Logger] 开始关闭日志系统...");
            
            // 1. 停止接收新日志
            isRunning = false;
            logQuene.CompleteAdding();
            
            // 2. 等待日志线程处理完剩余日志
            if (logThread != null && logThread.IsAlive)
            {
                Console.WriteLine("[Logger] 等待日志线程退出...");
                logThread.Join(TimeSpan.FromSeconds(5));
            }
            
            // 3. 关闭所有监听器
            lock (listenerLock)
            {
                foreach (var listener in listeners)
                {
                    try
                    {
                        Console.WriteLine($"[Logger] 关闭监听器: {listener.Name}");
                        listener.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"[Logger] 关闭监听器 {listener.Name} 失败: {ex.Message}");
                    }
                }
                listeners.Clear();
            }
            
            Console.WriteLine("[Logger] ✅ 日志系统已关闭");
        }
    }
}
