
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
        //  If value is not found and value is less than one or more elements in array, 
        // the negative number returned is the bitwise complement of the index of the first 
        // element that is larger than value. 
        ix = ix < 0 ? ~ix : ix;
        return labels[ix];
    }
}
