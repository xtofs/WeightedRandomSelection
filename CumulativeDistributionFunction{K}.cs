
class CumulativeDistributionFunction<K> where K : notnull
{
    public CumulativeDistributionFunction(List<(double, K)> density)
    {
        this.density = density;
    }

    private readonly List<(double, K)> density;

    public K Pick(double val)
    {
        var ix = density.BinarySearch((val, default!), Comparer);
        ix = ix < 0 ? ~ix : ix;
        return density[ix].Item2;
    }

    private static WeightComparer Comparer = new WeightComparer();

    class WeightComparer : IComparer<(double, K)>
    {
        public int Compare((double, K) x, (double, K) y)
        {
            return x.Item1.CompareTo(y.Item1);
        }
    }
}



