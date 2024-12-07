using SignalRChat.Hubs;
using Swashbuckle.AspNetCore.SwaggerGen;
using TunzooApi.Domain.Entities;
using TunzooAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

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

var rep = new Repository();
rep.PingDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/lobby", () =>
{
    var lobby = new Lobby();
    return Results.Ok(lobby.LobbyId);
});
app.UseHttpsRedirection();

app.UseCors();

app.MapHub<LobbyHub>("/hubs/lobby");

app.Run();