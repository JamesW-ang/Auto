namespace COTUI.运动控制页面
{
    partial class MotionControlPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpDirectionControl = new System.Windows.Forms.GroupBox();
            this.btnUpLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnUpRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnCenter = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnDownLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnDownRight = new System.Windows.Forms.Button();
            this.grpModeSelection = new System.Windows.Forms.GroupBox();
            this.numStepDistance = new System.Windows.Forms.NumericUpDown();
            this.lblStepDistance = new System.Windows.Forms.Label();
            this.lblModeDescription = new System.Windows.Forms.Label();
            this.rbJog = new System.Windows.Forms.RadioButton();
            this.rbRelative = new System.Windows.Forms.RadioButton();
            this.rbAbsolute = new System.Windows.Forms.RadioButton();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.grpZAxis = new System.Windows.Forms.GroupBox();
            this.btnZClear = new System.Windows.Forms.Button();
            this.btnZHome = new System.Windows.Forms.Button();
            this.btnZMove = new System.Windows.Forms.Button();
            this.txtZTarget = new System.Windows.Forms.TextBox();
            this.lblZTarget = new System.Windows.Forms.Label();
            this.txtZCurrent = new System.Windows.Forms.TextBox();
            this.lblZCurrent = new System.Windows.Forms.Label();
            this.flpZStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.grpYAxis = new System.Windows.Forms.GroupBox();
            this.btnYClear = new System.Windows.Forms.Button();
            this.btnYHome = new System.Windows.Forms.Button();
            this.btnYMove = new System.Windows.Forms.Button();
            this.txtYTarget = new System.Windows.Forms.TextBox();
            this.lblYTarget = new System.Windows.Forms.Label();
            this.txtYCurrent = new System.Windows.Forms.TextBox();
            this.lblYCurrent = new System.Windows.Forms.Label();
            this.flpYStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.grpXAxis = new System.Windows.Forms.GroupBox();
            this.btnXClear = new System.Windows.Forms.Button();
            this.btnXHome = new System.Windows.Forms.Button();
            this.btnXMove = new System.Windows.Forms.Button();
            this.txtXTarget = new System.Windows.Forms.TextBox();
            this.lblXTarget = new System.Windows.Forms.Label();
            this.txtXCurrent = new System.Windows.Forms.TextBox();
            this.lblXCurrent = new System.Windows.Forms.Label();
            this.flpXStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpPointManager = new System.Windows.Forms.GroupBox();
            this.btnSavePoints = new System.Windows.Forms.Button();
            this.btnDeletePoint = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.dgvPoints = new System.Windows.Forms.DataGridView();
            this.tlpMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpDirectionControl.SuspendLayout();
            this.grpModeSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDistance)).BeginInit();
            this.pnlMiddle.SuspendLayout();
            this.grpZAxis.SuspendLayout();
            this.grpYAxis.SuspendLayout();
            this.grpXAxis.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpPointManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlpMain.Controls.Add(this.pnlLeft, 0, 0);
            this.tlpMain.Controls.Add(this.pnlMiddle, 1, 0);
            this.tlpMain.Controls.Add(this.pnlRight, 2, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1200, 800);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpDirectionControl);
            this.pnlLeft.Controls.Add(this.grpModeSelection);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(3, 3);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(294, 794);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpDirectionControl
            // 
            this.grpDirectionControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpDirectionControl.Controls.Add(this.btnUpLeft);
            this.grpDirectionControl.Controls.Add(this.btnUp);
            this.grpDirectionControl.Controls.Add(this.btnUpRight);
            this.grpDirectionControl.Controls.Add(this.btnLeft);
            this.grpDirectionControl.Controls.Add(this.btnCenter);
            this.grpDirectionControl.Controls.Add(this.btnRight);
            this.grpDirectionControl.Controls.Add(this.btnDownLeft);
            this.grpDirectionControl.Controls.Add(this.btnDown);
            this.grpDirectionControl.Controls.Add(this.btnDownRight);
            this.grpDirectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDirectionControl.Location = new System.Drawing.Point(0, 200);
            this.grpDirectionControl.Name = "grpDirectionControl";
            this.grpDirectionControl.Size = new System.Drawing.Size(294, 594);
            this.grpDirectionControl.TabIndex = 1;
            this.grpDirectionControl.TabStop = false;
            this.grpDirectionControl.Text = "方向控制";
            // 
            // btnUpLeft
            // 
            this.btnUpLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUpLeft.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnUpLeft.Location = new System.Drawing.Point(30, 50);
            this.btnUpLeft.Name = "btnUpLeft";
            this.btnUpLeft.Size = new System.Drawing.Size(60, 60);
            this.btnUpLeft.TabIndex = 0;
            this.btnUpLeft.Text = "↖";
            this.btnUpLeft.UseVisualStyleBackColor = false;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUp.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnUp.Location = new System.Drawing.Point(116, 50);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(60, 60);
            this.btnUp.TabIndex = 1;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = false;
            // 
            // btnUpRight
            // 
            this.btnUpRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnUpRight.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnUpRight.Location = new System.Drawing.Point(202, 50);
            this.btnUpRight.Name = "btnUpRight";
            this.btnUpRight.Size = new System.Drawing.Size(60, 60);
            this.btnUpRight.TabIndex = 2;
            this.btnUpRight.Text = "↗";
            this.btnUpRight.UseVisualStyleBackColor = false;
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLeft.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnLeft.Location = new System.Drawing.Point(30, 136);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(60, 60);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "←";
            this.btnLeft.UseVisualStyleBackColor = false;
            // 
            // btnCenter
            // 
            this.btnCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCenter.Enabled = false;
            this.btnCenter.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnCenter.Location = new System.Drawing.Point(116, 136);
            this.btnCenter.Name = "btnCenter";
            this.btnCenter.Size = new System.Drawing.Size(60, 60);
            this.btnCenter.TabIndex = 4;
            this.btnCenter.Text = "●";
            this.btnCenter.UseVisualStyleBackColor = false;
            // 
            // btnRight
            // 
            this.btnRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRight.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnRight.Location = new System.Drawing.Point(202, 136);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(60, 60);
            this.btnRight.TabIndex = 5;
            this.btnRight.Text = "→";
            this.btnRight.UseVisualStyleBackColor = false;
            // 
            // btnDownLeft
            // 
            this.btnDownLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDownLeft.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnDownLeft.Location = new System.Drawing.Point(30, 222);
            this.btnDownLeft.Name = "btnDownLeft";
            this.btnDownLeft.Size = new System.Drawing.Size(60, 60);
            this.btnDownLeft.TabIndex = 6;
            this.btnDownLeft.Text = "↙";
            this.btnDownLeft.UseVisualStyleBackColor = false;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDown.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnDown.Location = new System.Drawing.Point(116, 222);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(60, 60);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = false;
            // 
            // btnDownRight
            // 
            this.btnDownRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDownRight.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.btnDownRight.Location = new System.Drawing.Point(202, 222);
            this.btnDownRight.Name = "btnDownRight";
            this.btnDownRight.Size = new System.Drawing.Size(60, 60);
            this.btnDownRight.TabIndex = 8;
            this.btnDownRight.Text = "↘";
            this.btnDownRight.UseVisualStyleBackColor = false;
            // 
            // grpModeSelection
            // 
            this.grpModeSelection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpModeSelection.Controls.Add(this.numStepDistance);
            this.grpModeSelection.Controls.Add(this.lblStepDistance);
            this.grpModeSelection.Controls.Add(this.lblModeDescription);
            this.grpModeSelection.Controls.Add(this.rbJog);
            this.grpModeSelection.Controls.Add(this.rbRelative);
            this.grpModeSelection.Controls.Add(this.rbAbsolute);
            this.grpModeSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpModeSelection.Location = new System.Drawing.Point(0, 0);
            this.grpModeSelection.Name = "grpModeSelection";
            this.grpModeSelection.Size = new System.Drawing.Size(294, 200);
            this.grpModeSelection.TabIndex = 0;
            this.grpModeSelection.TabStop = false;
            this.grpModeSelection.Text = "运动模式";
            // 
            // numStepDistance
            // 
            this.numStepDistance.DecimalPlaces = 2;
            this.numStepDistance.Location = new System.Drawing.Point(80, 160);
            this.numStepDistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numStepDistance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numStepDistance.Name = "numStepDistance";
            this.numStepDistance.Size = new System.Drawing.Size(120, 27);
            this.numStepDistance.TabIndex = 5;
            this.numStepDistance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblStepDistance
            // 
            this.lblStepDistance.AutoSize = true;
            this.lblStepDistance.Location = new System.Drawing.Point(15, 162);
            this.lblStepDistance.Name = "lblStepDistance";
            this.lblStepDistance.Size = new System.Drawing.Size(81, 20);
            this.lblStepDistance.TabIndex = 4;
            this.lblStepDistance.Text = "步长(mm):";
            // 
            // lblModeDescription
            // 
            this.lblModeDescription.ForeColor = System.Drawing.Color.Blue;
            this.lblModeDescription.Location = new System.Drawing.Point(15, 110);
            this.lblModeDescription.Name = "lblModeDescription";
            this.lblModeDescription.Size = new System.Drawing.Size(270, 40);
            this.lblModeDescription.TabIndex = 3;
            this.lblModeDescription.Text = "绝对模式：移动到指定坐标位置";
            // 
            // rbJog
            // 
            this.rbJog.AutoSize = true;
            this.rbJog.Location = new System.Drawing.Point(190, 30);
            this.rbJog.Name = "rbJog";
            this.rbJog.Size = new System.Drawing.Size(89, 24);
            this.rbJog.TabIndex = 2;
            this.rbJog.Text = "JOG模式";
            this.rbJog.UseVisualStyleBackColor = true;
            // 
            // rbRelative
            // 
            this.rbRelative.AutoSize = true;
            this.rbRelative.Location = new System.Drawing.Point(100, 30);
            this.rbRelative.Name = "rbRelative";
            this.rbRelative.Size = new System.Drawing.Size(90, 24);
            this.rbRelative.TabIndex = 1;
            this.rbRelative.Text = "相对模式";
            this.rbRelative.UseVisualStyleBackColor = true;
            // 
            // rbAbsolute
            // 
            this.rbAbsolute.AutoSize = true;
            this.rbAbsolute.Checked = true;
            this.rbAbsolute.Location = new System.Drawing.Point(15, 30);
            this.rbAbsolute.Name = "rbAbsolute";
            this.rbAbsolute.Size = new System.Drawing.Size(90, 24);
            this.rbAbsolute.TabIndex = 0;
            this.rbAbsolute.TabStop = true;
            this.rbAbsolute.Text = "绝对模式";
            this.rbAbsolute.UseVisualStyleBackColor = true;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlMiddle.Controls.Add(this.grpZAxis);
            this.pnlMiddle.Controls.Add(this.grpYAxis);
            this.pnlMiddle.Controls.Add(this.grpXAxis);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(303, 3);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(544, 794);
            this.pnlMiddle.TabIndex = 1;
            // 
            // grpZAxis
            // 
            this.grpZAxis.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpZAxis.Controls.Add(this.btnZClear);
            this.grpZAxis.Controls.Add(this.btnZHome);
            this.grpZAxis.Controls.Add(this.btnZMove);
            this.grpZAxis.Controls.Add(this.txtZTarget);
            this.grpZAxis.Controls.Add(this.lblZTarget);
            this.grpZAxis.Controls.Add(this.txtZCurrent);
            this.grpZAxis.Controls.Add(this.lblZCurrent);
            this.grpZAxis.Controls.Add(this.flpZStatus);
            this.grpZAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpZAxis.Location = new System.Drawing.Point(0, 400);
            this.grpZAxis.Name = "grpZAxis";
            this.grpZAxis.Size = new System.Drawing.Size(544, 200);
            this.grpZAxis.TabIndex = 2;
            this.grpZAxis.TabStop = false;
            this.grpZAxis.Text = "Z轴";
            // 
            // btnZClear
            // 
            this.btnZClear.Location = new System.Drawing.Point(450, 60);
            this.btnZClear.Name = "btnZClear";
            this.btnZClear.Size = new System.Drawing.Size(75, 30);
            this.btnZClear.TabIndex = 7;
            this.btnZClear.Text = "清错";
            this.btnZClear.UseVisualStyleBackColor = true;
            // 
            // btnZHome
            // 
            this.btnZHome.Location = new System.Drawing.Point(360, 60);
            this.btnZHome.Name = "btnZHome";
            this.btnZHome.Size = new System.Drawing.Size(75, 30);
            this.btnZHome.TabIndex = 6;
            this.btnZHome.Text = "原点";
            this.btnZHome.UseVisualStyleBackColor = true;
            // 
            // btnZMove
            // 
            this.btnZMove.Location = new System.Drawing.Point(270, 60);
            this.btnZMove.Name = "btnZMove";
            this.btnZMove.Size = new System.Drawing.Size(75, 30);
            this.btnZMove.TabIndex = 5;
            this.btnZMove.Text = "移动";
            this.btnZMove.UseVisualStyleBackColor = true;
            // 
            // txtZTarget
            // 
            this.txtZTarget.Location = new System.Drawing.Point(320, 25);
            this.txtZTarget.Name = "txtZTarget";
            this.txtZTarget.Size = new System.Drawing.Size(100, 27);
            this.txtZTarget.TabIndex = 4;
            this.txtZTarget.Text = "0.000";
            // 
            // lblZTarget
            // 
            this.lblZTarget.AutoSize = true;
            this.lblZTarget.Location = new System.Drawing.Point(250, 28);
            this.lblZTarget.Name = "lblZTarget";
            this.lblZTarget.Size = new System.Drawing.Size(73, 20);
            this.lblZTarget.TabIndex = 3;
            this.lblZTarget.Text = "目标位置:";
            // 
            // txtZCurrent
            // 
            this.txtZCurrent.Location = new System.Drawing.Point(110, 25);
            this.txtZCurrent.Name = "txtZCurrent";
            this.txtZCurrent.ReadOnly = true;
            this.txtZCurrent.Size = new System.Drawing.Size(100, 27);
            this.txtZCurrent.TabIndex = 2;
            this.txtZCurrent.Text = "0.000";
            // 
            // lblZCurrent
            // 
            this.lblZCurrent.AutoSize = true;
            this.lblZCurrent.Location = new System.Drawing.Point(20, 28);
            this.lblZCurrent.Name = "lblZCurrent";
            this.lblZCurrent.Size = new System.Drawing.Size(73, 20);
            this.lblZCurrent.TabIndex = 1;
            this.lblZCurrent.Text = "当前位置:";
            // 
            // flpZStatus
            // 
            this.flpZStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpZStatus.Location = new System.Drawing.Point(3, 110);
            this.flpZStatus.Name = "flpZStatus";
            this.flpZStatus.Size = new System.Drawing.Size(538, 87);
            this.flpZStatus.TabIndex = 0;
            // 
            // grpYAxis
            // 
            this.grpYAxis.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpYAxis.Controls.Add(this.btnYClear);
            this.grpYAxis.Controls.Add(this.btnYHome);
            this.grpYAxis.Controls.Add(this.btnYMove);
            this.grpYAxis.Controls.Add(this.txtYTarget);
            this.grpYAxis.Controls.Add(this.lblYTarget);
            this.grpYAxis.Controls.Add(this.txtYCurrent);
            this.grpYAxis.Controls.Add(this.lblYCurrent);
            this.grpYAxis.Controls.Add(this.flpYStatus);
            this.grpYAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpYAxis.Location = new System.Drawing.Point(0, 200);
            this.grpYAxis.Name = "grpYAxis";
            this.grpYAxis.Size = new System.Drawing.Size(544, 200);
            this.grpYAxis.TabIndex = 1;
            this.grpYAxis.TabStop = false;
            this.grpYAxis.Text = "Y轴";
            // 
            // btnYClear
            // 
            this.btnYClear.Location = new System.Drawing.Point(450, 60);
            this.btnYClear.Name = "btnYClear";
            this.btnYClear.Size = new System.Drawing.Size(75, 30);
            this.btnYClear.TabIndex = 7;
            this.btnYClear.Text = "清错";
            this.btnYClear.UseVisualStyleBackColor = true;
            // 
            // btnYHome
            // 
            this.btnYHome.Location = new System.Drawing.Point(360, 60);
            this.btnYHome.Name = "btnYHome";
            this.btnYHome.Size = new System.Drawing.Size(75, 30);
            this.btnYHome.TabIndex = 6;
            this.btnYHome.Text = "原点";
            this.btnYHome.UseVisualStyleBackColor = true;
            // 
            // btnYMove
            // 
            this.btnYMove.Location = new System.Drawing.Point(270, 60);
            this.btnYMove.Name = "btnYMove";
            this.btnYMove.Size = new System.Drawing.Size(75, 30);
            this.btnYMove.TabIndex = 5;
            this.btnYMove.Text = "移动";
            this.btnYMove.UseVisualStyleBackColor = true;
            // 
            // txtYTarget
            // 
            this.txtYTarget.Location = new System.Drawing.Point(320, 25);
            this.txtYTarget.Name = "txtYTarget";
            this.txtYTarget.Size = new System.Drawing.Size(100, 27);
            this.txtYTarget.TabIndex = 4;
            this.txtYTarget.Text = "0.000";
            // 
            // lblYTarget
            // 
            this.lblYTarget.AutoSize = true;
            this.lblYTarget.Location = new System.Drawing.Point(250, 28);
            this.lblYTarget.Name = "lblYTarget";
            this.lblYTarget.Size = new System.Drawing.Size(73, 20);
            this.lblYTarget.TabIndex = 3;
            this.lblYTarget.Text = "目标位置:";
            // 
            // txtYCurrent
            // 
            this.txtYCurrent.Location = new System.Drawing.Point(110, 25);
            this.txtYCurrent.Name = "txtYCurrent";
            this.txtYCurrent.ReadOnly = true;
            this.txtYCurrent.Size = new System.Drawing.Size(100, 27);
            this.txtYCurrent.TabIndex = 2;
            this.txtYCurrent.Text = "0.000";
            // 
            // lblYCurrent
            // 
            this.lblYCurrent.AutoSize = true;
            this.lblYCurrent.Location = new System.Drawing.Point(20, 28);
            this.lblYCurrent.Name = "lblYCurrent";
            this.lblYCurrent.Size = new System.Drawing.Size(73, 20);
            this.lblYCurrent.TabIndex = 1;
            this.lblYCurrent.Text = "当前位置:";
            // 
            // flpYStatus
            // 
            this.flpYStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpYStatus.Location = new System.Drawing.Point(3, 110);
            this.flpYStatus.Name = "flpYStatus";
            this.flpYStatus.Size = new System.Drawing.Size(538, 87);
            this.flpYStatus.TabIndex = 0;
            // 
            // grpXAxis
            // 
            this.grpXAxis.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpXAxis.Controls.Add(this.btnXClear);
            this.grpXAxis.Controls.Add(this.btnXHome);
            this.grpXAxis.Controls.Add(this.btnXMove);
            this.grpXAxis.Controls.Add(this.txtXTarget);
            this.grpXAxis.Controls.Add(this.lblXTarget);
            this.grpXAxis.Controls.Add(this.txtXCurrent);
            this.grpXAxis.Controls.Add(this.lblXCurrent);
            this.grpXAxis.Controls.Add(this.flpXStatus);
            this.grpXAxis.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpXAxis.Location = new System.Drawing.Point(0, 0);
            this.grpXAxis.Name = "grpXAxis";
            this.grpXAxis.Size = new System.Drawing.Size(544, 200);
            this.grpXAxis.TabIndex = 0;
            this.grpXAxis.TabStop = false;
            this.grpXAxis.Text = "X轴";
            // 
            // btnXClear
            // 
            this.btnXClear.Location = new System.Drawing.Point(450, 60);
            this.btnXClear.Name = "btnXClear";
            this.btnXClear.Size = new System.Drawing.Size(75, 30);
            this.btnXClear.TabIndex = 7;
            this.btnXClear.Text = "清错";
            this.btnXClear.UseVisualStyleBackColor = true;
            // 
            // btnXHome
            // 
            this.btnXHome.Location = new System.Drawing.Point(360, 60);
            this.btnXHome.Name = "btnXHome";
            this.btnXHome.Size = new System.Drawing.Size(75, 30);
            this.btnXHome.TabIndex = 6;
            this.btnXHome.Text = "原点";
            this.btnXHome.UseVisualStyleBackColor = true;
            // 
            // btnXMove
            // 
            this.btnXMove.Location = new System.Drawing.Point(270, 60);
            this.btnXMove.Name = "btnXMove";
            this.btnXMove.Size = new System.Drawing.Size(75, 30);
            this.btnXMove.TabIndex = 5;
            this.btnXMove.Text = "移动";
            this.btnXMove.UseVisualStyleBackColor = true;
            // 
            // txtXTarget
            // 
            this.txtXTarget.Location = new System.Drawing.Point(320, 25);
            this.txtXTarget.Name = "txtXTarget";
            this.txtXTarget.Size = new System.Drawing.Size(100, 27);
            this.txtXTarget.TabIndex = 4;
            this.txtXTarget.Text = "0.000";
            // 
            // lblXTarget
            // 
            this.lblXTarget.AutoSize = true;
            this.lblXTarget.Location = new System.Drawing.Point(250, 28);
            this.lblXTarget.Name = "lblXTarget";
            this.lblXTarget.Size = new System.Drawing.Size(73, 20);
            this.lblXTarget.TabIndex = 3;
            this.lblXTarget.Text = "目标位置:";
            // 
            // txtXCurrent
            // 
            this.txtXCurrent.Location = new System.Drawing.Point(110, 25);
            this.txtXCurrent.Name = "txtXCurrent";
            this.txtXCurrent.ReadOnly = true;
            this.txtXCurrent.Size = new System.Drawing.Size(100, 27);
            this.txtXCurrent.TabIndex = 2;
            this.txtXCurrent.Text = "0.000";
            // 
            // lblXCurrent
            // 
            this.lblXCurrent.AutoSize = true;
            this.lblXCurrent.Location = new System.Drawing.Point(20, 28);
            this.lblXCurrent.Name = "lblXCurrent";
            this.lblXCurrent.Size = new System.Drawing.Size(73, 20);
            this.lblXCurrent.TabIndex = 1;
            this.lblXCurrent.Text = "当前位置:";
            // 
            // flpXStatus
            // 
            this.flpXStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpXStatus.Location = new System.Drawing.Point(3, 110);
            this.flpXStatus.Name = "flpXStatus";
            this.flpXStatus.Size = new System.Drawing.Size(538, 87);
            this.flpXStatus.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpPointManager);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(853, 3);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(344, 794);
            this.pnlRight.TabIndex = 2;
            // 
            // grpPointManager
            // 
            this.grpPointManager.Controls.Add(this.btnSavePoints);
            this.grpPointManager.Controls.Add(this.btnDeletePoint);
            this.grpPointManager.Controls.Add(this.btnAddPoint);
            this.grpPointManager.Controls.Add(this.dgvPoints);
            this.grpPointManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPointManager.Location = new System.Drawing.Point(0, 0);
            this.grpPointManager.Name = "grpPointManager";
            this.grpPointManager.Size = new System.Drawing.Size(344, 794);
            this.grpPointManager.TabIndex = 0;
            this.grpPointManager.TabStop = false;
            this.grpPointManager.Text = "点位管理";
            // 
            // btnSavePoints
            // 
            this.btnSavePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePoints.Location = new System.Drawing.Point(245, 750);
            this.btnSavePoints.Name = "btnSavePoints";
            this.btnSavePoints.Size = new System.Drawing.Size(90, 35);
            this.btnSavePoints.TabIndex = 3;
            this.btnSavePoints.Text = "保存配置";
            this.btnSavePoints.UseVisualStyleBackColor = true;
            // 
            // btnDeletePoint
            // 
            this.btnDeletePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeletePoint.Location = new System.Drawing.Point(105, 750);
            this.btnDeletePoint.Name = "btnDeletePoint";
            this.btnDeletePoint.Size = new System.Drawing.Size(90, 35);
            this.btnDeletePoint.TabIndex = 2;
            this.btnDeletePoint.Text = "删除点位";
            this.btnDeletePoint.UseVisualStyleBackColor = true;
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddPoint.Location = new System.Drawing.Point(9, 750);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(90, 35);
            this.btnAddPoint.TabIndex = 1;
            this.btnAddPoint.Text = "添加点位";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            // 
            // dgvPoints
            // 
            this.dgvPoints.AllowUserToAddRows = false;
            this.dgvPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPoints.Location = new System.Drawing.Point(6, 22);
            this.dgvPoints.Name = "dgvPoints";
            this.dgvPoints.RowHeadersWidth = 51;
            this.dgvPoints.RowTemplate.Height = 25;
            this.dgvPoints.Size = new System.Drawing.Size(332, 720);
            this.dgvPoints.TabIndex = 0;
            // 
            // MotionControlPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.Name = "MotionControlPage";
            this.Text = "运动控制";
            this.Load += new System.EventHandler(this.MotionControlPage_Load);
            this.tlpMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grpDirectionControl.ResumeLayout(false);
            this.grpModeSelection.ResumeLayout(false);
            this.grpModeSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStepDistance)).EndInit();
            this.pnlMiddle.ResumeLayout(false);
            this.grpZAxis.ResumeLayout(false);
            this.grpZAxis.PerformLayout();
            this.grpYAxis.ResumeLayout(false);
            this.grpYAxis.PerformLayout();
            this.grpXAxis.ResumeLayout(false);
            this.grpXAxis.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.grpPointManager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpModeSelection;
        private System.Windows.Forms.RadioButton rbAbsolute;
        private System.Windows.Forms.RadioButton rbRelative;
        private System.Windows.Forms.RadioButton rbJog;
        private System.Windows.Forms.Label lblModeDescription;
        private System.Windows.Forms.Label lblStepDistance;
        private System.Windows.Forms.NumericUpDown numStepDistance;
        private System.Windows.Forms.GroupBox grpDirectionControl;
        private System.Windows.Forms.Button btnUpLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnUpRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnCenter;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnDownLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDownRight;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.GroupBox grpXAxis;
        private System.Windows.Forms.FlowLayoutPanel flpXStatus;
        private System.Windows.Forms.Label lblXCurrent;
        private System.Windows.Forms.TextBox txtXCurrent;
        private System.Windows.Forms.Label lblXTarget;
        private System.Windows.Forms.TextBox txtXTarget;
        private System.Windows.Forms.Button btnXMove;
        private System.Windows.Forms.Button btnXHome;
        private System.Windows.Forms.Button btnXClear;
        private System.Windows.Forms.GroupBox grpYAxis;
        private System.Windows.Forms.Button btnYClear;
        private System.Windows.Forms.Button btnYHome;
        private System.Windows.Forms.Button btnYMove;
        private System.Windows.Forms.TextBox txtYTarget;
        private System.Windows.Forms.Label lblYTarget;
        private System.Windows.Forms.TextBox txtYCurrent;
        private System.Windows.Forms.Label lblYCurrent;
        private System.Windows.Forms.FlowLayoutPanel flpYStatus;
        private System.Windows.Forms.GroupBox grpZAxis;
        private System.Windows.Forms.Button btnZClear;
        private System.Windows.Forms.Button btnZHome;
        private System.Windows.Forms.Button btnZMove;
        private System.Windows.Forms.TextBox txtZTarget;
        private System.Windows.Forms.Label lblZTarget;
        private System.Windows.Forms.TextBox txtZCurrent;
        private System.Windows.Forms.Label lblZCurrent;
        private System.Windows.Forms.FlowLayoutPanel flpZStatus;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpPointManager;
        private System.Windows.Forms.DataGridView dgvPoints;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button btnDeletePoint;
        private System.Windows.Forms.Button btnSavePoints;
    }
}
