using System;

namespace Microsoft.HostIntegration.DrdaClient.Packages
{
	// Token: 0x02000A45 RID: 2629
	public enum OptionsPackageExecuteAuthorization
	{
		// Token: 0x0400409B RID: 16539
		Requester,
		// Token: 0x0400409C RID: 16540
		Owner,
		// Token: 0x0400409D RID: 16541
		Invoker,
		// Token: 0x0400409E RID: 16542
		Definer,
		// Token: 0x0400409F RID: 16543
		InvokerRevertToRequester,
		// Token: 0x040040A0 RID: 16544
		InvokerRevertToOwner,
		// Token: 0x040040A1 RID: 16545
		DefinerRevertToRequester,
		// Token: 0x040040A2 RID: 16546
		DefinerRevertToOwner
	}
}
