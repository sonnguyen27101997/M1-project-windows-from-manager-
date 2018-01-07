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
    public partial class EditRole : Form
    {
        public EditRole()
        {
            InitializeComponent();
        }
        public string emailUserSelect;
        public EditRole(string email)
        {
            this.emailUserSelect = email;
            InitializeComponent();

        }
        public session1Entities db = new session1Entities();
        void loadOffice()
        {
            var office = from c in db.Offices select c;
            cbOfficle.DataSource = office.ToList();
            cbOfficle.DisplayMember = "Title";
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
        private void EditRole_Load(object sender, EventArgs e)
        {
            loadOffice();
            User userSelect = new User();
            userSelect.Email = emailUserSelect;
            var result = from c in db.Users where c.Email == emailUserSelect select c;
            List<User> dsuser = result.ToList();
            if (dsuser.Count == 1)
            {
                userSelect = dsuser[0];
            }
            txtEmail.Text = userSelect.Email;
            txtFirstName.Text = userSelect.FirstName;
            txtLastName.Text = userSelect.LastName;
            cbOfficle.Text = userSelect.Office.Title;
            if (isAdmin(userSelect.Email))
            {
                rbAdmin.Checked = true;
                rbUser.Checked = false;
            }
            else
            {
                rbAdmin.Checked = false;
                rbUser.Checked = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                User us = db.Users.Where(p => p.Email == emailUserSelect).SingleOrDefault();
                Office office = db.Offices.Where(p => p.Title == cbOfficle.Text).SingleOrDefault();
                us.Email = txtEmail.Text;
                us.FirstName = txtFirstName.Text;
                us.LastName = txtLastName.Text;
                if (rbAdmin.Checked == true)
                {
                    us.RoleID = 1;
                }
                else
                {
                    us.RoleID = 2;
                }
                us.OfficeID = office.ID;
                us.Office = office;
                db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        private void EditRole_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
