using Ajax_Crud.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddCors((op =>
{
    op.AddPolicy("shop", o =>
    {
        o.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
}));
builder.Services.AddDbContext<StudentContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("string")));
builder.Services.AddControllers(op =>
{
    op.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
    op.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNameCaseInsensitive = false,
        PropertyNamingPolicy = null,
        WriteIndented = true

    }));
});
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
app.UseCors("shop");
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
