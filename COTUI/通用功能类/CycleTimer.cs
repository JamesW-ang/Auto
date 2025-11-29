using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace COTUI.通用功能类
{
    /// <summary>
    /// 周期时间（CT - Cycle Time）计时器
    /// 
    /// <para><b>功能：</b></para>
    /// - 高精度计时（微秒级）
    /// - 支持多个计时点
    /// - 自动计算各阶段耗时
    /// 
    /// <para><b>使用场景：</b></para>
    /// - 生产周期时间测量
    /// - 设备性能分析
    /// - 工艺流程优化
    /// 
    /// <para><b>示例：</b></para>
    /// <code>
    /// // 1. 创建计时器
    /// var timer = new CycleTimer("生产周期");
    /// 
    /// // 2. 开始计时
    /// timer.Start();
    /// 
    /// // 3. 执行动作并标记
    /// DoLoading();
    /// timer.Mark("上料完成");
    /// 
    /// DoInspection();
    /// timer.Mark("检测完成");
    /// 
    /// DoUnloading();
    /// timer.Mark("下料完成");
    /// 
    /// // 4. 结束并获取结果
    /// var result = timer.Stop();
    /// Console.WriteLine($"总耗时: {result.TotalMilliseconds:F3}ms");
    /// </code>
    /// </summary>
    public class CycleTimer
    {
        #region 字段

        private readonly string name;
        private readonly Stopwatch stopwatch;
        private readonly List<TimeMark> marks;
        private DateTime startTime;
        private bool isRunning;

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建周期计时器
        /// </summary>
        /// <param name="name">计时器名称（如："生产周期"、"检测流程"）</param>
        public CycleTimer(string name = "默认计时器")
        {
            this.name = name;
            this.stopwatch = new Stopwatch();
            this.marks = new List<TimeMark>();
        }

        #endregion

        #region 基础计时

        /// <summary>
        /// 开始计时
        /// </summary>
        public void Start()
        {
            if (isRunning)
            {
                throw new InvalidOperationException("计时器已在运行中");
            }

            marks.Clear();
            startTime = DateTime.Now;
            stopwatch.Restart();
            isRunning = true;
        }

        /// <summary>
        /// 标记时间点
        /// </summary>
        /// <param name="markName">标记名称（如："上料完成"、"检测完成"）</param>
        /// <returns>从开始到当前标记的总耗时（毫秒）</returns>
        public double Mark(string markName)
        {
            if (!isRunning)
            {
                throw new InvalidOperationException("计时器未运行");
            }

            double elapsed = stopwatch.Elapsed.TotalMilliseconds;
            marks.Add(new TimeMark
            {
                Name = markName,
                ElapsedMilliseconds = elapsed,
                Timestamp = DateTime.Now
            });

            return elapsed;
        }

        /// <summary>
        /// 停止计时并返回结果
        /// </summary>
        /// <returns>计时结果</returns>
        public CycleTimerResult Stop()
        {
            if (!isRunning)
            {
                throw new InvalidOperationException("计时器未运行");
            }

            stopwatch.Stop();
            isRunning = false;

            var result = new CycleTimerResult
            {
                Name = name,
                StartTime = startTime,
                EndTime = DateTime.Now,
                TotalMilliseconds = stopwatch.Elapsed.TotalMilliseconds,
                Marks = new List<TimeMark>(marks)
            };

            return result;
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        public void Reset()
        {
            stopwatch.Reset();
            marks.Clear();
            isRunning = false;
        }

        #endregion

        #region 快捷方法

        /// <summary>
        /// 测量一段代码的执行时间
        /// </summary>
        /// <param name="action">要测量的代码</param>
        /// <param name="actionName">操作名称</param>
        /// <returns>执行耗时（毫秒）</returns>
        public static double Measure(Action action, string actionName = "操作")
        {
            var timer = new CycleTimer(actionName);
            timer.Start();
            
            try
            {
                action();
            }
            finally
            {
                timer.Stop();
            }
            
            return timer.ElapsedMilliseconds;
        }

        /// <summary>
        /// 测量一段代码的执行时间（带返回值）
        /// </summary>
        public static (T Result, double Milliseconds) Measure<T>(Func<T> func, string actionName = "操作")
        {
            var timer = new CycleTimer(actionName);
            timer.Start();
            T result = default(T);
            
            try
            {
                result = func();
            }
            finally
            {
                timer.Stop();
            }
            
            return (result, timer.ElapsedMilliseconds);
        }

        #endregion

        #region 属性

        /// <summary>
        /// 计时器名称
        /// </summary>
        public string Name => name;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning => isRunning;

        /// <summary>
        /// 当前已运行时间（毫秒）
        /// </summary>
        public double ElapsedMilliseconds => stopwatch.Elapsed.TotalMilliseconds;

        /// <summary>
        /// 标记点数量
        /// </summary>
        public int MarkCount => marks.Count;

        #endregion
    }

    #region 数据类

    /// <summary>
    /// 时间标记点
    /// </summary>
    public class TimeMark
    {
        /// <summary>
        /// 标记名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 从开始到此标记的总耗时（毫秒）
        /// </summary>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 标记时刻
        /// </summary>
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"{Name}: {ElapsedMilliseconds:F3}ms";
        }
    }

    /// <summary>
    /// 周期计时结果
    /// </summary>
    public class CycleTimerResult
    {
        /// <summary>
        /// 计时器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 总耗时（毫秒）
        /// </summary>
        public double TotalMilliseconds { get; set; }

        /// <summary>
        /// 总耗时（秒）
        /// </summary>
        public double TotalSeconds => TotalMilliseconds / 1000.0;

        /// <summary>
        /// 所有时间标记
        /// </summary>
        public List<TimeMark> Marks { get; set; }

        /// <summary>
        /// 获取指定阶段的耗时
        /// </summary>
        /// <param name="markName">阶段标记名称</param>
        /// <returns>该阶段的耗时（毫秒），如果未找到返回0</returns>
        public double GetStageTime(string markName)
        {
            TimeMark mark = Marks.FirstOrDefault(m => m.Name == markName);
            if (mark == null) return 0;

            int index = Marks.IndexOf(mark);
            if (index == 0)
            {
                // 第一个阶段：从开始到第一个标记
                return mark.ElapsedMilliseconds;
            }
            else
            {
                // 其他阶段：当前标记 - 前一个标记
                return mark.ElapsedMilliseconds - Marks[index - 1].ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// 获取所有阶段的耗时
        /// </summary>
        /// <returns>阶段名称 → 耗时（毫秒）</returns>
        public Dictionary<string, double> GetAllStageTimes()
        {
            var result = new Dictionary<string, double>();

            for (int i = 0; i < Marks.Count; i++)
            {
                string stageName = i == 0 
                    ? $"阶段1: 开始 → {Marks[i].Name}" 
                    : $"阶段{i + 1}: {Marks[i - 1].Name} → {Marks[i].Name}";

                double stageTime = i == 0 
                    ? Marks[i].ElapsedMilliseconds 
                    : Marks[i].ElapsedMilliseconds - Marks[i - 1].ElapsedMilliseconds;

                result[stageName] = stageTime;
            }

            return result;
        }

        /// <summary>
        /// 生成详细报告
        /// </summary>
        public string GenerateReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine($"【{Name}】周期时间报告");
            report.AppendLine($"开始时间: {StartTime:yyyy-MM-dd HH:mm:ss.fff}");
            report.AppendLine($"结束时间: {EndTime:yyyy-MM-dd HH:mm:ss.fff}");
            report.AppendLine($"总耗时: {TotalMilliseconds:F3}ms ({TotalSeconds:F3}s)");
            report.AppendLine();

            if (Marks.Count > 0)
            {
                report.AppendLine("阶段详情:");
                var stageTimes = GetAllStageTimes();
                foreach (var stage in stageTimes)
                {
                    double percentage = (stage.Value / TotalMilliseconds) * 100;
                    report.AppendLine($"  {stage.Key}: {stage.Value:F3}ms ({percentage:F1}%)");
                }
            }

            return report.ToString();
        }

        public override string ToString()
        {
            return $"{Name}: {TotalMilliseconds:F3}ms (共{Marks.Count}个阶段)";
        }
    }

    #endregion
    
    #region 带日志的计时器
    
    /// <summary>
    /// 带日志的周期计时器
    /// 
    /// <para><b>功能：</b></para>
    /// - 自动记录日志到 Logger
    /// - 无需手动调用 Gvar.Logger.Log()
    /// - 简化代码
    /// 
    /// <para><b>使用示例：</b></para>
    /// <code>
    /// var timer = new LoggingCycleTimer("生产周期");
    /// timer.Start();  // 自动打印：⏱ 开始计时
    /// 
    /// DoLoading();
    /// timer.Mark("上料完成");  // 自动打印：✓ 上料完成 (850.250ms)
    /// 
    /// DoInspection();
    /// timer.Mark("检测完成");  // 自动打印：✓ 检测完成 (2050.750ms)
    /// 
    /// var result = timer.Stop();  // 自动打印：⏱ 总CT: 2333.125ms
    /// </code>
    /// </summary>
    public class LoggingCycleTimer : CycleTimer
    {
        public LoggingCycleTimer(string name = "生产周期") : base(name)
        {
        }
        
        /// <summary>
        /// 开始计时（自动记录日志）
        /// </summary>
        public new void Start()
        {
            base.Start();
            Gvar.Logger.Log($"⏱ 开始计时: {Name}");
        }
        
        /// <summary>
        /// 标记时间点（自动记录日志）
        /// </summary>
        public new double Mark(string markName)
        {
            double elapsed = base.Mark(markName);
            
            // 自动记录日志
            Gvar.Logger.Log($"✓ {markName} (耗时: {elapsed:F3}ms)");
            
            return elapsed;
        }
        
        /// <summary>
        /// 停止计时（自动记录日志）
        /// </summary>
        public new CycleTimerResult Stop()
        {
            var result = base.Stop();
            
            // 自动记录总时间
            Gvar.Logger.Log($"⏱ 总CT: {result.TotalMilliseconds:F3}ms");
            
            return result;
        }
    }
    
    #endregion
}
