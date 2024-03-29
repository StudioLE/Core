using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace StudioLE.Extensions.Logging.Cache.Tests;

internal sealed class CacheLoggerProviderTests
{
    [Test]
    public void CacheLoggerProvider_CreateLogger([Values] LogLevel logLevel)
    {
        // Arrange
        const string message = "Hello, world!";
        CacheLoggerProvider cache = new();
        ILogger logger = cache.CreateLogger("ExampleContext");

        // Act
        logger.Log(logLevel, message);

        // Assert
        Assert.That(cache.Logs.Count, Is.EqualTo(1));
        LogEntry log = cache.Logs.First();
        Assert.That(log.LogLevel, Is.EqualTo(logLevel));
        Assert.That(log.Message, Is.EqualTo(message));
    }
}
