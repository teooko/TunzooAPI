using SignalRChat.Hubs;
using TunzooApi.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

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
app.MapHub<ChatHub>("/chatHub");

app.Run();