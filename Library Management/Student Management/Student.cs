using System;
using System.Windows.Forms;
using Library_Management.BL;
using Library_Management.Model;
using Library_Management.Reports;

namespace Library_Management.User_Management
{
    public partial class User : Form
    {
        public UserRepo userRepo = new UserRepo();
        private int? UserId = null;

        public User()
        {
            InitializeComponent();
           
        }
        private void User_Load(object sender, EventArgs e)
        {           
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            loadGrid();
            GetRole();
        }
        private void loadGrid()
        {
                  

        }
        private int selectedUserId = -1;

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                txtEmail.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtClass.Text))
            {
                txtClass.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtContact.Text))
            {
                txtContact.Focus();
                return;
            }
            if (selectedUserId != -1) // Editing
            {
                UserModel updatedUser = new UserModel
                {
                    Id = selectedUserId,
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Class = txtClass.Text,
                    Contact = txtContact.Text
                };

                userRepo.UpdateUser(updatedUser); 
                loadGrid(); 
                selectedUserId = -1;
                btnSave.Text = "Save";
                txtName.Clear();
                txtEmail.Clear();
                txtClass.Clear();
                txtContact.Clear();
            }
            else
            {
                
                UserModel newUser = new UserModel
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Class = txtClass.Text,
                    Contact = txtContact.Text
                };

                userRepo.AddUser(newUser); 
                loadGrid(); 
            }
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var row = dataGridView2.SelectedRows[0];
                selectedUserId = (int)row.Cells["Id"].Value;
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtClass.Text = row.Cells["Class"].Value.ToString();
                txtContact.Text = row.Cells["Contact"].Value.ToString();

                btnSave.Text = "Update";
                tabControl1.SelectedTab = tabPage1;
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                int id = (int)dataGridView2.CurrentRow.Cells["Id"].Value;
                userRepo.Deleteuser(id);
                loadGrid();
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

     
        }

        private void User_LocationChanged(object sender, EventArgs e)
        {           
            userRepo.FormLocation(this);

        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = @"E:\ Member Report.pdf";
            string logoPath = @"E:\logo.png";
            float[] widths = { 1f, 3f, 4f, 1f, 2f };

            Report.ExportGridToPDF(dataGridView2, savePath, "Member Report", DateTime.Now, DateTime.Now.AddMonths(-1), widths, logoPath);

        }
        private string _role;
        public void SetUserRole(string role)
        {
            _role = role;
        }
        public void GetRole()
        {

            if (_role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                deleteToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                tabControl1.TabPages.Remove(tabPage1);
            }
            else if (_role.Equals("Librarian", StringComparison.OrdinalIgnoreCase))
            {
                deleteToolStripMenuItem.Enabled = false;
            }

        }
    }
}



