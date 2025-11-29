namespace COTUI.报警页面
{
    partial class WarningPage
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvWarningLog = new System.Windows.Forms.DataGridView();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalWarnings = new System.Windows.Forms.Label();
            this.lblWarningPercent = new System.Windows.Forms.Label();
            this.lblWarningDuration = new System.Windows.Forms.Label();
            this.lblProductionTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvDailyStats = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDailyWarnings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDailyProductionTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDailyWarningTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDailyPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnQueryMonth = new System.Windows.Forms.Button();
            this.cmbStatsMonth = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbStatsYear = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarningLog)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyStats)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1067, 562);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1059, 533);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "报警日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvWarningLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 154);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panel2.Size = new System.Drawing.Size(1051, 375);
            this.panel2.TabIndex = 1;
            // 
            // dgvWarningLog
            // 
            this.dgvWarningLog.AllowUserToAddRows = false;
            this.dgvWarningLog.AllowUserToDeleteRows = false;
            this.dgvWarningLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWarningLog.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvWarningLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWarningLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTime,
            this.colLevel,
            this.colMessage,
            this.colSource});
            this.dgvWarningLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWarningLog.Location = new System.Drawing.Point(7, 6);
            this.dgvWarningLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvWarningLog.Name = "dgvWarningLog";
            this.dgvWarningLog.ReadOnly = true;
            this.dgvWarningLog.RowHeadersWidth = 51;
            this.dgvWarningLog.RowTemplate.Height = 23;
            this.dgvWarningLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWarningLog.Size = new System.Drawing.Size(1037, 363);
            this.dgvWarningLog.TabIndex = 0;
            // 
            // colTime
            // 
            this.colTime.FillWeight = 80F;
            this.colTime.HeaderText = "时间";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colLevel
            // 
            this.colLevel.FillWeight = 40F;
            this.colLevel.HeaderText = "级别";
            this.colLevel.MinimumWidth = 6;
            this.colLevel.Name = "colLevel";
            this.colLevel.ReadOnly = true;
            // 
            // colMessage
            // 
            this.colMessage.FillWeight = 120F;
            this.colMessage.HeaderText = "消息";
            this.colMessage.MinimumWidth = 6;
            this.colMessage.Name = "colMessage";
            this.colMessage.ReadOnly = true;
            // 
            // colSource
            // 
            this.colSource.FillWeight = 60F;
            this.colSource.HeaderText = "来源";
            this.colSource.MinimumWidth = 6;
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 150);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.lblTotalWarnings);
            this.groupBox2.Controls.Add(this.lblWarningPercent);
            this.groupBox2.Controls.Add(this.lblWarningDuration);
            this.groupBox2.Controls.Add(this.lblProductionTime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(560, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(491, 150);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计信息";
            // 
            // lblTotalWarnings
            // 
            this.lblTotalWarnings.AutoSize = true;
            this.lblTotalWarnings.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalWarnings.ForeColor = System.Drawing.Color.Red;
            this.lblTotalWarnings.Location = new System.Drawing.Point(372, 31);
            this.lblTotalWarnings.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalWarnings.Name = "lblTotalWarnings";
            this.lblTotalWarnings.Size = new System.Drawing.Size(18, 19);
            this.lblTotalWarnings.TabIndex = 7;
            this.lblTotalWarnings.Text = "0";
            // 
            // lblWarningPercent
            // 
            this.lblWarningPercent.AutoSize = true;
            this.lblWarningPercent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningPercent.ForeColor = System.Drawing.Color.Red;
            this.lblWarningPercent.Location = new System.Drawing.Point(372, 106);
            this.lblWarningPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWarningPercent.Name = "lblWarningPercent";
            this.lblWarningPercent.Size = new System.Drawing.Size(45, 19);
            this.lblWarningPercent.TabIndex = 6;
            this.lblWarningPercent.Text = "0.0%";
            // 
            // lblWarningDuration
            // 
            this.lblWarningDuration.AutoSize = true;
            this.lblWarningDuration.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWarningDuration.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblWarningDuration.Location = new System.Drawing.Point(372, 69);
            this.lblWarningDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWarningDuration.Name = "lblWarningDuration";
            this.lblWarningDuration.Size = new System.Drawing.Size(65, 19);
            this.lblWarningDuration.TabIndex = 5;
            this.lblWarningDuration.Text = "0.0 小时";
            // 
            // lblProductionTime
            // 
            this.lblProductionTime.AutoSize = true;
            this.lblProductionTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductionTime.ForeColor = System.Drawing.Color.Green;
            this.lblProductionTime.Location = new System.Drawing.Point(129, 106);
            this.lblProductionTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductionTime.Name = "lblProductionTime";
            this.lblProductionTime.Size = new System.Drawing.Size(65, 19);
            this.lblProductionTime.TabIndex = 4;
            this.lblProductionTime.Text = "0.0 小时";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 108);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "报警占比：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(272, 70);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "报警时长：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "报警次数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "生产时长：";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(560, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "时间筛选";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(434, 58);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 38);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "开始时间：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1059, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "每日统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvDailyStats);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 92);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panel4.Size = new System.Drawing.Size(1051, 437);
            this.panel4.TabIndex = 1;
            // 
            // dgvDailyStats
            // 
            this.dgvDailyStats.AllowUserToAddRows = false;
            this.dgvDailyStats.AllowUserToDeleteRows = false;
            this.dgvDailyStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDailyStats.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDailyStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDailyStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDate,
            this.colDailyWarnings,
            this.colDailyProductionTime,
            this.colDailyWarningTime,
            this.colDailyPercent});
            this.dgvDailyStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDailyStats.Location = new System.Drawing.Point(7, 6);
            this.dgvDailyStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDailyStats.Name = "dgvDailyStats";
            this.dgvDailyStats.ReadOnly = true;
            this.dgvDailyStats.RowHeadersWidth = 51;
            this.dgvDailyStats.RowTemplate.Height = 23;
            this.dgvDailyStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDailyStats.Size = new System.Drawing.Size(1037, 425);
            this.dgvDailyStats.TabIndex = 0;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "日期";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colDailyWarnings
            // 
            this.colDailyWarnings.HeaderText = "报警次数";
            this.colDailyWarnings.MinimumWidth = 6;
            this.colDailyWarnings.Name = "colDailyWarnings";
            this.colDailyWarnings.ReadOnly = true;
            // 
            // colDailyProductionTime
            // 
            this.colDailyProductionTime.HeaderText = "生产时长(小时)";
            this.colDailyProductionTime.MinimumWidth = 6;
            this.colDailyProductionTime.Name = "colDailyProductionTime";
            this.colDailyProductionTime.ReadOnly = true;
            // 
            // colDailyWarningTime
            // 
            this.colDailyWarningTime.HeaderText = "报警时长(小时)";
            this.colDailyWarningTime.MinimumWidth = 6;
            this.colDailyWarningTime.Name = "colDailyWarningTime";
            this.colDailyWarningTime.ReadOnly = true;
            // 
            // colDailyPercent
            // 
            this.colDailyPercent.HeaderText = "报警占比(%)";
            this.colDailyPercent.MinimumWidth = 6;
            this.colDailyPercent.Name = "colDailyPercent";
            this.colDailyPercent.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1051, 88);
            this.panel3.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.btnQueryMonth);
            this.groupBox3.Controls.Add(this.cmbStatsMonth);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.cmbStatsYear);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1051, 88);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "统计时间";
            // 
            // btnQueryMonth
            // 
            this.btnQueryMonth.Location = new System.Drawing.Point(436, 31);
            this.btnQueryMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQueryMonth.Name = "btnQueryMonth";
            this.btnQueryMonth.Size = new System.Drawing.Size(100, 38);
            this.btnQueryMonth.TabIndex = 4;
            this.btnQueryMonth.Text = "查询";
            this.btnQueryMonth.UseVisualStyleBackColor = true;
            this.btnQueryMonth.Click += new System.EventHandler(this.btnQueryMonth_Click);
            // 
            // cmbStatsMonth
            // 
            this.cmbStatsMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatsMonth.FormattingEnabled = true;
            this.cmbStatsMonth.Location = new System.Drawing.Point(295, 38);
            this.cmbStatsMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatsMonth.Name = "cmbStatsMonth";
            this.cmbStatsMonth.Size = new System.Drawing.Size(105, 23);
            this.cmbStatsMonth.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(243, 41);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "月：";
            // 
            // cmbStatsYear
            // 
            this.cmbStatsYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatsYear.FormattingEnabled = true;
            this.cmbStatsYear.Location = new System.Drawing.Point(101, 38);
            this.cmbStatsYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStatsYear.Name = "cmbStatsYear";
            this.cmbStatsYear.Size = new System.Drawing.Size(105, 23);
            this.cmbStatsYear.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(49, 41);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "年：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(112, 31);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 25);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(112, 80);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 25);
            this.dateTimePicker2.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "结束时间：";
            // 
            // WarningPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WarningPage";
            this.Text = "WarningPage";
            this.Load += new System.EventHandler(this.WarningPage_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWarningLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDailyStats)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvWarningLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProductionTime;
        private System.Windows.Forms.Label lblTotalWarnings;
        private System.Windows.Forms.Label lblWarningPercent;
        private System.Windows.Forms.Label lblWarningDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvDailyStats;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnQueryMonth;
        private System.Windows.Forms.ComboBox cmbStatsMonth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbStatsYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDailyWarnings;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDailyProductionTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDailyWarningTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDailyPercent;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}