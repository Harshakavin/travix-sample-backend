using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using Microsoft.AspNetCore.Builder;
using TravixBackend.UserService.API.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TravixBackend.UserService.Domain;

namespace TravixBackend.UserService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TravixBackendDBContext>(options =>
                          options.UseLazyLoadingProxies().UseNpgsql(Configuration.GetConnectionString("TravixBackendDB")));

            services.AddGrpc();

            //Plugging automapper
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(UserMapper).Assembly);
            services.AddHeaderPropagation(config => config.Headers.Add(Constants.HEADER_USERNAME));

            services.Scan(scan => scan
                        .FromAssemblies(typeof(TravixBackendDBContext).Assembly)
                        .AddClasses(classes => classes.InNamespaces("TravixBackend.UserService.Domain.Services"))
                        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()) ;

            Log.ForContext<Startup>().Information(Configuration.GetConnectionString("TravixBackendDB") != null
                ? $"TravixBackendDB connectionString is not null"
                : $"TravixBackendDB connectionString is null");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            
            app.UseRouting();
            app.UseHeaderPropagation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                builder.AddUserSecrets<Startup>();
            }
            else
            {
                app.UseExceptionHandler(a => a.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature.Error;
                    var result = JsonConvert.SerializeObject(new { error = exception.Message });
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                }));
            }
            
            Configuration = builder.Build();

            UpdateDatabase(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcServices.UserService>();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<TravixBackendDBContext>();
            context.Database.Migrate();
        }
    }
}
