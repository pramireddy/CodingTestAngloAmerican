using AngloAmerican.Account.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Anglo.American.IntegrationTests.Api.AccountTypes
{
    public class Get : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public Get(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnAccountTypes()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/accounttype");
            response.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountType>>(stringResponse).ToList();

            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
        }

    }
}
