using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028B RID: 651
	public abstract class ReadOnlyHashSetBase<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001BCB RID: 7115 RVA: 0x0004DBB3 File Offset: 0x0004BDB3
		protected ReadOnlyHashSetBase(HashSet<T> underlyingSet)
		{
			this._underlyingSet = ArgumentValidation.CheckNotNull<HashSet<T>>(underlyingSet, "underlyingSet");
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0004DBCC File Offset: 0x0004BDCC
		public int Count
		{
			get
			{
				return this.UnderlyingSet.Count;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0004DBD9 File Offset: 0x0004BDD9
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x0004DBDC File Offset: 0x0004BDDC
		private HashSet<T> UnderlyingSet
		{
			get
			{
				return this._underlyingSet;
			}
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return this.UnderlyingSet.IsProperSubsetOf(other);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0004DBF2 File Offset: 0x0004BDF2
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return this.UnderlyingSet.IsProperSupersetOf(other);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0004DC00 File Offset: 0x0004BE00
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return this.UnderlyingSet.IsSubsetOf(other);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0004DC0E File Offset: 0x0004BE0E
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return this.UnderlyingSet.IsSupersetOf(other);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0004DC1C File Offset: 0x0004BE1C
		public bool Overlaps(IEnumerable<T> other)
		{
			return this.UnderlyingSet.Overlaps(other);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0004DC2A File Offset: 0x0004BE2A
		public bool SetEquals(IEnumerable<T> other)
		{
			return this.UnderlyingSet.SetEquals(other);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0004DC38 File Offset: 0x0004BE38
		public bool Contains(T item)
		{
			return this.UnderlyingSet.Contains(item);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0004DC46 File Offset: 0x0004BE46
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.UnderlyingSet.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0004DC55 File Offset: 0x0004BE55
		public virtual IEnumerator<T> GetEnumerator()
		{
			return this.UnderlyingSet.GetEnumerator();
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0004DC67 File Offset: 0x0004BE67
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x0004DC6F File Offset: 0x0004BE6F
		bool ISet<T>.Add(T item)
		{
			throw new NotSupportedException("Add()");
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0004DC7B File Offset: 0x0004BE7B
		void ICollection<T>.Add(T item)
		{
			throw new NotSupportedException("Add()");
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0004DC87 File Offset: 0x0004BE87
		void ICollection<T>.Clear()
		{
			throw new NotSupportedException("Clear()");
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0004DC93 File Offset: 0x0004BE93
		bool ICollection<T>.Remove(T item)
		{
			throw new NotSupportedException("Remove()");
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0004DC9F File Offset: 0x0004BE9F
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException("ExceptWith()");
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0004DCAB File Offset: 0x0004BEAB
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw new NotSupportedException("IntersectWith()");
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0004DCB7 File Offset: 0x0004BEB7
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw new NotSupportedException("SymmetricExceptWith()");
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0004DCC3 File Offset: 0x0004BEC3
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw new NotSupportedException("UnionWith()");
		}

		// Token: 0x04000F30 RID: 3888
		private readonly HashSet<T> _underlyingSet;
	}
}
