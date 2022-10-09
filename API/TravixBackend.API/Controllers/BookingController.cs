using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravixBackend.API.ExceptionFilters;

namespace TravixBackend.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Route("v{version:apiVersion}/bookings")]
    [RpcExceptionFilter]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Backend is working....");
        }
    }
}
