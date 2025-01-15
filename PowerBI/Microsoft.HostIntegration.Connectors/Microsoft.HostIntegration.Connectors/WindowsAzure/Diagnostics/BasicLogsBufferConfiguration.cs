using System;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000468 RID: 1128
	[Obsolete("This API is deprecated.")]
	public class BasicLogsBufferConfiguration : DiagnosticDataBufferConfiguration
	{
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x00077A67 File Offset: 0x00075C67
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x00077A6F File Offset: 0x00075C6F
		public LogLevel ScheduledTransferLogLevelFilter { get; set; }
	}
}
