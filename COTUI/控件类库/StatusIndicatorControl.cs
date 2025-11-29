using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace COTUI.控件类库
{
    /// <summary>
    /// 状态指示器控件 - 包含3D圆形指示灯和可点击Label
    /// </summary>
    [ToolboxItem(true)]
    [DefaultEvent("StatusChanged")]
    public class StatusIndicatorControl : UserControl
    {
        private bool isActive = false;
        private string labelText = "状态";
        private Color activeColor = Color.LimeGreen;
        private Color inactiveColor = Color.Red;
        private int indicatorSize = 30;
        private Font labelFont = new Font("微软雅黑", 10, FontStyle.Bold);

        private Panel indicatorPanel;
        private Label statusLabel;

        /// <summary>
        /// 状态改变事件
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        public StatusIndicatorControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.UserPaint, true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // 创建指示灯面板
            indicatorPanel = new Panel
            {
                Size = new Size(indicatorSize + 10, indicatorSize + 10),
                Location = new Point(5, 5),
                BackColor = Color.Transparent
            };
            indicatorPanel.Paint += IndicatorPanel_Paint;

            // 创建状态标签
            statusLabel = new Label
            {
                Text = labelText,
                AutoSize = true,
                Location = new Point(indicatorSize + 20, 10),
                Font = labelFont,
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };
            statusLabel.Click += StatusLabel_Click;
            statusLabel.MouseEnter += StatusLabel_MouseEnter;
            statusLabel.MouseLeave += StatusLabel_MouseLeave;

            // 设置控件
            this.MinimumSize = new Size(100, indicatorSize + 20);
            this.Size = new Size(150, indicatorSize + 20);
            this.BackColor = Color.Transparent;
            this.AutoScaleMode = AutoScaleMode.None;  // 禁用自动缩放
            this.Controls.Add(indicatorPanel);
            this.Controls.Add(statusLabel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// 绘制3D圆形指示灯
        /// </summary>
        private void IndicatorPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 计算圆形位置
            int x = 5;
            int y = 5;
            int size = indicatorSize;

            // 选择颜色
            Color baseColor = isActive ? activeColor : inactiveColor;

            // 绘制外阴影
            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddEllipse(x + 2, y + 2, size, size);
                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb(100, Color.Black);
                    shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            // 绘制主体 - 渐变效果
            using (GraphicsPath mainPath = new GraphicsPath())
            {
                mainPath.AddEllipse(x, y, size, size);
                using (LinearGradientBrush mainBrush = new LinearGradientBrush(
                    new Rectangle(x, y, size, size),
                    LightenColor(baseColor, 0.3f),
                    DarkenColor(baseColor, 0.2f),
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillPath(mainBrush, mainPath);
                }
            }

            // 绘制高光效果
            using (GraphicsPath highlightPath = new GraphicsPath())
            {
                int highlightSize = (int)(size * 0.4);
                int highlightX = x + (int)(size * 0.25);
                int highlightY = y + (int)(size * 0.15);
                
                highlightPath.AddEllipse(highlightX, highlightY, highlightSize, highlightSize);
                using (PathGradientBrush highlightBrush = new PathGradientBrush(highlightPath))
                {
                    highlightBrush.CenterColor = Color.FromArgb(200, Color.White);
                    highlightBrush.SurroundColors = new Color[] { Color.Transparent };
                    g.FillPath(highlightBrush, highlightPath);
                }
            }

            // 绘制边框
            using (Pen borderPen = new Pen(DarkenColor(baseColor, 0.4f), 2))
            {
                g.DrawEllipse(borderPen, x, y, size, size);
            }

            // 如果激活状态，添加发光效果
            if (isActive)
            {
                using (GraphicsPath glowPath = new GraphicsPath())
                {
                    glowPath.AddEllipse(x - 3, y - 3, size + 6, size + 6);
                    using (PathGradientBrush glowBrush = new PathGradientBrush(glowPath))
                    {
                        glowBrush.CenterColor = Color.FromArgb(100, baseColor);
                        glowBrush.SurroundColors = new Color[] { Color.Transparent };
                        g.FillPath(glowBrush, glowPath);
                    }
                }
            }
        }

        /// <summary>
        /// 标签点击事件
        /// </summary>
        private void StatusLabel_Click(object sender, EventArgs e)
        {
            // 切换状态
            IsActive = !isActive;
        }

        /// <summary>
        /// 鼠标进入标签
        /// </summary>
        private void StatusLabel_MouseEnter(object sender, EventArgs e)
        {
            statusLabel.ForeColor = Color.Blue;
        }

        /// <summary>
        /// 鼠标离开标签
        /// </summary>
        private void StatusLabel_MouseLeave(object sender, EventArgs e)
        {
            statusLabel.ForeColor = Color.Black;
        }

        /// <summary>
        /// 颜色加亮
        /// </summary>
        private Color LightenColor(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                Math.Min(255, (int)(color.R + (255 - color.R) * factor)),
                Math.Min(255, (int)(color.G + (255 - color.G) * factor)),
                Math.Min(255, (int)(color.B + (255 - color.B) * factor))
            );
        }

        /// <summary>
        /// 颜色变暗
        /// </summary>
        private Color DarkenColor(Color color, float factor)
        {
            return Color.FromArgb(
                color.A,
                (int)(color.R * (1 - factor)),
                (int)(color.G * (1 - factor)),
                (int)(color.B * (1 - factor))
            );
        }

        #region 公共属性

        /// <summary>
        /// 获取或设置指示器状态
        /// </summary>
        [Category("状态")]
        [Description("获取或设置指示器的激活状态")]
        [DefaultValue(false)]
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    bool oldValue = isActive;
                    isActive = value;
                    
                    // 强制重绘并刷新
                    if (indicatorPanel != null)
                    {
                        if (indicatorPanel.InvokeRequired)
                        {
                            indicatorPanel.Invoke(new Action(() =>
                            {
                                indicatorPanel.Invalidate();
                                indicatorPanel.Update();
                            }));
                        }
                        else
                        {
                            indicatorPanel.Invalidate();
                            indicatorPanel.Update();
                        }
                    }
                    
                    // 触发状态改变事件
                    OnStatusChanged(new StatusChangedEventArgs(oldValue, isActive));
                }
            }
        }

        /// <summary>
        /// 获取或设置标签文本
        /// </summary>
        [Category("外观")]
        [Description("获取或设置标签显示的文本")]
        [DefaultValue("状态")]
        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                if (statusLabel != null)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke(new Action(() => statusLabel.Text = value));
                    }
                    else
                    {
                        statusLabel.Text = value;
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置激活状态颜色
        /// </summary>
        [Category("外观")]
        [Description("获取或设置激活状态时的颜色")]
        public Color ActiveColor
        {
            get { return activeColor; }
            set
            {
                activeColor = value;
                if (isActive && indicatorPanel != null)
                {
                    if (indicatorPanel.InvokeRequired)
                    {
                        indicatorPanel.Invoke(new Action(() =>
                        {
                            indicatorPanel.Invalidate();
                            indicatorPanel.Update();
                        }));
                    }
                    else
                    {
                        indicatorPanel.Invalidate();
                        indicatorPanel.Update();
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置非激活状态颜色
        /// </summary>
        [Category("外观")]
        [Description("获取或设置非激活状态时的颜色")]
        public Color InactiveColor
        {
            get { return inactiveColor; }
            set
            {
                inactiveColor = value;
                if (!isActive && indicatorPanel != null)
                {
                    if (indicatorPanel.InvokeRequired)
                    {
                        indicatorPanel.Invoke(new Action(() =>
                        {
                            indicatorPanel.Invalidate();
                            indicatorPanel.Update();
                        }));
                    }
                    else
                    {
                        indicatorPanel.Invalidate();
                        indicatorPanel.Update();
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置指示器大小
        /// </summary>
        [Category("外观")]
        [Description("获取或设置指示器的大小")]
        [DefaultValue(30)]
        public int IndicatorSize
        {
            get { return indicatorSize; }
            set
            {
                indicatorSize = Math.Max(20, Math.Min(100, value));
                if (indicatorPanel != null && statusLabel != null)
                {
                    indicatorPanel.Size = new Size(indicatorSize + 10, indicatorSize + 10);
                    statusLabel.Location = new Point(indicatorSize + 20, (indicatorSize - statusLabel.Height) / 2 + 5);
                    this.Height = indicatorSize + 20;
                    indicatorPanel.Invalidate();
                    indicatorPanel.Update();
                }
            }
        }

        /// <summary>
        /// 获取或设置标签字体
        /// </summary>
        [Category("外观")]
        [Description("获取或设置标签的字体")]
        public Font LabelFont
        {
            get { return labelFont; }
            set
            {
                labelFont = value;
                if (statusLabel != null)
                {
                    if (statusLabel.InvokeRequired)
                    {
                        statusLabel.Invoke(new Action(() => statusLabel.Font = value));
                    }
                    else
                    {
                        statusLabel.Font = value;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 触发状态改变事件
        /// </summary>
        protected virtual void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                statusLabel?.Dispose();
                indicatorPanel?.Dispose();
                labelFont?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// 状态改变事件参数
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        public bool OldStatus { get; }
        public bool NewStatus { get; }

        public StatusChangedEventArgs(bool oldStatus, bool newStatus)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
}
