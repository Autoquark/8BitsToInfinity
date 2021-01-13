using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Extensions
{
    static class EnumerableExtensions
    {
        public static T MaxByOrDefault<T>(this IEnumerable<T> sequence, Func<T, float> selector, T defaultValue = default)
            => sequence.Any() ? sequence.MaxBy(selector) : defaultValue;

        public static T MaxBy<T>(this IEnumerable<T> sequence, Func<T, float> selector)
            => sequence.Aggregate((element: sequence.First(), value: selector(sequence.First())),
                (a, b) =>
                {
                    var bValue = selector(b);
                    return a.value > bValue ? a : (b, bValue);
                }).element;

        public static T MinByOrDefault<T>(this IEnumerable<T> sequence, Func<T, float> selector, T defaultValue = default)
            => sequence.Any() ? sequence.MinBy(selector) : defaultValue;

        public static T MinBy<T>(this IEnumerable<T> sequence, Func<T, float> selector)
            => sequence.Aggregate((element: sequence.First(), value: selector(sequence.First())),
                (a, b) =>
                {
                    var bValue = selector(b);
                    return a.value < bValue ? a : (b, bValue);
                }).element;
    }
}
