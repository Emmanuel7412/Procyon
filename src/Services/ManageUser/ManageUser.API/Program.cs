// Licensed to the .NET Foundation under one or more agreements.

using System.Reflection;
using System.Text;
using Core;
using ManageUser.Application;
using ManageUser.Domain.Entities;
using ManageUser.Infrastructure.Data;
using ManageUser.Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Procyon.Core.Exceptions.Handler;
using Procyon.Core.Shared.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddDataInfrastructure(builder.Configuration);

// For identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Déclarer la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedHosts",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["AllowedHosts"] ?? throw new InvalidOperationException("AllowedHosts not configured."))
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.RegisterQueryHandlers();
builder.Services.RegisterCommandHandlers();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Set to true in production
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

var app = builder.Build();

// Utiliser la politique CORS
app.UseCors("AllowedHosts");

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsDevelopment())
{
    await app.Services.InitializeDatabaseAsync();
}
app.UseExceptionHandler(options => { });

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.MapControllers();
app.UseHttpLogging();
app.Run();
