using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Business;
using Portal.Common.Interfaces;
using Portal.Data;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SmartBreadcrumbs.Extensions;
using SmartBreadcrumbs;
using Portal.Web.Controllers;
using Portal.Web.Filters;

namespace Portal.Web
{
    public class Startup
    {
        //public Startup()
        //{
        //    Configuration =
        //}  

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Azure AD
            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
        .AddAzureAD(options => Configuration.Bind("AzureAd", options));
            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                options.Authority = options.Authority + "/v2.0/";

                // Per the code below, this application signs in users in any Work and School
                // accounts and any Microsoft Personal Accounts.
                // If you want to direct Azure AD to restrict the users that can sign-in, change 
                // the tenant value of the appsettings.json file in the following way:
                // - only Work and School accounts => 'organizations'
                // - only Microsoft Personal accounts => 'consumers'
                // - Work and School and Personal accounts => 'common'

                // If you want to restrict the users that can sign-in to only one tenant
                // set the tenant value in the appsettings.json file to the tenant ID of this
                // organization, and set ValidateIssuer below to true.

                // If you want to restrict the users that can sign-in to several organizations
                // Set the tenant value in the appsettings.json file to 'organizations', set
                // ValidateIssuer, above to 'true', and add the issuers you want to accept to the
                // options.TokenValidationParameters.ValidIssuers collection
                options.TokenValidationParameters.ValidateIssuer = false;

                options.Events.OnSignedOutCallbackRedirect = (context) =>
                {

                    context.Response.Redirect("/");
                    context.HandleResponse();

                    return Task.CompletedTask;
                };
            });
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddBreadcrumbs(GetType().Assembly, optionsSetter => new BreadcrumbOptions()
            //{
            //    TagName = "nav",
            //    TagClasses = "",
            //    OlClasses = "breadcrumb",
            //    LiClasses = "breadcrumb-item",
            //    ActiveLiClasses = "breadcrumb-item active",
            //    SeparatorElement = "<li class=\"separator\">/</li>"
            //});
            services.AddBreadcrumbs(GetType().Assembly);

            //Identity
            services.AddHttpContextAccessor();

            //Repositories
            services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
            services.AddTransient<IGeneralRepository, GeneralRepository>();
            services.AddTransient<IPharmacyRepository, PharmacyRepository>();
            services.AddTransient<IUserContextRepository, UserContextRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();


            //Business Logics
            services.AddTransient<ISpecialtyLogic, SpecialtyLogic>();
            services.AddTransient<IGeneralLogic, GeneralLogic>();
            services.AddTransient<IPharmacyLogic, PharmacyLogic>();
            services.AddTransient<IUserContextLogic, UserContextLogic>();
            services.AddTransient<IPermissionLogic, PermissionLogic>();

            //Controll and Action level filters.
            services.AddScoped<ValidationFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    var excHandler = context.Features.Get<IExceptionHandlerFeature>();
                    //if (context.Request.GetTypedHeaders().Accept.Any(header => header.MediaType == "application/json"))
                    //{
                    //var jsonString = string.Format("{{\"error\":\"{0}\"}}", excHandler.Error.Message);
                    var jsonString = string.Format("{0}", excHandler.Error.Message);

                    context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                    await context.Response.WriteAsync(jsonString, Encoding.UTF8);


                    //}
                    //else
                    //{
                    //    //I haven't figured out a better way of signally ExceptionHandlerMiddleware that we can't handle the exception
                    //    //But this will do the trick of letting the other error handlers to intervene
                    //    //as the ExceptionHandlerMiddleware class will swallow this exception and rethrow the original one
                    //    throw excHandler.Error;
                    //}
                });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
