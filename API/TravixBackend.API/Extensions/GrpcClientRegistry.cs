using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravixBackend.BookingService.API.Protos;
using TravixBackend.UserService.API.Protos;

namespace TravixBackend.API.Extensions
{
    public static class GrpcClientRegistry
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<BookingGrpcService.BookingGrpcServiceClient>(options =>
            {
                var bookingServiceUrl = configuration["GrpcConfig:BookingService"];
                var serviceIp = $"http://{bookingServiceUrl}";
                options.Address = new Uri(serviceIp);
            }).AddHeaderPropagation();

            services.AddGrpcClient<UserGrpcService.UserGrpcServiceClient>(options =>
            {
                var userServiceUrl = configuration["GrpcConfig:UserService"];
                var serviceIp = $"http://{userServiceUrl}";
                options.Address = new Uri(serviceIp);
            }).AddHeaderPropagation();

            return services;
        }
    }
}
