using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using TravixBackend.API.Extensions;
using TravixBackend.API.Mappers;
using TravixBackend.API.Middleware;

namespace TravixBackend.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetPreflightMaxAge(TimeSpan.FromHours(1));
                });
            });
            services.AddHeaderPropagation(config => config.Headers.Add(Constants.HEADER_USERNAME));

            services.AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.SuppressMapClientErrors = true;
               });
            services.AddControllers();
            services.AddApiVersioning(options =>{});
            services.AddGrpcClients(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Swagger:Version"],
                    new OpenApiInfo
                    {
                        Title = Configuration["Swagger:Title"],
                        Version = Configuration["Swagger:Version"]
                    }
                );
            });
            services.AddOptions();
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(BookingMapper).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            // Call insecure gRPC services with .NET Core client
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(UserMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHeaderPropagation();
            app.UsePathBase("/api");
            app.UseCors();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["Swagger:SwaggerEndpoint"],
                    $"{Configuration["Swagger:Title"]} {Configuration["Swagger:Version"]}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false }
                }
            };
        }
    }
}
