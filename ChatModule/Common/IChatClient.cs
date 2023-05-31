namespace Common;

public interface IChatClient
{
    public Task<SendResult> Send(PlatformMessageRequest messageRequest);
    public PlatformMessageResponse ToPlatformMessage(IApiMessage apiMessage);
}