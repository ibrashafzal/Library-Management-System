using System;
using System.Net.Http;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using System.Xml.Linq;
using Library_Management.Admin;
using Library_Management.BL;
using Library_Management.Reports;


namespace Library_Management.UI
{
    public partial class Books : Form
    {
        public BookRepo bookRepo = new BookRepo();
        public Report report = new Report();
        public UserRepo userRepo = new UserRepo();
        
        
        public Books()
        {
            InitializeComponent();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            LoadGrid();
          
        }
        public void LoadGrid()
        {
            var book = bookRepo.GetBooks();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = book;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !dataGridView1.CurrentRow.IsNewRow)
            {
                var book = new Book
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    Tittle = txtTittle.Text,
                    Author = txtAuthor.Text,
                    Quantity = Convert.ToInt32(txtQuantity.Text),
                    Category = comboCategory.Text

                };
                bookRepo.UpdateBook(book);

            }
            else
            {
                var qty = Convert.ToInt32(txtQuantity.Text);
                var book = new Book
                {
                    Tittle = txtTittle.Text,
                    Author = txtAuthor.Text,
                    Quantity =qty,
                    Category = comboCategory.Text,
                    CreatedAt = DateTime.Now,
                    AvailableQuantity = qty


                };
                bookRepo.AddNew(book);
                btnSave.Text = "Save";
            }
            LoadGrid();
            txtTittle.Clear();
            txtAuthor.Clear();
            txtQuantity.Clear();
            tabControl1.SelectedTab = tabPage1;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
            }
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);       
            bookRepo.Delete(id);
            LoadGrid();
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pdfPath = @"E:\Book Report.pdf";
            string reportTitle = "Library Book Report";
            DateTime startDate = new DateTime();
            DateTime endDate = DateTime.Now;
            float[] columnWidths = { 1f, 3f, 3f, 2f,3f,3f };
            string logoPath = @"E:\logo.png";
            Report.ExportGridToPDF(dataGridView1,pdfPath,reportTitle,startDate,endDate,columnWidths,logoPath);


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                txtTittle.Text = row.Cells[1].Value?.ToString();
                txtAuthor.Text = row.Cells[2].Value?.ToString();
                txtQuantity.Text = row.Cells[3].Value?.ToString();
                comboCategory.Text = row.Cells[4].Value?.ToString();
                tabControl1.SelectedTab = tabPage2;
                btnSave.Text = "Update";

            }
        }

        private void dataGridView1_LocationChanged(object sender, EventArgs e)
        {
            if (this.MdiParent is ADMIN parent)
            {
                userRepo.FormLocation(this);
            }
        }

        private void Books_LocationChanged(object sender, EventArgs e)
        {
            if (this.MdiParent is ADMIN parent)
            {
                userRepo.FormLocation(this);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            dataGridView1.DataSource = bookRepo.Search(searchText);
        }
    }
}
