using System;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000225 RID: 549
	public enum ManagedIdentitySource
	{
		// Token: 0x0400099F RID: 2463
		None,
		// Token: 0x040009A0 RID: 2464
		Imds,
		// Token: 0x040009A1 RID: 2465
		AppService,
		// Token: 0x040009A2 RID: 2466
		AzureArc,
		// Token: 0x040009A3 RID: 2467
		CloudShell,
		// Token: 0x040009A4 RID: 2468
		ServiceFabric,
		// Token: 0x040009A5 RID: 2469
		DefaultToImds
	}
}
