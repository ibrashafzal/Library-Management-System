using LMS.Entities;

namespace LMS.DTO
{
    public class IssueData1
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public int IssueQuantity { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public int Fine { get; set; }

        // mapping helper
        public static IssueData1 FromEntity(Issue issue)
        {
            return new IssueData1
            {
                Id = issue.Id,
                StudentId = issue.StudentId,
                BookId = issue.BookId,
                IssueQuantity = issue.IssueQuantity,
                IssueDate = issue.IssueDate,
                ExpectedReturnDate = (DateTime)issue.ExpectedReturnDate,
                ReturnDate = issue.ReturnDate,
                IsReturned = issue.IsReturned,
                Fine = issue.Fine ?? 0
            };
        }
    }
}
