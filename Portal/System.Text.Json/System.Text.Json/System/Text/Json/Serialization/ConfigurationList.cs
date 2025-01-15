using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000086 RID: 134
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[DebuggerTypeProxy(typeof(ConfigurationList<>.ConfigurationListDebugView))]
	internal abstract class ConfigurationList<TItem> : IList<TItem>, ICollection<TItem>, IEnumerable<TItem>, IEnumerable
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00024D4A File Offset: 0x00022F4A
		public ConfigurationList(IEnumerable<TItem> source = null)
		{
			this._list = ((source == null) ? new List<TItem>() : new List<TItem>(source));
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600083F RID: 2111
		public abstract bool IsReadOnly { get; }

		// Token: 0x06000840 RID: 2112
		protected abstract void OnCollectionModifying();

		// Token: 0x06000841 RID: 2113 RVA: 0x00024D68 File Offset: 0x00022F68
		protected virtual void ValidateAddedValue(TItem item)
		{
		}

		// Token: 0x170001A6 RID: 422
		public TItem this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				if (value == null)
				{
					ThrowHelper.ThrowArgumentNullException("value");
				}
				this.ValidateAddedValue(value);
				this.OnCollectionModifying();
				this._list[index] = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00024DA6 File Offset: 0x00022FA6
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00024DB3 File Offset: 0x00022FB3
		public void Add(TItem item)
		{
			if (item == null)
			{
				ThrowHelper.ThrowArgumentNullException("item");
			}
			this.ValidateAddedValue(item);
			this.OnCollectionModifying();
			this._list.Add(item);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public void Clear()
		{
			this.OnCollectionModifying();
			this._list.Clear();
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00024DF3 File Offset: 0x00022FF3
		public bool Contains(TItem item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00024E01 File Offset: 0x00023001
		public void CopyTo(TItem[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00024E10 File Offset: 0x00023010
		public List<TItem>.Enumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00024E1D File Offset: 0x0002301D
		public int IndexOf(TItem item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00024E2B File Offset: 0x0002302B
		public void Insert(int index, TItem item)
		{
			if (item == null)
			{
				ThrowHelper.ThrowArgumentNullException("item");
			}
			this.ValidateAddedValue(item);
			this.OnCollectionModifying();
			this._list.Insert(index, item);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00024E59 File Offset: 0x00023059
		public bool Remove(TItem item)
		{
			this.OnCollectionModifying();
			return this._list.Remove(item);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00024E6D File Offset: 0x0002306D
		public void RemoveAt(int index)
		{
			this.OnCollectionModifying();
			this._list.RemoveAt(index);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00024E81 File Offset: 0x00023081
		IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00024E93 File Offset: 0x00023093
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00024EA5 File Offset: 0x000230A5
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("Count = {0}, IsReadOnly = {1}", this.Count, this.IsReadOnly);
			}
		}

		// Token: 0x040002EA RID: 746
		protected readonly List<TItem> _list;

		// Token: 0x02000132 RID: 306
		private sealed class ConfigurationListDebugView
		{
			// Token: 0x06000DD7 RID: 3543 RVA: 0x00035CE3 File Offset: 0x00033EE3
			public ConfigurationListDebugView(ConfigurationList<TItem> collection)
			{
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00035CF2 File Offset: 0x00033EF2
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public TItem[] Items
			{
				get
				{
					return this.<collection>P._list.ToArray();
				}
			}

			// Token: 0x040004C2 RID: 1218
			[CompilerGenerated]
			private ConfigurationList<TItem> <collection>P = collection;
		}
	}
}
