
class CumulativeDistributionFunction<TKey> where TKey : notnull
{
    public CumulativeDistributionFunction(IEnumerable<(double, TKey)> ranges)
    {
        bounds = ranges.Select(p => p.Item1).ToArray();
        labels = ranges.Select(p => p.Item2).ToArray();
    }

    private double[] bounds;
    private TKey[] labels;

    public TKey Pick(double val)
    {
        var ix = Array.BinarySearch(bounds, val);
        ix = ix < 0 ? ~ix : ix;
        return labels[ix];
    }
}



