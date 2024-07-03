using Backend.API.Entities;
using System.Data;
using Backend.API.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddTransient<IDbConnection>((pgc) => new NpgsqlConnection(connectionString));

builder.Services.AddTransient(sp => new RabbitMQService(builder.Configuration.GetValue<string>("RabbitMQ")!));
builder.Services.AddTransient<GeneralService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

