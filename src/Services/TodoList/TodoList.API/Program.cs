
using System.Reflection;
using System.Text;
using Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Procyon.Core.Exceptions.Handler;
using Procyon.Core.Shared.API.Extensions;
using TodoList.Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    //.AddApplicationServices(builder.Configuration)
    .AddDataInfrastructure(builder.Configuration);
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

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Set to true in production
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT signing key not configured (Jwt:Key).")))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Utiliser la politique CORS
app.UseCors("AllowedHosts");

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsDevelopment())
{
    //await app.Services.InitializeDatabaseAsync();
}
app.UseExceptionHandler(options => { });
app.UseAuthorization();

app.MapControllers();
