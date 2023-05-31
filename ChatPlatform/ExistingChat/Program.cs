using Common;
using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = new HostBuilder()
    .UseOrleansClient(clientBuilder =>
    {
        clientBuilder.UseLocalhostClustering(new[] { 30000, 30001 }).AddMemoryStreams("chat");
    })
    .UseConsoleLifetime()
    .Build();
await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();
var grain = client.GetGrain<ISenderGrain>(Guid.NewGuid());
var streamId = await grain.GetStreamId();
var provider = client.GetStreamProvider("chat");
var stream = provider.GetStream<PlatformMessageRequest>(streamId);

Console.WriteLine("Enter ChatId");
var chatId = Console.ReadLine() ?? throw new ArgumentNullException("ChatId cannot be null");

while (true)
{
    Console.WriteLine("Enter message text. Write /q to exit");
    var messageText = Console.ReadLine();
    if (messageText != null)
    {
        if(messageText=="/q") break;
        var msg = new PlatformMessageRequest(){ChatId = chatId,ChatType = ChatType.Facebook,Message = messageText};
        await stream.OnNextAsync(msg);
    } 
}
