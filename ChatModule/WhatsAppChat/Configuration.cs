using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhatsAppChat;

namespace FacebookChat;

public static class Configuration
{
    public static IServiceCollection ConfigureWhatsAppClient(this IServiceCollection services)
    {
        services.AddTransient<WhatsAppClient>()
            .AddTransient<IChatClient, WhatsAppClient>(s => s.GetService<WhatsAppClient>()!);

        // set callback with httpclient
        // PATCH /v1/settings/application
        // {
        //     "callback_persist": true,
        //     "sent_status": true,  // Either use this or webhooks.message.sent, but webhooks.message.sent property is preferred as sent_status will be deprecated soon
        //     "webhooks": { 
        //         "url": "webhook.your-domain", 
        //         "message": {     // Available on v2.41.2 and above
        //             "sent": false,
        //             "delivered": true,
        //             "read": false
        //         },
        //     }
        // }

        return services;
    }

    public static void UseWhatsAppClient(this IHost app)
    {
        var clientProvider = app.Services.GetService<ChatClientProvider>()!;
        clientProvider.Add(ChatType.WhatsApp, typeof(WhatsAppClient));
        if (app is not IEndpointRouteBuilder routeBuilder) return;
        routeBuilder.MapPost("/whatsapp/webhook/receive/",
            (WhatsAppMessage message, MessageBridge messageProcessor, WhatsAppClient client) =>
            {
                var platformMessage = client.ToPlatformMessage(message);
                messageProcessor.Receive(platformMessage);
            });
    }
}