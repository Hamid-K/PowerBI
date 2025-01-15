using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000088 RID: 136
	[Guid("60e824e7-9b4f-425c-9078-293280a5a8c1")]
	public sealed class ExternalRoleMemberCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000242C7 File Offset: 0x000224C7
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000190 RID: 400
		public ExternalRoleMember this[int index]
		{
			get
			{
				return (ExternalRoleMember)this.items[index];
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

		// Token: 0x060006CD RID: 1741 RVA: 0x00024309 File Offset: 0x00022509
		public int Add(ExternalRoleMember item)
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

		// Token: 0x060006CE RID: 1742 RVA: 0x00024348 File Offset: 0x00022548
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				ExternalRoleMember externalRoleMember = (ExternalRoleMember)obj;
				this.Add(externalRoleMember);
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000243AC File Offset: 0x000225AC
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000243B9 File Offset: 0x000225B9
		public bool Contains(ExternalRoleMember item)
		{
			return item != null && this.items.Contains(item);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000243CC File Offset: 0x000225CC
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000243DB File Offset: 0x000225DB
		public int IndexOf(ExternalRoleMember item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000243EE File Offset: 0x000225EE
		public void Insert(int index, ExternalRoleMember item)
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

		// Token: 0x060006D4 RID: 1748 RVA: 0x0002442E File Offset: 0x0002262E
		public void Remove(ExternalRoleMember item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00024440 File Offset: 0x00022640
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0002444E File Offset: 0x0002264E
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00024451 File Offset: 0x00022651
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000193 RID: 403
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
				if (!(value is ExternalRoleMember))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000244B4 File Offset: 0x000226B4
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ExternalRoleMember))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00024521 File Offset: 0x00022721
		bool IList.Contains(object value)
		{
			return value != null && value is ExternalRoleMember && this.items.Contains(value);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0002453C File Offset: 0x0002273C
		int IList.IndexOf(object value)
		{
			if (value != null && value is ExternalRoleMember)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00024558 File Offset: 0x00022758
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is ExternalRoleMember))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000245C6 File Offset: 0x000227C6
		void IList.Remove(object value)
		{
			if (value == null || !(value is ExternalRoleMember))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000245E0 File Offset: 0x000227E0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x000245E3 File Offset: 0x000227E3
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000245E6 File Offset: 0x000227E6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000245F4 File Offset: 0x000227F4
		internal void CopyTo(ExternalRoleMemberCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			col.items.Clear();
			foreach (object obj in this.items)
			{
				ExternalRoleMember externalRoleMember = (ExternalRoleMember)obj;
				col.items.Add(externalRoleMember.Clone());
			}
		}

		// Token: 0x0400045F RID: 1119
		private ArrayList items = new ArrayList();
	}
}
