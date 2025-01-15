using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EE RID: 1262
	public class ReadOnlyMetadataCollection<T> : ReadOnlyCollection<T> where T : MetadataItem
	{
		// Token: 0x06003EB0 RID: 16048 RVA: 0x000D0B34 File Offset: 0x000CED34
		internal ReadOnlyMetadataCollection()
			: base(new MetadataCollection<T>())
		{
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000D0B41 File Offset: 0x000CED41
		internal ReadOnlyMetadataCollection(MetadataCollection<T> collection)
			: base(collection)
		{
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000D0B4A File Offset: 0x000CED4A
		internal ReadOnlyMetadataCollection(List<T> list)
			: base(MetadataCollection<T>.Wrap(list))
		{
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003EB3 RID: 16051 RVA: 0x000D0B58 File Offset: 0x000CED58
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C48 RID: 3144
		public virtual T this[string identity]
		{
			get
			{
				return ((MetadataCollection<T>)base.Items)[identity];
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003EB5 RID: 16053 RVA: 0x000D0B70 File Offset: 0x000CED70
		internal MetadataCollection<T> Source
		{
			get
			{
				MetadataCollection<T> metadataCollection;
				try
				{
					metadataCollection = (MetadataCollection<T>)base.Items;
				}
				finally
				{
					EventHandler sourceAccessed = this.SourceAccessed;
					if (sourceAccessed != null)
					{
						sourceAccessed(this, null);
					}
				}
				return metadataCollection;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06003EB6 RID: 16054 RVA: 0x000D0BB0 File Offset: 0x000CEDB0
		// (remove) Token: 0x06003EB7 RID: 16055 RVA: 0x000D0BE8 File Offset: 0x000CEDE8
		internal event EventHandler SourceAccessed;

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000D0C1D File Offset: 0x000CEE1D
		public virtual T GetValue(string identity, bool ignoreCase)
		{
			return ((MetadataCollection<T>)base.Items).GetValue(identity, ignoreCase);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000D0C31 File Offset: 0x000CEE31
		public virtual bool Contains(string identity)
		{
			return ((MetadataCollection<T>)base.Items).ContainsIdentity(identity);
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x000D0C44 File Offset: 0x000CEE44
		public virtual bool TryGetValue(string identity, bool ignoreCase, out T item)
		{
			return ((MetadataCollection<T>)base.Items).TryGetValue(identity, ignoreCase, out item);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000D0C59 File Offset: 0x000CEE59
		public new ReadOnlyMetadataCollection<T>.Enumerator GetEnumerator()
		{
			return new ReadOnlyMetadataCollection<T>.Enumerator(base.Items);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x000D0C66 File Offset: 0x000CEE66
		public new virtual int IndexOf(T value)
		{
			return base.IndexOf(value);
		}

		// Token: 0x02000B0C RID: 2828
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006436 RID: 25654 RVA: 0x0015AA99 File Offset: 0x00158C99
			internal Enumerator(IList<T> collection)
			{
				this._parent = collection;
				this._nextIndex = 0;
				this._current = default(T);
			}

			// Token: 0x170010E0 RID: 4320
			// (get) Token: 0x06006437 RID: 25655 RVA: 0x0015AAB5 File Offset: 0x00158CB5
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170010E1 RID: 4321
			// (get) Token: 0x06006438 RID: 25656 RVA: 0x0015AABD File Offset: 0x00158CBD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006439 RID: 25657 RVA: 0x0015AACA File Offset: 0x00158CCA
			public void Dispose()
			{
			}

			// Token: 0x0600643A RID: 25658 RVA: 0x0015AACC File Offset: 0x00158CCC
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

			// Token: 0x0600643B RID: 25659 RVA: 0x0015AB20 File Offset: 0x00158D20
			public void Reset()
			{
				this._current = default(T);
				this._nextIndex = 0;
			}

			// Token: 0x04002C93 RID: 11411
			private int _nextIndex;

			// Token: 0x04002C94 RID: 11412
			private readonly IList<T> _parent;

			// Token: 0x04002C95 RID: 11413
			private T _current;
		}
	}
}
