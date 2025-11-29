using System;
using System.Windows.Forms;
using COTUI.通用功能类;

namespace COTUI.统计分析
{
    /// <summary>
    /// 统计分析主窗体
    /// 
    /// <para><b>包含3个功能标签页：</b></para>
    /// 1. 实时数据看板 - 显示产量、良率、CT等实时数据
    /// 2. SPC过程监控 - 显示Cpk、控制图等统计数据
    /// 3. 产品追溯查询 - 根据SN查询完整生产历史
    /// 
    /// <para><b>优化特性：</b></para>
    /// - 延迟加载（Lazy Loading）：只在首次访问时创建子窗体
    /// - 节省内存和启动时间
    /// - 自动释放不用的资源
    /// </summary>
    public partial class StatisticsAnalysisPage : Form
    {
        #region 字段

        private Logger logger = Logger.GetInstance();
        
        // 子窗体实例（延迟创建）
        private DashboardPage dashboardPage;
        private SPCMonitorPage spcMonitorPage;
        private TraceabilityPage traceabilityPage;
        
        // 记录哪些页面已经加载过
        private bool isDashboardLoaded = false;
        private bool isSPCLoaded = false;
        private bool isTraceabilityLoaded = false;

        #endregion

        #region 构造函数

        public StatisticsAnalysisPage()
        {
            InitializeComponent();
            Gvar.Logger.Info("统计分析页面创建");
            
            // ✅ 不再在构造函数中创建所有子窗体
            // 改为在标签页切换时按需加载
        }

        #endregion

        #region 页面加载

        private void StatisticsAnalysisPage_Load(object sender, EventArgs e)
        {
            try
            {
                // ✅ 只加载当前显示的标签页（默认是第一个）
                LoadCurrentTabPage();
                
                Gvar.Logger.Info("统计分析页面加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "统计分析页面加载失败");
                MessageBox.Show($"页面加载失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 标签页切换（延迟加载核心逻辑）

        /// <summary>
        /// 标签页切换事件 - 实现延迟加载
        /// </summary>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = tabControl1.SelectedIndex;
                Gvar.Logger.Info($"切换到标签页: {selectedIndex}");
                
                // ✅ 只加载当前选中的标签页
                LoadCurrentTabPage();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "切换标签页失败");
            }
        }

        /// <summary>
        /// 加载当前选中的标签页（延迟加载）
        /// </summary>
        private void LoadCurrentTabPage()
        {
            int selectedIndex = tabControl1.SelectedIndex;
            
            switch (selectedIndex)
            {
                case 0: // 实时数据看板
                    LoadDashboardPage();
                    break;
                    
                case 1: // SPC过程监控
                    LoadSPCMonitorPage();
                    break;
                    
                case 2: // 产品追溯查询
                    LoadTraceabilityPage();
                    break;
            }
        }

        /// <summary>
        /// 加载实时数据看板（延迟加载）
        /// </summary>
        private void LoadDashboardPage()
        {
            if (isDashboardLoaded)
            {
                Gvar.Logger.Info("实时数据看板已加载，跳过");
                return; // 已经加载过，直接返回
            }
            
            try
            {
                Gvar.Logger.Log("开始加载实时数据看板...");
                
                dashboardPage = new DashboardPage();
                LoadFormIntoPanel(dashboardPage, panel_Dashboard);
                
                isDashboardLoaded = true;
                Gvar.Logger.Info(" 实时数据看板加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载实时数据看板失败");
                MessageBox.Show($"加载实时数据看板失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载SPC过程监控（延迟加载）
        /// </summary>
        private void LoadSPCMonitorPage()
        {
            if (isSPCLoaded)
            {
                Gvar.Logger.Info("SPC过程监控已加载，跳过");
                return; // 已经加载过，直接返回
            }
            
            try
            {
                Gvar.Logger.Log("开始加载SPC过程监控...");
                
                spcMonitorPage = new SPCMonitorPage();
                LoadFormIntoPanel(spcMonitorPage, panel_SPC);
                
                isSPCLoaded = true;
                Gvar.Logger.Info("SPC过程监控加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载SPC过程监控失败");
                MessageBox.Show($"加载SPC过程监控失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载产品追溯查询（延迟加载）
        /// </summary>
        private void LoadTraceabilityPage()
        {
            if (isTraceabilityLoaded)
            {
                Gvar.Logger.Info("产品追溯查询已加载，跳过");
                return; // 已经加载过，直接返回
            }
            
            try
            {
                Gvar.Logger.Log("开始加载产品追溯查询...");
                
                traceabilityPage = new TraceabilityPage();
                LoadFormIntoPanel(traceabilityPage, panel_Traceability);
                
                isTraceabilityLoaded = true;
                Gvar.Logger.Info(" 产品追溯查询加载完成");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载产品追溯查询失败");
                MessageBox.Show($"加载产品追溯查询失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 将Form加载到Panel中
        /// </summary>
        private void LoadFormIntoPanel(Form form, Panel panel)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(form);
            form.Show();
        }

        #endregion

        #region 窗体关闭

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // 关闭已加载的子窗体
                if (isDashboardLoaded && dashboardPage != null)
                {
                    dashboardPage.Close();
                    Gvar.Logger.Info("实时数据看板已关闭");
                }
                
                if (isSPCLoaded && spcMonitorPage != null)
                {
                    spcMonitorPage.Close();
                    Gvar.Logger.Log("SPC过程监控已关闭");
                }
                
                if (isTraceabilityLoaded && traceabilityPage != null)
                {
                    traceabilityPage.Close();
                    Gvar.Logger.Log("产品追溯查询已关闭");
                }

                Gvar.Logger.Log("统计分析页面关闭");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "关闭统计分析页面失败");
            }

            base.OnFormClosing(e);
        }

        #endregion
    }
}
