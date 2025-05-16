using Insurance1CompAppServices;
using Insurance1CompAppServices.Repositories.LocalRepositories.Ef;
using Insurance1CompAppServices.Repositories.LocalRepositories;
using Insurance1CompAppServices.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Insurance1CompAppServices.DataInitialization;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// Add services to the container.
builder.Services.TryAddTransient<ICountryGwpRepository, CountryGwpRepository>();
builder.Services.TryAddTransient<CountryGwpService>();

if (builder.Environment.IsDevelopment())
{
    // Use In-Memory DB for local dev or testing
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("GwpTestDb"));
    builder.Services.AddSingleton<CsvImporter>();
    builder.Services.AddTransient<GwpDataSeeder>();
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// ---------------------------------------------------------
// Current web app services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed the database with test data
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<GwpDataSeeder>();
    var env = app.Environment;
    var path = Path.Combine(env.ContentRootPath, "SampleData", "gwpByCountry.csv"); // adjust path to your CSV
    seeder.Seed(path);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
