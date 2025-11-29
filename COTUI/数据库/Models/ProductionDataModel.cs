using System;

namespace COTUI.数据库.Models
{
    /// <summary>
    /// 生产数据模型（增强版 - 支持完整追溯）
    /// 
    /// <para><b>38个字段分类：</b></para>
    /// - 基础信息：Station, Operator, ProductionTime
    /// - 产品信息：ProductSN?, ProductModel, MaterialBatchNo, TrayCode
    /// - 检测结果：OverallResult, TestTime
    /// - 缺陷信息：DefectCode, DefectDescription, DefectPosition
    /// - 视觉检测：OriginalImagePath, ResultImagePath
    /// - 测量数据：Dimension_X/Y/Z, Angle, Gap, Thickness, Diameter
    /// - 工艺参数：CycleTime, Temperature, Humidity, Pressure
    /// - 追溯信息：UpstreamSN, DownstreamSN, Rework, ReworkCount
    /// - 其他：Remark, ExtendedData1, ExtendedData2
    /// - 兼容旧字段：ProductInfo, Result, DefectType, ImagePath, BatchNo
    /// </summary>
    public class ProductionDataModel
    {
        #region 基础字段

        /// <summary>
        /// 记录ID（数据库自增）
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public DateTime ProductionTime { get; set; }

        /// <summary>
        /// 工站
        /// </summary>
        public string Station { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

        #endregion

        #region 产品信息（追溯关键）

        /// <summary>
        /// 产品序列号（SN）- 唯一标识
        /// </summary>
        public string ProductSN { get; set; }

        /// <summary>
        /// 产品型号（如：iPhone 15 Pro）
        /// </summary>
        public string ProductModel { get; set; }

        /// <summary>
        /// 物料批次号
        /// </summary>
        public string MaterialBatchNo { get; set; }

        /// <summary>
        /// 托盘码
        /// </summary>
        public string TrayCode { get; set; }

        #endregion

        #region 检测结果

        /// <summary>
        /// 总体检测结果（OK/NG）
        /// </summary>
        public string OverallResult { get; set; }

        /// <summary>
        /// 检测耗时（秒）
        /// </summary>
        public double TestTime { get; set; }

        #endregion

        #region 缺陷信息

        /// <summary>
        /// 缺陷代码（如：DEF001）
        /// </summary>
        public string DefectCode { get; set; }

        /// <summary>
        /// 缺陷描述（如：划痕）
        /// </summary>
        public string DefectDescription { get; set; }

        /// <summary>
        /// 缺陷位置（如：左上角）
        /// </summary>
        public string DefectPosition { get; set; }

        #endregion

        #region 视觉检测

        /// <summary>
        /// 原始图像路径
        /// </summary>
        public string OriginalImagePath { get; set; }

        /// <summary>
        /// 结果图像路径（带标注）
        /// </summary>
        public string ResultImagePath { get; set; }

        #endregion

        #region 测量数据

        /// <summary>
        /// X轴尺寸（mm）
        /// </summary>
        public double? Dimension_X { get; set; }

        /// <summary>
        /// Y轴尺寸（mm）
        /// </summary>
        public double? Dimension_Y { get; set; }

        /// <summary>
        /// Z轴尺寸（mm）
        /// </summary>
        public double? Dimension_Z { get; set; }

        /// <summary>
        /// 角度（度）
        /// </summary>
        public double? Angle { get; set; }

        /// <summary>
        /// 间隙（mm）
        /// </summary>
        public double? Gap { get; set; }

        /// <summary>
        /// 厚度（mm）
        /// </summary>
        public double? Thickness { get; set; }

        /// <summary>
        /// 直径（mm）
        /// </summary>
        public double? Diameter { get; set; }

        #endregion

        #region 工艺参数

        /// <summary>
        /// 周期时间（CT, 秒）
        /// </summary>
        public double? CycleTime { get; set; }

        /// <summary>
        /// 温度（℃）
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// 湿度（%）
        /// </summary>
        public double? Humidity { get; set; }

        /// <summary>
        /// 压力（Pa）
        /// </summary>
        public double? Pressure { get; set; }

        #endregion

        #region 追溯信息

        /// <summary>
        /// 上游工站SN
        /// </summary>
        public string UpstreamSN { get; set; }

        /// <summary>
        /// 下游工站SN
        /// </summary>
        public string DownstreamSN { get; set; }

        /// <summary>
        /// 返工标记（0=首次，1=返工）
        /// </summary>
        public int Rework { get; set; }

        /// <summary>
        /// 返工次数
        /// </summary>
        public int ReworkCount { get; set; }

        #endregion

        #region 其他

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展字段1（可存JSON）
        /// </summary>
        public string ExtendedData1 { get; set; }

        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string ExtendedData2 { get; set; }

        #endregion

        #region 兼容旧字段（保持向后兼容）

        /// <summary>
        /// 产品信息（兼容旧版，现在推荐使用ProductSN）
        /// </summary>
        public string ProductInfo { get; set; }

        /// <summary>
        /// 检测结果（兼容旧版，现在推荐使用OverallResult）
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 缺陷类型（兼容旧版，现在推荐使用DefectCode）
        /// </summary>
        public string DefectType { get; set; }

        /// <summary>
        /// 图像路径（兼容旧版，现在推荐使用OriginalImagePath）
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// 批次号（兼容旧版，现在推荐使用MaterialBatchNo）
        /// </summary>
        public string BatchNo { get; set; }

        #endregion
    }
}
