using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Library_Management.BL;
using Library_Management.UI;

namespace Library_Management.Admin
{
    public partial class ADMIN : Form
    {
        
        public UserRepo userRepo = new UserRepo();
        public ADMIN()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin;
        }

        private void ADMIN_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Empty;
            foreach (Control form in this.Controls)
            {
                if (form is MdiClient mdiClient)
                {
                    mdiClient.BackColor = Color.Beige;               
                }
            }
            toolStrip1.Visible = false;
        }
        private void ShowAdminPanel()
        {
            toolStrip1.Visible = true;
            labelLogin.Visible = false;
            labelUserName.Visible = false;
            txtUserName.Visible = false;
            labelPassword.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            checkPassword.Visible = false;
            panel1.Visible = false;

        }
        private void ShowLogin()
        {
            toolStrip1.Visible = false;
            labelLogin.Visible = true;
            labelUserName.Visible = true;
            txtUserName.Visible = true;
            labelPassword.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            checkPassword.Visible = true;
            panel1.Visible = true;
            
        }


        private void toolStripStudent_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is Students)
                {
                    child.Activate();
                    return;
                }
            }
            Students studentForm = new Students();
            studentForm.MdiParent = this;
            studentForm.Show();

        }

        private void toolStripBook_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child is Books)
                {
                    child.Activate();
                    return;
                }

            }
            Books bookfrom = new Books();
            bookfrom.MdiParent = this;
            bookfrom.Show();
        }

        private void toolStripIssue_Click(object sender, EventArgs e)
        {
            foreach (Form child in MdiChildren)
            {
                if (child is Issue)
                {
                    child.Activate();
                    return;
                }
            }
            Library_Management.UI.Issue issueForm = new Library_Management.UI.Issue();
            issueForm.MdiParent = this;
            issueForm.Show();

        }
        private void toolStripLogout_Click(object sender, EventArgs e)
        {
           ShowLogin();
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUserName = txtUserName.Text;
            string enteredPassword = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(enteredUserName) && string.IsNullOrWhiteSpace(enteredPassword))
            {
                MessageBox.Show("UserName and Password are incorrect!");
            }
            bool login = userRepo.GetUsers(enteredUserName, enteredPassword);
            if (login)
            {
                ShowAdminPanel();
                
            }
            else
            {
                MessageBox.Show("Login Failed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
            }
        }

     

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
        }
    }
}



