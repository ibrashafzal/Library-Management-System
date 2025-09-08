using LMS.Entities;

namespace LMS.DTO
{
    public class StudentDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Class { get; set; } = null!;
        public string Contact { get; set; } = null!;

        public Student ToStudent()
        {
            return new Student
            {
                Name = this.Name,
                Email = this.Email,
                Class = this.Class,
                Contact = this.Contact
            };
        }
    }
}
