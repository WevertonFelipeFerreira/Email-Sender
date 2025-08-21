using EmailSender.Api.Middlewares;
using EmailSender.Api.ProblemDetail;
using EmailSender.Application;
using EmailSender.Application.Mappers;
using EmailSender.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDb(builder.Configuration);
builder.Services.AddMapper();
builder.Services.AddCqrs();
builder.Services.AddRepositories();
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddCustomModelStateValidation();

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
