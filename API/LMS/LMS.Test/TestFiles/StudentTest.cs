using LMS.Controllers;
using LMS.DTO;
using NUnit.Framework;

namespace LMS.Test.TestFiles
{
      public class StudentTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll()
        {
            //Arrange
            StudentController studentApi = new StudentController();
            //Act
            List<StudentData> students = studentApi.Get();
            //Assert
            Assert.IsNotNull(students,"No Student found!");
            Assert.IsTrue(students.Count > 0, "No Student found in the list");
        }      

        [Test]
        public void GetById()
        {
            //Arrange
            StudentController studentapi = new StudentController();
            int Id = 2;
            //Act
            var result = studentapi.GetbyId(Id);
            //Assert
            Assert.IsNotNull(result, "This student is not found");
            Assert.IsTrue(result.Count > 0, "No data found related to this Id");
        }
        [Test]
        public void AddNewStudent()
        {
            //Arrange
            StudentController studentapi = new StudentController();
            StudentDTO student = new StudentDTO()
            {
                Name = "John",
                Email = "john@email.com",
                Class = "A",
                Contact = "234345344"
            };
            //Act
            var result = studentapi.Add(student);
            //Assert
            Assert.IsNotNull(result, "Invalid Data Entered!");
        }
        [Test]
        public void UpdateStudent()
        {
            //Arrange
            StudentController studentapi = new StudentController();
            int Id = 2;
            StudentDTO student = new StudentDTO()
            {
                Name = "John Doe",
                Email = "doe@example.com",
                Class = "A",
                Contact = "96845459"
            };
            //Act
            var result = studentapi.Update(Id, student);

        }
        [Test]
        public void Delete()
        {
            //Arrange
            StudentController studentApi = new StudentController();
            int studentId = 2004;
            //Act
            var deleted = studentApi.Delete(studentId);
            //Assert    
            Assert.NotNull(deleted);
        }
        [Test]
        public void PDFReport()
        {
            //Arrange
            StudentController studentApi = new StudentController();
            //Act
            var report = studentApi.ExportStudentReport();
            //Assert    
            Assert.NotNull(report);
        }
        [Test]
        public void Search()
        {
            //Arrange
            StudentRepo studentRepo = new StudentRepo();
            var name = "Ahmad";
            //Act
            var searchstudent = studentRepo.Search(name);
            //Assert
            Assert.IsNotNull(searchstudent);
            Assert.IsTrue(searchstudent.Count > 0, "No student found related to this name");
        }

    }
}
