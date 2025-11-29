using System;

namespace COTUI.数据库.Models
{
    /// <summary>
    /// 用户数据模型
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户ID（主键，自增）
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名（唯一）
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码（建议使用加密存储）
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 角色（admin=管理员, user=操作员, viewer=查看者）
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
