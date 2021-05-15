using AngloAmerican.Account.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace AngloAmerican.Account.Services
{
    public class AddressService : IAddressService
    {
        // TO DO: read RandomuserRequestUri through config
        private const string RandomuserRequestUri = "https://randomuser.me/api/?nat=gb";
        private readonly ILogger<AddressService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AddressService(IHttpClientFactory httpClientFactory, ILogger<AddressService> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetAddress()
        {
            var http = _httpClientFactory.CreateClient();
            var response = await http.GetAsync(RandomuserRequestUri);
            var content = await response.Content.ReadAsStringAsync();
            var address = GetCityAndPostCode(content);

            return address;
        }

        private string GetCityAndPostCode(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
            dynamic city = jsonObject.results[0].location.city;
            dynamic postcode = jsonObject.results[0].location.postcode;

            var address = $"{city.ToString()} {postcode.ToString()}";

            return address;
        }
    }
}