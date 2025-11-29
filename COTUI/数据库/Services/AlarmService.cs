using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using COTUI.数据库.Models;

namespace COTUI.数据库.Services
{
    /// <summary>
    /// 加载加载化
    /// </summary>
    public class AlarmService
    {
        // 使用全局变量 Gvar.DB 访问数据库

        /// <summary>
        /// 到位ӱ到位
        /// </summary>
        public bool AddAlarm(AlarmModel alarm)
        {
            try
            {
                string sql = @"INSERT INTO Alarms (AlarmTime, Level, Content, Station, AlarmCode, IsHandled) 
                              VALUES (@alarmTime, @level, @content, @station, @alarmCode, @isHandled)";

                Gvar.DB.ExecuteNonQuery(sql,
                    new SQLiteParameter("@alarmTime", alarm.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    new SQLiteParameter("@level", alarm.Level),
                    new SQLiteParameter("@content", alarm.Content),
                    new SQLiteParameter("@station", alarm.Station ?? ""),
                    new SQLiteParameter("@alarmCode", alarm.AlarmCode ?? ""),
                    new SQLiteParameter("@isHandled", alarm.IsHandled ? 1 : 0));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 查询加载加载ɸѡ加载化
        /// </summary>
        public List<AlarmModel> GetAlarms(DateTime? startTime = null, DateTime? endTime = null,
            string level = null, string station = null, bool? isHandled = null, int maxRecords = 1000)
        {
            List<string> conditions = new List<string>();
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();

            if (startTime.HasValue)
            {
                conditions.Add("datetime(AlarmTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                conditions.Add("datetime(AlarmTime) <= datetime(@endTime)");
                parameters.Add(new SQLiteParameter("@endTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (!string.IsNullOrEmpty(level))
            {
                conditions.Add("Level = @level");
                parameters.Add(new SQLiteParameter("@level", level));
            }

            if (!string.IsNullOrEmpty(station) && station != "ȫ加载վ")
            {
                conditions.Add("Station = @station");
                parameters.Add(new SQLiteParameter("@station", station));
            }

            if (isHandled.HasValue)
            {
                conditions.Add("IsHandled = @isHandled");
                parameters.Add(new SQLiteParameter("@isHandled", isHandled.Value ? 1 : 0));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            string sql = $"SELECT * FROM Alarms {whereClause} ORDER BY datetime(AlarmTime) DESC LIMIT {maxRecords}";

            DataTable dt = Gvar.DB.ExecuteQuery(sql, parameters.ToArray());

            List<AlarmModel> alarms = new List<AlarmModel>();
            foreach (DataRow row in dt.Rows)
            {
                alarms.Add(DataRowToAlarm(row));
            }
            return alarms;
        }

        /// <summary>
        /// 加载加载
        /// </summary>
        public bool HandleAlarm(long alarmId, string handleUser, string handleRemark)
        {
            try
            {
                string sql = @"UPDATE Alarms SET IsHandled = 1, HandleTime = @handleTime, 
                              HandleUser = @handleUser, HandleRemark = @handleRemark WHERE Id = @id";

                Gvar.DB.ExecuteNonQuery(sql,
                    new SQLiteParameter("@handleTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SQLiteParameter("@handleUser", handleUser),
                    new SQLiteParameter("@handleRemark", handleRemark ?? ""),
                    new SQLiteParameter("@id", alarmId));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 化取加载ͳ夹ƣ加载죩
        /// </summary>
        public Dictionary<string, int> GetDailyAlarmStats(DateTime startDate, DateTime endDate)
        {
            string sql = @"SELECT DATE(AlarmTime) as AlarmDate, COUNT(*) as Count 
                          FROM Alarms 
                          WHERE datetime(AlarmTime) >= datetime(@startDate) AND datetime(AlarmTime) <= datetime(@endDate) 
                          GROUP BY DATE(AlarmTime) 
                          ORDER BY AlarmDate DESC";

            DataTable dt = Gvar.DB.ExecuteQuery(sql,
                new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@endDate", endDate.AddDays(1).ToString("yyyy-MM-dd")));

            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                stats[row["AlarmDate"].ToString()] = Convert.ToInt32(row["Count"]);
            }
            return stats;
        }

        /// <summary>
        /// 化取加载ͳ夹ƣ加载到位
        /// </summary>
        public Dictionary<string, int> GetAlarmStatsByLevel(DateTime? startTime = null, DateTime? endTime = null)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            List<string> conditions = new List<string>();

            if (startTime.HasValue)
            {
                conditions.Add("datetime(AlarmTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                conditions.Add("datetime(AlarmTime) <= datetime(@endTime)");
                parameters.Add(new SQLiteParameter("@endTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            string sql = $"SELECT Level, COUNT(*) as Count FROM Alarms {whereClause} GROUP BY Level";

            DataTable dt = Gvar.DB.ExecuteQuery(sql, parameters.ToArray());

            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                stats[row["Level"].ToString()] = Convert.ToInt32(row["Count"]);
            }
            return stats;
        }

        /// <summary>
        /// 化 DataRow ת化为 AlarmModel
        /// </summary>
        private AlarmModel DataRowToAlarm(DataRow row)
        {
            return new AlarmModel
            {
                Id = Convert.ToInt64(row["Id"]),
                AlarmTime = DateTime.Parse(row["AlarmTime"].ToString()),
                Level = row["Level"].ToString(),
                Content = row["Content"].ToString(),
                Station = row["Station"].ToString(),
                AlarmCode = row["AlarmCode"].ToString(),
                IsHandled = Convert.ToInt32(row["IsHandled"]) == 1,
                HandleTime = row["HandleTime"] != DBNull.Value ? (DateTime?)DateTime.Parse(row["HandleTime"].ToString()) : null,
                HandleUser = row["HandleUser"].ToString(),
                HandleRemark = row["HandleRemark"].ToString()
            };
        }
    }
}
