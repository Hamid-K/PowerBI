using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000010 RID: 16
	public static class CompareUtil
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003428 File Offset: 0x00001628
		public static bool CheckReferenceAndTypeEquality<TThis, TOther>(TThis @this, TOther other, out bool areEqual, out TThis otherTyped) where TThis : class, TOther where TOther : class
		{
			bool? flag = CompareUtil.AreEqual<TThis, TOther>(@this, other);
			if (flag != null)
			{
				otherTyped = default(TThis);
				areEqual = flag.Value;
				return true;
			}
			otherTyped = other as TThis;
			if (otherTyped == null)
			{
				areEqual = false;
				return true;
			}
			areEqual = false;
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003484 File Offset: 0x00001684
		public static bool? AreEqual<TFirst, TSecond>(TFirst first, TSecond second) where TFirst : class where TSecond : class
		{
			if (first == second)
			{
				return new bool?(true);
			}
			if (first == null || second == null)
			{
				return new bool?(false);
			}
			return null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000034C6 File Offset: 0x000016C6
		public static bool SequenceEqual<T>(this List<T> first, List<T> second)
		{
			return first.SequenceEqualReadOnly(second, EqualityComparer<T>.Default);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000034D4 File Offset: 0x000016D4
		public static bool SequenceEqual<T>(this List<T> first, List<T> second, IEqualityComparer<T> comparer)
		{
			return first.SequenceEqualReadOnly(second, comparer);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034DE File Offset: 0x000016DE
		public static bool SequenceEqualReadOnly<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second)
		{
			return first.SequenceEqualReadOnly(second, EqualityComparer<T>.Default);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000034EC File Offset: 0x000016EC
		public static bool SequenceEqualReadOnly<T>(this IReadOnlyList<T> first, IReadOnlyList<T> second, IEqualityComparer<T> comparer)
		{
			bool? flag = CompareUtil.AreEqual<IReadOnlyList<T>, IReadOnlyList<T>>(first, second);
			if (flag != null)
			{
				return flag.Value;
			}
			int count = first.Count;
			if (count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!comparer.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003549 File Offset: 0x00001749
		public static bool SequenceEqual<T>(this List<List<T>> first, List<List<T>> second)
		{
			return first.SequenceEqualReadOnly(second, EqualityComparer<T>.Default);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003557 File Offset: 0x00001757
		public static bool SequenceEqual<T>(this List<List<T>> first, List<List<T>> second, IEqualityComparer<T> comparer)
		{
			return first.SequenceEqualReadOnly(second, comparer);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003561 File Offset: 0x00001761
		public static bool SequenceEqualReadOnly<T>(this IReadOnlyList<IReadOnlyList<T>> first, IReadOnlyList<IReadOnlyList<T>> second)
		{
			return first.SequenceEqualReadOnly(second, EqualityComparer<T>.Default);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003570 File Offset: 0x00001770
		public static bool SequenceEqualReadOnly<T>(this IReadOnlyList<IReadOnlyList<T>> first, IReadOnlyList<IReadOnlyList<T>> second, IEqualityComparer<T> comparer)
		{
			bool? flag = CompareUtil.AreEqual<IReadOnlyList<IReadOnlyList<T>>, IReadOnlyList<IReadOnlyList<T>>>(first, second);
			if (flag != null)
			{
				return flag.Value;
			}
			int count = first.Count;
			if (count != second.Count)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!first[i].SequenceEqualReadOnly(second[i], comparer))
				{
					return false;
				}
			}
			return true;
		}
	}
}
