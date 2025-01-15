using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Internal
{
	// Token: 0x020001AF RID: 431
	public static class IEnumerableExtensions
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x0000F754 File Offset: 0x0000D954
		public static T[] MinK<T>(this IEnumerable<T> list, int count, IComparer<T> comparer)
		{
			return list.MaxK(count, new IEnumerableExtensions.NegativeComparer<T>(comparer));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0000F764 File Offset: 0x0000D964
		public static T[] MaxK<T>(this IEnumerable<T> list, int count, IComparer<T> comparer)
		{
			if (count == 0)
			{
				return EmptyArray<T>.Instance;
			}
			PriorityQueue<T> priorityQueue = new PriorityQueue<T>(Math.Min(count, 16), comparer);
			foreach (T t in list)
			{
				priorityQueue.Insert(t);
				if (priorityQueue.Count > count)
				{
					priorityQueue.Remove();
				}
			}
			T[] array = new T[priorityQueue.Count];
			for (int i = array.Length; i > 0; i--)
			{
				array[i - 1] = priorityQueue.Remove();
			}
			return array;
		}

		// Token: 0x020001B0 RID: 432
		private class NegativeComparer<T> : IComparer<T>
		{
			// Token: 0x06000834 RID: 2100 RVA: 0x0000F804 File Offset: 0x0000DA04
			public NegativeComparer(IComparer<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x0000F813 File Offset: 0x0000DA13
			public int Compare(T x, T y)
			{
				return -this.comparer.Compare(x, y);
			}

			// Token: 0x040004AD RID: 1197
			private IComparer<T> comparer;
		}
	}
}
