using LMS.DTO;
using LMS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentRepo studentRepo = new StudentRepo();
        [HttpGet]
        public List<StudentData> Get()
        {
            List<Student> students = studentRepo.GetStudents();
            return students.Select(x => new StudentData(x)).ToList();
        }
        [HttpGet("{id}")]
        public List<StudentData> GetbyId(int id)
        {
            List<Student> students = studentRepo.GetStudents().Where(s => s.Id == id).ToList();
            return students.Select(x => new StudentData(x)).ToList();
        }
        [HttpPost]
        public StudentData Add([FromBody] StudentDTO student)
        {
            Student x = student.ToStudent();
            studentRepo.AddNew(x);
            return new StudentData(x);
        }
        [HttpPut]
        public ActionResult<StudentData> Update(int id, [FromBody]StudentDTO student)
        {
            var existingStudent = studentRepo.GetStudents().FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Class = student.Class;
            existingStudent.Contact = student.Contact;
            studentRepo.Update(existingStudent);
            return Ok(new StudentData(existingStudent));
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var studentid = studentRepo.GetStudents().FirstOrDefault(s => s.Id == id);
            if (studentid == null)
            {
                return NotFound();
            }
            studentRepo.Delete(id);
            return NoContent();
        }

    }
}
