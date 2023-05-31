using ChatModule;
using Common;
using FacebookChat;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder
            .UseLocalhostClustering(siloPort: 11112)
            .AddMemoryGrainStorage("Storage")
            .AddMemoryGrainStorage("PubSubStore")
            .AddMemoryStreams("chat");
    })
    .ConfigureServices(services =>
        {
            services.ConfigureChatModule();
            services.ConfigureFacebookClient();
            services.ConfigureGoogleClient();
            services.ConfigureWhatsAppClient();
        }
    )
    .UseConsoleLifetime();
var app = host.Build();
app.UseFacebookClient();
app.UseGoogleClient();
app.UseWhatsAppClient();
app.Run();