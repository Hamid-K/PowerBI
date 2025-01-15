using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	internal class ListCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00022527 File Offset: 0x00020727
		public ListCollection()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002253A File Offset: 0x0002073A
		public ListCollection(IEnumerable<T> items)
		{
			this.AddRange(items);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00022554 File Offset: 0x00020754
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0002255C File Offset: 0x0002075C
		protected List<T> Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				this.m_items = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		public virtual T this[int index]
		{
			get
			{
				return this.Items[index];
			}
			set
			{
				this.Insert(index, value);
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002257D File Offset: 0x0002077D
		public int IndexOf(T item)
		{
			return this.Items.IndexOf(item);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002258B File Offset: 0x0002078B
		public virtual void Insert(int index, T item)
		{
			this.Items.Insert(index, item);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002259A File Offset: 0x0002079A
		public virtual void RemoveAt(int index)
		{
			this.Items.RemoveAt(index);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000225A8 File Offset: 0x000207A8
		public IEnumerator<T> GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000225BA File Offset: 0x000207BA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Items.GetEnumerator();
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x000225CC File Offset: 0x000207CC
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000225D9 File Offset: 0x000207D9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000225DC File Offset: 0x000207DC
		public virtual void Add(T item)
		{
			this.Items.Add(item);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000225EA File Offset: 0x000207EA
		public void Clear()
		{
			this.Items.Clear();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000225F7 File Offset: 0x000207F7
		public bool Contains(T item)
		{
			return this.Items.Contains(item);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00022605 File Offset: 0x00020805
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.Items.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00022614 File Offset: 0x00020814
		public virtual bool Remove(T item)
		{
			return this.Items.Remove(item);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00022622 File Offset: 0x00020822
		public virtual object Clone()
		{
			return new ListCollection<T>(this.Items);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002262F File Offset: 0x0002082F
		public virtual void AddRange(IEnumerable<T> items)
		{
			this.Items.AddRange(items);
		}

		// Token: 0x0400012E RID: 302
		private List<T> m_items = new List<T>();
	}
}
