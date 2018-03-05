using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;

namespace IdentityServerDemo.Server
{
    public class Startup
    {
        private ServerConfig _serverConfig;
        public Startup()
        {
            _serverConfig = new ServerConfig();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(_serverConfig.GetIdentityResources())
                .AddInMemoryClients(_serverConfig.GetClients())
                .AddInMemoryApiResources(_serverConfig.GetApiResources())
                .AddTestUsers(_serverConfig.GetUsers().ToList());

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "537114340738-41u5e7rkfa6e4qupn965h1llt0f0rmgf.apps.googleusercontent.com";
                    options.ClientSecret = "gRX4-NSH_yjnpQQeWKX6X54y";
                });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();

        }
    }
}
