using System.Reflection;

namespace StudioLE.Serialization.Parsing;

/// <inheritdoc/>
public class Parser : IParser
{
    private readonly IReadOnlyDictionary<Type, Func<string, object?>> _directParsers;
    private readonly IReadOnlyDictionary<Type, Func<string, Type, object?>> _parentParsers;

    /// <summary>
    /// Creates a new instance of <see cref="Parser"/> with default parsers.
    /// </summary>
    public Parser()
    {
        _directParsers = DefaultDirectParsers();
        _parentParsers = DefaultParentParsers();
    }

    /// <summary>
    /// Creates a new instance of <see cref="Parser"/> with custom parsers.
    /// </summary>
    public Parser(
        IReadOnlyDictionary<Type, Func<string, object?>> directParsers,
        IReadOnlyDictionary<Type, Func<string, Type, object?>> parentParsers)
    {
        _directParsers = directParsers;
        _parentParsers = parentParsers;
    }

    /// <inheritdoc/>
    public bool CanParse(Type target)
    {
        return _directParsers.ContainsKey(target)
               || (target.BaseType is not null && _parentParsers.ContainsKey(target.BaseType))
               || GetStringConstructor(target) is not null;
    }

    /// <inheritdoc/>
    public object? Parse(string source, Type target)
    {
        if (_directParsers.ContainsKey(target))
            return _directParsers[target].Invoke(source);
        if (target.BaseType is not null && _parentParsers.ContainsKey(target.BaseType))
            return _parentParsers[target.BaseType].Invoke(source, target);
        ConstructorInfo? constructor = GetStringConstructor(target);
        if (constructor is not null)
            return constructor.Invoke([source]);
        return null;
    }

    private static ConstructorInfo? GetStringConstructor(Type target)
    {
        return target.GetConstructor([typeof(string)]);
    }

    private static IReadOnlyDictionary<Type, Func<string, object?>> DefaultDirectParsers()
    {
        return new Dictionary<Type, Func<string, object?>>
        {
            { typeof(byte), s => byte.TryParse(s, out byte b) ? b : null },
            { typeof(short), s => short.TryParse(s, out short sh) ? sh : null },
            { typeof(int), s => int.TryParse(s, out int i) ? i : null },
            { typeof(uint), s => uint.TryParse(s, out uint u) ? u : null },
            { typeof(double), s => double.TryParse(s, out double d) ? d : null },
            { typeof(float), s => float.TryParse(s, out float f) ? f : null },
            { typeof(decimal), s => decimal.TryParse(s, out decimal d) ? d : null },
            { typeof(long), s => long.TryParse(s, out long l) ? l : null },
            { typeof(bool), s => bool.TryParse(s, out bool b) ? b : null },
            { typeof(char), s => char.TryParse(s, out char c) ? c : null },
            { typeof(string), s => s },
            { typeof(DateTime), s => DateTime.TryParse(s, out DateTime d) ? d : null },
            { typeof(TimeSpan), s => TimeSpan.TryParse(s, out TimeSpan t) ? t : null }
        };
    }

    private static IReadOnlyDictionary<Type, Func<string, Type, object?>> DefaultParentParsers()
    {
        return new Dictionary<Type, Func<string, Type, object?>>
        {
            { typeof(Enum), (s, t) => Enum.TryParse(t, s, out object? e) ? e : null }
        };
    }
}
