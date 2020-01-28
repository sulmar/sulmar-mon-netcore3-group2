using Microsoft.Extensions.DependencyInjection;
using Resources.ConsoleClient.Services;
using Resources.Domain.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Resources.ConsoleClient
{
    class Program
    {
        // dotnet add package Microsoft.Extensions.DependencyInjection
        // dotnet add package Microsoft.Extensions.Http
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            var credentialBytes = Encoding.ASCII.GetBytes("marcin:123");
            var credentials = Convert.ToBase64String(credentialBytes);

            services.AddScoped<IVehicleRepository, ApiVehicleService>();
            services.AddHttpClient<ResourcesClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            });

            using (var serviceProvider = services.BuildServiceProvider())
            {
                IVehicleRepository vehicleRepository = serviceProvider.GetRequiredService<IVehicleRepository>();

                var vehicles = await vehicleRepository.GetAsync();
            }
        }
    }

    public class ResourcesClient
    {
        public ResourcesClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}
