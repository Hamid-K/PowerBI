using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AE RID: 174
	[DebuggerDisplay("Count = {count}")]
	internal struct ArraySet<T> : IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00018644 File Offset: 0x00016844
		public ArraySet(int capacity)
		{
			this.items = new T[capacity];
			this.count = 0;
			this.version = 0;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00018660 File Offset: 0x00016860
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000125 RID: 293
		public T this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00018678 File Offset: 0x00016878
		public bool Add(T item, Func<T, T, bool> equalityComparer)
		{
			if (equalityComparer != null && this.Contains(item, equalityComparer))
			{
				return false;
			}
			int num = this.count;
			this.count = num + 1;
			int num2 = num;
			if (this.items == null || num2 == this.items.Length)
			{
				Array.Resize<T>(ref this.items, Math.Min(Math.Max(num2, 16), 1073741823) * 2);
			}
			this.items[num2] = item;
			this.version++;
			return true;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000186F4 File Offset: 0x000168F4
		public bool Contains(T item, Func<T, T, bool> equalityComparer)
		{
			return 0 <= this.IndexOf(item, equalityComparer);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00018704 File Offset: 0x00016904
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.count; i = num)
			{
				yield return this.items[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018718 File Offset: 0x00016918
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018720 File Offset: 0x00016920
		public int IndexOf(T item, Func<T, T, bool> comparer)
		{
			return this.IndexOf<T>(item, new Func<T, T>(ArraySet<T>.IdentitySelect), comparer);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00018738 File Offset: 0x00016938
		public int IndexOf<K>(K item, Func<T, K> select, Func<K, K, bool> comparer)
		{
			T[] array = this.items;
			if (array != null)
			{
				int num = this.count;
				for (int i = 0; i < num; i++)
				{
					if (comparer(item, select(array[i])))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001877C File Offset: 0x0001697C
		public T Remove(T item, Func<T, T, bool> equalityComparer)
		{
			int num = this.IndexOf(item, equalityComparer);
			if (0 <= num)
			{
				item = this.items[num];
				this.RemoveAt(num);
				return item;
			}
			return default(T);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000187B8 File Offset: 0x000169B8
		public void RemoveAt(int index)
		{
			T[] array = this.items;
			int num = this.count - 1;
			this.count = num;
			int num2 = num;
			array[index] = array[num2];
			array[num2] = default(T);
			if (num2 == 0 && 256 <= array.Length)
			{
				this.items = null;
			}
			else if (256 < array.Length && num2 < array.Length / 4)
			{
				Array.Resize<T>(ref this.items, array.Length / 2);
			}
			this.version++;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00018844 File Offset: 0x00016A44
		public void Sort<K>(Func<T, K> selector, Func<K, K, int> comparer)
		{
			if (this.items != null)
			{
				ArraySet<T>.SelectorComparer<K> selectorComparer;
				selectorComparer.Selector = selector;
				selectorComparer.Comparer = comparer;
				Array.Sort<T>(this.items, 0, this.count, selectorComparer);
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018881 File Offset: 0x00016A81
		public void TrimToSize()
		{
			Array.Resize<T>(ref this.items, this.count);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00002DF3 File Offset: 0x00000FF3
		private static T IdentitySelect(T arg)
		{
			return arg;
		}

		// Token: 0x04000281 RID: 641
		private T[] items;

		// Token: 0x04000282 RID: 642
		private int count;

		// Token: 0x04000283 RID: 643
		private int version;

		// Token: 0x02000189 RID: 393
		private struct SelectorComparer<K> : IComparer<T>
		{
			// Token: 0x06000E19 RID: 3609 RVA: 0x00030BA0 File Offset: 0x0002EDA0
			int IComparer<T>.Compare(T x, T y)
			{
				return this.Comparer(this.Selector(x), this.Selector(y));
			}

			// Token: 0x0400075E RID: 1886
			internal Func<T, K> Selector;

			// Token: 0x0400075F RID: 1887
			internal Func<K, K, int> Comparer;
		}
	}
}
