using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using COTUI.通用功能类;
using COTUI.扫码管理;

namespace COTUI.扫码管理
{
    /// <summary>
    /// 条码扫描页面
    /// 
    /// <para><b>功能：</b></para>
    /// - 支持3种扫码模式：自动/加载/手动输入
    /// - SN格式验证
    /// - 扫码历史记录
    /// - 实时日志提示
    /// </summary>
    public partial class BarcodeScanPage : Form
    {
        #region 字段

        private Logger logger = Logger.GetInstance();
        private BarcodeScanner scanner = BarcodeScanner.Instance;
        private ProductSNManager snManager = ProductSNManager.Instance;

        private Queue<string> logQueue = new Queue<string>();
        private const int MAX_LOG_LINES = 50;

        #endregion

        #region 构造函数

        public BarcodeScanPage()
        {
            InitializeComponent();
            InitializeEvents();
            InitializeScanModes();
            
            Gvar.Logger.Log(LogLevel.Info, "条码扫描页面创建完成");
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitializeEvents()
        {
            // 加载ɨ到位¼夹
            scanner.OnBarcodeScanned += Scanner_OnBarcodeScanned;
            scanner.OnScanError += Scanner_OnScanError;

            // 夹󶨰夹ť事件
            btnManualInput.Click += BtnManualInput_Click;
            btnClearLog.Click += BtnClearLog_Click;
            btnTestSN.Click += BtnTestSN_Click;
            
            // 到位ü化̼到位
            this.KeyPreview = true;
            this.KeyPress += BarcodeScanPage_KeyPress;
            
            // ģ式夹л化¼夹
            cmbScanMode.SelectedIndexChanged += CmbScanMode_SelectedIndexChanged;
        }

        /// <summary>
        /// 开始化ɨ化ģ式
        /// </summary>
        private void InitializeScanModes()
        {
            cmbScanMode.Items.Clear();
            cmbScanMode.Items.Add("加载ģ化ģ式到位Ƽ到位");
            cmbScanMode.Items.Add("加载ͨ化ģ式");
            cmbScanMode.Items.Add("夹ֶ加载夹ģ式加载夹ԣ夹");
            cmbScanMode.SelectedIndex = 2; // 错到位ֶ加载夹ģ式
        }

        #endregion

        #region ҳ加载夹

        private void BarcodeScanPage_Load(object sender, EventArgs e)
        {
            try
            {
                // 错夹ϳ夹ʼ化为夹ֶ加载夹ģ式
                scanner.Initialize(BarcodeScanner.ScanMode.Manual);
                UpdateStatus("夹ѳ夹ʼ加载夹ֶ加载夹ģ式");
                AddLog("? 加载ɨ加载开始化成功");
                AddLog("?? 提示加载到位Ϸ夹ѡ化ɨ化ģ式");
                
                Gvar.Logger.Log(LogLevel.Info, "加载ɨ化ҳ加载加载");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载ɨ化ҳ加载夹失败");
                MessageBox.Show($"ҳ加载夹ʧ夹ܣ夹{ex.Message}", "加载", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ɨ化ģ式夹л夹

        /// <summary>
        /// ɨ化ģ式夹л夹
        /// </summary>
        private void CmbScanMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BarcodeScanner.ScanMode mode;
                
                switch (cmbScanMode.SelectedIndex)
                {
                    case 0: // 加载ģ化
                        mode = BarcodeScanner.ScanMode.Keyboard;
                        UpdateStatus("到位л加载到位ģ化ģ式");
                        AddLog("? 夹л加载到位ģ化ģ式");
                        AddLog("?? 夹뽫化꽹加载ڴ加载ϣ夹Ȼ化ɨ加载化");
                        break;

                    case 1: // 加载ͨ化
                        mode = BarcodeScanner.ScanMode.SerialPort;
                        UpdateStatus("到位л加载到位ͨ化ģ式");
                        AddLog("? 夹л加载到位ͨ化ģ式");
                        AddLog("?? 化ȷ化ɨ化ǹ加载夹Ӵ到位");
                        break;

                    case 2: // 夹ֶ加载夹
                    default:
                        mode = BarcodeScanner.ScanMode.Manual;
                        UpdateStatus("到位л加载ֶ加载夹ģ式");
                        AddLog("? 夹л加载ֶ加载夹ģ式");
                        AddLog("?? 加载夹·化ֶ加载夹SN到位в到位");
                        break;
                }

                // 开始化ɨ加载
                scanner.Initialize(mode);
                
                Gvar.Logger.Log(LogLevel.Info, $"ɨ化ģ式夹л夹: {mode}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "夹л夹ɨ化ģ式失败");
                MessageBox.Show($"夹л夹ʧ夹ܣ夹{ex.Message}", "加载", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ɨ到位¼加载夹

        /// <summary>
        /// ɨ夹成功事件
        /// </summary>
        private void Scanner_OnBarcodeScanned(string barcode)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(Scanner_OnBarcodeScanned), barcode);
                    return;
                }

                // 提示到位ɨ到位SN
                lblLastSN.Text = barcode;
                lblLastScanTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // 化֤SN化式
                bool isValid = scanner.ValidateAppleSN(barcode);
                string validationResult = isValid ? "? 化式化ȷ" : "? 化式加载";

                // 到位SN夹Ƿ化ظ夹
                bool exists = snManager.IsSNExists(barcode);
                string existsResult = exists ? "? SN夹Ѵ到位" : "? SN加载";

                // 到位入到位ʷ列表
                int rowIndex = dgvHistory.Rows.Add(
                    DateTime.Now.ToString("HH:mm:ss"),
                    barcode,
                    validationResult,
                    existsResult
                );

                // 加载加载ɫ
                if (exists)
                {
                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                }
                else if (!isValid)
                {
                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }

                // 加载化ʷ化¼加载
                while (dgvHistory.Rows.Count > 100)
                {
                    dgvHistory.Rows.RemoveAt(0);
                }

                // 化¼化志
                AddLog($"?? ɨ夹赽: {barcode}");
                if (!isValid)
                {
                    AddLog($"   ? 化式到位󣨱夹׼化式化12λ加载C02XXXXXXXXXXX化");
                }
                if (exists)
                {
                    AddLog($"   ? SN夹Ѵ加载加载ݿ夹");
                }

                Gvar.Logger.Log(LogLevel.Info, $"ɨ夹成功: {barcode}, 化式={isValid}, 夹ظ夹={exists}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载ɨ加载失败");
            }
        }

        /// <summary>
        /// ɨ化ʧ到位¼夹
        /// </summary>
        private void Scanner_OnScanError(string error)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(Scanner_OnScanError), error);
                    return;
                }

                AddLog($"? 加载: {error}");
                Gvar.Logger.Log(LogLevel.Error, $"ɨ加载夹: {error}");
            }
            catch { }
        }

        #endregion

        #region 加载夹¼加载到位ģ化ģ式化

        /// <summary>
        /// 到位̰加载¼夹
        /// </summary>
        private void BarcodeScanPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // 到位ݸ夹ɨ加载加载
                scanner.ProcessKeyPress(e.KeyChar);
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "加载加载加载失败");
            }
        }

        #endregion

        #region 夹ֶ加载夹

        /// <summary>
        /// 夹ֶ加载밴ť到位
        /// </summary>
        private void BtnManualInput_Click(object sender, EventArgs e)
        {
            try
            {
                string sn = txtManualInput.Text.Trim();

                if (string.IsNullOrEmpty(sn))
                {
                    MessageBox.Show("加载化SN", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManualInput.Focus();
                    return;
                }

                // 夹ֶ加载夹ɨ化
                scanner.ManualInput(sn);

                // 加载加载
                txtManualInput.Clear();
                txtManualInput.Focus();
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "夹ֶ加载夹失败");
                MessageBox.Show($"加载ʧ夹ܣ夹{ex.Message}", "加载", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 到位ɲ到位SN化ť到位
        /// </summary>
        private void BtnTestSN_Click(object sender, EventArgs e)
        {
            try
            {
                // 到位ɲ到位SN
                string testSN = snManager.GenerateTestSN();
                
                // 化䵽加载夹
                txtManualInput.Text = testSN;
                txtManualInput.Focus();
                txtManualInput.SelectAll();

                AddLog($"?? 到位ɲ到位SN: {testSN}");
                Gvar.Logger.Log(LogLevel.Debug, $"到位ɲ到位SN: {testSN}");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "到位ɲ到位SN失败");
            }
        }

        #endregion

        #region 化志加载

        /// <summary>
        /// 加载化志
        /// </summary>
        private void AddLog(string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(AddLog), message);
                    return;
                }

                string logLine = $"[{DateTime.Now:HH:mm:ss}] {message}";
                
                logQueue.Enqueue(logLine);

                // 加载化志加载
                while (logQueue.Count > MAX_LOG_LINES)
                {
                    logQueue.Dequeue();
                }

                // 加载提示
                txtLog.Text = string.Join(Environment.NewLine, logQueue);

                // 夹Զ加载加载ײ夹
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
            catch { }
        }

        /// <summary>
        /// 加载夹志化ť
        /// </summary>
        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            try
            {
                logQueue.Clear();
                txtLog.Clear();
                AddLog("? 化志加载夹");
            }
            catch { }
        }

        /// <summary>
        /// 加载状态提示
        /// </summary>
        private void UpdateStatus(string status)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action<string>(UpdateStatus), status);
                    return;
                }

                lblStatus.Text = status;
            }
            catch { }
        }

        #endregion

        #region 加载ر夹

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                // 取加载到位¼夹
                scanner.OnBarcodeScanned -= Scanner_OnBarcodeScanned;
                scanner.OnScanError -= Scanner_OnScanError;

                Gvar.Logger.Log(LogLevel.Info, "加载ɨ化ҳ化ر夹");
            }
            catch (Exception ex)
            {
                Gvar.Logger.ErrorException(ex, "夹ر加载夹ɨ化ҳ化失败");
            }

            base.OnFormClosing(e);
        }

        #endregion
    }
}
