using System;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000052 RID: 82
	internal class DataIndexTelemetry
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000084E9 File Offset: 0x000066E9
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000084F1 File Offset: 0x000066F1
		public DataIndexOperationStatus Status { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000084FA File Offset: 0x000066FA
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00008502 File Offset: 0x00006702
		public DataIndexStatistics Statistics { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000850B File Offset: 0x0000670B
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008513 File Offset: 0x00006713
		public string Message { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000851C File Offset: 0x0000671C
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00008524 File Offset: 0x00006724
		public Exception Exception { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000852D File Offset: 0x0000672D
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00008535 File Offset: 0x00006735
		public LuciaSessionOptions Options { get; set; }
	}
}
