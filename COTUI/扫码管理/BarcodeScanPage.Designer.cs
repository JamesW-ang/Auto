namespace COTUI.扫码管理
{
    partial class BarcodeScanPage
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
            this.cmbScanMode = new System.Windows.Forms.ComboBox();
            this.txtManualInput = new System.Windows.Forms.TextBox();
            this.btnManualInput = new System.Windows.Forms.Button();
            this.btnTestSN = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLastSN = new System.Windows.Forms.Label();
            this.lblLastScanTime = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbScanMode
            // 
            this.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanMode.FormattingEnabled = true;
            this.cmbScanMode.Location = new System.Drawing.Point(20, 20);
            this.cmbScanMode.Name = "cmbScanMode";
            this.cmbScanMode.Size = new System.Drawing.Size(200, 21);
            this.cmbScanMode.TabIndex = 0;
            // 
            // txtManualInput
            // 
            this.txtManualInput.Location = new System.Drawing.Point(20, 60);
            this.txtManualInput.Name = "txtManualInput";
            this.txtManualInput.Size = new System.Drawing.Size(300, 20);
            this.txtManualInput.TabIndex = 1;
            // 
            // btnManualInput
            // 
            this.btnManualInput.Location = new System.Drawing.Point(340, 58);
            this.btnManualInput.Name = "btnManualInput";
            this.btnManualInput.Size = new System.Drawing.Size(75, 23);
            this.btnManualInput.TabIndex = 2;
            this.btnManualInput.Text = "确认";
            this.btnManualInput.UseVisualStyleBackColor = true;
            // 
            // btnTestSN
            // 
            this.btnTestSN.Location = new System.Drawing.Point(430, 58);
            this.btnTestSN.Name = "btnTestSN";
            this.btnTestSN.Size = new System.Drawing.Size(100, 23);
            this.btnTestSN.TabIndex = 3;
            this.btnTestSN.Text = "生成测试SN";
            this.btnTestSN.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(20, 400);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 4;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(20, 250);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(550, 140);
            this.txtLog.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(250, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "状态";
            // 
            // lblLastSN
            // 
            this.lblLastSN.AutoSize = true;
            this.lblLastSN.Location = new System.Drawing.Point(20, 100);
            this.lblLastSN.Name = "lblLastSN";
            this.lblLastSN.Size = new System.Drawing.Size(53, 13);
            this.lblLastSN.TabIndex = 7;
            this.lblLastSN.Text = "最后SN:";
            // 
            // lblLastScanTime
            // 
            this.lblLastScanTime.AutoSize = true;
            this.lblLastScanTime.Location = new System.Drawing.Point(20, 120);
            this.lblLastScanTime.Name = "lblLastScanTime";
            this.lblLastScanTime.Size = new System.Drawing.Size(65, 13);
            this.lblLastScanTime.TabIndex = 8;
            this.lblLastScanTime.Text = "扫描时间:";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(20, 150);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.Size = new System.Drawing.Size(550, 90);
            this.dgvHistory.TabIndex = 9;
            // 
            // BarcodeScanPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblLastScanTime);
            this.Controls.Add(this.lblLastSN);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnTestSN);
            this.Controls.Add(this.btnManualInput);
            this.Controls.Add(this.txtManualInput);
            this.Controls.Add(this.cmbScanMode);
            this.Name = "BarcodeScanPage";
            this.Text = "条码扫描";
            this.Load += new System.EventHandler(this.BarcodeScanPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbScanMode;
        private System.Windows.Forms.TextBox txtManualInput;
        private System.Windows.Forms.Button btnManualInput;
        private System.Windows.Forms.Button btnTestSN;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLastSN;
        private System.Windows.Forms.Label lblLastScanTime;
        private System.Windows.Forms.DataGridView dgvHistory;
    }
}
