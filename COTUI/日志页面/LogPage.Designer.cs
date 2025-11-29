namespace COTUI.日志页面
{
    partial class LogPage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblTotalLogs = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkFatal = new System.Windows.Forms.CheckBox();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.chkWarn = new System.Windows.Forms.CheckBox();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.numMaxLogs = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbStation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 188);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Controls.Add(this.lblTotalLogs);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.btnQuery);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(800, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(267, 188);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // lblTotalLogs
            // 
            this.lblTotalLogs.AutoSize = true;
            this.lblTotalLogs.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalLogs.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalLogs.Location = new System.Drawing.Point(159, 144);
            this.lblTotalLogs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalLogs.Name = "lblTotalLogs";
            this.lblTotalLogs.Size = new System.Drawing.Size(20, 24);
            this.lblTotalLogs.TabIndex = 4;
            this.lblTotalLogs.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 146);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "日志总条数：";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(23, 98);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(213, 38);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导出日志";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(23, 60);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(213, 38);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空显示";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(23, 22);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(213, 38);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询日志";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.chkFatal);
            this.groupBox2.Controls.Add(this.chkError);
            this.groupBox2.Controls.Add(this.chkWarn);
            this.groupBox2.Controls.Add(this.chkInfo);
            this.groupBox2.Controls.Add(this.chkDebug);
            this.groupBox2.Controls.Add(this.chkTrace);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(533, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(267, 188);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "日志级别筛选";
            // 
            // chkFatal
            // 
            this.chkFatal.AutoSize = true;
            this.chkFatal.Location = new System.Drawing.Point(136, 112);
            this.chkFatal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFatal.Name = "chkFatal";
            this.chkFatal.Size = new System.Drawing.Size(69, 19);
            this.chkFatal.TabIndex = 5;
            this.chkFatal.Text = "Fatal";
            this.chkFatal.UseVisualStyleBackColor = true;
            this.chkFatal.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // chkError
            // 
            this.chkError.AutoSize = true;
            this.chkError.Location = new System.Drawing.Point(136, 78);
            this.chkError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(69, 19);
            this.chkError.TabIndex = 4;
            this.chkError.Text = "Error";
            this.chkError.UseVisualStyleBackColor = true;
            this.chkError.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // chkWarn
            // 
            this.chkWarn.AutoSize = true;
            this.chkWarn.Location = new System.Drawing.Point(136, 42);
            this.chkWarn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkWarn.Name = "chkWarn";
            this.chkWarn.Size = new System.Drawing.Size(61, 19);
            this.chkWarn.TabIndex = 3;
            this.chkWarn.Text = "Warn";
            this.chkWarn.UseVisualStyleBackColor = true;
            this.chkWarn.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // chkInfo
            // 
            this.chkInfo.AutoSize = true;
            this.chkInfo.Location = new System.Drawing.Point(23, 112);
            this.chkInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkInfo.Name = "chkInfo";
            this.chkInfo.Size = new System.Drawing.Size(61, 19);
            this.chkInfo.TabIndex = 2;
            this.chkInfo.Text = "Info";
            this.chkInfo.UseVisualStyleBackColor = true;
            this.chkInfo.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(23, 78);
            this.chkDebug.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(69, 19);
            this.chkDebug.TabIndex = 1;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(23, 42);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 19);
            this.chkTrace.TabIndex = 0;
            this.chkTrace.Text = "Trace";
            this.chkTrace.UseVisualStyleBackColor = true;
            this.chkTrace.CheckedChanged += new System.EventHandler(this.LogLevelFilter_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.chkAutoRefresh);
            this.groupBox1.Controls.Add(this.numMaxLogs);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmbStation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(533, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "筛选条件";
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Location = new System.Drawing.Point(373, 145);
            this.chkAutoRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(128, 19);
            this.chkAutoRefresh.TabIndex = 8;
            this.chkAutoRefresh.Text = "自动刷新(5秒)";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.chkAutoRefresh_CheckedChanged);
            // 
            // numMaxLogs
            // 
            this.numMaxLogs.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxLogs.Location = new System.Drawing.Point(125, 142);
            this.numMaxLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numMaxLogs.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxLogs.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxLogs.Name = "numMaxLogs";
            this.numMaxLogs.Size = new System.Drawing.Size(160, 25);
            this.numMaxLogs.TabIndex = 7;
            this.numMaxLogs.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 146);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "最大显示数：";
            // 
            // cmbStation
            // 
            this.cmbStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStation.FormattingEnabled = true;
            this.cmbStation.Location = new System.Drawing.Point(125, 102);
            this.cmbStation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbStation.Name = "cmbStation";
            this.cmbStation.Size = new System.Drawing.Size(372, 23);
            this.cmbStation.TabIndex = 5;
            this.cmbStation.SelectedIndexChanged += new System.EventHandler(this.cmbStation_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "选择工站：";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(125, 65);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(372, 25);
            this.dtpEndDate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(125, 29);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(372, 25);
            this.dtpStartDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间：";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 188);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panel2.Size = new System.Drawing.Size(1067, 374);
            this.panel2.TabIndex = 1;
            // 
            // LogPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LogPage";
            this.Text = "日志查看";
            this.Load += new System.EventHandler(this.LogPage_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkFatal;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.CheckBox chkWarn;
        private System.Windows.Forms.CheckBox chkInfo;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.NumericUpDown numMaxLogs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Label lblTotalLogs;
        private System.Windows.Forms.Label label9;
    }
}
