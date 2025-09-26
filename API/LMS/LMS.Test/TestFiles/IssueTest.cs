using Library_Management.BLL;
using Library_Management.DAL;
using LMS.Controllers;
using LMS.DTO;

namespace LMS.Test.TestFiles
{
    public class IssueTest
    {

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void GetAllIssue()
        {
            //Arrange
            IssueController issueApi = new IssueController();
            //Act
            var issueData = issueApi.GetAll();
            //Assert
            Assert.That(issueData, Is.Not.Null);
        }
        [Test]
        public void GetById()
        {
            //Arrange
            IssueController issueApi = new IssueController();
            int id = 2;
            //Act
            var result = issueApi.GetById(id);
            //Assert
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void NewIssue()
        {
            //Arrange
            IssueController issueApi = new IssueController();
            IssueDTO issue = new IssueDTO()
            {
                StudentId = 1,
                BookId = 2,
                IssueQuantity = 2
            };
            //Act 
            var result = issueApi.IssueBook(issue);
            //Assert    
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void PdFReport()
        {
            //Arrange
            IssueController issueApi = new IssueController();
            int id = 1;
            //Act
            var result = issueApi.ExportBooksReport();
            //Assert
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void Search()
        {
            //Arrange
            IssueRepo issuerepo = new IssueRepo();
            var tittle = "Chips";

            //Act
            var searchIssue = issuerepo.Search(tittle);
            //Assert
            Assert.That(searchIssue, Is.Not.Null);
           
        }
        [Test]
        public void ReturnBook()
        {
            //Arrange
            IssueRepo issuerepo = new IssueRepo();
            int issueId = 1;
            DateTime returnDate = DateTime.Now;
            int fine = 0;
            //Act
            issuerepo.ReturnBook(issueId, returnDate, fine);
            //Assert
            Assert.Pass("Book Returned Successfully");
        }
    }
}
