using Common;

namespace FacebookChat;

public class IdField
{
    public required string Id { get; set; }
}

public class FacebookMessageBody
{
    public required string Mid { get; set; }
    public required string Text { get; set; }
}
public class FacebookMessage: IApiMessage
{
    public required IdField Sender { get; set; }
    public required IdField Recipient { get; set; }
    public required long Timestamp { get; set; }
    public required FacebookMessageBody Body { get; set; }
}