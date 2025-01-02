// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using grpcServer;
using grpcMessageServer;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("http://localhost:5219");
var greetClient = new Greeter.GreeterClient(channel);
var messageClient = new Message.MessageClient(channel);

HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "Fahri Gedik " });

MessageResponse messageResponse = await messageClient.SendMessageAsync(new MessageRequest { Name= "Şehide Sena Demirel", Message= "bu bir Unary RPC örneğidir." });


Console.WriteLine(result.Message);
Console.WriteLine($"Message Client Response : {messageResponse.Message}");


