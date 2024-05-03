using Caramel.Pattern.Services.Api.Example.Middlewares;
using Caramel.Pattern.Services.Application;
using Caramel.Pattern.Services.Domain.AutoMapper;
using Caramel.Pattern.Services.Infra;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Swashbuckle - API Documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Exemplo",
        Version = "v1",
        Description = "Exemplo da Arquitetura a ser seguida nos Microsserviços do Projeto Caramel (TCC - EC 2024)."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Exceptions Handlers
builder.Services.AddExceptionHandler<BusinessExceptionHandler>();
builder.Services.AddExceptionHandler<DatabaseExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Serialization Pattern
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Layer's Dependency
builder.Services.AddDatabaseModule(builder.Configuration);
builder.Services.AddApplicationModule();


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

// Exceptions Handlers
app.UseExceptionHandler();

//Swashbuckle - API Documentation
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
});

// MiddleWareJWT
app.UseMiddleware<JwtMiddleware>();

app.Run();
