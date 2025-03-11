using E_Ticket_System.DataAcess;
using E_Ticket_System.Models;
using E_Ticket_System.Repositries;
using E_Ticket_System.Repositries.Irepostries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace E_Ticket_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => 
            option.SignIn.RequireConfirmedAccount=false
            )
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IcinemaReposirotry,CinemaRepository>();
            builder.Services.AddScoped<ImovieRepository, MovieRepository>();
            builder.Services.AddScoped<IActorMoviesRepository, ActorMoviesRepository>();




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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Identity}/{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
