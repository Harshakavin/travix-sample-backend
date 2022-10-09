using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravixBackend.BookingService.API.Protos;

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
            });

            return services;
        }
    }
}
