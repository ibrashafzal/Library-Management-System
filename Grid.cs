using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Library_Management.BL;
using Library_Management.Model;

namespace Library_Management
{
    public partial class Grid : Form
    {
        public BookRepo repo { get; set; }
        public Grid()
        {
            InitializeComponent();
            repo = new BookRepo();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Book form = new Book(); 
            form.Show();
            List<Model.BookModel> books = repo.GetAll();
            //dataGridView.DataSource = books; // Bind the DataGridView to the list of books
        }
       
        }
    
    }

