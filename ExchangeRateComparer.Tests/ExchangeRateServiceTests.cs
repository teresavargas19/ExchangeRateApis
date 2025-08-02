using Xunit;
using Moq;
using ExchangeRateComparer.Models;
using ExchangeRateComparer.Strategies;
using ExchangeRateComparer.Services;

namespace ExchangeRateComparer.Tests;

public class ExchangeRateServiceTests
{
    [Fact]
    public async Task GetBestRateAsync_Returns_HighestRate()
    {
        // Arrange
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

        // Act
        var result = await service.GetBestRateAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApiMock2", result.SourceApi);
        Assert.Equal(92.3m, result.ConvertedAmount);
    }

    [Fact]
    public async Task GetBestRateAsync_Returns_Null_WhenAllFail()
    {
        // Arrange
        var strategy1 = new Mock<IApiStrategy>();
        strategy1.Setup(s => s.GetRateAsync()).ReturnsAsync((ExchangeResult?)null);

        var strategy2 = new Mock<IApiStrategy>();
        strategy2.Setup(s => s.GetRateAsync()).ReturnsAsync((ExchangeResult?)null);

        var service = new ExchangeRateService(new[] { strategy1.Object, strategy2.Object });

        // Act
        var result = await service.GetBestRateAsync();

        // Assert
        Assert.Null(result);
    }
}
