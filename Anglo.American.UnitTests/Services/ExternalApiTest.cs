using AngloAmerican.Account.Services;
using FluentAssertions;
using Xunit;

namespace Anglo.American.UnitTests.Services
{
    public class ExternalApiTest
    {
        [Theory]
        [InlineData("Rene")]
        [InlineData("Kirk")]
        [InlineData("Escarole")]
        public void Should_CheckAccountBalance_Return_False_For_Person_Is_In_List_And_Balance_GreaterThan_10000(string lastName)
        {
            // Arrange
            var balance = 10001;
            var externalApi = new ExternalApi();

            // Act
            var checkAccountBalanceResult = externalApi.CheckAccountBalance(balance, lastName);

            // Assert
            checkAccountBalanceResult.Should().BeFalse();
        }

        [Theory]
        [InlineData("Rene")]
        [InlineData("Kirk")]
        [InlineData("Escarole")]
        public void Should_CheckAccountBalance_Return_True_For_Person_Is_In_List_And_Balance_LessThan_10000(string lastName)
        {
            // Arrange
            var balance = 9999;
            var externalApi = new ExternalApi();

            // Act
            var checkAccountBalanceResult = externalApi.CheckAccountBalance(balance, lastName);

            // Assert
            checkAccountBalanceResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("ReneTest")]
        [InlineData("KirkTest")]
        [InlineData("EscaroleTest")]
        public void Should_CheckAccountBalance_Return_True_For_Person_Is_Not_In_List_And_Balance_LessThan_10000(string lastName)
        {
            // Arrange
            var balance = 9999;
            var externalApi = new ExternalApi();

            // Act
            var checkAccountBalanceResult = externalApi.CheckAccountBalance(balance, lastName);

            // Assert
            checkAccountBalanceResult.Should().BeTrue();
        }

        [Theory]
        [InlineData("ReneTest")]
        [InlineData("KirkTest")]
        [InlineData("EscaroleTest")]
        public void Should_CheckAccountBalance_Return_True_For_Person_Is_Not_In_List_And_Balance_GreaterThan_10000(string lastName)
        {
            // Arrange
            var balance = 10001;
            var externalApi = new ExternalApi();

            // Act
            var checkAccountBalanceResult = externalApi.CheckAccountBalance(balance, lastName);

            // Assert
            checkAccountBalanceResult.Should().BeTrue();
        }
    }
}