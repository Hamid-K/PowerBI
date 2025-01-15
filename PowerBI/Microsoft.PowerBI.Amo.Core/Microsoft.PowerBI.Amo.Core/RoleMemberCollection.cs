using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000B7 RID: 183
	[Guid("39DFDC96-0B38-4ddf-9514-F387EE32407B")]
	public sealed class RoleMemberCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00028979 File Offset: 0x00026B79
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x1700020A RID: 522
		public RoleMember this[int index]
		{
			get
			{
				return (RoleMember)this.items[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
				}
				int num = this.IndexOfMember(value.Name, value.Sid);
				if (num != -1 && num != index)
				{
					throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x000289F4 File Offset: 0x00026BF4
		public int Add(RoleMember item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.IndexOfMember(item.Name, item.Sid) != -1)
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			return this.items.Add(item);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00028A48 File Offset: 0x00026C48
		public void AddRange(ICollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (object obj in items)
			{
				RoleMember roleMember = (RoleMember)obj;
				this.Add(roleMember);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00028AAC File Offset: 0x00026CAC
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00028AB9 File Offset: 0x00026CB9
		public bool Contains(RoleMember item)
		{
			return item != null && this.IndexOfMember(item.Name, item.Sid) != -1;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00028AD8 File Offset: 0x00026CD8
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00028AE7 File Offset: 0x00026CE7
		public int IndexOf(RoleMember item)
		{
			if (item != null)
			{
				return this.items.IndexOf(item);
			}
			return -1;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00028AFC File Offset: 0x00026CFC
		public void Insert(int index, RoleMember item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item", SR.Collections_CannotAddANullItem);
			}
			if (this.IndexOfMember(item.Name, item.Sid) != -1)
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "item");
			}
			this.items.Insert(index, item);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00028B4E File Offset: 0x00026D4E
		public void Remove(RoleMember item)
		{
			if (item == null)
			{
				return;
			}
			this.items.Remove(item);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00028B60 File Offset: 0x00026D60
		public void RemoveAt(int index)
		{
			this.items.RemoveAt(index);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00028B6E File Offset: 0x00026D6E
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00028B71 File Offset: 0x00026D71
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020D RID: 525
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
				if (!(value is RoleMember))
				{
					throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
				}
				this.items[index] = value;
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00028BD4 File Offset: 0x00026DD4
		int IList.Add(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RoleMember))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			return this.items.Add(value);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00028C41 File Offset: 0x00026E41
		bool IList.Contains(object value)
		{
			return value != null && value is RoleMember && this.items.Contains(value);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00028C5C File Offset: 0x00026E5C
		int IList.IndexOf(object value)
		{
			if (value != null && value is RoleMember)
			{
				return this.items.IndexOf(value);
			}
			return -1;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00028C78 File Offset: 0x00026E78
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", SR.Collections_CannotAddANullItem);
			}
			if (!(value is RoleMember))
			{
				throw new ArgumentException(SR.Collections_AddingObjectOfInvalidType(value.GetType().Name), "value");
			}
			if (this.items.Contains(value))
			{
				throw new ArgumentException(SR.Collections_ItemAlreadyExists, "value");
			}
			this.items.Insert(index, value);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00028CE6 File Offset: 0x00026EE6
		void IList.Remove(object value)
		{
			if (value == null || !(value is RoleMember))
			{
				return;
			}
			this.items.Remove(value);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00028D00 File Offset: 0x00026F00
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00028D03 File Offset: 0x00026F03
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00028D06 File Offset: 0x00026F06
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00028D14 File Offset: 0x00026F14
		internal void CopyTo(RoleMemberCollection col)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			col.Clear();
			foreach (object obj in ((IEnumerable)this))
			{
				RoleMember roleMember = (RoleMember)obj;
				try
				{
					col.Add(roleMember.Clone());
				}
				catch (ArgumentException)
				{
				}
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00028D94 File Offset: 0x00026F94
		private int IndexOfMember(string name, string sid)
		{
			if (string.IsNullOrEmpty(sid))
			{
				if (string.IsNullOrEmpty(name))
				{
					return -1;
				}
				for (int i = 0; i < this.items.Count; i++)
				{
					if (string.Compare(name, ((RoleMember)this.items[i]).Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.items.Count; j++)
				{
					if (string.Compare(sid, ((RoleMember)this.items[j]).Sid, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x040004E9 RID: 1257
		private ArrayList items = new ArrayList();
	}
}
