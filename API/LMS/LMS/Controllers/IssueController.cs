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
        [HttpGet]
        public ActionResult<List<IssueDTO>> GetAllIssues()
        {
            var issues = issueRepo.GetAllIssues();

            var result = issues.Select(IssueDTO.FromIssue).ToList();

            return Ok(result);
        }
    }
}
