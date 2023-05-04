using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStoreMvc.Models.Domain;

namespace MovieStoreMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //register dbcontext
            builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("con")));
            //register identity and specify user and role model
            builder.Services.AddIdentity<ApplicationUsers,IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            //
            //builder.Services.ConfigureApplicationCookie(option => option.LoginPath = "UserAuthentication/Login");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // have right to user give you rights
            app.UseAuthentication();
            // allow to use
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}