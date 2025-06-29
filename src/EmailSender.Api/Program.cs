using EmailSender.Application.Mappers;
using EmailSender.Infraestructure;
using EmailSender.Application;
using EmailSender.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddMapper();
builder.Services.AddCqrs();
builder.Services.AddDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
