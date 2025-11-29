namespace COTUI.统计分析
{
    partial class StatisticsAnalysisPage
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
            this.tabPage_Dashboard = new System.Windows.Forms.TabPage();
            this.panel_Dashboard = new System.Windows.Forms.Panel();
            this.tabPage_SPC = new System.Windows.Forms.TabPage();
            this.panel_SPC = new System.Windows.Forms.Panel();
            this.tabPage_Traceability = new System.Windows.Forms.TabPage();
            this.panel_Traceability = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage_Dashboard.SuspendLayout();
            this.tabPage_SPC.SuspendLayout();
            this.tabPage_Traceability.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_Dashboard);
            this.tabControl1.Controls.Add(this.tabPage_SPC);
            this.tabControl1.Controls.Add(this.tabPage_Traceability);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1000, 700);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage_Dashboard
            // 
            this.tabPage_Dashboard.BackColor = System.Drawing.Color.White;
            this.tabPage_Dashboard.Controls.Add(this.panel_Dashboard);
            this.tabPage_Dashboard.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Dashboard.Name = "tabPage_Dashboard";
            this.tabPage_Dashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Dashboard.Size = new System.Drawing.Size(992, 667);
            this.tabPage_Dashboard.TabIndex = 0;
            this.tabPage_Dashboard.Text = "实时数据看板";
            // 
            // panel_Dashboard
            // 
            this.panel_Dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Dashboard.Location = new System.Drawing.Point(3, 3);
            this.panel_Dashboard.Name = "panel_Dashboard";
            this.panel_Dashboard.Size = new System.Drawing.Size(986, 661);
            this.panel_Dashboard.TabIndex = 0;
            // 
            // tabPage_SPC
            // 
            this.tabPage_SPC.BackColor = System.Drawing.Color.White;
            this.tabPage_SPC.Controls.Add(this.panel_SPC);
            this.tabPage_SPC.Location = new System.Drawing.Point(4, 29);
            this.tabPage_SPC.Name = "tabPage_SPC";
            this.tabPage_SPC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_SPC.Size = new System.Drawing.Size(992, 667);
            this.tabPage_SPC.TabIndex = 1;
            this.tabPage_SPC.Text = "SPC过程监控";
            // 
            // panel_SPC
            // 
            this.panel_SPC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_SPC.Location = new System.Drawing.Point(3, 3);
            this.panel_SPC.Name = "panel_SPC";
            this.panel_SPC.Size = new System.Drawing.Size(986, 661);
            this.panel_SPC.TabIndex = 0;
            // 
            // tabPage_Traceability
            // 
            this.tabPage_Traceability.BackColor = System.Drawing.Color.White;
            this.tabPage_Traceability.Controls.Add(this.panel_Traceability);
            this.tabPage_Traceability.Location = new System.Drawing.Point(4, 29);
            this.tabPage_Traceability.Name = "tabPage_Traceability";
            this.tabPage_Traceability.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Traceability.Size = new System.Drawing.Size(992, 667);
            this.tabPage_Traceability.TabIndex = 2;
            this.tabPage_Traceability.Text = "产品追溯查询";
            // 
            // panel_Traceability
            // 
            this.panel_Traceability.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Traceability.Location = new System.Drawing.Point(3, 3);
            this.panel_Traceability.Name = "panel_Traceability";
            this.panel_Traceability.Size = new System.Drawing.Size(986, 661);
            this.panel_Traceability.TabIndex = 0;
            // 
            // StatisticsAnalysisPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StatisticsAnalysisPage";
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.StatisticsAnalysisPage_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Dashboard.ResumeLayout(false);
            this.tabPage_SPC.ResumeLayout(false);
            this.tabPage_Traceability.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_Dashboard;
        private System.Windows.Forms.TabPage tabPage_SPC;
        private System.Windows.Forms.TabPage tabPage_Traceability;
        private System.Windows.Forms.Panel panel_Dashboard;
        private System.Windows.Forms.Panel panel_SPC;
        private System.Windows.Forms.Panel panel_Traceability;
    }
}
