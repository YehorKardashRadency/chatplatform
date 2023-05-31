namespace Common;

public class MessageBridge
{
    private readonly ChatGrainFactory _chatGrainFactory;
    private readonly ChatClientProvider _chatClientProvider;
    public MessageBridge(ChatGrainFactory chatGrainFactory, ChatClientProvider chatClientProvider)
    {
        _chatGrainFactory = chatGrainFactory;
        _chatClientProvider = chatClientProvider;
    }
    // public async Task Receive(ChatType chatType, IApiMessage message)
    public async Task Receive(PlatformMessageResponse message)
    {
        // var client = _chatClientProvider.Get(message.ChatType);
        // var platformMessage = client.ToPlatformMessage(message);
        await _chatGrainFactory.SetChat(message.ChatId, message.ChatType);
        var stream = await _chatGrainFactory.GetSendingStream();
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