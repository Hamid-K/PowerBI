using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000291 RID: 657
	public abstract class ReadOnlyKeyedCollection<TKey, TItem> : ReadOnlyCollection<TItem> where TKey : class
	{
		// Token: 0x06001BF6 RID: 7158 RVA: 0x0004DF78 File Offset: 0x0004C178
		protected ReadOnlyKeyedCollection(IEnumerable<TItem> items)
			: this(new ReadOnlyKeyedCollection<TKey, TItem>.KeyedCollectionImpl(), items)
		{
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0004DF86 File Offset: 0x0004C186
		protected ReadOnlyKeyedCollection(IEnumerable<TItem> items, IEqualityComparer<TKey> comparer)
			: this(new ReadOnlyKeyedCollection<TKey, TItem>.KeyedCollectionImpl(comparer), items)
		{
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0004DF95 File Offset: 0x0004C195
		private ReadOnlyKeyedCollection(ReadOnlyKeyedCollection<TKey, TItem>.KeyedCollectionImpl list, IEnumerable<TItem> items)
			: base(list)
		{
			list.Initialize(this, items);
			this._list = list;
		}

		// Token: 0x170007C5 RID: 1989
		public TItem this[TKey key]
		{
			get
			{
				return this._list.FindItem(key);
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0004DFBB File Offset: 0x0004C1BB
		public bool Contains(TKey key)
		{
			return this._list.Contains(key);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0004DFC9 File Offset: 0x0004C1C9
		public bool TryGetItem(TKey key, out TItem item)
		{
			return this._list.TryGetItem(key, out item);
		}

		// Token: 0x06001BFC RID: 7164
		protected abstract TKey GetKeyForItem(TItem item);

		// Token: 0x04000F33 RID: 3891
		private readonly ReadOnlyKeyedCollection<TKey, TItem>.KeyedCollectionImpl _list;

		// Token: 0x0200041D RID: 1053
		private sealed class KeyedCollectionImpl : KeyedCollection<TKey, TItem>
		{
			// Token: 0x060021C1 RID: 8641 RVA: 0x0005ABE8 File Offset: 0x00058DE8
			internal KeyedCollectionImpl()
			{
			}

			// Token: 0x060021C2 RID: 8642 RVA: 0x0005ABF0 File Offset: 0x00058DF0
			internal KeyedCollectionImpl(IEqualityComparer<TKey> comparer)
				: base(comparer)
			{
			}

			// Token: 0x060021C3 RID: 8643 RVA: 0x0005ABF9 File Offset: 0x00058DF9
			protected override TKey GetKeyForItem(TItem item)
			{
				return this._owner.GetKeyForItem(item);
			}

			// Token: 0x060021C4 RID: 8644 RVA: 0x0005AC08 File Offset: 0x00058E08
			internal void Initialize(ReadOnlyKeyedCollection<TKey, TItem> owner, IEnumerable<TItem> items)
			{
				this._owner = owner;
				foreach (TItem titem in items)
				{
					base.Add(titem);
				}
			}

			// Token: 0x060021C5 RID: 8645 RVA: 0x0005AC58 File Offset: 0x00058E58
			internal bool TryGetItem(TKey key, out TItem item)
			{
				if (key == null || base.Dictionary == null)
				{
					item = default(TItem);
					return false;
				}
				return base.Dictionary.TryGetValue(key, out item);
			}

			// Token: 0x060021C6 RID: 8646 RVA: 0x0005AC80 File Offset: 0x00058E80
			internal TItem FindItem(TKey key)
			{
				TItem titem;
				if (this.TryGetItem(key, out titem))
				{
					return titem;
				}
				return base.Items.SingleOrDefault((TItem i) => this.Comparer.Equals(this.GetKeyForItem(i), key));
			}

			// Token: 0x0400148F RID: 5263
			private ReadOnlyKeyedCollection<TKey, TItem> _owner;
		}
	}
}
