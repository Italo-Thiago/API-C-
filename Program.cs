using PersonAPI.Data;
using PersonAPI.Routes;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<PersonContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // * Como estamos usando o .NET 9.0, não temos o Swagger, para resolver esse problema usamos OpenAPI + Scalar para documentar a API, para isso devemos abrir a aplicação em http/localhost:5051/scalar/v1
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.PersonRoutes();

app.UseHttpsRedirection();

app.Run();
