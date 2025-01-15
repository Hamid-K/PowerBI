using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav
{
	// Token: 0x02000013 RID: 19
	internal static class Hashing
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000520E File Offset: 0x0000340E
		internal static uint CombineHash(uint u1, uint u2)
		{
			return ((u1 << 7) | (u1 >> 25)) ^ u2;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000521A File Offset: 0x0000341A
		internal static int CombineHash(int n1, int n2)
		{
			return (int)Hashing.CombineHash((uint)n1, (uint)n2);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005223 File Offset: 0x00003423
		internal static int CombineHash(int n1, int n2, int n3)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005232 File Offset: 0x00003432
		internal static int CombineHash(int n1, int n2, int n3, int n4)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005247 File Offset: 0x00003447
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005263 File Offset: 0x00003463
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005286 File Offset: 0x00003486
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000052B0 File Offset: 0x000034B0
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000052E1 File Offset: 0x000034E1
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005319 File Offset: 0x00003519
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9), (uint)n10);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005358 File Offset: 0x00003558
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10, int n11)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9), (uint)n10), (uint)n11);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000053AC File Offset: 0x000035AC
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10, int n11, int n12)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9), (uint)n10), (uint)n11), (uint)n12);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005404 File Offset: 0x00003604
		internal static int CombineHash(int n1, int n2, int n3, int n4, int n5, int n6, int n7, int n8, int n9, int n10, int n11, int n12, int n13)
		{
			return (int)Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash(Hashing.CombineHash((uint)n1, (uint)n2), (uint)n3), (uint)n4), (uint)n5), (uint)n6), (uint)n7), (uint)n8), (uint)n9), (uint)n10), (uint)n11), (uint)n12), (uint)n13);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005464 File Offset: 0x00003664
		internal static int CombineHash<T>(IList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			comparer = comparer ?? EqualityComparer<T>.Default;
			for (int i = 0; i < values.Count; i++)
			{
				T t = values[i];
				int num2 = 0;
				if (t != null)
				{
					num2 = comparer.GetHashCode(t);
				}
				num = ((num << 5) + num) ^ num2;
			}
			return num;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000054C1 File Offset: 0x000036C1
		internal static int CombineHashReadOnly<T>(IReadOnlyList<T> values, IEqualityComparer<T> comparer = null)
		{
			comparer = comparer ?? EqualityComparer<T>.Default;
			return Hashing.CombineHashReadOnly<T>(values, new Func<T, int>(comparer.GetHashCode));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000054E4 File Offset: 0x000036E4
		internal static int CombineHashReadOnly<T>(IReadOnlyList<T> values, Func<T, int> getHashCode)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = values.Count;
			for (int i = 0; i < values.Count; i++)
			{
				T t = values[i];
				int num2 = 0;
				if (t != null)
				{
					num2 = getHashCode(t);
				}
				num = ((num << 5) + num) ^ num2;
			}
			return num;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005538 File Offset: 0x00003738
		internal static int CombineHash<T>(IEnumerable<T> values, int count, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			int num = count;
			comparer = comparer ?? EqualityComparer<T>.Default;
			foreach (T t in values)
			{
				int num2 = 0;
				if (t != null)
				{
					num2 = comparer.GetHashCode(t);
				}
				num = ((num << 5) + num) ^ num2;
			}
			return num;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000055AC File Offset: 0x000037AC
		internal static int CombineHash<T>(ISet<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			comparer = comparer ?? EqualityComparer<T>.Default;
			int num = values.Count;
			foreach (T t in values)
			{
				num ^= Hashing.CombineHashCommutative(Hashing.GetHashCode<T>(t, comparer));
			}
			return num;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000561C File Offset: 0x0000381C
		internal static int CombineHashUnordered<T>(IList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			comparer = comparer ?? EqualityComparer<T>.Default;
			int num = values.Count;
			foreach (T t in values)
			{
				num ^= Hashing.CombineHashCommutative(Hashing.GetHashCode<T>(t, comparer));
			}
			return num;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000568C File Offset: 0x0000388C
		internal static int CombineHashUnorderedReadOnly<T>(IReadOnlyList<T> values, IEqualityComparer<T> comparer = null)
		{
			if (values == null)
			{
				return -48879;
			}
			comparer = comparer ?? EqualityComparer<T>.Default;
			int num = values.Count;
			foreach (T t in values)
			{
				num ^= Hashing.CombineHashCommutative(Hashing.GetHashCode<T>(t, comparer));
			}
			return num;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000056FC File Offset: 0x000038FC
		internal static int CombineHash<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null)
		{
			if (dictionary == null)
			{
				return -48879;
			}
			keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			int num = dictionary.Count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				TKey tkey;
				TValue tvalue;
				keyValuePair.Deconstruct(out tkey, out tvalue);
				TKey tkey2 = tkey;
				TValue tvalue2 = tvalue;
				num ^= Hashing.CombineHashCommutative(Hashing.CombineHash(Hashing.GetHashCode<TKey>(tkey2, keyComparer), Hashing.GetHashCode<TValue>(tvalue2, valueComparer)));
			}
			return num;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005790 File Offset: 0x00003990
		internal static int CombineHashReadOnly<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null)
		{
			if (dictionary == null)
			{
				return -48879;
			}
			keyComparer = keyComparer ?? EqualityComparer<TKey>.Default;
			valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
			int num = dictionary.Count;
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				TKey tkey;
				TValue tvalue;
				keyValuePair.Deconstruct(out tkey, out tvalue);
				TKey tkey2 = tkey;
				TValue tvalue2 = tvalue;
				num ^= Hashing.CombineHashCommutative(Hashing.CombineHash(Hashing.GetHashCode<TKey>(tkey2, keyComparer), Hashing.GetHashCode<TValue>(tvalue2, valueComparer)));
			}
			return num;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005824 File Offset: 0x00003A24
		internal static int GetHashCode<T>(T value, IEqualityComparer<T> comparer = null)
		{
			if (!typeof(T).IsValueType && value == null)
			{
				return -48879;
			}
			if (comparer == null)
			{
				return value.GetHashCode();
			}
			return comparer.GetHashCode(value);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000585D File Offset: 0x00003A5D
		private static int CombineHashCommutative(int elementHashcode)
		{
			return elementHashcode ^ int.MaxValue;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005868 File Offset: 0x00003A68
		public static long ComputeLongHash(string strVal)
		{
			if (string.IsNullOrEmpty(strVal))
			{
				return 0L;
			}
			ulong num = 3074457345618258791UL;
			for (int i = 0; i < strVal.Length; i++)
			{
				num += (ulong)strVal[i];
				num *= 3074457345618258799UL;
			}
			return (long)num;
		}

		// Token: 0x04000040 RID: 64
		internal const int NullHashCode = -48879;
	}
}
