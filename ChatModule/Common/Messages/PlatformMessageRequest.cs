namespace Common;

[GenerateSerializer]
public class PlatformMessageRequest
{
    [Id(0)]
    public string ChatId { get; set; }
    [Id(1)]
    public string Message { get; set; }
    [Id(2)]
    public ChatType ChatType { get; set; }
}