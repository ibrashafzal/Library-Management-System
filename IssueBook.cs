using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Library_Management.BL;
using Library_Management.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Library_Management.Issue_Book
{
    public partial class IssueBook : Form
    {
        string path = "E://issued.txt";


         public IssueBook()
        {
            InitializeComponent();
           
        }
        public IssueBook(List<BookModel> selectedBooks)
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Rows.Clear();
            
            foreach (var book in selectedBooks)
            {
                dataGridView1.Rows.Add(book.Tittle, book.Quantity);
            }
            UserRepo userRepo = new UserRepo();
            var users = userRepo.GetAll();

            comboStudent.DataSource = users;
            comboStudent.DisplayMember = "Name";   // Show only the name
            comboStudent.ValueMember = "ID";
        }


        private void IssueBook_Load_1(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;

            UserRepo userRepo = new UserRepo();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy hh:mm:ss tt"; ;
        }
              
        private void btnIssue_Click(object sender, EventArgs e)
        {
            string selectedUser = comboStudent.Text;
            //  Build a list of issued books
            List<string> lines = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) 
                    continue;

                string title = row.Cells["Column1"].Value?.ToString();
                string quantity = row.Cells["Column2"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(title))
                {
                    lines.Add($"{selectedUser}|{title}|{quantity}|{DateTime.Now.ToString()}");
                }
            }
            File.AppendAllLines(path, lines);

            MessageBox.Show("Books issued successfully!");
            this.Close();
        }

       
    }
}

