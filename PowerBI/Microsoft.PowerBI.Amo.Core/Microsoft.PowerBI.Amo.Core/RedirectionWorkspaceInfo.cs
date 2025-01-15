using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200001C RID: 28
	internal struct RedirectionWorkspaceInfo
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005854 File Offset: 0x00003A54
		public RedirectionWorkspaceInfo(string tenantId, string aasInstance, string pbiEndpoint, string pbiResourceId)
		{
			this.TenantId = tenantId;
			this.AasInstance = aasInstance;
			this.PbiEndpoint = pbiEndpoint;
			this.PbiResourceId = pbiResourceId;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00005873 File Offset: 0x00003A73
		public string TenantId { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000587B File Offset: 0x00003A7B
		public string AasInstance { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005883 File Offset: 0x00003A83
		public string PbiEndpoint { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000588B File Offset: 0x00003A8B
		public string PbiResourceId { get; }
	}
}
