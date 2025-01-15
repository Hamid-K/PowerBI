using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007C RID: 124
	internal abstract class SnapshottingCollection<TItem, TCollection> : ICollection<TItem>, IEnumerable<TItem>, IEnumerable where TCollection : class, ICollection<TItem>
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00011A38 File Offset: 0x0000FC38
		protected SnapshottingCollection(TCollection collection)
		{
			this.Collection = collection;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00011A47 File Offset: 0x0000FC47
		public int Count
		{
			get
			{
				return this.GetSnapshot().Count;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00011A59 File Offset: 0x0000FC59
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00011A5C File Offset: 0x0000FC5C
		public void Add(TItem item)
		{
			object obj = this.Collection;
			lock (obj)
			{
				this.Collection.Add(item);
				this.snapshot = default(TCollection);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		public virtual void Clear()
		{
			object obj = this.Collection;
			lock (obj)
			{
				this.Collection.Clear();
				this.snapshot = default(TCollection);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011B14 File Offset: 0x0000FD14
		public bool Contains(TItem item)
		{
			return this.GetSnapshot().Contains(item);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011B27 File Offset: 0x0000FD27
		public void CopyTo(TItem[] array, int arrayIndex)
		{
			this.GetSnapshot().CopyTo(array, arrayIndex);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public virtual bool Remove(TItem item)
		{
			object obj = this.Collection;
			bool flag3;
			lock (obj)
			{
				bool flag2 = this.Collection.Remove(item);
				if (flag2)
				{
					this.snapshot = default(TCollection);
				}
				flag3 = flag2;
			}
			return flag3;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public IEnumerator<TItem> GetEnumerator()
		{
			return this.GetSnapshot().GetEnumerator();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011BB2 File Offset: 0x0000FDB2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060003F6 RID: 1014
		protected abstract TCollection CreateSnapshot(TCollection collection);

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011BBC File Offset: 0x0000FDBC
		protected TCollection GetSnapshot()
		{
			TCollection tcollection = this.snapshot;
			if (tcollection == null)
			{
				object obj = this.Collection;
				lock (obj)
				{
					this.snapshot = this.CreateSnapshot(this.Collection);
					tcollection = this.snapshot;
				}
			}
			return tcollection;
		}

		// Token: 0x04000197 RID: 407
		protected readonly TCollection Collection;

		// Token: 0x04000198 RID: 408
		protected TCollection snapshot;
	}
}
