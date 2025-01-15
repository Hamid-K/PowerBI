using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004C9 RID: 1225
	public class NotifyingCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001B47 RID: 6983 RVA: 0x0005227C File Offset: 0x0005047C
		// (remove) Token: 0x06001B48 RID: 6984 RVA: 0x000522B4 File Offset: 0x000504B4
		public event EventHandler<CollectionEvent<T>> Changed;

		// Token: 0x06001B49 RID: 6985 RVA: 0x000522E9 File Offset: 0x000504E9
		public IEnumerator<T> GetEnumerator()
		{
			return this._backingStore.GetEnumerator();
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000522E9 File Offset: 0x000504E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._backingStore.GetEnumerator();
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000522FC File Offset: 0x000504FC
		public void Add(IEnumerable<T> items)
		{
			IReadOnlyList<T> readOnlyList = (items as IReadOnlyList<T>) ?? items.ToList<T>();
			EventHandler<CollectionEvent<T>> changed = this.Changed;
			if (changed != null)
			{
				changed(this, new CollectionEvent<T>(CollectionAction.PreAdd, readOnlyList));
			}
			this._backingStore.AddRange(readOnlyList);
			EventHandler<CollectionEvent<T>> changed2 = this.Changed;
			if (changed2 == null)
			{
				return;
			}
			changed2(this, new CollectionEvent<T>(CollectionAction.Add, readOnlyList));
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00052357 File Offset: 0x00050557
		public void Add(T item)
		{
			this.Add(item.Yield<T>());
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00052365 File Offset: 0x00050565
		public void Add(T item, params T[] items)
		{
			this.Add(item.Yield<T>().Concat(items));
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00052379 File Offset: 0x00050579
		public void Clear()
		{
			this._backingStore.Clear();
			EventHandler<CollectionEvent<T>> changed = this.Changed;
			if (changed == null)
			{
				return;
			}
			changed(this, new CollectionEvent<T>(CollectionAction.Reset, null));
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000523A0 File Offset: 0x000505A0
		public bool Remove(IEnumerable<T> items)
		{
			IReadOnlyList<T> toRemove = (items as IReadOnlyList<T>) ?? items.ToList<T>();
			bool flag = this._backingStore.Intersect(toRemove).Any<T>();
			if (flag)
			{
				this._backingStore.RemoveAll((T c) => toRemove.Contains(c));
				EventHandler<CollectionEvent<T>> changed = this.Changed;
				if (changed == null)
				{
					return flag;
				}
				changed(this, new CollectionEvent<T>(CollectionAction.Remove, toRemove));
			}
			return flag;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00052417 File Offset: 0x00050617
		public IImmutableList<T> AsImmutable()
		{
			return this._backingStore.ToImmutable();
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00052424 File Offset: 0x00050624
		public bool Remove(params T[] items)
		{
			return this.Remove(items);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0005242D File Offset: 0x0005062D
		public bool Remove(T item)
		{
			return this.Remove(new T[] { item });
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00052443 File Offset: 0x00050643
		public bool Contains(T item)
		{
			return this._backingStore.Contains(item);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00052451 File Offset: 0x00050651
		public void CopyTo(T[] array, int arrayIndex)
		{
			this._backingStore.CopyTo(array, arrayIndex);
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x00052460 File Offset: 0x00050660
		public int Count
		{
			get
			{
				return this._backingStore.Count;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0005246D File Offset: 0x0005066D
		public bool IsReadOnly { get; }

		// Token: 0x04000D6F RID: 3439
		private readonly ImmutableList<T>.Builder _backingStore = ImmutableList<T>.Empty.ToBuilder();
	}
}
