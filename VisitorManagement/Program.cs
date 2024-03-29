using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisitorManagement.Data;
using VisitorManagement.Models;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();//To Store session in Memory, This is default implementation of IDistributedCache    
builder.Services.AddSession();


builder.Services.AddMailKit(optionBuilder =>
{
    optionBuilder.UseMailKit(new MailKitOptions()
    {
        //get options from sercets.json
        Server = "smtp.gmail.com",
        Port = 25,
        SenderName = "VISITOR MANAGEMENT",
        SenderEmail = "sigauquetk@gmail.com",
        //can be optional with no authentication 
        Account = "sigauquetk@gmail.com",
        Password = "THIma$!305",
        Security = true,

    });
});
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<AppDBContext>(option =>

     option.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
        )
);
builder.Services.AddIdentity<Admin, IdentityRole>().AddEntityFrameworkStores<AppDBContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Home/Index";
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler($"/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
