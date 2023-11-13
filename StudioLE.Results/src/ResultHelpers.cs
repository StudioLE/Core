namespace StudioLE.Results;

public static class ResultHelpers
{
    public static T? GetValueAsNullable<T>(this IResult<T> result) where T : class
    {
        return result is Success<T> success
            ? success.Value
            : null;
    }

    /// <summary>
    /// Validate the <paramref name="result"/>.
    /// Throw an exception if it is not a success.
    /// </summary>
    public static T GetValueOrThrow<T>(this IResult<T> result, string? contextMessage = null)
    {
        if (result is Success<T> success)
            return success;
        string message = contextMessage is null
            ? string.Empty
            : contextMessage + Environment.NewLine;
        if (result.Errors.Any())
            message += string.Join(Environment.NewLine, result.Errors) + Environment.NewLine;
        throw new(message);
    }

    /// <summary>
    /// Validate the <paramref name="result"/>.
    /// Throw an exception if it is not a success.
    /// </summary>
    public static void ThrowOnFailure(this IResult result, string? contextMessage = null)
    {
        if (result is Success)
            return;
        string message = contextMessage is null
            ? string.Empty
            : contextMessage + Environment.NewLine;
        if (result.Errors.Any())
            message += string.Join(Environment.NewLine, result.Errors) + Environment.NewLine;
        throw new(message);
    }
}