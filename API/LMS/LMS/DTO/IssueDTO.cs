using LMS.Entities;

namespace LMS.DTO
{
    public class IssueDTO
    {
        public int Id { get; set; }
        public int studentId { get;  set; }
        public int BookId { get; set; }
        public int issueQuantity { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Fine { get; set; }
        public static IssueDTO FromIssue(Issue issue)
        {
            return new IssueDTO
            {
                Id = issue.Id,
                studentId = issue.StudentId,
                BookId = issue.BookId,
                issueQuantity = issue.IssueQuantity,
                IssueDate = issue.IssueDate,
                ReturnDate = issue.ReturnDate,
                Fine = issue.Fine ?? 0
            };
        }
    }
}
