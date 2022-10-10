

class Counter<K> : IFormattable where K : notnull
{
    private readonly Dictionary<K, ulong> counts = new();

    public ulong this[K key]
    {
        get => counts.TryGetValue(key, out var val) ? val : 0;
        set => counts[key] = value;
    }

    public override string ToString() => ToString(null, null);

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return counts.Format(format, formatProvider);
    }
}
