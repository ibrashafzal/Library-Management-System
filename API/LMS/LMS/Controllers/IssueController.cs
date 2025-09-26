using Library_Management.BLL;
using LMS.DTO;
using LMS.Reports;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    /// <summary>
    /// Issue APIs Endpoints for issuing and returning books:
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public IssueRepo issueRepo = new IssueRepo();
        private IssueReport issueReport = new IssueReport();
        /// <summary>Issue a new book to Student</summary>
        ///  <response code="201">Book issued successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <remarks>
        /// This endpoint is used to issue a book to a student.
        ///It records the student, the book, the issue date, and the due date.
           /// </remarks>

         [HttpPost]
        [ProducesResponseType(typeof(IssueDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        public IActionResult IssueBook([FromBody] IssueDTO request)
        {
            issueRepo.IssueBook(request.StudentId, request.BookId, request.IssueQuantity);

            return Ok(new
            {
                studentId = request.StudentId,
                bookId = request.BookId,
                quantity = request.IssueQuantity,
                issueDate = DateTime.Now,
                expectedReturnDate = DateTime.Now.AddDays(2)
            });
        }

        /// <summary>
        /// Retrieve all issued books.
        /// </summary>
        /// <reponse code = "200">List of issued books found</reponse>
        /// <response code ="204">list of issued books not found</response>
        /// <remarks>This endpoint returns a list of all issued books with details about the student, book, issue date, due date, and status.</remarks>

        [HttpGet]
        [ProducesResponseType(typeof(IssueData1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
        public ActionResult<List<IssueData1>> GetAll()
        {
           
            var issues = issueRepo.GetAllIssues().Select(IssueData1.FromEntity).ToList();
            return Ok(issues);
        }
        /// <summary>
        /// Retrieve details of a specific issued book by issue ID.
        /// </summary>
        /// <param name="id">The Id of issued book</param>
        /// <response code="200">Issued book found</response>
        /// <reponse code ="204">This book is not issued</reponse>
        /// <remarks>This endpoint retrieves the details of a specific issued book record by its unique issueId.</remarks>
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IssueData1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]
        public ActionResult<IssueData1> GetById(int id)
        {
            var issue = issueRepo.GetAllIssues().FirstOrDefault(i => i.Id == id);
            if (issue == null)
                return NotFound();

            return Ok(IssueData1.FromEntity(issue));
        }
        /// <summary>
        /// Get all issued books in PDF with download link
        /// </summary>
        /// <remarks>This endpoint generates a PDF file containing all issued books with details like student, book, issue date, due date, and return status.</remarks>
        [HttpGet("Issue/report")]
        public IActionResult ExportBooksReport()
        {
            var issue = issueRepo.GetAllIssues();
            string filePath = "E:/Issue Book Report";

            IssueReport.ExportIssueToPDF(issue, filePath, "Books Report", DateTime.Now.AddDays(-7), DateTime.Now);

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "application/pdf", "IssueReport.pdf");
        }
    }
}
