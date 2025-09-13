using LMS.Entities;

namespace LMS.DTO
{
    public class IssueDTO
    {
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public int IssueQuantity { get; set; }
    }
}
