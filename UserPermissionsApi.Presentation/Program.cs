
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserPermissionsApi.Infrastructure.Data;
using UserPermissionsApi.Domain.Entities;
using UserPermissionsApi.Domain.Interfaces;
using UserPermissionsApi.Infrastructure.Repositories.Generic;
using UserPermissionsApi.Infrastructure.Repositories.Interfaces;
using UserPermissionsApi.Infrastructure.Repositories;
using Serilog;
using System.Diagnostics;
using UserPermissionApi.Application.Interfaces;
using UserPermissionApi.Aplication.Interfaces;
using UserPermissionApi.Application.Services;
using UserPermissionApi.Aplication.Services;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar el host

// Configurar servicios
builder.Configuration.AddJsonFile("config/secure.appsettings.json");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB")));

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsetting.json");

var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               //.Enrich.WithExceptionDetails()
               .WriteTo.Debug()
               .WriteTo.Console()
               .WriteTo.Elasticsearch(ConfigureElasticSink())
               .CreateLogger();


ElasticsearchSinkOptions ConfigureElasticSink()
{
    return new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM-dd}",
        NumberOfReplicas = 2,
        NumberOfShards = 2
    };
}


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog(logger);


builder.Services.AddSingleton<ProducerService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPermissionTypeService, PermissionTypeService>();
builder.Services.AddScoped<IEmployeePermissionService, EmployeePermissionService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
builder.Services.AddScoped<IEmployeePermissionRepository, EmployeePermissionRepository>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserPermissionApi", Version = "v1" });
});

var app = builder.Build();



// Configurar middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserPermissionApi v1"));
    Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
    Serilog.Debugging.SelfLog.Enable(Console.Error);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ejecutar la aplicación
app.Run();



