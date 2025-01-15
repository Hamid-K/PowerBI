using System;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002CF RID: 719
	internal abstract class AbstractAcquireTokenByUsernameParameters
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00056E83 File Offset: 0x00055083
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x00056E8B File Offset: 0x0005508B
		public string Username { get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x00056E94 File Offset: 0x00055094
		// (set) Token: 0x06001AD2 RID: 6866 RVA: 0x00056E9C File Offset: 0x0005509C
		public string FederationMetadata { get; set; }
	}
}
