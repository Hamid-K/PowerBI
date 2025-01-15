using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	internal class ZoneAllocator<T> : IMemoryUsage, IZoneAllocator<T>, IAllocator<T>, IReset where T : new()
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00027DBE File Offset: 0x00025FBE
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x00027DCB File Offset: 0x00025FCB
		public int Capacity
		{
			get
			{
				return this.m_items.Count;
			}
			set
			{
				while (this.m_items.Count < value)
				{
					this.m_items.Add(new T());
				}
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00027DED File Offset: 0x00025FED
		public int Count
		{
			get
			{
				return this.m_nextIndex;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00027DF5 File Offset: 0x00025FF5
		public List<T> RawItems
		{
			get
			{
				return this.m_items;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00027E00 File Offset: 0x00026000
		public T New()
		{
			if (this.m_nextIndex == this.m_items.Count)
			{
				this.m_items.Add(new T());
			}
			else if (this.m_items[this.m_nextIndex] is IReset)
			{
				(this.m_items[this.m_nextIndex] as IReset).Reset();
			}
			List<T> items = this.m_items;
			int nextIndex = this.m_nextIndex;
			this.m_nextIndex = nextIndex + 1;
			return items[nextIndex];
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00027E8C File Offset: 0x0002608C
		public T NewNoReset()
		{
			if (this.m_nextIndex == this.m_items.Count)
			{
				this.m_items.Add(new T());
			}
			List<T> items = this.m_items;
			int nextIndex = this.m_nextIndex;
			this.m_nextIndex = nextIndex + 1;
			return items[nextIndex];
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00027ED8 File Offset: 0x000260D8
		public T NewFastNoReset()
		{
			List<T> items = this.m_items;
			int nextIndex = this.m_nextIndex;
			this.m_nextIndex = nextIndex + 1;
			return items[nextIndex];
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00027F01 File Offset: 0x00026101
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_items.Capacity * 4);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00027F11 File Offset: 0x00026111
		public void Reset()
		{
			this.m_nextIndex = 0;
		}

		// Token: 0x0400017A RID: 378
		private int m_nextIndex;

		// Token: 0x0400017B RID: 379
		private List<T> m_items = new List<T>();
	}
}
