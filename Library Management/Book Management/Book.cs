using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Library_Management.BL;
using Library_Management.Issue_Book;
using Library_Management.Model;
using Library_Management.Return_Book;

namespace Library_Management
{
    public partial class Book : Form
    {
        public BookRepo repo { get; set; }
        private BookRepo bookRepo = new BookRepo();
        List<BookCategory> bookList = Enum.GetValues(typeof(BookCategory)).Cast<BookCategory>().ToList();
        public int Index = -1;
        private BookModel _selectedBook = null;
       
        public Book()
        {
            InitializeComponent();
            comboCat.DataSource = bookList;
            comboCat.DropDownStyle = ComboBoxStyle.DropDownList;
            repo = new BookRepo();
            string Category = comboCat.SelectedItem?.ToString() ?? string.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true;
            LoadGrid();
        }

        
        private void LoadGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bookRepo.GetAll(); // Bind list to grid
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTittle.Text))
            {
                MessageBox.Show("Please fill Book Title!", "Validation", MessageBoxButtons.OK);
                txtTittle.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtAuthor.Text))
            {
                MessageBox.Show("Please fill Author Name!", "Validation", MessageBoxButtons.OK);
                return;
            }
            else if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Please fill Quantity!", "Validation", MessageBoxButtons.OK);
                return;
            }
            else
            {
                MessageBox.Show("Book Added Successfully!", "Success", MessageBoxButtons.OK);
            }

            // Here Book is the model/DTO
            var book = new BookModel
            {
                Tittle = txtTittle.Text,
                Author = txtAuthor.Text,
                Quantity = int.TryParse(txtQuantity.Text, out int qty) ? qty : 0,
                Category = comboCat.SelectedItem?.ToString() ?? ""
            };

            if (Index >= 0)
            {
                bookRepo.EditBooks(Index, book);
                Index = -1;
            }
            else
            {
                bookRepo.AddBook(book);
            }

            LoadGrid();
            tabControl1.SelectedTab = tabPage2;
            txtTittle.Clear();
            txtAuthor.Clear();
            txtQuantity.Clear();

            
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.CurrentRow == null) return;
            
            Index = dataGridView1.CurrentRow.Index;
            BookModel selectedBook = bookRepo.GetAll()[Index];

            txtTittle.Text = selectedBook.Tittle;
            txtAuthor.Text = selectedBook.Author;
            txtQuantity.Text = selectedBook.Quantity.ToString();
            comboCat.SelectedItem = selectedBook.Category;
            tabControl1.SelectedTab = tabPage2;
            LoadGrid();
            
        }
     
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int index = dataGridView1.CurrentRow.Index;
                bookRepo.DeleteBooks(index);
                LoadGrid();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();
            dataGridView1.DataSource = bookRepo.Search(searchText);
        }

        private void issueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedBooks = new List<BookModel>();

            // Get selected rows from the DataGridView
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.DataBoundItem is BookModel book)
                {
                    selectedBooks.Add(book);
                }
            }

            // Open IssueBook and pass the selected books
            IssueBook issueBookForm = new IssueBook(selectedBooks);
            issueBookForm.Show();
        }
    }
}    


    
