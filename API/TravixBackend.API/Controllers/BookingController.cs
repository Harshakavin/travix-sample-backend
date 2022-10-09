using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TravixBackend.API.Dtos.V1.Response;
using TravixBackend.API.ExceptionFilters;
using TravixBackend.BookingService.API.Protos;

namespace TravixBackend.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Route("v{version:apiVersion}/bookings")]
    [RpcExceptionFilter]
    public class BookingController : ControllerBase
    {
        private readonly BookingGrpcService.BookingGrpcServiceClient _bookingServiceClient;
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;

        public BookingController(IServiceProvider provider)
        {
            _bookingServiceClient = (BookingGrpcService.BookingGrpcServiceClient)provider.GetService(typeof(BookingGrpcService.BookingGrpcServiceClient));
            _logger = (ILogger<BookingController>)provider.GetService(typeof(ILogger<BookingController>));
            _mapper = (IMapper)provider.GetService(typeof(IMapper));
        }

        [HttpGet]
        public async Task<ObjectResult> Get([FromQuery] int limit)
        {
            var response = await _bookingServiceClient.GetBookingsAsync(new BookingRequest
            {
                Limit = limit
            });

            var validateResponse = _mapper.Map<
              BookingResponse,
              Dtos.V1.BookingDto>(response);

            return Ok(new SuccessResponse(validateResponse));
        }
    }
}
