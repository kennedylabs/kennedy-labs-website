using KennedyLabsWebsite.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace KennedyLabsWebsite
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApplicationInsightsTelemetry(Configuration)
                .AddMvc().Services
                .AddAuthentication(SharedOptions => SharedOptions.SignInScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddCors();
            services.AddDbContext<WebsiteContext>(ServiceLifetime.Scoped);
        }

        public void Configure(
            IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            app
                .UseApplicationInsightsRequestTelemetry()
                .UseCors(config => config.AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app
                .UseApplicationInsightsExceptionTelemetry()
                .UseStaticFiles()
                .UseCookieAuthentication()
                .UseOpenIdConnectAuthentication(new OpenIdConnectOptions
                {
                    ClientId = Configuration["Authentication:AzureAd:ClientId"],
                    ClientSecret = Configuration["Authentication:AzureAd:ClientSecret"],
                    Authority = Configuration["Authentication:AzureAd:AADInstance"] +
                        Configuration["Authentication:AzureAd:TenantId"],
                    CallbackPath = Configuration["Authentication:AzureAd:CallbackPath"],
                    ResponseType = OpenIdConnectResponseType.CodeIdToken
                })
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

        }
    }
}
