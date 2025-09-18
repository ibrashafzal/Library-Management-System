using Library_Management.Reports;
using LMS.DTO;
using LMS.Entities;
using LMS.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        StudentRepo studentRepo = new StudentRepo();
        private StudentReport StudentReport = new StudentReport();
        /// <summary>
        /// Retrieve All Students.
        /// </summary>
        /// <response code="200">List of Student found </response>
        /// <response code="204">List of Student not found</response>
        /// <remarks>This endpoint returns a list of all registered students in the system with their details.</remarks>

        [HttpGet]
        [ProducesResponseType(typeof(StudentData),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status204NoContent)]
        public List<StudentData> Get()
        {
            List<Student> students = studentRepo.GetStudents();
            return students.Select(x => new StudentData(x)).ToList();
        }
        /// <summary>
        /// Get a required student by Id.
        /// </summary>
        /// <param name="id">The unique identifier of the student.</param>
        /// <response code="200">Student found</response>
        /// <response code ="404">Student not exist of that Id</response>
        /// <remarks>This endpoint retrieves the full details of a single
        /// student using their unique studentId.</remarks>

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
        public List<StudentData> GetbyId(int id)
        {
            List<Student> students = studentRepo.GetStudents().Where(s => s.Id == id).ToList();
            return students.Select(x => new StudentData(x)).ToList();
        }
        /// <summary>
        /// Add a new Student.
        /// </summary>
        ///<response code= "201">Student Added Successfully</response>
        ///<response code ="400">Bad Request</response>
        /// <response code="409">Student already exists</response>
        /// <remarks>
        /// This endpoint is used to register a new student in the system by providing their personal and contact details.
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(StudentData), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status409Conflict)]
        public StudentData Add([FromBody] StudentDTO student)
        {
            Student x = student.ToStudent();
            studentRepo.AddNew(x);
            return new StudentData(x);
        }
        /// <summary>
        /// Update a Student.
        /// </summary>
        /// <param name="id">The unique identifier of the Student to update.</param>
        /// <response code = "200"> Student Updated Succesfully</response>
        /// <response code = "404">Invalid Input </response>
        /// <remarks>
        /// This endpoint updates the information of an existing student. You must provide a valid studentId in the path.
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(StudentData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
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
        /// <summary>Delete a Student</summary>
        /// <param name="id">The unique identifier of the Student to delete.</param>
        /// <response code = "204">Student deleted successfully</response>
        /// <response code="404">Student with the specified ID not found</response>
        /// <remarks>
        /// This endpoint removes a student from the system using their unique studentId.
        ///</remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Get all Student in PDF with download links.
        /// </summary>
        /// <remarks>
        /// This endpoint generates and returns a PDF report of all registered students in the system.
        /// </remarks>
        [HttpGet("Student/report")]
        public IActionResult ExportStudentReport()
        {
            var students = studentRepo.GetStudents();
            string filePath = "E:/student Report";

            StudentReport.ExportStudentToPDF(students,filePath, "Student Report", DateTime.Now, DateTime.Now.AddDays(3));

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf", "StudentsReport.pdf");
        }

    }
}
