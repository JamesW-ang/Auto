using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrightIdeasSoftware;  // ✅ 添加 ObjectListView 引用
using COTUI.通用功能类;
using COTUI.数据库.Services;
using COTUI.数据库.Models;

namespace COTUI.统计分析
{
    /// <summary>
    /// 产品追溯查询页面（Form版本）
    /// 
    /// <para><b>功能：</b></para>
    /// - 根据产品SN查询完整生产历史
    /// - 显示测量数据、缺陷信息、操作记录
    /// - 显示上下游追溯关系
    /// - 导出追溯报告
    /// </summary>
    public partial class TraceabilityPage : Form
    {
        #region 字段

        private ProductionDataService productionService = new ProductionDataService();
        
        private ProductionDataModel currentRecord = null;

        #endregion

        #region 构造函数

        public TraceabilityPage()
        {
            InitializeComponent();
            InitializeObjectListView();  // ✅ 初始化 ObjectListView
            Gvar.Logger.Info("产品追溯查询页面创建");
        }

        #endregion

        #region 页面加载

        private void TraceabilityPage_Load(object sender, EventArgs e)
        {
            try
            {
                txtSN.Focus();
                Gvar.Logger.Info("产品追溯查询页面加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "产品追溯查询页面加载失败");
                MessageBox.Show($"页面加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ObjectListView 初始化

        /// <summary>
        /// ✅ 初始化 ObjectListView
        /// </summary>
        private void InitializeObjectListView()
        {
            // ✅ 自定义行格式 (根据结果设置颜色)
            olvRecords.RowFormatter = delegate(OLVListItem item)
            {
                if (item.RowObject is ProductionDataModel record)
                {
                    if (record.OverallResult == "OK")
                    {
                        item.BackColor = Color.FromArgb(200, 230, 201);  // 绿色
                    }
                    else if (record.OverallResult == "NG")
                    {
                        item.BackColor = Color.FromArgb(255, 205, 210);  // 红色
                    }
                }
            };
        }

        #endregion

        #region 查询功能

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                string sn = txtSN.Text.Trim().ToUpper();

                if (string.IsNullOrEmpty(sn))
                {
                    MessageBox.Show("请输入产品SN", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSN.Focus();
                    return;
                }

                var record = productionService.GetRecordBySN(sn);

                if (record == null)
                {
                    MessageBox.Show($"未找到SN: {sn} 的记录", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearDisplay();
                    return;
                }

                currentRecord = record;
                DisplayRecord(record);

                Gvar.Logger.Info($"查询产品追溯: {sn}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "查询失败");
                MessageBox.Show($"查询失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ 使用 ObjectListView 显示记录
        /// </summary>
        private void DisplayRecord(ProductionDataModel record)
        {
            // ✅ 一行代码绑定数据!
            olvRecords.SetObjects(new[] { record });
            
            ShowDetails(record);
        }

        private void ShowDetails(ProductionDataModel record)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("=== 基本信息 ===");
            sb.AppendLine($"产品SN:        {record.ProductSN ?? "--"}");
            sb.AppendLine($"产品型号:      {record.ProductModel ?? "--"}");
            sb.AppendLine($"批次号:        {record.MaterialBatchNo ?? "--"}");
            sb.AppendLine($"工站:          {record.Station ?? "--"}");
            sb.AppendLine($"操作员:        {record.Operator ?? "--"}");
            sb.AppendLine($"生产时间:      {record.ProductionTime:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();
            
            sb.AppendLine("=== 检测结果 ===");
            sb.AppendLine($"总体结果:      {record.OverallResult ?? "--"}");
            sb.AppendLine($"周期时间:      {(record.CycleTime.HasValue ? $"{record.CycleTime.Value:F2}s" : "--")}");
            sb.AppendLine();
            
            sb.AppendLine("=== 测量数据 ===");
            if (record.Dimension_X.HasValue)
                sb.AppendLine($"X尺寸:         {record.Dimension_X.Value:F3} mm");
            if (record.Dimension_Y.HasValue)
                sb.AppendLine($"Y尺寸:         {record.Dimension_Y.Value:F3} mm");
            if (record.Dimension_Z.HasValue)
                sb.AppendLine($"Z尺寸:         {record.Dimension_Z.Value:F3} mm");
            if (record.Angle.HasValue)
                sb.AppendLine($"角度:          {record.Angle.Value:F2} °");
            if (record.Gap.HasValue)
                sb.AppendLine($"间隙:          {record.Gap.Value:F3} mm");
            if (record.Thickness.HasValue)
                sb.AppendLine($"厚度:          {record.Thickness.Value:F3} mm");
            if (record.Diameter.HasValue)
                sb.AppendLine($"直径:          {record.Diameter.Value:F3} mm");
            sb.AppendLine();
            
            sb.AppendLine("=== 环境数据 ===");
            if (record.Temperature.HasValue)
                sb.AppendLine($"温度:          {record.Temperature.Value:F1} °C");
            if (record.Humidity.HasValue)
                sb.AppendLine($"湿度:          {record.Humidity.Value:F1} %");
            if (record.Pressure.HasValue)
                sb.AppendLine($"压力:          {record.Pressure.Value:F2} Pa");
            sb.AppendLine();
            
            sb.AppendLine("=== 缺陷信息 ===");
            sb.AppendLine($"缺陷代码:      {record.DefectCode ?? "无"}");
            sb.AppendLine($"缺陷描述:      {record.DefectDescription ?? "无"}");
            
            lblDetailInfo.Text = sb.ToString();
        }

        private void ClearDisplay()
        {
            olvRecords.ClearObjects();  // ✅ 改为 olvRecords
            lblDetailInfo.Text = "选择一条记录查看详细信息...";
            currentRecord = null;
        }

        #endregion

        #region 表格选择变化

        /// <summary>
        /// ✅ ObjectListView 选择变化事件
        /// </summary>
        private void olvRecords_SelectionChanged(object sender, EventArgs e)
        {
            if (olvRecords.SelectedObject is ProductionDataModel record)
            {
                currentRecord = record;
                ShowDetails(record);
            }
        }

        #endregion

        #region 生成测试数据

        private void btnGenerateTestData_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "生成测试数据用于界面演示\n\n" +
                    "将生成10条随机测试数据，包括：\n" +
                    "• 产品SN（唯一）\n" +
                    "• 测量数据（X/Y/Z尺寸、角度等）\n" +
                    "• 检测结果（OK/NG，约30%不良率）\n" +
                    "• 环境数据（温度、湿度）\n" +
                    "• 缺陷信息（NG时随机生成）\n\n" +
                    "注意：数据仅在内存中显示，不保存到数据库\n\n" +
                    "是否继续？",
                    "生成测试数据",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    GenerateTestData();
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "生成测试数据失败");
                MessageBox.Show($"生成失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateTestData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Gvar.Logger.Info("开始生成测试数据...");

                Random rand = new Random();
                List<ProductionDataModel> testDataList = new List<ProductionDataModel>();
                
                string[] productModels = { "Model-A", "Model-B", "Model-C", "Model-X", "Model-Y" };
                string[] operators = { "张三", "李四", "王五", "赵六", "孙七" };
                string[] stations = { "工站1", "工站2", "工站3", "测试工站", "终检工站" };
                string[] defectCodes = { "DIM_X_OUT", "DIM_Y_OUT", "DIM_Z_OUT", "ANGLE_ERR", "GAP_ERR", "SURFACE_DEFECT" };
                string[] defectDescs = { "X尺寸超差", "Y尺寸超差", "Z尺寸超差", "角度偏差", "间隙不良", "表面缺陷" };

                // 生成10条测试数据
                for (int i = 0; i < 10; i++)
                {
                    double baseX = 50.0;
                    double baseY = 30.0;
                    double baseZ = 20.0;
                    double baseAngle = 90.0;
                    double baseGap = 0.5;
                    
                    double stdDev = 0.01;
                    
                    double dimX = baseX + GenerateGaussianRandom(rand) * stdDev;
                    double dimY = baseY + GenerateGaussianRandom(rand) * stdDev;
                    double dimZ = baseZ + GenerateGaussianRandom(rand) * stdDev;
                    double angle = baseAngle + GenerateGaussianRandom(rand) * 0.1;
                    double gap = baseGap + GenerateGaussianRandom(rand) * 0.005;
                    double thickness = 2.0 + GenerateGaussianRandom(rand) * 0.002;
                    double diameter = 10.0 + GenerateGaussianRandom(rand) * 0.005;
                    
                    // 30%不良率
                    bool isNG = rand.Next(0, 100) < 30;
                    
                    int defectIndex = 0;
                    if (isNG)
                    {
                        defectIndex = rand.Next(0, 6);
                        switch (defectIndex)
                        {
                            case 0: dimX += rand.NextDouble() * 0.1; break;
                            case 1: dimY += rand.NextDouble() * 0.1; break;
                            case 2: dimZ += rand.NextDouble() * 0.1; break;
                            case 3: angle += rand.NextDouble() * 5; break;
                            case 4: gap += rand.NextDouble() * 0.05; break;
                            case 5: thickness += rand.NextDouble() * 0.02; break;
                        }
                    }
                    
                    string sn = $"TEST{DateTime.Now:yyMMddHHmmss}{i:D3}";
                    
                    var testData = new ProductionDataModel
                    {
                        ProductSN = sn,
                        ProductModel = productModels[rand.Next(productModels.Length)],
                        MaterialBatchNo = $"BATCH{DateTime.Now:yyyyMMdd}001",
                        Station = stations[rand.Next(stations.Length)],
                        Operator = operators[rand.Next(operators.Length)],
                        ProductionTime = DateTime.Now.AddMinutes(-rand.Next(0, 1440)), // 最近24小时
                        
                        OverallResult = isNG ? "NG" : "OK",
                        Result = isNG ? "NG" : "OK",
                        TestTime = 120 + rand.NextDouble() * 60,
                        CycleTime = 120 + rand.NextDouble() * 60,
                        
                        Dimension_X = dimX,
                        Dimension_Y = dimY,
                        Dimension_Z = dimZ,
                        Angle = angle,
                        Gap = gap,
                        Thickness = thickness,
                        Diameter = diameter,
                        
                        Temperature = 20 + rand.NextDouble() * 5,
                        Humidity = 40 + rand.NextDouble() * 20,
                        Pressure = 100000 + rand.NextDouble() * 1000,
                        
                        DefectCode = isNG ? defectCodes[defectIndex] : null,
                        DefectDescription = isNG ? defectDescs[defectIndex] : null,
                        DefectType = isNG ? defectCodes[defectIndex] : null,
                        
                        Remark = $"测试数据{i + 1}",
                        ProductInfo = sn,
                        BatchNo = $"BATCH{DateTime.Now:yyyyMMdd}001"
                    };
                    
                    testDataList.Add(testData);
                }
                
                // 显示生成的测试数据
                olvRecords.SetObjects(testDataList);
                
                // 自动选择第一条
                if (testDataList.Count > 0)
                {
                    olvRecords.SelectedObject = testDataList[0];
                    currentRecord = testDataList[0];
                    ShowDetails(testDataList[0]);
                }
                
                Cursor = Cursors.Default;
                
                int okCount = testDataList.Count(d => d.OverallResult == "OK");
                int ngCount = testDataList.Count(d => d.OverallResult == "NG");
                
                Gvar.Logger.Log($"测试数据生成完成: 共{testDataList.Count}条, OK:{okCount}条, NG:{ngCount}条");
                
                MessageBox.Show(
                    $"✅ 测试数据生成完成！\n\n" +
                    $"共生成：{testDataList.Count} 条\n" +
                    $"OK：{okCount} 条\n" +
                    $"NG：{ngCount} 条\n\n" +
                    $"数据已显示在列表中，可以:\n" +
                    $"• 点击查看详细信息\n" +
                    $"• 查看OK/NG颜色标识\n" +
                    $"• 测试界面显示效果\n\n" +
                    $"💡 提示: 数据仅在内存中，不会保存到数据库",
                    "生成成功",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Gvar.Logger.ErrorException(ex, "生成测试数据异常");
                
                MessageBox.Show(
                    $"❌ 生成测试数据时发生异常！\n\n" +
                    $"错误信息：{ex.Message}\n\n" +
                    $"详细信息：\n{ex.StackTrace}",
                    "生成失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        
        private double GenerateGaussianRandom(Random rand)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return randStdNormal;
        }

        #endregion
    }
}
