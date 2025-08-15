using System;
using System.Windows.Forms;
using Library_Management.BL;
using Library_Management.Model;

namespace Library_Management.User_Management
{
    public partial class User : Form
    {
        public UserRepo repo { get; set; }
        public UserRepo userRepo = new UserRepo();
        private int? UserId = null; 

        public User()
        {
            InitializeComponent();
            repo = new UserRepo();

        }
        private void User_Load(object sender, EventArgs e)
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            loadGrid();


        }
        private void loadGrid()
        {          
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = userRepo.GetAll(); 
        }
             

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please Enter Name!", "Validation", MessageBoxButtons.OK);
                return;
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please Enter Email Address!", "Validation", MessageBoxButtons.OK);
                return;
            }
            else if (string.IsNullOrEmpty(txtClass.Text))
            {
                MessageBox.Show("Please Enter Class Name!", "Validation", MessageBoxButtons.OK);
                return;
            }
            else if (string.IsNullOrEmpty(txtContact.Text))
            {
                MessageBox.Show("Please Enter Contact Number!", "Validation", MessageBoxButtons.OK);
                return;
            }
            
            UserModel user = new UserModel
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Class = txtClass.Text,
                Contact = txtContact.Text
            };
            if (UserId.HasValue)
            {
                userRepo.EditUser(UserId.Value, user);
                UserId = null;
                MessageBox.Show("User Updated Successfully!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                userRepo.AddUser(user);
                MessageBox.Show("User Added Successfully!", "Success", MessageBoxButtons.OK);
               
            }
               
            loadGrid();
            txtName.Clear();
            txtEmail.Clear();
            txtClass.Clear();
            txtContact.Clear();

        }
        
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (dataGridView2.CurrentRow == null) return;

            
            int userId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);
            UserModel selectedUser = userRepo.GetAll().Find(u => u.Id == userId);
            if (selectedUser != null)
            {
                
                txtName.Text = selectedUser.Name;
                txtEmail.Text = selectedUser.Email;
                txtClass.Text = selectedUser.Class;
                txtContact.Text = selectedUser.Contact;
                UserId = selectedUser.Id;
                tabControl1.SelectedTab = tabPage1;
            }
            loadGrid();
        }
           
            
        

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (dataGridView2.CurrentRow == null) return;

            int UserId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);
            userRepo.DeleteUser(UserId);
            loadGrid();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();
            dataGridView2.DataSource = userRepo.Search(searchText);
        }
        
    
    }
}



