
public static class RandomExtensions
{
    public static T SelectFrom<T>(this Random rng, CumulativeDistributionFunction<T> distribution) where T : notnull
    {
        return distribution.Pick(rng.NextDouble());
    }
}