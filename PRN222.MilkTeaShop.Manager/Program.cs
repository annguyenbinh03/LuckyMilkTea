using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Repositories;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.Services;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Manager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<MilkTeaDBContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped(typeof(GenericRepository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<CloudinaryService>();

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
            pattern: "{controller=Combos}/{action=Index}");

        app.Run();
    }
}
