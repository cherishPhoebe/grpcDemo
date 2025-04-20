// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcGreeterClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7194");

var client = new Greeter.GreeterClient(channel);
var reply = client.SayHello(new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

