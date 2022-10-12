
public class WeightedCollection<TKey> where TKey : notnull
{

    public WeightedCollection(IReadOnlyDictionary<TKey, int> items)
    {
        double sum = items.Sum(tuple => tuple.Value);
        Items = items
            .Select(tuple => (tuple.Key, tuple.Value / sum))
            .ToArray();
    }

    public WeightedCollection(IEnumerable<(TKey Item, int Weight)> items)
    {
        // normalize
        double sum = items.Sum(tuple => tuple.Item2);
        Items = items.Select(tuple => (tuple.Item, tuple.Weight / sum)).ToArray();
    }

    public (TKey Item, double Weight)[] Items { get; }

    public TKey Choose(Random rng)
    {
        return Items
            .Select(i => (i.Item, Weight: Math.Pow(rng.NextDouble(), 1.0 / i.Weight)))
            .MaxBy(p => p.Weight)
            .Item;
    }

    public IEnumerable<TKey> Choose(int n, Random rng)
    {
        // http://utopia.duth.gr/~pefraimi/research/data/2007EncOfAlg.pdf
        // Input : A population V of n weighted items
        // Output : A WRS of size m
        // 1: For each vi ∈ V , ui = random(0, 1) and ki = u_i ^ (1/wi)
        // 2: Select the m items with the largest keys ki as a WR
        if (n == 1)
        {
            return Single(Choose(rng));
        }
        return Items
            .Select(i => (i.Item, Weight: Math.Pow(rng.NextDouble(), i.Weight)))
            .OrderBy(p => p.Weight)
            .Select(p => p.Item)
            .Take(n);
        // TODO: ordering all an only taking the top n seems wasteful
    }

    private IEnumerable<TKey> Single(TKey key)
    {
        yield return key;
    }
}
