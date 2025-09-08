using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.BL;
using Library_Management.DAL;
using Library_Management.UI;

namespace Library_Management.BLL
{
    public class IssueRepo
    { 
        public IssueDAL issueDAL = new IssueDAL();
        public BookDAL bookDAL = new BookDAL();
        public BookRepo bookRepo = new BookRepo();

        public void IssueBook(int studentId, int bookId, int quantity)
        {
            Issue issue = new Issue
            {
                StudentId = studentId,
                BookId = bookId,
                IssueQuantity = quantity
            };
            issueDAL.AddIssue(issue);
        }
        public List<Issue> GetAllIssues()
        {
            return issueDAL.GetIssues();
        }
        public void ReturnBook(int issueId, DateTime returnDate, int fine)
        {
            issueDAL.ReturnBook(issueId, returnDate, fine);
        }
        public List<Book> Search(string searchText)
        { 
            var books = bookRepo.GetBooks();

            if (string.IsNullOrWhiteSpace(searchText))
                return books;

            searchText = searchText.ToLower();

            var filtered = books.Where(b =>!string.IsNullOrEmpty(b.Tittle) && b.Tittle.ToLower().Contains(searchText)
            ).ToList();

            return filtered;
        }
       
    }
}
