using Microsoft.EntityFrameworkCore;
using ExchangeRateAPI.Contexts;
using Hangfire;
using ExchangeRateAPI.BusinessProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ReactFrontendLocalhostOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000");
        });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ExchangeRateContext>(opt => opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExchangeRateDb;Trusted_Connection=True"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Database=ExchangeRateHangfireDb;Trusted_Connection=True");
});
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactFrontendLocalhostOrigin");

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

// Configure background jobs
if (app.Environment.IsDevelopment())
{
    // Run exchange rate import as a background job once in local dev environment
    BackgroundJob.Enqueue<ExchangeRatesBusinessProvider>(x => x.ImportTodaysExchangeRates());
} else
{
    // Run exchange rate import as a background job daily at 04.00 on server
    RecurringJob.AddOrUpdate<ExchangeRatesBusinessProvider>("ImportTodaysExchangeRates", x => x.ImportTodaysExchangeRates(), "0 4 * * *", TimeZoneInfo.Utc);
}

app.Run();
