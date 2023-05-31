namespace Common;

public class ChatClientProvider
{
    public ChatClientProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private Dictionary<ChatType, Type> ClientTypes { get; set; } = new();

    private readonly IServiceProvider _serviceProvider;
    
    public void Add(ChatType chatType, Type clientType)
    {
        Console.WriteLine($"Add {chatType}");
        ClientTypes.Add(chatType,clientType);

    }

    public IChatClient Get(ChatType chatType)
    {
        return (IChatClient) _serviceProvider.GetService(ClientTypes[chatType]);
    }
}