using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using COTUI.通用功能类;
using COTUI.数据库.Services;
using COTUI.数据库.Models;
using ScottPlot;
using ScottPlot.Plottable;

namespace COTUI.统计分析
{
    /// <summary>
    /// SPC过程监控页面（Form版本）
    /// 
    /// <para><b>显示内容：</b></para>
    /// - Cpk计算
    /// - 过程能力分析
    /// - 控制图（X-R图）
    /// - 趋势分析
    /// </summary>
    public partial class SPCMonitorPage : Form
    {
        #region 字段

        private ProductionDataService productionService = new ProductionDataService();

        #endregion

        #region 构造函数

        public SPCMonitorPage()
        {
            InitializeComponent();
            Gvar.Logger.Info("SPC监控页面创建");
            
            // 设置默认选中项
            if (cmbMeasureType.Items.Count > 0)
            {
                cmbMeasureType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 页面加载

        private void SPCMonitorPage_Load(object sender, EventArgs e)
        {
            try
            {
                Gvar.Logger.Info("SPC监控页面加载");
                Gvar.Logger.Info("SPC监控页面加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "SPC监控页面加载失败");
                MessageBox.Show($"页面加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 计算Cpk

        /// <summary>
        /// 计算按钮点击事件
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取选中的测量项目
                if (cmbMeasureType.SelectedItem == null)
                {
                    MessageBox.Show("请选择测量项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                string measureType = cmbMeasureType.SelectedItem.ToString();
                string fieldName = GetFieldNameByMeasureType(measureType);

                if (string.IsNullOrEmpty(fieldName))
                {
                    MessageBox.Show("请选择测量项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 查询最近100条数据
                var allRecords = productionService.GetRecordsByDateRange(
                    DateTime.Now.AddDays(-7),
                    DateTime.Now
                );

                // ✅ 在这里检查是否有数据，没有数据时才弹窗提示
                if (allRecords.Count == 0)
                {
                    MessageBox.Show(
                        " 暂无测量数据\n\n" +
                        "请先进行以下操作：\n" +
                        "1. 在主页面进行产品测量\n" +
                        "2. 扫描产品条码\n" +
                        "3. 保存测量数据\n\n" +
                        "或使用【产品追溯查询】页面中的\n" +
                        "【生成测试数据】功能创建测试数据",
                        "无数据",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 手动取前100条
                var records = new List<ProductionDataModel>();
                int count = Math.Min(100, allRecords.Count);
                for (int i = 0; i < count; i++)
                {
                    records.Add(allRecords[i]);
                }

                // 提取测量值
                List<double> values = ExtractValues(records, fieldName);

                if (values.Count < 10)
                {
                    MessageBox.Show(
                        $"数据量不足\n\n" +
                        $"当前数据量：{values.Count} 条\n" +
                        $"最少需要：10 条\n\n" +
                        $"请积累更多测量数据后再进行Cpk分析",
                        "提示", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 计算统计量
                double mean = values.Average();
                double stdDev = CalculateStdDev(values, mean);

                // ✅ 从Config.ini读取规格限
                var config = ConfigManager.Instance;
                string uslKey = $"USL_{fieldName}";
                string lslKey = $"LSL_{fieldName}";
                
                double usl = config.GetDoubleValue("SPC", uslKey, 0);
                double lsl = config.GetDoubleValue("SPC", lslKey, 0);

                // ✅ 验证规格限是否有效
                if (usl == 0 || lsl == 0 || usl <= lsl)
                {
                    MessageBox.Show(
                        $"规格限配置错误\n\n" +
                        $"测量项目：{measureType}\n" +
                        $"字段名称：{fieldName}\n" +
                        $"上限（USL）：{usl}\n" +
                        $"下限（LSL）：{lsl}\n\n" +
                        $"请在Config.ini的[SPC]节中正确配置：\n" +
                        $"{uslKey}=上限值\n" +
                        $"{lslKey}=下限值\n\n" +
                        $"注意：上限必须大于下限！",
                        "配置错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    Gvar.Logger.Info($"规格限配置错误: {fieldName}, USL={usl}, LSL={lsl}");
                    return;
                }

                Gvar.Logger.Info($"从配置读取规格限: {fieldName}, USL={usl}, LSL={lsl}");

                // 计算Cp和Cpk
                double cp = (usl - lsl) / (6 * stdDev);
                double cpkUpper = (usl - mean) / (3 * stdDev);
                double cpkLower = (mean - lsl) / (3 * stdDev);
                double cpk = Math.Min(cpkUpper, cpkLower);

                // 更新UI
                UpdateStatistics(cpk, cp, mean, stdDev, usl, lsl);
                UpdateDataGrid(records, fieldName, mean);
                ShowControlChart(values);

                Gvar.Logger.Info($"Cpk计算完成: {cpk:F3} (基于{values.Count}条数据)");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "计算Cpk失败");
                MessageBox.Show($"计算失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 根据测量项目获取字段名
        /// </summary>
        private string GetFieldNameByMeasureType(string measureType)
        {
            if (measureType.Contains("X尺寸")) return "Dimension_X";
            if (measureType.Contains("Y尺寸")) return "Dimension_Y";
            if (measureType.Contains("Z尺寸")) return "Dimension_Z";
            if (measureType.Contains("角度")) return "Angle";
            if (measureType.Contains("间隙")) return "Gap";
            if (measureType.Contains("厚度")) return "Thickness";
            if (measureType.Contains("直径")) return "Diameter";
            if (measureType.Contains("周期时间")) return "CycleTime";
            return "";
        }

        /// <summary>
        /// 提取测量值
        /// </summary>
        private List<double> ExtractValues(List<ProductionDataModel> records, string fieldName)
        {
            var values = new List<double>();

            foreach (var record in records)
            {
                double? value = null;

                switch (fieldName)
                {
                    case "Dimension_X": value = record.Dimension_X; break;
                    case "Dimension_Y": value = record.Dimension_Y; break;
                    case "Dimension_Z": value = record.Dimension_Z; break;
                    case "Angle": value = record.Angle; break;
                    case "Gap": value = record.Gap; break;
                    case "Thickness": value = record.Thickness; break;
                    case "Diameter": value = record.Diameter; break;
                    case "CycleTime": value = record.CycleTime; break;
                }

                if (value.HasValue && value.Value > 0)
                {
                    values.Add(value.Value);
                }
            }

            return values;
        }

        /// <summary>
        /// 计算标准差
        /// </summary>
        private double CalculateStdDev(List<double> values, double mean)
        {
            double sumOfSquares = values.Sum(v => Math.Pow(v - mean, 2));
            return Math.Sqrt(sumOfSquares / (values.Count - 1));
        }

        /// <summary>
        /// 更新统计结果
        /// </summary>
        private void UpdateStatistics(double cpk, double cp, double mean, double stdDev, double usl, double lsl)
        {
            lblCpk.Text = $"{cpk:F3}";
            lblCpk.ForeColor = cpk >= 1.33 ? Color.Green : cpk >= 1.0 ? Color.Orange : Color.Red;

            lblCp.Text = $"{cp:F3}";
            lblMean.Text = $"{mean:F3}";
            lblStdDev.Text = $"{stdDev:F4}";
            lblUSL.Text = $"{usl:F3}";
            lblLSL.Text = $"{lsl:F3}";
        }

        /// <summary>
        /// 更新数据表格
        /// </summary>
        private void UpdateDataGrid(List<ProductionDataModel> records, string fieldName, double mean)
        {
            dgvResults.Rows.Clear();

            int displayCount = Math.Min(100, records.Count);
            double stdDev = 0;
            
            // 先收集所有值以计算标准差
            List<double> allValues = new List<double>();
            foreach (var record in records)
            {
                double? value = GetFieldValue(record, fieldName);
                if (value.HasValue && value.Value > 0)
                {
                    allValues.Add(value.Value);
                }
            }
            
            if (allValues.Count > 1)
            {
                stdDev = CalculateStdDev(allValues, mean);
            }

            for (int i = 0; i < displayCount; i++)
            {
                var record = records[i];
                double? value = GetFieldValue(record, fieldName);

                if (!value.HasValue || value.Value <= 0)
                    continue;

                double deviation = value.Value - mean;
                double zScore = stdDev > 0 ? Math.Abs(deviation) / stdDev : 0;
                string status = zScore > 3 ? "异常" : (zScore > 2 ? "警告" : "正常");

                int rowIndex = dgvResults.Rows.Add(
                    record.ProductSN,
                    record.ProductionTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    $"{value.Value:F3}",
                    $"{deviation:+0.000;-0.000}",
                    status,
                    record.ProductModel ?? "未知型号"
                );

                // 根据状态设置行颜色
                if (status == "异常")
                {
                    dgvResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 210);
                    dgvResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                }
                else if (status == "警告")
                {
                    dgvResults.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 157);
                    dgvResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(245, 124, 0);
                }
            }
            
            // 显示统计信息
            UpdateStatusMessage(records.Count, allValues.Count);
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        private double? GetFieldValue(ProductionDataModel record, string fieldName)
        {
            switch (fieldName)
            {
                case "Dimension_X": return record.Dimension_X;
                case "Dimension_Y": return record.Dimension_Y;
                case "Dimension_Z": return record.Dimension_Z;
                case "Angle": return record.Angle;
                case "Gap": return record.Gap;
                case "Thickness": return record.Thickness;
                case "Diameter": return record.Diameter;
                case "CycleTime": return record.CycleTime;
                default: return null;
            }
        }

        /// <summary>
        /// 更新状态消息
        /// </summary>
        private void UpdateStatusMessage(int totalRecords, int validRecords)
        {
            // 更新图表区域的占位符文本
            if (panelChart.Controls.Count > 0)
            {
                var lblPlaceholder = panelChart.Controls[0] as Label;
                if (lblPlaceholder != null)
                {
                    lblPlaceholder.Text = $"控制图区域\n\n已分析 {validRecords} 条有效数据（共 {totalRecords} 条记录）\n\n（需要集成图表库如LiveCharts或ScottPlot显示X-R控制图）";
                }
            }
        }

        private void ShowControlChart(List<double> values)
        {
            try
            {
                // 清空panel
                panelChart.Controls.Clear();
                
                // 创建ScottPlot控件
                var formsPlot = new ScottPlot.FormsPlot();
                formsPlot.Dock = DockStyle.Fill;
                
                // 准备数据
                double[] dataX = Enumerable.Range(1, values.Count).Select(x => (double)x).ToArray();
                double[] dataY = values.ToArray();
                
                // 计算均值和控制限
                double mean = values.Average();
                double stdDev = CalculateStdDev(values, mean);
                double ucl = mean + 3 * stdDev;  // 上控制限
                double lcl = mean - 3 * stdDev;  // 下控制限
                
                // 添加散点图和连线（测量值）
                var scatter = formsPlot.Plot.AddScatterLines(dataX, dataY);
                scatter.Label = "测量值";
                scatter.MarkerSize = 8;
                scatter.Color = System.Drawing.Color.Blue;
                scatter.LineWidth = 2;
                
                // 添加均值线
                var meanLine = formsPlot.Plot.AddHorizontalLine(mean);
                meanLine.Label = string.Format("均值 ({0:F3})", mean);
                meanLine.Color = System.Drawing.Color.Green;
                meanLine.LineWidth = 2;
                meanLine.LineStyle = LineStyle.Solid;
                
                // 添加上控制限
                var uclLine = formsPlot.Plot.AddHorizontalLine(ucl);
                uclLine.Label = string.Format("UCL ({0:F3})", ucl);
                uclLine.Color = System.Drawing.Color.Red;
                uclLine.LineWidth = 2;
                uclLine.LineStyle = LineStyle.Dash;
                
                // 添加下控制限
                var lclLine = formsPlot.Plot.AddHorizontalLine(lcl);
                lclLine.Label = string.Format("LCL ({0:F3})", lcl);
                lclLine.Color = System.Drawing.Color.Red;
                lclLine.LineWidth = 2;
                lclLine.LineStyle = LineStyle.Dash;
                
                // 标记异常点
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i] > ucl || values[i] < lcl)
                    {
                        // 异常点用红色标记
                        var marker = formsPlot.Plot.AddPoint(dataX[i], dataY[i]);
                        marker.Color = System.Drawing.Color.Red;
                        marker.MarkerSize = 12;
                        marker.MarkerShape = MarkerShape.filledCircle;
                    }
                    else if (values[i] > mean + 2 * stdDev || values[i] < mean - 2 * stdDev)
                    {
                        // 警告点用橙色标记
                        var marker = formsPlot.Plot.AddPoint(dataX[i], dataY[i]);
                        marker.Color = System.Drawing.Color.Orange;
                        marker.MarkerSize = 10;
                        marker.MarkerShape = MarkerShape.filledCircle;
                    }
                }
                
                // 设置坐标轴
                formsPlot.Plot.XLabel("样本序号");
                formsPlot.Plot.YLabel("测量值");
                formsPlot.Plot.Title(string.Format("X控制图 ({0}个样本)", values.Count));
                
                // 显示图例
                formsPlot.Plot.Legend(location: Alignment.UpperRight);
                
                // 自动缩放
                formsPlot.Plot.AxisAuto();
                
                // 设置网格样式
                formsPlot.Plot.Grid(color: System.Drawing.Color.LightGray);
                
                // 刷新图表
                formsPlot.Refresh();
                
                // 添加到panel
                panelChart.Controls.Add(formsPlot);
                
                Gvar.Logger.Info(string.Format("ScottPlot控制图绘制成功，数据点数：{0}", values.Count));
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "绘制ScottPlot图表失败");
                ShowPlaceholder(values.Count, "图表加载失败：" + ex.Message);
            }
        }
        #endregion

        #region 定时刷新

        private System.Windows.Forms.Timer refreshTimer;

        private void InitializeTimer()
        {
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 60000; // 1分钟
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            btnCalculate_Click(null, EventArgs.Empty); // 触发重新计算
        }

        #endregion

        #region 显示占位符

        /// <summary>
        /// 显示占位符
        /// </summary>
        private void ShowPlaceholder(int dataCount, string message = null)
        {
            panelChart.Controls.Clear();

            Label lblPlaceholder = new Label
            {
                Dock = DockStyle.Fill,
                Text = message ?? 
                       "📊 控制图区域\n\n" +
                       "✅ 已分析 " + dataCount + " 条数据\n\n" +
                       "⚠️ 等待数据加载...",
                Font = new Font("微软雅黑", 11),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(250, 250, 250)
            };
            panelChart.Controls.Add(lblPlaceholder);
        }

        #endregion
    }
}
