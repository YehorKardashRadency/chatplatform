using Common;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Orleans.Streams;

namespace Grains;

[ImplicitStreamSubscription("RECEIVE")]
public class ChatGrain: Grain, IChatGrain
{
    private readonly IPersistentState<ChatState> _state;
    private readonly ILogger<ChatGrain> _logger;
    private IAsyncStream<PlatformMessageResponse> _stream = null!;

    private static string GrainType => nameof(ChatGrain);
    private string GrainKey => this.GetPrimaryKeyString();

    
    public ChatGrain(
        [PersistentState(stateName: "sender", storageName: "Storage")]
        IPersistentState<ChatState> state, ILogger<ChatGrain> logger)
    {
        _state = state;
        _logger = logger;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{GrainType} {GrainKey} activated.", GrainType, GrainKey);
        var streamProvider = this.GetStreamProvider("chat");
        var streamId = StreamId.Create(
            "RECEIVE", this.GetPrimaryKeyString());

        _stream = streamProvider.GetStream<PlatformMessageResponse>(
            streamId);

        await _stream.SubscribeAsync(ReceiveMessage);

        await base.OnActivateAsync(cancellationToken);
    }

    public async Task<bool> IsConfigured()
    {
        return _state.State.Configured;
    }
    
    public async Task Configure(ChatType chatType)
    {
        _state.State.ChatId = GrainKey;
        _state.State.ChatType = chatType;
        _state.State.Configured = true;
        await _state.WriteStateAsync();
        _logger.LogInformation("{GrainType} {GrainKey} initialized.", GrainType, GrainKey);
    }

    public async Task<StreamId> GetStreamId()
    {
        return _stream.StreamId;
    }
    
    public async Task ReceiveMessage(PlatformMessageResponse msg, StreamSequenceToken token)
    {
        _logger.LogInformation("Received message with ChatId:{ChatId}, Body: {Message}, ChatType:{ChatType}",
            _state.State.ChatId, msg.Message, _state.State.ChatType);
        _state.State.Messages.Add(msg);
        await _state.WriteStateAsync();
    }
}