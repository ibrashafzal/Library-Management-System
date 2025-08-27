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
using Library_Management.DAL;

namespace Library_Management.UI
{
    public partial class Issue : Form
    {
        public BookRepo bookRepo = new BookRepo();
        public StudentRepo studentRepo = new StudentRepo();
        public IssueRepo issueRepo = new IssueRepo();
        public UserRepo userRepo = new UserRepo();
        public Issue()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Issue_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadStudent();
            LoadIssuedBooksInTree();
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

            // 1. Save issue record in DB
            issueRepo.IssueBook(selectedStudent.Id, selectedBook.Id, quantity);

            // 2. Decrease available stock permanently
            bookRepo.DecreaseAvailable(selectedBook.Id, quantity);

            // 3. Reload grid to show updated AvailableQuantity
            LoadBooks();

            // 4. Reload TreeView
            LoadIssuedBooksInTree();

            MessageBox.Show("Book issued successfully!");

            // Reload TreeView
            LoadIssuedBooksInTree();

        }
        private void LoadIssuedBooksInTree()
        {
            treeView1.Nodes.Clear();
            var issues = issueRepo.GetIssuesWithDetails();
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

                        // 🔹 Create child node
                        TreeNode childNode = new TreeNode(childText);

                        // 🔹 Store Issue.Id in Tag for return functionality
                        childNode.Tag = issue.Id;

                        // 🔹 Add to student node
                        studentNode.Nodes.Add(childNode);
                    }
                }

                treeView1.Nodes.Add(studentNode);
            }

            treeView1.ExpandAll();
        }
        private int GetSelectedIssueId()
        {
            var selectedNode = treeView1.SelectedNode;

            if (selectedNode == null)
                return -1; // Nothing selected

            if (selectedNode.Parent == null)
                return -1; // Parent node (student) selected, not a book

            if (selectedNode.Tag == null)
                return -1; // Tag not set

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

            // Get issue details
            var issue = issueRepo.GetIssuesWithDetails().FirstOrDefault(i => i.Id == issueId);
            if (issue == null) return;

            int fine = 0;

            // Calculate fine if late
            if (returnDate > issue.ExpectedReturnDate)
            {
                int daysLate = (returnDate - issue.ExpectedReturnDate.Value).Days;
                fine = 100 * daysLate * issue.IssueQuantity;
            }

            // 1. Update Issue table
            issueRepo.ReturnBook(issueId, returnDate, fine);
            var book = bookRepo.GetBooks().FirstOrDefault(b => b.Id == issue.BookId);
            // 2. Update AvailableQuantity in Book table
            bookRepo.IncreaseAvailable(issue.BookId, issue.IssueQuantity);
            if (book.AvailableQuantity > book.Quantity)
            {
                MessageBox.Show("Available quantity cannot exceed total stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Remove from TreeView
            treeView1.SelectedNode.Remove();
            for (int i = treeView1.Nodes.Count - 1; i >= 0; i--)
            {
                if (treeView1.Nodes[i].Nodes.Count == 0)
                    treeView1.Nodes.RemoveAt(i);
            }

            MessageBox.Show($"Book returned successfully. Fine: {fine}");

            // Optional: reload grid to show updated AvailableQuantity
            LoadBooks();
        }

       
        private void Issue_LocationChanged(object sender, EventArgs e)
        {
        
            if (this.MdiParent is ADMIN parent)
            {
                userRepo.FormLocation(this);
            }
        
        }
    }
}

