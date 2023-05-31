using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FacebookChat;

public static class Configuration
{
    public static IServiceCollection ConfigureFacebookClient(this IServiceCollection services)
    {
        services.AddTransient<FacebookClient>()
            .AddTransient<IChatClient, FacebookClient>(s => s.GetService<FacebookClient>()!);
        

        return services;
    }

    public static void UseFacebookClient(this IHost app)
    {
        var clientProvider =  app.Services.GetService<ChatClientProvider>()!;
        clientProvider.Add(ChatType.Facebook,typeof(FacebookClient));
        if (app is not IEndpointRouteBuilder routeBuilder) return;
        // place special endpoints here
        routeBuilder.MapGet("/facebook/webhook",  (IConfiguration configuration) =>
        {
            // verify facebook webhook
            return Results.Ok();
        });
        routeBuilder.MapPost("/facebook/webhook/receive/",
            (FacebookMessage message, MessageBridge messageProcessor, FacebookClient client) =>
            {
                var platformMessage = client.ToPlatformMessage(message);
                messageProcessor.Receive(platformMessage);
            });

    }
}