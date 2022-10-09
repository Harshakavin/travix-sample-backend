using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using TravixBackend.BookingService.Domain.Services;
using TravixBackend.BookingService.API.Protos;
using static TravixBackend.BookingService.Domain.Enums.ExceptionEnum;

namespace TravixBackend.BookingService.API.GrpcServices
{

    public class BookingService : Protos.BookingGrpcService.BookingGrpcServiceBase
    {
        private readonly ILogger<BookingService> _logger;
        private readonly IMapper _mapper;
        private readonly IBookingService _bookingService;

        public BookingService(IServiceProvider provider)
        {
            _logger = (ILogger<BookingService>)provider.GetService(typeof(ILogger<BookingService>));
            _mapper = (IMapper)provider.GetService(typeof(IMapper));
            _bookingService = (IBookingService)provider.GetService(typeof(IBookingService));
        }

        public override async Task<BookingResponse> GetBookings(BookingRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _bookingService.GetBookingAsync(request.UserId);

                var chargePlanResponse = new BookingResponse();
                return chargePlanResponse;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetBookings: General Exception occured");
                throw new RpcException(new Status(StatusCode.Internal,
                    Enum.GetName(typeof(ExceptionList), ExceptionList.INTERNAL_ERROR)));
            } 
        }
    }
}
