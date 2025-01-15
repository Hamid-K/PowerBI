using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001F RID: 31
	public sealed class CloudConnectionAuthenticationProperties
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008EAE File Offset: 0x000070AE
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00008EA5 File Offset: 0x000070A5
		public string Authority { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008EBF File Offset: 0x000070BF
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00008EB6 File Offset: 0x000070B6
		public string ResourceId { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008ED0 File Offset: 0x000070D0
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00008EC7 File Offset: 0x000070C7
		public bool IsCommonTenant { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008EE1 File Offset: 0x000070E1
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00008ED8 File Offset: 0x000070D8
		public string ManagedTenantNameOrId { get; set; }
	}
}
