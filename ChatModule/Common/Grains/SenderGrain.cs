using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Orleans.Streams;

namespace Common;

[ImplicitStreamSubscription("SEND")]
public class SenderGrain : Grain, ISenderGrain
{
    private readonly ILogger<SenderGrain> _logger;
    private IAsyncStream<PlatformMessageRequest> _stream = null!;
    private readonly MessageBridge _messageBridge;
    private static string GrainType => nameof(SenderGrain);
    private string GrainKey => this.GetPrimaryKeyString();


    public SenderGrain(ILogger<SenderGrain> logger, MessageBridge messageBridge)
    {
        _logger = logger;
        _messageBridge = messageBridge;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{GrainType} {GrainKey} activated.", GrainType, GrainKey);
        var streamProvider = this.GetStreamProvider("chat");
        var streamId = StreamId.Create(
            "SEND", this.GetPrimaryKeyString());

        _stream = streamProvider.GetStream<PlatformMessageRequest>(
            streamId);

        await _stream.SubscribeAsync(SendMessage);

        await base.OnActivateAsync(cancellationToken);
    }


    public async Task<StreamId> GetStreamId()
    {
        return _stream.StreamId;
    }

    public async Task SendMessage(PlatformMessageRequest msg, StreamSequenceToken token)
    {
        _logger.LogInformation("Received message with ChatId:{ChatId}, Body: {Message}, ChatType:{ChatType}",
            msg.ChatId, msg.Message, msg.ChatType);
        await _messageBridge.Send(msg);
    }
}