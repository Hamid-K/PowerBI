using System;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000005 RID: 5
	public class LogEntry
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002058 File Offset: 0x00000258
		public EventLogLevel EventLogLevel { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002069 File Offset: 0x00000269
		public string Message { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000207A File Offset: 0x0000027A
		public string CorrelationId { get; set; }
	}
}
