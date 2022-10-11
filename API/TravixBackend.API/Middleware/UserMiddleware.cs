using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "userName")?.Value;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                context.Request.Headers.TryAdd("x-custom-username", HttpUtility.HtmlEncode(userName));
                context.Request.Headers.TryAdd("x-custom-userid", HttpUtility.HtmlEncode(userId));
            }

            await _next(context);
        }
    }
}
