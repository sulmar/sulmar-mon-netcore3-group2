using Bogus;
using Messaging.Domain;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.SenderConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hello Signal-R Sender!");

            const string url = "http://localhost:5030/signalr/messages";

            // dotnet add package Microsoft.AspNetCore.SignalR.Client

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            IEnumerable<Message> messages = new Faker<Message>()
                  .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                  .RuleFor(p => p.Content, f => f.Lorem.Paragraph())
                  .GenerateForever();

            string[] units = new string[] { "UnitA", "UnitB" };
            Random random = new Random();

            foreach(var message in messages)
            {
                //Message message = new Message 
                //{
                //    Title = "Hello Signal-R!", 
                //    Content = "Signal-R is awesome!" };

                // await connection.SendAsync("SendMessage", message);

                string unit = units[random.Next() % 2];

                await connection.SendAsync("SendMessageToUnit", message, unit);

                Console.WriteLine($"Sent {message.Title} to [{unit}]");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();


        }
    }
}
