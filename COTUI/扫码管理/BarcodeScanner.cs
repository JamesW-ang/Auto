using System;
using System.Text;
using System.Threading.Tasks;

namespace COTUI.扫码管理
{
    /// <summary>
    /// 条码扫描器管理类（简化版）
    /// 
    /// <para><b>支持模式：</b></para>
    /// - 键盘模拟模式（USB扫码枪，免驱动）
    /// - 串口通信模式（串口扫码枪）
    /// - 手动输入模式（测试/调试）
    /// </summary>
    public class BarcodeScanner
    {
        #region 单例模式

        private static BarcodeScanner instance;
        private static readonly object lockObj = new object();

        public static BarcodeScanner Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new BarcodeScanner();
                        }
                    }
                }
                return instance;
            }
        }

        private BarcodeScanner() { }

        #endregion

        #region 枚举

        /// <summary>
        /// 扫码模式
        /// </summary>
        public enum ScanMode
        {
            /// <summary>键盘模拟模式</summary>
            Keyboard,
            /// <summary>串口通信模式</summary>
            SerialPort,
            /// <summary>手动输入模式</summary>
            Manual
        }

        #endregion

        #region 字段

        private ScanMode currentMode = ScanMode.Manual;
        private StringBuilder keyBuffer = new StringBuilder();
        private System.Timers.Timer keyTimer;

        #endregion

        #region 事件

        /// <summary>
        /// 扫码成功事件
        /// </summary>
        public event Action<string> OnBarcodeScanned;

        /// <summary>
        /// 扫码失败事件
        /// </summary>
        public event Action<string> OnScanError;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化扫码器
        /// </summary>
        public void Initialize(ScanMode mode)
        {
            currentMode = mode;

            switch (mode)
            {
                case ScanMode.Keyboard:
                    InitializeKeyboardMode();
                    break;

                case ScanMode.SerialPort:
                    InitializeSerialPortMode();
                    break;

                case ScanMode.Manual:
                    // 手动模式无需初始化
                    break;
            }
        }

        /// <summary>
        /// 初始化键盘模式
        /// </summary>
        private void InitializeKeyboardMode()
        {
            keyBuffer.Clear();
            
            // 创建超时计时器
            if (keyTimer == null)
            {
                keyTimer = new System.Timers.Timer(100); // 100ms超时
                keyTimer.Elapsed += (s, e) =>
                {
                    if (keyBuffer.Length > 0)
                    {
                        ProcessBuffer();
                    }
                };
                keyTimer.AutoReset = false;
            }
        }

        /// <summary>
        /// 初始化串口模式
        /// </summary>
        private void InitializeSerialPortMode()
        {
            // TODO: 实现串口初始化
            // 需要扫码枪硬件才能测试
        }

        #endregion

        #region 键盘模式

        /// <summary>
        /// 处理键盘输入（键盘模式）
        /// </summary>
        public void ProcessKeyPress(char keyChar)
        {
            if (currentMode != ScanMode.Keyboard)
                return;

            if (keyChar == '\r' || keyChar == '\n')  // 回车键
            {
                ProcessBuffer();
            }
            else
            {
                keyBuffer.Append(keyChar);
                keyTimer?.Stop();
                keyTimer?.Start();
            }
        }

        /// <summary>
        /// 处理缓冲区数据
        /// </summary>
        private void ProcessBuffer()
        {
            keyTimer?.Stop();

            if (keyBuffer.Length > 0)
            {
                string barcode = keyBuffer.ToString().Trim().ToUpper();
                keyBuffer.Clear();

                if (!string.IsNullOrEmpty(barcode))
                {
                    OnBarcodeScanned?.Invoke(barcode);
                }
            }
        }

        #endregion

        #region 手动输入

        /// <summary>
        /// 手动输入（测试模式）
        /// </summary>
        public void ManualInput(string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
            {
                OnScanError?.Invoke("SN不能为空");
                return;
            }

            string barcode = sn.Trim().ToUpper();
            OnBarcodeScanned?.Invoke(barcode);
        }

        #endregion

        #region SN验证

        /// <summary>
        /// 验证苹果SN格式
        /// </summary>
        public bool ValidateAppleSN(string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
                return false;

            sn = sn.Trim();

            // 基本格式验证
            if (sn.Length != 12)
                return false;

            // 苹果SN通常以C02开头（iPhone系列）
            if (!sn.StartsWith("C02"))
                return false;

            // 检查字符合法性（只允许字母和数字）
            foreach (char c in sn)
            {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
