using EventService.Application.Queries.Handlers;
using EventService.Infrastructure;
using EventService.Infrastructure.Impl;
using EventService.Models;
using EventService.Services.Impl;
using EventService.Services;
using Microsoft.EntityFrameworkCore;
using EventService.Validations.Commands;
using FluentValidation;
using EventService.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllEventsQueryHandler>());


builder.Services.AddDbContext<EventDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IGenericRepository<Event>, EventRepository>();

//RedisCache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "eventservice-redis";
});
builder.Services.AddSingleton(typeof(ICacheService<>), typeof(RedisCacheService<>));

//FluentValidation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<CreateEventCommandValidator>();

//AzureFunctions
builder.Services.AddHttpClient<IAzureFunctionService, AzureFunctionService>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5001";
app.Urls.Add($"http://localhost:{port}");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
