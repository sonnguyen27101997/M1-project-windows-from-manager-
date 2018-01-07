using M1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace M1
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }
        public session1Entities db = new session1Entities();
        private void AddUser_Load(object sender, EventArgs e)
        {
            var dataOffice = from c in db.Offices select c;
            Office temp = new Office();
            temp.ID = 0;
            temp.Title = "Office Name";
            List<Office> dsOffice = dataOffice.ToList();
            dsOffice.Insert(0, temp);
            cbOffice.DataSource = dsOffice;
            cbOffice.DisplayMember = "Title";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                Office uof = new Office();
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;
                user.RoleID = 2;
                
                if (cbOffice.SelectedIndex != 0)
                {
                    user.OfficeID = cbOffice.SelectedIndex;
                    uof = db.Offices.Find(user.OfficeID);
                    user.Office = uof;
                    
                    

                }
                user.Role = db.Roles.Find(user.RoleID);
                user.LastName = txtLastName.Text;
                user.FirstName = txtFirstName.Text;
                user.Birthdate = dtBrithDate.Value;
                user.Active = true;
                db.Users.Add(user);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "warning");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
