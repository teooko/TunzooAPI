using SignalRChat.Hubs;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using TunzooAPI.Constants;
using TunzooApi.Domain.Entities;
using TunzooAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var connection = ConnectionMultiplexer.Connect($"{DbConstants.HostName}:{DbConstants.PortNumber},password={DbConstants.Password}");
    return connection;
});

builder.Services.AddScoped<RedisService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5174") // Replace with your client URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/lobby", (RedisService redisService) =>
{
    var lobby = redisService.SetLobby();
    return Results.Ok(lobby.LobbyId);
});
app.UseHttpsRedirection();

app.UseCors();

app.MapHub<LobbyHub>("/hubs/lobby");

app.Run();