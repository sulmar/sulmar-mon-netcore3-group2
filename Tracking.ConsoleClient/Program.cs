using Bogus;
using Grpc.Net.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Tracking.API;

namespace Tracking.ConsoleClient
{
    class Program
    {
        // <= C#7.0
       // static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC Client!");

            const string url = "https://localhost:5041";

            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Latitude, f => f.Random.Float(-90f, 90f))
                .RuleFor(p => p.Longitude, f => f.Random.Float(-90f, 90f))
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 120))
                .RuleFor(p => p.Direction, f=>f.Random.Float())
                .GenerateForever();
            
            var channel = GrpcChannel.ForAddress(url);
            var client = new Tracking.API.TrackingService.TrackingServiceClient(channel);

            Stopwatch stopwatch = new Stopwatch();


            foreach (var request in locations)
            {
                stopwatch.Restart();

                var response = await client.AddLocationAsync(request);

                stopwatch.Stop();

                Console.WriteLine($"elapsed {stopwatch.ElapsedMilliseconds} ms {response.IsConfirmed} ");

                await Task.Delay(TimeSpan.FromMilliseconds(1000));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();



        }
    }
}
