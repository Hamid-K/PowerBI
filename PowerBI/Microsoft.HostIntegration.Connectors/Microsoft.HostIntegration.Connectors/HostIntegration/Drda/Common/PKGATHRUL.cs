using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007A5 RID: 1957
	public enum PKGATHRUL : byte
	{
		// Token: 0x04002955 RID: 10581
		REQUESTER,
		// Token: 0x04002956 RID: 10582
		OWNER,
		// Token: 0x04002957 RID: 10583
		INVOKER_REVERT_TO_REQUESTER,
		// Token: 0x04002958 RID: 10584
		INVOKER_REVERT_TO_OWNER,
		// Token: 0x04002959 RID: 10585
		DEFINER_REVERT_TO_REQUESTER,
		// Token: 0x0400295A RID: 10586
		DEFINER_REVERT_TO_OWNER
	}
}
