using Microsoft.Extensions.Logging;

namespace StudioLE.Extensions.Logging.Cache;

/// <summary>
/// A log entry for <see cref="CacheLogger"/>.
/// </summary>
public class LogEntry
{
    /// <summary>
    /// The log category name.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The log level.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// The event id.
    /// </summary>
    public EventId EventId { get; set; }

    /// <summary>
    /// The state.
    /// </summary>
    public Type? State { get; set; }

    /// <summary>
    /// The exception.
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// The log message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{LogLevel}: {Message}";
    }
}
