using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001F RID: 31
	public sealed class CloudConnectionAuthenticationProperties
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008BAE File Offset: 0x00006DAE
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00008BA5 File Offset: 0x00006DA5
		public string Authority { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008BBF File Offset: 0x00006DBF
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00008BB6 File Offset: 0x00006DB6
		public string ResourceId { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008BD0 File Offset: 0x00006DD0
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00008BC7 File Offset: 0x00006DC7
		public bool IsCommonTenant { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00008BE1 File Offset: 0x00006DE1
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public string ManagedTenantNameOrId { get; set; }
	}
}
