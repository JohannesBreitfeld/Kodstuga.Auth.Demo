using Backend.MinimalApi.Endpoints;
using Backend.MinimalApi.Interfaces;
using Backend.MinimalApi.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add scoped
builder.Services.AddSingleton<IAnimalService, AnimalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.MapAnimalEndpoints();


app.Run();

