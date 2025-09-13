using Library_Management.BLL;
using Library_Management.DAL;
using LMS.DTO;
using LMS.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        public IssueRepo issueRepo = new IssueRepo();


        [HttpPost]
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
        [HttpGet]
        public ActionResult<List<IssueData1>> GetAllIssues()
        {
            var issues = issueRepo.GetAllIssues().Select(IssueData1.FromEntity).ToList();
            return Ok(issues);
        }
        [HttpGet("{id}")]
        public ActionResult<IssueData1> GetById(int id)
        {
            var issue = issueRepo.GetAllIssues().FirstOrDefault(i => i.Id == id);
            if (issue == null)
                return NotFound();

            return Ok(IssueData1.FromEntity(issue));
        }
    }
}
