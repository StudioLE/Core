namespace StudioLE.Extensions.System.IO;

/// <summary>
/// Methods to help with <see cref="Path"/>.
/// </summary>
public static class PathHelpers
{
    /// <summary>
    /// Replace invalid file name characters with <paramref name="replaceWith"/>.
    /// </summary>
    /// <remarks>
    /// <see href="https://stackoverflow.com/a/333268/247218">Source</see>
    /// </remarks>
    public static string ReplaceInvalidFileNameChars(string fileName, char replaceWith = '-')
    {
        return Path
            .GetInvalidFileNameChars()
            .Aggregate(fileName, (current, c) => current.Replace(c, replaceWith));
    }

    /// <summary>
    /// Get a unique temporary file path with the specified extension.
    /// </summary>
    /// <remarks>
    /// <see cref="Path.GetTempFileName()"/> is used to get a unique file name, as a side effect that file is created.
    /// </remarks>
    public static FileInfo GetTempFile(string extension)
    {
        return new(Path.GetTempFileName() + extension);
    }
}
