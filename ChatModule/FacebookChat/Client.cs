using Common;

namespace FacebookChat;

public class FacebookClient: IChatClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FacebookClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SendResult> Send(PlatformMessageRequest messageRequest)
    {
        var client = _httpClientFactory.CreateClient();
        // send post request 
        //"https://graph.facebook.com/LATEST-API-VERSION/PAGE-ID/messages
        // ?recipient={'id':'PSID'}
        // &messaging_type=RESPONSE
        // &message={'text':'hello,world'}
        // &access_token=PAGE-ACCESS-TOKEN
        Console.WriteLine($"Sending text {messageRequest.Message} to user {messageRequest.ChatId} via Facebook Messenger");
        return new SendResult()
        {
            ChatId = messageRequest.ChatId,
            Successful = true
        };
    }

    public PlatformMessageResponse ToPlatformMessage(IApiMessage apiMessage)
    {
        var message = (FacebookMessage)apiMessage;
        var user = GetUserProfile(message.Sender.Id);
        return new PlatformMessageResponse()
        {
            ChatType = ChatType.Facebook,
            Message = message.Body.Text,
            ChatId = message.Sender.Id,
            Timestamp = message.Timestamp,
            Sender = new Sender()
            {
                Id = user.Id,
                Name = user.Name
            }
        };
    }

    public FacebookProfile GetUserProfile(string userId)
    {
        var client = _httpClientFactory.CreateClient();
        // request user profile
        // https://graph.facebook.com/<PSID>?fields=first_name,last_name,name,id&access_token=<PAGE_ACCESS_TOKEN>
        return new FacebookProfile()
        {
            Id = userId,
            Gender = "male",
            Name = "Peter Chang",
            FirstName = "Peter",
            LastName = "Chang",
            Locale = "en_US",
        };
    }
    
}