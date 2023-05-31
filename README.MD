# Chat Conversation module

## ChatModule

Server that handles communication between ChatPlatform and other messaging platforms

```
ChatModule/
  Server/ - ASP.NET Core server that additionaly hosts SenderGrain
  Chats/ - Chat plugins
  Common/
    Grains/
      IChatGrain.cs - interface for the ChatPlatform grain
      ISenderGrain.cs, SenderGrain.cs - our grain that receives messages from ChatPlatform
    Messages/
      PlatformMessageRequest.cs - MessagePlatform-Chat message
      PlatformMessageResponse.cs - Chat-MessagePlatform message
    ChatGrainFactory.cs - utility factory for getting IChatGrain grain
    MessageBridge.cs - implements communication between ChatPlatform and our ChatModule
```

### Usage

```bash
cd ChatModule/ChatModule
dotnet run
```

## ChatPlatform

Implementation of ChatPlatform

```
ChatPlatform/
  ExistingChat/ - console chat implementation to demonstrate ChatPlatform to ChatModule communication
  Server/ - server that hosts IChatGrain
```

### Usage

```bash
# start server
cd ChatPlatform/Server
dotnet run
# start chat client
cd ChatPlatform/ExistingChat
dotnet run
```
