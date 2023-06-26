using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NETApi;
using NETApi.Core.IServices;
using NETApi.Data;
using NETApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NetApiDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("PatientRegistration")));
builder.Services.AddTransient<INetApiDbContext, NetApiDbContext>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientsByDoctorService, PatientsByDoctorService>();

builder.Services.AddSingleton<IMapper>(AutoMapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
