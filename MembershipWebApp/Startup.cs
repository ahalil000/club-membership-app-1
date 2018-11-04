using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using MembershipWebApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MembershipWebApp.Interfaces;
using MembershipWebApp.Services;
using AutoMapper;
using MembershipWebApp.Mapping;

namespace MembershipWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("configuration.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddEntityFrameworkSqlServer();

            // Dependency injection scoping.
            services.AddScoped<IMembershipDataRequest, MembershipDataRequest>();
            //services.AddScoped<ISeedData, SeedData>();

            // Obtain configuration settings
            var connection = Configuration.GetConnectionString("connstr");
            services.AddDbContext<MembershipContext>
                (options => options.UseSqlServer(connection));

            // Automapper bootstrapping.
            services.AddAutoMapper(a => a.AddProfile(new CustomMapping()));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddEventSourceLogger();
            
            // Configure a rewrite rule to auto-lookup for standard default files such as index.html.
            app.UseDefaultFiles();

            // Serve static files (html, css, js, images & more). See also the following URL:
            // https://docs.asp.net/en/latest/fundamentals/static-files.html for further reference.
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Disable caching for all static files.
                    context.Context.Response.Headers["Cache-Control"] = Configuration["StaticFiles:Headers:Cache-Control"];
                    context.Context.Response.Headers["Pragma"] = Configuration["StaticFiles:Headers:Pragma"];
                    context.Context.Response.Headers["Expires"] = Configuration["StaticFiles:Headers:Expires"];
                }
            });

            // Add MVC to the pipeline
            app.UseMvc();

            // Get current environment DEV, TEST, PROD
            IConfigurationSection configurationSection = Configuration.GetSection("CurrentEnvironment");
            string devenv = configurationSection.GetValue<string>("environment");

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MembershipContext>();
                //context.Database.EnsureDeleted(); // recreate DB -- for demo we comment out/remove this line.
                context.Database.EnsureCreated(); // create database if not already created.

                if (devenv == "DEV")
                {
                    Initialize(context);
                }
            }
        }

        public static void Initialize(MembershipContext context)
        {
            // Do any initialization code here for the DB. 
            // Can include populate lookups, data based configurations etc.

            SeedData seed_data = new SeedData(context);

            if (context.Members.Count() == 0)
            {
                // Add 50 random members to database.
                seed_data.GenerateMembers(50);

                //context.Members.Add(
                //    new Member()
                //    {
                //        ID = 1,
                //        FirstName = "Bill",
                //        LastName = "Bloggs",
                //        EmailAddress = "abc@fhg.com",
                //        ContactNumber = "0298459567",
                //        DateOfBirth = DateTime.Parse("1994-02-12"),
                //        AccountStatus = "Active",
                //        LastUpdated = DateTime.Now
                //    });

                //context.Members.Add(
                //    new Member()
                //    {
                //        ID = 2,
                //        FirstName = "Lucy",
                //        LastName = "Smith",
                //        EmailAddress = "les@rtg.com",
                //        ContactNumber = "0258459567",
                //        DateOfBirth = DateTime.Parse("1990-10-12"),
                //        AccountStatus = "Active",
                //        LastUpdated = DateTime.Now
                //    });

                //context.Members.Add(
                //    new Member()
                //    {
                //        ID = 3,
                //        FirstName = "Dave",
                //        LastName = "Wagner",
                //        EmailAddress = "dw@ryryrtg.com",
                //        ContactNumber = "0254595673",
                //        DateOfBirth = DateTime.Parse("1985-10-12"),
                //        AccountStatus = "Active",
                //        LastUpdated = DateTime.Now
                //    });

                //context.Database.OpenConnection();
                //try
                //{
                //    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Members ON");
                //    context.SaveChanges();
                //    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Members OFF");
                //}
                //finally
                //{
                //    context.Database.CloseConnection();
                //}
            }
        }
    }
}
