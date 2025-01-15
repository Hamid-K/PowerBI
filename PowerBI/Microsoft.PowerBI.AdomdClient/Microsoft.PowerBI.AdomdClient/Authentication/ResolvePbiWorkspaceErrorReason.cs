using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x0200010B RID: 267
	internal enum ResolvePbiWorkspaceErrorReason
	{
		// Token: 0x040008E2 RID: 2274
		None,
		// Token: 0x040008E3 RID: 2275
		WorkspaceNotFound,
		// Token: 0x040008E4 RID: 2276
		[Obsolete("Deprecated!")]
		WorkspaceNotOnPbiPremium,
		// Token: 0x040008E5 RID: 2277
		WorkspaceNameDuplicated
	}
}
