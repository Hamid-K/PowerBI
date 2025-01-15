using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200040C RID: 1036
	public static class CollectionUtils
	{
		// Token: 0x0600177F RID: 6015 RVA: 0x00047ABE File Offset: 0x00045CBE
		public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> source)
		{
			IEnumerator<T>[] enumerators = source.Select((IEnumerable<T> e) => e.GetEnumerator()).ToArray<IEnumerator<T>>();
			if (enumerators.Length == 0)
			{
				yield break;
			}
			try
			{
				for (;;)
				{
					if (!enumerators.All((IEnumerator<T> e) => e.MoveNext()))
					{
						break;
					}
					yield return enumerators.Select((IEnumerator<T> e) => e.Current).ToArray<T>();
				}
			}
			finally
			{
				IEnumerator<T>[] array = enumerators;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00047ACE File Offset: 0x00045CCE
		public static IEnumerable<IReadOnlyList<T>> SplitFixedLength<T>(this IEnumerable<T> source, int length)
		{
			List<T> list = null;
			foreach (T t in source)
			{
				if (list == null)
				{
					list = new List<T>(length);
				}
				list.Add(t);
				if (list.Count == length)
				{
					yield return list;
					list = null;
				}
			}
			IEnumerator<T> enumerator = null;
			if (list != null)
			{
				yield return list;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public static TElement ArgMax<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func, out TComparable maxValue) where TComparable : IComparable<TComparable>
		{
			bool flag = false;
			maxValue = default(TComparable);
			TElement telement = default(TElement);
			foreach (TElement telement2 in xs)
			{
				TComparable tcomparable = func(telement2);
				if (!flag || tcomparable.CompareTo(maxValue) > 0)
				{
					maxValue = tcomparable;
					telement = telement2;
					flag = true;
				}
			}
			return telement;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00047B6C File Offset: 0x00045D6C
		public static TElement ArgMax<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func) where TComparable : IComparable<TComparable>
		{
			TComparable tcomparable;
			return xs.ArgMax(func, out tcomparable);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00047B84 File Offset: 0x00045D84
		public static Optional<TElement> MaybeArgMax<TElement, TComparable>(this IEnumerable<TElement> seq, Func<TElement, TComparable> keySelector) where TComparable : IComparable<TComparable>
		{
			bool flag = false;
			TComparable tcomparable = default(TComparable);
			TElement telement = default(TElement);
			bool flag2 = false;
			foreach (TElement telement2 in seq)
			{
				flag2 = true;
				TComparable tcomparable2 = keySelector(telement2);
				if (!flag || tcomparable2.CompareTo(tcomparable) > 0)
				{
					tcomparable = tcomparable2;
					telement = telement2;
					flag = true;
				}
			}
			if (!flag2)
			{
				return Optional<TElement>.Nothing;
			}
			return telement.Some<TElement>();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00047C18 File Offset: 0x00045E18
		public static TElement ArgMaxBy<TElement, TResult, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TResult> func, Func<TResult, TComparable> by, out TResult maxResult) where TComparable : IComparable<TComparable>
		{
			bool flag = false;
			maxResult = default(TResult);
			TComparable tcomparable = default(TComparable);
			TElement telement = default(TElement);
			foreach (TElement telement2 in xs)
			{
				TResult tresult = func(telement2);
				TComparable tcomparable2 = by(tresult);
				if (!flag || tcomparable2.CompareTo(tcomparable) > 0)
				{
					maxResult = tresult;
					tcomparable = tcomparable2;
					telement = telement2;
					flag = true;
				}
			}
			return telement;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00047CAC File Offset: 0x00045EAC
		public static TElement ArgMin<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func, out TComparable minValue) where TComparable : IComparable<TComparable>
		{
			bool flag = false;
			minValue = default(TComparable);
			TElement telement = default(TElement);
			foreach (TElement telement2 in xs)
			{
				TComparable tcomparable = func(telement2);
				if (!flag || tcomparable.CompareTo(minValue) < 0)
				{
					minValue = tcomparable;
					telement = telement2;
					flag = true;
				}
			}
			return telement;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00047D30 File Offset: 0x00045F30
		public static TElement ArgMin<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func) where TComparable : IComparable<TComparable>
		{
			TComparable tcomparable;
			return xs.ArgMin(func, out tcomparable);
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00047D48 File Offset: 0x00045F48
		public static Optional<TElement> MaybeArgMin<TElement, TComparable>(this IEnumerable<TElement> seq, Func<TElement, TComparable> keySelector) where TComparable : IComparable<TComparable>
		{
			bool flag = false;
			TComparable tcomparable = default(TComparable);
			TElement telement = default(TElement);
			bool flag2 = false;
			foreach (TElement telement2 in seq)
			{
				flag2 = true;
				TComparable tcomparable2 = keySelector(telement2);
				if (!flag || tcomparable2.CompareTo(tcomparable) < 0)
				{
					tcomparable = tcomparable2;
					telement = telement2;
					flag = true;
				}
			}
			if (!flag2)
			{
				return Optional<TElement>.Nothing;
			}
			return telement.Some<TElement>();
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00047DDC File Offset: 0x00045FDC
		public static IEnumerable<TElement> ArgMinMultiple<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func) where TComparable : IComparable<TComparable>
		{
			return xs.ArgBestMultiple(func, (TComparable v1, TComparable v2) => v1.CompareTo(v2) < 0);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00047E04 File Offset: 0x00046004
		public static IEnumerable<TElement> ArgMaxMultiple<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func) where TComparable : IComparable<TComparable>
		{
			return xs.ArgBestMultiple(func, (TComparable v1, TComparable v2) => v1.CompareTo(v2) > 0);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00047E2C File Offset: 0x0004602C
		private static IEnumerable<TElement> ArgBestMultiple<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> func, Func<TComparable, TComparable, bool> isBetterThan) where TComparable : IComparable<TComparable>
		{
			List<TElement> list = new List<TElement>();
			bool flag = false;
			TComparable tcomparable = default(TComparable);
			foreach (TElement telement in xs)
			{
				TComparable tcomparable2 = func(telement);
				if (!flag || isBetterThan(tcomparable2, tcomparable))
				{
					tcomparable = tcomparable2;
					flag = true;
					list.Clear();
					list.Add(telement);
				}
				else if (flag && tcomparable2.CompareTo(tcomparable) == 0)
				{
					list.Add(telement);
				}
			}
			return list;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00047EC8 File Offset: 0x000460C8
		public static int MaxIndex<TElement, TComparable>(this IEnumerable<TElement> xs, Func<TElement, TComparable> keyFunc) where TComparable : IComparable<TComparable>
		{
			TComparable tcomparable = default(TComparable);
			int num = -1;
			int num2 = -1;
			foreach (TElement telement in xs)
			{
				num++;
				TComparable tcomparable2 = keyFunc(telement);
				if (num2 < 0 || tcomparable2.CompareTo(tcomparable) > 0)
				{
					num2 = num;
					tcomparable = tcomparable2;
				}
			}
			return num2;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00047F40 File Offset: 0x00046140
		public static Optional<Record<T, T>> MaybeExtrema<T>(this IEnumerable<T> xs, IComparer<T> comparer = null)
		{
			comparer = comparer ?? Comparer<T>.Default;
			T min = default(T);
			T max = default(T);
			bool flag = false;
			foreach (T t in xs)
			{
				if (!flag)
				{
					min = t;
					max = t;
					flag = true;
				}
				else
				{
					if (comparer.Compare(t, min) < 0)
					{
						min = t;
					}
					if (comparer.Compare(t, max) > 0)
					{
						max = t;
					}
				}
			}
			return flag.Then(() => Record.Create<T, T>(min, max));
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00048004 File Offset: 0x00046204
		public static Record<T, T> Extrema<T>(this IEnumerable<T> xs, IComparer<T> comparer = null)
		{
			return xs.MaybeExtrema(comparer).Value;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00048020 File Offset: 0x00046220
		public static bool ExtremaWithin(this IEnumerable<double> xs, double tolerance)
		{
			double num;
			double num2;
			xs.Extrema(null).Deconstruct(out num, out num2);
			double num3 = num;
			double num4 = num2;
			return MathUtils.WithinTolerance(num3, num4, tolerance);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00048048 File Offset: 0x00046248
		public static bool ExtremaWithin(this IEnumerable<int> xs, int tolerance)
		{
			int num;
			int num2;
			xs.Extrema(null).Deconstruct(out num, out num2);
			int num3 = num;
			int num4 = num2;
			return MathUtils.WithinTolerance(num3, num4, tolerance);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00048070 File Offset: 0x00046270
		public static bool ExtremaWithinOrElse(this IEnumerable<double> xs, double tolerance, Func<bool> elseFunc)
		{
			return xs.MaybeExtrema(null).Select(delegate(Record<double, double> extrema)
			{
				double num;
				double num2;
				extrema.Deconstruct(out num, out num2);
				double num3 = num;
				double num4 = num2;
				return MathUtils.WithinTolerance(num3, num4, tolerance);
			}).OrElseCompute(elseFunc);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x000480A8 File Offset: 0x000462A8
		public static bool ExtremaWithinOrElse(this IEnumerable<int> xs, int tolerance, Func<bool> elseFunc)
		{
			return xs.MaybeExtrema(null).Select(delegate(Record<int, int> extrema)
			{
				int num;
				int num2;
				extrema.Deconstruct(out num, out num2);
				int num3 = num;
				int num4 = num2;
				return MathUtils.WithinTolerance(num3, num4, tolerance);
			}).OrElseCompute(elseFunc);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x000480E0 File Offset: 0x000462E0
		public static void AddOrCreate<TKey, TValue, TCollection>(this IDictionary<TKey, TCollection> dict, TKey key, TValue value) where TCollection : ICollection<TValue>, new()
		{
			TCollection tcollection;
			if (dict.TryGetValue(key, out tcollection))
			{
				tcollection.Add(value);
				return;
			}
			TCollection tcollection2 = new TCollection();
			tcollection2.Add(value);
			dict[key] = tcollection2;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00048124 File Offset: 0x00046324
		public static bool AddOrCreate<TKey, TValue, TInternalKey>(this IDictionary<TKey, Dictionary<TInternalKey, TValue>> dict, TKey key, TInternalKey internalKey, TValue value, IEqualityComparer<TInternalKey> comparer = null, Func<TValue, bool> shouldUpdateFunc = null)
		{
			Dictionary<TInternalKey, TValue> dictionary;
			if (dict.TryGetValue(key, out dictionary))
			{
				TValue tvalue;
				if (shouldUpdateFunc != null && dictionary.TryGetValue(internalKey, out tvalue) && !shouldUpdateFunc(tvalue))
				{
					return false;
				}
				dictionary[internalKey] = value;
			}
			else if (comparer == null)
			{
				Dictionary<TInternalKey, TValue> dictionary2 = new Dictionary<TInternalKey, TValue>();
				dictionary2[internalKey] = value;
				dict[key] = dictionary2;
			}
			else
			{
				Dictionary<TInternalKey, TValue> dictionary3 = new Dictionary<TInternalKey, TValue>(comparer);
				dictionary3[internalKey] = value;
				dict[key] = dictionary3;
			}
			return true;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00048198 File Offset: 0x00046398
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueFunc)
		{
			TValue tvalue;
			if (dict.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			TValue tvalue2 = valueFunc(key);
			dict[key] = tvalue2;
			return tvalue2;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x000481C4 File Offset: 0x000463C4
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue newValue)
		{
			TValue tvalue;
			if (dict.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			dict[key] = newValue;
			return newValue;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x000481E7 File Offset: 0x000463E7
		public static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : new()
		{
			return dict.GetOrAdd(key, (TKey _) => new TValue());
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00048210 File Offset: 0x00046410
		public static IReadOnlyCollection<TElement> GetOrEmpty<TKey, TElement>(this IReadOnlyDictionary<TKey, IReadOnlyCollection<TElement>> dict, TKey key)
		{
			IReadOnlyCollection<TElement> readOnlyCollection;
			if (!dict.TryGetValue(key, out readOnlyCollection))
			{
				return new TElement[0];
			}
			return readOnlyCollection;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00048234 File Offset: 0x00046434
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def = default(TValue))
		{
			TValue tvalue;
			if (!dict.TryGetValue(key, out tvalue))
			{
				return def;
			}
			return tvalue;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00048250 File Offset: 0x00046450
		public static IEnumerable<IImmutableDictionary<TKey, TElement>> CartesianProduct<TKey, TCollection, TElement>(this IDictionary<TKey, TCollection> dict) where TCollection : IEnumerable<TElement>
		{
			IEnumerable<IImmutableDictionary<TKey, TElement>> enumerable = Seq.Of<ImmutableDictionary<TKey, TElement>>(new ImmutableDictionary<TKey, TElement>[] { ImmutableDictionary.Create<TKey, TElement>() });
			using (IEnumerator<KeyValuePair<TKey, TCollection>> enumerator = dict.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<TKey, TCollection> pair = enumerator.Current;
					enumerable = from accdict in enumerable
						from item in pair.Value
						select accdict.Add(pair.Key, item);
				}
			}
			return enumerable;
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000482D0 File Offset: 0x000464D0
		public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
		{
			IEnumerable<IEnumerable<T>> enumerable = Seq.Of<IEnumerable<T>>(new IEnumerable<T>[] { Enumerable.Empty<T>() });
			using (IEnumerator<IEnumerable<T>> enumerator = sequences.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEnumerable<T> sequence = enumerator.Current;
					enumerable = from accseq in enumerable
						from item in sequence
						select accseq.AppendItem(item);
				}
			}
			return enumerable;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00048364 File Offset: 0x00046564
		public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(params IEnumerable<T>[] sequences)
		{
			return sequences.CartesianProduct<T>();
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004836C File Offset: 0x0004656C
		public static IEnumerable<Record<T1, T2>> CartesianProduct<T1, T2>(this IEnumerable<T1> xs, IEnumerable<T2> ys)
		{
			return xs.SelectMany((T1 x) => ys.Select((T2 y) => Record.Create<T1, T2>(x, y)));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00048398 File Offset: 0x00046598
		public static IEnumerable<Record<T1, T2>> OrderedPairs<T1, T2>(this IEnumerable<T1> seq1, IEnumerable<T2> seq2)
		{
			ICollection<T2> seq2Collection = (seq2 as ICollection<T2>) ?? seq2.ToList<T2>();
			foreach (T1 t in seq1)
			{
				foreach (T2 t2 in seq2Collection)
				{
					yield return Record.Create<T1, T2>(t, t2);
				}
				IEnumerator<T2> enumerator2 = null;
				t = default(T1);
			}
			IEnumerator<T1> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000483AF File Offset: 0x000465AF
		public static IEnumerable<Record<T, T>> UnorderedPairs<T>(this IEnumerable<T> sequence, bool reflexive)
		{
			IReadOnlyList<T> materializedSequence = (sequence as IReadOnlyList<T>) ?? sequence.ToList<T>();
			int num;
			for (int i = 0; i < materializedSequence.Count; i = num + 1)
			{
				for (int j = i + ((!reflexive) ? 1 : 0); j < materializedSequence.Count; j = num + 1)
				{
					yield return Record.Create<T, T>(materializedSequence[i], materializedSequence[j]);
					num = j;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000483C8 File Offset: 0x000465C8
		public static IEnumerable<IEnumerable<T>> OrderedCartesianProduct<T, TKey>(this IEnumerable<IEnumerable<T>> sequences, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
		{
			IEnumerable<IEnumerable<T>> enumerable = Seq.Of<IEnumerable<T>>(new IEnumerable<T>[] { Enumerable.Empty<T>() });
			using (IEnumerator<IEnumerable<T>> enumerator = sequences.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEnumerable<T> sequence = enumerator.Current;
					enumerable = from accseq in enumerable
						from item in sequence.OrderBy(keySelector)
						select accseq.AppendItem(item);
				}
			}
			return enumerable;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00048470 File Offset: 0x00046670
		public static IEnumerable<IEnumerable<T>> OrderedCartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences) where T : IComparable<T>
		{
			return sequences.OrderedCartesianProduct((T x) => x);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00048497 File Offset: 0x00046697
		public static IEnumerable<Record<T1, T2>> ZipWith<T1, T2>(this IEnumerable<T1> seq, IEnumerable<T2> other)
		{
			Func<T1, T2, Record<T1, T2>> func;
			if ((func = CollectionUtils.<ZipWith>O__34_0<T1, T2>.<0>__Create) == null)
			{
				func = (CollectionUtils.<ZipWith>O__34_0<T1, T2>.<0>__Create = new Func<T1, T2, Record<T1, T2>>(Record.Create<T1, T2>));
			}
			return seq.Zip(other, func);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000484BC File Offset: 0x000466BC
		public static IEnumerable<TResult> Zip<T1, T2, T3, TResult>(this IEnumerable<T1> seq, IEnumerable<T2> other, IEnumerable<T3> other2, Func<T1, T2, T3, TResult> func)
		{
			return seq.ZipWith(other).Zip(other2, (Record<T1, T2> t12, T3 t3) => func(t12.Item1, t12.Item2, t3));
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000484EF File Offset: 0x000466EF
		public static IEnumerable<Record<T1, T2, T3>> ZipWith<T1, T2, T3>(this IEnumerable<T1> seq, IEnumerable<T2> other, IEnumerable<T3> other2)
		{
			Func<T1, T2, T3, Record<T1, T2, T3>> func;
			if ((func = CollectionUtils.<ZipWith>O__36_0<T1, T2, T3>.<0>__Create) == null)
			{
				func = (CollectionUtils.<ZipWith>O__36_0<T1, T2, T3>.<0>__Create = new Func<T1, T2, T3, Record<T1, T2, T3>>(Record.Create<T1, T2, T3>));
			}
			return seq.Zip(other, other2, func);
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00048514 File Offset: 0x00046714
		public static IEnumerable<Record<int, T>> Enumerate<T>(this IEnumerable<T> seq)
		{
			int i = 0;
			foreach (T t in seq)
			{
				int num = i;
				i = num + 1;
				yield return Record.Create<int, T>(num, t);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00048524 File Offset: 0x00046724
		public static IEnumerable<int> Indices<T>(this IReadOnlyCollection<T> seq)
		{
			return Enumerable.Range(0, seq.Count);
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00048532 File Offset: 0x00046732
		[NullableContext(1)]
		public static IEnumerable<T> Collect<T>([Nullable(new byte[] { 1, 2 })] this IEnumerable<T> seq) where T : class
		{
			return seq.Where((T x) => x != null);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00048559 File Offset: 0x00046759
		[NullableContext(1)]
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		public static IEnumerable<Record<T1, T2>> Collect2<T1, T2>([Nullable(new byte[] { 1, 0, 2, 2 })] this IEnumerable<Record<T1, T2>> seq) where T1 : class where T2 : class
		{
			return seq.Where2((T1 x, T2 y) => x != null && y != null);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00048580 File Offset: 0x00046780
		public static IEnumerable<T> Collect<T>(this IEnumerable<T?> seq) where T : struct
		{
			return from x in seq
				where x != null
				select x.Value;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x000485D6 File Offset: 0x000467D6
		[NullableContext(1)]
		public static IEnumerable<TResult> Collect<[Nullable(2)] T, TResult>(this IEnumerable<T> seq, [Nullable(new byte[] { 1, 1, 2 })] Func<T, TResult> func) where TResult : class
		{
			return from x in seq.Select(func)
				where x != null
				select x;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00048604 File Offset: 0x00046804
		public static IEnumerable<TResult> Collect<T, TResult>(this IEnumerable<T> seq, Func<T, TResult?> func) where TResult : struct
		{
			return from x in seq.Select(func)
				where x != null
				select x.Value;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00048660 File Offset: 0x00046860
		[NullableContext(1)]
		public static IEnumerable<TResult> Collect<[Nullable(2)] T, TResult>(this IEnumerable<T> seq, [Nullable(new byte[] { 1, 1, 2 })] Func<T, int, TResult> func) where TResult : class
		{
			return from x in seq.Select(func)
				where x != null
				select x;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00048690 File Offset: 0x00046890
		public static IEnumerable<TResult> Collect<T, TResult>(this IEnumerable<T> seq, Func<T, int, TResult?> func) where TResult : struct
		{
			return from x in seq.Select(func)
				where x != null
				select x.Value;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000486EC File Offset: 0x000468EC
		public static IEnumerable<T> AppendItem<T>(this IEnumerable<T> seq, T item)
		{
			return seq.Concat(Seq.Of<T>(new T[] { item }));
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00048707 File Offset: 0x00046907
		public static IEnumerable<T> PrependItem<T>(this IEnumerable<T> seq, T item)
		{
			return Seq.Of<T>(new T[] { item }).Concat(seq);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00048724 File Offset: 0x00046924
		public static IEnumerable<T> ExtendToLength<T>(this IEnumerable<T> seq, int length, T defaultValue = default(T))
		{
			CollectionUtils.<>c__DisplayClass48_0<T> CS$<>8__locals1 = new CollectionUtils.<>c__DisplayClass48_0<T>();
			CS$<>8__locals1.seq = seq;
			CS$<>8__locals1.defaultValue = defaultValue;
			CS$<>8__locals1.length = length;
			if (CS$<>8__locals1.seq == null)
			{
				return Enumerable.Repeat<T>(CS$<>8__locals1.defaultValue, CS$<>8__locals1.length);
			}
			ICollection<T> collection = CS$<>8__locals1.seq as ICollection<T>;
			if (collection != null && collection.Count >= CS$<>8__locals1.length)
			{
				return CS$<>8__locals1.seq;
			}
			return CS$<>8__locals1.<ExtendToLength>g__ExtendToLengthNonNull|0();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00048790 File Offset: 0x00046990
		public static IEnumerable<T> ExtendToLength<T>(this IEnumerable<T> seq, int length, Func<int, T> defaultValueFunc)
		{
			CollectionUtils.<>c__DisplayClass49_0<T> CS$<>8__locals1 = new CollectionUtils.<>c__DisplayClass49_0<T>();
			CS$<>8__locals1.seq = seq;
			CS$<>8__locals1.defaultValueFunc = defaultValueFunc;
			CS$<>8__locals1.length = length;
			if (CS$<>8__locals1.seq == null)
			{
				return Enumerable.Range(0, CS$<>8__locals1.length).Select(CS$<>8__locals1.defaultValueFunc);
			}
			ICollection<T> collection = CS$<>8__locals1.seq as ICollection<T>;
			if (collection != null && collection.Count >= CS$<>8__locals1.length)
			{
				return CS$<>8__locals1.seq;
			}
			return CS$<>8__locals1.<ExtendToLength>g__ExtendToLengthNonNull|0();
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00048802 File Offset: 0x00046A02
		public static HashSet<T> ConvertToHashSet<T>(this IEnumerable<T> seq)
		{
			return new HashSet<T>(seq);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0004880A File Offset: 0x00046A0A
		public static HashSet<T> ConvertToHashSet<T>(this IEnumerable<T> seq, IEqualityComparer<T> comparer)
		{
			return new HashSet<T>(seq, comparer);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00048813 File Offset: 0x00046A13
		public static bool HasAtLeast<T>(this IEnumerable<T> source, int n)
		{
			return source.Skip(n - 1).Any<T>();
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00048823 File Offset: 0x00046A23
		public static bool HasAtMost<T>(this IEnumerable<T> source, int n)
		{
			return !source.Skip(n).Any<T>();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00048834 File Offset: 0x00046A34
		public static IEnumerable<T> TakeEvery<T>(this IEnumerable<T> source, int step)
		{
			return source.Where((T _, int i) => i % step == 0);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00048860 File Offset: 0x00046A60
		public static IEnumerable<T> TakeExceptEvery<T>(this IEnumerable<T> source, int step)
		{
			return source.Where((T _, int i) => i % step != 0);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004888C File Offset: 0x00046A8C
		public static IEnumerable<T> DropLast<T>(this IEnumerable<T> source)
		{
			T t = default(T);
			bool flag = false;
			foreach (T x in source)
			{
				if (flag)
				{
					yield return t;
				}
				t = x;
				flag = true;
				x = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0004889C File Offset: 0x00046A9C
		public static IEnumerable<T> DropLast<T>(this IEnumerable<T> source, int count)
		{
			CollectionUtils.<>c__DisplayClass57_0<T> CS$<>8__locals1 = new CollectionUtils.<>c__DisplayClass57_0<T>();
			CS$<>8__locals1.count = count;
			CS$<>8__locals1.source = source;
			if (CS$<>8__locals1.count == 0)
			{
				return CS$<>8__locals1.source;
			}
			ICollection<T> collection = CS$<>8__locals1.source as ICollection<T>;
			if (collection != null)
			{
				return CS$<>8__locals1.source.Take(collection.Count - CS$<>8__locals1.count);
			}
			if (CS$<>8__locals1.count == 1)
			{
				return CS$<>8__locals1.source.DropLast<T>();
			}
			return CS$<>8__locals1.<DropLast>g__Generator|0();
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0004890F File Offset: 0x00046B0F
		public static IEnumerable<T> TakeKDistinctOn<T, TKey>(this IEnumerable<T> seq, Func<T, TKey> keySelector, int k, IEqualityComparer<TKey> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			using (IEnumerator<T> enumerator = seq.GetEnumerator())
			{
				if (k <= 0 || !enumerator.MoveNext())
				{
					yield break;
				}
				yield return enumerator.Current;
				int distinctScores = 1;
				TKey lastScore = keySelector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					TKey tkey = keySelector(t);
					if (!comparer.Equals(tkey, lastScore))
					{
						int num = distinctScores + 1;
						distinctScores = num;
					}
					lastScore = tkey;
					if (distinctScores > k)
					{
						yield break;
					}
					yield return enumerator.Current;
				}
				lastScore = default(TKey);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00048934 File Offset: 0x00046B34
		public static IEnumerable<T> DistinctOn<T, TKey>(this IEnumerable<T> seq, Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			HashSet<TKey> seen = new HashSet<TKey>(comparer);
			using (IEnumerator<T> enumerator = seq.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					TKey tkey = keySelector(t);
					if (seen.Add(tkey))
					{
						yield return enumerator.Current;
					}
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00048954 File Offset: 0x00046B54
		public static bool SequencePrefixEqual<T>(this IEnumerable<T> xs, IEnumerable<T> ys, int count, EqualityComparer<T> equality = null)
		{
			equality = equality ?? EqualityComparer<T>.Default;
			using (IEnumerator<T> enumerator = xs.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = ys.GetEnumerator())
				{
					while (count-- > 0)
					{
						if (!enumerator.MoveNext() || !enumerator2.MoveNext())
						{
							return false;
						}
						if (!equality.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000489E8 File Offset: 0x00046BE8
		public static IEnumerable<TResult> Select2<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> seq, Func<TKey, TValue, TResult> func)
		{
			return seq.Select((KeyValuePair<TKey, TValue> t) => func(t.Key, t.Value));
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00048A14 File Offset: 0x00046C14
		public static IEnumerable<TResult> Select2<T1, T2, TResult>(this IEnumerable<Record<T1, T2>> seq, Func<T1, T2, TResult> func)
		{
			return seq.Select((Record<T1, T2> t) => func(t.Item1, t.Item2));
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00048A40 File Offset: 0x00046C40
		public static IEnumerable<TResult> Select2<T1, T2, T3, TResult>(this IEnumerable<Record<T1, T2, T3>> seq, Func<T1, T2, T3, TResult> func)
		{
			return seq.Select((Record<T1, T2, T3> t) => func(t.Item1, t.Item2, t.Item3));
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00048A6C File Offset: 0x00046C6C
		public static IEnumerable<TResult> Select2<T1, T2, TResult>(this IEnumerable<Tuple<T1, T2>> seq, Func<T1, T2, TResult> func)
		{
			return seq.Select((Tuple<T1, T2> t) => func(t.Item1, t.Item2));
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00048A98 File Offset: 0x00046C98
		public static IEnumerable<TResult> Select2<T1, T2, T3, TResult>(this IEnumerable<Tuple<T1, T2, T3>> seq, Func<T1, T2, T3, TResult> func)
		{
			return seq.Select((Tuple<T1, T2, T3> t) => func(t.Item1, t.Item2, t.Item3));
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00048AC4 File Offset: 0x00046CC4
		public static IEnumerable<TResult> SelectMany2<T1, T2, TResult>(this IEnumerable<Record<T1, T2>> seq, Func<T1, T2, IEnumerable<TResult>> func)
		{
			return seq.SelectMany((Record<T1, T2> t) => func(t.Item1, t.Item2));
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00048AF0 File Offset: 0x00046CF0
		public static IEnumerable<TResult> SelectMany2<T1, T2, TResult>(this IEnumerable<KeyValuePair<T1, T2>> seq, Func<T1, T2, IEnumerable<TResult>> func)
		{
			return seq.SelectMany((KeyValuePair<T1, T2> t) => func(t.Key, t.Value));
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00048B1C File Offset: 0x00046D1C
		public static IEnumerable<Record<T1, T2>> Where2<T1, T2>(this IEnumerable<Record<T1, T2>> seq, Func<T1, T2, bool> func)
		{
			return seq.Where((Record<T1, T2> t) => func(t.Item1, t.Item2));
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00048B48 File Offset: 0x00046D48
		public static IEnumerable<Record<T1, T2, T3>> Where2<T1, T2, T3>(this IEnumerable<Record<T1, T2, T3>> seq, Func<T1, T2, T3, bool> func)
		{
			return seq.Where((Record<T1, T2, T3> t) => func(t.Item1, t.Item2, t.Item3));
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00048B74 File Offset: 0x00046D74
		public static IEnumerable<KeyValuePair<TKey, TValue>> Where2<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> seq, Func<TKey, TValue, bool> func)
		{
			return seq.Where((KeyValuePair<TKey, TValue> t) => func(t.Key, t.Value));
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00048BA0 File Offset: 0x00046DA0
		public static TAccumulate AggregateSeedFunc<TSource, TAccumulate>(this IEnumerable<TSource> source, Func<TSource, TAccumulate> seedFunc, Func<TAccumulate, TSource, TAccumulate> func)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (seedFunc == null)
			{
				throw new ArgumentNullException("seedFunc");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			TAccumulate taccumulate2;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException();
				}
				TAccumulate taccumulate = seedFunc(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					taccumulate = func(taccumulate, tsource);
				}
				taccumulate2 = taccumulate;
			}
			return taccumulate2;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00048C30 File Offset: 0x00046E30
		public static TResult Aggregate2<T1, T2, TResult>(this IEnumerable<Record<T1, T2>> seq, TResult seed, Func<TResult, T1, T2, TResult> folder)
		{
			return seq.Aggregate(seed, (TResult acc, Record<T1, T2> t) => folder(acc, t.Item1, t.Item2));
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00048C60 File Offset: 0x00046E60
		public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<Tuple<T1, T2>> seq)
		{
			return seq.ToDictionary((Tuple<T1, T2> t) => t.Item1, (Tuple<T1, T2> t) => t.Item2);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00048CB4 File Offset: 0x00046EB4
		public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<Record<T1, T2>> seq)
		{
			return seq.ToDictionary((Record<T1, T2> t) => t.Item1, (Record<T1, T2> t) => t.Item2);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00048D08 File Offset: 0x00046F08
		public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> seq)
		{
			return seq.ToDictionary((KeyValuePair<T1, T2> t) => t.Key, (KeyValuePair<T1, T2> t) => t.Value);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00048D5C File Offset: 0x00046F5C
		public static Dictionary<T1, T2> ToDictionaryDistinct<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> seq)
		{
			Dictionary<T1, T2> dictionary = new Dictionary<T1, T2>();
			foreach (KeyValuePair<T1, T2> keyValuePair in seq)
			{
				T1 t;
				T2 t2;
				keyValuePair.Deconstruct(out t, out t2);
				T1 t3 = t;
				T2 t4 = t2;
				T2 t5;
				if (dictionary.TryGetValue(t3, out t5))
				{
					if (!object.Equals(t4, t5))
					{
						throw new Exception(string.Format("Multiple non-equal values for key: {0}", t3));
					}
				}
				else
				{
					dictionary[t3] = t4;
				}
			}
			return dictionary;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00048DF0 File Offset: 0x00046FF0
		public static SortedDictionary<T1, T2> ToSortedDictionary<T1, T2>(this IEnumerable<KeyValuePair<T1, T2>> seq)
		{
			return new SortedDictionary<T1, T2>(seq.ToDictionary<T1, T2>());
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00048E00 File Offset: 0x00047000
		public static Dictionary<TKey, TValue> ToDictionaryChecked<T, TKey, TValue>(this IEnumerable<T> xs, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
		{
			Dictionary<TKey, TValue> dictionary;
			try
			{
				dictionary = xs.ToDictionary(keySelector, valueSelector);
			}
			catch (ArgumentException)
			{
				List<TKey> list;
				if (xs.Select(keySelector).HasRepeats(EqualityComparer<TKey>.Default, out list))
				{
					throw new Exception("Cannot create dictionary because the following keys are repeated: " + string.Join<TKey>(", ", list));
				}
				throw;
			}
			return dictionary;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00048E5C File Offset: 0x0004705C
		public static string Format<T>(this ObjectFormatting formatting, T value)
		{
			if (formatting == ObjectFormatting.Literal)
			{
				return value.ToLiteral(null);
			}
			if (formatting != ObjectFormatting.ToString)
			{
				throw new ArgumentOutOfRangeException("formatting");
			}
			return value.ToString();
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00048E8C File Offset: 0x0004708C
		public static string DumpCollection<T>(this IEnumerable<T> collection, ObjectFormatting formatting = ObjectFormatting.Literal, string openDelim = "[", string closeDelim = "]", string separator = ", ", Dictionary<object, int> identityCache = null)
		{
			StringBuilder stringBuilder = new StringBuilder(openDelim);
			int num = 0;
			foreach (T t in collection)
			{
				if (num++ > 0)
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.Append(t.InternedFormat(identityCache, formatting));
			}
			stringBuilder.Append(closeDelim);
			return stringBuilder.ToString();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00048F0C File Offset: 0x0004710C
		public static IEnumerable<object> ToEnumerable<T>(this T obj)
		{
			if (obj is object[])
			{
				return obj as object[];
			}
			IEnumerable<object> enumerable = obj as IEnumerable<object>;
			if (enumerable != null)
			{
				return enumerable;
			}
			IEnumerable enumerable2 = obj as IEnumerable;
			if (enumerable2 == null)
			{
				return null;
			}
			return enumerable2.Cast<object>();
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00048F59 File Offset: 0x00047159
		public static IEnumerable<T> Yield<T>(this T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00048F69 File Offset: 0x00047169
		public static IEnumerable<object> BuildArrays(IEnumerable<object> arrayValues, int lengthBound = 3)
		{
			yield return new object[0];
			object[] values = (arrayValues as object[]) ?? arrayValues.ToArray<object>();
			List<ImmutableList<object>> list = new List<ImmutableList<object>> { ImmutableList.Create<object>() };
			List<ImmutableList<object>> arraysNext = new List<ImmutableList<object>>();
			int num;
			for (int length = 1; length <= lengthBound; length = num)
			{
				foreach (ImmutableList<object> prevArray in list)
				{
					foreach (object obj in values)
					{
						ImmutableList<object> newArray = prevArray.Add(obj);
						yield return newArray.ToArray<object>();
						arraysNext.Add(newArray);
						newArray = null;
					}
					object[] array = null;
					prevArray = null;
				}
				List<ImmutableList<object>>.Enumerator enumerator = default(List<ImmutableList<object>>.Enumerator);
				list = arraysNext;
				arraysNext = new List<ImmutableList<object>>();
				num = length + 1;
			}
			yield break;
			yield break;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00048F80 File Offset: 0x00047180
		public static Optional<int> BinarySearchIndex<T>(this IList<T> coll, Func<T, int, ItemLocation> getLocation)
		{
			int num = 0;
			int num2 = coll.Count;
			while (num + 1 < num2)
			{
				int num3 = num + (num2 - num) / 2;
				switch (getLocation(coll[num3], num3))
				{
				case ItemLocation.Here:
					return num3.Some<int>();
				case ItemLocation.GoFurther:
					num = num3;
					break;
				case ItemLocation.GoEarlier:
					num2 = num3;
					break;
				}
			}
			if (num < coll.Count && getLocation(coll[num], num) == ItemLocation.Here)
			{
				return num.Some<int>();
			}
			return Optional<int>.Nothing;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00048FFC File Offset: 0x000471FC
		public static Optional<int> BinarySearchIndex<T>(this IList<T> coll, Func<T, ItemLocation> getLocation)
		{
			return coll.BinarySearchIndex((T el, int _) => getLocation(el));
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00049028 File Offset: 0x00047228
		public static Func<T, ItemLocation> KeyCompareWith<T, TKey>(Func<T, TKey> keySelector, TKey target)
		{
			return delegate(T x)
			{
				int num = Comparer<TKey>.Default.Compare(keySelector(x), target);
				if (num == 0)
				{
					return ItemLocation.Here;
				}
				if (num >= 0)
				{
					return ItemLocation.GoEarlier;
				}
				return ItemLocation.GoFurther;
			};
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00049048 File Offset: 0x00047248
		public static IEnumerable<T> Intersect<T>(this IEnumerable<IEnumerable<T>> collections)
		{
			HashSet<T> hashSet = null;
			foreach (IEnumerable<T> enumerable in collections)
			{
				if (hashSet == null)
				{
					hashSet = new HashSet<T>(enumerable);
				}
				else
				{
					hashSet.IntersectWith(enumerable);
					if (hashSet.Count == 0)
					{
						return hashSet;
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000490B0 File Offset: 0x000472B0
		public static IEnumerable<T> Union<T>(this IEnumerable<IEnumerable<T>> collections)
		{
			HashSet<T> hashSet = null;
			foreach (IEnumerable<T> enumerable in collections)
			{
				if (hashSet == null)
				{
					hashSet = new HashSet<T>(enumerable);
				}
				else
				{
					hashSet.UnionWith(enumerable);
				}
			}
			return hashSet;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00049108 File Offset: 0x00047308
		public static IReadOnlyCollection<IReadOnlyCollection<T>> EquivalenceClasses<T>(IEnumerable<Record<T, T>> equalElements, IEqualityComparer<T> baseEquality = null)
		{
			CollectionUtils.UnionFind<T> unionFind = new CollectionUtils.UnionFind<T>(baseEquality ?? EqualityComparer<T>.Default);
			equalElements.ForEach(delegate(Record<T, T> equalPair)
			{
				unionFind.Union(equalPair.Item1, equalPair.Item2);
			});
			return unionFind.Sets;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0004914D File Offset: 0x0004734D
		public static IEnumerable<IReadOnlyList<T>> SplitOn<T>(this IEnumerable<T> xs, Func<T, bool> isDelimiter)
		{
			IEnumerator<T> enumerator = xs.GetEnumerator();
			List<T> list = new List<T>();
			while (enumerator.MoveNext())
			{
				if (isDelimiter(enumerator.Current))
				{
					yield return list;
					list = new List<T>();
				}
				else
				{
					list.Add(enumerator.Current);
				}
			}
			yield return list;
			yield break;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00049164 File Offset: 0x00047364
		public static IEnumerable<IReadOnlyList<T>> SplitOn<T>(this IEnumerable<T> xs, T delimiter)
		{
			return xs.SplitOn((T t) => object.Equals(t, delimiter));
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00049190 File Offset: 0x00047390
		public static IEnumerable<IGrouping<TKey, TElement>> SplitRuns<TKey, TElement>(this IEnumerable<TElement> seq, Func<TElement, TKey> keySelector, Func<TKey, TKey, bool> sameRunFunc, Func<TKey, TKey, TKey> keyCombiner)
		{
			keyCombiner = keyCombiner ?? new Func<TKey, TKey, TKey>(CollectionUtils.<SplitRuns>g__First|93_0<TKey, TElement>);
			using (IEnumerator<KeyValuePair<TKey, TElement>> iterator = seq.Select((TElement item) => new KeyValuePair<TKey, TElement>(keySelector(item), item)).GetEnumerator())
			{
				TKey tkey = default(TKey);
				List<TElement> list = null;
				while (iterator.MoveNext())
				{
					KeyValuePair<TKey, TElement> item2 = iterator.Current;
					if (list != null && sameRunFunc(tkey, item2.Key))
					{
						tkey = keyCombiner(tkey, item2.Key);
						list.Add(item2.Value);
					}
					else
					{
						if (list != null)
						{
							yield return CollectionUtils.CreateGroupAdjacentGrouping<TKey, TElement>(tkey, list);
						}
						tkey = item2.Key;
						list = new List<TElement> { item2.Value };
					}
					item2 = default(KeyValuePair<TKey, TElement>);
				}
				if (list != null)
				{
					yield return CollectionUtils.CreateGroupAdjacentGrouping<TKey, TElement>(tkey, list);
				}
			}
			IEnumerator<KeyValuePair<TKey, TElement>> iterator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000491B8 File Offset: 0x000473B8
		public static IEnumerable<IGrouping<TKey, TElement>> SplitRuns<TKey, TElement>(this IEnumerable<TElement> seq, Func<TElement, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			return seq.SplitRuns(keySelector, (TKey x, TKey y) => comparer.Equals(x, y), null);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000491FC File Offset: 0x000473FC
		public static IEnumerable<IGrouping<T, T>> SplitRuns<T>(this IEnumerable<T> seq, IEqualityComparer<T> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<T>.Default;
			return seq.SplitRuns((T x) => x, (T x, T y) => comparer.Equals(x, y), null);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00049260 File Offset: 0x00047460
		private static IGrouping<TKey, TElement> CreateGroupAdjacentGrouping<TKey, TElement>(TKey key, IList<TElement> members)
		{
			IList<TElement> list2;
			if (!members.IsReadOnly)
			{
				IList<TElement> list = new ReadOnlyCollection<TElement>(members);
				list2 = list;
			}
			else
			{
				list2 = members;
			}
			return new CollectionUtils.Grouping<TKey, TElement>(key, list2);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00049288 File Offset: 0x00047488
		public static void AddRange<T>(this ISet<T> set, IEnumerable<T> range)
		{
			foreach (T t in range)
			{
				set.Add(t);
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000492D4 File Offset: 0x000474D4
		public static IEnumerable<int> Integers(this int start, int step = 1)
		{
			int i = start;
			for (;;)
			{
				yield return i;
				i += step;
			}
			yield break;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000492EB File Offset: 0x000474EB
		public static bool IsAny<T>(this IEnumerable<T> data)
		{
			return data != null && data.Any<T>();
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000492F8 File Offset: 0x000474F8
		public static bool AnyOrElse<T>(this IEnumerable<T> xs, Func<T, bool> predicate, Func<bool> emptyPredicate)
		{
			bool flag = false;
			foreach (T t in xs)
			{
				if (predicate(t))
				{
					return true;
				}
				flag = true;
			}
			return !flag && emptyPredicate();
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00049358 File Offset: 0x00047558
		public static bool AllOrElseCompute<T>(this IEnumerable<T> xs, Func<T, bool> predicate, Func<bool> emptyPredicate)
		{
			bool flag = false;
			foreach (T t in xs)
			{
				if (!predicate(t))
				{
					return false;
				}
				flag = true;
			}
			return flag || emptyPredicate();
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000493B8 File Offset: 0x000475B8
		public static bool Any<T>(this IEnumerable<T> xs, Func<T, int, bool> predicate)
		{
			return xs.Enumerate<T>().Any((Record<int, T> t) => predicate(t.Item2, t.Item1));
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000493EC File Offset: 0x000475EC
		public static bool Any2<T1, T2>(this IEnumerable<Record<T1, T2>> xs, Func<T1, T2, bool> predicate)
		{
			return xs.Any((Record<T1, T2> t) => predicate(t.Item1, t.Item2));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00049418 File Offset: 0x00047618
		public static bool All2<T1, T2>(this IEnumerable<Record<T1, T2>> xs, Func<T1, T2, bool> predicate)
		{
			return xs.All((Record<T1, T2> t) => predicate(t.Item1, t.Item2));
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00049444 File Offset: 0x00047644
		public static bool IsSubsequenceOf<T>(this IEnumerable<T> xs, IEnumerable<T> ys)
		{
			using (IEnumerator<T> enumerator = ys.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = xs.GetEnumerator())
				{
					IL_003B:
					while (enumerator2.MoveNext())
					{
						T t = enumerator2.Current;
						while (enumerator.MoveNext())
						{
							if (object.Equals(enumerator.Current, t))
							{
								goto IL_003B;
							}
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000494CC File Offset: 0x000476CC
		public static int? IndexOf<T>(this IEnumerable<T> xs, T obj)
		{
			int num = 0;
			using (IEnumerator<T> enumerator = xs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (object.Equals(enumerator.Current, obj))
					{
						return new int?(num);
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00049538 File Offset: 0x00047738
		public static int? IndexOf<T>(this T[] xs, T obj)
		{
			int num = xs.Length;
			for (int i = 0; i < num; i++)
			{
				if (object.Equals(xs[i], obj))
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00049580 File Offset: 0x00047780
		public static IReadOnlyList<T> ToRandomlyOrderedList<T>(this IEnumerable<T> xs, Random random)
		{
			List<T> list = xs.Select((T x) => x).ToList<T>();
			for (int i = list.Count - 1; i > 0; i--)
			{
				int num = random.Next(0, i + 1);
				T t = list[num];
				list[num] = list[i];
				list[i] = t;
			}
			return list;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000495F8 File Offset: 0x000477F8
		public static IEnumerable<T> RandomlySample<T>(this IReadOnlyList<T> xs, Random random, int sampleSize)
		{
			if (xs.Count > sampleSize)
			{
				return xs._RandomlySample(random, sampleSize);
			}
			return xs;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0004961C File Offset: 0x0004781C
		public static IEnumerable<T> RandomlySample<T>(this IReadOnlyList<T> xs, int randomSeed, int sampleSize)
		{
			if (xs.Count > sampleSize)
			{
				return xs._RandomlySample(new Random(randomSeed), sampleSize);
			}
			return xs;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00049644 File Offset: 0x00047844
		public static IReadOnlyList<T> RandomlySampleToList<T>(this IReadOnlyList<T> xs, int randomSeed, int sampleSize)
		{
			if (xs.Count > sampleSize)
			{
				return xs._RandomlySampleToList(new Random(randomSeed), sampleSize);
			}
			return xs;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0004966B File Offset: 0x0004786B
		private static IEnumerable<T> _RandomlySample<T>(this IReadOnlyList<T> xs, Random random, int sampleSize)
		{
			Dictionary<int, int> indices = new Dictionary<int, int>();
			int num2;
			for (int i = 0; i < sampleSize; i = num2 + 1)
			{
				int j = random.Next(i, xs.Count);
				int num;
				if (!indices.TryGetValue(j, out num))
				{
					num = j;
				}
				yield return xs[num];
				if (!indices.TryGetValue(i, out num))
				{
					num = i;
				}
				indices[j] = num;
				num2 = i;
			}
			yield break;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0004968C File Offset: 0x0004788C
		private static T[] _RandomlySampleToList<T>(this IReadOnlyList<T> xs, Random random, int sampleSize)
		{
			T[] array = new T[sampleSize];
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < sampleSize; i++)
			{
				int num = random.Next(i, xs.Count);
				int num2;
				if (!dictionary.TryGetValue(num, out num2))
				{
					num2 = num;
				}
				array[i] = xs[num2];
				if (!dictionary.TryGetValue(i, out num2))
				{
					num2 = i;
				}
				dictionary[num] = num2;
			}
			return array;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000496F8 File Offset: 0x000478F8
		public static T RandomElement<T>(this IReadOnlyList<T> xs, Random random)
		{
			int count = xs.Count;
			if (count != 0)
			{
				return xs[random.Next(count)];
			}
			return default(T);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00049728 File Offset: 0x00047928
		public static IEnumerable<T> RandomlySampleWithReplacement<T>(this IReadOnlyList<T> xs, Random random, int sampleSize)
		{
			return from _ in Enumerable.Range(0, sampleSize)
				select xs.RandomElement(random);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00049761 File Offset: 0x00047961
		public static double InnerProduct(this IReadOnlyList<double> xs, IReadOnlyList<double> ys)
		{
			return xs.Zip(ys, (double x, double y) => x * y).Sum();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00049790 File Offset: 0x00047990
		public static T? FirstOrNull<T>(this IEnumerable<T> items) where T : struct
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return new T?(enumerator.Current);
				}
			}
			return null;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000497E4 File Offset: 0x000479E4
		public static T? FirstOrNull<T>(this IEnumerable<T> items, Func<T, bool> predicate) where T : struct
		{
			foreach (T t in items)
			{
				if (predicate(t))
				{
					return new T?(t);
				}
			}
			return null;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00049844 File Offset: 0x00047A44
		public static T OnlyOrDefault<T>(this IEnumerable<T> xs)
		{
			T t = default(T);
			bool flag = false;
			foreach (T t2 in xs)
			{
				if (flag)
				{
					return default(T);
				}
				flag = true;
				t = t2;
			}
			return t;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000498A8 File Offset: 0x00047AA8
		public static T OnlyOrDefault<T>(this IEnumerable<T> xs, Func<T, bool> predicate)
		{
			return xs.Where(predicate).OnlyOrDefault<T>();
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000498B6 File Offset: 0x00047AB6
		public static IEnumerable<T> Interleave<T>(this IEnumerable<T> xs, IEnumerable<T> ys)
		{
			using (IEnumerator<T> xEnumerator = xs.GetEnumerator())
			{
				using (IEnumerator<T> yEnumerator = ys.GetEnumerator())
				{
					bool flag;
					bool yHasNext;
					while ((flag = xEnumerator.MoveNext()) | (yHasNext = yEnumerator.MoveNext()))
					{
						if (flag)
						{
							yield return xEnumerator.Current;
						}
						if (yHasNext)
						{
							yield return yEnumerator.Current;
						}
					}
				}
				IEnumerator<T> yEnumerator = null;
			}
			IEnumerator<T> xEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000498D0 File Offset: 0x00047AD0
		public static Record<IReadOnlyList<T1>, IReadOnlyList<T2>> UnzipToLists<T1, T2>(this IEnumerable<Record<T1, T2>> xs)
		{
			List<T1> list = new List<T1>();
			List<T2> list2 = new List<T2>();
			foreach (Record<T1, T2> record in xs)
			{
				T1 t;
				T2 t2;
				record.Deconstruct(out t, out t2);
				T1 t3 = t;
				T2 t4 = t2;
				list.Add(t3);
				list2.Add(t4);
			}
			return Record.Create<IReadOnlyList<T1>, IReadOnlyList<T2>>(list, list2);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00049944 File Offset: 0x00047B44
		public static Record<T1[], T2[]> UnzipToArrays<T1, T2>(this IEnumerable<Record<T1, T2>> xs)
		{
			IReadOnlyList<T1> readOnlyList;
			IReadOnlyList<T2> readOnlyList2;
			xs.UnzipToLists<T1, T2>().Deconstruct(out readOnlyList, out readOnlyList2);
			IEnumerable<T1> enumerable = readOnlyList;
			IReadOnlyList<T2> readOnlyList3 = readOnlyList2;
			return Record.Create<T1[], T2[]>(enumerable.ToArray<T1>(), readOnlyList3.ToArray<T2>());
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00049973 File Offset: 0x00047B73
		public static IEnumerable<T> MutateLast<T>(this IEnumerable<T> xs, Func<T, T> mutate)
		{
			T t = default(T);
			bool flag = false;
			foreach (T x in xs)
			{
				if (flag)
				{
					yield return t;
				}
				t = x;
				flag = true;
				x = default(T);
			}
			IEnumerator<T> enumerator = null;
			if (flag)
			{
				yield return mutate(t);
			}
			yield break;
			yield break;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0004998A File Offset: 0x00047B8A
		public static IEnumerable<T> MutateFirst<T>(this IEnumerable<T> xs, Func<T, T> mutate)
		{
			bool flag = false;
			foreach (T t in xs)
			{
				if (flag)
				{
					yield return t;
				}
				else
				{
					yield return mutate(t);
				}
				flag = true;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000499A1 File Offset: 0x00047BA1
		public static IEnumerable<T> MutateAt<T>(this IEnumerable<T> xs, int idx, Func<T, T> mutate)
		{
			int i = 0;
			foreach (T t in xs)
			{
				int num = i;
				i = num + 1;
				if (num == idx)
				{
					yield return mutate(t);
				}
				else
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000499C0 File Offset: 0x00047BC0
		public static IEnumerable<T> DeterministicallySample<T>(this IReadOnlyCollection<T> sequence, int sampleSize)
		{
			if (sampleSize >= sequence.Count)
			{
				return sequence;
			}
			int num = (int)Math.Floor((double)sequence.Count / (double)sampleSize);
			return sequence.TakeEvery(num);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000499F0 File Offset: 0x00047BF0
		public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> ll)
		{
			for (LinkedListNode<T> currentNode = ll.First; currentNode != null; currentNode = currentNode.Next)
			{
				yield return currentNode;
			}
			yield break;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00049A00 File Offset: 0x00047C00
		public static T[,] ToMultidimensionalArray<T>(this T[][] arr)
		{
			T[,] array = new T[arr.Length, arr[0].Length];
			for (int i = 0; i < arr.Length; i++)
			{
				for (int j = 0; j < arr[0].Length; j++)
				{
					array[i, j] = arr[i][j];
				}
			}
			return array;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00049A4B File Offset: 0x00047C4B
		public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp, out TKey key, out TValue value)
		{
			key = kvp.Key;
			value = kvp.Value;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00049A68 File Offset: 0x00047C68
		public static bool DictionaryEquals<TKey, TValue>(this IDictionary<TKey, TValue> dict, IDictionary<TKey, TValue> other, IEqualityComparer<TValue> valueEqualityComparer = null)
		{
			valueEqualityComparer = valueEqualityComparer ?? EqualityComparer<TValue>.Default;
			if (dict == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (dict.Count != other.Count)
			{
				return false;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dict)
			{
				TValue tvalue;
				if (!other.TryGetValue(keyValuePair.Key, out tvalue) || !valueEqualityComparer.Equals(keyValuePair.Value, tvalue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00049AF8 File Offset: 0x00047CF8
		public static bool ReadOnlyDictionaryEquals<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, IReadOnlyDictionary<TKey, TValue> other, IEqualityComparer<TValue> valueEqualityComparer = null)
		{
			valueEqualityComparer = valueEqualityComparer ?? EqualityComparer<TValue>.Default;
			if (dict == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (dict.Count != other.Count)
			{
				return false;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dict)
			{
				TValue tvalue;
				if (!other.TryGetValue(keyValuePair.Key, out tvalue) || !valueEqualityComparer.Equals(keyValuePair.Value, tvalue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00049B88 File Offset: 0x00047D88
		public static void PartitionByPredicate<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out IList<T> positives, out IList<T> negatives)
		{
			positives = new List<T>();
			negatives = new List<T>();
			foreach (T t in enumerable)
			{
				if (predicate(t))
				{
					positives.Add(t);
				}
				else
				{
					negatives.Add(t);
				}
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00049BF4 File Offset: 0x00047DF4
		public static IList<T> SelectIndices<T>(this IList<T> collection, IList<int> indices, bool containing = true)
		{
			IList<T> list;
			if (containing)
			{
				list = new T[indices.Count];
				for (int i = 0; i < indices.Count; i++)
				{
					list[i] = collection[indices[i]];
				}
			}
			else
			{
				list = new List<T>(collection.Count - indices.Count);
				HashSet<int> hashSet = indices.ConvertToHashSet<int>();
				for (int j = 0; j < collection.Count; j++)
				{
					if (!hashSet.Contains(j))
					{
						list.Add(collection[j]);
					}
				}
			}
			return list;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00049C79 File Offset: 0x00047E79
		public static IEnumerable<int> FindAllIndexes<T>(this IEnumerable<T> xs, Func<T, int, bool> predicate, int startIndex = 0)
		{
			int i = 0;
			foreach (T t in xs)
			{
				if (i >= startIndex && predicate(t, i))
				{
					yield return i;
				}
				int num = i;
				i = num + 1;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00049C98 File Offset: 0x00047E98
		public static IEnumerable<int> FindAllIndexes<T>(this IEnumerable<T> xs, Func<T, bool> predicate, int startIndex = 0)
		{
			return xs.FindAllIndexes((T x, int _) => predicate(x), startIndex);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00049CC8 File Offset: 0x00047EC8
		public static int? IndexOfByReference<T>(this IEnumerable<T> xs, T element)
		{
			return xs.FindAllIndexes((T n) => n == element, 0).FirstOrNull<int>();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00049CFA File Offset: 0x00047EFA
		public static IEnumerable<TResult> Windowed<T, TResult>(this IEnumerable<T> seq, Func<T, T, TResult> func)
		{
			bool flag = true;
			T t = default(T);
			foreach (T item in seq)
			{
				if (!flag)
				{
					yield return func(t, item);
				}
				flag = false;
				t = item;
				item = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00049D11 File Offset: 0x00047F11
		public static IEnumerable<TResult> Windowed3<T, TResult>(this IEnumerable<T> seq, Func<T, T, T, TResult> func)
		{
			int seen = 0;
			T t = default(T);
			T prev2 = default(T);
			foreach (T item in seq)
			{
				int num = seen + 1;
				seen = num;
				if (num >= 3)
				{
					yield return func(t, prev2, item);
				}
				t = prev2;
				prev2 = item;
				item = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00049D28 File Offset: 0x00047F28
		public static IEnumerable<Record<T, T>> Windowed<T>(this IEnumerable<T> seq)
		{
			Func<T, T, Record<T, T>> func;
			if ((func = CollectionUtils.<Windowed>O__141_0<T>.<0>__Create) == null)
			{
				func = (CollectionUtils.<Windowed>O__141_0<T>.<0>__Create = new Func<T, T, Record<T, T>>(Record.Create<T, T>));
			}
			return seq.Windowed(func);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00049D4B File Offset: 0x00047F4B
		public static IEnumerable<Record<T, T, T>> Windowed3<T>(this IEnumerable<T> seq)
		{
			Func<T, T, T, Record<T, T, T>> func;
			if ((func = CollectionUtils.<Windowed3>O__142_0<T>.<0>__Create) == null)
			{
				func = (CollectionUtils.<Windowed3>O__142_0<T>.<0>__Create = new Func<T, T, T, Record<T, T, T>>(Record.Create<T, T, T>));
			}
			return seq.Windowed3(func);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00049D70 File Offset: 0x00047F70
		public static IEnumerable<IEnumerable<T>> Choose<T>(this IReadOnlyList<T> xs, int k)
		{
			int count = xs.Count;
			return CollectionUtils._Choose<T>(xs, 0, count, k);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00049D90 File Offset: 0x00047F90
		private static IEnumerable<IEnumerable<T>> _Choose<T>(IReadOnlyList<T> xs, int start, int n, int k)
		{
			if (n < k)
			{
				return Enumerable.Empty<IEnumerable<T>>();
			}
			if (n == k)
			{
				return xs.Range(start, start + n).Yield<IEnumerable<T>>();
			}
			if (k == 0)
			{
				return Enumerable.Empty<T>().Yield<IEnumerable<T>>();
			}
			T first = xs[start];
			IEnumerable<IEnumerable<T>> enumerable = from x in CollectionUtils._Choose<T>(xs, start + 1, n - 1, k - 1)
				select x.PrependItem(first);
			IEnumerable<IEnumerable<T>> enumerable2 = CollectionUtils._Choose<T>(xs, start + 1, n - 1, k);
			return enumerable.Concat(enumerable2);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00049E10 File Offset: 0x00048010
		public static IEnumerable<T> SortAndRemoveSubordinates<T>(this IEnumerable<T> elements, IComparer<T> comparer, Func<T, T, bool> supersedes)
		{
			return elements.SortAndRemoveSubordinatesBy((T x) => x, comparer, supersedes);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00049E3C File Offset: 0x0004803C
		public static IEnumerable<TElement> SortAndRemoveSubordinatesBy<TElement, TKey>(this IEnumerable elements, Func<TElement, TKey> keySelector, IComparer<TKey> comparer, Func<TKey, TKey, bool> supersedes)
		{
			LinkedList<TElement> linkedList = new LinkedList<TElement>();
			foreach (object obj in elements)
			{
				TElement telement = (TElement)((object)obj);
				bool flag = false;
				for (LinkedListNode<TElement> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					if (supersedes(keySelector(linkedListNode.Value), keySelector(telement)))
					{
						flag = true;
						break;
					}
					if (comparer.Compare(keySelector(telement), keySelector(linkedListNode.Value)) > 0)
					{
						flag = true;
						linkedList.AddBefore(linkedListNode, telement);
						for (LinkedListNode<TElement> linkedListNode2 = linkedListNode; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
						{
							if (supersedes(keySelector(telement), keySelector(linkedListNode2.Value)))
							{
								linkedListNode2 = linkedListNode2.Previous;
								linkedList.Remove(linkedListNode2.Next);
							}
						}
						break;
					}
				}
				if (!flag)
				{
					linkedList.AddLast(telement);
				}
			}
			return linkedList;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00049F58 File Offset: 0x00048158
		public static IEnumerable<int> RangeUpTo(int start, int end)
		{
			int num;
			for (int i = start; i < end; i = num + 1)
			{
				yield return i;
				num = i;
			}
			yield break;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00049F70 File Offset: 0x00048170
		public static IEnumerable<T> IterativeFilterOf<T>(this IEnumerable<Func<T, bool>> filters, IEnumerable<T> xs)
		{
			HashSet<T> hashSet = (xs as HashSet<T>) ?? xs.ConvertToHashSet<T>();
			using (IEnumerator<Func<T, bool>> enumerator = filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Func<T, bool> filter = enumerator.Current;
					hashSet.RemoveWhere((T el) => !filter(el));
					if (hashSet.Count == 0)
					{
						break;
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00049FEC File Offset: 0x000481EC
		public static bool TryDequeue<T>(this Queue<T> queue, out T value)
		{
			if (queue.Count > 0)
			{
				value = queue.Dequeue();
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0004A00D File Offset: 0x0004820D
		public static bool TryPop<T>(this Stack<T> stack, out T value)
		{
			if (stack.Count > 0)
			{
				value = stack.Pop();
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0004A030 File Offset: 0x00048230
		public static void AddOrInsert<TKey, TCollection, TValue>(this IDictionary<TKey, TCollection> dict, TKey key, TValue value) where TCollection : ICollection<TValue>, new()
		{
			TCollection orCreateValue = dict.GetOrCreateValue(key);
			orCreateValue.Add(value);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x0004A053 File Offset: 0x00048253
		public static bool HasExactly<T>(this IEnumerable<T> xs, int k)
		{
			return xs.Take(k + 1).Count<T>() == k;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0004A068 File Offset: 0x00048268
		public static bool HasRepeats<T>(this IEnumerable<T> xs, IEqualityComparer<T> equalityComparer, out List<T> repeats)
		{
			repeats = (from g in xs.GroupBy((T x) => x, equalityComparer ?? EqualityComparer<T>.Default)
				where g.Count<T>() > 1
				select g.Key).ToList<T>();
			return repeats.Any<T>();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0004A0FA File Offset: 0x000482FA
		public static bool HasRepeats<T>(this IEnumerable<T> xs, Func<T, T, bool> equalityComparer, out List<T> repeats)
		{
			return xs.HasRepeats(FuncEqualityComparer.Create<T>(equalityComparer), out repeats);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0004A109 File Offset: 0x00048309
		public static bool HasRepeats<T>(this IEnumerable<T> xs, out List<T> repeats)
		{
			return xs.HasRepeats(null, out repeats);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0004A114 File Offset: 0x00048314
		public static bool HasRepeats<T>(this IEnumerable<T> xs, IEqualityComparer<T> equalityComparer)
		{
			List<T> list;
			return xs.HasRepeats(equalityComparer, out list);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0004A12C File Offset: 0x0004832C
		public static bool HasRepeats<T>(this IEnumerable<T> xs, Func<T, T, bool> equalityComparer)
		{
			List<T> list;
			return xs.HasRepeats(equalityComparer, out list);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0004A144 File Offset: 0x00048344
		public static bool HasRepeats<T>(this IEnumerable<T> xs)
		{
			List<T> list;
			return xs.HasRepeats(null, out list);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0004A15C File Offset: 0x0004835C
		public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> xs, int amount)
		{
			CollectionUtils.<>c__DisplayClass159_0<T> CS$<>8__locals1 = new CollectionUtils.<>c__DisplayClass159_0<T>();
			CS$<>8__locals1.amount = amount;
			CS$<>8__locals1.xs = xs;
			if (CS$<>8__locals1.amount == 0)
			{
				return CS$<>8__locals1.xs;
			}
			if (CS$<>8__locals1.amount < 0)
			{
				return CS$<>8__locals1.xs.RotateRight(-CS$<>8__locals1.amount);
			}
			return CS$<>8__locals1.<RotateLeft>g__Enumerator|0();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0004A1B0 File Offset: 0x000483B0
		public static IEnumerable<T> RotateRight<T>(this IEnumerable<T> xs, int amount)
		{
			if (amount == 0)
			{
				return xs;
			}
			if (amount < 0)
			{
				return xs.RotateLeft(-amount);
			}
			ICollection<T> collection = (xs as ICollection<T>) ?? xs.ToList<T>();
			return collection.RotateLeft(collection.Count - amount);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0004A1EE File Offset: 0x000483EE
		public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> xs)
		{
			return xs == null || xs.Count == 0;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0004A200 File Offset: 0x00048400
		public static T Mode<T>(this IEnumerable<T> xs, IEqualityComparer<T> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<T>.Default;
			return xs.GroupBy((T x) => x, comparer).ArgMax((IGrouping<T, T> g) => g.Count<T>()).Key;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0004A268 File Offset: 0x00048468
		public static IReadOnlyList<Record<int, int>> AdjacentClumps(this IEnumerable<int> xs, int tolerance, int? maxSize)
		{
			List<Record<int, int>> list = new List<Record<int, int>>();
			foreach (int num in xs)
			{
				Record<int, int> record = list.LastOrDefault<Record<int, int>>();
				if (list.Count == 0 || num > record.Item2 + tolerance)
				{
					list.Add(new Record<int, int>(num, num));
				}
				else if (maxSize == null || record.Item2 - record.Item1 + 1 < maxSize.Value)
				{
					list[list.Count - 1] = new Record<int, int>(record.Item1, num);
				}
				else
				{
					list.Add(new Record<int, int>(record.Item1 + 1, num));
				}
			}
			return list;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0004A330 File Offset: 0x00048530
		public static void Clear<T>(this BlockingCollection<T> collection)
		{
			T t;
			while (collection.TryTake(out t))
			{
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0004A347 File Offset: 0x00048547
		public static IEnumerable<T> BatchedConsumingEnumerable<T>(this BlockingCollection<T> collection, CancellationToken cancellationToken = default(CancellationToken))
		{
			yield return collection.Take(cancellationToken);
			T t;
			while (collection.TryTake(out t, -1, cancellationToken))
			{
				yield return t;
			}
			yield break;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0004A35E File Offset: 0x0004855E
		public static T[] EmptyArray<T>()
		{
			return EmptyArrayImpl<T>.Value;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0004A365 File Offset: 0x00048565
		[NullableContext(1)]
		public static IEnumerable<IGrouping<TKey, TValue>> GroupByNonNull<TKey, [Nullable(2)] TValue>(this IEnumerable<TValue> xs, [Nullable(new byte[] { 1, 1, 2 })] Func<TValue, TKey> keySelector) where TKey : class
		{
			return from g in xs.GroupBy(keySelector)
				where g.Key != null
				select g;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0004A394 File Offset: 0x00048594
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		public static IEnumerable<IGrouping<TKey, TValue>> GroupByNonNull<TKey, [Nullable(2)] TValue>([Nullable(1)] this IEnumerable<TValue> xs, [Nullable(new byte[] { 1, 1, 0 })] Func<TValue, TKey?> keySelector) where TKey : struct
		{
			return from x in xs
				select KVP.Create<TKey?, TValue>(keySelector(x), x) into kv
				where kv.Key != null
				group kv.Value by kv.Key.Value;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0004A428 File Offset: 0x00048628
		public static IReadOnlyList<T> WholeNonNullSequence<T>(this IEnumerable<T> xs)
		{
			List<T> list = new List<T>();
			foreach (T t in xs)
			{
				if (t == null)
				{
					return null;
				}
				list.Add(t);
			}
			return list;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0004A488 File Offset: 0x00048688
		public static T Median<T>(this IReadOnlyCollection<T> elements) where T : IComparable<T>
		{
			return elements.OrderStatistic(elements.Count / 2);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0004A498 File Offset: 0x00048698
		public static T OrderStatistic<T>(this IReadOnlyCollection<T> elements, int k) where T : IComparable<T>
		{
			return elements.OrderBy((T e) => e).Skip(k - 1).First<T>();
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0004A4CC File Offset: 0x000486CC
		public static IEnumerable<Record<T, int>> WithIndex<T>(this IEnumerable<T> seq, int startIndex = 0, int step = 1)
		{
			int index = startIndex;
			foreach (T t in seq)
			{
				yield return Record.Create<T, int>(t, index);
				index += step;
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0004A4EC File Offset: 0x000486EC
		public static bool IsSorted<T>(this IEnumerable<T> seq, IComparer<T> comparer)
		{
			return seq.Windowed((T a, T b) => comparer.Compare(a, b) <= 0).All((bool x) => x);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0004A53C File Offset: 0x0004873C
		public static bool IsSorted<T>(this IEnumerable<T> seq)
		{
			return seq.Windowed((T a, T b) => Comparer<T>.Default.Compare(a, b) <= 0).All((bool x) => x);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0004A592 File Offset: 0x00048792
		public static IEnumerable<T> Range<T>(this IEnumerable<T> seq, int start, int end)
		{
			return seq.Skip(start).Take(end - start);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0004A5A4 File Offset: 0x000487A4
		public static int SequenceFind<T>(this IReadOnlyList<T> haystack, IReadOnlyList<T> needle)
		{
			for (int i = 0; i < haystack.Count; i++)
			{
				bool flag = true;
				int j = 0;
				while (j < needle.Count)
				{
					if (i + j < haystack.Count)
					{
						T t = haystack[i + j];
						if (t.Equals(needle[j]))
						{
							j++;
							continue;
						}
					}
					flag = false;
					break;
				}
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0004A60F File Offset: 0x0004880F
		public static Dictionary<TKey, TValue> ToReferenceEqualityDictionary<T, TKey, TValue>(this IEnumerable<T> dict, Func<T, TKey> keySelector, Func<T, TValue> valueSelector) where TKey : class
		{
			return dict.ToDictionary(keySelector, valueSelector, IdentityEquality.Comparer);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00004FAE File Offset: 0x000031AE
		[CompilerGenerated]
		internal static TKey <SplitRuns>g__First|93_0<TKey, TElement>(TKey x, TKey _)
		{
			return x;
		}

		// Token: 0x0200040D RID: 1037
		public class UnionFind<T>
		{
			// Token: 0x06001830 RID: 6192 RVA: 0x0004A61E File Offset: 0x0004881E
			public UnionFind(IEqualityComparer<T> baseEquality)
			{
				this._items = new List<CollectionUtils.UnionFind<T>.ParentElement>();
				this._itemIndex = new Dictionary<T, int>(baseEquality);
			}

			// Token: 0x06001831 RID: 6193 RVA: 0x0004A640 File Offset: 0x00048840
			public int GetId(T t)
			{
				int count;
				if (!this._itemIndex.TryGetValue(t, out count))
				{
					count = this._items.Count;
					this._items.Add(new CollectionUtils.UnionFind<T>.ParentElement(count, t));
					this._itemIndex[t] = count;
				}
				return count;
			}

			// Token: 0x06001832 RID: 6194 RVA: 0x0004A68C File Offset: 0x0004888C
			public int Find(int i)
			{
				if (this._items[i].Parent != i)
				{
					return this._items[i].Parent = this.Find(this._items[i].Parent);
				}
				return i;
			}

			// Token: 0x06001833 RID: 6195 RVA: 0x0004A6DA File Offset: 0x000488DA
			public void Find(T x)
			{
				this.Find(this.GetId(x));
			}

			// Token: 0x06001834 RID: 6196 RVA: 0x0004A6EA File Offset: 0x000488EA
			public void Union(int i, int j)
			{
				this._items[this.Find(i)].Parent = this.Find(j);
			}

			// Token: 0x06001835 RID: 6197 RVA: 0x0004A70A File Offset: 0x0004890A
			public void Union(T x, T y)
			{
				this.Union(this.GetId(x), this.GetId(y));
			}

			// Token: 0x1700045D RID: 1117
			// (get) Token: 0x06001836 RID: 6198 RVA: 0x0004A720 File Offset: 0x00048920
			public IReadOnlyCollection<IReadOnlyCollection<T>> Sets
			{
				get
				{
					return (from item in this._items
						group item by this.Find(item.Parent) into g
						select g.Select((CollectionUtils.UnionFind<T>.ParentElement item) => item.Element).ToList<T>()).ToList<List<T>>();
				}
			}

			// Token: 0x04000B45 RID: 2885
			private readonly List<CollectionUtils.UnionFind<T>.ParentElement> _items;

			// Token: 0x04000B46 RID: 2886
			private readonly Dictionary<T, int> _itemIndex;

			// Token: 0x0200040E RID: 1038
			private class ParentElement
			{
				// Token: 0x1700045E RID: 1118
				// (get) Token: 0x06001838 RID: 6200 RVA: 0x0004A77B File Offset: 0x0004897B
				// (set) Token: 0x06001839 RID: 6201 RVA: 0x0004A783 File Offset: 0x00048983
				public int Parent { get; set; }

				// Token: 0x1700045F RID: 1119
				// (get) Token: 0x0600183A RID: 6202 RVA: 0x0004A78C File Offset: 0x0004898C
				public T Element { get; }

				// Token: 0x0600183B RID: 6203 RVA: 0x0004A794 File Offset: 0x00048994
				public ParentElement(int parent, T element)
				{
					this.Parent = parent;
					this.Element = element;
				}
			}
		}

		// Token: 0x02000410 RID: 1040
		private class Grouping<TKey, TElement> : IGrouping<TKey, TElement>, IEnumerable<TElement>, IEnumerable
		{
			// Token: 0x06001840 RID: 6208 RVA: 0x0004A7EA File Offset: 0x000489EA
			public Grouping(TKey key, IList<TElement> elements)
			{
				this.Key = key;
				this._elements = elements;
			}

			// Token: 0x06001841 RID: 6209 RVA: 0x0004A800 File Offset: 0x00048A00
			public IEnumerator<TElement> GetEnumerator()
			{
				return this._elements.GetEnumerator();
			}

			// Token: 0x06001842 RID: 6210 RVA: 0x0004A80D File Offset: 0x00048A0D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x06001843 RID: 6211 RVA: 0x0004A815 File Offset: 0x00048A15
			public TKey Key { get; }

			// Token: 0x04000B4C RID: 2892
			private readonly IList<TElement> _elements;
		}

		// Token: 0x02000471 RID: 1137
		[CompilerGenerated]
		private static class <Windowed3>O__142_0<T>
		{
			// Token: 0x04000CA1 RID: 3233
			public static Func<T, T, T, Record<T, T, T>> <0>__Create;
		}

		// Token: 0x02000473 RID: 1139
		[CompilerGenerated]
		private static class <Windowed>O__141_0<T>
		{
			// Token: 0x04000CAD RID: 3245
			public static Func<T, T, Record<T, T>> <0>__Create;
		}

		// Token: 0x02000477 RID: 1143
		[CompilerGenerated]
		private static class <ZipWith>O__34_0<T1, T2>
		{
			// Token: 0x04000CC7 RID: 3271
			public static Func<T1, T2, Record<T1, T2>> <0>__Create;
		}

		// Token: 0x02000478 RID: 1144
		[CompilerGenerated]
		private static class <ZipWith>O__36_0<T1, T2, T3>
		{
			// Token: 0x04000CC8 RID: 3272
			public static Func<T1, T2, T3, Record<T1, T2, T3>> <0>__Create;
		}
	}
}
