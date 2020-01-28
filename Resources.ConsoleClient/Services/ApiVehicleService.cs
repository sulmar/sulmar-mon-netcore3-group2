using Newtonsoft.Json;
using Resources.Domain.Models;
using Resources.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Resources.ConsoleClient.Services
{
    public class ApiVehicleService : IVehicleRepository
    {
        private readonly HttpClient client;

        public ApiVehicleService(ResourcesClient client)
        {
            this.client = client.Client;
        }

        public async Task AddAsync(Vehicle entity)
        {
            var json = JsonConvert.SerializeObject(entity);

            HttpContent content = new StringContent(json);

            var response = await client.PostAsync($"api/vehicles", content);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            entity = JsonConvert.DeserializeObject<Vehicle>(jsonResponse);

        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vehicle>> GetAsync(VehicleSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetAsync(string vin)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vehicle>> GetAsync()
        {
            var json = await client.GetStringAsync("api/vehicles");

            var results = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(json);

            return results;
        }

        public async Task<Vehicle> GetAsync(int id)
        {
            var json = await client.GetStringAsync($"api/vehicles/{id}");

            var result = JsonConvert.DeserializeObject<Vehicle>(json);

            return result;
        }

        public async Task RemoveAsync(int id)
        {
            await client.DeleteAsync($"api/vehicles/{id}");
        }

        public async Task UpdateAsync(Vehicle entity)
        {
            var json = JsonConvert.SerializeObject(entity);

            HttpContent content = new StringContent(json);

            await client.PutAsync($"api/vehicles/{entity.Id}", content);
        }
    }
}
