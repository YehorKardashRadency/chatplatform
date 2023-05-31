using Common;

namespace GoogleChat;

public class GoogleClient: IChatClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GoogleClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SendResult> Send(PlatformMessageRequest messageRequest)
    {
        var client = _httpClientFactory.CreateClient();
        // message.ChatId = spaces/AAAAAAAAAAA
        // send post request 
        // POST https://chat.googleapis.com/v1/spaces/AAAAAAAA/messages
        // { "text": "message"}
        Console.WriteLine($"Sending text ${messageRequest.Message} to space ${messageRequest.ChatId} via Google Chat");
        return new SendResult()
        {
            ChatId = messageRequest.ChatId,
            Successful = true
        };
    }

    public PlatformMessageResponse ToPlatformMessage(IApiMessage apiMessage)
    {
        var message = (GoogleMessage)apiMessage;
        return new PlatformMessageResponse()
        {
            ChatType = ChatType.Google,
            Message = message.Body.Text,
            ChatId = message.GoogleSpace.Name,
            Timestamp = DateTime.Parse(message.EventTime).Ticks,
        };
    }
    
}