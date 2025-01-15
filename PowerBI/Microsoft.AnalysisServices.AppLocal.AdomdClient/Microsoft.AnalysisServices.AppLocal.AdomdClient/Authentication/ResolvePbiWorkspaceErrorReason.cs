using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x0200010B RID: 267
	internal enum ResolvePbiWorkspaceErrorReason
	{
		// Token: 0x040008EF RID: 2287
		None,
		// Token: 0x040008F0 RID: 2288
		WorkspaceNotFound,
		// Token: 0x040008F1 RID: 2289
		[Obsolete("Deprecated!")]
		WorkspaceNotOnPbiPremium,
		// Token: 0x040008F2 RID: 2290
		WorkspaceNameDuplicated
	}
}
