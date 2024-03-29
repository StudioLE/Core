using StudioLE.Patterns;

namespace StudioLE.Conversion;

/// <inheritdoc />
public class StringToInteger : IConverter<string, int?>
{
    /// <inheritdoc />
    public int? Convert(string source)
    {
        return int.TryParse(source, out int result)
            ? result
            : null;
    }
}
