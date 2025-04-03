using Microsoft.EntityFrameworkCore;
using PRN222.LuckyMilkTea.BlazorWebApp.Components;
using PRN222.LuckyMilkTea.BlazorWebApp.Service;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Repositories;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.Services;
using PRN222.MilkTeaShop.Service.Services.Interface;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<MilkTeaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddBlazorBootstrap();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("/products");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();