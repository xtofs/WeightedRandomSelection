static class CumulativeDistributionFunction
{
    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K Label, int Weight)> items) where K : notnull
    {
        double sum = items.Sum(p => p.Weight);
        var ranges = items.Scan(p => p.Weight / sum, 0.0, (a, b) => a + b, (item, a) => (UpperBound: a, item.Label));

        return new CumulativeDistributionFunction<K>(ranges);
    }

    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K Label, double Weight)> items) where K : notnull
    {
        double sum = items.Sum(p => p.Weight);
        var ranges = items.Scan(p => p.Weight / sum, 0.0, (a, b) => a + b, (item, a) => (UpperBound: a, item.Label));

        return new CumulativeDistributionFunction<K>(ranges);
    }

    public static CumulativeDistributionFunction<K> Create<K>(params (K, int)[] items) where K : notnull => Create(items.AsEnumerable());
}