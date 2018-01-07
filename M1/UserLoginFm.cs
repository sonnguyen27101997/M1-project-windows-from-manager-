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
    public partial class UserLoginFm : Form
    {
        public UserLoginFm()
        {
            InitializeComponent();
        }
        User ulogin;
        UserLogReason ulr = new UserLogReason();
        public UserLoginFm(User userlogin)
        {
            this.ulogin = userlogin;
            InitializeComponent();
            ulr.timelogin = DateTime.Now;

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ulr.timelogout = DateTime.Now;
            ulr.insertUserLog(ulogin.Email);
            Application.Exit();
        }
        public static session1Entities db = new session1Entities();
        private string converDate(DateTime? da)
        {
  
           
            return (da.Value.Day.ToString() + "/" + da.Value.Month.ToString() + "/" + da.Value.Year.ToString());
        }

        private string converTime(DateTime? da )
        {
            return (da.Value.Hour.ToString()+":"+da.Value.Minute.ToString());
        }
        private void UserLoginFm_Load(object sender, EventArgs e)
        {
            lbhello.Text = "Hello "+ulogin.FirstName+", Welcome to AMONIC Airlines.";
            var log = from c in db.UserLogs where c.Email == ulogin.Email select new { Date = c.TimeLogin, LginTime = c.TimeLogin , LgoutTime = c.TimeLogout, timeOnSys = (c.TimeLogout.Value.Hour - c.TimeLogin.Value.Hour), Describ = c.Describ};
            dataGridView1.DataSource = log.ToList();
        }

        private void UserLoginFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
