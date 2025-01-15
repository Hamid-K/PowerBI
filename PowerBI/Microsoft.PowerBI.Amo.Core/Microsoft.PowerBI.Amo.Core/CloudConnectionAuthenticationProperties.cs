using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000037 RID: 55
	public sealed class CloudConnectionAuthenticationProperties
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000BDAA File Offset: 0x00009FAA
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000BDA1 File Offset: 0x00009FA1
		public string Authority { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000BDBB File Offset: 0x00009FBB
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000BDB2 File Offset: 0x00009FB2
		public string ResourceId { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000BDCC File Offset: 0x00009FCC
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000BDC3 File Offset: 0x00009FC3
		public bool IsCommonTenant { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000BDDD File Offset: 0x00009FDD
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		public string ManagedTenantNameOrId { get; set; }
	}
}
