using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000008 RID: 8
	internal struct RedirectionWorkspaceInfo
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000239C File Offset: 0x0000059C
		public RedirectionWorkspaceInfo(string tenantId, string aasInstance, string pbiEndpoint, string pbiResourceId)
		{
			this.TenantId = tenantId;
			this.AasInstance = aasInstance;
			this.PbiEndpoint = pbiEndpoint;
			this.PbiResourceId = pbiResourceId;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023BB File Offset: 0x000005BB
		public string TenantId { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023C3 File Offset: 0x000005C3
		public string AasInstance { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023CB File Offset: 0x000005CB
		public string PbiEndpoint { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023D3 File Offset: 0x000005D3
		public string PbiResourceId { get; }
	}
}
