using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcServer
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task GetStream(EmptyRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            while (true)
            {
                responseStream.WriteAsync(new HelloReply() { Message = new Random().Next().ToString() });
                await Task.Delay(100);
            }
        }
    }
}
