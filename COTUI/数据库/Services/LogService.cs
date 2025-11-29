using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using COTUI.数据库.Models;

namespace COTUI.数据库.Services
{
    /// <summary>
    /// 化志加载化
    /// </summary>
    public class LogService
    {
        private readonly DatabaseHelper db = DatabaseHelper.Instance;

        /// <summary>
        /// 加载化志
        /// </summary>
        public bool AddLog(LogModel log)
        {
            try
            {
                string sql = @"INSERT INTO Logs (LogTime, Level, Message, Source, Station, ThreadId, StackTrace) 
                              VALUES (@logTime, @level, @message, @source, @station, @threadId, @stackTrace)";

                db.ExecuteNonQuery(sql,
                    new SQLiteParameter("@logTime", log.LogTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    new SQLiteParameter("@level", log.Level),
                    new SQLiteParameter("@message", log.Message),
                    new SQLiteParameter("@source", log.Source ?? ""),
                    new SQLiteParameter("@station", log.Station ?? ""),
                    new SQLiteParameter("@threadId", log.ThreadId),
                    new SQLiteParameter("@stackTrace", log.StackTrace ?? ""));

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 加载加载化志加载加载ܣ夹
        /// </summary>
        public bool AddLogBatch(List<LogModel> logs)
        {
            try
            {
                using (var conn = db.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string sql = @"INSERT INTO Logs (LogTime, Level, Message, Source, Station, ThreadId, StackTrace) 
                                          VALUES (@logTime, @level, @message, @source, @station, @threadId, @stackTrace)";

                            foreach (var log in logs)
                            {
                                using (var cmd = new SQLiteCommand(sql, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@logTime", log.LogTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                    cmd.Parameters.AddWithValue("@level", log.Level);
                                    cmd.Parameters.AddWithValue("@message", log.Message);
                                    cmd.Parameters.AddWithValue("@source", log.Source ?? "");
                                    cmd.Parameters.AddWithValue("@station", log.Station ?? "");
                                    cmd.Parameters.AddWithValue("@threadId", log.ThreadId);
                                    cmd.Parameters.AddWithValue("@stackTrace", log.StackTrace ?? "");
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 查询化志加载ɸѡ加载化
        /// </summary>
        public List<LogModel> GetLogs(DateTime? startTime = null, DateTime? endTime = null, 
            string level = null, string station = null, int maxRecords = 1000)
        {
            List<string> conditions = new List<string>();
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();

            if (startTime.HasValue)
            {
                // ʹ化 datetime() 加载ȷ加载ȷ夹Ƚ夹
                conditions.Add("datetime(LogTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                // ʹ化 datetime() 加载ȷ加载ȷ夹Ƚ夹
                conditions.Add("datetime(LogTime) <= datetime(@endTime)");
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

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            // ʹ化 datetime(LogTime) 加载加载加载加载化
            string sql = $"SELECT * FROM Logs {whereClause} ORDER BY datetime(LogTime) DESC LIMIT {maxRecords}";

            DataTable dt = db.ExecuteQuery(sql, parameters.ToArray());

            List<LogModel> logs = new List<LogModel>();
            foreach (DataRow row in dt.Rows)
            {
                logs.Add(DataRowToLog(row));
            }
            return logs;
        }

        /// <summary>
        /// 加载化ͳ加载志加载
        /// </summary>
        public Dictionary<string, int> GetLogStatsByLevel(DateTime? startTime = null, DateTime? endTime = null)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            List<string> conditions = new List<string>();

            if (startTime.HasValue)
            {
                conditions.Add("datetime(LogTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                conditions.Add("datetime(LogTime) <= datetime(@endTime)");
                parameters.Add(new SQLiteParameter("@endTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            string sql = $"SELECT Level, COUNT(*) as Count FROM Logs {whereClause} GROUP BY Level";

            DataTable dt = db.ExecuteQuery(sql, parameters.ToArray());

            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                stats[row["Level"].ToString()] = Convert.ToInt32(row["Count"]);
            }
            return stats;
        }

        /// <summary>
        /// ɾ化ָ最后化֮前加载志
        /// </summary>
        public bool DeleteLogsBefore(DateTime cutoffDate)
        {
            try
            {
                string sql = "DELETE FROM Logs WHERE datetime(LogTime) < datetime(@cutoffDate)";
                db.ExecuteNonQuery(sql, new SQLiteParameter("@cutoffDate", cutoffDate.ToString("yyyy-MM-dd HH:mm:ss")));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 化 DataRow ת化为 LogModel
        /// </summary>
        private LogModel DataRowToLog(DataRow row)
        {
            return new LogModel
            {
                Id = Convert.ToInt64(row["Id"]),
                LogTime = DateTime.Parse(row["LogTime"].ToString()),
                Level = row["Level"].ToString(),
                Message = row["Message"].ToString(),
                Source = row["Source"].ToString(),
                Station = row["Station"].ToString(),
                ThreadId = Convert.ToInt32(row["ThreadId"]),
                StackTrace = row["StackTrace"].ToString()
            };
        }
    }
}
