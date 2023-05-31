using Common;

namespace Grains;

[GenerateSerializer]
public record  ChatState
{
    [Id(0)]
    public ChatType ChatType { get; set; }
    
    [Id(1)]
    public string ChatId { get; set; }

    [Id(2)] 
    public bool Configured { get; set; } = false;
    [Id(3)] 
    public List<PlatformMessageResponse> Messages { get; set; } = new();
}