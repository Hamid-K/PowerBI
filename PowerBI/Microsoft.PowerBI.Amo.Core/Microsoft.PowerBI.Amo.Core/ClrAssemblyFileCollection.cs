using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000078 RID: 120
	[Guid("8DB99C4E-C9A1-4b63-8C42-8D31A593E380")]
	public sealed class ClrAssemblyFileCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00023520 File Offset: 0x00021720
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000172 RID: 370
		public ClrAssemblyFile this[int index]
		{
			get
			{
				return (ClrAssemblyFile)this.items[index];
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

		// Token: 0x0600066A RID: 1642 RVA: 0x00023562 File Offset: 0x00021762
		public int Add(ClrAssemblyFile item)
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

		// Token: 0x0600066B RID: 1643 RVA: 0x000235A4 File Offset: 0x000217A4
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				ClrAssemblyFile clrAssemblyFile = (ClrAssemblyFile)obj;
				this.Add(clrAssemblyFile);
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00023608 File Offset: 0x00021808
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00023615 File Offset: 0x00021815
		public bool Contains(ClrAssemblyFile item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00023628 File Offset: 0x00021828
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00023637 File Offset: 0x00021837
		public int IndexOf(ClrAssemblyFile item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0002364A File Offset: 0x0002184A
		public void Insert(int index, ClrAssemblyFile item)
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

		// Token: 0x06000671 RID: 1649 RVA: 0x0002368A File Offset: 0x0002188A
		public void Remove(ClrAssemblyFile item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0002369C File Offset: 0x0002189C
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000236AA File Offset: 0x000218AA
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000236AD File Offset: 0x000218AD
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000175 RID: 373
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
				if (!(value is ClrAssemblyFile))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00023710 File Offset: 0x00021910
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ClrAssemblyFile))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0002377D File Offset: 0x0002197D
		bool IList.Contains(object value)
		{
			return value != null && value is ClrAssemblyFile && this.items.Contains(value);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00023798 File Offset: 0x00021998
		int IList.IndexOf(object value)
		{
			if (value != null && value is ClrAssemblyFile)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000237B4 File Offset: 0x000219B4
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ClrAssemblyFile))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00023822 File Offset: 0x00021A22
		void IList.Remove(object value)
		{
			if (value == null || !(value is ClrAssemblyFile))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0002383C File Offset: 0x00021A3C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0002383F File Offset: 0x00021A3F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00023842 File Offset: 0x00021A42
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00023850 File Offset: 0x00021A50
		internal void CopyTo(ClrAssemblyFileCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			col.Clear();
			foreach (object obj in ((IEnumerable)this))
			{
				ClrAssemblyFile clrAssemblyFile = (ClrAssemblyFile)obj;
				col.Add(clrAssemblyFile.Clone());
			}
		}

		// Token: 0x04000424 RID: 1060
		private ArrayList items = new ArrayList();
	}
}
