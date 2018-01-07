using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M1.Model;
namespace M1
{
    public partial class Form1 : Form
    {
        public session1Entities db = new session1Entities();
        public int countTime = 0;
        int time = 10;
        public Form1()
        {
            time = 10;
            InitializeComponent();


        }
        public bool checkLogin(string username, string password)
        {
            var result = from a in db.Users where a.Email == username && a.Password ==password select a;
            if(result.ToList().Count==1)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        User ulogin;
        bool isAdmin(string UserName)
        {
            ulogin = db.Users.Where(p => p.Email == UserName).SingleOrDefault();
            if(ulogin.RoleID == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            
            if(checkLogin(txtUserName.Text, txtPassword.Text)==true)
            {
                if(isAdmin(txtUserName.Text))
                {
                    this.Hide();
                    Loginscreen lo = new Loginscreen(ulogin);
                    lo.Show();
                }
               else
                {
                    this.Hide();
                    UserLoginFm ul = new UserLoginFm(ulogin);
                    ul.Show();
                }
            }
            else
            {
                if(countTime < 3 )
                {
                    lbNotification.Text = "Bạn nhập sai mật khẩu";
                    countTime++;
                }
                else
                {
                    btnLogin.Enabled = false;
                    timer1.Start();
                }
                
            }
        
            //var resul = from a in db.Users select a;
            //dataGridView1.DataSource = resul.ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time--;
            lbNotification.Text = "Vui lòng đợi trong 10s : " + time.ToString();
            if(time == 0)
            {
                btnLogin.Enabled = true;
                lbNotification.Text = "";
                time = 10;
                countTime = 0;
                timer1.Stop();
            }
        }
    }
}
