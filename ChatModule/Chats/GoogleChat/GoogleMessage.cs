using Common;

namespace GoogleChat;

public class GoogleSender
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public required string AvatarUrl { get; set; }
    public required string Email { get; set; }
}

public class GoogleThread
{
    public required string Name { get; set; }
}

public class GoogleSpace
{
    public required string Name { get; set; }
    public required string DisplayName { get; set; }
    public required string Type { get; set; }
}

public class GoogleMessageBody
{
    public required string Name { get; set; }
    public required GoogleSender GoogleSender { get; set; }
    public required string CreateTime { get; set; }
    public required string Text { get; set; }
    public required GoogleThread GoogleThread { get; set; }
}

public class GoogleMessage: IApiMessage
{
    public required string Type { get; set; }
    public required string EventTime { get; set; }
    public required GoogleSpace GoogleSpace { get; set; }
    public required GoogleMessageBody Body { get; set; }
}
