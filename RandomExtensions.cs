
public static class RandomExtensions
{
    public static T Choose<T>(this Random rng, WeightedCollection<T> distribution) where T : notnull
    {
        return distribution.Choose(rng);
    }

    public static IReadOnlyList<T> ChooseMany<T>(this Random rng, int n, WeightedCollection<T> distribution) where T : notnull
    {
        return distribution.Choose(n, rng).ToList();
    }
}