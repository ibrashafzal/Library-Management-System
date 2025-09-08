using LMS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management.DAL
{
    public class IssueDAL
    {
        public void AddIssue(Issue issue)
        {
            using (var db = new LmsContext())
            {
                issue.IssueDate = DateTime.Now;
                issue.ExpectedReturnDate = DateTime.Now.AddDays(2);
                issue.IsReturned = false;

                db.Issues.Add(issue);
                db.SaveChanges();
            }
        }

        public List<Issue> GetIssues()
        {
            using(var db = new LmsContext()) 
            return db.Issues.Include("Student").Include("Book").ToList();
        }
        public void ReturnBook(int issueId, DateTime returnDate, int fine)
        {
            using (var db = new LmsContext())
            {
                var issue = db.Issues.FirstOrDefault(i => i.Id == issueId);
                if (issue != null)
                {
                    issue.ReturnDate = returnDate;
                    issue.Fine = fine;
                    issue.IsReturned = true;
                    db.SaveChanges();
                }
            }
        }

    }
}
