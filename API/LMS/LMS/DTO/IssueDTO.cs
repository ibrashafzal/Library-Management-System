using LMS.Entities;

namespace LMS.DTO
{
    public class IssueDTO
    {
        /// <summary>Student ID</summary>
        /// <example>01</example>
        public int StudentId { get; set; }
        /// <summary>Book ID</summary>
        /// <example>1</example>
        public int BookId { get; set; }
        /// <summary>How many books you want to issue</summary>
        /// <example>5</example>
        public int IssueQuantity { get; set; }
    }
}
