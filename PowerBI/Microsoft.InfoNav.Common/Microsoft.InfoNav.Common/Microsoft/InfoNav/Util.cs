using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav
{
	// Token: 0x02000012 RID: 18
	internal static class Util
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000319C File Offset: 0x0000139C
		[DebuggerStepThrough]
		internal static bool? AreEqual<T>(T x, T y) where T : class
		{
			if (x == y)
			{
				return new bool?(true);
			}
			if (x == null || y == null)
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000031E0 File Offset: 0x000013E0
		[DebuggerStepThrough]
		internal static bool BagEquals<T>(this IEnumerable<T> x, IEnumerable<T> y, IEqualityComparer<T> equalityComparer)
		{
			bool? flag = Util.AreEqual<IEnumerable<T>>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			Dictionary<T, int> elementCounts = x.GetElementCounts(equalityComparer);
			Dictionary<T, int> elementCounts2 = y.GetElementCounts(equalityComparer);
			if (elementCounts.Count != elementCounts2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<T, int> keyValuePair in elementCounts2)
			{
				int num;
				if (!elementCounts.TryGetValue(keyValuePair.Key, out num) || num != keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003288 File Offset: 0x00001488
		internal static bool IsStoppingException(this Exception ex)
		{
			return ex.ExceptionOrInnerExceptionsSatisfiesCondition(Util._fatalExceptionCondition);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003295 File Offset: 0x00001495
		internal static bool ExceptionOrInnerExceptionsSatisfiesCondition(this Exception exception, Predicate<Exception> condition)
		{
			while (exception != null)
			{
				if (condition(exception))
				{
					return true;
				}
				if (!(exception is AggregateException))
				{
					exception = exception.InnerException;
				}
			}
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000032B8 File Offset: 0x000014B8
		internal static bool ContainsInnerExceptionOfType<TException>(this Exception exception, out TException innerException) where TException : Exception
		{
			while (exception != null)
			{
				if (exception is TException)
				{
					innerException = (TException)((object)exception);
					return true;
				}
				AggregateException ex = exception as AggregateException;
				if (ex != null)
				{
					using (IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.ContainsInnerExceptionOfType(out innerException))
							{
								return true;
							}
						}
					}
					innerException = default(TException);
					return false;
				}
				exception = exception.InnerException;
			}
			innerException = default(TException);
			return false;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000334C File Offset: 0x0000154C
		internal static string ToTelemetrySafeString(this Exception exception)
		{
			return string.Concat(new string[]
			{
				"Exception of type ",
				exception.GetType().Name,
				": '",
				exception.Message.MarkAsUserContent(),
				"' at ",
				exception.StackTrace
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000033A4 File Offset: 0x000015A4
		internal static int LowerBoundBinarySearch<T>(IReadOnlyList<T> values, int start, int count, T searchValue, IComparer<T> comparer)
		{
			return Util.LowerBoundBinarySearchInternal<T>(values, start, count, searchValue, comparer, (int index) => values[index]);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000033DC File Offset: 0x000015DC
		internal static int LowerBoundBinarySearchList<T>(IList<T> values, int start, int count, T searchValue, IComparer<T> comparer)
		{
			return Util.LowerBoundBinarySearchInternal<T>(values, start, count, searchValue, comparer, (int index) => values[index]);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003414 File Offset: 0x00001614
		private static int LowerBoundBinarySearchInternal<T>(IEnumerable<T> values, int start, int count, T searchValue, IComparer<T> comparer, Func<int, T> getElementAt)
		{
			int i = start;
			int num = start + count - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(getElementAt(num2), searchValue);
				if (num3 == 0 && (num2 == i || comparer.Compare(searchValue, getElementAt(num2 - 1)) != 0))
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003475 File Offset: 0x00001675
		[DebuggerStepThrough]
		internal static T[] ArrayWrap<T>(this T item)
		{
			return new T[] { item };
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003485 File Offset: 0x00001685
		internal static List<T> ListWrap<T>(this T item)
		{
			return new List<T> { item };
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003494 File Offset: 0x00001694
		[DebuggerStepThrough]
		internal static void Swap<T>(ref T x, ref T y)
		{
			T t = x;
			x = y;
			y = t;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000034BC File Offset: 0x000016BC
		[DebuggerStepThrough]
		internal static bool IsDefault<T>(this T v) where T : struct
		{
			return EqualityComparer<T>.Default.Equals(v, default(T));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000034DD File Offset: 0x000016DD
		[DebuggerStepThrough]
		internal static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000034E6 File Offset: 0x000016E6
		[DebuggerStepThrough]
		internal static KeyValuePair<TKey, TValue> WithValue<TKey, TValue>(this TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000034EF File Offset: 0x000016EF
		[DebuggerStepThrough]
		internal static KeyValuePair<T, T> ToKeyValuePairOfSameValue<T>(T value)
		{
			return new KeyValuePair<T, T>(value, value);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000034F8 File Offset: 0x000016F8
		[DebuggerStepThrough]
		internal static void LeftRotate<T>(this IList<T> list)
		{
			if (list.Count <= 1)
			{
				return;
			}
			T t = list.First<T>();
			list.RemoveAt(0);
			list.Add(t);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003524 File Offset: 0x00001724
		[DebuggerStepThrough]
		internal static IEnumerable<T> Skip<T>(this IList<T> list, int skipCount)
		{
			if (skipCount == 0)
			{
				return list;
			}
			return Util.SkipInternal<T>(list, skipCount);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003532 File Offset: 0x00001732
		private static IEnumerable<T> SkipInternal<T>(IList<T> list, int skipCount)
		{
			int num;
			for (int i = skipCount; i < list.Count; i = num + 1)
			{
				yield return list[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000354C File Offset: 0x0000174C
		internal static IList<T> SkipElementAt<T>(this IList<T> list, int index)
		{
			T[] array = new T[list.Count - 1];
			if (index > 0)
			{
				list.CopyTo(array, 0, 0, index);
			}
			if (index < list.Count - 1)
			{
				list.CopyTo(array, index + 1, index, list.Count - index - 1);
			}
			return array;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003598 File Offset: 0x00001798
		internal static T Single<T>(this IEnumerable<T> collection, string failureMessage, params string[] parameters)
		{
			IList<T> list = collection as IList<T>;
			if (list != null)
			{
				if (list.Count == 1)
				{
					return list[0];
				}
			}
			else
			{
				IReadOnlyList<T> readOnlyList = collection as IReadOnlyList<T>;
				if (readOnlyList != null)
				{
					if (readOnlyList.Count == 1)
					{
						return readOnlyList[0];
					}
				}
				else
				{
					using (IEnumerator<T> enumerator = collection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							T t = enumerator.Current;
							if (!enumerator.MoveNext())
							{
								return t;
							}
						}
					}
				}
			}
			throw new InvalidOperationException(StringUtil.FormatInvariant(failureMessage, parameters));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003630 File Offset: 0x00001830
		internal static void CopyTo<T>(this IList<T> source, T[] dest, int srcIndex, int destIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				dest[destIndex + i] = source[srcIndex + i];
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000365C File Offset: 0x0000185C
		[DebuggerStepThrough]
		internal static bool DictionaryEquals<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second, IEqualityComparer<TValue> valueComparer = null)
		{
			if (first == second)
			{
				return true;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in first)
			{
				TValue tvalue;
				if (!second.TryGetValue(keyValuePair.Key, out tvalue))
				{
					return false;
				}
				if (!valueComparer.Equals(tvalue, keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000036EC File Offset: 0x000018EC
		[DebuggerStepThrough]
		internal static bool SequenceEqual<T>(this IList<T> first, IList<T> second)
		{
			return first.SequenceEqual(second, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000036FC File Offset: 0x000018FC
		[DebuggerStepThrough]
		internal static bool SequenceEqual<T>(this IList<T> first, IList<T> second, IEqualityComparer<T> comparer)
		{
			bool? flag = Util.AreEqual<IList<T>>(first, second);
			if (flag != null)
			{
				return flag.Value;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < first.Count; i++)
			{
				if (!comparer.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000375C File Offset: 0x0000195C
		[DebuggerStepThrough]
		internal static bool SequenceEqualReadOnly<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second)
		{
			return first.SequenceEqualReadOnly(second, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000376C File Offset: 0x0000196C
		[DebuggerStepThrough]
		internal static bool SequenceEqualReadOnly<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
		{
			bool? flag = Util.AreEqual<IReadOnlyList<T>>(first, second);
			if (flag != null)
			{
				return flag.Value;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < first.Count; i++)
			{
				if (!comparer.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000037CC File Offset: 0x000019CC
		[DebuggerStepThrough]
		internal static bool SequenceEqualReadOnly<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second, Func<T, T, bool> comparer)
		{
			bool? flag = Util.AreEqual<IReadOnlyList<T>>(first, second);
			if (flag != null)
			{
				return flag.Value;
			}
			if (first.Count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < first.Count; i++)
			{
				if (!comparer(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000382C File Offset: 0x00001A2C
		[DebuggerStepThrough]
		internal static bool SubsequenceEqual<T>(this IList<T> sourceSequence, IList<T> searchSequence, int startIndex, IEqualityComparer<T> comparer)
		{
			if (searchSequence.Count > sourceSequence.Count - startIndex)
			{
				return false;
			}
			for (int i = 0; i < searchSequence.Count; i++)
			{
				if (!comparer.Equals(sourceSequence[i + startIndex], searchSequence[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003877 File Offset: 0x00001A77
		[DebuggerStepThrough]
		internal static bool SequenceContains<T>(this IList<T> sourceSequence, IList<T> searchSequence)
		{
			return sourceSequence.SequenceContains(searchSequence, EqualityComparer<T>.Default);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003885 File Offset: 0x00001A85
		[DebuggerStepThrough]
		internal static bool SequenceContains<T>(this IList<T> sourceSequence, IList<T> searchSequence, IEqualityComparer<T> comparer)
		{
			return sourceSequence.SequenceIndexOf(searchSequence, comparer) != -1;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003895 File Offset: 0x00001A95
		[DebuggerStepThrough]
		internal static int SequenceIndexOf<T>(this IList<T> sourceSequence, IList<T> searchSequence)
		{
			return sourceSequence.SequenceIndexOf(searchSequence, EqualityComparer<T>.Default);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000038A4 File Offset: 0x00001AA4
		[DebuggerStepThrough]
		internal static int SequenceIndexOf<T>(this IList<T> sourceSequence, IList<T> searchSequence, IEqualityComparer<T> comparer)
		{
			int num = sourceSequence.Count - searchSequence.Count + 1;
			for (int i = 0; i < num; i++)
			{
				bool flag = true;
				for (int j = 0; j < searchSequence.Count; j++)
				{
					if (!comparer.Equals(sourceSequence[j + i], searchSequence[j]))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003904 File Offset: 0x00001B04
		[DebuggerStepThrough]
		internal static bool SetEquals<T>(this IEnumerable<T> source, IEnumerable<T> other)
		{
			ISet<T> set = source as ISet<T>;
			if (set != null)
			{
				return set.SetEquals(other);
			}
			ISet<T> set2 = other as ISet<T>;
			if (set2 != null)
			{
				return set2.SetEquals(source);
			}
			return new HashSet<T>(source).SetEquals(other);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003944 File Offset: 0x00001B44
		[DebuggerStepThrough]
		internal static bool DictionaryEqualsReadOnly<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, IReadOnlyDictionary<TKey, TValue> other, IEqualityComparer<TValue> valueComparer = null)
		{
			bool? flag = Util.AreEqual<IReadOnlyDictionary<TKey, TValue>>(source, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (source.Count != other.Count)
			{
				return false;
			}
			valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
			{
				TValue tvalue;
				if (!other.TryGetValue(keyValuePair.Key, out tvalue) || !valueComparer.Equals(keyValuePair.Value, tvalue))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000039E4 File Offset: 0x00001BE4
		[DebuggerStepThrough]
		internal static int IndexOf<T>(this IList<T> list, T item, IEqualityComparer<T> comparer)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (comparer.Equals(list[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003A18 File Offset: 0x00001C18
		[DebuggerStepThrough]
		internal static int FindIndexOf<T>(this IReadOnlyList<T> list, T item, IEqualityComparer<T> comparer = null)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			return list.FindIndexOf((T currentItem) => comparer.Equals(currentItem, item));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003A60 File Offset: 0x00001C60
		[DebuggerStepThrough]
		internal static int FindIndexOf<T>(this IReadOnlyList<T> list, Func<T, bool> isItem)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (isItem(list[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003A90 File Offset: 0x00001C90
		[DebuggerStepThrough]
		internal static bool Replace<T>(this IList<T> list, T findItem, T replaceWith, IEqualityComparer<T> comparer)
		{
			int num = list.IndexOf(findItem, comparer);
			if (num >= 0)
			{
				list[num] = replaceWith;
				return true;
			}
			return false;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003AB5 File Offset: 0x00001CB5
		[DebuggerStepThrough]
		internal static DisposableList<T> AsDisposable<T>(this IList<T> source) where T : IDisposable
		{
			if (source != null)
			{
				return new DisposableList<T>(source);
			}
			return DisposableList<T>.Empty;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003AC6 File Offset: 0x00001CC6
		internal static IEnumerable<T> AsEnumerable<T>(T item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003AD8 File Offset: 0x00001CD8
		[DebuggerStepThrough]
		internal static IList<T> Evaluate<T>(this IEnumerable<T> source)
		{
			IList<T> list = source as IList<T>;
			return list ?? source.ToList<T>();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003AF8 File Offset: 0x00001CF8
		[DebuggerStepThrough]
		internal static IList<T> Evaluate<T>(this IEnumerable<T> source, int maxResultCount)
		{
			IList<T> list = source as IList<T>;
			if (list != null && list.Count <= maxResultCount)
			{
				return list;
			}
			return source.Take(maxResultCount).ToList<T>();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003B28 File Offset: 0x00001D28
		[DebuggerStepThrough]
		internal static IReadOnlyList<T> EvaluateAsReadOnlyList<T>(this IEnumerable<T> source)
		{
			IReadOnlyList<T> readOnlyList = source as IReadOnlyList<T>;
			return readOnlyList ?? source.ToList<T>();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003B48 File Offset: 0x00001D48
		[DebuggerStepThrough]
		internal static IReadOnlyList<T> EvaluateAsReadOnlyList<T>(this IEnumerable<T> source, int maxResultCount)
		{
			IReadOnlyList<T> readOnlyList = source as IReadOnlyList<T>;
			if (readOnlyList != null && readOnlyList.Count <= maxResultCount)
			{
				return readOnlyList;
			}
			return source.Take(maxResultCount).ToList<T>();
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003B76 File Offset: 0x00001D76
		[Conditional("DEBUG")]
		internal static void DebugForceEvaluation<T>(ref IEnumerable<T> collection)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003B78 File Offset: 0x00001D78
		[DebuggerStepThrough]
		internal static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
		{
			return source.Concat(new T[] { item });
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003B8E File Offset: 0x00001D8E
		[DebuggerStepThrough]
		internal static IEnumerable<T> Concat<T>(this T head, IEnumerable<T> tail)
		{
			return new T[] { head }.Concat(tail);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003BA4 File Offset: 0x00001DA4
		[DebuggerStepThrough]
		internal static IEnumerable<T> ConcatIfNotNull<T>(this IEnumerable<T> source, T item)
		{
			if (item == null)
			{
				return source;
			}
			if (source == null)
			{
				return Util.AsEnumerable<T>(item);
			}
			return source.Concat(item);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003BC1 File Offset: 0x00001DC1
		[DebuggerStepThrough]
		internal static IEnumerable<T> ConcatWithNullCheck<T>(this IEnumerable<T> source, IEnumerable<T> other)
		{
			if (source == null)
			{
				return other;
			}
			if (other == null)
			{
				return source;
			}
			return source.Concat(other);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003BD4 File Offset: 0x00001DD4
		[DebuggerStepThrough]
		internal static void RemoveLast<T>(this IList<T> list)
		{
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[DebuggerStepThrough]
		internal static T PopLast<T>(this IList<T> list)
		{
			int num = list.Count - 1;
			T t = list[num];
			list.RemoveAt(num);
			return t;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003C08 File Offset: 0x00001E08
		[DebuggerStepThrough]
		internal static bool AllDistinct<T>(this IEnumerable<T> source)
		{
			return source.AllDistinct(EqualityComparer<T>.Default);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003C18 File Offset: 0x00001E18
		[DebuggerStepThrough]
		internal static bool AllDistinct<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			HashSet<T> hashSet = new HashSet<T>(comparer);
			foreach (T t in source)
			{
				if (!hashSet.Add(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003C70 File Offset: 0x00001E70
		[DebuggerStepThrough]
		internal static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				return Enumerable.Empty<T>();
			}
			return source;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003C7C File Offset: 0x00001E7C
		[DebuggerStepThrough]
		internal static IList<T> NullIfEmpty<T>(this IList<T> source)
		{
			if (source == null || source.Count == 0)
			{
				return null;
			}
			return source;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003C8C File Offset: 0x00001E8C
		[DebuggerStepThrough]
		internal static IReadOnlyList<T> NullIfEmptyReadOnly<T>(this IReadOnlyList<T> source)
		{
			if (source == null || source.Count == 0)
			{
				return null;
			}
			return source;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003C9C File Offset: 0x00001E9C
		[DebuggerStepThrough]
		internal static IEnumerable<T> WhereNonNull<T>(this IEnumerable<T> source) where T : class
		{
			return source.Where(Util.SingleTypeGenericInstances<T>.NonNullPredicateInstance);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003CA9 File Offset: 0x00001EA9
		[DebuggerStepThrough]
		internal static IEnumerable<string> WhereNonNullNorWhiteSpace(this IEnumerable<string> source)
		{
			return source.Where(Util.StringDelegates.NonNullNorWhiteSpacePredicateInstance);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003CB6 File Offset: 0x00001EB6
		internal static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
		{
			return source.SelectMany(Util.IdentityDelegate<IEnumerable<T>>());
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003CC3 File Offset: 0x00001EC3
		[DebuggerStepThrough]
		internal static IEnumerable<IGrouping<TKey, TValue>> GroupBy<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return items.GroupBy(Util.KeyDelegate<TKey, TValue>(), Util.ValueDelegate<TKey, TValue>());
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003CD8 File Offset: 0x00001ED8
		internal static IReadOnlyCollection<T> AsIReadOnlyCollection<T>(this ICollection<T> collection)
		{
			if (collection == null)
			{
				return Util.EmptyReadOnlyCollection<T>();
			}
			IReadOnlyCollection<T> readOnlyCollection = collection as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection;
			}
			return new Util.ReadOnlyCollectionWrapper<T>(collection);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003D00 File Offset: 0x00001F00
		[DebuggerStepThrough]
		internal static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> items)
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
					return Util.EmptyReadOnlyCollection<T>();
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
			return Util.EmptyReadOnlyCollection<T>();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003D84 File Offset: 0x00001F84
		[DebuggerStepThrough]
		internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
		{
			ICollection<T> collection = items as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count > 0)
				{
					T[] array = new T[collection.Count];
					collection.CopyTo(array, 0);
					return Array.AsReadOnly<T>(array);
				}
			}
			else if (items != null)
			{
				List<T> list = items.ToList<T>();
				if (list.Count > 0)
				{
					return list.AsReadOnly();
				}
			}
			return Util.EmptyReadOnlyCollection<T>();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[DebuggerStepThrough]
		internal static IReadOnlyList<T> AsReadOnlyList<T>(this IEnumerable<T> items)
		{
			if (items == null)
			{
				return Util.EmptyReadOnlyCollection<T>();
			}
			IReadOnlyList<T> readOnlyList = items as IReadOnlyList<T>;
			if (readOnlyList != null)
			{
				if (readOnlyList.Count <= 0)
				{
					return Util.EmptyReadOnlyCollection<T>();
				}
				return readOnlyList;
			}
			else
			{
				ICollection<T> collection = items as ICollection<T>;
				if (collection != null && collection.Count == 0)
				{
					return Util.EmptyReadOnlyCollection<T>();
				}
				return items.ToList<T>();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003E31 File Offset: 0x00002031
		internal static IEnumerable<TKey> GetKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
			{
				yield return keyValuePair.Key;
			}
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003E41 File Offset: 0x00002041
		[DebuggerStepThrough]
		internal static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, TValue>(), Util.ValueDelegate<TKey, TValue>());
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003E53 File Offset: 0x00002053
		[DebuggerStepThrough]
		internal static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> comparer)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, TValue>(), Util.ValueDelegate<TKey, TValue>(), comparer);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003E66 File Offset: 0x00002066
		[DebuggerStepThrough]
		internal static ReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> items)
		{
			return items.ToDictionary<TKey, TValue>().AsReadOnlyDictionary<TKey, TValue>();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003E74 File Offset: 0x00002074
		[DebuggerStepThrough]
		internal static List<ReadOnlyCollection<T>> ToListWithReadOnlyValueCollection<T>(this IList<List<T>> items)
		{
			List<ReadOnlyCollection<T>> list = new List<ReadOnlyCollection<T>>(items.Count);
			foreach (List<T> list2 in items)
			{
				list.Add(list2.AsReadOnlyCollection<T>());
			}
			return list;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003ED0 File Offset: 0x000020D0
		[DebuggerStepThrough]
		internal static Dictionary<TKey, ReadOnlyCollection<TValue>> ToDictionaryWithReadOnlyValueCollection<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, List<TValue>>> items)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, List<TValue>>(), (KeyValuePair<TKey, List<TValue>> k) => k.Value.AsReadOnlyCollection<TValue>());
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003EFC File Offset: 0x000020FC
		[DebuggerStepThrough]
		internal static Dictionary<TKey, ReadOnlyCollection<TValue>> ToDictionaryWithReadOnlyValueCollection<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, List<TValue>>> items, IEqualityComparer<TKey> comparer)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, List<TValue>>(), (KeyValuePair<TKey, List<TValue>> k) => k.Value.AsReadOnlyCollection<TValue>(), comparer);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003F29 File Offset: 0x00002129
		[DebuggerStepThrough]
		internal static Dictionary<TKey, IReadOnlyList<TValue>> ToDictionaryWithReadOnlyValueList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, List<TValue>>> items)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, List<TValue>>(), (KeyValuePair<TKey, List<TValue>> k) => k.Value.AsReadOnlyList<TValue>());
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003F55 File Offset: 0x00002155
		[DebuggerStepThrough]
		internal static Dictionary<TKey, IReadOnlyList<TValue>> ToDictionaryWithReadOnlyValueList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, List<TValue>>> items, IEqualityComparer<TKey> comparer)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, List<TValue>>(), (KeyValuePair<TKey, List<TValue>> k) => k.Value.AsReadOnlyList<TValue>(), comparer);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003F82 File Offset: 0x00002182
		[DebuggerStepThrough]
		internal static Dictionary<TKey, IReadOnlyList<TValue>> ToDictionaryWithReadOnlyList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, ConcurrentBag<TValue>>> items)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, ConcurrentBag<TValue>>(), (KeyValuePair<TKey, ConcurrentBag<TValue>> k) => k.Value.AsReadOnlyList<TValue>());
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003FB0 File Offset: 0x000021B0
		[DebuggerStepThrough]
		internal static Dictionary<TKey, ReadOnlyDictionary<TValueKey, ReadOnlyCollection<TValue>>> ToDictionaryWithValueAsReadOnlyDictionaryWithReadOnlyValueCollection<TKey, TValueKey, TValue>(this IEnumerable<KeyValuePair<TKey, Dictionary<TValueKey, List<TValue>>>> items, IEqualityComparer<TKey> comparer)
		{
			return items.ToDictionary((KeyValuePair<TKey, Dictionary<TValueKey, List<TValue>>> i) => i.Key, (KeyValuePair<TKey, Dictionary<TValueKey, List<TValue>>> i) => i.Value.ToDictionaryWithReadOnlyValueCollection<TValueKey, TValue>().AsReadOnlyDictionary<TValueKey, ReadOnlyCollection<TValue>>(), comparer);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004002 File Offset: 0x00002202
		[DebuggerStepThrough]
		internal static Dictionary<TKey, ReadOnlySet<TValue>> ToDictionaryWithReadOnlyValueSet<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, HashSet<TValue>>> items)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, HashSet<TValue>>(), (KeyValuePair<TKey, HashSet<TValue>> k) => k.Value.AsReadOnlySet(null));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000402E File Offset: 0x0000222E
		[DebuggerStepThrough]
		internal static Dictionary<TKey, ReadOnlySet<TValue>> ToDictionaryWithReadOnlyValueSet<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, HashSet<TValue>>> items, IEqualityComparer<TKey> keyComparer)
		{
			return items.ToDictionary(Util.KeyDelegate<TKey, HashSet<TValue>>(), (KeyValuePair<TKey, HashSet<TValue>> k) => k.Value.AsReadOnlySet(null), keyComparer);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000405C File Offset: 0x0000225C
		[DebuggerStepThrough]
		internal static Dictionary<TKey, List<TValue>> ToDictionaryWithValueList<TKey, TValue>(this IEnumerable<TValue> items, Func<TValue, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
		{
			Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>(comparer);
			foreach (TValue tvalue in items)
			{
				dictionary.Add(keySelector(tvalue), tvalue, 4);
			}
			return dictionary;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000040B4 File Offset: 0x000022B4
		[DebuggerStepThrough]
		internal static Queue<T> ToQueue<T>(this IList<T> list)
		{
			int count = list.Count;
			Queue<T> queue = new Queue<T>(count);
			for (int i = 0; i < count; i++)
			{
				queue.Enqueue(list[i]);
			}
			return queue;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000040EC File Offset: 0x000022EC
		[DebuggerStepThrough]
		internal static Dictionary<TKey, TValue> OverrideDictionary<TKey, TValue>(this IDictionary<TKey, TValue> baseDictionary, IDictionary<TKey, TValue> overrideDictionary, IEqualityComparer<TKey> comparer = null)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(baseDictionary.Count + overrideDictionary.Count, comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in baseDictionary)
			{
				dictionary[keyValuePair.Key] = keyValuePair.Value;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair2 in overrideDictionary)
			{
				dictionary[keyValuePair2.Key] = keyValuePair2.Value;
			}
			return dictionary;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000419C File Offset: 0x0000239C
		[DebuggerStepThrough]
		internal static void AddToLazy(ref HashSet<string> hashSet, string value)
		{
			if (hashSet == null)
			{
				hashSet = new HashSet<string>();
			}
			hashSet.Add(value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000041B4 File Offset: 0x000023B4
		[DebuggerStepThrough]
		internal static void Add<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value, int newListCapacity = 4)
		{
			List<TValue> list;
			if (!dictionary.TryGetValue(key, out list))
			{
				list = new List<TValue>(newListCapacity);
				dictionary.Add(key, list);
			}
			list.Add(value);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000041E4 File Offset: 0x000023E4
		[DebuggerStepThrough]
		internal static void Add<TKey, TValue>(this IDictionary<TKey, IList<TValue>> dictionary, TKey key, TValue value, int newListCapacity = 4)
		{
			IList<TValue> list;
			if (!dictionary.TryGetValue(key, out list))
			{
				list = new List<TValue>(newListCapacity);
				dictionary.Add(key, list);
			}
			list.Add(value);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004214 File Offset: 0x00002414
		internal static void Add<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue value, IEqualityComparer<TValue> comparer = null)
		{
			HashSet<TValue> hashSet;
			if (!dictionary.TryGetValue(key, out hashSet))
			{
				comparer = comparer ?? EqualityComparer<TValue>.Default;
				hashSet = new HashSet<TValue>(comparer);
				dictionary.Add(key, hashSet);
			}
			hashSet.Add(value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004250 File Offset: 0x00002450
		[DebuggerStepThrough]
		internal static void AddOrAppend<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, IEnumerable<TValue> values)
		{
			List<TValue> list;
			if (!dictionary.TryGetValue(key, out list))
			{
				List<TValue> list2 = values as List<TValue>;
				if (list2 == null)
				{
					list2 = values.ToList<TValue>();
				}
				dictionary.Add(key, list2);
				return;
			}
			list.AddRange(values);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004289 File Offset: 0x00002489
		[DebuggerStepThrough]
		internal static Func<T, T> IdentityDelegate<T>()
		{
			return Util.SingleTypeGenericInstances<T>.IdentityDelegateInstance;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004290 File Offset: 0x00002490
		[DebuggerStepThrough]
		internal static Func<TChild, TParent> IdentityDelegate<TChild, TParent>() where TChild : TParent
		{
			return Util.InheritedTypeGenericInstances<TChild, TParent>.IdentityDelegateInstance;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004297 File Offset: 0x00002497
		[DebuggerStepThrough]
		internal static Func<KeyValuePair<TKey, TValue>, TValue> ValueDelegate<TKey, TValue>()
		{
			return Util.TwoTypeGenericInstances<TKey, TValue>.KeyValuePairToValueDelegateInstance;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000429E File Offset: 0x0000249E
		[DebuggerStepThrough]
		internal static Func<KeyValuePair<TKey, TValue>, TKey> KeyDelegate<TKey, TValue>()
		{
			return Util.TwoTypeGenericInstances<TKey, TValue>.KeyValuePairToKeyDelegateInstance;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000042A5 File Offset: 0x000024A5
		[DebuggerStepThrough]
		internal static Func<IGrouping<TKey, TElement>, TKey> IGroupingToKeyDelegate<TKey, TElement>()
		{
			return Util.TwoTypeGenericInstances<TKey, TElement>.IGroupingToKeyDelegateInstance;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000042AC File Offset: 0x000024AC
		[DebuggerStepThrough]
		internal static ReadOnlyCollection<T> EmptyReadOnlyCollection<T>()
		{
			return Util.SingleTypeGenericInstances<T>.ReadOnlyCollectionInstance;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000042B3 File Offset: 0x000024B3
		[DebuggerStepThrough]
		internal static ILookup<TKey, TValue> EmptyLookup<TKey, TValue>()
		{
			return Util.TwoTypeGenericInstances<TKey, TValue>.EmptyLookupInstance;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000042BA File Offset: 0x000024BA
		[DebuggerStepThrough]
		internal static ReadOnlyDictionary<TKey, TValue> EmptyReadOnlyDictionary<TKey, TValue>()
		{
			return Util.TwoTypeGenericInstances<TKey, TValue>.ReadOnlyDictionaryInstance;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000042C1 File Offset: 0x000024C1
		[DebuggerStepThrough]
		internal static ReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null || dictionary.Count == 0)
			{
				return Util.TwoTypeGenericInstances<TKey, TValue>.ReadOnlyDictionaryInstance;
			}
			return (dictionary as ReadOnlyDictionary<TKey, TValue>) ?? new ReadOnlyDictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000042E4 File Offset: 0x000024E4
		[DebuggerStepThrough]
		internal static IDictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
		{
			if (dictionary == null)
			{
				return null;
			}
			IDictionary<TKey, TValue> dictionary2 = dictionary as IDictionary<TKey, TValue>;
			return dictionary2 ?? dictionary.ToDictionary(Util.KeyDelegate<TKey, TValue>(), Util.ValueDelegate<TKey, TValue>());
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004314 File Offset: 0x00002514
		[DebuggerStepThrough]
		internal static int AddOrIncrement<TKey>(this IDictionary<TKey, int> dict, TKey key)
		{
			int num;
			if (dict.TryGetValue(key, out num))
			{
				return dict[key] = num + 1;
			}
			return dict[key] = 1;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004348 File Offset: 0x00002548
		[DebuggerStepThrough]
		internal static IList<T> AsList<T>(this IEnumerable<T> items)
		{
			if (items == null)
			{
				return null;
			}
			IList<T> list = items as IList<T>;
			return list ?? items.ToList<T>();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000436C File Offset: 0x0000256C
		[DebuggerStepThrough]
		internal static IEnumerable<T> ConcatAll<T>(params IEnumerable<T>[] collections)
		{
			IEnumerable<T> enumerable = Util.EmptyReadOnlyCollection<T>();
			foreach (IEnumerable<T> enumerable2 in collections)
			{
				enumerable = enumerable.Concat(enumerable2);
			}
			return enumerable;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000439C File Offset: 0x0000259C
		[DebuggerStepThrough]
		internal static bool ContainsAll<T>(this IReadOnlyList<T> collection, IReadOnlyList<T> elements)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				T t = elements[i];
				if (!collection.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000043CE File Offset: 0x000025CE
		internal static IEnumerable<IReadOnlyList<T>> GenerateSequences<T>(this IReadOnlyList<IReadOnlyList<T>> elements, int maxSequenceCount = -1)
		{
			return elements.GenerateSequences(maxSequenceCount);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000043D7 File Offset: 0x000025D7
		internal static IEnumerable<IReadOnlyList<TElement>> GenerateSequences<TList, TElement>(this IReadOnlyList<TList> elements, int maxSequenceCount = -1) where TList : IReadOnlyList<TElement>
		{
			if (elements.Count == 0)
			{
				yield break;
			}
			for (int i = 0; i < elements.Count; i++)
			{
				TList tlist = elements[i];
				if (tlist.Count == 0)
				{
					yield break;
				}
			}
			int resultsCount = 0;
			int[] indices = new int[elements.Count];
			for (;;)
			{
				yield return Util.GetElements<TList, TElement>(elements, indices);
				int num = resultsCount + 1;
				resultsCount = num;
				if (num == maxSequenceCount)
				{
					break;
				}
				if (!Util.AddOne<TList, TElement>(elements, indices))
				{
					goto Block_5;
				}
			}
			yield break;
			Block_5:
			yield break;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000043F0 File Offset: 0x000025F0
		private static IReadOnlyList<TElement> GetElements<TList, TElement>(IReadOnlyList<TList> elements, int[] indices) where TList : IReadOnlyList<TElement>
		{
			TElement[] array = new TElement[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				TElement[] array2 = array;
				int num = i;
				TList tlist = elements[i];
				array2[num] = tlist[indices[i]];
			}
			return array;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004434 File Offset: 0x00002634
		private static bool AddOne<TList, TElement>(IReadOnlyList<TList> elements, int[] indices) where TList : IReadOnlyList<TElement>
		{
			for (int i = 0; i < indices.Length; i++)
			{
				int num = i;
				int num2 = indices[i] + 1;
				TList tlist = elements[i];
				indices[num] = num2 % tlist.Count;
				if (indices[i] != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004478 File Offset: 0x00002678
		[DebuggerStepThrough]
		internal static bool HasAnyFlag(this Enum variable, Enum flag)
		{
			if (variable.GetType() != flag.GetType())
			{
				throw new ArgumentException("Argument_EnumTypeDoesNotMatch");
			}
			ulong num = Convert.ToUInt64(variable, CultureInfo.InvariantCulture);
			ulong num2 = Convert.ToUInt64(flag, CultureInfo.InvariantCulture);
			return (num & num2) > 0UL;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000044C0 File Offset: 0x000026C0
		[DebuggerStepThrough]
		internal static ReadOnlySet<T> AsReadOnlySet<T>(this IEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
		{
			ISet<T> set = sequence as ISet<T>;
			if (set != null)
			{
				return set.AsReadOnlySet(comparer);
			}
			return sequence.ToReadOnlySet(comparer);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000044E8 File Offset: 0x000026E8
		[DebuggerStepThrough]
		internal static ReadOnlySet<T> AsReadOnlySet<T>(this ISet<T> set, IEqualityComparer<T> comparer = null)
		{
			if (set == null)
			{
				return ReadOnlySet<T>.Empty;
			}
			ReadOnlySet<T> readOnlySet = set as ReadOnlySet<T>;
			if (readOnlySet != null)
			{
				return readOnlySet;
			}
			int count = set.Count;
			if (count == 0)
			{
				return ReadOnlySet<T>.Empty;
			}
			if (count == 1)
			{
				if (comparer == null)
				{
					HashSet<T> hashSet = set as HashSet<T>;
					if (hashSet != null)
					{
						comparer = hashSet.Comparer;
					}
				}
				if (comparer != null)
				{
					return new ReadOnlySetSingleValue<T>(set.First<T>(), comparer);
				}
			}
			return new ReadOnlySetGeneric<T>(set);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000454C File Offset: 0x0000274C
		[DebuggerStepThrough]
		internal static ReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
		{
			if (items == null)
			{
				return ReadOnlySet<T>.Empty;
			}
			ICollection<T> collection = items as ICollection<T>;
			if (collection != null)
			{
				int count = collection.Count;
				if (count == 0)
				{
					return ReadOnlySet<T>.Empty;
				}
				if (count == 1)
				{
					return new ReadOnlySetSingleValue<T>(collection.First<T>(), comparer);
				}
			}
			return new ReadOnlySetGeneric<T>(new HashSet<T>(items, comparer));
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004599 File Offset: 0x00002799
		internal static HashSet<T> ToHashSet<T>(this IEnumerable<T> items, IEqualityComparer<T> comparer = null)
		{
			return new HashSet<T>(items, comparer);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000045A4 File Offset: 0x000027A4
		[DebuggerStepThrough]
		internal static IReadOnlyDictionary<T, U> Union<T, U>(this IReadOnlyDictionary<T, U> first, IReadOnlyDictionary<T, U> second, IEqualityComparer<T> keyComparer = null, bool ignoreDuplicates = false)
		{
			if (first == null || first.Count == 0)
			{
				return second;
			}
			if (second == null || second.Count == 0)
			{
				return first;
			}
			Dictionary<T, U> dictionary = new Dictionary<T, U>(first.Count + second.Count, keyComparer ?? EqualityComparer<T>.Default);
			foreach (KeyValuePair<T, U> keyValuePair in first)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<T, U> keyValuePair2 in second)
			{
				if (!ignoreDuplicates || !dictionary.ContainsKey(keyValuePair2.Key))
				{
					dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004688 File Offset: 0x00002888
		[DebuggerStepThrough]
		internal static IDictionary<T, List<U>> MergeWith<T, U>(this IDictionary<T, List<U>> first, IDictionary<T, List<U>> second, IEqualityComparer<T> keyComparer = null)
		{
			if (first == null || first.Count == 0)
			{
				return second;
			}
			if (second == null || second.Count == 0)
			{
				return first;
			}
			foreach (KeyValuePair<T, List<U>> keyValuePair in second)
			{
				List<U> list;
				if (!first.TryGetValue(keyValuePair.Key, out list))
				{
					first.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					list.AddRange(keyValuePair.Value);
				}
			}
			return first;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004718 File Offset: 0x00002918
		[DebuggerStepThrough]
		internal static ISet<T> IntersectAll<T>(this IEnumerable<ISet<T>> sets)
		{
			ISet<T> set = null;
			List<ISet<T>> list = null;
			foreach (ISet<T> set2 in sets)
			{
				int count = set2.Count;
				if (count == 0)
				{
					return ReadOnlySet<T>.Empty;
				}
				if (set == null)
				{
					set = set2;
				}
				else if (count >= set.Count)
				{
					if (list == null)
					{
						list = new List<ISet<T>>();
					}
					list.Add(set2);
				}
				else
				{
					if (list == null)
					{
						list = new List<ISet<T>>();
					}
					list.Add(set);
					set = set2;
				}
			}
			if (list == null)
			{
				return set ?? ReadOnlySet<T>.Empty;
			}
			int count2 = list.Count;
			HashSet<T> hashSet = new HashSet<T>();
			using (IEnumerator<T> enumerator2 = set.GetEnumerator())
			{
				IL_00D9:
				while (enumerator2.MoveNext())
				{
					T t = enumerator2.Current;
					for (int num = 0; num != count2; num++)
					{
						if (!list[num].Contains(t))
						{
							goto IL_00D9;
						}
					}
					hashSet.Add(t);
				}
			}
			return hashSet;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004838 File Offset: 0x00002A38
		[DebuggerStepThrough]
		internal static IEnumerable<T> Intersect<T>(ISet<T> arg0, ISet<T> arg1)
		{
			if (arg0 == arg1)
			{
				return arg0;
			}
			return Util.IntersectCore<T>(arg0, arg1);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004847 File Offset: 0x00002A47
		private static IEnumerable<T> IntersectCore<T>(ISet<T> arg0, ISet<T> arg1)
		{
			if (arg0.Count > arg1.Count)
			{
				ISet<T> set = arg0;
				arg0 = arg1;
				arg1 = set;
			}
			foreach (T t in arg0)
			{
				if (arg1.Contains(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004860 File Offset: 0x00002A60
		[DebuggerStepThrough]
		internal static ISet<T> IntersectOrDefault<T>(ISet<T> arg0, ISet<T> arg1)
		{
			if (arg0.Count > arg1.Count)
			{
				ISet<T> set = arg0;
				arg0 = arg1;
				arg1 = set;
			}
			HashSet<T> hashSet = null;
			foreach (T t in arg0)
			{
				if (arg1.Contains(t))
				{
					if (hashSet == null)
					{
						hashSet = new HashSet<T>();
					}
					hashSet.Add(t);
				}
			}
			return hashSet;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000048D4 File Offset: 0x00002AD4
		[DebuggerStepThrough]
		internal static IEnumerable<T> Except<T>(this IEnumerable<T> first, ISet<T> second)
		{
			if (second.Count == 0)
			{
				return first;
			}
			return first.ExceptCore(second);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000048E7 File Offset: 0x00002AE7
		private static IEnumerable<T> ExceptCore<T>(this IEnumerable<T> first, ISet<T> second)
		{
			foreach (T t in first)
			{
				if (!second.Contains(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000048FE File Offset: 0x00002AFE
		[DebuggerStepThrough]
		internal static IEnumerable<T> Except<T>(this IEnumerable<T> sequence, T item)
		{
			return sequence.Except(item, EqualityComparer<T>.Default);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000490C File Offset: 0x00002B0C
		internal static IEnumerable<T> Except<T>(this IEnumerable<T> sequence, T item, IEqualityComparer<T> comparer)
		{
			foreach (T t in sequence)
			{
				if (!comparer.Equals(t, item))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000492A File Offset: 0x00002B2A
		internal static bool IsSupersetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !second.Except(first).Any<T>();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000493B File Offset: 0x00002B3B
		internal static bool IsSubsetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
		{
			return !first.Except(second).Any<T>();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000494C File Offset: 0x00002B4C
		[DebuggerStepThrough]
		internal static string FormatInvariant(this Guid value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000495C File Offset: 0x00002B5C
		[DebuggerStepThrough]
		internal static void TrimExcessIfPossible<T>(this ISet<T> set)
		{
			HashSet<T> hashSet = set as HashSet<T>;
			if (hashSet == null)
			{
				return;
			}
			hashSet.TrimExcess();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000497C File Offset: 0x00002B7C
		[DebuggerStepThrough]
		internal static void OptimizeDictionaryValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TValue> valueComparer) where TValue : class
		{
			ReferenceEqualizer<TValue> referenceEqualizer = new ReferenceEqualizer<TValue>(valueComparer);
			dictionary.OptimizeDictionaryValues(referenceEqualizer);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004998 File Offset: 0x00002B98
		[DebuggerStepThrough]
		internal static void OptimizeDictionaryValues<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, ReferenceEqualizer<TValue> equalizer) where TValue : class
		{
			if (dictionary.Count < 2)
			{
				return;
			}
			foreach (TKey tkey in dictionary.Keys.ToList<TKey>())
			{
				TValue tvalue = dictionary[tkey];
				TValue tvalue2;
				if (!equalizer.TryAdd(tvalue, out tvalue2))
				{
					dictionary[tkey] = tvalue2;
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004A10 File Offset: 0x00002C10
		[DebuggerStepThrough]
		internal static void OptimizeDictionaryValuesSets<TKey, TValue>(this IDictionary<TKey, ISet<TValue>> dictionary, IEqualityComparer<TValue> valueComparer) where TValue : class
		{
			List<KeyValuePair<TKey, ISet<TValue>>> list = null;
			foreach (KeyValuePair<TKey, ISet<TValue>> keyValuePair in dictionary)
			{
				ISet<TValue> set = keyValuePair.Value;
				if (set.Count == 1)
				{
					if (list == null)
					{
						list = new List<KeyValuePair<TKey, ISet<TValue>>>(dictionary.Count);
					}
					list.Add(Util.ToKeyValuePair<TKey, ISet<TValue>>(keyValuePair.Key, new ReadOnlySetSingleValue<TValue>(keyValuePair.Value.First<TValue>(), valueComparer)));
				}
				else
				{
					CopyOnWriteSet<TValue> copyOnWriteSet = set as CopyOnWriteSet<TValue>;
					if (copyOnWriteSet != null)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<TKey, ISet<TValue>>>(dictionary.Count);
						}
						set = copyOnWriteSet.InnerSet;
						list.Add(Util.ToKeyValuePair<TKey, ISet<TValue>>(keyValuePair.Key, set));
					}
					set.TrimExcessIfPossible<TValue>();
				}
			}
			if (list == null)
			{
				return;
			}
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				KeyValuePair<TKey, ISet<TValue>> keyValuePair2 = list[i];
				dictionary[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004B1C File Offset: 0x00002D1C
		[DebuggerStepThrough]
		internal static bool IsEmpty<T>(this IReadOnlyCollection<T> collection)
		{
			return collection.Count == 0;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004B27 File Offset: 0x00002D27
		[DebuggerStepThrough]
		internal static bool IsEmptyCollection<T>(this ICollection<T> collection)
		{
			return collection.Count == 0;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004B32 File Offset: 0x00002D32
		[DebuggerStepThrough]
		internal static bool IsNotEmpty<T>(this IReadOnlyCollection<T> collection)
		{
			return collection.Count > 0;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004B3D File Offset: 0x00002D3D
		[DebuggerStepThrough]
		internal static bool IsNotEmptyCollection<T>(this ICollection<T> collection)
		{
			return collection.Count > 0;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004B48 File Offset: 0x00002D48
		[DebuggerStepThrough]
		internal static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004B58 File Offset: 0x00002D58
		[DebuggerStepThrough]
		internal static bool IsNullOrEmptyCollection<T>(this ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004B68 File Offset: 0x00002D68
		internal static List<T> EnsureList<T>(ref List<T> list)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			return list;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004B77 File Offset: 0x00002D77
		internal static Dictionary<T, S> EnsureDictionary<T, S>(ref Dictionary<T, S> dictionary, IEqualityComparer<T> comparer = null)
		{
			if (dictionary == null)
			{
				dictionary = new Dictionary<T, S>(comparer);
			}
			return dictionary;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004B88 File Offset: 0x00002D88
		private static List<T> RewriteCoreReadOnly<T>(IReadOnlyList<T> list, Func<T, T> rewriteItem) where T : class
		{
			if (list == null)
			{
				return null;
			}
			List<T> list2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				T t = rewriteItem(list[i]);
				if (t == list[i])
				{
					if (list2 != null)
					{
						list2.Add(t);
					}
				}
				else
				{
					if (list2 == null)
					{
						list2 = new List<T>(list.Count);
						for (int j = 0; j < i; j++)
						{
							list2.Add(list[j]);
						}
					}
					list2.Add(t);
				}
			}
			return list2;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004C0C File Offset: 0x00002E0C
		private static List<T> RewriteCore<T>(IList<T> list, Func<T, T> rewriteItem) where T : class
		{
			if (list == null)
			{
				return null;
			}
			List<T> list2 = null;
			for (int i = 0; i < list.Count; i++)
			{
				T t = rewriteItem(list[i]);
				if (t == list[i])
				{
					if (list2 != null)
					{
						list2.Add(t);
					}
				}
				else
				{
					if (list2 == null)
					{
						list2 = new List<T>(list.Count);
						for (int j = 0; j < i; j++)
						{
							list2.Add(list[j]);
						}
					}
					list2.Add(t);
				}
			}
			return list2;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004C8F File Offset: 0x00002E8F
		internal static List<T> Rewrite<T>(this List<T> list, Func<T, T> rewriteItem) where T : class
		{
			return Util.RewriteCoreReadOnly<T>(list, rewriteItem) ?? list;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal static IReadOnlyList<T> Rewrite<T>(this IReadOnlyList<T> list, Func<T, T> rewriteItem) where T : class
		{
			IReadOnlyList<T> readOnlyList = Util.RewriteCoreReadOnly<T>(list, rewriteItem);
			return readOnlyList ?? list;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004CBC File Offset: 0x00002EBC
		internal static IList<T> Rewrite<T>(this IList<T> list, Func<T, T> rewriteItem) where T : class
		{
			IList<T> list2 = Util.RewriteCore<T>(list, rewriteItem);
			return list2 ?? list;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004CD8 File Offset: 0x00002ED8
		internal static List<TResult> RemapReadOnly<TInput, TResult>(IReadOnlyList<TInput> inputList, Func<TInput, TResult> rewriteItem)
		{
			if (inputList == null)
			{
				return null;
			}
			List<TResult> list = new List<TResult>(inputList.Count);
			for (int i = 0; i < inputList.Count; i++)
			{
				list.Add(rewriteItem(inputList[i]));
			}
			return list;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004D1B File Offset: 0x00002F1B
		[DebuggerStepThrough]
		internal static IList<KeyValuePair<string, TValue>> OrderByKeyLength<TValue>(this IEnumerable<KeyValuePair<string, TValue>> source)
		{
			return source.OrderBy(Util.KeyDelegate<string, TValue>(), Util.StringLengthComparer.Instance).Evaluate<KeyValuePair<string, TValue>>();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004D32 File Offset: 0x00002F32
		[DebuggerStepThrough]
		internal static IEnumerable<T> OrderBy<T>(this IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			if (comparer != null)
			{
				return collection.OrderBy(Util.IdentityDelegate<T>(), comparer);
			}
			return collection.OrderBy(Util.IdentityDelegate<T>());
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004D4F File Offset: 0x00002F4F
		internal static IIndexedValue<T> WithIndex<T>(this T value, int index)
		{
			return new Util.IndexedValue<T>(index, value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004D58 File Offset: 0x00002F58
		internal static IEnumerable<IIndexedValue<T>> WithIndexes<T>(this IEnumerable<T> collection)
		{
			int index = 0;
			foreach (T t in collection)
			{
				T t2 = t;
				int num = index;
				index = num + 1;
				yield return t2.WithIndex(num);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004D68 File Offset: 0x00002F68
		[DebuggerStepThrough]
		internal static IEnumerable<T> ValuesOrderedByIndex<T>(this IEnumerable<IIndexedValue<T>> collection)
		{
			return collection.OrderBy(Util.IIndexedValueInstances<T>.IndexSelector).Select(Util.IIndexedValueInstances<T>.ValueSelector);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004D7F File Offset: 0x00002F7F
		[DebuggerStepThrough]
		internal static bool IsSortedAscending<T>(this IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			return collection.IsSorted(comparer, true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004D89 File Offset: 0x00002F89
		[DebuggerStepThrough]
		internal static bool IsSortedDescending<T>(this IEnumerable<T> collection, IComparer<T> comparer = null)
		{
			return collection.IsSorted(comparer, false);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004D93 File Offset: 0x00002F93
		[DebuggerStepThrough]
		internal static bool IsSortedAscending<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector, IComparer<TKey> comparer = null)
		{
			return collection.IsSorted(keySelector, comparer, true);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004D9E File Offset: 0x00002F9E
		[DebuggerStepThrough]
		internal static bool IsSortedDescending<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector, IComparer<TKey> comparer = null)
		{
			return collection.IsSorted(keySelector, comparer, false);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004DAC File Offset: 0x00002FAC
		[DebuggerStepThrough]
		internal static Dictionary<T, int> GetElementCounts<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
		{
			Dictionary<T, int> dictionary = new Dictionary<T, int>(comparer);
			foreach (T t in source)
			{
				int num;
				if (!dictionary.TryGetValue(t, out num))
				{
					dictionary.Add(t, 1);
				}
				else
				{
					dictionary[t] = num + 1;
				}
			}
			return dictionary;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004E14 File Offset: 0x00003014
		internal static IEnumerable<T> GetCombinations<T>(this Func<T, T, T> combine, params IList<T>[] values)
		{
			return Util.GetCombinationsCore<T>(combine, 0, values);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004E20 File Offset: 0x00003020
		internal static void ExecuteOnCollection<T>(this IEnumerable<T> collection, Action<T> action)
		{
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				action(t);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004E6C File Offset: 0x0000306C
		private static IEnumerable<T> GetCombinationsCore<T>(Func<T, T, T> combine, int currentIndex, IList<IList<T>> values)
		{
			if (currentIndex + 1 < values.Count)
			{
				IList<T> currentList = values[currentIndex];
				foreach (T tailCombination in Util.GetCombinationsCore<T>(combine, currentIndex + 1, values))
				{
					int num;
					for (int i = 0; i < currentList.Count; i = num + 1)
					{
						yield return combine(currentList[i], tailCombination);
						num = i;
					}
					tailCombination = default(T);
				}
				IEnumerator<T> enumerator = null;
				currentList = null;
			}
			else
			{
				IList<T> currentList = values[values.Count - 1];
				int num;
				for (int i = 0; i < currentList.Count; i = num + 1)
				{
					yield return currentList[i];
					num = i;
				}
				currentList = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004E8C File Offset: 0x0000308C
		private static bool IsSorted<T>(this IEnumerable<T> collection, IComparer<T> comparer, bool isAscending)
		{
			IEnumerator<T> enumerator = collection.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return true;
			}
			if (comparer == null)
			{
				comparer = Comparer<T>.Default;
			}
			T t = enumerator.Current;
			while (enumerator.MoveNext())
			{
				T t2 = enumerator.Current;
				int num = comparer.Compare(t, t2);
				if (isAscending)
				{
					if (num > 0)
					{
						return false;
					}
				}
				else if (num < 0)
				{
					return false;
				}
				t = t2;
			}
			return true;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004EE8 File Offset: 0x000030E8
		private static bool IsSorted<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector, IComparer<TKey> comparer, bool isAscending)
		{
			IEnumerator<T> enumerator = collection.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return true;
			}
			if (comparer == null)
			{
				comparer = Comparer<TKey>.Default;
			}
			TKey tkey = keySelector(enumerator.Current);
			while (enumerator.MoveNext())
			{
				T t = enumerator.Current;
				TKey tkey2 = keySelector(t);
				int num = comparer.Compare(tkey, tkey2);
				if (isAscending)
				{
					if (num > 0)
					{
						return false;
					}
				}
				else if (num < 0)
				{
					return false;
				}
				tkey = tkey2;
			}
			return true;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004F4F File Offset: 0x0000314F
		internal static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T t in source)
			{
				if (!predicate(t))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004F68 File Offset: 0x00003168
		internal static TValue InterlockedAddOrGet<TKey, TValue>(ref ReadOnlyDictionary<TKey, TValue> dictionary, TKey key, Lazy<TValue> value, IEqualityComparer<TKey> keyComparer = null)
		{
			keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			TValue tvalue;
			for (;;)
			{
				ReadOnlyDictionary<TKey, TValue> readOnlyDictionary = dictionary;
				if (readOnlyDictionary.TryGetValue(key, out tvalue))
				{
					break;
				}
				Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(readOnlyDictionary.Count + 1, keyComparer);
				dictionary2.Add(key, value.Value);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in readOnlyDictionary)
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				ReadOnlyDictionary<TKey, TValue> readOnlyDictionary2 = dictionary2.AsReadOnlyDictionary<TKey, TValue>();
				if (Interlocked.CompareExchange<ReadOnlyDictionary<TKey, TValue>>(ref dictionary, readOnlyDictionary2, readOnlyDictionary) == readOnlyDictionary)
				{
					goto Block_4;
				}
			}
			return tvalue;
			Block_4:
			return value.Value;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005014 File Offset: 0x00003214
		internal static void InterlockedAddOrUpdate<TKey, TValue>(ref ReadOnlyDictionary<TKey, TValue> dictionary, IList<KeyValuePair<TKey, TValue>> keyValuePairs, IEqualityComparer<TKey> keyComparer = null)
		{
			keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			ReadOnlyDictionary<TKey, TValue> readOnlyDictionary;
			ReadOnlyDictionary<TKey, TValue> readOnlyDictionary2;
			do
			{
				readOnlyDictionary = dictionary;
				Dictionary<TKey, TValue> dictionary2 = new Dictionary<TKey, TValue>(readOnlyDictionary.Count + keyValuePairs.Count, keyComparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in readOnlyDictionary)
				{
					dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
				}
				int count = keyValuePairs.Count;
				for (int i = 0; i < count; i++)
				{
					KeyValuePair<TKey, TValue> keyValuePair2 = keyValuePairs[i];
					dictionary2[keyValuePair2.Key] = keyValuePair2.Value;
				}
				readOnlyDictionary2 = dictionary2.AsReadOnlyDictionary<TKey, TValue>();
			}
			while (Interlocked.CompareExchange<ReadOnlyDictionary<TKey, TValue>>(ref dictionary, readOnlyDictionary2, readOnlyDictionary) != readOnlyDictionary);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000050DC File Offset: 0x000032DC
		internal static bool IsInstanceOfGenericType(this object instance, Type genericType)
		{
			return instance != null && instance.GetType().IsDerivedFromGenericType(genericType);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000050EF File Offset: 0x000032EF
		internal static bool IsDerivedFromGenericType(this Type type, Type genericType)
		{
			while (type != null)
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005120 File Offset: 0x00003320
		internal static bool TryIncrementOdometer(this int[] odometer, IReadOnlyList<int> odometerLimits)
		{
			if (odometer.Length != odometerLimits.Count)
			{
				return false;
			}
			int num = 0;
			int count = odometerLimits.Count;
			while (num < count && odometer[num] >= odometerLimits[num] - 1)
			{
				num++;
			}
			if (num >= count)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				odometer[i] = 0;
			}
			odometer[num]++;
			return true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000517E File Offset: 0x0000337E
		internal static IEnumerable<IList<T>> GenerateOdometerCombinations<T>(this IEnumerable<IEnumerable<T>> source)
		{
			ReadOnlyCollection<ReadOnlyCollection<T>> sourceAsList = source.Select((IEnumerable<T> collection) => collection.ToReadOnlyCollection<T>()).ToReadOnlyCollection<ReadOnlyCollection<T>>();
			if (sourceAsList.Count == 0)
			{
				yield break;
			}
			List<int> odometerLimits = sourceAsList.Select((ReadOnlyCollection<T> list) => list.Count).ToList<int>();
			if (odometerLimits.Any((int limit) => limit == 0))
			{
				yield break;
			}
			int[] odometer = new int[sourceAsList.Count];
			do
			{
				yield return odometer.Select((int odometerValue, int index) => sourceAsList[index][odometerValue]).Evaluate<T>();
			}
			while (odometer.TryIncrementOdometer(odometerLimits));
			yield break;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000518E File Offset: 0x0000338E
		internal static IEnumerable<T> DistinctGivenAttribute<T, S>(this IEnumerable<T> source, Func<T, S> attributeSelector, IEqualityComparer<S> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<S>.Default;
			HashSet<S> alreadySeen = new HashSet<S>(comparer);
			foreach (T t in source)
			{
				if (alreadySeen.Add(attributeSelector(t)))
				{
					yield return t;
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000051AC File Offset: 0x000033AC
		internal static void TraceTime(Action action, ITracer tracer, string message)
		{
			action();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000051B4 File Offset: 0x000033B4
		internal static T TraceTime<T>(Func<T> func, ITracer tracer, string message)
		{
			return func();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000051BC File Offset: 0x000033BC
		internal static T Max<T>(T x, T y) where T : IComparable
		{
			if (x.CompareTo(y) < 0)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000051D7 File Offset: 0x000033D7
		internal static void TraceMemoryUsage(Action action, ITracer tracer, string message)
		{
			action();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000051DF File Offset: 0x000033DF
		internal static T TraceMemoryUsage<T>(Func<T> func, ITracer tracer, string message)
		{
			return func();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000051E7 File Offset: 0x000033E7
		internal static void SanitizedTraceMemoryUsage(Action action, ITracer tracer, string message)
		{
			action();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000051EF File Offset: 0x000033EF
		internal static T SanitizedTraceMemoryUsage<T>(Func<T> func, ITracer tracer, string message)
		{
			return func();
		}

		// Token: 0x0400003F RID: 63
		private static Predicate<Exception> _fatalExceptionCondition = (Exception ex) => (ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException;

		// Token: 0x02000094 RID: 148
		private static class TwoTypeGenericInstances<TKey, TValue>
		{
			// Token: 0x04000143 RID: 323
			internal static readonly ReadOnlyDictionary<TKey, TValue> ReadOnlyDictionaryInstance = new ReadOnlyDictionary<TKey, TValue>(new Dictionary<TKey, TValue>(0));

			// Token: 0x04000144 RID: 324
			internal static readonly ILookup<TKey, TValue> EmptyLookupInstance = Util.SingleTypeGenericInstances<TValue>.ReadOnlyCollectionInstance.ToLookup((TValue key) => default(TKey));

			// Token: 0x04000145 RID: 325
			internal static readonly Func<KeyValuePair<TKey, TValue>, TKey> KeyValuePairToKeyDelegateInstance = (KeyValuePair<TKey, TValue> kvp) => kvp.Key;

			// Token: 0x04000146 RID: 326
			internal static readonly Func<KeyValuePair<TKey, TValue>, TValue> KeyValuePairToValueDelegateInstance = (KeyValuePair<TKey, TValue> kvp) => kvp.Value;

			// Token: 0x04000147 RID: 327
			internal static readonly Func<IGrouping<TKey, TValue>, TKey> IGroupingToKeyDelegateInstance = (IGrouping<TKey, TValue> g) => g.Key;
		}

		// Token: 0x02000095 RID: 149
		private static class SingleTypeGenericInstances<T>
		{
			// Token: 0x04000148 RID: 328
			internal static readonly ReadOnlyCollection<T> ReadOnlyCollectionInstance = Array.AsReadOnly<T>(new T[0]);

			// Token: 0x04000149 RID: 329
			internal static readonly Func<T, T> IdentityDelegateInstance = (T x) => x;

			// Token: 0x0400014A RID: 330
			internal static readonly Func<T, bool> NonNullPredicateInstance = (T x) => x != null;
		}

		// Token: 0x02000096 RID: 150
		private static class StringDelegates
		{
			// Token: 0x0400014B RID: 331
			internal static readonly Func<string, bool> NonNullNorWhiteSpacePredicateInstance = (string x) => !string.IsNullOrWhiteSpace(x);
		}

		// Token: 0x02000097 RID: 151
		private static class InheritedTypeGenericInstances<TChild, TParent> where TChild : TParent
		{
			// Token: 0x0400014C RID: 332
			internal static readonly Func<TChild, TParent> IdentityDelegateInstance = (TChild x) => (TParent)((object)x);
		}

		// Token: 0x02000098 RID: 152
		private sealed class StringLengthComparer : IComparer<string>
		{
			// Token: 0x06000512 RID: 1298 RVA: 0x0000D771 File Offset: 0x0000B971
			private StringLengthComparer()
			{
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0000D77C File Offset: 0x0000B97C
			public int Compare(string x, string y)
			{
				return x.Length.CompareTo(y.Length);
			}

			// Token: 0x0400014D RID: 333
			internal static readonly Util.StringLengthComparer Instance = new Util.StringLengthComparer();
		}

		// Token: 0x02000099 RID: 153
		private sealed class IndexedValue<T> : Tuple<int, T>, IIndexedValue<T>
		{
			// Token: 0x06000515 RID: 1301 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
			internal IndexedValue(int index, T value)
				: base(index, value)
			{
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000D7B3 File Offset: 0x0000B9B3
			int IIndexedValue<T>.Index
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000D7BB File Offset: 0x0000B9BB
			T IIndexedValue<T>.Value
			{
				get
				{
					return base.Item2;
				}
			}
		}

		// Token: 0x0200009A RID: 154
		private static class IIndexedValueInstances<T>
		{
			// Token: 0x0400014E RID: 334
			internal static readonly Func<IIndexedValue<T>, int> IndexSelector = (IIndexedValue<T> iv) => iv.Index;

			// Token: 0x0400014F RID: 335
			internal static readonly Func<IIndexedValue<T>, T> ValueSelector = (IIndexedValue<T> iv) => iv.Value;
		}

		// Token: 0x0200009B RID: 155
		private sealed class ReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x06000519 RID: 1305 RVA: 0x0000D7EF File Offset: 0x0000B9EF
			internal ReadOnlyCollectionWrapper(ICollection<T> coll)
			{
				this._coll = coll;
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000D7FE File Offset: 0x0000B9FE
			public int Count
			{
				get
				{
					return this._coll.Count;
				}
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0000D80B File Offset: 0x0000BA0B
			public IEnumerator<T> GetEnumerator()
			{
				return this._coll.GetEnumerator();
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x0000D818 File Offset: 0x0000BA18
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._coll.GetEnumerator();
			}

			// Token: 0x04000150 RID: 336
			private readonly ICollection<T> _coll;
		}
	}
}
