using Common;
using GoogleChat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FacebookChat;

public static class Configuration
{
    public static IServiceCollection ConfigureGoogleClient(this IServiceCollection services)
    {
        services.AddTransient<GoogleClient>()
            .AddTransient<IChatClient, GoogleClient>(s => s.GetService<GoogleClient>()!);
        
        // add specific services, configurations, etc.
        
        return services;
    }
    public static void UseGoogleClient(this IHost app)
    {
        var clientProvider =  app.Services.GetService<ChatClientProvider>()!;
        clientProvider.Add(ChatType.Google,typeof(GoogleClient));
        if (app is not IEndpointRouteBuilder routeBuilder) return;
        routeBuilder.MapPost("/google/webhook/receive/",
            (GoogleMessage message, MessageBridge messageProcessor, GoogleClient client) =>
            {
                var platformMessage = client.ToPlatformMessage(message);
                messageProcessor.Receive(platformMessage);
            });
        
    }
}