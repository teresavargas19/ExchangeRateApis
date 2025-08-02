using Xunit;
using Moq;
using ExchangeRateComparer.Models;
using ExchangeRateComparer.Strategies;
using ExchangeRateComparer.Services;

namespace ExchangeRateComparer.Tests
{
    public class ExchangeRateServiceTests
    {
       
        [Fact]
        public async Task GetBestRateAsync_Returns_HighestRate()
        {
            var strategy1 = new Mock<IApiStrategy>();
            strategy1.Setup(s => s.Name).Returns("ApiMock1");
            strategy1.Setup(s => s.GetRateAsync()).ReturnsAsync(new ExchangeResult
            {
                SourceApi = "ApiMock1",
                ConvertedAmount = 78.5m
            });

            var strategy2 = new Mock<IApiStrategy>();
            strategy2.Setup(s => s.Name).Returns("ApiMock2");
            strategy2.Setup(s => s.GetRateAsync()).ReturnsAsync(new ExchangeResult
            {
                SourceApi = "ApiMock2",
                ConvertedAmount = 92.3m
            });

            var service = new ExchangeRateService(new[] { strategy1.Object, strategy2.Object });

            var result = await service.GetBestRateAsync();

            Assert.NotNull(result);
            Assert.Equal("ApiMock2", result.SourceApi);
            Assert.Equal(92.3m, result.ConvertedAmount);
        }

     
        [Fact]
        public async Task GetBestRateAsync_Returns_Null_WhenAllFail()
        {
            var strategy1 = new Mock<IApiStrategy>();
            strategy1.Setup(s => s.GetRateAsync()).ReturnsAsync((ExchangeResult?)null);

            var strategy2 = new Mock<IApiStrategy>();
            strategy2.Setup(s => s.GetRateAsync()).ReturnsAsync((ExchangeResult?)null);

            var service = new ExchangeRateService(new[] { strategy1.Object, strategy2.Object });

            var result = await service.GetBestRateAsync();

            Assert.Null(result);
        }

      
        [Fact]
        public async Task GetBestRateAsync_Returns_Valid_WhenSomeFail()
        {
            var failingStrategy = new Mock<IApiStrategy>();
            failingStrategy.Setup(s => s.GetRateAsync()).ReturnsAsync((ExchangeResult?)null);

            var validStrategy = new Mock<IApiStrategy>();
            validStrategy.Setup(s => s.Name).Returns("ValidAPI");
            validStrategy.Setup(s => s.GetRateAsync()).ReturnsAsync(new ExchangeResult
            {
                SourceApi = "ValidAPI",
                ConvertedAmount = 88.8m
            });

            var service = new ExchangeRateService(new[] { failingStrategy.Object, validStrategy.Object });

            var result = await service.GetBestRateAsync();

            Assert.NotNull(result);
            Assert.Equal("ValidAPI", result.SourceApi);
            Assert.Equal(88.8m, result.ConvertedAmount);
        }

      
        [Fact]
        public async Task GetBestRateAsync_Returns_First_WhenRatesEqual()
        {
            var strategy1 = new Mock<IApiStrategy>();
            strategy1.Setup(s => s.Name).Returns("API1");
            strategy1.Setup(s => s.GetRateAsync()).ReturnsAsync(new ExchangeResult
            {
                SourceApi = "API1",
                ConvertedAmount = 90.0m
            });

            var strategy2 = new Mock<IApiStrategy>();
            strategy2.Setup(s => s.Name).Returns("API2");
            strategy2.Setup(s => s.GetRateAsync()).ReturnsAsync(new ExchangeResult
            {
                SourceApi = "API2",
                ConvertedAmount = 90.0m
            });

            var service = new ExchangeRateService(new[] { strategy1.Object, strategy2.Object });

            var result = await service.GetBestRateAsync();

            Assert.NotNull(result);
            Assert.Equal(90.0m, result.ConvertedAmount);
            Assert.Equal("API1", result.SourceApi); // según la lógica de orden
        }
    }
}
