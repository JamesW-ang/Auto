using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using COTUI.数据库.Services;
using COTUI.数据库.Models;
using COTUI.通用功能类;

namespace COTUI.权限管理
{
    public partial class Permission_Management : CCSkinMain
    {
        private UserService _userService = new UserService();
        private Logger _logger = Gvar.Logger.GetInstance();

        public Permission_Management()
        {
            InitializeComponent();
        }
        string _username = "";
        string _password = "";
        private void Permission_Management_Load(object sender, EventArgs e)
        {

        }

        private void Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("用户名和密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 使用数据库验证用户
                if (_userService.ValidateUser(username, password))
                {
                    // 获取用户信息
                    UserModel user = _userService.GetUser(username);
                    
                    if (user != null && user.IsEnabled)
                    {
                        Gvar.User = username;
                        Gvar.Logger.Log($"用户 {username} ({user.RealName}) 登录成功");
                        MessageBox.Show($"欢迎，{user.RealName ?? username}！", "登录成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户已被禁用，请联系管理员！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Gvar.Logger.Log($"用户 {username} 尝试登录失败：账户已禁用");
                    }
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Gvar.Logger.Log($"用户 {username} 登录失败：密码错误");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"登录失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Gvar.Logger.ErrorException(ex, "登录过程发生异常");
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            _username = cobx_User.Text;
            _password = txt_Password.Text;
            Login(_username, _password);
        }
    }
}
