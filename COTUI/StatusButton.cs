using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace CustomControls
{
    /// <summary>
    /// 自定义控件：包含一个状态指示灯和一个按钮
    /// </summary>
    public class StatusButton : UserControl
    {
        private LedIndicator? statusLight;
        private Button? actionButton;
        
        /// <summary>
        /// 状态改变事件
        /// </summary>
        public event EventHandler? StatusChanged;
        
        /// <summary>
        /// 获取或设置按钮文本
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ButtonText
        {
            get => actionButton?.Text ?? "";
            set 
            { 
                if (actionButton != null) 
                    actionButton.Text = value; 
            }
        }
        
        /// <summary>
        /// 获取当前状态（true表示激活状态，false表示未激活状态）
        /// </summary>
        public bool IsActive { get; private set; } = false;
        
        public StatusButton()
        {
            InitializeComponent();
            UpdateLightColor();
        }
        
        private void InitializeComponent()
        {
            // 初始化面板布局
            this.Size = new Size(150, 40);
            this.MinimumSize = new Size(100, 30);
            
            // 创建状态指示灯控件
            statusLight = new LedIndicator
            {
                Size = new Size(20, 20),
                Location = new Point(10, 10),
                Active = false
            };
            
            // 创建按钮
            actionButton = new Button
            {
                Text = "Action",
                Size = new Size(80, 30),
                Location = new Point(40, 5),
                UseVisualStyleBackColor = true
            };
            
            // 注册事件
            actionButton.Click += ActionButton_Click;
            
            // 添加控件到用户控件
            this.Controls.Add(statusLight);
            this.Controls.Add(actionButton);
        }
        
        private void ActionButton_Click(object? sender, EventArgs e)
        {
            // 切换状态
            IsActive = !IsActive;
            UpdateLightColor();
            
            // 触发状态改变事件
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void UpdateLightColor()
        {
            // 根据当前状态更新指示灯颜色
            if (statusLight != null)
            {
                statusLight.Active = IsActive;
            }
        }
        
        /// <summary>
        /// 重置状态为未激活
        /// </summary>
        public void ResetStatus()
        {
            IsActive = false;
            UpdateLightColor();
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="active">true为激活状态，false为未激活状态</param>
        public void SetStatus(bool active)
        {
            IsActive = active;
            UpdateLightColor();
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    /// <summary>
    /// 圆形LED指示灯控件
    /// </summary>
    public class LedIndicator : Control
    {
        private bool active = false;
        private Color inactiveColor = Color.Gray;
        private Color activeColor = Color.Green;
        
        public LedIndicator()
        {
            this.Size = new Size(20, 20);
            this.TabStop = false;
        }
        
        /// <summary>
        /// 获取或设置指示灯是否激活
        /// </summary>
        public bool Active
        {
            get { return active; }
            set 
            { 
                active = value;
                this.Invalidate();
            }
        }
        
        /// <summary>
        /// 获取或设置非激活状态下的颜色
        /// </summary>
        public Color InactiveColor
        {
            get { return inactiveColor; }
            set 
            { 
                inactiveColor = value;
                if (!active) this.Invalidate();
            }
        }
        
        /// <summary>
        /// 获取或设置激活状态下的颜色
        /// </summary>
        public Color ActiveColor
        {
            get { return activeColor; }
            set 
            { 
                activeColor = value;
                if (active) this.Invalidate();
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            
            // 获取当前颜色
            Color ledColor = active ? activeColor : inactiveColor;
            
            // 绘制圆形LED
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            
            // 绘制外圈高光
            using (Pen pen = new Pen(Color.FromArgb(200, Color.White), 1))
            {
                g.DrawEllipse(pen, new Rectangle(1, 1, bounds.Width - 2, bounds.Height - 2));
            }
            
            // 绘制主LED
            using (SolidBrush brush = new SolidBrush(ledColor))
            {
                g.FillEllipse(brush, bounds);
            }
            
            // 绘制内圈高光（创建立体效果）
            using (Brush highlightBrush = new SolidBrush(Color.FromArgb(100, Color.White)))
            {
                g.FillEllipse(highlightBrush, new Rectangle(2, 2, this.Width / 3, this.Height / 3));
            }
            
            // 绘制边框
            using (Pen borderPen = new Pen(Color.FromArgb(100, Color.Black)))
            {
                g.DrawEllipse(borderPen, bounds);
            }
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }
    }
}