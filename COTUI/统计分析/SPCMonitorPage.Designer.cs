namespace COTUI.统计分析
{
    partial class SPCMonitorPage
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblHelp = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.cmbMeasureType = new System.Windows.Forms.ComboBox();
            this.lblMeasure = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.Panel();
            this.gbCpk = new System.Windows.Forms.GroupBox();
            this.lblCpk = new System.Windows.Forms.Label();
            this.gbCp = new System.Windows.Forms.GroupBox();
            this.lblCp = new System.Windows.Forms.Label();
            this.gbMean = new System.Windows.Forms.GroupBox();
            this.lblMean = new System.Windows.Forms.Label();
            this.gbStdDev = new System.Windows.Forms.GroupBox();
            this.lblStdDev = new System.Windows.Forms.Label();
            this.gbUSL = new System.Windows.Forms.GroupBox();
            this.lblUSL = new System.Windows.Forms.Label();
            this.gbLSL = new System.Windows.Forms.GroupBox();
            this.lblLSL = new System.Windows.Forms.Label();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeviation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelChart = new System.Windows.Forms.Panel();
            this.lblChartPlaceholder = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.gbCpk.SuspendLayout();
            this.gbCp.SuspendLayout();
            this.gbMean.SuspendLayout();
            this.gbStdDev.SuspendLayout();
            this.gbUSL.SuspendLayout();
            this.gbLSL.SuspendLayout();
            this.gbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelTop.Controls.Add(this.lblHelp);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnCalculate);
            this.panelTop.Controls.Add(this.cmbMeasureType);
            this.panelTop.Controls.Add(this.lblMeasure);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelTop.Size = new System.Drawing.Size(980, 70);
            this.panelTop.TabIndex = 0;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHelp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lblHelp.ForeColor = System.Drawing.Color.Gray;
            this.lblHelp.Location = new System.Drawing.Point(560, 22);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(400, 30);
            this.lblHelp.TabIndex = 4;
            this.lblHelp.Text = "💡 提示：选择测量项目后点击【计算Cpk】按钮进行过程能力分析";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(440, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 刷新数据";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCalculate.FlatAppearance.BorderSize = 0;
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnCalculate.ForeColor = System.Drawing.Color.White;
            this.btnCalculate.Location = new System.Drawing.Point(320, 20);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(110, 35);
            this.btnCalculate.TabIndex = 2;
            this.btnCalculate.Text = "📊 计算Cpk";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // cmbMeasureType
            // 
            this.cmbMeasureType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbMeasureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeasureType.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cmbMeasureType.FormattingEnabled = true;
            this.cmbMeasureType.Items.AddRange(new object[] {
            "X尺寸 (mm)",
            "Y尺寸 (mm)",
            "Z尺寸 (mm)",
            "角度 (°)",
            "间隙 (mm)",
            "周期时间 (s)"});
            this.cmbMeasureType.Location = new System.Drawing.Point(110, 22);
            this.cmbMeasureType.Name = "cmbMeasureType";
            this.cmbMeasureType.Size = new System.Drawing.Size(200, 31);
            this.cmbMeasureType.TabIndex = 1;
            // 
            // lblMeasure
            // 
            this.lblMeasure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMeasure.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblMeasure.Location = new System.Drawing.Point(15, 22);
            this.lblMeasure.Name = "lblMeasure";
            this.lblMeasure.Size = new System.Drawing.Size(90, 30);
            this.lblMeasure.TabIndex = 0;
            this.lblMeasure.Text = "测量项目:";
            this.lblMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.SystemColors.Control;
            this.panelCards.Controls.Add(this.gbCpk);
            this.panelCards.Controls.Add(this.gbCp);
            this.panelCards.Controls.Add(this.gbMean);
            this.panelCards.Controls.Add(this.gbStdDev);
            this.panelCards.Controls.Add(this.gbUSL);
            this.panelCards.Controls.Add(this.gbLSL);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 70);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelCards.Size = new System.Drawing.Size(980, 120);
            this.panelCards.TabIndex = 1;
            // 
            // gbCpk
            // 
            this.gbCpk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCpk.Controls.Add(this.lblCpk);
            this.gbCpk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbCpk.Location = new System.Drawing.Point(15, 8);
            this.gbCpk.Name = "gbCpk";
            this.gbCpk.Size = new System.Drawing.Size(150, 104);
            this.gbCpk.TabIndex = 0;
            this.gbCpk.TabStop = false;
            this.gbCpk.Text = "Cpk";
            // 
            // lblCpk
            // 
            this.lblCpk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCpk.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblCpk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblCpk.Location = new System.Drawing.Point(3, 23);
            this.lblCpk.Name = "lblCpk";
            this.lblCpk.Size = new System.Drawing.Size(144, 78);
            this.lblCpk.TabIndex = 0;
            this.lblCpk.Text = "--";
            this.lblCpk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbCp
            // 
            this.gbCp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCp.Controls.Add(this.lblCp);
            this.gbCp.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbCp.Location = new System.Drawing.Point(175, 8);
            this.gbCp.Name = "gbCp";
            this.gbCp.Size = new System.Drawing.Size(150, 104);
            this.gbCp.TabIndex = 1;
            this.gbCp.TabStop = false;
            this.gbCp.Text = "Cp";
            // 
            // lblCp
            // 
            this.lblCp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCp.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblCp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblCp.Location = new System.Drawing.Point(3, 23);
            this.lblCp.Name = "lblCp";
            this.lblCp.Size = new System.Drawing.Size(144, 78);
            this.lblCp.TabIndex = 0;
            this.lblCp.Text = "--";
            this.lblCp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbMean
            // 
            this.gbMean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMean.Controls.Add(this.lblMean);
            this.gbMean.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbMean.Location = new System.Drawing.Point(335, 8);
            this.gbMean.Name = "gbMean";
            this.gbMean.Size = new System.Drawing.Size(150, 104);
            this.gbMean.TabIndex = 2;
            this.gbMean.TabStop = false;
            this.gbMean.Text = "均值 (μ)";
            // 
            // lblMean
            // 
            this.lblMean.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMean.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblMean.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblMean.Location = new System.Drawing.Point(3, 23);
            this.lblMean.Name = "lblMean";
            this.lblMean.Size = new System.Drawing.Size(144, 78);
            this.lblMean.TabIndex = 0;
            this.lblMean.Text = "--";
            this.lblMean.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbStdDev
            // 
            this.gbStdDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStdDev.Controls.Add(this.lblStdDev);
            this.gbStdDev.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbStdDev.Location = new System.Drawing.Point(495, 8);
            this.gbStdDev.Name = "gbStdDev";
            this.gbStdDev.Size = new System.Drawing.Size(150, 104);
            this.gbStdDev.TabIndex = 3;
            this.gbStdDev.TabStop = false;
            this.gbStdDev.Text = "标准差 (σ)";
            // 
            // lblStdDev
            // 
            this.lblStdDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStdDev.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblStdDev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblStdDev.Location = new System.Drawing.Point(3, 23);
            this.lblStdDev.Name = "lblStdDev";
            this.lblStdDev.Size = new System.Drawing.Size(144, 78);
            this.lblStdDev.TabIndex = 0;
            this.lblStdDev.Text = "--";
            this.lblStdDev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbUSL
            // 
            this.gbUSL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUSL.Controls.Add(this.lblUSL);
            this.gbUSL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbUSL.Location = new System.Drawing.Point(655, 8);
            this.gbUSL.Name = "gbUSL";
            this.gbUSL.Size = new System.Drawing.Size(150, 104);
            this.gbUSL.TabIndex = 4;
            this.gbUSL.TabStop = false;
            this.gbUSL.Text = "上限 (USL)";
            // 
            // lblUSL
            // 
            this.lblUSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUSL.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblUSL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblUSL.Location = new System.Drawing.Point(3, 23);
            this.lblUSL.Name = "lblUSL";
            this.lblUSL.Size = new System.Drawing.Size(144, 78);
            this.lblUSL.TabIndex = 0;
            this.lblUSL.Text = "--";
            this.lblUSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbLSL
            // 
            this.gbLSL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLSL.Controls.Add(this.lblLSL);
            this.gbLSL.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.gbLSL.Location = new System.Drawing.Point(815, 8);
            this.gbLSL.Name = "gbLSL";
            this.gbLSL.Size = new System.Drawing.Size(150, 104);
            this.gbLSL.TabIndex = 5;
            this.gbLSL.TabStop = false;
            this.gbLSL.Text = "下限 (LSL)";
            // 
            // lblLSL
            // 
            this.lblLSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLSL.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.lblLSL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblLSL.Location = new System.Drawing.Point(3, 23);
            this.lblLSL.Name = "lblLSL";
            this.lblLSL.Size = new System.Drawing.Size(144, 78);
            this.lblLSL.TabIndex = 0;
            this.lblLSL.Text = "--";
            this.lblLSL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbData
            // 
            this.gbData.Controls.Add(this.dgvResults);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbData.Location = new System.Drawing.Point(0, 190);
            this.gbData.Name = "gbData";
            this.gbData.Padding = new System.Windows.Forms.Padding(10);
            this.gbData.Size = new System.Drawing.Size(980, 300);
            this.gbData.TabIndex = 2;
            this.gbData.TabStop = false;
            this.gbData.Text = "📋 最近100条测量数据";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersHeight = 35;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSN,
            this.colTime,
            this.colValue,
            this.colDeviation,
            this.colStatus,
            this.colModel});
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgvResults.Location = new System.Drawing.Point(10, 32);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.RowTemplate.Height = 30;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(960, 258);
            this.dgvResults.TabIndex = 0;
            // 
            // colSN
            // 
            this.colSN.HeaderText = "产品SN";
            this.colSN.MinimumWidth = 6;
            this.colSN.Name = "colSN";
            this.colSN.ReadOnly = true;
            // 
            // colTime
            // 
            this.colTime.HeaderText = "测量时间";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "测量值";
            this.colValue.MinimumWidth = 6;
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // colDeviation
            // 
            this.colDeviation.HeaderText = "偏差";
            this.colDeviation.MinimumWidth = 6;
            this.colDeviation.Name = "colDeviation";
            this.colDeviation.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "状态";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colModel
            // 
            this.colModel.HeaderText = "型号";
            this.colModel.MinimumWidth = 6;
            this.colModel.Name = "colModel";
            this.colModel.ReadOnly = true;
            // 
            // panelChart
            // 
            this.panelChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChart.Controls.Add(this.lblChartPlaceholder);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChart.Location = new System.Drawing.Point(0, 490);
            this.panelChart.Name = "panelChart";
            this.panelChart.Padding = new System.Windows.Forms.Padding(10);
            this.panelChart.Size = new System.Drawing.Size(980, 210);
            this.panelChart.TabIndex = 3;
            // 
            // lblChartPlaceholder
            // 
            this.lblChartPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChartPlaceholder.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblChartPlaceholder.ForeColor = System.Drawing.Color.Gray;
            this.lblChartPlaceholder.Location = new System.Drawing.Point(10, 10);
            this.lblChartPlaceholder.Name = "lblChartPlaceholder";
            this.lblChartPlaceholder.Size = new System.Drawing.Size(958, 188);
            this.lblChartPlaceholder.TabIndex = 0;
            this.lblChartPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SPCMonitorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(980, 700);
            this.Controls.Add(this.gbData);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SPCMonitorPage";
            this.Text = "SPC过程监控";
            this.Load += new System.EventHandler(this.SPCMonitorPage_Load);
            this.panelTop.ResumeLayout(false);
            this.panelCards.ResumeLayout(false);
            this.gbCpk.ResumeLayout(false);
            this.gbCp.ResumeLayout(false);
            this.gbMean.ResumeLayout(false);
            this.gbStdDev.ResumeLayout(false);
            this.gbUSL.ResumeLayout(false);
            this.gbLSL.ResumeLayout(false);
            this.gbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.ComboBox cmbMeasureType;
        private System.Windows.Forms.Label lblMeasure;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.GroupBox gbCpk;
        private System.Windows.Forms.Label lblCpk;
        private System.Windows.Forms.GroupBox gbCp;
        private System.Windows.Forms.Label lblCp;
        private System.Windows.Forms.GroupBox gbMean;
        private System.Windows.Forms.Label lblMean;
        private System.Windows.Forms.GroupBox gbStdDev;
        private System.Windows.Forms.Label lblStdDev;
        private System.Windows.Forms.GroupBox gbUSL;
        private System.Windows.Forms.Label lblUSL;
        private System.Windows.Forms.GroupBox gbLSL;
        private System.Windows.Forms.Label lblLSL;
        private System.Windows.Forms.GroupBox gbData;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeviation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModel;
        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.Label lblChartPlaceholder;
    }
}
