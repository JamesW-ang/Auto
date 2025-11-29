using System;

namespace COTUI.数据库.Models
{
    /// <summary>
    /// 报警数据模型
    /// </summary>
    public class AlarmModel
    {
        /// <summary>
        /// 报警ID（主键，自增）
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }

        /// <summary>
        /// 报警级别（Info=提示, Warn=警告, Error=严重）
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 报警内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 工站
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// 报警代码（方便统计分类）
        /// </summary>
        public string AlarmCode { get; set; }

        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsHandled { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string HandleUser { get; set; }

        /// <summary>
        /// 处理备注
        /// </summary>
        public string HandleRemark { get; set; }
    }
}
