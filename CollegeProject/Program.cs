using CollegeProject.DbContexts;
using CollegeProject.GlobalExceptions;
using CollegeProject.Mapper;
using CollegeProject.Repositories;
using CollegeProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(builder.Configuration["logFilePath:path"] + "log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
//
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CollegeDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeDbConnection")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = (builder.Configuration["Jwt:Isssuer"]),
            ValidAudience = (builder.Configuration["Jwt:Audience"]),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });
builder.Services.AddScoped<IRegistrationRepository , RegistrationRepository>();
builder.Services.AddScoped<ILoginRepository , LoginRepository>();
builder.Services.AddScoped<IRoleRepository , RoleRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CustomGlobalException>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
