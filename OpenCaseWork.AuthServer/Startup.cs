using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenCaseWork.AuthServer.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OpenCaseWork.AuthServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }

        public IHostingEnvironment Environment { get; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {         
            // configure identity server with in-memory stores, keys, clients and scopes
            if (Environment.IsDevelopment())
            {
                services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>(); // IdentityServer4.AspNetIdentity.;

                // Identity & SQLite.
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(("Filename=MyDatabase.db"))); 
            }
            else
            {
                services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())                
                .AddAspNetIdentity<ApplicationUser>(); // IdentityServer4.AspNetIdentity.

                // Identity & Sql Server.
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            }

            //Add AspNet User Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddSingleton(typeof(UserInitializer));
            services.AddTransient(typeof(UserManager<ApplicationUser>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext appDbContext, UserInitializer userInitializer)
        {
            loggerFactory.AddConsole();

            userInitializer.Initialize(appDbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                userInitializer.Seed(appDbContext);
            }

            // Adds Identity.
            app.UseIdentity();
            app.UseIdentityServer();
            
        }
    }
}
