using System;
using System.Collections.Generic;
using System.Linq;
using COTUI.数据库.Models;
using COTUI.数据库.Services;
using COTUI.通用功能类;

namespace COTUI.扫码管理
{
    /// <summary>
    /// 产品SN管理器（单例模式）
    /// 
    /// <para><b>功能：</b></para>
    /// - SN重复检测
    /// - SN数据保存
    /// - SN数据查询
    /// - 统计分析
    /// </summary>
    public class ProductSNManager
    {
        #region 单例模式

        private static ProductSNManager instance;
        private static readonly object lockObj = new object();

        public static ProductSNManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new ProductSNManager();
                        }
                    }
                }
                return instance;
            }
        }

        private ProductSNManager()
        {
            productionService = new ProductionDataService();
        }

        #endregion

        #region 字段

        private ProductionDataService productionService;

        #endregion

        #region SN化֤

        /// <summary>
        /// 到位SN夹Ƿ化Ѵ到位
        /// </summary>
        public bool IsSNExists(string sn)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sn))
                    return false;

                var product = productionService.GetRecordBySN(sn.Trim().ToUpper());
                return product != null;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "到位SN夹ظ夹失败");
                return false;
            }
        }

        #endregion

        #region SN查询

        /// <summary>
        /// 加载SN查询化Ʒ
        /// </summary>
        public ProductionDataModel GetProductBySN(string sn)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sn))
                    return null;

                return productionService.GetRecordBySN(sn.Trim().ToUpper());
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"查询SN失败: {sn}");
                return null;
            }
        }

        /// <summary>
        /// 加载到位κŲ夹ѯ到位в夹Ʒ
        /// </summary>
        public List<ProductionDataModel> GetProductsByBatchNo(string batchNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(batchNo))
                    return new List<ProductionDataModel>();

                return productionService.GetRecordsByBatchNo(batchNo);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"查询到位κ夹失败: {batchNo}");
                return new List<ProductionDataModel>();
            }
        }

        #endregion

        #region 到位ݱ到位

        /// <summary>
        /// 加载夹Ʒ加载
        /// </summary>
        public bool SaveProductData(ProductionDataModel data)
        {
            try
            {
                if (data == null)
                {
                    Gvar.Logger.Log("化Ʒ加载为化");
                    return false;
                }

                // 化֤化Ҫ字段
                if (string.IsNullOrWhiteSpace(data.ProductSN))
                {
                    Gvar.Logger.Log("化ƷSN为化");
                    return false;
                }

                // 到位SN夹ظ夹
                if (IsSNExists(data.ProductSN))
                {
                    Gvar.Logger.Log($"SN夹Ѵ到位: {data.ProductSN}");
                    return false;
                }

                // 到位浽数据库
                bool success = productionService.AddProductionData(data);

                if (success)
                {
                    Gvar.Logger.Log($"加载夹Ʒ到位ݳɹ夹: {data.ProductSN}");
                }
                else
                {
                    Gvar.Logger.Log($"加载夹Ʒ加载失败: {data.ProductSN}");
                }

                return success;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载夹Ʒ加载失败");
                return false;
            }
        }

        #endregion

        #region SN加载

        /// <summary>
        /// 到位ɲ到位SN
        /// </summary>
        public string GenerateTestSN()
        {
            // 化式化C02YYMMDDHHMMSS
            return $"C02{DateTime.Now:yyMMddHHmmss}";
        }

        #endregion

        #region ͳ夹Ʒ到位

        /// <summary>
        /// 化取到位ղ到位
        /// </summary>
        public int GetTodayOutput()
        {
            try
            {
                var records = productionService.GetRecordsByDateRange(DateTime.Today, DateTime.Now.AddDays(1));
                return records.Count;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "化取到位ղ到位失败");
                return 0;
            }
        }

        /// <summary>
        /// 化取加载加载
        /// </summary>
        public double GetTodayYieldRate()
        {
            try
            {
                var records = productionService.GetRecordsByDateRange(DateTime.Today, DateTime.Now.AddDays(1));
                
                if (records.Count == 0)
                    return 0;

                int okCount = 0;
                foreach (var record in records)
                {
                    if (record.OverallResult == "OK" || record.Result == "OK")
                    {
                        okCount++;
                    }
                }

                return (double)okCount / records.Count * 100;
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "化取加载加载失败");
                return 0;
            }
        }

        /// <summary>
        /// 化取缺陷统计
        /// </summary>
        public Dictionary<string, int> GetDefectStatistics(DateTime startDate, DateTime endDate)
        {
            try
            {
                return productionService.GetDefectTypeStats(startDate, endDate);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "化取缺陷统计失败");
                return new Dictionary<string, int>();
            }
        }

        /// <summary>
        /// 化取加载ļ夹¼
        /// </summary>
        public List<ProductionDataModel> GetRecentRecords(int count = 100)
        {
            try
            {
                return productionService.GetRecentRecords(count);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "化取加载夹¼失败");
                return new List<ProductionDataModel>();
            }
        }

        #endregion
    }
}
