using System;

namespace COTUI.数据库.Models
{
    /// <summary>
    /// 系统配置数据模型
    /// </summary>
    public class SystemConfigModel
    {
        /// <summary>
        /// 配置ID（主键，自增）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置分组（如：System, Vision, MQTT）
        /// </summary>
        public string ConfigGroup { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateUser { get; set; }
    }
}
