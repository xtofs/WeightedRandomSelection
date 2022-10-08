static class CumulativeDistributionFunction
{
    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K, int)> items) where K : notnull
    {
        var density = new List<(double, K)>();
        double current = 0;
        double sum = items.Sum(kvp => kvp.Item2);
        foreach (var (k, v) in items)
        {
            current += ((double)v) / sum;
            density.Add((current, k));
        }
        return new CumulativeDistributionFunction<K>(density);
    }

    public static CumulativeDistributionFunction<K> Create<K>(IEnumerable<(K, double)> items) where K : notnull
    {
        var density = new List<(double, K)>();
        double current = 0;
        var sum = items.Sum(kvp => kvp.Item2);
        foreach (var (k, v) in items)
        {
            current += ((double)v) / sum;
            density.Add((current, k));
        }
        return new CumulativeDistributionFunction<K>(density);
    }

    public static CumulativeDistributionFunction<K> Create<K>(params (K, int)[] items) where K : notnull => Create(items.AsEnumerable());
}


