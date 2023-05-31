using Common;

namespace Server;

public static class Configuration
{
    public static IServiceCollection ConfigureChatModule(this IServiceCollection services)
    {
        services.AddTransient<ChatGrainFactory>();
        services.AddTransient<MessageBridge>();
        services.AddSingleton<ChatClientProvider>();
        services.AddHttpClient();
        return services;
    }
}