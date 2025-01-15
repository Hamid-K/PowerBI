using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000206 RID: 518
	public static class ExtendedEnumerable
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00030354 File Offset: 0x0002E554
		[LinqTunnel]
		public static IEnumerable<T> DuplicateAndAppend<T>(this IEnumerable<T> sequence, T item)
		{
			foreach (T t in sequence)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield return item;
			yield break;
			yield break;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003036B File Offset: 0x0002E56B
		[LinqTunnel]
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> sequence, int partitionSize)
		{
			List<T> list = new List<T>(partitionSize);
			foreach (T t in sequence)
			{
				list.Add(t);
				if (list.Count == partitionSize)
				{
					yield return list;
					list = new List<T>(partitionSize);
				}
			}
			IEnumerator<T> enumerator = null;
			if (list.Any<T>())
			{
				yield return list;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00030382 File Offset: 0x0002E582
		[Pure]
		public static bool None<T>([InstantHandle] this IEnumerable<T> source, Func<T, bool> predicate)
		{
			return !source.Any(predicate);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003038E File Offset: 0x0002E58E
		[Pure]
		public static bool None<T>([InstantHandle] this IEnumerable<T> source)
		{
			return !source.Any<T>();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003039C File Offset: 0x0002E59C
		[LinqTunnel]
		public static IEnumerable<T> WhereOr<T>(this IEnumerable<T> input, IEnumerable<Func<T, bool>> predicates)
		{
			return input.Where((T inp) => ExtendedFunc.Or<T>(inp, predicates));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000303C8 File Offset: 0x0002E5C8
		[LinqTunnel]
		public static IEnumerable<T> WhereAnd<T>(this IEnumerable<T> input, IEnumerable<Func<T, bool>> predicates)
		{
			return input.Where((T inp) => ExtendedFunc.And<T>(inp, predicates));
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000303F4 File Offset: 0x0002E5F4
		[Pure]
		public static IEnumerable<T> Materialize<T>([InstantHandle] this IEnumerable<T> input)
		{
			if (input is Array)
			{
				return input;
			}
			if (input is List<T>)
			{
				return input;
			}
			return input.ToList<T>();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00030410 File Offset: 0x0002E610
		[LinqTunnel]
		public static IEnumerable<T> RandomizeOrder<T>(this IEnumerable<T> input)
		{
			return input.OrderBy((T i) => Guid.NewGuid());
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00030437 File Offset: 0x0002E637
		[Pure]
		public static bool Equivalent<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> other)
		{
			return source.Count == other.Count && source.All(new Func<KeyValuePair<TKey, TValue>, bool>(other.Contains));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0003045C File Offset: 0x0002E65C
		[Pure]
		public static bool Equivalent<T>([InstantHandle] this IEnumerable<T> source, [InstantHandle] IEnumerable<T> other)
		{
			if (!typeof(IComparable).IsAssignableFrom(typeof(T)))
			{
				return source.Count<T>() == other.Count<T>() && source.All(new Func<T, bool>(other.Contains<T>));
			}
			return source.OrderBy((T x) => x).SequenceEqual(other.OrderBy((T x) => x));
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000304F6 File Offset: 0x0002E6F6
		[Pure]
		public static bool Equivalent<T>([InstantHandle] this IEnumerable<T> source, [InstantHandle] IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			return source.Count<T>() == other.Count<T>() && source.Intersect(other, comparer).Count<T>() == source.Count<T>();
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0003051D File Offset: 0x0002E71D
		[Pure]
		public static bool ContainsAll<T>([InstantHandle] this IEnumerable<T> source, [InstantHandle] IEnumerable<T> items)
		{
			return items.All(new Func<T, bool>(source.Contains<T>));
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00030534 File Offset: 0x0002E734
		[Pure]
		public static int FirstPosition<TSource>([InstantHandle] this IEnumerable<TSource> source, [InstantHandle] Func<TSource, bool> predicate)
		{
			for (int i = 0; i < source.Count<TSource>(); i++)
			{
				if (predicate(source.ElementAt(i)))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00030564 File Offset: 0x0002E764
		[LinqTunnel]
		public static IEnumerable<T> Distinct<T, K>(this IEnumerable<T> source, Func<T, K> selector)
		{
			return source.Distinct(new GenericComparer<T, K>(selector));
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00030572 File Offset: 0x0002E772
		[LinqTunnel]
		public static IEnumerable<T> Distinct<T, K>(this IEnumerable<T> source, Func<T, K> selector, IEqualityComparer<K> keyComparer)
		{
			return source.Distinct(new GenericComparer<T, K>(selector, keyComparer));
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00030581 File Offset: 0x0002E781
		public static HashSet<T> ToHashSet<T>([InstantHandle] this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00030589 File Offset: 0x0002E789
		public static HashSet<T> ToHashSet<T>([InstantHandle] this IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(source, comparer);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00030594 File Offset: 0x0002E794
		[LinqTunnel]
		public static IEnumerable<T> GetFairShareBy<T, TResult>([NotNull] [InstantHandle] this IEnumerable<T> source, [NotNull] Func<T, TResult> selector, int capacity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<T>>(source, "source");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<T, TResult>>(selector, "selector");
			ExtendedDiagnostics.EnsureArgumentIsPositive(capacity, "capacity");
			if (source.Count<T>() <= capacity)
			{
				return source;
			}
			return (from x in source.GroupBy(selector).SelectMany((IGrouping<TResult, T> g) => g.Select((T x, int i) => new Pair<T, int>(x, i)))
				orderby x.Second
				select x.First).Take(capacity);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00030648 File Offset: 0x0002E848
		public static ReadOnlyCollection<T> AsReadOnlyCollection<T>([InstantHandle] this IEnumerable<T> items)
		{
			if (items != null)
			{
				ReadOnlyCollection<T> readOnlyCollection = items as ReadOnlyCollection<T>;
				if (readOnlyCollection != null)
				{
					return readOnlyCollection;
				}
				ICollection<T> collection = items as ICollection<T>;
				if (collection != null && collection.Count == 0)
				{
					return Array.AsReadOnly<T>(new T[0]);
				}
				IList<T> list = items as IList<T>;
				if (list != null)
				{
					return new ReadOnlyCollection<T>(list);
				}
				if (collection != null)
				{
					T[] array = new T[collection.Count];
					collection.CopyTo(array, 0);
					return Array.AsReadOnly<T>(array);
				}
				List<T> list2 = items.ToList<T>();
				if (list2.Count > 0)
				{
					return list2.AsReadOnly();
				}
			}
			return Array.AsReadOnly<T>(new T[0]);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000306D6 File Offset: 0x0002E8D6
		public static bool IsNullOrEmpty<T>([InstantHandle] this IEnumerable<T> source)
		{
			return source == null || !source.Any<T>();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000306E6 File Offset: 0x0002E8E6
		public static IEnumerable<T> EnsureNotNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000306F2 File Offset: 0x0002E8F2
		public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> key)
		{
			return source.OrderByDescending(key).FirstOrDefault<T>();
		}
	}
}
