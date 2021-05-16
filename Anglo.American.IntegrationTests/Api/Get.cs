using AngloAmerican.Account.Api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Anglo.American.IntegrationTests.Api
{
    public class Get : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _httpClient;

        public Get(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnAccounts()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/accounts");
            response.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountResponse>>(stringResponse).ToList();

            result.FirstOrDefault().Address.Should().NotBeNull();
            result.FirstOrDefault().TypeId.Should().BeInRange(1, 3);

            result.Should().NotBeEmpty();
            result.Should().HaveCount(7);
        }

        [Fact]
        public async Task ShouldReturnAccountsWithAddress()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/accounts");
            response.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountResponse>>(stringResponse).ToList();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(7);

            result.FirstOrDefault().Address.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnAccountsWithAccoutType()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/accounts");
            response.EnsureSuccessStatusCode();

            // Assert
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountResponse>>(stringResponse).ToList();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(7);

            result.FirstOrDefault().TypeId.Should().BeInRange(1, 3);
        }
    }
}