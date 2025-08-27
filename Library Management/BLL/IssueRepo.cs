using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_Management.DAL;

namespace Library_Management.BLL
{
    public class IssueRepo
    { 
        public IssueDAL issueDAL = new IssueDAL();

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
        public List<Issue> GetIssuesWithDetails()
        {
            return issueDAL.GetIssuesWithDetails();
        }
        public void ReturnBook(int issueId, DateTime returnDate, int fine)
        {
            issueDAL.ReturnBook(issueId, returnDate, fine);
        }
    }
}
