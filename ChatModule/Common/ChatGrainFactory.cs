using Microsoft.Extensions.Logging;
using Orleans.Streams;

namespace Common;

public class ChatGrainFactory
{
    private readonly IClusterClient _clusterClient;
    private ILogger<ChatGrainFactory> _logger;
    private string? _chatId;

    public ChatGrainFactory(IClusterClient clusterClient, ILogger<ChatGrainFactory> logger)
    {
        _clusterClient = clusterClient;
        _logger = logger;
    }
    public IChatGrain? GetGrain()
    {
        if (_chatId == null) throw new Exception("ChatId is not set");
        try
        {
            var grain = _clusterClient.GetGrain<IChatGrain>(_chatId);
            return grain;
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<IAsyncStream<PlatformMessageResponse>> GetSendingStream()
    {
        var grain = GetGrain();
        var streamId = await grain.GetStreamId();
        var provider = _clusterClient.GetStreamProvider("chat");
        return provider.GetStream<PlatformMessageResponse>(streamId);
    }
    public async Task SetChat(string chatId, ChatType chatType)
    {
        _chatId = chatId;
        var grain = GetGrain();
        var grainConfigured = await grain.IsConfigured();
        if (!grainConfigured) await grain.Configure(chatType);
    }
    
}