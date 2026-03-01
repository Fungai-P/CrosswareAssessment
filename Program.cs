using CrosswareAssessment.Handlers;
using CrosswareAssessment.Infrastructure;
using CrosswareAssessment.Repositories;
using CrosswareAssessment.Services;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

services.AddEndpointsApiExplorer();

var mongoConnectionString = builder.Configuration.GetConnectionString("Mongo") ?? "mongodb://localhost:27017";
var mongoDbName = builder.Configuration["Mongo:Database"] ?? "posts_db";

services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConnectionString));
services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbName));

var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConnectionString));

// Repos + Handlers
services.AddScoped<IPostsRepository, PostsRepository>();
services.AddScoped<ILikesRepository, LikesRepository>();
services.AddScoped<IFeedsRepository, FeedsRepository>();

services.AddScoped<IPostHandler, PostHandler>();
services.AddScoped<ILikesHandler, LikesHandler>();
services.AddScoped<IFeedsHandler, FeedsHandler>();

services.AddScoped<ICacheService, RedisCacheSevice>();

services.AddHostedService<MongoIndexesHostedService>();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Likes API",
        Version = "v1"
    });

    c.AddSecurityDefinition("UserIdHeader", new OpenApiSecurityScheme
    {
        Name = "X-User-Id",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Enter a string user id (e.g. user-123)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UserIdHeader"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { ok = true }));

app.Run();
