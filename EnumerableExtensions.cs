static class EnumerableExtensions
{

    public static IEnumerable<T> Scan<S, T, A>(this IEnumerable<S> source, Func<S, A> selector, A init, Func<A, A, A> agg, Func<S, A, T> constructor)
    {
        var accu = init;
        foreach (var item in source)
        {
            var v = selector(item);
            accu = agg(accu, selector(item));
            yield return constructor(item, accu);
        }
    }
}

