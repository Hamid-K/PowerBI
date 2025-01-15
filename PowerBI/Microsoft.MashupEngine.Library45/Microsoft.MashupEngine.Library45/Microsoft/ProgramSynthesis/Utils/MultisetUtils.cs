using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004C3 RID: 1219
	public static class MultisetUtils
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x00051EA4 File Offset: 0x000500A4
		public static int GetAndIncrement<TKey>(this IDictionary<TKey, int> dict, TKey key)
		{
			int num;
			if (dict.TryGetValue(key, out num))
			{
				num++;
			}
			else
			{
				num = 1;
			}
			dict[key] = num;
			return num;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00051ED0 File Offset: 0x000500D0
		public static Dictionary<T, int> ToMultiset<T>(this IEnumerable<T> seq)
		{
			Dictionary<T, int> counter = new Dictionary<T, int>();
			seq.ForEach(delegate(T elem)
			{
				counter.GetAndIncrement(elem);
			});
			return counter;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00051F08 File Offset: 0x00050108
		public static Dictionary<T, int> MultisetUnion<T>(this IReadOnlyDictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne, EqualityComparer<T> keyComparer = null)
		{
			HashSet<T> hashSet = new HashSet<T>(thisOne.Keys, keyComparer ?? EqualityComparer<T>.Default);
			hashSet.UnionWith(thatOne.Keys);
			Dictionary<T, int> dictionary = new Dictionary<T, int>();
			foreach (T t in hashSet)
			{
				int num;
				thisOne.TryGetValue(t, out num);
				int num2;
				thatOne.TryGetValue(t, out num2);
				dictionary[t] = num + num2;
			}
			return dictionary;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00051F98 File Offset: 0x00050198
		public static void MultisetUnionWith<T>(this Dictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne)
		{
			foreach (KeyValuePair<T, int> keyValuePair in thatOne)
			{
				T t;
				int num;
				keyValuePair.Deconstruct(out t, out num);
				T t2 = t;
				int num2 = num;
				int num3;
				thisOne.TryGetValue(t2, out num3);
				thisOne[t2] = num3 + num2;
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00051FFC File Offset: 0x000501FC
		public static Dictionary<T, int> MultisetDifference<T>(this IReadOnlyDictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne)
		{
			Dictionary<T, int> dictionary = new Dictionary<T, int>();
			foreach (KeyValuePair<T, int> keyValuePair in thisOne)
			{
				T t;
				int num;
				keyValuePair.Deconstruct(out t, out num);
				T t2 = t;
				int num2 = num;
				int num3;
				thatOne.TryGetValue(t2, out num3);
				int num4 = num2 - num3;
				if (num4 > 0)
				{
					dictionary[t2] = num4;
				}
			}
			return dictionary;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00052070 File Offset: 0x00050270
		public static bool MultisetDifferenceIsNonEmpty<T>(this IReadOnlyDictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne)
		{
			foreach (KeyValuePair<T, int> keyValuePair in thisOne)
			{
				T t;
				int num;
				keyValuePair.Deconstruct(out t, out num);
				T t2 = t;
				int num2 = num;
				int num3;
				thatOne.TryGetValue(t2, out num3);
				if (num2 - num3 > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000520D8 File Offset: 0x000502D8
		public static Dictionary<T, int> MultisetSymmetricDifference<T>(this IReadOnlyDictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne)
		{
			Dictionary<T, int> dictionary = new Dictionary<T, int>();
			foreach (KeyValuePair<T, int> keyValuePair in thisOne)
			{
				T t;
				int num;
				keyValuePair.Deconstruct(out t, out num);
				T t2 = t;
				int num2 = num;
				int num3;
				thatOne.TryGetValue(t2, out num3);
				int num4 = Math.Abs(num2 - num3);
				if (num4 > 0)
				{
					dictionary[t2] = num4;
				}
			}
			return dictionary;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00052150 File Offset: 0x00050350
		public static Dictionary<T, int> MultisetSignedDifference<T>(this IReadOnlyDictionary<T, int> thisOne, IReadOnlyDictionary<T, int> thatOne, EqualityComparer<T> keyComparer = null)
		{
			HashSet<T> hashSet = new HashSet<T>(thisOne.Keys, keyComparer ?? EqualityComparer<T>.Default);
			hashSet.UnionWith(thatOne.Keys);
			Dictionary<T, int> dictionary = new Dictionary<T, int>();
			foreach (T t in hashSet)
			{
				int num;
				thisOne.TryGetValue(t, out num);
				int num2;
				thatOne.TryGetValue(t, out num2);
				if (num != num2)
				{
					dictionary[t] = num - num2;
				}
			}
			return dictionary;
		}
	}
}
