using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200003E RID: 62
	public sealed class BoundedHashSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000848C File Offset: 0x0000668C
		public BoundedHashSet(int capacity)
		{
			this._capacity = capacity;
			this._set = new HashSet<T>();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000084A6 File Offset: 0x000066A6
		public BoundedHashSet(IEnumerable<T> collection, int compacity)
		{
			this._capacity = compacity;
			this._set = new HashSet<T>(collection);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000084C1 File Offset: 0x000066C1
		public BoundedHashSet(IEqualityComparer<T> comparer, int compacity)
		{
			this._capacity = compacity;
			this._set = new HashSet<T>(comparer);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000084DC File Offset: 0x000066DC
		public int Count
		{
			get
			{
				return this._set.Count;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000084E9 File Offset: 0x000066E9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000084EC File Offset: 0x000066EC
		public bool IsFull
		{
			get
			{
				return this.Count >= this._capacity;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000084FF File Offset: 0x000066FF
		public bool Add(T item)
		{
			return !this.IsFull && this._set.Add(item);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008517 File Offset: 0x00006717
		public void Clear()
		{
			this._set.Clear();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008524 File Offset: 0x00006724
		public bool Contains(T item)
		{
			return this._set.Contains(item);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008532 File Offset: 0x00006732
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._set.CopyTo(array, arrayIndex);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008541 File Offset: 0x00006741
		public void ExceptWith(IEnumerable<T> other)
		{
			this._set.ExceptWith(other);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000854F File Offset: 0x0000674F
		public IEnumerator<T> GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008561 File Offset: 0x00006761
		public void IntersectWith(IEnumerable<T> other)
		{
			this._set.IntersectWith(other);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000856F File Offset: 0x0000676F
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSubsetOf(other);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000857D File Offset: 0x0000677D
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsProperSupersetOf(other);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000858B File Offset: 0x0000678B
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this._set.IsSubsetOf(other);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008599 File Offset: 0x00006799
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this._set.IsSupersetOf(other);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000085A7 File Offset: 0x000067A7
		public bool Overlaps(IEnumerable<T> other)
		{
			return this._set.Overlaps(other);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000085B5 File Offset: 0x000067B5
		public bool Remove(T item)
		{
			return this._set.Remove(item);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000085C3 File Offset: 0x000067C3
		public bool SetEquals(IEnumerable<T> other)
		{
			return this._set.SetEquals(other);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000085D1 File Offset: 0x000067D1
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			this._set.SymmetricExceptWith(other);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000085E0 File Offset: 0x000067E0
		public void UnionWith(IEnumerable<T> other)
		{
			if (this.IsFull)
			{
				return;
			}
			foreach (T t in other)
			{
				this.Add(t);
				if (this.IsFull)
				{
					break;
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000863C File Offset: 0x0000683C
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008646 File Offset: 0x00006846
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._set.GetEnumerator();
		}

		// Token: 0x0400009B RID: 155
		private readonly int _capacity;

		// Token: 0x0400009C RID: 156
		private readonly HashSet<T> _set;
	}
}
