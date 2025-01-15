using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000357 RID: 855
	internal sealed class LinkedLruCache
	{
		// Token: 0x06001C33 RID: 7219 RVA: 0x00072197 File Offset: 0x00070397
		public LinkedLruCache()
		{
			this.m_sentinal = new LinkedLruCache.SentinalLruEntry();
			this.Clear();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000721B0 File Offset: 0x000703B0
		public void Add(LinkedLruCache.ILruEntry item)
		{
			this.m_count++;
			this.AddToEnd(item);
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000721C7 File Offset: 0x000703C7
		public void Bump(LinkedLruCache.ILruEntry item)
		{
			this.RemoveFromCurrentPosition(item);
			this.AddToEnd(item);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000721D8 File Offset: 0x000703D8
		public LinkedLruCache.ILruEntry ExtractLRU()
		{
			if (this.m_count == 0)
			{
				throw new InvalidOperationException("Cannot ExtractLRU from empty cache");
			}
			LinkedLruCache.ILruEntry next = this.m_sentinal.Next;
			this.Remove(next);
			return next;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0007220C File Offset: 0x0007040C
		public void Remove(LinkedLruCache.ILruEntry item)
		{
			if (this.m_count == 0)
			{
				throw new InvalidOperationException("Cannot ExtractLRU from empty cache");
			}
			if (item.Next == null || item.Previous == null)
			{
				return;
			}
			this.m_count--;
			this.RemoveFromCurrentPosition(item);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00072247 File Offset: 0x00070447
		public LinkedLruCache.ILruEntry Peek()
		{
			if (this.m_count == 0)
			{
				return null;
			}
			return this.m_sentinal.Next;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0007225E File Offset: 0x0007045E
		public void Clear()
		{
			this.m_count = 0;
			this.m_sentinal.Previous = this.m_sentinal;
			this.m_sentinal.Next = this.m_sentinal;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00072289 File Offset: 0x00070489
		public int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00072291 File Offset: 0x00070491
		private void AddToEnd(LinkedLruCache.ILruEntry item)
		{
			item.Next = this.m_sentinal;
			item.Previous = this.m_sentinal.Previous;
			this.m_sentinal.Previous.Next = item;
			this.m_sentinal.Previous = item;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000722CD File Offset: 0x000704CD
		private void RemoveFromCurrentPosition(LinkedLruCache.ILruEntry item)
		{
			item.Previous.Next = item.Next;
			item.Next.Previous = item.Previous;
			item.Next = null;
			item.Previous = null;
		}

		// Token: 0x04000BAC RID: 2988
		private int m_count;

		// Token: 0x04000BAD RID: 2989
		private LinkedLruCache.ILruEntry m_sentinal;

		// Token: 0x020004F4 RID: 1268
		internal interface ILruEntry
		{
			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x060024C1 RID: 9409
			// (set) Token: 0x060024C2 RID: 9410
			LinkedLruCache.ILruEntry Next { get; set; }

			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x060024C3 RID: 9411
			// (set) Token: 0x060024C4 RID: 9412
			LinkedLruCache.ILruEntry Previous { get; set; }
		}

		// Token: 0x020004F5 RID: 1269
		private sealed class SentinalLruEntry : LinkedLruCache.ILruEntry
		{
			// Token: 0x17000AAE RID: 2734
			// (get) Token: 0x060024C5 RID: 9413 RVA: 0x00086E36 File Offset: 0x00085036
			// (set) Token: 0x060024C6 RID: 9414 RVA: 0x00086E3E File Offset: 0x0008503E
			public LinkedLruCache.ILruEntry Next { get; set; }

			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x060024C7 RID: 9415 RVA: 0x00086E47 File Offset: 0x00085047
			// (set) Token: 0x060024C8 RID: 9416 RVA: 0x00086E4F File Offset: 0x0008504F
			public LinkedLruCache.ILruEntry Previous { get; set; }
		}
	}
}
