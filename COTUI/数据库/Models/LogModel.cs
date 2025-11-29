using System;

namespace COTUI.数据库.Models
{
    /// <summary>
    /// 日志数据模型
    /// </summary>
    public class LogModel
    {
        /// <summary>
        /// 日志ID（主键，自增）
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 日志级别（Trace, Debug, Info, Warn, Error, Fatal）
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 来源（文件名、类名等）
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 工站（工站1-5或全部工站）
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// 线程ID
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// 异常堆栈（如果有）
        /// </summary>
        public string StackTrace { get; set; }
    }
}
