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
    public partial class Loginscreen : Form
    {
        public Loginscreen()
        {
            InitializeComponent();
        }
        User uslogin;
        public Loginscreen(User ulogin)
        {
            this.uslogin = ulogin;
            
            InitializeComponent();
        }
        public session1Entities db = new session1Entities();
        public string emailUserSelect = "";
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private bool checkActive(string email)
        {
            var result = from c in db.Users.Where(p => p.Email == email) select c;
            if (result.ToList()[0].Active == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool isAdmin(string email)
        {
            var result = from c in db.Users.Where(p => p.Email == email) select c;
            if (result.ToList()[0].RoleID == 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        void loadDataGridview()
        {
            var name = from c in db.Users select new { FirstName = c.FirstName, LastName = c.LastName, Age = (DateTime.Now.Year - c.Birthdate.Value.Year), Title = c.Role.Title, Email = c.Email, OfTitle = c.Office.Title };

            dataGridView1.DataSource = name.ToList();

            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string email = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    if (checkActive(email) && isAdmin(email))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                    else if (checkActive(email) && isAdmin(email) == false)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        void loadDataGridviewForOffice(string OfficeName)
        {
            var name = from c in db.Users where c.Office.Title  == OfficeName select new { FirstName = c.FirstName, LastName = c.LastName, Age = (DateTime.Now.Year - c.Birthdate.Value.Year), Title = c.Role.Title, Email = c.Email, OfTitle = c.Office.Title };

            dataGridView1.DataSource = name.ToList();

            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string email = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    if (checkActive(email) && isAdmin(email))
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    }
                    else if (checkActive(email) && isAdmin(email) == false)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }
        void loadOffice()
        {
            var office = from c in db.Offices select c;
            List<Office> dsof = office.ToList();
            Office abc = new Office();
            abc.Title = "ALl office";
            abc.ID = 0;
            dsof.Insert(0, abc);
            cbOffice.DataSource = dsof;
            cbOffice.DisplayMember = "Title";
        }
        private void Loginscreen_Load(object sender, EventArgs e)
        {
            try
            {
                loadDataGridview();
                loadOffice();
                loadDataGridview();
            }catch(Exception ex)
            {
                ErrorRepost er = new ErrorRepost();
                er.Show();
            }
            

        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddUser addu = new AddUser();
            addu.ShowDialog();
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Bạn muốn thoát chứ ??", "Cảnh báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                UserLogReason ul = new UserLogReason();
                ul.insertUserLog(uslogin.Email);
                Application.Exit();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void cbOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOffice.SelectedIndex != 0)
            {

                loadDataGridviewForOffice(cbOffice.Text);
            }
            else
            {

                Loginscreen_Load(sender, e);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditRole rl = new EditRole(emailUserSelect);
            rl.ShowDialog();
            this.Show();
            Loginscreen_Load(sender, e);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.DataSource != null)
                {
                    int index = dataGridView1.CurrentCell.RowIndex;
                    emailUserSelect = dataGridView1.Rows[index].Cells[4].Value.ToString();


                }
                
                
                
                
            }catch(Exception ex)
            {
                
            }
           
        }
        
        void updateRoleUser(string email)
        {
            User userUpadeRole = db.Users.Where(p => p.Email == email).SingleOrDefault();
            if (userUpadeRole.Active == true)
            {
                userUpadeRole.Active = false;
            }
            else
            {
                userUpadeRole.Active = true;
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dataGridView1.DataSource != null)
                {
                    int index = dataGridView1.CurrentCell.RowIndex;
                    emailUserSelect = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    updateRoleUser(emailUserSelect);
                    Loginscreen_Load(sender, e);

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
