using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using COTUI.数据库.Models;

namespace COTUI.数据库.Services
{
    /// <summary>
    /// 生产数据服务（单例） - 支持字段追踪
    /// </summary>
    public class ProductionDataService
    {
        // 使用全局变量 Gvar.DB 访问数据库

        #region 加载加载

        /// <summary>
        /// 添加生产数据（单条） - 支持事务字段
        /// </summary>
        public bool AddProductionData(ProductionDataModel data)
        {
            try
            {
                string sql = @"INSERT INTO ProductionData (
                    ProductionTime, Station, Operator,
                    ProductSN, ProductModel, MaterialBatchNo, TrayCode,
                    OverallResult, TestTime,
                    DefectCode, DefectDescription, DefectPosition,
                    OriginalImagePath, ResultImagePath,
                    Dimension_X, Dimension_Y, Dimension_Z, Angle, Gap, Thickness, Diameter,
                    CycleTime, Temperature, Humidity, Pressure,
                    UpstreamSN, DownstreamSN, Rework, ReworkCount,
                    Remark, ExtendedData1, ExtendedData2,
                    ProductInfo, Result, DefectType, ImagePath, BatchNo
                ) VALUES (
                    @productionTime, @station, @operator,
                    @productSN, @productModel, @materialBatchNo, @trayCode,
                    @overallResult, @testTime,
                    @defectCode, @defectDescription, @defectPosition,
                    @originalImagePath, @resultImagePath,
                    @dimensionX, @dimensionY, @dimensionZ, @angle, @gap, @thickness, @diameter,
                    @cycleTime, @temperature, @humidity, @pressure,
                    @upstreamSN, @downstreamSN, @rework, @reworkCount,
                    @remark, @extendedData1, @extendedData2,
                    @productInfo, @result, @defectType, @imagePath, @batchNo
                )";

                Gvar.DB.ExecuteNonQuery(sql,
                    new SQLiteParameter("@productionTime", data.ProductionTime.ToString("yyyy-MM-dd HH:mm:ss.fff")),
                    new SQLiteParameter("@station", data.Station ?? ""),
                    new SQLiteParameter("@operator", data.Operator ?? ""),
                    
                    // 化Ʒ化Ϣ
                    new SQLiteParameter("@productSN", data.ProductSN ?? ""),
                    new SQLiteParameter("@productModel", data.ProductModel ?? ""),
                    new SQLiteParameter("@materialBatchNo", data.MaterialBatchNo ?? ""),
                    new SQLiteParameter("@trayCode", data.TrayCode ?? ""),
                    
                    // 加载夹
                    new SQLiteParameter("@overallResult", data.OverallResult ?? ""),
                    new SQLiteParameter("@testTime", data.TestTime),
                    
                    // ȱ加载Ϣ
                    new SQLiteParameter("@defectCode", data.DefectCode ?? ""),
                    new SQLiteParameter("@defectDescription", data.DefectDescription ?? ""),
                    new SQLiteParameter("@defectPosition", data.DefectPosition ?? ""),
                    
                    // 夹Ӿ加载
                    new SQLiteParameter("@originalImagePath", data.OriginalImagePath ?? ""),
                    new SQLiteParameter("@resultImagePath", data.ResultImagePath ?? ""),
                    
                    // 加载加载
                    new SQLiteParameter("@dimensionX", (object)data.Dimension_X ?? DBNull.Value),
                    new SQLiteParameter("@dimensionY", (object)data.Dimension_Y ?? DBNull.Value),
                    new SQLiteParameter("@dimensionZ", (object)data.Dimension_Z ?? DBNull.Value),
                    new SQLiteParameter("@angle", (object)data.Angle ?? DBNull.Value),
                    new SQLiteParameter("@gap", (object)data.Gap ?? DBNull.Value),
                    new SQLiteParameter("@thickness", (object)data.Thickness ?? DBNull.Value),
                    new SQLiteParameter("@diameter", (object)data.Diameter ?? DBNull.Value),
                    
                    // 到位ղ到位
                    new SQLiteParameter("@cycleTime", (object)data.CycleTime ?? DBNull.Value),
                    new SQLiteParameter("@temperature", (object)data.Temperature ?? DBNull.Value),
                    new SQLiteParameter("@humidity", (object)data.Humidity ?? DBNull.Value),
                    new SQLiteParameter("@pressure", (object)data.Pressure ?? DBNull.Value),
                    
                    // ׷加载Ϣ
                    new SQLiteParameter("@upstreamSN", data.UpstreamSN ?? ""),
                    new SQLiteParameter("@downstreamSN", data.DownstreamSN ?? ""),
                    new SQLiteParameter("@rework", data.Rework),
                    new SQLiteParameter("@reworkCount", data.ReworkCount),
                    
                    // 加载
                    new SQLiteParameter("@remark", data.Remark ?? ""),
                    new SQLiteParameter("@extendedData1", data.ExtendedData1 ?? ""),
                    new SQLiteParameter("@extendedData2", data.ExtendedData2 ?? ""),
                    
                    // 到位ݾ夹字段
                    new SQLiteParameter("@productInfo", data.ProductSN ?? data.ProductInfo ?? ""),
                    new SQLiteParameter("@result", data.OverallResult ?? ""),
                    new SQLiteParameter("@defectType", data.DefectCode ?? ""),
                    new SQLiteParameter("@imagePath", data.OriginalImagePath ?? ""),
                    new SQLiteParameter("@batchNo", data.MaterialBatchNo ?? ""));

                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"[生产数据] 加载数据失败: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region SN查询

        /// <summary>
        /// 加载SN查询化Ʒ化¼
        /// </summary>
        public ProductionDataModel GetRecordBySN(string sn)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sn))
                    return null;

                string sql = "SELECT * FROM ProductionData WHERE ProductSN = @sn LIMIT 1";
                
                DataTable dt = Gvar.DB.ExecuteQuery(sql, new SQLiteParameter("@sn", sn.Trim().ToUpper()));

                if (dt.Rows.Count > 0)
                {
                    return DataRowToProductionData(dt.Rows[0]);
                }

                return null;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"[生产数据] 查询SN失败: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 加载到位κŲ夹ѯ到位в夹Ʒ
        /// </summary>
        public List<ProductionDataModel> GetRecordsByBatchNo(string batchNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(batchNo))
                    return new List<ProductionDataModel>();

                string sql = "SELECT * FROM ProductionData WHERE MaterialBatchNo = @batchNo ORDER BY ProductionTime DESC";
                
                DataTable dt = Gvar.DB.ExecuteQuery(sql, new SQLiteParameter("@batchNo", batchNo));

                List<ProductionDataModel> records = new List<ProductionDataModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add(DataRowToProductionData(row));
                }

                return records;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"[生产数据] 查询最后记录失败: {ex.Message}");
                return new List<ProductionDataModel>();
            }
        }

        /// <summary>
        /// 化取加载ļ夹¼
        /// </summary>
        public List<ProductionDataModel> GetRecentRecords(int count = 100)
        {
            try
            {
                string sql = $"SELECT * FROM ProductionData ORDER BY ProductionTime DESC LIMIT {count}";
                
                DataTable dt = Gvar.DB.ExecuteQuery(sql);

                List<ProductionDataModel> records = new List<ProductionDataModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add(DataRowToProductionData(row));
                }

                return records;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"[生产数据] 获取最后记录失败: {ex.Message}");
                return new List<ProductionDataModel>();
            }
        }

        /// <summary>
        /// 加载一批时夹䷶Χ查询
        /// </summary>
        public List<ProductionDataModel> GetRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                string sql = @"SELECT * FROM ProductionData 
                              WHERE datetime(ProductionTime) >= datetime(@startDate) 
                              AND datetime(ProductionTime) < datetime(@endDate)
                              ORDER BY ProductionTime DESC";
                
                DataTable dt = Gvar.DB.ExecuteQuery(sql,
                    new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SQLiteParameter("@endDate", endDate.ToString("yyyy-MM-dd HH:mm:ss")));

                List<ProductionDataModel> records = new List<ProductionDataModel>();
                foreach (DataRow row in dt.Rows)
                {
                    records.Add(DataRowToProductionData(row));
                }

                return records;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Error(LogLevel.Error, $"[生产数据] 查询批次数据失败: {ex.Message}");
                return new List<ProductionDataModel>();
            }
        }

        #endregion

        #region ԭ夹в夹ѯ加载加载夹ݣ夹

        /// <summary>
        /// 查询加载到位ݣ夹ԭ夹з加载加载ּ化ݣ夹
        /// </summary>
        public List<ProductionDataModel> GetProductionData(DateTime? startTime = null, DateTime? endTime = null,
            string station = null, string result = null, string batchNo = null, int maxRecords = 1000)
        {
            List<string> conditions = new List<string>();
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();

            if (startTime.HasValue)
            {
                conditions.Add("datetime(ProductionTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                conditions.Add("datetime(ProductionTime) <= datetime(@endTime)");
                parameters.Add(new SQLiteParameter("@endTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (!string.IsNullOrEmpty(station) && station != "ȫ加载վ")
            {
                conditions.Add("Station = @station");
                parameters.Add(new SQLiteParameter("@station", station));
            }

            if (!string.IsNullOrEmpty(result))
            {
                conditions.Add("(Result = @result OR OverallResult = @result)");
                parameters.Add(new SQLiteParameter("@result", result));
            }

            if (!string.IsNullOrEmpty(batchNo))
            {
                conditions.Add("(BatchNo = @batchNo OR MaterialBatchNo = @batchNo)");
                parameters.Add(new SQLiteParameter("@batchNo", batchNo));
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            string sql = $"SELECT * FROM ProductionData {whereClause} ORDER BY datetime(ProductionTime) DESC LIMIT {maxRecords}";

            DataTable dt = Gvar.DB.ExecuteQuery(sql, parameters.ToArray());

            List<ProductionDataModel> dataList = new List<ProductionDataModel>();
            foreach (DataRow row in dt.Rows)
            {
                dataList.Add(DataRowToProductionData(row));
            }
            return dataList;
        }

        /// <summary>
        /// 化取加载ͳ夹ƣ加载化ڣ夹
        /// </summary>
        public Dictionary<string, ProductionStats> GetDailyProductionStats(DateTime startDate, DateTime endDate)
        {
            string sql = @"SELECT DATE(ProductionTime) as ProductionDate, 
                          COUNT(*) as TotalCount,
                          SUM(CASE WHEN Result = 'OK' OR OverallResult = 'OK' THEN 1 ELSE 0 END) as OKCount,
                          SUM(CASE WHEN Result = 'NG' OR OverallResult = 'NG' THEN 1 ELSE 0 END) as NGCount,
                          AVG(TestTime) as AvgTestTime
                          FROM ProductionData 
                          WHERE datetime(ProductionTime) >= datetime(@startDate) AND datetime(ProductionTime) <= datetime(@endDate) 
                          GROUP BY DATE(ProductionTime) 
                          ORDER BY ProductionDate DESC";

            DataTable dt = Gvar.DB.ExecuteQuery(sql,
                new SQLiteParameter("@startDate", startDate.ToString("yyyy-MM-dd")),
                new SQLiteParameter("@endDate", endDate.AddDays(1).ToString("yyyy-MM-dd")));

            Dictionary<string, ProductionStats> stats = new Dictionary<string, ProductionStats>();
            foreach (DataRow row in dt.Rows)
            {
                string date = row["ProductionDate"].ToString();
                stats[date] = new ProductionStats
                {
                    TotalCount = Convert.ToInt32(row["TotalCount"]),
                    OKCount = Convert.ToInt32(row["OKCount"]),
                    NGCount = Convert.ToInt32(row["NGCount"]),
                    AvgTestTime = row["AvgTestTime"] != DBNull.Value ? Convert.ToDouble(row["AvgTestTime"]) : 0
                };
            }
            return stats;
        }

        /// <summary>
        /// 化取ȱ加载化统计
        /// </summary>
        public Dictionary<string, int> GetDefectTypeStats(DateTime? startTime = null, DateTime? endTime = null)
        {
            List<SQLiteParameter> parameters = new List<SQLiteParameter>();
            List<string> conditions = new List<string> { 
                "(Result = 'NG' OR OverallResult = 'NG')", 
                "(DefectType IS NOT NULL AND DefectType != '' OR DefectCode IS NOT NULL AND DefectCode != '')" 
            };

            if (startTime.HasValue)
            {
                conditions.Add("datetime(ProductionTime) >= datetime(@startTime)");
                parameters.Add(new SQLiteParameter("@startTime", startTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            if (endTime.HasValue)
            {
                conditions.Add("datetime(ProductionTime) <= datetime(@endTime)");
                parameters.Add(new SQLiteParameter("@endTime", endTime.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            }

            string whereClause = "WHERE " + string.Join(" AND ", conditions);
            string sql = $@"SELECT COALESCE(DefectCode, DefectType) as DefectType, COUNT(*) as Count 
                           FROM ProductionData {whereClause} 
                           GROUP BY COALESCE(DefectCode, DefectType) 
                           ORDER BY Count DESC";

            DataTable dt = Gvar.DB.ExecuteQuery(sql, parameters.ToArray());

            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                stats[row["DefectType"].ToString()] = Convert.ToInt32(row["Count"]);
            }
            return stats;
        }

        #endregion

        #region 加载ת化

        /// <summary>
        /// 化 DataRow ת化为 ProductionDataModel加载ǿ夹棩
        /// </summary>
        private ProductionDataModel DataRowToProductionData(DataRow row)
        {
            return new ProductionDataModel
            {
                Id = Convert.ToInt64(row["Id"]),
                ProductionTime = DateTime.Parse(row["ProductionTime"].ToString()),
                Station = GetStringValue(row, "Station"),
                Operator = GetStringValue(row, "Operator"),
                
                // 化Ʒ化Ϣ
                ProductSN = GetStringValue(row, "ProductSN"),
                ProductModel = GetStringValue(row, "ProductModel"),
                MaterialBatchNo = GetStringValue(row, "MaterialBatchNo"),
                TrayCode = GetStringValue(row, "TrayCode"),
                
                // 加载夹
                OverallResult = GetStringValue(row, "OverallResult"),
                TestTime = GetDoubleValue(row, "TestTime"),
                
                // ȱ加载Ϣ
                DefectCode = GetStringValue(row, "DefectCode"),
                DefectDescription = GetStringValue(row, "DefectDescription"),
                DefectPosition = GetStringValue(row, "DefectPosition"),
                
                // 夹Ӿ加载
                OriginalImagePath = GetStringValue(row, "OriginalImagePath"),
                ResultImagePath = GetStringValue(row, "ResultImagePath"),
                
                // 加载加载
                Dimension_X = GetNullableDouble(row, "Dimension_X"),
                Dimension_Y = GetNullableDouble(row, "Dimension_Y"),
                Dimension_Z = GetNullableDouble(row, "Dimension_Z"),
                Angle = GetNullableDouble(row, "Angle"),
                Gap = GetNullableDouble(row, "Gap"),
                Thickness = GetNullableDouble(row, "Thickness"),
                Diameter = GetNullableDouble(row, "Diameter"),
                
                // 到位ղ到位
                CycleTime = GetNullableDouble(row, "CycleTime"),
                Temperature = GetNullableDouble(row, "Temperature"),
                Humidity = GetNullableDouble(row, "Humidity"),
                Pressure = GetNullableDouble(row, "Pressure"),
                
                // ׷加载Ϣ
                UpstreamSN = GetStringValue(row, "UpstreamSN"),
                DownstreamSN = GetStringValue(row, "DownstreamSN"),
                Rework = GetIntValue(row, "Rework"),
                ReworkCount = GetIntValue(row, "ReworkCount"),
                
                // 加载
                Remark = GetStringValue(row, "Remark"),
                ExtendedData1 = GetStringValue(row, "ExtendedData1"),
                ExtendedData2 = GetStringValue(row, "ExtendedData2")
            };
        }

        private string GetStringValue(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value 
                ? row[columnName].ToString() 
                : string.Empty;
        }

        private double GetDoubleValue(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value 
                ? Convert.ToDouble(row[columnName]) 
                : 0;
        }

        private double? GetNullableDouble(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value 
                ? Convert.ToDouble(row[columnName]) 
                : (double?)null;
        }

        private int GetIntValue(DataRow row, string columnName)
        {
            return row.Table.Columns.Contains(columnName) && row[columnName] != DBNull.Value 
                ? Convert.ToInt32(row[columnName]) 
                : 0;
        }

        #endregion
    }

    /// <summary>
    /// 加载ͳ加载加载
    /// </summary>
    public class ProductionStats
    {
        public int TotalCount { get; set; }
        public int OKCount { get; set; }
        public int NGCount { get; set; }
        public double AvgTestTime { get; set; }
        public double YieldRate => TotalCount > 0 ? (double)OKCount / TotalCount * 100 : 0;
    }
}
