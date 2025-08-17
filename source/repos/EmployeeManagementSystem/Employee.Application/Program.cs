using Employee.Application.Controllers;
using Employee.Application.Middleware;
using Employee.Application.Validators;
using Employee.BLL.DI;
using Employee.BLL.Interfaces;
using Employee.BLL.Mappings;
using Employee.BLL.Services;
using Employee.BLL.Validators;
using Employee.Core.Utilities;
using Employee.Dal.Context;
using Employee.Dal.Entities;
using Employee.Dal.Interfaces;
using Employee.Dal.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NzWalks.API.CustomActionFilter;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var logger = new LoggerConfiguration().
    WriteTo.Console()
    .WriteTo.File("Logs/Employee_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

builder.Services.AddFluentValidationAutoValidation(); 

// Register all your validators (request and entity level)
builder.Services.AddValidatorsFromAssemblyContaining<DepartmentUpsertBOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEmployeeBOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeBOValidator>();

builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//swagger setup for the authorizeze feauee 
builder.Services.AddSwaggerGen(options =>
{
    //This sets up the metadata for the Swagger UIs
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Employee API",
        Version = "v1"
    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});




builder.Services.AddDbContext<EmployeeDbContext>(options =>{ options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnectionString"));});

// Register the repositories and unit of work in the Seperate folder 
builder.Services.AddApplicationService();

// this will dynamically gets and inject the configuration for the Google authentication
builder.Services.AddOptions<GoogleOptions>()
    .BindConfiguration("Google")
    .ValidateDataAnnotations()
    .ValidateOnStart();

// to use the custom middlewares which are implementing in the IMidddleware interface needs to use AddTransient Method
builder.Services.AddTransient<CustomAuthMiddleware>();
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

//add auth method
// 1.defining the default authentication scheme
//2. adding the JwtBearer authentication scheme
// 3.Passing the TokenValidationParameters to validate the JWT token

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthentication().AddGoogle(GoogleDefaults.AuthenticationScheme ,options =>
{
    options.ClientId = builder.Configuration["Google:client_id"];
    options.ClientSecret = builder.Configuration["Google:client_secret"];
});

builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();

var app = builder.Build();


app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseMiddleware<CustomAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
