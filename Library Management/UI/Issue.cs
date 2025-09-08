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
    public partial class Issue : Form
    {
        public BookRepo bookRepo = new BookRepo();
        public StudentRepo studentRepo = new StudentRepo();
        public IssueRepo issueRepo = new IssueRepo();
        public UserRepo userRepo = new UserRepo();
        public Report reports = new Report();

        public Issue()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Issue_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadStudent();
            LoadTree();
        }
        public void LoadBooks()
        {
            var books = bookRepo.GetBooks();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = books;
        }
        public void LoadStudent()
        {
            var students = studentRepo.GetStudents();
            comboStudent.DataSource = null;
            comboStudent.DataSource = students;
            comboStudent.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboStudent.SelectedItem == null || dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a student and a book.");
                return;
            }
            var selectedStudent = (Student)comboStudent.SelectedItem;
            var selectedBook = (Book)dataGridView1.CurrentRow.DataBoundItem;
            int quantity = (int)numericUpDown1.Value;
            if (quantity > selectedBook.AvailableQuantity)
            {
                MessageBox.Show("Not enough stock.");
                return;
            }

            issueRepo.IssueBook(selectedStudent.Id, selectedBook.Id, quantity);

            bookRepo.DecreaseAvailable(selectedBook.Id, quantity);

            LoadBooks();
            LoadTree();

            MessageBox.Show("Book issued successfully!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
        private void LoadTree()
        {
            treeView1.Nodes.Clear();
            var issues = issueRepo.GetAllIssues();
            var pendingIssues = issues.Where(i => !i.IsReturned).ToList();

            List<string> studentNames = new List<string>();
            foreach (var i in pendingIssues)
            {
                if (!studentNames.Contains(i.Student.Name))
                    studentNames.Add(i.Student.Name);
            }

            foreach (var studentName in studentNames)
            {
                TreeNode studentNode = new TreeNode(studentName);

                foreach (var issue in pendingIssues)
                {
                    if (issue.Student.Name == studentName)
                    {
                        string childText = issue.Book.Tittle + " | " + issue.IssueQuantity + " | " + issue.IssueDate.ToString("dd-MMM-yyyy");

                        TreeNode childNode = new TreeNode(childText);

                        childNode.Tag = issue.Id;
                        studentNode.Nodes.Add(childNode);
                    }
                }

                treeView1.Nodes.Add(studentNode);
            }
        }
        private int GetSelectedIssueId()
        {
            var selectedNode = treeView1.SelectedNode;

            if (selectedNode == null)
                return -1; 

            if (selectedNode.Parent == null)
                return -1; 

            if (selectedNode.Tag == null)
                return -1; 

            return (int)selectedNode.Tag;
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            int issueId = GetSelectedIssueId();
            if (issueId == -1)
            {
                MessageBox.Show("Select a book to return");
                return;
            }

            DateTime returnDate = dateTimePicker1.Value;
            var issue = issueRepo.GetAllIssues().FirstOrDefault(i => i.Id == issueId);
            if (issue == null) return;

            int fine = 0;

            if (returnDate > issue.ExpectedReturnDate)
            {
                int daysLate = (returnDate - issue.ExpectedReturnDate.Value).Days;
                fine = 100 * daysLate * issue.IssueQuantity;
            }

            issueRepo.ReturnBook(issueId, returnDate, fine);
            var book = bookRepo.GetBooks().FirstOrDefault(b => b.Id == issue.BookId);
            bookRepo.Increase(issue.BookId, issue.IssueQuantity);

            if (book.AvailableQuantity > book.Quantity)
            {
                MessageBox.Show("Available quantity cannot exceed total stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            treeView1.SelectedNode.Remove();
            for (int i = treeView1.Nodes.Count - 1; i >= 0; i--)
            {
                if (treeView1.Nodes[i].Nodes.Count == 0)
                    treeView1.Nodes.RemoveAt(i);
            }

            MessageBox.Show($"Book returned successfully.");

            LoadBooks();
        }

       
        private void Issue_LocationChanged(object sender, EventArgs e)
        {
        
            if (this.MdiParent is ADMIN parent)
            {
                userRepo.FormLocation(this);
            }
        
        }
         
        private void btnPDF_Click(object sender, EventArgs e)
        {
            string pdfPath = @"E:\Issue Book.pdf";
            string reportTitle = "Issue Book";
            float[] columnWidths = {3f,2f,2f};
            string logoPath = @"E:/logo.png";
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(2);

            Report.ExportGridToPDF(dataGridView1, pdfPath, reportTitle, startDate, endDate, columnWidths, logoPath);   
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = issueRepo.Search(searchText);
        }

        private void txtTreeSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            var issue = issueRepo.GetAllIssues();
            MessageBox.Show(issue.ToString());
           
           
        }
    }
}

