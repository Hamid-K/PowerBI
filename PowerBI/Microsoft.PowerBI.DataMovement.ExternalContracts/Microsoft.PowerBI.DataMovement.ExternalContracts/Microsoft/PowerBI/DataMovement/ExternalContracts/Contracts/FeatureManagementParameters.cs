using System;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Contracts
{
	// Token: 0x02000022 RID: 34
	public sealed class FeatureManagementParameters
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002D5A File Offset: 0x00000F5A
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002D62 File Offset: 0x00000F62
		public string TenantObjectId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002D6B File Offset: 0x00000F6B
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002D73 File Offset: 0x00000F73
		public string UserObjectId { get; set; }
	}
}
