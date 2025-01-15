using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200005C RID: 92
	public struct CacheSize
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00002F9F File Offset: 0x0000119F
		public CacheSize(int entryCount, long totalSize)
		{
			this.entryCount = entryCount;
			this.totalSize = totalSize;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00002FAF File Offset: 0x000011AF
		public int EntryCount
		{
			get
			{
				return this.entryCount;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002FB7 File Offset: 0x000011B7
		public long TotalSize
		{
			get
			{
				return this.totalSize;
			}
		}

		// Token: 0x0400013B RID: 315
		private readonly int entryCount;

		// Token: 0x0400013C RID: 316
		private readonly long totalSize;
	}
}
