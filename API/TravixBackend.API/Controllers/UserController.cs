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
using TravixBackend.UserService.API.Protos;

namespace TravixBackend.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Route("v{version:apiVersion}/user")]
    [RpcExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly UserGrpcService.UserGrpcServiceClient _userServiceClient;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IServiceProvider provider)
        {
            _userServiceClient = (UserGrpcService.UserGrpcServiceClient)provider.GetService(typeof(UserGrpcService.UserGrpcServiceClient));
            _logger = (ILogger<UserController>)provider.GetService(typeof(ILogger<UserController>));
            _mapper = (IMapper)provider.GetService(typeof(IMapper));
        }

        [HttpPost("signin")]
        public async Task<ObjectResult> SignIn([FromBody] SignInDto singInDto)
        {
            var response = await _userServiceClient.SingInAsync(new SignInRequest
            {
                UserName = singInDto.UserName,
                Password = singInDto.Password
            });

            return Ok(new SuccessResponse(response));
        }

        [HttpPost("signup")]
        public async Task<ObjectResult> Add([FromBody] SignUpDto signUpDto)
        {
            var singUpRequest = _mapper.Map<SignUpDto, SignUpRequest>(signUpDto);
            var response = await _userServiceClient.SingUpAsync(singUpRequest);

            return Ok(new SuccessResponse(response));
        }
    }
}
