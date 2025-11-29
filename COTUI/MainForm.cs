using CCWin;
using COTUI.报警页面;
using COTUI.日志页面;
using COTUI.控件类库;
using COTUI.权限管理;
using COTUI.通用功能类;
using COTUI.统计分析;
using COTUI.扫码管理;  // ← 添加扫码管理命名空间
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI
{
    public partial class Form1 : CCSkinMain
    {
        // 使用全局变量 Gvar.Logger 访问日志服务
        
        public Form1()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            InitializeStatusTimer();
        }

        #region    //toolstrip控件相关变量声明
        CustomToolStripRenderer render = new CustomToolStripRenderer();
        //创建文字状态的Label
        ToolStripLabel statuslabel = new ToolStripLabel();
        //创建图片label
        ToolStripLabel imagelabel = new ToolStripLabel();
        #endregion

        #region   //窗体声明
        MainPage mainPage = new MainPage();
        //设置窗体（延迟创建，不在初始化时创建）
        private SettingForm settingForm = null;
        //判断当前窗体是否已加载
        private Form currentForm;
        //视觉窗体（延迟创建）
        private VisionForm visionForm = null;
        //日志窗体
        private LogPage logPage = null;
        //报警窗体
        private WarningPage warningPage = null;
        //运动控制窗体（延迟创建）
        private COTUI.运动控制页面.MotionControlPage motionControlPage = null;
        //统计分析窗体（延迟创建）← 添加
        private StatisticsAnalysisPage statisticsPage = null;
        #endregion

        #region//状态栏
        // 性能监控（使用 Task 替代 Timer）
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private CancellationTokenSource statusUpdateCts;
        #endregion
        
        /// <summary>
        /// 初始化性能计数器
        /// </summary>
        private void InitializePerformanceCounters()
        {
            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            }
            catch (Exception ex)
            {
                Gvar.Logger.Log(LogLevel.Warn, $"初始化性能计数器失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化状态栏更新任务（使用 Task 替代 Timer）
        /// </summary>
        private void InitializeStatusTimer()
        {
            statusUpdateCts = new CancellationTokenSource();

            // 启动异步状态更新任务
            Task.Run(async () =>
            {
                try
                {
                    while (!statusUpdateCts.Token.IsCancellationRequested)
                    {
                        UpdateStatusInfo();
                        await Task.Delay(1000, statusUpdateCts.Token); // 每秒更新一次
                    }
                }
                catch (OperationCanceledException)
                {
                    // 正常取消，不记录错误
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, "状态栏更新异常");
                }
            }, statusUpdateCts.Token);
        }

        /// <summary>
        /// 更新状态栏信息
        /// </summary>
        private void UpdateStatusInfo()
        {
            try
            {
                if (cpuCounter != null && ramCounter != null)
                {
                    float cpuUsage = cpuCounter.NextValue();
                    // 等待一小段时间以获得更准确的CPU使用率
                    System.Threading.Thread.Sleep(10);
                    cpuUsage = cpuCounter.NextValue();

                    float availableMemory = ramCounter.NextValue();

                    // 更新状态栏信息
                    if (!this.IsDisposed && this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateStatusStripDisplay(cpuUsage, availableMemory);
                        });
                    }
                    else if (!this.IsDisposed)
                    {
                        UpdateStatusStripDisplay(cpuUsage, availableMemory);
                    }
                }
            }
            catch (Exception ex)
            {
                // 静默处理性能计数器异常
                Gvar.Logger.Log(LogLevel.Debug, $"更新状态信息失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 更新状态栏显示
        /// </summary>
        private void UpdateStatusStripDisplay(float cpuUsage, float availableMemory)
        {
            try
            {
                if (statusStrip1.Items.Count == 0)
                {
                    var statusLabel = new ToolStripStatusLabel();
                    statusLabel.Name = "statusLabel";
                    statusStrip1.Items.Add(statusLabel);
                }

                var cpuUsageText = $"CPU: {cpuUsage:F1}%";
                var memoryText = $"可用内存: {availableMemory:F0} MB";
                statusStrip1.Items[0].Text = $"{cpuUsageText} | {memoryText}";
            }
            catch (Exception ex)
            {
                Gvar.Logger.Log(LogLevel.Debug, $"更新状态栏显示失败: {ex.Message}");
            }
        }
        /// <summary>
        /// 工具栏toolstrip控件加载
        /// </summary>
        private void toolstripLoad()
        {

            //设置label图像
            imagelabel.Alignment = ToolStripItemAlignment.Right;
            imagelabel.AutoSize = false;
            imagelabel.Size = new Size(156, 60);
            imagelabel.Image = Properties.Resources.LOGO2;
            imagelabel.Text = "";
            imagelabel.ImageScaling = ToolStripItemImageScaling.None;
            skinToolStrip1.Items.Add(imagelabel);

            //设置状态
            statuslabel.Alignment = ToolStripItemAlignment.Right;
            statuslabel.AutoSize = false;
            statuslabel.Size = new Size(120, 78);
            statuslabel.Font = new Font("微软雅黑", 11, FontStyle.Bold);
            statuslabel.Name = "statusLabel";
            statuslabel.Text = "状态：待料";
            skinToolStrip1.Items.Add(statuslabel);
            render.SetlabelColor("statusLabel", Color.Orange);

            skinToolStrip1.Invalidate();
            skinToolStrip1.Renderer = render;
            rankControl.ArrangeRightControls(skinToolStrip1, imagelabel, statuslabel);

        }

        /// <summary>
        /// 主窗体MainPage加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// panel1.Controls.Clear();
        private void Load_MainPage()
        {
            // 如果当前已经是主页面，不重复加载
            if (currentForm == mainPage && !mainPage.IsDisposed)
            {
                return;
            }

            DestroyFormExceptFirstpage();
            skinPanel1.Controls.Clear();
            mainPage.FormBorderStyle = FormBorderStyle.None; // 无边框
            mainPage.TopLevel = false; // 不是最顶层窗体
            mainPage.Dock = DockStyle.Fill;
            skinPanel1.Controls.Add(mainPage);  // 添加到 Panel中
            mainPage.Show();     // 显示
            currentForm = mainPage;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            toolstripLoad();
            Load_MainPage();
            
            // 启用工具栏拖动功能
            ToolStripHelper.EnableDragging(skinToolStrip1, this);
        }

        /// <summary>
        /// 销毁除FirstPage之外的其他窗口
        /// </summary>
        private void DestroyFormExceptFirstpage()
        {
            foreach (Control ct in skinPanel1.Controls)
            {
                if (ct.Text != "主画面" && ct.Text != "EnvSetting_Setting")
                {
                    ct.Dispose();
                }
            }
            skinPanel1.Controls.Clear();
        }

        private async void toolStrip_startButton_Click(object sender, EventArgs e)
        {
            await Task.Delay(50);
            statuslabel.Text = "状态：运行";
            render.SetlabelColor("statusLabel", Color.Green);
            
            // 记录到运行日志
            Gvar.Logger.Log(LogLevel.Info, "▶ 系统开始运行");
        }

        private async void toolStrip_pauseButton_Click(object sender, EventArgs e)
        {
            await Task.Delay(50);
            statuslabel.Text = "状态：待料";
            render.SetlabelColor("statusLabel", Color.Orange);
            
            // 记录到运行日志
            Gvar.Logger.Log(LogLevel.Warn, "⏸ 系统暂停，等待上料");
        }

        private async void toolStrip_stopButton_Click(object sender, EventArgs e)
        {
            await Task.Delay(50);
            statuslabel.Text = "状态：停机";
            render.SetlabelColor("statusLabel", Color.Red);
            
            // 记录到运行日志
            Gvar.Logger.Log(LogLevel.Error, "⏹ 系统停止运行");
        }

        private void toolStrip_Admin_Click(object sender, EventArgs e)
        {
            new Permission_Management().Show();
        }

        private void toolStrip_Waining_Click(object sender, EventArgs e)
        {
            //if (Gvar._User != "admin" && Gvar._User != "user1" )
            //{
            //    MessageBox.Show("无权限");
            //    return;
            //}

            // 如果报警窗体已存在且未被释放，直接显示
            if (warningPage != null && !warningPage.IsDisposed && currentForm == warningPage)
            {
                return;
            }

            // 创建新的报警窗体实例
            warningPage = new WarningPage();
            ShowFormInPanel(warningPage);
        }

        private void toolStrip_MainButton_Click(object sender, EventArgs e)
        {
            Load_MainPage();
        }

        private void toolStrip_Set_ButtonClick(object sender, EventArgs e)
        {
            // 检查设置窗体是否已释放，如果释放则重新创建
            if (settingForm == null || settingForm.IsDisposed)
            {
                settingForm = new SettingForm();
            }

            // 如果设置窗体已显示，不重复加载
            if (currentForm == settingForm)
            {
                return;
            }

            ShowFormInPanel(settingForm);
        }
        //加载到主窗体的Skinpanel
        private void ShowFormInPanel(Form form)
        {
            try {
                // 如果要加载的窗体与当前窗体相同且未被释放，不重复加载
                if (currentForm == form && !form.IsDisposed)
                {
                    return;
                }

                //清理当前窗体（但不释放 mainPage、settingForm 和 visionForm）
                if (currentForm != null && !currentForm.IsDisposed)
                {
                    // 对于可复用的窗体，只隐藏不释放
                    if (currentForm == mainPage || currentForm == settingForm || currentForm == visionForm)
                    {
                        currentForm.Hide();
                    }
                    else
                    {
                        // 对于其他窗体，关闭并释放
                        currentForm.Close();
                        currentForm.Dispose();
                    }
                }
                
                //清除当前panel
                skinPanel1.Controls.Clear();
                form.FormBorderStyle = FormBorderStyle.None; // 无边框
                form.TopLevel = false; // 不是最顶层窗体
                form.Dock = DockStyle.Fill;
                //确保窗体不在其他容器中
                if (form.Parent != null)
                {
                    form.Parent.Controls.Remove(form);
                }
                skinPanel1.Controls.Add(form);  // 添加到 Panel中
                form.Show();
                currentForm = form;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载窗体出错：" + ex.Message);
                return;
            }
          
        }
        /// <summary>
        /// 窗体关闭时释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 取消异步状态更新任务
            if (statusUpdateCts != null)
            {
                statusUpdateCts.Cancel();
                statusUpdateCts.Dispose();
            }

            // 释放性能计数器
            if (cpuCounter != null)
            {
                cpuCounter.Dispose();
            }
            
            if (ramCounter != null)
            {
                ramCounter.Dispose();
            }

            // 释放所有窗体
            if (settingForm != null && !settingForm.IsDisposed)
            {
                settingForm.Dispose();
            }

            if (visionForm != null && !visionForm.IsDisposed)
            {
                visionForm.Dispose();
            }

            if (logPage != null && !logPage.IsDisposed)
            {
                logPage.Dispose();
            }

            if (warningPage != null && !warningPage.IsDisposed)
            {
                warningPage.Dispose();
            }

            if (motionControlPage != null && !motionControlPage.IsDisposed)
            {
                motionControlPage.Dispose();
            }

            // 释放统计分析窗体← 添加
            if (statisticsPage != null && !statisticsPage.IsDisposed)
            {
                statisticsPage.Dispose();
            }

            if (mainPage != null && !mainPage.IsDisposed)
            {
                mainPage.Dispose();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // 检查视觉窗体是否已释放，如果释放则重新创建
            if (visionForm == null || visionForm.IsDisposed)
            {
                visionForm = new VisionForm();
            }

            // 如果视觉窗体已显示，不重复加载
            if (currentForm == visionForm)
            {
                return;
            }

            ShowFormInPanel(visionForm);
        }

        /// <summary>
        /// 日志按钮点击事件
        /// </summary>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // 如果日志窗体已存在且未被释放，直接显示
            if (logPage != null && !logPage.IsDisposed && currentForm == logPage)
            {
                return;
            }

            // 创建新的日志窗体实例
            logPage = new LogPage();
            // 设置当前工站（从全局变量获取）
            logPage.SetStation(Gvar.CurrentStation);
            ShowFormInPanel(logPage);
        }

        /// <summary>
        /// 运动控制按钮点击事件
        /// </summary>
        private void toolStripButton_MotionControl_Click(object sender, EventArgs e)
        {
            // 检查运动控制窗体是否已释放，如果释放则重新创建
            if (motionControlPage == null || motionControlPage.IsDisposed)
            {
                motionControlPage = new COTUI.运动控制页面.MotionControlPage();
            }

            // 如果运动控制窗体已显示，不重复加载
            if (currentForm == motionControlPage)
            {
                return;
            }

            ShowFormInPanel(motionControlPage);
            Gvar.Logger.Log(LogLevel.Info, "打开运动控制页面");
        }

        /// <summary>
        /// 统计分析按钮点击事件← 新增方法
        /// </summary>
        private void toolStripButton_Statistics_Click(object sender, EventArgs e)
        {
            // 检查统计分析窗体是否已释放，如果释放则重新创建
            if (statisticsPage == null || statisticsPage.IsDisposed)
            {
                statisticsPage = new StatisticsAnalysisPage();
            }

            // 如果统计分析窗体已显示，不重复加载
            if (currentForm == statisticsPage)
            {
                return;
            }

            ShowFormInPanel(statisticsPage);
            Gvar.Logger.Log(LogLevel.Info, "打开统计分析页面");
        }

    }
}
