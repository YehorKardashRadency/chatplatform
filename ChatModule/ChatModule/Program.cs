using System.Net;
using ChatModule;
using FacebookChat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseLocalhostClustering(siloPort: 11112, primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, 11111))
        .AddMemoryGrainStorage("Storage")
        .AddMemoryGrainStorage("PubSubStore")
        .AddMemoryStreams("chat");
});
builder.Services.ConfigureChatModule();
builder.Services.ConfigureFacebookClient();
builder.Services.ConfigureGoogleClient();
builder.Services.ConfigureWhatsAppClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFacebookClient();
app.UseGoogleClient();
app.UseWhatsAppClient();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();