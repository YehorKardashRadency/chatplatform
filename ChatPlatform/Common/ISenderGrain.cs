using Common;
using Orleans.Runtime;
using Orleans.Streams;

namespace Common;

public interface ISenderGrain: IGrainWithGuidKey
{
    public Task<StreamId> GetStreamId();
    public Task SendMessage(PlatformMessageRequest msg, StreamSequenceToken token);
}