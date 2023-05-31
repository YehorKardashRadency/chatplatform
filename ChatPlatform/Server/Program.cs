using System.Net;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder
            .UseLocalhostClustering(siloPort: 11111)
            .AddMemoryGrainStorage("Storage")
            .AddMemoryGrainStorage("PubSubStore")
            .AddMemoryStreams("chat")
            .UseDashboard();
    })
    .UseConsoleLifetime()
    .RunConsoleAsync();