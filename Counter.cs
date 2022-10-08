

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
        if (format != null && format.Equals("P"))
        {
            var sum = counts.Sum(kvp => (double)kvp.Value);
            return string.Join(", ", from kvp in counts select $"{kvp.Key}={((double)kvp.Value / sum).ToString("P", formatProvider)}");
        }
        else
        {
            return string.Join(", ", from kvp in counts select $"{kvp.Key}={kvp.Value}");
        }
    }
}

