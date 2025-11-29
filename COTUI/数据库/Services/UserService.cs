using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using COTUI.数据库.Models;

namespace COTUI.数据库.Services
{
    /// <summary>
    /// 夹û加载到位
    /// </summary>
    public class UserService
    {
        // 使用全局变量 Gvar.DB 访问数据库

        /// <summary>
        /// 化֤夹û到位¼
        /// </summary>
        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password AND IsEnabled = 1";
            object result = Gvar.DB.ExecuteScalar(sql,
                new SQLiteParameter("@username", username),
                new SQLiteParameter("@password", password));

            bool isValid = Convert.ToInt32(result) > 0;

            if (isValid)
            {
                // 加载加载¼时间
                UpdateLastLoginTime(username);
            }

            return isValid;
        }

        /// <summary>
        /// 化取夹û到位Ϣ
        /// </summary>
        public UserModel GetUser(string username)
        {
            string sql = "SELECT * FROM Users WHERE Username = @username";
            DataTable dt = Gvar.DB.ExecuteQuery(sql, new SQLiteParameter("@username", username));

            if (dt.Rows.Count > 0)
            {
                return DataRowToUser(dt.Rows[0]);
            }
            return null;
        }

        /// <summary>
        /// 化取加载用户
        /// </summary>
        public List<UserModel> GetAllUsers()
        {
            string sql = "SELECT * FROM Users ORDER BY CreateTime DESC";
            DataTable dt = Gvar.DB.ExecuteQuery(sql);

            List<UserModel> users = new List<UserModel>();
            foreach (DataRow row in dt.Rows)
            {
                users.Add(DataRowToUser(row));
            }
            return users;
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        public bool AddUser(UserModel user)
        {
            try
            {
                string sql = @"INSERT INTO Users (Username, Password, RealName, Role, IsEnabled, CreateTime, Remark) 
                              VALUES (@username, @password, @realName, @role, @isEnabled, @createTime, @remark)";

                Gvar.DB.ExecuteNonQuery(sql,
                    new SQLiteParameter("@username", user.Username),
                    new SQLiteParameter("@password", user.Password),
                    new SQLiteParameter("@realName", user.RealName ?? ""),
                    new SQLiteParameter("@role", user.Role),
                    new SQLiteParameter("@isEnabled", user.IsEnabled ? 1 : 0),
                    new SQLiteParameter("@createTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SQLiteParameter("@remark", user.Remark ?? ""));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 加载用户
        /// </summary>
        public bool UpdateUser(UserModel user)
        {
            try
            {
                string sql = @"UPDATE Users SET Password = @password, RealName = @realName, 
                              Role = @role, IsEnabled = @isEnabled, Remark = @remark 
                              WHERE Username = @username";

                Gvar.DB.ExecuteNonQuery(sql,
                    new SQLiteParameter("@password", user.Password),
                    new SQLiteParameter("@realName", user.RealName ?? ""),
                    new SQLiteParameter("@role", user.Role),
                    new SQLiteParameter("@isEnabled", user.IsEnabled ? 1 : 0),
                    new SQLiteParameter("@remark", user.Remark ?? ""),
                    new SQLiteParameter("@username", user.Username));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ɾ到位û夹
        /// </summary>
        public bool DeleteUser(string username)
        {
            try
            {
                // 加载化ɾ化admin用户
                if (username.ToLower() == "admin")
                {
                    return false;
                }

                string sql = "DELETE FROM Users WHERE Username = @username";
                Gvar.DB.ExecuteNonQuery(sql, new SQLiteParameter("@username", username));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 加载加载¼时间
        /// </summary>
        private void UpdateLastLoginTime(string username)
        {
            string sql = "UPDATE Users SET LastLoginTime = @time WHERE Username = @username";
            Gvar.DB.ExecuteNonQuery(sql,
                new SQLiteParameter("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new SQLiteParameter("@username", username));
        }

        /// <summary>
        /// 夹޸加载夹
        /// </summary>
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            // 加载֤加载化
            if (!ValidateUser(username, oldPassword))
            {
                return false;
            }

            string sql = "UPDATE Users SET Password = @newPassword WHERE Username = @username";
            Gvar.DB.ExecuteNonQuery(sql,
                new SQLiteParameter("@newPassword", newPassword),
                new SQLiteParameter("@username", username));

            return true;
        }

        /// <summary>
        /// 化 DataRow ת化为 UserModel
        /// </summary>
        private UserModel DataRowToUser(DataRow row)
        {
            return new UserModel
            {
                Id = Convert.ToInt32(row["Id"]),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                RealName = row["RealName"].ToString(),
                Role = row["Role"].ToString(),
                IsEnabled = Convert.ToInt32(row["IsEnabled"]) == 1,
                CreateTime = DateTime.Parse(row["CreateTime"].ToString()),
                LastLoginTime = row["LastLoginTime"] != DBNull.Value ? (DateTime?)DateTime.Parse(row["LastLoginTime"].ToString()) : null,
                Remark = row["Remark"].ToString()
            };
        }
    }
}
