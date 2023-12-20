using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using System.Reflection;
using Tekton.TechnicalTest.Api.Filters;
using Tekton.TechnicalTest.Application;
using Tekton.TechnicalTest.Infrastructure;
using Tekton.TechnicalTest.Infrastructure.Persistence.Contexts;
using Tekton.TechnicalTest.Infrastructure.Persistence.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetValue<string>("LoggingPath") + $@"\log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting Web API");

    await SeedAppDb();

    Log.Information("Running in:");
    Log.Information("https://localhost:7236");
    Log.Information("http://localhost:5245");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return;
}
finally
{
    Log.CloseAndFlush();
}

async Task SeedAppDb()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
    if (app.Environment.IsDevelopment())
    {
        await AppDbContextSeed.SeedDataAsync(context);
    }
}