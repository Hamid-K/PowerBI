using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200007D RID: 125
	internal class SnapshottingList<T> : SnapshottingCollection<T, IList<T>>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00011C24 File Offset: 0x0000FE24
		public SnapshottingList()
			: base(new List<T>())
		{
		}

		// Token: 0x170000EE RID: 238
		public virtual T this[int index]
		{
			get
			{
				return base.GetSnapshot()[index];
			}
			set
			{
				IList<T> collection = this.Collection;
				lock (collection)
				{
					this.Collection[index] = value;
					this.snapshot = null;
				}
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00011C90 File Offset: 0x0000FE90
		public int IndexOf(T item)
		{
			return base.GetSnapshot().IndexOf(item);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public virtual void Insert(int index, T item)
		{
			IList<T> collection = this.Collection;
			lock (collection)
			{
				this.Collection.Insert(index, item);
				this.snapshot = null;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		public virtual void RemoveAt(int index)
		{
			IList<T> collection = this.Collection;
			lock (collection)
			{
				this.Collection.RemoveAt(index);
				this.snapshot = null;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011D40 File Offset: 0x0000FF40
		protected sealed override IList<T> CreateSnapshot(IList<T> collection)
		{
			return new List<T>(collection);
		}
	}
}
