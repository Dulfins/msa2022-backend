using Microsoft.OpenApi.Models;
using Service_Layer.Services;
using System.Reflection; 
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain_Layer.Data;


var builder = WebApplication.CreateBuilder(args);

//MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddSingleton<GamesService>
    
var Configuration = builder.Configuration;

builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAP  I at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Game API",
        Description = "MSA 2022 Phase 2 Backend API - By **Vince Guan**",
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddHttpClient(builder.Configuration["CheapSharkClientName"], configureClient: client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CheapSharkAddress"]);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

