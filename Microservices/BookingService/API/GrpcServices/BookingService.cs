using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using TravixBackend.BookingService.Domain.Services;
using TravixBackend.BookingService.API.Protos;
using static TravixBackend.BookingService.Domain.Enums.ExceptionEnum;
using System.Linq;
using TravixBackend.BookingService.Domain.Dtos;
using System.Collections.Generic;

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

        public override async Task<BookingResponse> GetBookings(GetBookingRequest request, ServerCallContext context)
        {
            try
            {
                var userId = GetUserIdFromHeader(context);
                var result = await _bookingService.GetBookingAsync(userId, (int)request.Limit);

                return GenarateBookingResponse(result);
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

        public override async Task<BookingResponse> AddBookings(BookingRequest request, ServerCallContext context)
        {
            try
            {
                var userId = GetUserIdFromHeader(context);
                var bookingDto = _mapper.Map<BookingRequest, BookingDto>(request);
                var result = await _bookingService.AddBookingAsync(userId, bookingDto);

                return GenarateBookingResponse(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogCritical(ex, "AddBookings: Unauthenticated user");
                throw new RpcException(new Status(StatusCode.Unauthenticated,
                    Enum.GetName(typeof(ExceptionList), ExceptionList.UNAUTHORIZE_REQUEST)));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "AddBookings: General Exception occured");
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    Enum.GetName(typeof(ExceptionList), ExceptionList.INTERNAL_ERROR)));
            }
        }

        private BookingResponse  GenarateBookingResponse(List<BookingDto> bookings)
        {
            var response = new BookingResponse();
            bookings.ForEach(b =>
            {
                response.Bookings.Add(_mapper.Map<BookingDto, BookingRequest>(b));
            });
            return response;
        }

        private static string GetUserIdFromHeader(ServerCallContext context) =>
                        System.Web.HttpUtility.HtmlDecode(context.RequestHeaders.SingleOrDefault(header => header.Key == "x-custom-userid")?.Value);
    }
}
