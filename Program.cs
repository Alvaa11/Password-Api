using Routes.UsersRoutes;
using Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UsersContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    DatabaseFacade database = new DatabaseFacade(new UsersContext());
    database.EnsureCreated();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UsersRoute();

app.UseHttpsRedirection();

app.Run();
