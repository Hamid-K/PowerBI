using System;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000467 RID: 1127
	[Obsolete("This API is deprecated.")]
	public abstract class DiagnosticDataBufferConfiguration
	{
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600274A RID: 10058 RVA: 0x00077A45 File Offset: 0x00075C45
		// (set) Token: 0x0600274B RID: 10059 RVA: 0x00077A4D File Offset: 0x00075C4D
		public int BufferQuotaInMB { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x00077A56 File Offset: 0x00075C56
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x00077A5E File Offset: 0x00075C5E
		public TimeSpan ScheduledTransferPeriod { get; set; }
	}
}
