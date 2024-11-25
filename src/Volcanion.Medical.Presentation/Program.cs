using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Volcanion.Core.Common;
using Volcanion.Core.Common.Providers;
using Volcanion.Core.Models.Response;
using Volcanion.Core.Presentation.Middlewares;
using Volcanion.Medical.Infrastructure;
using Volcanion.Medical.Infrastructure.Middlewares;
using Volcanion.Medical.Handlers;
using Volcanion.Medical.Models.Context;
using Volcanion.Medical.Models.MappingProfiles;
using Volcanion.Medical.Services;
using Serilog;
using System.Net;
using Volcanion.Medical.Models.Setting;
using StackExchange.Redis;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Common.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.RegisterProviders();
builder.Services.RegisterIdentityInfrastructure();
builder.Services.RegisterIdentityService();
builder.Services.RegisterIdentityHandler();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(DtoMappingProfile), typeof(BoMappingProfile));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Redis
var redisConfiguration = builder.Configuration.GetSection("Redis:ConnectionString").Value;
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfiguration!));
builder.Services.AddSingleton<IRedisCacheProvider, RedisCacheProvider>();

// Add JWT Authentication settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Configure CORS
var corsOptions = builder.Configuration.GetSection("Cors");
var origins = corsOptions.GetSection("Origins").Get<string[]>() ?? ["*"];
var headers = corsOptions.GetSection("Headers").Get<string[]>() ?? ["*"];
var methods = corsOptions.GetSection("Methods").Get<string[]>() ?? ["*"];
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", policy =>
    {
        policy.WithOrigins(origins).WithHeaders(headers).WithMethods(methods);
        //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// API Versioning
builder.Services.AddApiVersioning(x =>
{
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.ReportApiVersions = true;
    x.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")
    );
});

// Add Entity Framework DBContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    //options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Volcanion.Medical.Presentation"));
    var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("Volcanion.Medical.Presentation"));
});

// Use serilog
configureLogging();
builder.Host.UseSerilog();

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowOrigins");

app.UseGlobalErrorHandlingMiddleware();
app.UseVolcanionAuthMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void configureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    environment ??= "Development";

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .AddEnvironmentVariables()
    .Build();

    if (configuration == null)
    {
        Console.WriteLine("configuration is null");
    }

    LogProvider.LoggerSetting(configuration, environment);
}