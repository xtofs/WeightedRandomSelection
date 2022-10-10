static class EnumerableExtensions
{

    // 
    /// <summary>
    ///  Produces an Enumerable containing cumulative results of applying the operator going left to right, including the initial value.
    /// </summary>
    /// <typeparam name="S">source item type</typeparam>
    /// <typeparam name="A">accumulator type</typeparam>
    /// <param name="source">source items</param>
    /// <param name="init">initial value of accumulator</param>
    /// <param name="agg">function combining current accumulator value with item</param>
    /// <returns></returns>
    public static IEnumerable<A> Scan<S, A>(this IEnumerable<S> source, A init, Func<A, S, A> agg)
    {
        var accu = init;
        foreach (var item in source)
        {
            accu = agg(accu, item);
            yield return accu;
        }
    }

    /// <summary>
    ///  Produces an Enumerable containing cumulative results of applying the operator going left to right, including the initial value.
    /// </summary>
    /// <typeparam name="S">source item type</typeparam>
    /// <typeparam name="T">result item type</typeparam>
    /// <typeparam name="A">accumulator type</typeparam>
    /// <param name="source">source items</param>
    /// <param name="selector">function selecting the value to be accumulated from an item</param>
    /// <param name="init">initial value of accumulator</param>
    /// <param name="agg">function combining selected item value and accumulator into new accumulator value</param>
    /// <param name="constructor">function constring output value from item and accumulated value</param>
    /// <returns></returns>
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


    public static string Format<K>(this IEnumerable<KeyValuePair<K, ulong>> items, string? format, IFormatProvider? formatProvider = null)
    {
        if (format != null && (format.StartsWith("P")))
        {
            var sum = items.Sum(kvp => (double)kvp.Value);
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={((double)kvp.Value / sum).ToString(format, formatProvider)}");
        }
        else
        {
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={kvp.Value}");
        }
    }

    public static string Format<K>(this IEnumerable<KeyValuePair<K, int>> items, string? format, IFormatProvider? formatProvider = null)
    {
        if (format != null && format.Equals("P"))
        {
            var sum = items.Sum(kvp => (double)kvp.Value);
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={((double)kvp.Value / sum).ToString(format, formatProvider)}");
        }
        else
        {
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={kvp.Value}");
        }
    }


    public static string Format<K>(this IEnumerable<(K Key, int Value)> items, string? format, IFormatProvider? formatProvider = null)
    {
        if (format != null && format.Equals(format))
        {
            var sum = items.Sum(kvp => (double)kvp.Value);
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={((double)kvp.Value / sum).ToString(format, formatProvider)}");
        }
        else
        {
            return string.Join(", ", from kvp in items orderby kvp.Key select $"{kvp.Key}={kvp.Value}");
        }
    }
}




