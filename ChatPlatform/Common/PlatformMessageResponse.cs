namespace Common;

[GenerateSerializer]
public class Sender
{
    [Id(0)]
    public string Id { get; set; }
    [Id(1)]
    public string Name { get; set; }
    [Id(2)]
    public string? Email { get; set; }
}
[GenerateSerializer]
public class PlatformMessageResponse
{
    [Id(0)]
    public string ChatId { get; set; }
    [Id(1)]
    public string Message { get; set; }
    [Id(2)]
    public ChatType ChatType { get; set; }
    [Id(3)]
    public long Timestamp { get; set; }
    [Id(4)]
    public Sender Sender { get; set; }
}