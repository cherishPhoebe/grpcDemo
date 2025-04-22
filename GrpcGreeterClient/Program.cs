// See https://aka.ms/new-console-template for more information

using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7194");

var client = new Greeter.GreeterClient(channel);

var callOption = new CallOptions();
callOption.WithDeadline(DateTime.UtcNow.AddSeconds(5));

var reply = await client.SayHelloUnaryAsync(new HelloRequest { Name = "GreeterClient" }, callOption);

//var call = client.SayHelloServerStreaming(new HelloRequest { Name = "World" });

//while (await call.ResponseStream.MoveNext())
//{
//    Console.WriteLine("Greeting: " + call.ResponseStream.Current.Message);
//    // "Greeting: Hello World" is written multiple times
//}

Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

