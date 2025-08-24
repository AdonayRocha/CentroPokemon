using Infrastructure.Data;
using Infrastructure.Repositories;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Oracle EF Core
var conn = builder.Configuration.GetConnectionString("Oracle");
builder.Services.AddDbContext<PokemonDbContext>(opt =>
    opt.UseOracle(conn));

builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
