using FluentValidation;
using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.API.Validators;
using HotelManagement.Api.Business;
using HotelManagement.Api.Business.Implementations;
using HotelManagement.Api.Data;
using HotelManagement.Api.Data.Repositories;
using HotelManagement.Api.Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(builder.Configuration.GetConnectionString("Db")); });

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<IValidator<CreateRoomRequestDto>, CreateRoomRequestValidator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }