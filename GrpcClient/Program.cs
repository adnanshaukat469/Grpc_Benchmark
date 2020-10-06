using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var request = new HelloRequest() { Name = "Adnan" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);
            var response = await client.SayHelloAsync(request);

            Console.WriteLine(response.Message);

            var stream = client.GetStream(new EmptyRequest()).ResponseStream;
            while (await stream.MoveNext())
            {
                Console.WriteLine(stream.Current.Message);
            }
        }
    }
}
