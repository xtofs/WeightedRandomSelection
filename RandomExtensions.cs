
public static class RandomExtensions
{
    public static T Choose<T>(this Random rng, CumulativeDistributionFunction<T> distribution) where T : notnull
    {
        return distribution.Choose(rng.NextDouble());
    }

    public static IReadOnlyList<T> ChooseMany<T>(this Random rng, int n, CumulativeDistributionFunction<T> distribution) where T : notnull
    {
        return distribution.ChooseN(n, rng);
    }
}