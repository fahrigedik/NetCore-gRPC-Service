// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using grpcServer;
using grpcMessageServer;
using System.Linq.Expressions;
using System.Net.Cache;

Console.WriteLine("Hello, World!");

var channel = GrpcChannel.ForAddress("http://localhost:5219");
var greetClient = new Greeter.GreeterClient(channel);
var messageClient = new Message.MessageClient(channel);

HelloReply result = await greetClient.SayHelloAsync(new HelloRequest { Name = "Fahri Gedik " });


// Unary RPC
// MessageResponse messageResponse = await messageClient.SendMessageAsync(new MessageRequest { Name= "Şehide Sena Demirel", Message= "bu bir Unary RPC örneğidir." });

// var streamMessageResponse = messageClient.SendMessage(new  MessageRequest { Name = "Fahri Gedik", Message = "Bu bir Server Streaming RPC Örneğidir."});


Console.WriteLine( $"Unary RPC Response {result.Message}");
// Console.WriteLine($"Message Client Response : {messageResponse.}");


//  CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

//  while (await streamMessageResponse.ResponseStream.MoveNext(cancellationTokenSource.Token))
//  {
//      var item = streamMessageResponse.ResponseStream.Current;
//      Console.WriteLine($"Server Streaming RPC Response : {item.Message}");
//  }

 

 //Clienttan stream request gönderildikten sonra tek bir parça olarak response gönderilir.

 var clientStreamRequest = messageClient.SendMessage();

 for (int i = 0; i < 10; i++)
 {
     await clientStreamRequest.RequestStream.WriteAsync(new MessageRequest
     {
         Name = "Fahri Gedik",
         Message = $"Client Stream Message {i}"
     });
 } 
await clientStreamRequest.RequestStream.CompleteAsync();

var clientStreamResponse = await clientStreamRequest.ResponseAsync;

Console.WriteLine($"Server Response : {clientStreamResponse.Message}");