namespace Common;

public class MessageBridge
{
    private readonly ChatFactory _chatFactory;
    private readonly ChatClientProvider _chatClientProvider;
    public MessageBridge(ChatFactory chatFactory, ChatClientProvider chatClientProvider)
    {
        _chatFactory = chatFactory;
        _chatClientProvider = chatClientProvider;
    }
    // public async Task Receive(ChatType chatType, IApiMessage message)
    public async Task Receive(PlatformMessageResponse message)
    {
        // var client = _chatClientProvider.Get(message.ChatType);
        // var platformMessage = client.ToPlatformMessage(message);
        await _chatFactory.SetChat(message.ChatId, message.ChatType);
        var stream = await _chatFactory.GetSendingStream();
        await stream.OnNextAsync(message);
    }

    public async Task Send(PlatformMessageRequest message)
    {
        var client = _chatClientProvider.Get(message.ChatType);
        var result = await client.Send(message);
        if (!result.Successful)
        {
            Console.WriteLine($"Send request to chat ${result.ChatId} did not succeed. Details: {result.Details}");
        }
    }
}