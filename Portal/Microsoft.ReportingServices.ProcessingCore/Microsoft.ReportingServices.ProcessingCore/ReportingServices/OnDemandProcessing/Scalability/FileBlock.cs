using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A4 RID: 2212
	internal sealed class FileBlock
	{
		// Token: 0x06007923 RID: 31011 RVA: 0x001F32E5 File Offset: 0x001F14E5
		internal FileBlock()
		{
			this.m_spaceManager = new DynamicBucketedHeapSpaceManager();
			this.m_spaceManager.AllowEndAllocation = false;
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x001F3304 File Offset: 0x001F1504
		public void Free(long offset, long size)
		{
			this.m_spaceManager.Free(offset, size);
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x001F3313 File Offset: 0x001F1513
		public long AllocateSpace(long size)
		{
			return this.m_spaceManager.AllocateSpace(size);
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x001F3321 File Offset: 0x001F1521
		public long Resize(long offset, long oldSize, long newSize)
		{
			return this.m_spaceManager.Resize(offset, oldSize, newSize);
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x001F3331 File Offset: 0x001F1531
		public void TraceStats(string desc)
		{
			this.m_spaceManager.TraceStats();
		}

		// Token: 0x04003CD6 RID: 15574
		private DynamicBucketedHeapSpaceManager m_spaceManager;
	}
}
