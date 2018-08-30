using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SwashbuckleAspNetVersioningShim;
using ApplicationCore.Interfaces;
using Infrastructure.EfConfig;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using ApplicationCore.Services;
using System.Reflection;
using System.IO;
using System;

namespace WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration,
            IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //var config = new CrossCountrySkiConfig();
            //Configuration.Bind("Equipment:CrossCountrySki:Categories", config);
            //services.AddSingleton(config);
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var key = "%CONTENTROOTPATH%";
            if (connectionString.Contains(key))
                connectionString = connectionString.Replace(key, Environment.ContentRootPath);
            services.AddDbContext<EquipmentContext>(options =>
                options.UseSqlServer(connectionString));

            services.Bootstrap();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvcCore().AddVersionedApiExplorer();
            services.AddApiVersioning();
            services.AddSwaggerGen(c => {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                c.ConfigureSwaggerVersions(provider, $"{AppConstants.Swagger.Title}");
                c.DescribeAllEnumsAsStrings();
               
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ConfigureSwaggerVersions(provider, new SwaggerVersionOptions
                {
                    DescriptionTemplate = AppConstants.Swagger.Title + " v{0}",
                    RouteTemplate = "/swagger/{0}/swagger.json"
                });
                c.InjectStylesheet("/swagger-ui/custom.css");
                c.RoutePrefix = string.Empty;
            });
            app.UseStaticFiles();
        }
    }

    public static class ConfigureExtensions
    {
        public static void Bootstrap(this IServiceCollection services)
        {
            services.AddTransient<ICrossCountrySkiCategoryRepository, CrossCountrySkiCategoryRepository>();
            services.AddTransient<ICrossCountrySkiCategoryService, CrossCountrySkiCategoryService>();
            services.AddTransient<ICrossCountrySkiService, CrossCountrySkiService>();
        }
    }
}
