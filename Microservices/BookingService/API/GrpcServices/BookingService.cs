using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using TravixBackend.BookingService.Domain.Services;
using TravixBackend.BookingService.API.Protos;
using static TravixBackend.BookingService.Domain.Enums.ExceptionEnum;
using System.Linq;

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
                var username = GetUsernameFromHeader(context);
                var result = await _bookingService.GetBookingAsync(username,(int)request.Limit);

                var chargePlanResponse = new BookingResponse();
                chargePlanResponse.Name = result.Count.ToString();

                return chargePlanResponse;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogCritical(ex, "GetBookings: Unauthenticated user");
                throw new RpcException(new Status(StatusCode.Unauthenticated,
                    Enum.GetName(typeof(ExceptionList), ExceptionList.UNAUTHORIZE_REQUEST)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "GetBookings: General Exception occured");
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    Enum.GetName(typeof(ExceptionList), ExceptionList.INTERNAL_ERROR)));
            }
        }
        private static string GetUsernameFromHeader(ServerCallContext context) =>
                        System.Web.HttpUtility.HtmlDecode(context.RequestHeaders.SingleOrDefault(header => header.Key == "x-custom-username")?.Value);
    }
}
