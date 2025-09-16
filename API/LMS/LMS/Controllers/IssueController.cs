using Library_Management.BLL;
using Library_Management.DAL;
using Library_Management.Reports;
using LMS.DTO;
using LMS.Entities;
using LMS.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public IssueRepo issueRepo = new IssueRepo();
        private IssueReport issueReport = new IssueReport();
        /// <summary>Issue a new book to Student</summary>
        ///  <response code="201">Book issued successfully.</response>
        /// <response code="400">Invalid request.</response>

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
        /// Get all Issued Books
        /// </summary>
        /// <reponse code = "200">List of issued books found</reponse>
        /// <response code ="204">list of issued books not found</response>

        [HttpGet]
        [ProducesResponseType(typeof(IssueData1), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status204NoContent)]
        public ActionResult<List<IssueData1>> GetAllIssues()
        {
           
            var issues = issueRepo.GetAllIssues().Select(IssueData1.FromEntity).ToList();
            return Ok(issues);
        }
        /// <summary>
        /// Get a particular issued book
        /// </summary>
        /// <param name="id">The Id of issued book</param>
        /// <response code="200">Issued book found</response>
        /// <reponse code ="204">This book is not issued</reponse>
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
        /// <returns></returns>
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
