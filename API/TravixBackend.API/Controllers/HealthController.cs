using Microsoft.AspNetCore.Mvc;

namespace TravixBackend.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/healthcheck")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Backend is working....");
        }
    }
}
