using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityServer4.AccessTokenValidation;

namespace IdentityServerDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    // Endpoint to look at to validate token
                    options.Authority = "http://localhost:5000";
                    // Says if discovery endpoint requires https 
                    options.RequireHttpsMetadata = false;
                    // Names our api for others to request access to
                    options.ApiName = "IdentityApi";
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                     policy.WithOrigins("http://localhost:5004")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Tells our endpoint to use the configured authentication
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
