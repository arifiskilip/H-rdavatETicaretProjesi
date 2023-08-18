using Business.DependencyResolvers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebUI
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
            services.AddControllersWithViews();
            services.AddDependencies(); //Inversion Of Control ----> IoC Container
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(opt =>
           {
               opt.Cookie.Name = "MyCookie";
               opt.Cookie.HttpOnly = true; // Client Side Scriptleri Engelle
               opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; //Baþka sitelerde kullanma
               opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
               opt.ExpireTimeSpan = System.TimeSpan.FromMinutes(30);
               opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Hesap/Giris");
               opt.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogOut");
               opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied"); // Eriþim Engellendi
           });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
               name: "Admin",
               areaName: "Admin",
               pattern: "Admin/{controller=Urun}/{action=Index}/{id?}"
             );
                endpoints.MapAreaControllerRoute(
              name: "Member",
              areaName: "Member",
              pattern: "Member/{controller=Siparis}/{action=Index}/{id?}"
            );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=AnaSayfa}/{action=Index}/{id?}");
             
            });

        }
    }
}
