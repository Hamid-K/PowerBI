using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A7 RID: 167
	public class ReadOnlyMetadataCollection<T> : ReadOnlyCollection<T> where T : MetadataItem
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0001D111 File Offset: 0x0001B311
		internal ReadOnlyMetadataCollection(IList<T> collection)
			: base(collection)
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0001D11A File Offset: 0x0001B31A
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700040C RID: 1036
		public virtual T this[string identity]
		{
			get
			{
				return ((MetadataCollection<T>)base.Items)[identity];
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0001D130 File Offset: 0x0001B330
		internal MetadataCollection<T> Source
		{
			get
			{
				return (MetadataCollection<T>)base.Items;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001D13D File Offset: 0x0001B33D
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			return ((MetadataCollection<T>)base.Items).GetValue(identity, ignoreCase);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001D151 File Offset: 0x0001B351
		public virtual bool Contains(string identity)
		{
			return ((MetadataCollection<T>)base.Items).ContainsIdentity(identity);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001D164 File Offset: 0x0001B364
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			return ((MetadataCollection<T>)base.Items).TryGetValue(identity, ignoreCase, out item);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001D179 File Offset: 0x0001B379
		public new ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(base.Items);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001D186 File Offset: 0x0001B386
		public new virtual int IndexOf(T value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x020002C4 RID: 708
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06001C82 RID: 7298 RVA: 0x0004F03D File Offset: 0x0004D23D
			internal Enumerator(IList<T> collection)
			{
				this._parent = collection;
				this._nextIndex = 0;
				this._current = default(T);
			}

			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0004F059 File Offset: 0x0004D259
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0004F061 File Offset: 0x0004D261
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001C85 RID: 7301 RVA: 0x0004F06E File Offset: 0x0004D26E
			public void Dispose()
			{
			}

			// Token: 0x06001C86 RID: 7302 RVA: 0x0004F070 File Offset: 0x0004D270
			public bool MoveNext()
			{
				if (this._nextIndex < this._parent.Count)
				{
					this._current = this._parent[this._nextIndex];
					this._nextIndex++;
					return true;
				}
				this._current = default(T);
				return false;
			}

			// Token: 0x06001C87 RID: 7303 RVA: 0x0004F0C4 File Offset: 0x0004D2C4
			public void Reset()
			{
				this._current = default(T);
				this._nextIndex = 0;
			}

			// Token: 0x04000FD9 RID: 4057
			private int _nextIndex;

			// Token: 0x04000FDA RID: 4058
			private IList<T> _parent;

			// Token: 0x04000FDB RID: 4059
			private T _current;
		}
	}
}
