using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravixBackend.API.Dtos.V1;
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
            var response = await _bookingServiceClient.GetBookingsAsync(new GetBookingRequest
            {
                Limit = limit
            });

            var bookingList = new List<BookingDto>();
            response.Bookings.ToList().ForEach(b =>
            {
                bookingList.Add(_mapper.Map<BookingRequest, BookingDto>(b));
            });

            return Ok(new SuccessResponse(bookingList));
        }

        [HttpPost]
        public async Task<ObjectResult> Add([FromBody] BookingDto bookingDto)
        {
            var bookingRequest = _mapper.Map<BookingDto, BookingRequest>(bookingDto);
            var response = await _bookingServiceClient.AddBookingsAsync(bookingRequest);

            var bookingList = new List<BookingDto>();
            response.Bookings.ToList().ForEach(b =>
            {
                bookingList.Add(_mapper.Map<BookingRequest, BookingDto>(b));
            });

            return Ok(new SuccessResponse(bookingList));
        }
    }
}
