using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200009F RID: 159
	internal sealed class FreeMemoryRequestContext : MarshalByRefObject
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000F512 File Offset: 0x0000D712
		public FreeMemoryRequestContext(long kbytesToFree, bool notifyAllConsumers, bool isShrinkBackgroundOnly)
		{
			this.m_kBytesToFree = kbytesToFree;
			this.m_notifyAllConsumers = notifyAllConsumers;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000F528 File Offset: 0x0000D728
		public FreeMemoryRequestContext(long kbytesToFree, bool notifyAllConsumers)
			: this(kbytesToFree, notifyAllConsumers, false)
		{
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000F533 File Offset: 0x0000D733
		public long KBytesToFree
		{
			get
			{
				return this.m_kBytesToFree;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000F53B File Offset: 0x0000D73B
		public bool NotifyAllConsumers
		{
			get
			{
				return this.m_notifyAllConsumers;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000F543 File Offset: 0x0000D743
		public bool IsShrinkBackGroundOnly
		{
			get
			{
				return this.m_isShrinkBackgroundOnly;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000F54B File Offset: 0x0000D74B
		public void SetShrinkResults(bool shrinkAttempted, long estimatedKBytesFreed, long totalAuditedKBytes)
		{
			this.m_shrinkAttempted = shrinkAttempted;
			this.m_estimatedKBytesFreed = estimatedKBytesFreed;
			this.m_totalAuditedKb = totalAuditedKBytes;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000F562 File Offset: 0x0000D762
		public bool WasShrinkAttempted
		{
			get
			{
				return this.m_shrinkAttempted;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000F56A File Offset: 0x0000D76A
		public long EstimatedKBytesFreed
		{
			get
			{
				return this.m_estimatedKBytesFreed;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000F572 File Offset: 0x0000D772
		public long TotalAuditedKb
		{
			get
			{
				return this.m_totalAuditedKb;
			}
		}

		// Token: 0x040002DB RID: 731
		private readonly long m_kBytesToFree;

		// Token: 0x040002DC RID: 732
		private bool m_shrinkAttempted;

		// Token: 0x040002DD RID: 733
		private bool m_notifyAllConsumers;

		// Token: 0x040002DE RID: 734
		private bool m_isShrinkBackgroundOnly;

		// Token: 0x040002DF RID: 735
		private long m_estimatedKBytesFreed;

		// Token: 0x040002E0 RID: 736
		private long m_totalAuditedKb;
	}
}
