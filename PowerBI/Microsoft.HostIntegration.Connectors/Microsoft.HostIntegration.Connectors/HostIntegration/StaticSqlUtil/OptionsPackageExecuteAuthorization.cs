using System;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A6B RID: 2667
	public enum OptionsPackageExecuteAuthorization
	{
		// Token: 0x04004187 RID: 16775
		Requester,
		// Token: 0x04004188 RID: 16776
		Owner,
		// Token: 0x04004189 RID: 16777
		Invoker,
		// Token: 0x0400418A RID: 16778
		Definer,
		// Token: 0x0400418B RID: 16779
		InvokerRevertToRequester,
		// Token: 0x0400418C RID: 16780
		InvokerRevertToOwner,
		// Token: 0x0400418D RID: 16781
		DefinerRevertToRequester,
		// Token: 0x0400418E RID: 16782
		DefinerRevertToOwner
	}
}
