using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B4 RID: 180
	[Guid("240E340B-D20B-4d44-B3A1-B4CD22BAA938")]
	public sealed class RestoreLocationCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00028581 File Offset: 0x00026781
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000201 RID: 513
		public RestoreLocation this[int index]
		{
			get
			{
				return (RestoreLocation)this.items[index];
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

		// Token: 0x060008A3 RID: 2211 RVA: 0x000285C3 File Offset: 0x000267C3
		public int Add(RestoreLocation item)
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

		// Token: 0x060008A4 RID: 2212 RVA: 0x00028604 File Offset: 0x00026804
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				RestoreLocation restoreLocation = (RestoreLocation)obj;
				this.Add(restoreLocation);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00028668 File Offset: 0x00026868
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00028675 File Offset: 0x00026875
		public bool Contains(RestoreLocation item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00028688 File Offset: 0x00026888
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00028697 File Offset: 0x00026897
		public int IndexOf(RestoreLocation item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000286AA File Offset: 0x000268AA
		public void Insert(int index, RestoreLocation item)
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

		// Token: 0x060008AA RID: 2218 RVA: 0x000286EA File Offset: 0x000268EA
		public void Remove(RestoreLocation item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000286FC File Offset: 0x000268FC
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0002870A File Offset: 0x0002690A
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0002870D File Offset: 0x0002690D
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000204 RID: 516
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
				if (!(value is RestoreLocation))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00028770 File Offset: 0x00026970
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RestoreLocation))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000287DD File Offset: 0x000269DD
		bool IList.Contains(object value)
		{
			return value != null && value is RestoreLocation && this.items.Contains(value);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000287F8 File Offset: 0x000269F8
		int IList.IndexOf(object value)
		{
			if (value != null && value is RestoreLocation)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00028814 File Offset: 0x00026A14
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RestoreLocation))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00028882 File Offset: 0x00026A82
		void IList.Remove(object value)
		{
			if (value == null || !(value is RestoreLocation))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0002889C File Offset: 0x00026A9C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002889F File Offset: 0x00026A9F
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x000288A2 File Offset: 0x00026AA2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x040004E2 RID: 1250
		private ArrayList items = new ArrayList();
	}
}
