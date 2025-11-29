using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTUI
{
    public partial class SettingForm : Form
    {
        //声明tab页面
        private MQTTForm mqttForm;
        public SettingForm()
        {
            InitializeComponent();
            InitializeTabs();
        }

        private void InitializeTabs()
        {
            // 初始化MQTT设置页面
            mqttForm = new MQTTForm();
            mqttForm.TopLevel = false;
            mqttForm.Dock = DockStyle.Fill;
            tabPage4.Controls.Add(mqttForm);
            mqttForm.Show();
        }
    }
}
