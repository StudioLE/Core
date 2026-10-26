using StudioLE.Patterns;

namespace StudioLE.Conversion;

/// <inheritdoc/>
public class StringToParseable<TStruct> : IConverter<string, TStruct?> where TStruct : struct, IParsable<TStruct>
{
    private readonly IFormatProvider? _formatProvider;

    /// <summary>
    /// Create an instance of <see cref="StringToParseable{TStruct}"/>.
    /// </summary>
    public StringToParseable(IFormatProvider? formatProvider)
    {
        _formatProvider = formatProvider;
    }

    /// <inheritdoc/>
    public TStruct? Convert(string source)
    {
        return TStruct.TryParse(source, _formatProvider, out TStruct result)
            ? result
            : null;
    }
}
