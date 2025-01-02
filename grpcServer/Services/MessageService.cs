using Grpc.Core;
using grpcMessageServer;

namespace grpcMessageServer.Services;


public class MessageService : Message.MessageBase
{
    private readonly ILogger<MessageService> _logger;
    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger;
    }

    public override Task<MessageResponse> SendMessage(MessageRequest request, ServerCallContext context)
    {
        System.Console.WriteLine(request.Name);
        return Task.FromResult(new MessageResponse
        {
            Message = " Selam " + request.Name
        });
    }
}
