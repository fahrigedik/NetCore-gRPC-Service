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

    // public override async Task SendMessage(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
    // {
    //     Console.WriteLine($"Clienttan gelen MessageRequest {request.Message } - {request.Name}");

    //     for (int i = 0; i < 10; i++)
    //     {
    //         await Task.Delay(1000);
    //         await responseStream.WriteAsync(new MessageResponse
    //         {
    //             Message = $"Serverdan gelen mesaj {i}"
    //         });
    //     }

    // }

    public override async Task<MessageResponse> SendMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext(context.CancellationToken))
        {
            System.Console.WriteLine( "Client'tan gelen stream Message" + requestStream.Current.Message);
        }

        return new MessageResponse
        {
            Message = "Serverdan gelen mesaj"
        };
    }

}
