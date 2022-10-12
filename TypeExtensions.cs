using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static class TypeExtensions
{
    public static bool TryGetGenericParameterType(this Type type, Type template, [MaybeNullWhen(false)] out Type arg0)
    {
        CheckTemplate(template, 1);
        if (type.IsGenericType)
        {
            var args = type.GetGenericArguments();
            if (args.Length == 1)
            {
                arg0 = args[0];
                return true;
            }
        }
        arg0 = default;
        return false;
    }

    public static bool TryGetGenericParameterType(this Type type, Type template, [MaybeNullWhen(false)] out Type arg0, [MaybeNullWhen(false)] out Type arg1)
    {
        CheckTemplate(template, 2);
        if (type.IsGenericType)
        {
            var args = type.GetGenericArguments();
            if (args.Length == 2)
            {
                arg0 = args[0];
                arg1 = args[1];
                return true;
            }
        }
        arg0 = default;
        arg1 = default;
        return false;
    }

    private static void CheckTemplate(Type template, int numGenericParameters)
    {
        if (!template.IsGenericTypeDefinition)
        {
            throw new ArgumentException("template type be a generic type definition");
        }
        var genericParams = template.GetGenericTypeDefinition().GetGenericArguments();
        if (genericParams.Length != numGenericParameters)
        {
            throw new ArgumentException($"template type must have {numGenericParameters} generic type parameters");
        }
    }

    public static bool ImplementsIEnumerable(this Type type, [MaybeNullWhen(false)] out Type itemType)
    {
        bool TryGetIEnumerableArg(Type @interface, [MaybeNullWhen(false)] out Type itemT)
        {
            return @interface.TryGetGenericParameterType(typeof(IEnumerable<>), out itemT);
        }

        return type.GetInterfaces().TryGetFirst<Type, Type>(TryGetIEnumerableArg, out itemType);
    }

    /// <summary>
    /// check that the type is a generic type derived from IEnumerable<T> where T is a 
    /// generic pair i.e. either a KeyValuePair, Tuple or ValueTuple
    /// </summary>
    /// <param name="type"></param>
    /// <param name="keyType"></param>
    /// <param name="valueType"></param>
    /// <returns></returns>
    public static bool TryGetEnumerableOfPairsElementTypes(this Type type, [MaybeNullWhen(false)] out Type keyType, [MaybeNullWhen(false)] out Type valueType)
    {
        if (type.ImplementsIEnumerable(out var pair))
        {
            if (pair.TryGetGenericParameterType(typeof(ValueTuple<,>), out keyType, out valueType))
            {
                return true;
            }
            if (pair.TryGetGenericParameterType(typeof(KeyValuePair<,>), out keyType, out valueType))
            {
                return true;
            }
            if (pair.TryGetGenericParameterType(typeof(Tuple<,>), out keyType, out valueType))
            {
                return true;
            }
        }

        keyType = default;
        valueType = default;
        return false;
    }
}


