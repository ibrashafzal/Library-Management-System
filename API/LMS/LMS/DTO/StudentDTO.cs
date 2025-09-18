using LMS.Entities;

namespace LMS.DTO
{
    public class StudentDTO
    {
        /// <summary>Name of Student </summary>
        /// <example>John</example>
        public string Name { get; set; }
        /// <summary>Enail of Student </summary>
        /// <example>John@example.com</example>
        public string Email { get; set; }
        /// <summary>Name of Student </summary>
        /// <example>A</example>
        public string Class { get; set; }
        /// <summary>Class of Student </summary>
        /// <example>3334-000323</example>
        public string Contact { get; set; } 

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
