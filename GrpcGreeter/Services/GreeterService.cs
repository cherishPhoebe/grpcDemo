using Grpc.Core;
using GrpcGreeter;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override async Task<HelloReply> SayHelloUnary(HelloRequest request, ServerCallContext context)
    {
        await Task.Delay(6000);
        var message = "Hello " + request.Name;
        return await Task.FromResult(new HelloReply { Message = message });
    }

    public override async Task SayHelloServerStreaming(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested)
        {
            var message = ($"Hello {request.Name} {++i}");
            await responseStream.WriteAsync(new HelloReply { Message = message });

            await Task.Delay(1000);
        }
    }

    public override async Task<HelloReply> SayHelloClientStreaming(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
    {
        var names = new List<string>();

        await foreach (var request in requestStream.ReadAllAsync())
        {
            names.Add(request.Name);
        }

        var message = string.Join(", ", names);
        return new HelloReply { Message = message };
    }

    public override async Task SayHelloBidirectionalStreaming(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(
                new HelloReply { Message = $"Hello {request.Name}" });
        }
    }


}
