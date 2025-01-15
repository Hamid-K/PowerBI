using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NLog.Internal
{
	// Token: 0x02000140 RID: 320
	internal struct SingleItemOptimizedHashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00027AF4 File Offset: 0x00025CF4
		private IEqualityComparer<T> Comparer
		{
			get
			{
				return this._comparer ?? EqualityComparer<T>.Default;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00027B08 File Offset: 0x00025D08
		public int Count
		{
			get
			{
				HashSet<T> hashset = this._hashset;
				if (hashset != null)
				{
					return hashset.Count;
				}
				if (!EqualityComparer<T>.Default.Equals(this._singleItem, default(T)))
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00027B43 File Offset: 0x00025D43
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00027B48 File Offset: 0x00025D48
		public SingleItemOptimizedHashSet(T singleItem, SingleItemOptimizedHashSet<T> existing, IEqualityComparer<T> comparer = null)
		{
			this._comparer = existing._comparer ?? comparer ?? EqualityComparer<T>.Default;
			if (existing._hashset != null)
			{
				this._hashset = new HashSet<T>(existing._hashset, this._comparer);
				this._hashset.Add(singleItem);
				this._singleItem = default(T);
				return;
			}
			if (existing.Count == 1)
			{
				this._hashset = new HashSet<T>(this._comparer);
				this._hashset.Add(existing._singleItem);
				this._hashset.Add(singleItem);
				this._singleItem = default(T);
				return;
			}
			this._hashset = null;
			this._singleItem = singleItem;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00027C00 File Offset: 0x00025E00
		public void Add(T item)
		{
			if (this._hashset != null)
			{
				this._hashset.Add(item);
				return;
			}
			HashSet<T> hashSet = new HashSet<T>(this.Comparer);
			if (this.Count != 0)
			{
				hashSet.Add(this._singleItem);
			}
			hashSet.Add(item);
			this._hashset = hashSet;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00027C53 File Offset: 0x00025E53
		public void Clear()
		{
			if (this._hashset != null)
			{
				this._hashset.Clear();
				return;
			}
			this._hashset = new HashSet<T>(this.Comparer);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00027C7A File Offset: 0x00025E7A
		public bool Contains(T item)
		{
			if (this._hashset != null)
			{
				return this._hashset.Contains(item);
			}
			return this.Count == 1 && this.Comparer.Equals(this._singleItem, item);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00027CAE File Offset: 0x00025EAE
		public bool Remove(T item)
		{
			if (this._hashset != null)
			{
				return this._hashset.Remove(item);
			}
			this._hashset = new HashSet<T>(this.Comparer);
			return this.Comparer.Equals(this._singleItem, item);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00027CE8 File Offset: 0x00025EE8
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (this._hashset != null)
			{
				this._hashset.CopyTo(array, arrayIndex);
				return;
			}
			if (this.Count == 1)
			{
				array[arrayIndex] = this._singleItem;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00027D16 File Offset: 0x00025F16
		public IEnumerator<T> GetEnumerator()
		{
			if (this._hashset != null)
			{
				return this._hashset.GetEnumerator();
			}
			return this.SingleItemEnumerator();
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00027D37 File Offset: 0x00025F37
		private IEnumerator<T> SingleItemEnumerator()
		{
			if (this.Count != 0)
			{
				yield return this._singleItem;
			}
			yield break;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00027D4B File Offset: 0x00025F4B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000430 RID: 1072
		private readonly T _singleItem;

		// Token: 0x04000431 RID: 1073
		private HashSet<T> _hashset;

		// Token: 0x04000432 RID: 1074
		private readonly IEqualityComparer<T> _comparer;

		// Token: 0x0200027D RID: 637
		public struct SingleItemScopedInsert : IDisposable
		{
			// Token: 0x06001663 RID: 5731 RVA: 0x0003AA60 File Offset: 0x00038C60
			public SingleItemScopedInsert(T singleItem, ref SingleItemOptimizedHashSet<T> existing, bool forceHashSet, IEqualityComparer<T> comparer)
			{
				this._singleItem = singleItem;
				if (existing._hashset != null)
				{
					existing._hashset.Add(singleItem);
					this._hashset = existing._hashset;
					return;
				}
				if (forceHashSet)
				{
					existing = new SingleItemOptimizedHashSet<T>(singleItem, existing, comparer);
					existing.Add(singleItem);
					this._hashset = existing._hashset;
					return;
				}
				existing = new SingleItemOptimizedHashSet<T>(singleItem, existing, comparer);
				this._hashset = null;
			}

			// Token: 0x06001664 RID: 5732 RVA: 0x0003AADC File Offset: 0x00038CDC
			public void Dispose()
			{
				if (this._hashset != null)
				{
					this._hashset.Remove(this._singleItem);
				}
			}

			// Token: 0x040006C8 RID: 1736
			private readonly T _singleItem;

			// Token: 0x040006C9 RID: 1737
			private readonly HashSet<T> _hashset;
		}

		// Token: 0x0200027E RID: 638
		public sealed class ReferenceEqualityComparer : IEqualityComparer<T>
		{
			// Token: 0x06001665 RID: 5733 RVA: 0x0003AAF8 File Offset: 0x00038CF8
			bool IEqualityComparer<T>.Equals(T x, T y)
			{
				return x == y;
			}

			// Token: 0x06001666 RID: 5734 RVA: 0x0003AB08 File Offset: 0x00038D08
			int IEqualityComparer<T>.GetHashCode(T obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x040006CA RID: 1738
			public static readonly SingleItemOptimizedHashSet<T>.ReferenceEqualityComparer Default = new SingleItemOptimizedHashSet<T>.ReferenceEqualityComparer();
		}
	}
}
