using MaxiShop.Infrastructure;
using MaxiShop.Application;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using MaxiShop.Infrastructure.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomPolicy",x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#region Database Connectivity

builder.Services
    .AddDbContext<ApplicationDbContext>(options=> 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuration for Seeding Data to Database

static async void UpdateDatabaseAsync(IHost host)
{
    using(var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();
            }

            await SeedData.SeedDataAsync(context);
        }
        catch (Exception ex)
        {
            var logger  = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogError(ex, "An error occurred while migrating or seeding the database");
        }
    }
}

#endregion

var app = builder.Build();

UpdateDatabaseAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
