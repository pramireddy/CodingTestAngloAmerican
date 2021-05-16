using AngloAmerican.Account.Services;
using AngloAmerican.Account.Services.Interfaces;
using Moq;
using Xunit;

namespace Anglo.American.UnitTests.Services
{
    public class BalanceCheckerTest
    {
        private readonly Mock<IExternalApi> _mockExternalApi;
        private readonly Mock<INotification> _mockNotification;

        public BalanceCheckerTest()
        {
            _mockExternalApi = new Mock<IExternalApi>();
            _mockNotification = new Mock<INotification>();
        }

        [Fact]
        public void Should_BalanceChecker_Process_Return_True_For_Balance_LessThan_10000()
        {
            // arrange
            var balance = 999;
            var lastName = "TestUser";

            var balanceChecker = new BalanceChecker(_mockExternalApi.Object, _mockNotification.Object);
            // act
            var processResult = balanceChecker.Process(balance, lastName);

            //assert
            _mockNotification.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            _mockExternalApi.Verify(x => x.CheckAccountBalance(balance, lastName), Times.Never());
        }

        [Fact]
        public void Should_BalanceChecker_Process_Return_True_For_Balace_GreaterThan_10000()
        {
            // arrange
            var balance = 10001;
            var lastName = "TestUser";

            var balanceChecker = new BalanceChecker(_mockExternalApi.Object, _mockNotification.Object);
            // act
            var processResult = balanceChecker.Process(balance, lastName);

            //assert
            _mockNotification.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _mockNotification.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once());
            _mockExternalApi.Verify(x => x.CheckAccountBalance(balance, lastName), Times.Once());
        }

        [Theory]
        [InlineData("Rene")]
        [InlineData("Kirk")]
        [InlineData("Escarole")]
        public void Should_BalanceChecker_Process_Return_False_For_Balace_GreaterThan_10000(string lastName)
        {
            // arrange
            var balance = 10001;

            var balanceChecker = new BalanceChecker(_mockExternalApi.Object, _mockNotification.Object);
            // act
            var processResult = balanceChecker.Process(balance, lastName);

            //assert
            _mockNotification.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            _mockNotification.Verify(x => x.SendMessage(It.IsAny<string>()), Times.Once());
            _mockExternalApi.Verify(x => x.CheckAccountBalance(balance, lastName), Times.Once());
        }
    }
}