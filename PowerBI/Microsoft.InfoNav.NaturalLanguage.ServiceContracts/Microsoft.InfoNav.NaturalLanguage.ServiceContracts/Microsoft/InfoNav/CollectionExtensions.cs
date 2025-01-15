using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.InfoNav
{
	// Token: 0x0200000D RID: 13
	public static class CollectionExtensions
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002563 File Offset: 0x00000763
		[DebuggerStepThrough]
		public static IReadOnlyList<T> EmptyIfNull<T>(this IReadOnlyList<T> source)
		{
			return source ?? ImmutableList<T>.Empty;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000256F File Offset: 0x0000076F
		[DebuggerStepThrough]
		public static IReadOnlyDictionary<TKey, TValue> EmptyIfNull<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source)
		{
			return source ?? ImmutableDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
		[DebuggerStepThrough]
		public static IImmutableSet<T> AsImmutableSet<T>(this IEnumerable<T> source)
		{
			IImmutableSet<T> immutableSet = source as IImmutableSet<T>;
			if (immutableSet != null)
			{
				return immutableSet;
			}
			return ((source != null) ? source.ToImmutableHashSet<T>() : null) ?? ImmutableHashSet<T>.Empty;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025AA File Offset: 0x000007AA
		public static void Add<T>(this List<T> list, IEnumerable<T> items)
		{
			if (items != null)
			{
				list.AddRange(items);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025B8 File Offset: 0x000007B8
		public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			if (items != null)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002610 File Offset: 0x00000810
		public static void ReplaceWith<T>(this List<T> list, IEnumerable<T> newItems)
		{
			list.Clear();
			if (newItems != null)
			{
				list.AddRange(newItems);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002624 File Offset: 0x00000824
		public static void Add<TKey, TValueElement>(this Dictionary<TKey, ImmutableArray<TValueElement>.Builder> dictionary, TKey key, TValueElement value)
		{
			ImmutableArray<TValueElement>.Builder builder;
			if (!dictionary.TryGetValue(key, out builder))
			{
				builder = ImmutableArray.CreateBuilder<TValueElement>();
				dictionary.Add(key, builder);
			}
			builder.Add(value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002651 File Offset: 0x00000851
		public static ImmutableDictionary<TKey, IReadOnlyList<TValueElement>> ToImmutableDictionaryWithReadOnlyValues<TKey, TValueElement>(this IEnumerable<KeyValuePair<TKey, ImmutableArray<TValueElement>.Builder>> items)
		{
			if (items == null)
			{
				return ImmutableDictionary<TKey, IReadOnlyList<TValueElement>>.Empty;
			}
			return items.Select((KeyValuePair<TKey, ImmutableArray<TValueElement>.Builder> p) => p.Key.WithValue(p.Value.ToImmutable())).ToImmutableDictionary<TKey, IReadOnlyList<TValueElement>>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002688 File Offset: 0x00000888
		[DebuggerStepThrough]
		public static bool Add<TKey, TValue>(this Dictionary<TKey, ImmutableHashSet<TValue>.Builder> dictionary, TKey key, TValue value, IEqualityComparer<TValue> valueComparer = null)
		{
			ImmutableHashSet<TValue>.Builder builder;
			if (!dictionary.TryGetValue(key, out builder))
			{
				builder = ImmutableHashSet.CreateBuilder<TValue>(valueComparer ?? EqualityComparer<TValue>.Default);
				dictionary.Add(key, builder);
			}
			return builder.Add(value);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026BF File Offset: 0x000008BF
		[DebuggerStepThrough]
		public static ImmutableDictionary<TKey, ImmutableHashSet<TValue>> ToImmutableDictionaryWithImmutableValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>.Builder>> items)
		{
			if (items == null)
			{
				return ImmutableDictionary<TKey, ImmutableHashSet<TValue>>.Empty;
			}
			return items.Select((KeyValuePair<TKey, ImmutableHashSet<TValue>.Builder> p) => p.Key.WithValue(p.Value.ToImmutable())).ToImmutableDictionary<TKey, ImmutableHashSet<TValue>>();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026F4 File Offset: 0x000008F4
		[DebuggerStepThrough]
		public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueCreator)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueCreator();
			dictionary.Add(key, tvalue);
			return tvalue;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002720 File Offset: 0x00000920
		[DebuggerStepThrough]
		public static bool TryGetSingleValue<TValue>(this IEnumerable<TValue> values, out TValue value)
		{
			IEnumerator<TValue> enumerator = values.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				value = default(TValue);
				return false;
			}
			value = enumerator.Current;
			return !enumerator.MoveNext();
		}
	}
}
