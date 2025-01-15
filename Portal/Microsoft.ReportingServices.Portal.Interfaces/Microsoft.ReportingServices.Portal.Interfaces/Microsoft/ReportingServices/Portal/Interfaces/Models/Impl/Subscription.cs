using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models.Impl
{
	// Token: 0x020000A9 RID: 169
	public sealed class Subscription
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00004AD2 File Offset: 0x00002CD2
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00004ADA File Offset: 0x00002CDA
		public Guid Id { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00004AE3 File Offset: 0x00002CE3
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00004AEB File Offset: 0x00002CEB
		public string Description { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00004AF4 File Offset: 0x00002CF4
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00004AFC File Offset: 0x00002CFC
		public bool IsActive { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00004B05 File Offset: 0x00002D05
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00004B0D File Offset: 0x00002D0D
		public string EventType { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00004B16 File Offset: 0x00002D16
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00004B1E File Offset: 0x00002D1E
		public DateTime LastRunTime { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00004B27 File Offset: 0x00002D27
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00004B2F File Offset: 0x00002D2F
		public string LastStatus { get; set; }
	}
}
