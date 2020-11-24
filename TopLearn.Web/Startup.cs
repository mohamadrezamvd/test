using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TopLearn.DataLayer.Context;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Service;
using Microsoft.CodeAnalysis.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using TopLearn.Core.Convertors;

namespace TopLearn.Web
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
            services.AddControllersWithViews();
            services.AddDbContext<TopLearnContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("TopLearnConnection"));
            });
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IUserService,UserServices>();
            #region Authentication
            services.AddAuthentication(Option=> {
                Option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                Option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                Option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(Option=> {
                Option.LoginPath = "/Login";
                Option.LogoutPath = "/Logout";
                Option.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
                
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
