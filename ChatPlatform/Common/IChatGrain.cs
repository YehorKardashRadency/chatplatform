using Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Common;

public interface IChatGrain: IGrainWithStringKey
{
    public Task<bool> IsConfigured();
    public Task Configure(ChatType chatType);
    public Task<StreamId> GetStreamId();
    public Task ReceiveMessage(PlatformMessageResponse msg, StreamSequenceToken token);
}