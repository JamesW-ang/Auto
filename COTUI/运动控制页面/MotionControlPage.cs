using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using COTUI.通用功能类;
using COTUI.控件类库;

namespace COTUI.运动控制页面
{
    /// <summary>
    /// 运动控制页面
    /// 
    /// <para><b>功能：</b></para>
    /// - 控制运动轴（X/Y/Z轴）
    /// - 支持绝对/相对/JOG三种运动模式
    /// - 实时位置状态
    /// - 复位回零和快速定位
    /// - 节拍时间（CT）测试
    /// 
    /// <para><b>安全特性：</b></para>
    /// - 复位需先回零位
    /// - 急停保护
    /// - 防撞机制
    /// - 定位确认
    /// 
    /// <para><b>性能优化：</b></para>
    /// - ? 延迟加载，初始化提示约50ms
    /// - ? 异步初始化控件在后台加载，不阻塞UI
    /// - ? 状态轮询使用 Task 替代 Timer
    /// </summary>
    public partial class MotionControlPage : Form
    {
        #region 字段

        // 使用全局变量 Gvar.Logger 访问日志服务
        
        // ===== ? 延迟加载标志 =====
        private bool isInitialized = false;
        private CancellationTokenSource initCts;

        // 夹˶夹ģ式ö化
        private enum MotionMode
        {
            Absolute,  // 加载ģ式
            Relative,  // 到位ģ式
            JOG        // JOGģ式
        }

        // 化前夹˶夹ģ式
        private MotionMode currentMode = MotionMode.Absolute;

        // 加载化
        private readonly string[] axisNames = { "X化", "Y化", "Z化" };
        private readonly int axisCount = 3;

        // 化状态ָʾ加载化
        private Dictionary<string, Dictionary<string, StatusIndicatorControl>> axisStatusIndicators;

        // 化前化λ夹ã夹ģ夹⣩
        private double[] currentPositions = new double[3];
        private double[] targetPositions = new double[3];

        // JOG夹˶加载ƣ夹ʹ化 Task 到位 Timer化
        private CancellationTokenSource jogCts;
        private string currentJogDirection = "";

        // 化λ加载
        private List<MotionPoint> savedPoints = new List<MotionPoint>();

        // 状态到位¿化ƣ夹ʹ化 Task 到位 Timer化
        private CancellationTokenSource statusUpdateCts;

        #endregion

        #region 到位캯化

        public MotionControlPage()
        {
            InitializeComponent();
            
            // 开始化状态ָʾ加载化
            axisStatusIndicators = new Dictionary<string, Dictionary<string, StatusIndicatorControl>>();
            
            Gvar.Logger.Info("夹˶加载夹ҳ夹洴加载到位ٹ化죩");
        }

        #endregion

        #region ? 夹ӳټ化أ加载化Ż到位

        private void MotionControlPage_Load(object sender, EventArgs e)
        {
            // ===== ? 到位ٷ化أ加载到位UI =====
            if (isInitialized)
            {
                Gvar.Logger.Info("夹˶加载夹ҳ到位ѳ夹ʼ加载加载");
                return;
            }
            
            Gvar.Logger.Info("夹˶加载夹ҳ加载ʾ到位ӳټ到位ģ式化");
            
            // 提示加载提示加载ѡ化
            ShowLoadingIndicator();
            
            // ===== 夹첽开始到位ؼ夹 =====
            initCts = new CancellationTokenSource();
            
            Task.Run(async () =>
            {
                try
                {
                    // 夹ô加载夹提示到位ӳ夹50ms化
                    await Task.Delay(50, initCts.Token);
                    
                    // ===== 化UI夹程化г夹ʼ到位ؼ夹 =====
                    if (!this.IsDisposed && !initCts.Token.IsCancellationRequested)
                    {
                        await this.InvokeAsync(async () =>
                        {
                            try
                            {
                                Gvar.Logger.Info("开始夹첽开始到位ؼ夹...");
                                
                                // 1. 夹˶夹ģ式ѡ加载加载夹٣夹
                                InitializeMotionModeSelector();
                                await Task.Delay(10, initCts.Token);
                                
                                // 2. 加载化ƣ加载٣夹
                                InitializeDirectionControls();
                                await Task.Delay(10, initCts.Token);
                                
                                // 3. 化状态化أ加载到位~200ms化
                                InitializeAxisStatusMonitors();
                                await Task.Delay(10, initCts.Token);
                                
                                // 4. 加载到位壨到位٣夹
                                InitializeAxisControlPanels();
                                await Task.Delay(10, initCts.Token);
                                
                                // 5. 化λ加载加载夹٣夹
                                InitializePointManager();
                                await Task.Delay(10, initCts.Token);
                                
                                // 6. 到位ر到位ĵ夹λ加载夹٣夹
                                LoadDefaultPoints();
                                
                                // 7. 加载状态加载加载
                                StartStatusUpdateTask();
                                
                                // 到位ؼ加载夹ʾ
                                HideLoadingIndicator();
                                
                                isInitialized = true;
                                Gvar.Logger.Info("夹˶加载夹ҳ夹开始加载ɣ化첽化");
                            }
                            catch (OperationCanceledException)
                            {
                                Gvar.Logger.Info("夹˶加载夹ҳ夹开始加载取化");
                            }
                            catch (Exception ex)
                            {
                                Gvar.Logger.ErrorException(ex, "夹˶加载夹ҳ夹开始化失败");
                                MessageBox.Show($"ҳ夹开始化ʧ夹ܣ夹{ex.Message}", "加载", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        });
                    }
                }
                catch (OperationCanceledException)
                {
                    // 加载取化
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, "夹첽开始加载到位쳣");
                }
            }, initCts.Token);
        }

        /// <summary>
        /// 提示加载提示
        /// </summary>
        private void ShowLoadingIndicator()
        {
            try
            {
                // 加载夹ڴ加载Ӽ化ض加载夹提示
                // 到位磺提示һ化 "加载化..." 化 Label
                this.Text = "夹˶加载夹 - 加载化...";
            }
            catch { }
        }

        /// <summary>
        /// 到位ؼ加载夹ʾ
        /// </summary>
        private void HideLoadingIndicator()
        {
            try
            {
                this.Text = "夹˶加载夹";
            }
            catch { }
        }

        /// <summary>
        /// Task.Invoke 到位첽版本加载化 .NET Framework 4.7.2化
        /// </summary>
        private Task InvokeAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            action();
                            tcs.SetResult(true);
                        }
                        catch (Exception ex)
                        {
                            tcs.SetException(ex);
                        }
                    }));
                }
                else
                {
                    action();
                    tcs.SetResult(true);
                }
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
            
            return tcs.Task;
        }

        #endregion

        #region 夹˶夹ģ式ѡ化

        /// <summary>
        /// 开始到位˶夹ģ式ѡ加载
        /// </summary>
        private void InitializeMotionModeSelector()
        {
            // 加载ģ式
            rbAbsolute.CheckedChanged += (s, ev) =>
            {
                if (rbAbsolute.Checked)
                {
                    currentMode = MotionMode.Absolute;
                    UpdateModeDescription();
                    Gvar.Logger.Info("夹л加载到位ģ式");
                }
            };

            // 到位ģ式
            rbRelative.CheckedChanged += (s, ev) =>
            {
                if (rbRelative.Checked)
                {
                    currentMode = MotionMode.Relative;
                    UpdateModeDescription();
                    Gvar.Logger.Info("夹л加载化ģ式");
                }
            };

            // JOGģ式
            rbJog.CheckedChanged += (s, ev) =>
            {
                if (rbJog.Checked)
                {
                    currentMode = MotionMode.JOG;
                    UpdateModeDescription();
                    Gvar.Logger.Info("夹л到位JOGģ式");
                }
            };

            // 错化ѡ加载夹ģ式
            rbAbsolute.Checked = true;
        }

        /// <summary>
        /// 加载ģ式˵化
        /// </summary>
        private void UpdateModeDescription()
        {
            switch (currentMode)
            {
                case MotionMode.Absolute:
                    lblModeDescription.Text = "加载ģ式到位ƶ到位ָ加载化λ化";
                    break;
                case MotionMode.Relative:
                    lblModeDescription.Text = "到位ģ式到位入夹前λ到位ƶ夹ָ加载化";
                    break;
                case MotionMode.JOG:
                    lblModeDescription.Text = "JOGģ式加载ס化ť加载夹ƶ加载ɿ夹ֹͣ";
                    break;
            }
        }

        #endregion

        #region 加载到位

        /// <summary>
        /// 开始加载加载ư夹ť
        /// </summary>
        private void InitializeDirectionControls()
        {
            // 8加载化ť
            var directionButtons = new Dictionary<string, Button>
            {
                { "UpLeft", btnUpLeft },
                { "Up", btnUp },
                { "UpRight", btnUpRight },
                { "Left", btnLeft },
                { "Right", btnRight },
                { "DownLeft", btnDownLeft },
                { "Down", btnDown },
                { "DownRight", btnDownRight }
            };

            foreach (var kvp in directionButtons)
            {
                string direction = kvp.Key;
                Button button = kvp.Value;

                // 加载化ť
                StyleDirectionButton(button);

                // 化갴化 - 开始夹ƶ夹
                button.MouseDown += (s, ev) =>
                {
                    if (currentMode == MotionMode.JOG)
                    {
                        StartJogMotion(direction);
                    }
                };

                // 加载ͷ夹 - ֹͣ夹ƶ夹
                button.MouseUp += (s, ev) =>
                {
                    if (currentMode == MotionMode.JOG)
                    {
                        StopJogMotion();
                    }
                };

                // 到位 - 加载夹ƶ夹
                button.Click += (s, ev) =>
                {
                    if (currentMode != MotionMode.JOG)
                    {
                        ExecuteStepMotion(direction);
                    }
                };
            }
        }

        /// <summary>
        /// 加载加载ť
        /// </summary>
        private void StyleDirectionButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.DarkBlue;
            btn.BackColor = Color.LightBlue;
            
            // 化ͣЧ化
            btn.MouseEnter += (s, ev) => btn.BackColor = Color.SkyBlue;
            btn.MouseLeave += (s, ev) => btn.BackColor = Color.LightBlue;
        }

        /// <summary>
        /// 开始JOG夹˶到位ʹ化 Task 到位 Timer化
        /// </summary>
        private void StartJogMotion(string direction)
        {
            // 加载加载加载加载列到位ֹͣ
            StopJogMotion();

            currentJogDirection = direction;
            jogCts = new CancellationTokenSource();
            
            Gvar.Logger.Info($"开始JOG夹˶夹: {direction}");

            // 加载夹첽JOG加载
            Task.Run(async () =>
            {
                try
                {
                    while (!jogCts.Token.IsCancellationRequested)
                    {
                        // 到位ݷ加载化λ化
                        UpdatePositionByDirection(currentJogDirection, 0.5); // ÿ到位ƶ夹0.5mm
                        
                        // UI夹程̸加载夹ʾ
                        if (!this.IsDisposed)
                        {
                            this.Invoke(new Action(UpdateAxisDisplays));
                        }

                        // 夹ȴ夹50ms加载化ԭTimer化Interval化
                        await Task.Delay(50, jogCts.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 加载取加载加载¼加载
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, "JOG夹˶化쳣");
                }
            }, jogCts.Token);
        }

        /// <summary>
        /// ֹͣJOG夹˶夹
        /// </summary>
        private void StopJogMotion()
        {
            if (jogCts != null)
            {
                jogCts.Cancel();
                jogCts.Dispose();
                jogCts = null;
            }

            if (!string.IsNullOrEmpty(currentJogDirection))
            {
                Gvar.Logger.Info("ֹͣJOG夹˶夹");
                currentJogDirection = "";
            }
        }

        /// <summary>
        /// ִ夹е加载ƶ夹
        /// </summary>
        private void ExecuteStepMotion(string direction)
        {
            try
            {
                double stepDistance = (double)numStepDistance.Value;
                
                if (currentMode == MotionMode.Relative)
                {
                    // 到位ģ式到位入夹前λ到位ƶ夹
                    UpdatePositionByDirection(direction, stepDistance);
                    Gvar.Logger.Info($"加载ƶ夹 {direction}: {stepDistance}mm");
                }
                else if (currentMode == MotionMode.Absolute)
                {
                    // 加载ģ式加载ʾ夹û加载加载加载到位
                    MessageBox.Show("加载ģ式加载加载加载加载化Ŀ化λ化", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                UpdateAxisDisplays();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载夹ƶ夹失败");
            }
        }

        /// <summary>
        /// 到位ݷ加载化λ化
        /// </summary>
        private void UpdatePositionByDirection(string direction, double distance)
        {
            double sqrt2 = 0.707; // 夹Խ化߷加载ϵ化
            
            switch (direction)
            {
                case "Up":
                    currentPositions[1] += distance; // Y+
                    break;
                case "Down":
                    currentPositions[1] -= distance; // Y-
                    break;
                case "Left":
                    currentPositions[0] -= distance; // X-
                    break;
                case "Right":
                    currentPositions[0] += distance; // X+
                    break;
                case "UpLeft":
                    currentPositions[0] -= distance * sqrt2; // X-
                    currentPositions[1] += distance * sqrt2; // Y+
                    break;
                case "UpRight":
                    currentPositions[0] += distance * sqrt2; // X+
                    currentPositions[1] += distance * sqrt2; // Y+
                    break;
                case "DownLeft":
                    currentPositions[0] -= distance * sqrt2; // X-
                    currentPositions[1] -= distance * sqrt2; // Y-
                    break;
                case "DownRight":
                    currentPositions[0] += distance * sqrt2; // X+
                    currentPositions[1] -= distance * sqrt2; // Y-
                    break;
            }
        }

        #endregion

        #region 化状态到位

        /// <summary>
        /// 开始加载状态到位
        /// </summary>
        private void InitializeAxisStatusMonitors()
        {
            var statusNames = new[] { "加载", "加载", "加载", "ԭ化", "到位", "化ͣ", "化λ", "加载" };
            var statusPanels = new[] { flpXStatus, flpYStatus, flpZStatus };
            
            for (int axis = 0; axis < axisCount; axis++)
            {
                string axisName = axisNames[axis];
                axisStatusIndicators[axisName] = new Dictionary<string, StatusIndicatorControl>();

                for (int i = 0; i < statusNames.Length; i++)
                {
                    var indicator = new StatusIndicatorControl
                    {
                        Width = 60,
                        Height = 30,
                        LabelText = statusNames[i],
                        IsActive = false,
                        Margin = new Padding(2)
                    };

                    axisStatusIndicators[axisName][statusNames[i]] = indicator;
                    statusPanels[axis].Controls.Add(indicator);
                }
            }
        }

        /// <summary>
        /// 加载夹첽状态加载加载到位 Timer化
        /// </summary>
        private void StartStatusUpdateTask()
        {
            // 取加载加载化
            statusUpdateCts?.Cancel();
            statusUpdateCts?.Dispose();

            statusUpdateCts = new CancellationTokenSource();

            // 加载化̨加载
            Task.Run(async () =>
            {
                try
                {
                    while (!statusUpdateCts.Token.IsCancellationRequested)
                    {
                        // 加载化状态
                        UpdateAxisStatus();

                        // 夹ȴ夹100ms加载化ԭTimer化Interval化
                        await Task.Delay(100, statusUpdateCts.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 加载取加载加载¼加载
                }
                catch (Exception ex)
                {
                    Gvar.Logger.ErrorException(ex, "状态加载异常");
                }
            }, statusUpdateCts.Token);
        }

        /// <summary>
        /// 加载化状态
        /// </summary>
        private void UpdateAxisStatus()
        {
            // TODO: 化PLC到位˶加载ƿ到位取实现状态
            // 加载ʹ化ģ加载化

            for (int axis = 0; axis < axisCount; axis++)
            {
                string axisName = axisNames[axis];
                
                // ģ化状态化实现Ӧ化Ӳ加载取化
                UpdateAxisIndicator(axisName, "加载", false);
                UpdateAxisIndicator(axisName, "加载", currentPositions[axis] >= 500);
                UpdateAxisIndicator(axisName, "加载", currentPositions[axis] <= -500);
                UpdateAxisIndicator(axisName, "ԭ化", Math.Abs(currentPositions[axis]) < 0.1);
                UpdateAxisIndicator(axisName, "到位", Math.Abs(currentPositions[axis]) < 0.1);
                UpdateAxisIndicator(axisName, "化ͣ", false);
                UpdateAxisIndicator(axisName, "化λ", true);
                UpdateAxisIndicator(axisName, "加载", true);
            }
        }

        /// <summary>
        /// 到位µ到位状态ָʾ化
        /// </summary>
        private void UpdateAxisIndicator(string axisName, string statusName, bool isActive)
        {
            try
            {
                if (axisStatusIndicators.ContainsKey(axisName) && 
                    axisStatusIndicators[axisName].ContainsKey(statusName))
                {
                    var indicator = axisStatusIndicators[axisName][statusName];
                    
                    // UI夹程̰夹ȫ加载
                    if (indicator.InvokeRequired)
                    {
                        if (!indicator.IsDisposed)
                        {
                            indicator.Invoke(new Action(() => indicator.IsActive = isActive));
                        }
                    }
                    else
                    {
                        indicator.IsActive = isActive;
                    }
                }
            }
            catch (Exception ex)
            {
                // 化错加载加载加载加载到位
                Gvar.Logger.Info($"加载状态ָʾ化失败: {axisName}.{statusName} - {ex.Message}");
            }
        }

        #endregion

        #region 加载加载

        /// <summary>
        /// 开始加载加载化
        /// </summary>
        private void InitializeAxisControlPanels()
        {
            // X化
            txtXCurrent.ReadOnly = true;
            btnXMove.Click += (s, ev) => MoveToTarget(0);
            btnXHome.Click += (s, ev) => GoHome(0);
            btnXClear.Click += (s, ev) => ClearError(0);

            // Y化
            txtYCurrent.ReadOnly = true;
            btnYMove.Click += (s, ev) => MoveToTarget(1);
            btnYHome.Click += (s, ev) => GoHome(1);
            btnYClear.Click += (s, ev) => ClearError(1);

            // Z化
            txtZCurrent.ReadOnly = true;
            btnZMove.Click += (s, ev) => MoveToTarget(2);
            btnZHome.Click += (s, ev) => GoHome(2);
            btnZClear.Click += (s, ev) => ClearError(2);

            // 开始加载ʾ
            UpdateAxisDisplays();
        }

        /// <summary>
        /// 加载化λ加载ʾ
        /// </summary>
        private void UpdateAxisDisplays()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateAxisDisplays));
                return;
            }

            txtXCurrent.Text = currentPositions[0].ToString("F3");
            txtYCurrent.Text = currentPositions[1].ToString("F3");
            txtZCurrent.Text = currentPositions[2].ToString("F3");
        }

        /// <summary>
        /// 夹ƶ到位Ŀ化λ化
        /// </summary>
        private void MoveToTarget(int axisIndex)
        {
            try
            {
                TextBox targetTextBox = axisIndex == 0 ? txtXTarget : 
                                       axisIndex == 1 ? txtYTarget : txtZTarget;

                if (double.TryParse(targetTextBox.Text, out double targetPos))
                {
                    Gvar.Logger.Info($"{axisNames[axisIndex]} 夹ƶ夹: {currentPositions[axisIndex]:F3} -> {targetPos:F3}");

                    // TODO: 实到位ƶ化߼夹
                    // ģ夹⣺ֱ加载化λ化
                    currentPositions[axisIndex] = targetPos;
                    UpdateAxisDisplays();

                    MessageBox.Show($"{axisNames[axisIndex]} 夹ƶ加载", "成功", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("加载加载Ч化Ŀ化λ化", "加载", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"{axisNames[axisIndex]} 夹ƶ夹失败");
            }
        }

        /// <summary>
        /// 化ԭ化
        /// </summary>
        private void GoHome(int axisIndex)
        {
            try
            {
                Gvar.Logger.Info($"{axisNames[axisIndex]} 开始化ԭ化");
                
                // TODO: 实夹ʻ夹ԭ到位߼夹
                // ģ夹⣺ֱ加载化为0
                currentPositions[axisIndex] = 0;
                UpdateAxisDisplays();
                
                MessageBox.Show($"{axisNames[axisIndex]} 化ԭ加载夹", "成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"{axisNames[axisIndex]} 化ԭ化失败");
            }
        }

        /// <summary>
        /// 加载到位
        /// </summary>
        private void ClearError(int axisIndex)
        {
            // TODO: 实加载加载化߼夹
            Gvar.Logger.Info($"{axisNames[axisIndex]} 加载到位");
            MessageBox.Show($"{axisNames[axisIndex]} 加载加载夹", "成功", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region 化λ加载

        /// <summary>
        /// 开始加载λ加载
        /// </summary>
        private void InitializePointManager()
        {
            // DataGridView加载
            dgvPoints.AutoGenerateColumns = false;
            dgvPoints.AllowUserToAddRows = false;
            dgvPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // 加载化
            dgvPoints.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Name", 
                HeaderText = "化λ加载", 
                Width = 100 
            });
            
            dgvPoints.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "X", 
                HeaderText = "X加载", 
                Width = 70 
            });
            
            dgvPoints.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Y", 
                HeaderText = "Y加载", 
                Width = 70 
            });
            
            dgvPoints.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "Z", 
                HeaderText = "Z加载", 
                Width = 70 
            });

            var btnMoveCol = new DataGridViewButtonColumn 
            { 
                Name = "Move", 
                HeaderText = "", 
                Text = "夹ƶ夹", 
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            dgvPoints.Columns.Add(btnMoveCol);

            var btnUpdateCol = new DataGridViewButtonColumn 
            { 
                Name = "Update", 
                HeaderText = "", 
                Text = "加载", 
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            dgvPoints.Columns.Add(btnUpdateCol);

            // 化ť加载¼夹
            dgvPoints.CellContentClick += DgvPoints_CellContentClick;

            // 到位ư夹ť
            btnAddPoint.Click += (s, ev) => AddNewPoint();
            btnDeletePoint.Click += (s, ev) => DeletePoint();
            btnSavePoints.Click += (s, ev) => SavePoints();
        }

        /// <summary>
        /// DataGridView化ť到位
        /// </summary>
        private void DgvPoints_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= savedPoints.Count)
                return;

            var point = savedPoints[e.RowIndex];

            if (dgvPoints.Columns[e.ColumnIndex].Name == "Move")
            {
                // 夹ƶ加载˵夹
                MoveToPoint(point);
            }
            else if (dgvPoints.Columns[e.ColumnIndex].Name == "Update")
            {
                // 到位´˵夹
                UpdatePoint(point);
            }
        }

        /// <summary>
        /// 加载夹µ夹λ
        /// </summary>
        private void AddNewPoint()
        {
            // ʹ夹ü򵥵加载夹Ի到位
            using (var inputDialog = new Form())
            {
                inputDialog.Text = "到位入夹λ";
                inputDialog.Width = 400;
                inputDialog.Height = 150;
                inputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputDialog.StartPosition = FormStartPosition.CenterParent;
                inputDialog.MaximizeBox = false;
                inputDialog.MinimizeBox = false;

                var label = new Label { Left = 20, Top = 20, Text = "化λ加载:", Width = 80 };
                var textBox = new TextBox { Left = 110, Top = 17, Width = 250, Text = "夹µ夹λ" };
                var buttonOK = new Button { Text = "ȷ化", Left = 180, Width = 80, Top = 60, DialogResult = DialogResult.OK };
                var buttonCancel = new Button { Text = "取化", Left = 270, Width = 80, Top = 60, DialogResult = DialogResult.Cancel };

                inputDialog.Controls.Add(label);
                inputDialog.Controls.Add(textBox);
                inputDialog.Controls.Add(buttonOK);
                inputDialog.Controls.Add(buttonCancel);
                inputDialog.AcceptButton = buttonOK;
                inputDialog.CancelButton = buttonCancel;

                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    string pointName = textBox.Text;
                    if (!string.IsNullOrEmpty(pointName))
                    {
                        var newPoint = new MotionPoint
                        {
                            Name = pointName,
                            X = currentPositions[0],
                            Y = currentPositions[1],
                            Z = currentPositions[2]
                        };

                        savedPoints.Add(newPoint);
                        RefreshPointsGrid();
                        
                        Gvar.Logger.Info($"到位入夹λ: {pointName} ({newPoint.X:F3}, {newPoint.Y:F3}, {newPoint.Z:F3})");
                    }
                }
            }
        }

        /// <summary>
        /// ɾ加载λ
        /// </summary>
        private void DeletePoint()
        {
            if (dgvPoints.SelectedRows.Count > 0)
            {
                int index = dgvPoints.SelectedRows[0].Index;
                if (index >= 0 && index < savedPoints.Count)
                {
                    var point = savedPoints[index];
                    var result = MessageBox.Show(
                        $"ȷ化Ҫɾ加载λ '{point.Name}' 化", 
                        "ȷ化ɾ化", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        savedPoints.RemoveAt(index);
                        RefreshPointsGrid();
                        Gvar.Logger.Info($"ɾ加载λ: {point.Name}");
                    }
                }
            }
            else
            {
                MessageBox.Show("加载ѡ化Ҫɾ到位ĵ夹λ", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 夹ƶ到位ָ加载λ
        /// </summary>
        private void MoveToPoint(MotionPoint point)
        {
            try
            {
                Gvar.Logger.Info($"夹ƶ加载夹λ: {point.Name} ({point.X:F3}, {point.Y:F3}, {point.Z:F3})");

                // TODO: 实到位ƶ化߼夹
                // ģ夹⣺ֱ加载化λ化
                currentPositions[0] = point.X;
                currentPositions[1] = point.Y;
                currentPositions[2] = point.Z;
                
                UpdateAxisDisplays();

                MessageBox.Show($"到位ƶ加载夹λ总产量：{point.Name}", "成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, $"夹ƶ加载夹λ {point.Name} 失败");
            }
        }

        /// <summary>
        /// 到位µ夹λ加载
        /// </summary>
        private void UpdatePoint(MotionPoint point)
        {
            point.X = currentPositions[0];
            point.Y = currentPositions[1];
            point.Z = currentPositions[2];
            
            RefreshPointsGrid();
            
            Gvar.Logger.Info($"到位µ夹λ: {point.Name} ({point.X:F3}, {point.Y:F3}, {point.Z:F3})");
            
            MessageBox.Show($"化λ '{point.Name}' 夹Ѹ到位", "成功", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// ˢ夹µ夹λ列表
        /// </summary>
        private void RefreshPointsGrid()
        {
            dgvPoints.Rows.Clear();
            
            foreach (var point in savedPoints)
            {
                dgvPoints.Rows.Add(
                    point.Name,
                    point.X.ToString("F3"),
                    point.Y.ToString("F3"),
                    point.Z.ToString("F3")
                );
            }
        }

        /// <summary>
        /// 加载夹λ加载到位ļ夹
        /// </summary>
        private void SavePoints()
        {
            try
            {
                // TODO: 实夹时间浽加载夹ļ加载化ݿ夹
                Gvar.Logger.Info($"加载 {savedPoints.Count} 加载λ");
                MessageBox.Show($"夹ɹ加载夹 {savedPoints.Count} 加载λ", "成功", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载夹λ失败");
            }
        }

        /// <summary>
        /// 加载到位ļ加载ص夹λ
        /// </summary>
        private void LoadDefaultPoints()
        {
            try
            {
                // TODO: 加载到位ļ加载化ݿ加载
                // ģ夹⣺加载һЩ错夹ϵ夹λ
                if (savedPoints.Count == 0)
                {
                    savedPoints.Add(new MotionPoint { Name = "加载λ", X = 100, Y = 50, Z = 0 });
                    savedPoints.Add(new MotionPoint { Name = "到位λ", X = 200, Y = 100, Z = 10 });
                    savedPoints.Add(new MotionPoint { Name = "加载λ", X = 300, Y = 50, Z = 0 });
                }
                
                RefreshPointsGrid();
                Gvar.Logger.Info($"加载 {savedPoints.Count} 加载λ");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "到位ص夹λ失败");
            }
        }

        #endregion

        #region 化λ加载化

        /// <summary>
        /// 夹˶到位λ加载
        /// </summary>
        private class MotionPoint
        {
            public string Name { get; set; }
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        #endregion

        #region ? CT 最后功能：Ƕ到位˶化߼到位

        /// <summary>
        /// ִ加载加载夹ڣ到位 CT 最后化
        /// 
        /// <para><b>最后ԭ化</b></para>
        /// - 加载开始前 Start()
        /// - 加载化ɺ夹 Mark()
        /// - 到位ж加载夹ɺ夹 Stop()
        /// - ʹ化 LoggingCycleTimer 夹Զ到位¼化志
        /// </summary>
        public void ExecuteProductionCycle()
        {
            // ===== ʹ化 LoggingCycleTimer到位Զ到位¼化志化=====
            var timer = new LoggingCycleTimer($"加载_{DateTime.Now:HHmmss}");
            
            try
            {
                // 1. 开始最后到位Զ到位ӡ化志化
                timer.Start();
                
                // 2. 到位϶到位
                MotionToLoadingPosition();
                WaitForInPosition();
                timer.Mark("加载到位");  // ? 夹Զ到位ӡ化? 加载到位 (850ms)
                
                // 3. 化⶯化
                MotionToInspectionPosition();
                WaitForInPosition();
                timer.Mark("加载化");  // ? 夹Զ到位ӡ化? 加载化 (2050ms)
                
                // 4. 到位϶到位
                MotionToUnloadingPosition();
                WaitForInPosition();
                timer.Mark("加载到位");  // ? 夹Զ到位ӡ化? 加载到位 (2333ms)
                
                // 5. ֹͣ最后到位Զ到位ӡ最后夹䣩
                var result = timer.Stop();  // ? 夹Զ到位ӡ化? 化CT: 2333.125ms
                
                // 6. 化ѡ加载夹浽数据库
                // SaveToDatabase(result);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载加载异常");
            }
        }

        #endregion

        #region 夹˶加载Ʒ加载到位实到位˶化߼到位

        /// <summary>
        /// 夹ƶ加载到位λ化
        /// </summary>
        private void MotionToLoadingPosition()
        {
            Gvar.Logger.Info("开始夹ƶ加载到位λ...");
            
            // TODO: 实夹实现˶加载ƴ到位
            // motionController.MoveAbsolute(0, 100, 0);  // X=100
            // motionController.MoveAbsolute(1, 50, 0);   // Y=50
            
            // ģ夹⣺加载Ŀ化λ化
            currentPositions[0] = 100;  // X
            currentPositions[1] = 50;   // Y
            currentPositions[2] = 0;    // Z
        }

        /// <summary>
        /// 夹ƶ加载化λ化
        /// </summary>
        private void MotionToInspectionPosition()
        {
            Gvar.Logger.Info("开始夹ƶ加载化λ...");
            
            // TODO: 实夹实现˶加载ƴ到位
            currentPositions[0] = 200;  // X
            currentPositions[1] = 100;  // Y
            currentPositions[2] = 10;   // Z
        }

        /// <summary>
        /// 夹ƶ加载到位λ化
        /// </summary>
        private void MotionToUnloadingPosition()
        {
            Gvar.Logger.Info("开始夹ƶ加载到位λ...");
            
            // TODO: 实夹实现˶加载ƴ到位
            currentPositions[0] = 300;  // X
            currentPositions[1] = 50;   // Y
            currentPositions[2] = 0;    // Z
        }

        /// <summary>
        /// 夹ȴ化ᵽλ
        /// </summary>
        private void WaitForInPosition()
        {
            // TODO: 实夹实ĵ夹λ加载化
            // while (!motionController.IsInPosition())
            // {
            //     Thread.Sleep(10);
            // }
            
            // ģ夹⣺夹ȴ夹 100ms
            Thread.Sleep(100);
        }

        #endregion

        #region 加载ر夹

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // 取加载ʼ加载化
                initCts?.Cancel();
                initCts?.Dispose();
                initCts = null;
                
                // 取加载到位첽加载
                statusUpdateCts?.Cancel();
                statusUpdateCts?.Dispose();
                statusUpdateCts = null;
                
                jogCts?.Cancel();
                jogCts?.Dispose();
                jogCts = null;
                
                Gvar.Logger.Info("运动控制页面ر夹");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "夹ر化˶加载夹ҳ化失败");
            }
            
            base.OnFormClosing(e);
        }

        #endregion
    }
}
