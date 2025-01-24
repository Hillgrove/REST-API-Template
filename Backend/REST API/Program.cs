using ChairsLib;
using ChairsLib.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Determine if a Db is used and if so which Db
// TODO: doesn't properly check what db it should use
bool useDatabase = bool.Parse(builder.Configuration["RepositorySettings:UseDatabase"]);
string databaseType = builder.Configuration["RepositorySettings:DatabaseType"];
string connectionString = builder.Configuration.GetConnectionString(databaseType);

// Add services to the container.
if (useDatabase)
{
    builder.Services.AddDbContext<ChairsDbContext>(options => options.UseSqlServer(connectionString));
    // Use Scoped for database repository
    builder.Services.AddScoped<IChairsRepository, ChairsRepositoryDB>();
}
else
{
    // Use Singleton for list repository
    builder.Services.AddSingleton<IChairsRepository, ChairsRepositoryList>();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed the database if in development
if (app.Environment.IsDevelopment())
{
    if (useDatabase)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ChairsDbContext>();
            SeedDatabase(context);
        }
    }

    else
    {
        var repository = app.Services.GetRequiredService<IChairsRepository>() as ChairsRepositoryList;
        if (repository != null)
        {
            SeedListRepository(repository);
        }
    }

}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

#region "Helper Functions"
void SeedDatabase(ChairsDbContext context)
{
    if (!context.Chairs.Any())
    {
        context.Chairs.AddRange(
            new Chair() { Model = "The Egg", MaxWeight = 100, HasPillow = true },
            new Chair() { Model = "Swan", MaxWeight = 120, HasPillow = false },
            new Chair() { Model = "E27", MaxWeight = 130, HasPillow = true }
        );
        context.SaveChanges();
    }
}

void SeedListRepository(ChairsRepositoryList repository)
{
    repository.Add(new Chair() { Model = "The Egg", MaxWeight = 100, HasPillow = true });
    repository.Add(new Chair() { Model = "Swan", MaxWeight = 120, HasPillow = false });
    repository.Add(new Chair() { Model = "E27", MaxWeight = 130, HasPillow = true });
}
#endregion