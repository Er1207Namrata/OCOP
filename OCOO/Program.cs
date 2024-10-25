using DbHelper;
using Humanizer;
using Microsoft.AspNetCore.Cors.Infrastructure;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Office.Interop.Word;
using OCOO.Filter;
using Services;
using System.Text;



var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MenuAuthorizationFilter>();
builder.Services.AddSingleton<DbHelperService>();
builder.Services.AddSingleton<MasterServices>();
builder.Services.AddSingleton<BillServices>();
builder.Services.AddSingleton<DashbaordService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();




var app = builder.Build();

app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 || context.Response.StatusCode == 500)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();




// Default route
app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{idd?}");



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
