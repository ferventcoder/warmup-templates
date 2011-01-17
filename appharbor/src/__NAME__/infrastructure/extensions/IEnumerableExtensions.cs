namespace __NAME__.infrastructure.extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
        {
            return items.Select(item => item);
        }

        public static void each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }
    }
}