using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EcommerceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleEcommerceAspNet6")));

var app = builder.Build();

builder.Services.AddDbContext<EcommerceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SimpleEcommerceAspNet6")));
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
