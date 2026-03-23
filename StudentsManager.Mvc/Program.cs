using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Configurations;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Tests;

var builder = WebApplication.CreateBuilder(args);
var configurationManager = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSettings(configurationManager);
builder.Services.AddManagerDbContext(configurationManager.GetConnectionString("DefaultConnection"));
builder.Services.AddManagerIdentity();
builder.Services.AddAzureClients(configurationManager);
builder.Services.AddAzureTextAnalytic();
builder.Services.AddApplicationServices();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddCrossOriginResourceSharing();
//builder.Services.AddHostedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ManagerDbContext>();
    dbContext.Database.Migrate();
    var seeder = new DatabaseSeeder(dbContext);
    seeder.SeedDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();