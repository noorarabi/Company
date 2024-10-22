using Company.Controllers;
using Company.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add DbContext
            builder.Services.AddDbContext<CompanyDbContext>(options => {
                options.UseSqlServer(builder.Configuration
                .GetConnectionString("constr"));
            });

            // Add Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admain", policy =>
                    policy.RequireRole("Admain"));
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdmain", policy =>
                    policy.RequireRole("SuperAdmain"));
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Student", policy =>
                    policy.RequireRole("Student"));
            });
            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Add this to enable authentication
            app.UseAuthorization();
            app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
            
                app.MapControllerRoute(
                 name: "default",
                pattern: "{controller=Home}/{action=Privacy}/{id?}");

             CreateAdminUser(app.Services);
            app.Run();
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Check if Admin role exists
                if (!await roleManager.RoleExistsAsync("SuperAdmain"))
                {
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmain"));
                }

                // Check if the Admin user exists
                var adminUser = await userManager.FindByNameAsync("Admin");
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        Email = "Admin@gmail.com",
                        UserName = "Admin"
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "SuperAdmain");
                    }
                }
            }
        }
    }
}
