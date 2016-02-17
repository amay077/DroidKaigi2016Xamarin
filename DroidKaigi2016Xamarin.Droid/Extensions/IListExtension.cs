using System;
using System.Collections.Generic;

namespace DroidKaigi2016Xamarin.Droid.Extensions
{
    public static class IListExtension
    {
        public static void AddAll<T>(this IList<T> self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                self.Add(item);
            }
        }
    }
}

