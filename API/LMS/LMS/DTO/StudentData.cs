using LMS.Entities;

namespace LMS.DTO
{
    public class StudentData : StudentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public StudentData(Student x)
        {
            Id = x.Id;
            Name = x.Name;
            Email = x.Email;
            Class = x.Class;
            Contact = x.Contact;
            CreatedAt = x.CreatedAt ?? DateTime.Now;
        }
    }
}
