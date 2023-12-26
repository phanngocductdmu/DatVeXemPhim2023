using Microsoft.EntityFrameworkCore;
using DatVeXemPhim2023.Models;
using DatVeXemPhim2023.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("QldatVeXemPhimContext");
builder.Services.AddDbContext<QldatVeXemPhimContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<DiaChiRap, DiaChiRepository>();
builder.Services.AddSession();
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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<QldatVeXemPhimContext>();
        dbContext.Database.Migrate(); // Apply any pending migrations
    }
    catch (Exception ex)
    {
        // Handle any errors during migration
        Console.WriteLine($"An error occurred during database migration: {ex.Message}");
    }
}

app.Run();


// dotnet ef dbcontext scaffold -o Models -f -d "Data Source=./;Initial Catalog=QLDatVeXemPhim;Integrated Security=True; TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer"