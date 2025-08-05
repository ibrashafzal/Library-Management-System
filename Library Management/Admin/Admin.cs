using System;
using System.Drawing;
using System.Windows.Forms;
using Library_Management.User_Management;

namespace Library_Management.Admin
{
    public partial class ADMIN : Form
    {
        public ADMIN()
        {
            InitializeComponent();
            label1.Visible = false;
            toolStrip1.Visible = false;

        }

        private void ADMIN_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Empty;
        }
        private void ShowAdminPanel()
        {
            // Hide login controls
            txtUserName.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            labelUserName.Visible = false;
            labelPassword.Visible = false;
            labelLogin.Visible = false;
            // Show admin controls
            toolStrip1.Visible = true;
            label1.Visible = true;
        }
        private void ShowLoginPanel()
        {
            // Hide admin controls
            label1.Visible = false;
            toolStrip1.Visible = false;
            // Show login controls
           txtUserName.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            labelLogin.Visible = true;
            labelUserName.Visible = true;
            labelPassword.Visible = true;
        }  
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Focus();
                return;
            }
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            if (username == "admin" && password == "1234")
            {
                MessageBox.Show("Login successful!");
                ShowAdminPanel();
               
            }
        }
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            User user = new User();
            user.FormClosed += delegate
            {
                this.Show();
            };
            
            this.Hide();
            user.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.FormClosed += delegate
            {
                this.Show();
            };

            this.Hide();
            book.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ShowLoginPanel();
        }
    }
}
