using DevIO.Api.Configuration;
using DevIO.Api.Configurations;
using DevIO.Api.Extensions;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DevIO.Api
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
            services.AddDbContext<CustomDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddLoggingConfiguration(Configuration);
            services.AddIdentityConfig(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.WebApiConfig();
            services.AddSwaggerConfig();

            services.ResolveDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development"); // Resolver problemas de CORS
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production"); // Resolver problemas de CORS
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseLoggingConfiguration();
            app.UseMvcConfig();
            app.UseSwaggerConfig(provider);
        }
    }
}
