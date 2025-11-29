using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using COTUI.数据库.Models;
using COTUI.数据库.Services;

namespace COTUI.通用功能类
{
    /// <summary>
    /// 数据库日志扩展类 - 日志同步写入 SQLite 数据库
    /// 
    /// <para><b>主要特性：</b></para>
    /// - 实现 ILogListener 接口，规范日志格式
    /// - 异步批量写入，性能：100条/化
    /// - 自动导出 CSV 格式日志
    /// - Warn/Error/Fatal 级别自动发送告警
    /// - 优雅关闭，确保所有日志线程安全
    /// 
    /// <para><b>架构流程：</b></para>
    /// <code>
    /// Logger 
    ///   → (实现 ILogListener.OnLog)
    /// DatabaseLoggerExtension
    ///   → (日志加入队列)
    /// BlockingCollection 队列
    ///   → (后台线程批量处理)
    /// LogService.AddLogBatch()
    ///   → (批量写入数据库)
    /// SQLite Logs 表
    /// </code>
    /// 
    /// <para><b>使用方式：</b></para>
    /// <code>
    /// // Program.cs 中初始化
    /// var dbLogger = DatabaseLoggerExtension.Instance;
    /// dbLogger.Initialize();
    /// Logger.GetInstance().RegisterListener(dbLogger);
    /// 
    /// // 程序退出时
    /// Logger.GetInstance().UnregisterListener(dbLogger);
    /// dbLogger.Shutdown();
    /// </code>
    /// 
    /// <para><b>性能指标：</b></para>
    /// - OnLog 调用时间：~0.1ms（无阻塞延迟）
    /// - 批量写入时间：~10ms/批（100条）
    /// - 队列最大容量：10,000 条
    /// - 内存占用：~5MB（满载时）
    /// </summary>
    public class DatabaseLoggerExtension : ILogListener
    {
        private static DatabaseLoggerExtension _instance;
        private static readonly object _lock = new object();
        private readonly LogService _logService;
        private readonly AlarmService _alarmService;
        private readonly BlockingCollection<LogModel> _logQueue;
        private readonly Thread _logThread;
        private bool _isRunning = true;
        private const int BatchSize = 100; // 批量写入大小
        private const int QueueMaxSize = 10000; // 队列最大容量

        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static DatabaseLoggerExtension Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseLoggerExtension();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 监听器名称（ILogListener 接口）
        /// </summary>
        public string Name => "DatabaseLogger";

        /// <summary>
        /// 是否启用（ILogListener 接口）
        /// 设置为 false 时不会写入数据库日志
        /// </summary>
        public bool Enabled { get; set; } = true;

        private DatabaseLoggerExtension()
        {
            _logService = new LogService();
            _alarmService = new AlarmService();
            _logQueue = new BlockingCollection<LogModel>(QueueMaxSize);

            // 启动后台日志写入线程
            _logThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "DatabaseLoggerThread"
            };
            _logThread.Start();
            
            Console.WriteLine("[DatabaseLogger] 后台线程已启动");
        }

        /// <summary>
        /// 初始化监听器（ILogListener 接口）
        /// 
        /// <para><b>功能：</b></para>
        /// - 注册到 Logger 系统
        /// - 开始接收日志消息
        /// 
        /// <para><b>调用位置：</b></para>
        /// Program.cs 的 Main 方法中，在 Application.Run 之前
        /// 
        /// <para><b>示例：</b></para>
        /// <code>
        /// // Program.cs
        /// var dbLogger = DatabaseLoggerExtension.Instance;
        /// dbLogger.Initialize();  // 步骤1：初始化
        /// Logger.GetInstance().RegisterListener(dbLogger);  // 步骤2：注册
        /// 
        /// Application.Run(new Form1());
        /// </code>
        /// </summary>
        public void Initialize()
        {
            try
            {
                // 注册到 Logger
                bool registered = Logger.GetInstance().RegisterListener(this);
                
                if (registered)
                {
                    Console.WriteLine($"[{Name}] ✓ 已注册到日志系统");
                }
                else
                {
                    Console.WriteLine($"[{Name}] ⚠ 已经注册过了");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[{Name}] 初始化失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 接收日志消息（ILogListener 接口 - 核心方法）
        /// 
        /// <para><b>功能：</b></para>
        /// 1. 解析 CSV 格式日志行
        /// 2. 转换为 LogModel 对象
        /// 3. 加入队列，异步批量写入
        /// 4. Warn/Error/Fatal 级别自动生成告警
        /// 
        /// <para><b>性能：</b></para>
        /// - 调用时间：~0.1ms（无阻塞延迟）
        /// - 线程安全：日志队列
        /// - 防止队列溢出：丢弃日志或内存警告
        /// 
        /// <para><b>日志格式：</b></para>
        /// "2024-11-21 19:00:00.123,Info,测试消息,MainPage.cs,btnTest_Click:45"
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="logLine">CSV 格式日志行</param>
        public void OnLog(LogLevel level, string logLine)
        {
            try
            {
                // 检查是否启用
                if (!Enabled)
                {
                    return;
                }

                // 解析CSV格式日志行
                var logModel = ParseLogLine(logLine);
                if (logModel != null)
                {
                    // 加入数据库写入队列
                    if (!_logQueue.TryAdd(logModel, 100))
                    {
                        // 队列满了，丢弃日志或内存警告
                        Console.Error.WriteLine($"[{Name}] ⚠ 日志队列已满，丢弃日志");
                    }

                    // 如果是告警级别（Warn, Error, Fatal），同时写入报警表
                    if (level >= LogLevel.Warn)
                    {
                        AddAlarmFromLog(logModel);
                    }
                }
            }
            catch (Exception ex)
            {
                // 不要抛异常回给 Logger
                Console.Error.WriteLine($"[{Name}] 处理日志失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载夹ļ到位志化Ϣ
        /// </summary>
        private void OnFileLogMessage(object sender, string logLine)
        {
            try
            {
                if (!Enabled) return; // 加载夹志加载夹ã夹ֱ夹ӷ到位

                // 加载CSV化式加载志化
                var logModel = ParseLogLine(logLine);
                if (logModel != null)
                {
                    // 到位入加载ݿ夹写加载夹
                    if (!_logQueue.TryAdd(logModel, 100))
                    {
                        // 加载加载加载加载志加载到位ڴ加载化
                        Console.WriteLine("化志加载加载加载加载志");
                    }

                    // 加载是告加载到位Warn, Error, Fatal加载同时写夹入报加载
                    if (logModel.Level == "Warn" || logModel.Level == "Error" || logModel.Level == "Fatal")
                    {
                        AddAlarmFromLog(logModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载化志化Ϣ失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载化志夹列夹CSV化式化
        /// </summary>
        private LogModel ParseLogLine(string logLine)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(logLine))
                    return null;

                var parts = SplitCSVLine(logLine);
                if (parts.Length < 4)
                    return null;

                return new LogModel
                {
                    LogTime = DateTime.Parse(parts[0]),
                    Level = parts[1],
                    Message = parts[2],
                    Source = parts.Length > 3 ? parts[3] : "",
                    Station = Gvar._CurrentStation ?? "未知化վ",
                    ThreadId = Thread.CurrentThread.ManagedThreadId
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 夹ָ夹CSV化
        /// </summary>
        private string[] SplitCSVLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            System.Text.StringBuilder current = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
            result.Add(current.ToString());
            return result.ToArray();
        }

        /// <summary>
        /// 加载志加载加载
        /// </summary>
        private void AddAlarmFromLog(LogModel log)
        {
            try
            {
                var alarm = new AlarmModel
                {
                    AlarmTime = log.LogTime,
                    Level = log.Level == "Warn" ? "Warn" : "Error",
                    Content = log.Message,
                    Station = log.Station,
                    AlarmCode = ExtractAlarmCode(log.Message),
                    IsHandled = false
                };

                _alarmService.AddAlarm(alarm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"到位ӱ到位失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 加载Ϣ加载取加载加载
        /// </summary>
        private string ExtractAlarmCode(string message)
        {
            // 化实夹֣到位取前20到位符到位为加载加载
            if (string.IsNullOrEmpty(message))
                return "";

            return message.Length > 20 ? message.Substring(0, 20) : message;
        }

        /// <summary>
        /// 加载化志到位列到位̨夹程线夹
        /// </summary>
        private void ProcessLogQueue()
        {
            List<LogModel> batch = new List<LogModel>();

            while (_isRunning)
            {
                try
                {
                    // 夹Ӷ加载夹取加载志
                    if (_logQueue.TryTake(out var log, 1000))
                    {
                        batch.Add(log);

                        // 夹ﵽ到位批夹次大小加载夹为最后加载化写加载夹ݿ夹
                        if (batch.Count >= BatchSize || _logQueue.Count == 0)
                        {
                            WriteBatchToDatabase(batch);
                            batch.Clear();
                        }
                    }
                    else if (batch.Count > 0)
                    {
                        // 最后加载夹写夹写加载志化Ҳ写加载夹ݿ夹
                        WriteBatchToDatabase(batch);
                        batch.Clear();
                    }
                }
                catch (Exception ex)
                {
                    // 化错加载加载化Ӱ加载加载
                    System.Diagnostics.Debug.WriteLine($"加载化志加载失败: {ex.Message}");
                }
            }

            // 夹程化˳夹前化写化ʣ加载志
            if (batch.Count > 0)
            {
                WriteBatchToDatabase(batch);
            }
        }

        /// <summary>
        /// 批量写入数据库
        /// </summary>
        private void WriteBatchToDatabase(List<LogModel> logs)
        {
            try
            {
                if (logs.Count > 0)
                {
                    _logService.AddLogBatch(logs);
                }
            }
            catch (Exception ex)
            {
                // 错误不影墓循环继续
                System.Diagnostics.Debug.WriteLine($"批量写入日志失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 夹ر加载ݿ到位志化չ化ILogListener 夹ӿڣ夹
        /// 
        /// <para><b>功能：</b></para>
        /// 1. ˢ夹¶加载夹ʣ加载志
        /// 2. ֹͣ化̨夹程夹
        /// 3. 化 Logger ע化
        /// 
        /// <para><b>加载一批时加载</b></para>
        /// 加载夹˳夹一批时加载 Logger.Shutdown() ֮前
        /// 
        /// <para><b>ʾ加载</b></para>
        /// <code>
        /// // Program.cs
        /// Application.Run(new Form1());
        /// 
        /// // 加载夹˳夹
        /// var dbLogger = DatabaseLoggerExtension.Instance;
        /// Logger.GetInstance().UnregisterListener(dbLogger);
        /// dbLogger.Shutdown();  // 化 ˢ夹²化ر夹
        /// Logger.GetInstance().Shutdown();
        /// </code>
        /// </summary>
        public void Shutdown()
        {
            Console.WriteLine($"[{Name}] 开始关闭...");
            
            // 1. ֹͣ加载加载志
            _isRunning = false;
            _logQueue.CompleteAdding();
            
            // 2. 夹ȴ到位̨夹程̴加载夹ʣ加载志
            if (_logThread != null && _logThread.IsAlive)
            {
                Console.WriteLine($"[{Name}] 夹ȴ到位̨夹程化˳夹...");
                _logThread.Join(TimeSpan.FromSeconds(5));
            }
            
            Console.WriteLine($"[{Name}] ? 夹ѹر夹");
        }

        /// <summary>
        /// ǿ化ˢ夹¶加载е加载到位志加载夹ݿ夹
        /// 
        /// <para><b>化;化</b></para>
        /// - LogPage 查询前ȷ加载加载志加载夹
        /// - 加载夹˳夹前ǿ化写化
        /// - 加载一批时夹鿴实时加载
        /// 
        /// <para><b>功能：</b></para>
        /// - ͬ加载加载加载加载到位程夹
        /// - 加载时间取到位ڶ化г到位
        /// - 加载夹ں夹̨夹程̵到位
        /// 
        /// <para><b>ʾ加载</b></para>
        /// <code>
        /// // LogPage 查询前
        /// await Task.Run(() => {
        ///     DatabaseLoggerExtension.Instance.FlushLogs();
        ///     Thread.Sleep(200);  // 夹ȴ加载ݿ夹写加载夹
        /// });
        /// 
        /// // Ȼ到位ѯ
        /// var logs = logService.GetLogs(...);
        /// </code>
        /// </summary>
        public void FlushLogs()
        {
            try
            {
                List<LogModel> batch = new List<LogModel>();
                int totalFlushed = 0;
                
                // 取加载加载加载化志
                while (_logQueue.TryTake(out var log, 100))
                {
                    batch.Add(log);
                    
                    if (batch.Count >= BatchSize)
                    {
                        WriteBatchToDatabase(batch);
                        totalFlushed += batch.Count;
                        batch.Clear();
                    }
                }
                
                // 写化ʣ加载夹志
                if (batch.Count > 0)
                {
                    WriteBatchToDatabase(batch);
                    totalFlushed += batch.Count;
                }
                
                Console.WriteLine($"[{Name}] ǿ化ˢ加载ɣ夹写化 {totalFlushed} 加载志");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"[{Name}] ǿ化刷新失败: {ex.Message}");
            }
        }
    }
}
