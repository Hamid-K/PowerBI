using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000076 RID: 118
	[Guid("6CCE165D-2AFB-4c5d-93B4-0DF4C04F0947")]
	public sealed class BackupLocationCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00023076 File Offset: 0x00021276
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000168 RID: 360
		public BackupLocation this[int index]
		{
			get
			{
				return (BackupLocation)this.items[index];
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

		// Token: 0x06000645 RID: 1605 RVA: 0x000230B8 File Offset: 0x000212B8
		public int Add(BackupLocation item)
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

		// Token: 0x06000646 RID: 1606 RVA: 0x000230F8 File Offset: 0x000212F8
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				BackupLocation backupLocation = (BackupLocation)obj;
				this.Add(backupLocation);
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002315C File Offset: 0x0002135C
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00023169 File Offset: 0x00021369
		public bool Contains(BackupLocation item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002317C File Offset: 0x0002137C
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002318B File Offset: 0x0002138B
		public int IndexOf(BackupLocation item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002319E File Offset: 0x0002139E
		public void Insert(int index, BackupLocation item)
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

		// Token: 0x0600064C RID: 1612 RVA: 0x000231DE File Offset: 0x000213DE
		public void Remove(BackupLocation item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000231F0 File Offset: 0x000213F0
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x000231FE File Offset: 0x000213FE
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00023201 File Offset: 0x00021401
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016B RID: 363
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
				if (!(value is BackupLocation))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00023264 File Offset: 0x00021464
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is BackupLocation))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000232D1 File Offset: 0x000214D1
		bool IList.Contains(object value)
		{
			return value != null && value is BackupLocation && this.items.Contains(value);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000232EC File Offset: 0x000214EC
		int IList.IndexOf(object value)
		{
			if (value != null && value is BackupLocation)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00023308 File Offset: 0x00021508
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is BackupLocation))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00023376 File Offset: 0x00021576
		void IList.Remove(object value)
		{
			if (value == null || !(value is BackupLocation))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00023390 File Offset: 0x00021590
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00023393 File Offset: 0x00021593
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00023396 File Offset: 0x00021596
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400041F RID: 1055
		private ArrayList items = new ArrayList();
	}
}
