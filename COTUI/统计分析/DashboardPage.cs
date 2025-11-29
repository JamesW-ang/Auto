using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using COTUI.通用功能类;
using COTUI.数据库.Services;
using COTUI.数据库.Models;

namespace COTUI.统计分析
{
    /// <summary>
    /// 实时数据看板（Form版本）
    /// 
    /// <para><b>显示数据：</b></para>
    /// - 总产量、合格数（OK）、不合格数（NG）
    /// - 实时良率
    /// - 平均CT时间
    /// - 小时产量
    /// - Top 5缺陷类型
    /// - 设备状态
    /// </summary>
    public partial class DashboardPage : Form
    {
        #region 字段

        private Logger logger = Logger.GetInstance();
        private ProductionDataService productionService = new ProductionDataService();

        // 自动刷新
        private CancellationTokenSource refreshCts;
        private const int REFRESH_INTERVAL = 5000; // 5秒刷新一次

        #endregion

        #region 构造函数

        public DashboardPage()
        {
            InitializeComponent();
            Gvar.Logger.Log(LogLevel.Info, "实时数据看板创建");
        }

        #endregion

        #region 页面加载

        private void DashboardPage_Load(object sender, EventArgs e)
        {
            try
            {
                Gvar.Logger.Log(LogLevel.Info, "实时数据看板加载");
                LoadDashboardData();
                Gvar.Logger.Log(LogLevel.Info, "实时数据看板加载请求");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "实时数据看板加载失败");
                MessageBox.Show($"页面加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 数据加载

        /// <summary>
        /// 加载看板数据
        /// </summary>
        private void LoadDashboardData()
        {
            try
            {
                UpdateStatistics();
                Gvar.Logger.Log(LogLevel.Info, "看板数据加载成功");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载看板数据失败");
                MessageBox.Show($"数据加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 更新统计数据
        /// </summary>
        private void UpdateStatistics()
        {
            try
            {
                // 查询今日数据
                var records = productionService.GetRecordsByDateRange(
                    DateTime.Today,
                    DateTime.Now
                );

                int totalCount = records.Count;
                int okCount = records.Count(r => r.OverallResult == "OK");
                int ngCount = totalCount - okCount;
                double yieldRate = totalCount > 0 ? (double)okCount / totalCount * 100 : 0;

                // 更新统计控件
                lblTotalCount.Text = totalCount.ToString();
                lblOKCount.Text = okCount.ToString();
                lblNGCount.Text = ngCount.ToString();
                lblYieldRate.Text = $"{yieldRate:F2}%";

                // 更新最近记录
                UpdateRecentRecords(records);

                Gvar.Logger.Log(LogLevel.Debug, $"看板数据更新: 总产量：{totalCount}, 总产量：{okCount}, 不良：{ngCount}, 秒Ʒ总产量：{yieldRate:F2}%");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "更新统计数据失败");
            }
        }

        /// <summary>
        /// 更新最近记录
        /// </summary>
        private void UpdateRecentRecords(List<ProductionDataModel> records)
        {
            dgvRecent.Rows.Clear();

            // 取最近20条
            var recentRecords = records.OrderByDescending(r => r.ProductionTime).Take(20);

            foreach (var record in recentRecords)
            {
                int rowIndex = dgvRecent.Rows.Add(
                    record.ProductSN,
                    record.ProductionTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    record.ProductModel ?? "未知",
                    record.OverallResult,
                    record.Operator ?? "未知"
                );

                // 请求ݽ错误错误角色
                if (record.OverallResult == "OK")
                {
                    dgvRecent.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(200, 230, 201);
                }
                else
                {
                    dgvRecent.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 205, 210);
                }
            }
        }

        /// <summary>
        /// 定时秒刷新
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        #endregion

        #region 用户控件事件

        private void DashboardPage_ParentChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                // 绑定到父容器时加载数据
                LoadDashboardData();
            }
        }

        #endregion
    }
}
