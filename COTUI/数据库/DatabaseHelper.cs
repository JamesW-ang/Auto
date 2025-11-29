using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using COTUI.数据库.Models;
using System.Collections.Generic;

namespace COTUI.数据库
{
    /// <summary>
    /// SQLite 数据库辅助类
    /// </summary>
    public class DatabaseHelper
    {
        private static DatabaseHelper instance;
        private static readonly object locker = new object();
        private readonly string connectionString;
        private readonly string databasePath;

        /// <summary>
        /// 获取数据库辅助类单例实例
        /// </summary>
        public static DatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new DatabaseHelper();
                        }
                    }
                }
                return instance;
            }
        }

        private DatabaseHelper()
        {
            try
            {
                // 数据库文件路径（放在同一目录下的 Data 文件夹）
                string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (!Directory.Exists(dataDirectory))
                {
                    Directory.CreateDirectory(dataDirectory);
                }

                databasePath = Path.Combine(dataDirectory, "COTUI.db");
                connectionString = $"Data Source={databasePath};Version=3;";

                Console.WriteLine($"[DatabaseHelper] 数据库路径: {databasePath}");
                Console.WriteLine($"[DatabaseHelper] 文件存在: {File.Exists(databasePath)}");

                // 确保表结构存在
                EnsureTablesExist();
                
                Console.WriteLine("[DatabaseHelper] 数据库初始化成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DatabaseHelper] 初始化失败: {ex.Message}");
                Console.WriteLine($"[DatabaseHelper] 堆栈: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        /// <summary>
        /// 确保所有表结构存在
        /// </summary>
        private void EnsureTablesExist()
        {
            try
            {
                // 如果数据库文件不存在，先创建
                if (!File.Exists(databasePath))
                {
                    Console.WriteLine("[DatabaseHelper] 正在创建数据库文件");
                    SQLiteConnection.CreateFile(databasePath);
                }

                using (var conn = GetConnection())
                {
                    conn.Open();
                    Console.WriteLine("[DatabaseHelper] 数据库连接成功");

                    // 创建所有表
                    CreateTables(conn);
                    
                    // 插入默认配置
                    InsertDefaultUsers(conn);
                    InsertDefaultConfig(conn);
                    
                    Console.WriteLine("[DatabaseHelper] 表结构验证完成");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DatabaseHelper] 确保表结构失败: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 创建所有表
        /// </summary>
        private void CreateTables(SQLiteConnection conn)
        {
            // 创建用户表
            string createUsersTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL UNIQUE,
                Password TEXT NOT NULL,
                RealName TEXT,
                Role TEXT NOT NULL DEFAULT 'user',
                IsEnabled INTEGER NOT NULL DEFAULT 1,
                CreateTime TEXT NOT NULL,
                LastLoginTime TEXT,
                Remark TEXT
            );";

            // 创建日志表
            string createLogsTable = @"
            CREATE TABLE IF NOT EXISTS Logs (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                LogTime TEXT NOT NULL,
                Level TEXT NOT NULL,
                Message TEXT NOT NULL,
                Source TEXT,
                Station TEXT,
                ThreadId INTEGER,
                StackTrace TEXT
            );
            CREATE INDEX IF NOT EXISTS idx_logs_time ON Logs(LogTime);
            CREATE INDEX IF NOT EXISTS idx_logs_level ON Logs(Level);
            CREATE INDEX IF NOT EXISTS idx_logs_station ON Logs(Station);";

            // 创建告警表
            string createAlarmsTable = @"
            CREATE TABLE IF NOT EXISTS Alarms (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                AlarmTime TEXT NOT NULL,
                Level TEXT NOT NULL,
                Content TEXT NOT NULL,
                Station TEXT,
                AlarmCode TEXT,
                IsHandled INTEGER NOT NULL DEFAULT 0,
                HandleTime TEXT,
                HandleUser TEXT,
                HandleRemark TEXT
            );
            CREATE INDEX IF NOT EXISTS idx_alarms_time ON Alarms(AlarmTime);
            CREATE INDEX IF NOT EXISTS idx_alarms_level ON Alarms(Level);
            CREATE INDEX IF NOT EXISTS idx_alarms_code ON Alarms(AlarmCode);";

            // 创建生产数据表
            string createProductionTable = @"
            CREATE TABLE IF NOT EXISTS ProductionData (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ProductionTime TEXT NOT NULL,
                Station TEXT,
                ProductInfo TEXT,
                TestTime REAL,
                Result TEXT,
                DefectType TEXT,
                ImagePath TEXT,
                BatchNo TEXT,
                Operator TEXT,
                Remark TEXT
            );
            CREATE INDEX IF NOT EXISTS idx_production_time ON ProductionData(ProductionTime);
            CREATE INDEX IF NOT EXISTS idx_production_result ON ProductionData(Result);
            CREATE INDEX IF NOT EXISTS idx_production_batch ON ProductionData(BatchNo);";

            // 创建系统配置表
            string createConfigTable = @"
            CREATE TABLE IF NOT EXISTS SystemConfig (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ConfigGroup TEXT NOT NULL,
                ConfigKey TEXT NOT NULL,
                ConfigValue TEXT,
                Description TEXT,
                UpdateTime TEXT NOT NULL,
                UpdateUser TEXT,
                UNIQUE(ConfigGroup, ConfigKey)
            );";

            Console.WriteLine("[DatabaseHelper] 初始化表结构...");
            ExecuteNonQuery(conn, createUsersTable);
            Console.WriteLine("[DatabaseHelper] Users 表已创建");
            
            ExecuteNonQuery(conn, createLogsTable);
            Console.WriteLine("[DatabaseHelper] Logs 表已创建");
            
            ExecuteNonQuery(conn, createAlarmsTable);
            Console.WriteLine("[DatabaseHelper] Alarms 表已创建");
            
            ExecuteNonQuery(conn, createProductionTable);
            Console.WriteLine("[DatabaseHelper] ProductionData 表已创建");
            
            ExecuteNonQuery(conn, createConfigTable);
            Console.WriteLine("[DatabaseHelper] SystemConfig 表已创建");
        }

        /// <summary>
        /// 插入默认用户
        /// </summary>
        private void InsertDefaultUsers(SQLiteConnection conn)
        {
            try
            {
                string insertUsers = @"
                INSERT OR IGNORE INTO Users (Username, Password, RealName, Role, IsEnabled, CreateTime, Remark) 
                VALUES 
                    ('admin', '123', '系统管理员', 'admin', 1, @time, '默认管理员账户'),
                    ('user1', '456', '操作员1', 'user', 1, @time, '默认操作员账户'),
                    ('user2', '789', '操作员2', 'user', 1, @time, '默认操作员账户');";

                using (var cmd = new SQLiteCommand(insertUsers, conn))
                {
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int affected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"[DatabaseHelper] 插入默认用户: {affected} 条");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DatabaseHelper] 插入默认用户ʧ条: {ex.Message}");
            }
        }

        /// <summary>
        /// 插入默认配置
        /// </summary>
        private void InsertDefaultConfig(SQLiteConnection conn)
        {
            try
            {
                string insertConfig = @"
                INSERT OR IGNORE INTO SystemConfig (ConfigGroup, ConfigKey, ConfigValue, Description, UpdateTime, UpdateUser) 
                VALUES 
                    ('System', 'LogRetentionDays', '30', '条־条条条条', @time, 'system'),
                    ('System', 'MaxLogsPerQuery', '1000', '单次查询最大日志数', @time, 'system'),
                    ('System', 'AutoBackupEnabled', 'true', '是否启用自动备份', @time, 'system'),
                    ('Vision', 'ImageSavePath', 'Images', '图像保存路径', @time, 'system'),
                    ('MQTT', 'ServerAddress', 'localhost', 'MQTT条条条条ַ', @time, 'system');";

                using (var cmd = new SQLiteCommand(insertConfig, conn))
                {
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int affected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"[DatabaseHelper] 插入默认配置: {affected} 条");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DatabaseHelper] 插入默认配置ʧ条: {ex.Message}");
            }
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        private void ExecuteNonQuery(SQLiteConnection conn, string sql)
        {
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行非查询SQL语句（外部调用）
        /// </summary>
        public int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回单个值
        /// </summary>
        public object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行查询并返回 DataTable
        /// </summary>
        public DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 清理旧的日志数据（保留指定的保留天数）
        /// </summary>
        public void CleanupOldLogs()
        {
            try
            {
                // 条ȡ条־创建告警表条
                string sql = "SELECT ConfigValue FROM SystemConfig WHERE ConfigGroup = 'System' AND ConfigKey = 'LogRetentionDays'";
                object result = ExecuteScalar(sql);
                int retentionDays = result != null ? int.Parse(result.ToString()) : 30;

                // ɾ条条条条־
                DateTime cutoffDate = DateTime.Now.AddDays(-retentionDays);
                string deleteSql = "DELETE FROM Logs WHERE LogTime < @cutoffDate";
                ExecuteNonQuery(deleteSql, new SQLiteParameter("@cutoffDate", cutoffDate.ToString("yyyy-MM-dd HH:mm:ss")));

                // 删除早于保留期限的数据
                string deleteAlarmsSql = "DELETE FROM Alarms WHERE IsHandled = 1 AND AlarmTime < @cutoffDate";
                ExecuteNonQuery(deleteAlarmsSql, new SQLiteParameter("@cutoffDate", cutoffDate.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建告警表־ʧ条: {ex.Message}");
            }
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        public bool BackupDatabase(string backupPath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(backupPath))
                {
                    string backupDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backup");
                    if (!Directory.Exists(backupDirectory))
                    {
                        Directory.CreateDirectory(backupDirectory);
                    }
                    backupPath = Path.Combine(backupDirectory, $"COTUI_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");
                }

                File.Copy(databasePath, backupPath, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"备份数据库ʧ条: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 优化数据库（压缩和重建索引）
        /// </summary>
        public void OptimizeDatabase()
        {
            try
            {
                ExecuteNonQuery("VACUUM");
                ExecuteNonQuery("ANALYZE");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"优化数据库失败: {ex.Message}");
            }
        }
    }
}
