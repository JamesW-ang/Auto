using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using COTUI.通用功能类;
using COTUI.通用功能类.Hardware;

namespace COTUI.TestForms
{
    /// <summary>
    /// 硬件设备测试窗体
    /// </summary>
    public partial class HardwareTestForm : System.Windows.Forms.Form
    {
        private ModbusTcpDevice modbusDevice;
        private GenericTcpDevice genericDevice;

        public HardwareTestForm()
        {
            InitializeComponent();
            InitializeDevices();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form
            this.ClientSize = new Size(800, 600);
            this.Name = "HardwareTestForm";
            this.Text = "硬件设备测试";
            this.StartPosition = FormStartPosition.CenterScreen;

            // TabControl
            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 9F)
            };

            // Modbus Tab
            var modbusTab = new TabPage("Modbus TCP");
            CreateModbusTab(modbusTab);
            tabControl.TabPages.Add(modbusTab);

            // Generic TCP Tab
            var genericTab = new TabPage("通用TCP");
            CreateGenericTab(genericTab);
            tabControl.TabPages.Add(genericTab);

            this.Controls.Add(tabControl);
            this.ResumeLayout(false);
        }

        #region Modbus Tab

        private TextBox txtModbusIP, txtModbusPort, txtSlaveAddr, txtStartAddr, txtCount;
        private Button btnModbusConnect, btnModbusDisconnect, btnReadCoils, btnReadRegisters, btnWriteCoil, btnWriteRegister;
        private TextBox txtModbusLog;
        private Label lblModbusStatus;

        private void CreateModbusTab(TabPage tab)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            int y = 10;

            // 连接配置
            AddLabel(panel, "IP地址:", 10, y);
            txtModbusIP = AddTextBox(panel, "192.168.1.10", 100, y, 150);
            AddLabel(panel, "端口:", 260, y);
            txtModbusPort = AddTextBox(panel, "502", 320, y, 80);
            
            btnModbusConnect = AddButton(panel, "连接", 420, y, 80, async (s, e) => await ConnectModbus());
            btnModbusDisconnect = AddButton(panel, "断开", 510, y, 80, async (s, e) => await DisconnectModbus());
            
            lblModbusStatus = new Label { Location = new Point(610, y + 5), Size = new Size(150, 20), Text = "未连接", ForeColor = Color.Red };
            panel.Controls.Add(lblModbusStatus);

            y += 40;

            // 读写参数
            AddLabel(panel, "从站地址:", 10, y);
            txtSlaveAddr = AddTextBox(panel, "1", 100, y, 80);
            
            AddLabel(panel, "起始地址:", 190, y);
            txtStartAddr = AddTextBox(panel, "0", 280, y, 80);
            
            AddLabel(panel, "数量:", 370, y);
            txtCount = AddTextBox(panel, "10", 440, y, 80);

            y += 40;

            // 读取按钮
            btnReadCoils = AddButton(panel, "读取线圈", 10, y, 120, async (s, e) => await ReadCoils());
            btnReadRegisters = AddButton(panel, "读取寄存器", 140, y, 120, async (s, e) => await ReadRegisters());
            btnWriteCoil = AddButton(panel, "写入线圈", 270, y, 120, async (s, e) => await WriteCoil());
            btnWriteRegister = AddButton(panel, "写入寄存器", 400, y, 120, async (s, e) => await WriteRegister());

            y += 40;

            // 日志
            txtModbusLog = new TextBox
            {
                Location = new Point(10, y),
                Size = new Size(760, 400),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 9F)
            };
            panel.Controls.Add(txtModbusLog);

            tab.Controls.Add(panel);
        }

        private async Task ConnectModbus()
        {
            try
            {
                string ip = txtModbusIP.Text.Trim();
                int port = int.Parse(txtModbusPort.Text.Trim());

                modbusDevice = new ModbusTcpDevice("Modbus-PLC", ip, port);
                modbusDevice.ConnectionStatusChanged += (s, connected) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        lblModbusStatus.Text = connected ? "已连接" : "未连接";
                        lblModbusStatus.ForeColor = connected ? Color.Green : Color.Red;
                    }));
                };

                bool success = await modbusDevice.ConnectAsync();
                LogModbus(success ? "连接成功" : "连接失败");
            }
            catch (Exception ex)
            {
                LogModbus($"连接异常: {ex.Message}");
            }
        }

        private async Task DisconnectModbus()
        {
            if (modbusDevice != null)
            {
                await modbusDevice.DisconnectAsync();
                LogModbus("已断开连接");
            }
        }

        private async Task ReadCoils()
        {
            try
            {
                byte slaveAddr = byte.Parse(txtSlaveAddr.Text);
                ushort startAddr = ushort.Parse(txtStartAddr.Text);
                ushort count = ushort.Parse(txtCount.Text);

                bool[] coils = await modbusDevice.ReadCoilsAsync(slaveAddr, startAddr, count);
                
                if (coils != null)
                {
                    LogModbus($"读取线圈成功 [{startAddr}-{startAddr + count - 1}]:");
                    for (int i = 0; i < coils.Length; i++)
                    {
                        LogModbus($"  地址 {startAddr + i}: {(coils[i] ? "ON" : "OFF")}");
                    }
                }
                else
                {
                    LogModbus("读取线圈失败");
                }
            }
            catch (Exception ex)
            {
                LogModbus($"读取异常: {ex.Message}");
            }
        }

        private async Task ReadRegisters()
        {
            try
            {
                byte slaveAddr = byte.Parse(txtSlaveAddr.Text);
                ushort startAddr = ushort.Parse(txtStartAddr.Text);
                ushort count = ushort.Parse(txtCount.Text);

                ushort[] registers = await modbusDevice.ReadHoldingRegistersAsync(slaveAddr, startAddr, count);
                
                if (registers != null)
                {
                    LogModbus($"读取寄存器成功 [{startAddr}-{startAddr + count - 1}]:");
                    for (int i = 0; i < registers.Length; i++)
                    {
                        LogModbus($"  地址 {startAddr + i}: {registers[i]} (0x{registers[i]:X4})");
                    }
                }
                else
                {
                    LogModbus("读取寄存器失败");
                }
            }
            catch (Exception ex)
            {
                LogModbus($"读取异常: {ex.Message}");
            }
        }

        private async Task WriteCoil()
        {
            try
            {
                byte slaveAddr = byte.Parse(txtSlaveAddr.Text);
                ushort addr = ushort.Parse(txtStartAddr.Text);
                
                var result = MessageBox.Show("写入 ON (是) 还是 OFF (否)?", "写入线圈", MessageBoxButtons.YesNo);
                bool value = result == DialogResult.Yes;

                bool success = await modbusDevice.WriteSingleCoilAsync(slaveAddr, addr, value);
                LogModbus($"写入线圈 {addr}: {(value ? "ON" : "OFF")} - {(success ? "成功" : "失败")}");
            }
            catch (Exception ex)
            {
                LogModbus($"写入异常: {ex.Message}");
            }
        }

        private async Task WriteRegister()
        {
            try
            {
                byte slaveAddr = byte.Parse(txtSlaveAddr.Text);
                ushort addr = ushort.Parse(txtStartAddr.Text);
                
                string input = Microsoft.VisualBasic.Interaction.InputBox("请输入要写入的值 (0-65535):", "写入寄存器", "0");
                if (string.IsNullOrEmpty(input)) return;
                
                ushort value = ushort.Parse(input);

                bool success = await modbusDevice.WriteSingleRegisterAsync(slaveAddr, addr, value);
                LogModbus($"写入寄存器 {addr}: {value} - {(success ? "成功" : "失败")}");
            }
            catch (Exception ex)
            {
                LogModbus($"写入异常: {ex.Message}");
            }
        }

        private void LogModbus(string message)
        {
            if (txtModbusLog.InvokeRequired)
            {
                txtModbusLog.Invoke(new Action(() => LogModbus(message)));
                return;
            }

            string log = $"[{DateTime.Now:HH:mm:ss}] {message}\r\n";
            txtModbusLog.AppendText(log);
        }

        #endregion

        #region Generic TCP Tab

        private TextBox txtGenericIP, txtGenericPort, txtGenericCommand;
        private Button btnGenericConnect, btnGenericDisconnect, btnGenericSend;
        private TextBox txtGenericLog;
        private Label lblGenericStatus;

        private void CreateGenericTab(TabPage tab)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            int y = 10;

            // 连接配置
            AddLabel(panel, "IP地址:", 10, y);
            txtGenericIP = AddTextBox(panel, "192.168.1.20", 100, y, 150);
            AddLabel(panel, "端口:", 260, y);
            txtGenericPort = AddTextBox(panel, "3000", 320, y, 80);
            
            btnGenericConnect = AddButton(panel, "连接", 420, y, 80, async (s, e) => await ConnectGeneric());
            btnGenericDisconnect = AddButton(panel, "断开", 510, y, 80, async (s, e) => await DisconnectGeneric());
            
            lblGenericStatus = new Label { Location = new Point(610, y + 5), Size = new Size(150, 20), Text = "未连接", ForeColor = Color.Red };
            panel.Controls.Add(lblGenericStatus);

            y += 40;

            // 发送命令
            AddLabel(panel, "命令:", 10, y);
            txtGenericCommand = AddTextBox(panel, "TRIGGER\\r\\n", 100, y, 400);
            btnGenericSend = AddButton(panel, "发送", 510, y, 80, async (s, e) => await SendGenericCommand());

            y += 40;

            // 日志
            txtGenericLog = new TextBox
            {
                Location = new Point(10, y),
                Size = new Size(760, 450),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                Font = new Font("Consolas", 9F)
            };
            panel.Controls.Add(txtGenericLog);

            tab.Controls.Add(panel);
        }

        private async Task ConnectGeneric()
        {
            try
            {
                string ip = txtGenericIP.Text.Trim();
                int port = int.Parse(txtGenericPort.Text.Trim());

                genericDevice = new GenericTcpDevice("Generic-Device", ip, port);
                genericDevice.ConnectionStatusChanged += (s, connected) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        lblGenericStatus.Text = connected ? "已连接" : "未连接";
                        lblGenericStatus.ForeColor = connected ? Color.Green : Color.Red;
                    }));
                };
                
                genericDevice.TextReceived += (s, text) =>
                {
                    LogGeneric($"← 接收: {text}");
                };

                bool success = await genericDevice.ConnectAsync();
                LogGeneric(success ? "连接成功" : "连接失败");
            }
            catch (Exception ex)
            {
                LogGeneric($"连接异常: {ex.Message}");
            }
        }

        private async Task DisconnectGeneric()
        {
            if (genericDevice != null)
            {
                await genericDevice.DisconnectAsync();
                LogGeneric("已断开连接");
            }
        }

        private async Task SendGenericCommand()
        {
            try
            {
                string command = txtGenericCommand.Text.Replace("\\r", "\r").Replace("\\n", "\n");
                bool success = await genericDevice.SendTextAsync(command);
                LogGeneric($"→ 发送: {command.Replace("\r", "\\r").Replace("\n", "\\n")} - {(success ? "成功" : "失败")}");
            }
            catch (Exception ex)
            {
                LogGeneric($"发送异常: {ex.Message}");
            }
        }

        private void LogGeneric(string message)
        {
            if (txtGenericLog.InvokeRequired)
            {
                txtGenericLog.Invoke(new Action(() => LogGeneric(message)));
                return;
            }

            string log = $"[{DateTime.Now:HH:mm:ss}] {message}\r\n";
            txtGenericLog.AppendText(log);
        }

        #endregion

        #region 辅助方法

        private void InitializeDevices()
        {
            // 初始化时什么都不做
        }

        private Label AddLabel(Panel panel, string text, int x, int y)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y + 5),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            panel.Controls.Add(label);
            return label;
        }

        private TextBox AddTextBox(Panel panel, string text, int x, int y, int width)
        {
            var textBox = new TextBox
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 25)
            };
            panel.Controls.Add(textBox);
            return textBox;
        }

        private Button AddButton(Panel panel, string text, int x, int y, int width, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 28)
            };
            button.Click += onClick;
            panel.Controls.Add(button);
            return button;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            modbusDevice?.DisconnectAsync().Wait();
            genericDevice?.DisconnectAsync().Wait();
        }

        #endregion
    }
}
