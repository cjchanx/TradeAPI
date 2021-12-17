using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Webservice.ContextHelpers;
using Webservice.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;


namespace Webservice
{
    ///  REFERENCE : The following class is based on a class example from CPSC471F2021 Week 8 Lectures.
    ///  This class sets up services for the container.
    public class Startup
    {

        #region Initialization

        /// <summary>
        /// Gives us access to the variables in the appsettings.json file.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gives us access to the environment variables of the current selected profile.
        /// List of all profiles can be found in Properties/launchSettings.json.
        /// </summary>
        public IWebHostEnvironment HostingEnvironment { get; }

        /// <summary>
        /// Constructor called automatically in program.cs.
        /// Using injection to get the parameters.
        /// </summary>
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        #endregion

        #region Configuration

        /// <summary>
        ///  This method gets called by the runtime. 
        ///  Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup cors
            services.AddCors(options =>
            {
                options.AddPolicy("ALLOW_ALL_CORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });


            // Setup .net core version
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Setup app settings helper
            services.AddScoped<AppSettingsHelper>();

            // Setup database context helper
            services.AddScoped<DatabaseContextHelper>();
            //services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson();
        }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Get access to all the helpers added into the scope from ConfigureServices method
            using (var scope = app.ApplicationServices.CreateScope())
            {
                // Reference helpers
                var appSettingsHelper = scope.ServiceProvider.GetService<AppSettingsHelper>();
                var dbContextHelper = scope.ServiceProvider.GetService<DatabaseContextHelper>();

                // Setup how to handle unexpected errors (thrown exceptions)
                if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
                else
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts
                    app.UseHsts();

                // Setup databases
                /*bool databaseSetupSuccessfully = dbContextHelper.Initialize(out string setupMessage);
                if (!databaseSetupSuccessfully)
                    throw new Exception("Failed to establish a valid connection with the database.\n\nError: " + setupMessage);*/

                // Setup CORS
                app.UseCors("ALLOW_ALL_CORS");
                app.UseRouting();

                app.UseHttpsRedirection();
                //app.UseMvc();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "students",
                        pattern: "{controller}/{action}/{id?}");
                });
                // Default listener to the app
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Webservice started!");
                });
            }
        }

        #endregion

    }
}
