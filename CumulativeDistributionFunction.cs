static class CumulativeDistributionFunction
{
    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K Label, int Weight)> items) where K : notnull
    {
        double sum = items.Sum(p => p.Weight);
        var ranges = items.Scan(p => p.Weight / sum, 0.0, (a, b) => a + b, (item, a) => (UpperBound: a, item.Label));

        return new CumulativeDistributionFunction<K>(ranges);
    }

    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<KeyValuePair<K, int>> weights) where K : notnull
    {
        double sum = weights.Sum(p => p.Value);
        var ranges = weights.Scan(p => p.Value / sum, 0.0, (a, b) => a + b, (item, a) => (UpperBound: a, item.Key));

        return new CumulativeDistributionFunction<K>(ranges);
    }

    public static CumulativeDistributionFunction<K> Create<K>(params (K, int)[] items) where K : notnull =>
        Create(items.AsEnumerable());

    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K Label, double Weight)> items) where K : notnull
    {
        double sum = items.Sum(p => p.Weight);
        var ranges = items.Scan(p => p.Weight / sum, 0.0, (a, b) => a + b, (item, a) => (UpperBound: a, item.Label));

        return new CumulativeDistributionFunction<K>(ranges);
    }

    public static CumulativeDistributionFunction<K> Create<K>(params (K, double)[] items) where K : notnull =>
        Create(items.AsEnumerable());
}