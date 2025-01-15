using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200008F RID: 143
	[Guid("70FE779E-D08F-49ee-A662-E8496701058C")]
	public sealed class ImpactDetailCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00024838 File Offset: 0x00022A38
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170001A3 RID: 419
		public ImpactDetail this[int index]
		{
			get
			{
				return (ImpactDetail)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002487A File Offset: 0x00022A7A
		public int Add(ImpactDetail item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			return this.items.Add(item);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000248BC File Offset: 0x00022ABC
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				ImpactDetail impactDetail = (ImpactDetail)obj;
				this.Add(impactDetail);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00024920 File Offset: 0x00022B20
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0002492D File Offset: 0x00022B2D
		public bool Contains(ImpactDetail item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00024940 File Offset: 0x00022B40
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0002494F File Offset: 0x00022B4F
		public int IndexOf(ImpactDetail item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00024962 File Offset: 0x00022B62
		public void Insert(int index, ImpactDetail item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.items.Contains(item))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			this.items.Insert(index, item);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000249A2 File Offset: 0x00022BA2
		public void Remove(ImpactDetail item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000249B4 File Offset: 0x00022BB4
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000249C2 File Offset: 0x00022BC2
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000249C5 File Offset: 0x00022BC5
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A6 RID: 422
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				if (!(value is ImpactDetail))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00024A28 File Offset: 0x00022C28
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ImpactDetail))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00024A95 File Offset: 0x00022C95
		bool IList.Contains(object value)
		{
			return value != null && value is ImpactDetail && this.items.Contains(value);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00024AB0 File Offset: 0x00022CB0
		int IList.IndexOf(object value)
		{
			if (value != null && value is ImpactDetail)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00024ACC File Offset: 0x00022CCC
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ImpactDetail))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00024B3A File Offset: 0x00022D3A
		void IList.Remove(object value)
		{
			if (value == null || !(value is ImpactDetail))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00024B54 File Offset: 0x00022D54
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00024B57 File Offset: 0x00022D57
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00024B5A File Offset: 0x00022D5A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00024B68 File Offset: 0x00022D68
		public ArrayList GetInvalidObjects()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in ((IEnumerable)this))
			{
				ImpactDetail impactDetail = (ImpactDetail)obj;
				if (impactDetail.Impact == ImpactAnalysisType.Invalid)
				{
					object obj2 = impactDetail.Object;
					if (obj2 == null)
					{
						obj2 = impactDetail.ObjectReference;
					}
					if (obj2 != null)
					{
						arrayList.Add(obj2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00024BE4 File Offset: 0x00022DE4
		public ArrayList GetUnprocessedObjects()
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in ((IEnumerable)this))
			{
				ImpactDetail impactDetail = (ImpactDetail)obj;
				if (impactDetail.Impact == ImpactAnalysisType.Unprocessed)
				{
					object obj2 = impactDetail.Object;
					if (obj2 == null)
					{
						obj2 = impactDetail.ObjectReference;
					}
					if (obj2 != null)
					{
						arrayList.Add(obj2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04000474 RID: 1140
		private ArrayList items = new ArrayList();
	}
}
