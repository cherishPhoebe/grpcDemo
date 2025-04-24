using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcGreeterWebClient.Interceptors
{
    public class ExampleInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ExampleInterceptor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleInterceptor>();
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return base.BlockingUnaryCall(request, context, continuation);
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(
                HandleResponse(call.ResponseAsync),
                call.ResponseHeadersAsync,
                call.GetStatus,
                call.GetTrailers,
                call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> inner)
        {
            try
            {
                return await inner;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Custom error", ex);
            }
        }

    }
}
