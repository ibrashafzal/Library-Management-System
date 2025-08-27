using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library_Management.Admin;
using Library_Management.BL;
using Library_Management.BLL;
using Library_Management.Reports;

namespace Library_Management.UI
{
    public partial class Students : Form
    {
        public StudentRepo studentRepo = new StudentRepo();
        public Report report = new Report();
        public UserRepo userRepo = new UserRepo();
        public Students()
        {
            InitializeComponent();
        }

        private void Student_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
        }
        private void LoadGrid()
        {
            var student = studentRepo.GetStudents();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = student;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow)
            {
                var student = new Student
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Class = txtClass.Text,
                    contact = txtContact.Text,
                    CreatedAt = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["CreatedAt"].Value)
                };                
                studentRepo.Update(student);
            }
            else
            {
                var student = new Student
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Class = txtClass.Text,
                    contact = txtContact.Text,
                    CreatedAt = DateTime.Now
                };
                studentRepo.AddNew(student);
                tabControl1.SelectedTab = tabPage1;
            }
            LoadGrid();
            txtName.Clear();
            txtEmail.Clear();
            txtClass.Clear();
            txtContact.Clear();
           
           
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            studentRepo.Delete(id);
            LoadGrid();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1) 
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                txtName.Text = row.Cells[1].Value?.ToString(); 
                txtEmail.Text = row.Cells[2].Value?.ToString(); 
                txtClass.Text = row.Cells[3].Value?.ToString(); 
                txtContact.Text = row.Cells[4].Value?.ToString();
                tabControl1.SelectedTab = tabPage2;
                btnSave.Text = "Update";

            }
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdfPath = @"E:\Student Report.pdf";
            string reportTitle = "Library Student Report";
            DateTime startDate = new DateTime();
            DateTime endDate = DateTime.Now;
            float[] columnWidths = { 1f, 3f, 4f, 1f, 3f, 3f };
            string logoPath = @"E:\logo.png";
            Report.ExportGridToPDF(dataGridView1, pdfPath, reportTitle, startDate, endDate, columnWidths, logoPath); ;

        }

        private void Students_LocationChanged(object sender, EventArgs e)
        {
            if (this.MdiParent is ADMIN parent)
            {
               userRepo.FormLocation(this);
            }
        }

       
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            dataGridView1.DataSource = studentRepo.Search(searchText);
        }
    }
}
