using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	internal sealed class ObjectAllocator<T> : IMemoryUsage
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x000230AA File Offset: 0x000212AA
		public ObjectAllocator(ObjectAllocator<T>.ObjectConstructor constructor)
		{
			this.m_constructor = constructor;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000230C4 File Offset: 0x000212C4
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_freeItems.Count * 4);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000230D4 File Offset: 0x000212D4
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000230DC File Offset: 0x000212DC
		public ObjectAllocator<T>.ObjectConstructor Constructor
		{
			get
			{
				return this.m_constructor;
			}
			set
			{
				this.m_constructor = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000230E5 File Offset: 0x000212E5
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000230FC File Offset: 0x000212FC
		public int Capactity
		{
			get
			{
				return this.m_outstandingItems + this.m_freeItems.Count;
			}
			set
			{
				if (this.Capactity < value)
				{
					int num = value - this.Capactity - this.m_freeItems.Count;
					this.m_freeItems.Capacity = Math.Max(this.m_freeItems.Capacity, num);
					for (int i = 0; i < num; i++)
					{
						this.m_freeItems.Add(this.m_constructor());
					}
					return;
				}
				int capactity = this.Capactity;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00023170 File Offset: 0x00021370
		public T New()
		{
			this.m_outstandingItems++;
			if (this.m_freeItems.Count > 0)
			{
				T t = this.m_freeItems[this.m_freeItems.Count - 1];
				this.m_freeItems.RemoveAt(this.m_freeItems.Count - 1);
				return t;
			}
			return this.m_constructor();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000231D5 File Offset: 0x000213D5
		public void Add(T item)
		{
			this.m_freeItems.Add(item);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000231E3 File Offset: 0x000213E3
		public void Return(T item)
		{
			this.m_freeItems.Add(item);
			this.m_outstandingItems--;
		}

		// Token: 0x04000141 RID: 321
		private ObjectAllocator<T>.ObjectConstructor m_constructor;

		// Token: 0x04000142 RID: 322
		private List<T> m_freeItems = new List<T>();

		// Token: 0x04000143 RID: 323
		private int m_outstandingItems;

		// Token: 0x02000134 RID: 308
		// (Invoke) Token: 0x06000A10 RID: 2576
		public delegate T ObjectConstructor();
	}
}
