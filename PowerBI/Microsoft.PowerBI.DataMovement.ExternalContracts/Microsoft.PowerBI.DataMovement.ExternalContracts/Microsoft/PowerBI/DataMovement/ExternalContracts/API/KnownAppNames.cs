using System;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200005D RID: 93
	[Flags]
	public enum KnownAppNames
	{
		// Token: 0x04000224 RID: 548
		None = 0,
		// Token: 0x04000225 RID: 549
		PowerBI = 1,
		// Token: 0x04000226 RID: 550
		PowerApps = 2,
		// Token: 0x04000227 RID: 551
		LogicApps = 4,
		// Token: 0x04000228 RID: 552
		PowerBIPaaS = 8,
		// Token: 0x04000229 RID: 553
		AsAzure = 16,
		// Token: 0x0400022A RID: 554
		PowerPlatformAdminCenter = 32,
		// Token: 0x0400022B RID: 555
		AzureSearch = 64,
		// Token: 0x0400022C RID: 556
		DataMovement = 128,
		// Token: 0x0400022D RID: 557
		PowerQuery = 256
	}
}
