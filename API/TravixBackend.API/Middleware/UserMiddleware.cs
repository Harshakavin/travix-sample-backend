using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace TravixBackend.API.Middleware
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var username = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            context.Request.Headers.TryAdd(Constants.HEADER_USERNAME, HttpUtility.HtmlEncode(username));
            await _next(context);
        }
    }
}
