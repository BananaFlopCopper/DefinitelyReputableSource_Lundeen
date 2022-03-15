using DefinitelyReputableSource_Lundeen.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DefinitelyReputableSource_Lundeen.Context.ASourceOfDataContext>();
var app = builder.Build();

// update the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<ASourceOfDataContext>();
        if (dbContext.Database.IsSqlServer())
        {
            dbContext.Database.Migrate();

            // adding seed data
        }

    }
    catch (Exception ex)
    {
        //var logger = scope.ServiceProvider.GetRequiredService<Program>();
        //logger.LogError(ex, "An error occured while migrating or seeding the database.");

        throw;
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

//app.MapGet("/", () => "Hello World!");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Classes}/{action=Index}"
    );

app.Run();
