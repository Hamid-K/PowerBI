using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000846 RID: 2118
	internal sealed class LinkedLRUCache<T> where T : ItemHolder
	{
		// Token: 0x0600765D RID: 30301 RVA: 0x001EAB63 File Offset: 0x001E8D63
		public LinkedLRUCache()
		{
			this.m_sentinal = new ItemHolder();
			this.Clear();
		}

		// Token: 0x0600765E RID: 30302 RVA: 0x001EAB7C File Offset: 0x001E8D7C
		public void Add(ItemHolder item)
		{
			this.m_count++;
			item.Next = this.m_sentinal;
			item.Previous = this.m_sentinal.Previous;
			this.m_sentinal.Previous.Next = item;
			this.m_sentinal.Previous = item;
		}

		// Token: 0x0600765F RID: 30303 RVA: 0x001EABD4 File Offset: 0x001E8DD4
		public void Bump(ItemHolder item)
		{
			item.Previous.Next = item.Next;
			item.Next.Previous = item.Previous;
			item.Next = this.m_sentinal;
			item.Previous = this.m_sentinal.Previous;
			this.m_sentinal.Previous.Next = item;
			this.m_sentinal.Previous = item;
		}

		// Token: 0x06007660 RID: 30304 RVA: 0x001EAC40 File Offset: 0x001E8E40
		public T ExtractLRU()
		{
			if (this.m_count == 0)
			{
				Global.Tracer.Assert(false, "Cannot ExtractLRU from empty cache");
			}
			ItemHolder next = this.m_sentinal.Next;
			this.Remove(next);
			return (T)((object)next);
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x001EAC80 File Offset: 0x001E8E80
		public void Remove(ItemHolder item)
		{
			if (this.m_count == 0)
			{
				Global.Tracer.Assert(false, "Cannot ExtractLRU from empty cache");
			}
			this.m_count--;
			item.Previous.Next = item.Next;
			item.Next.Previous = item.Previous;
			item.Next = null;
			item.Previous = null;
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x001EACE4 File Offset: 0x001E8EE4
		public T Peek()
		{
			if (this.m_count == 0)
			{
				return default(T);
			}
			return (T)((object)this.m_sentinal.Next);
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x001EAD13 File Offset: 0x001E8F13
		public void Clear()
		{
			this.m_count = 0;
			this.m_sentinal.Previous = this.m_sentinal;
			this.m_sentinal.Next = this.m_sentinal;
		}

		// Token: 0x170027AD RID: 10157
		// (get) Token: 0x06007664 RID: 30308 RVA: 0x001EAD3E File Offset: 0x001E8F3E
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x04003BF5 RID: 15349
		private int m_count;

		// Token: 0x04003BF6 RID: 15350
		private ItemHolder m_sentinal;
	}
}
