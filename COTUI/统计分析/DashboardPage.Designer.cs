namespace COTUI.统计分析
{
    partial class DashboardPage
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
            if (disposing)
            {
                // 停止自动刷新
                try
                {
                    refreshCts?.Cancel();
                    refreshCts?.Dispose();
                }
                catch { }

                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.Panel();
            this.gbYield = new System.Windows.Forms.GroupBox();
            this.lblYieldRate = new System.Windows.Forms.Label();
            this.gbNG = new System.Windows.Forms.GroupBox();
            this.lblNGCount = new System.Windows.Forms.Label();
            this.gbOK = new System.Windows.Forms.GroupBox();
            this.lblOKCount = new System.Windows.Forms.Label();
            this.gbTotal = new System.Windows.Forms.GroupBox();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.colSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbRecentData = new System.Windows.Forms.GroupBox();
            this.panelCards.SuspendLayout();
            this.gbYield.SuspendLayout();
            this.gbNG.SuspendLayout();
            this.gbOK.SuspendLayout();
            this.gbTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.gbRecentData.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(980, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 实时数据看板";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.SystemColors.Control;
            this.panelCards.Controls.Add(this.gbYield);
            this.panelCards.Controls.Add(this.gbNG);
            this.panelCards.Controls.Add(this.gbOK);
            this.panelCards.Controls.Add(this.gbTotal);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 50);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelCards.Size = new System.Drawing.Size(980, 140);
            this.panelCards.TabIndex = 1;
            // 
            // gbYield
            // 
            this.gbYield.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbYield.Controls.Add(this.lblYieldRate);
            this.gbYield.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbYield.Location = new System.Drawing.Point(750, 15);
            this.gbYield.Name = "gbYield";
            this.gbYield.Size = new System.Drawing.Size(220, 110);
            this.gbYield.TabIndex = 3;
            this.gbYield.TabStop = false;
            this.gbYield.Text = "良品率";
            // 
            // lblYieldRate
            // 
            this.lblYieldRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYieldRate.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.lblYieldRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.lblYieldRate.Location = new System.Drawing.Point(3, 25);
            this.lblYieldRate.Name = "lblYieldRate";
            this.lblYieldRate.Size = new System.Drawing.Size(214, 82);
            this.lblYieldRate.TabIndex = 0;
            this.lblYieldRate.Text = "0%";
            this.lblYieldRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbNG
            // 
            this.gbNG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNG.Controls.Add(this.lblNGCount);
            this.gbNG.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbNG.Location = new System.Drawing.Point(510, 15);
            this.gbNG.Name = "gbNG";
            this.gbNG.Size = new System.Drawing.Size(220, 110);
            this.gbNG.TabIndex = 2;
            this.gbNG.TabStop = false;
            this.gbNG.Text = "不良品数";
            // 
            // lblNGCount
            // 
            this.lblNGCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNGCount.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.lblNGCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblNGCount.Location = new System.Drawing.Point(3, 25);
            this.lblNGCount.Name = "lblNGCount";
            this.lblNGCount.Size = new System.Drawing.Size(214, 82);
            this.lblNGCount.TabIndex = 0;
            this.lblNGCount.Text = "0";
            this.lblNGCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbOK
            // 
            this.gbOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOK.Controls.Add(this.lblOKCount);
            this.gbOK.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbOK.Location = new System.Drawing.Point(270, 15);
            this.gbOK.Name = "gbOK";
            this.gbOK.Size = new System.Drawing.Size(220, 110);
            this.gbOK.TabIndex = 1;
            this.gbOK.TabStop = false;
            this.gbOK.Text = "良品数";
            // 
            // lblOKCount
            // 
            this.lblOKCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOKCount.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.lblOKCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblOKCount.Location = new System.Drawing.Point(3, 25);
            this.lblOKCount.Name = "lblOKCount";
            this.lblOKCount.Size = new System.Drawing.Size(214, 82);
            this.lblOKCount.TabIndex = 0;
            this.lblOKCount.Text = "0";
            this.lblOKCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbTotal
            // 
            this.gbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTotal.Controls.Add(this.lblTotalCount);
            this.gbTotal.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbTotal.Location = new System.Drawing.Point(30, 15);
            this.gbTotal.Name = "gbTotal";
            this.gbTotal.Size = new System.Drawing.Size(220, 110);
            this.gbTotal.TabIndex = 0;
            this.gbTotal.TabStop = false;
            this.gbTotal.Text = "总产量";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCount.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTotalCount.Location = new System.Drawing.Point(3, 25);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(214, 82);
            this.lblTotalCount.TabIndex = 0;
            this.lblTotalCount.Text = "0";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvRecent
            // 
            this.dgvRecent.AllowUserToAddRows = false;
            this.dgvRecent.AllowUserToDeleteRows = false;
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecent.ColumnHeadersHeight = 35;
            this.dgvRecent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSN,
            this.colTime,
            this.colModel,
            this.colResult,
            this.colOperator});
            this.dgvRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecent.Location = new System.Drawing.Point(20, 32);
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersVisible = false;
            this.dgvRecent.RowHeadersWidth = 51;
            this.dgvRecent.RowTemplate.Height = 30;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(940, 408);
            this.dgvRecent.TabIndex = 0;
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
            this.colTime.HeaderText = "生产时间";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            // 
            // colModel
            // 
            this.colModel.HeaderText = "产品型号";
            this.colModel.MinimumWidth = 6;
            this.colModel.Name = "colModel";
            this.colModel.ReadOnly = true;
            // 
            // colResult
            // 
            this.colResult.HeaderText = "检测结果";
            this.colResult.MinimumWidth = 6;
            this.colResult.Name = "colResult";
            this.colResult.ReadOnly = true;
            // 
            // colOperator
            // 
            this.colOperator.HeaderText = "操作员";
            this.colOperator.MinimumWidth = 6;
            this.colOperator.Name = "colOperator";
            this.colOperator.ReadOnly = true;
            // 
            // gbRecentData
            // 
            this.gbRecentData.Controls.Add(this.dgvRecent);
            this.gbRecentData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRecentData.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.gbRecentData.Location = new System.Drawing.Point(0, 190);
            this.gbRecentData.Name = "gbRecentData";
            this.gbRecentData.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
            this.gbRecentData.Size = new System.Drawing.Size(980, 460);
            this.gbRecentData.TabIndex = 2;
            this.gbRecentData.TabStop = false;
            this.gbRecentData.Text = "最近生产记录";
            // 
            // DashboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(980, 650);
            this.Controls.Add(this.gbRecentData);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardPage";
            this.Text = "实时数据看板";
            this.Load += new System.EventHandler(this.DashboardPage_Load);
            this.panelCards.ResumeLayout(false);
            this.gbYield.ResumeLayout(false);
            this.gbNG.ResumeLayout(false);
            this.gbOK.ResumeLayout(false);
            this.gbTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.gbRecentData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.GroupBox gbTotal;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.GroupBox gbOK;
        private System.Windows.Forms.Label lblOKCount;
        private System.Windows.Forms.GroupBox gbNG;
        private System.Windows.Forms.Label lblNGCount;
        private System.Windows.Forms.GroupBox gbYield;
        private System.Windows.Forms.Label lblYieldRate;
        private System.Windows.Forms.GroupBox gbRecentData;
        private System.Windows.Forms.DataGridView dgvRecent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperator;
    }
}
