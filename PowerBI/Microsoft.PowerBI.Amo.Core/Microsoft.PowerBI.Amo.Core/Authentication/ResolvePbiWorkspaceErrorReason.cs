using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x02000100 RID: 256
	internal enum ResolvePbiWorkspaceErrorReason
	{
		// Token: 0x040008A8 RID: 2216
		None,
		// Token: 0x040008A9 RID: 2217
		WorkspaceNotFound,
		// Token: 0x040008AA RID: 2218
		[Obsolete("Deprecated!")]
		WorkspaceNotOnPbiPremium,
		// Token: 0x040008AB RID: 2219
		WorkspaceNameDuplicated
	}
}
