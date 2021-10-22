using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dispatcher.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Events;
using Shared.Models;

namespace Dispatcher.ExternalServices
{
    public class OutboxService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        private string _url;
        public OutboxService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
            _url = _config["ExternalServices:OutboxService"];
        }

        public async Task<List<OutboxEvent>> GetUnpublishedEventsAsync()
        {
            var response = await _client.GetAsync(_url);

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<OutboxEvent>>(jsonString);

            return result;
        }

        public async Task UpdateEventsAsync(List<UpdateEventStateRequest> request)
        {
            var requestBody = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _client.PatchAsync(_url, requestBody);
            
            response.EnsureSuccessStatusCode();
        }
    }
}