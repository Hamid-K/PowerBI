using System;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000072 RID: 114
	public enum PrincipalType
	{
		// Token: 0x04000270 RID: 624
		User = 1,
		// Token: 0x04000271 RID: 625
		Group,
		// Token: 0x04000272 RID: 626
		App,
		// Token: 0x04000273 RID: 627
		PbiPaasWorkspace,
		// Token: 0x04000274 RID: 628
		PbiPaasCollection,
		// Token: 0x04000275 RID: 629
		Service,
		// Token: 0x04000276 RID: 630
		ServicePrincipalProfile
	}
}
