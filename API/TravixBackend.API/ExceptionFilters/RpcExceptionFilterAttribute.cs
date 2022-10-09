using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using TravixBackend.API.Dtos.V1.Response;

namespace TravixBackend.API.ExceptionFilters
{
    public class RpcExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is RpcException)
            {
                var ex = context.Exception as RpcException;
                var exceptionType = ex.Status.Detail;

                if (ex.StatusCode == StatusCode.InvalidArgument)
                {
                    Log.ForContext<RpcExceptionFilterAttribute>().Error($"Invalid request body {context.HttpContext.Request.Path}", ex);

                    context.Result = new BadRequestObjectResult(new ErrorResponse
                    {
                        Code = exceptionType,
                        Message = new ErrorResponse.ErrorMessage()
                        {
                            En = "Invalid request body",
                        }
                    });
                }
            }
        }
    }
}
