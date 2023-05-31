using Common;

public class WAProfile
{
    public required string Name { get; set; }
}

public class WAContact
{
    public required WAProfile WaProfile { get; set; }
    public required string WaId { get; set; }
}

public class WAText
{
    public required string Body { get; set; }
}

public class WAMessage
{
    public required string From { get; set; }
    public required string Id { get; set; }
    public required string Timestamp { get; set; }
    public required WAText WaText { get; set; }
    public required string Type { get; set; }
}

public class WhatsAppMessage : IApiMessage
{
    public required List<WAContact> Contacts { get; set; }
    public required List<WAMessage> Messages { get; set; }
}