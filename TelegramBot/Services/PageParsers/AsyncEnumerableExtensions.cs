﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public static class AsyncEnumerableExtensions
    {
        public static async Task<List<T>> ToList<T>(this IAsyncEnumerable<T> items)
        {
            var enumerator = items.GetAsyncEnumerator();
            var result = new List<T>();
            while (await enumerator.MoveNextAsync())
                result.Add(enumerator.Current);
            await enumerator.DisposeAsync();
            return result;
        }
    }
}