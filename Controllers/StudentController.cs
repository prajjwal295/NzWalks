using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark" };

            return Ok(studentNames);
        }
    }
}
