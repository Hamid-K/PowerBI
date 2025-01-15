using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000080 RID: 128
	public sealed class ReportServerAbortInfo
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000B896 File Offset: 0x00009A96
		public ReportServerAbortInfo(ReportServerAbortInfo.AbortReason reason)
		{
			this.Reason = reason;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000B8AD File Offset: 0x00009AAD
		public ReportServerAbortInfo.AbortReason Reason
		{
			get
			{
				return this.m_reason;
			}
			set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x040001F0 RID: 496
		private ReportServerAbortInfo.AbortReason m_reason;

		// Token: 0x020000EF RID: 239
		public enum AbortReason
		{
			// Token: 0x040004B4 RID: 1204
			TimeoutExpired,
			// Token: 0x040004B5 RID: 1205
			JobCanceled,
			// Token: 0x040004B6 RID: 1206
			JobOrphaned
		}
	}
}
