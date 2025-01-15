using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000004 RID: 4
	[Guid("F97F7673-295F-4616-A99D-261B2731EC58")]
	[ComVisible(true)]
	public enum ResolvePbiWorkspaceErrorReason
	{
		// Token: 0x0400000C RID: 12
		None,
		// Token: 0x0400000D RID: 13
		WorkspaceNotFound,
		// Token: 0x0400000E RID: 14
		[Obsolete("Deprecated!")]
		WorkspaceNotOnPbiPremium,
		// Token: 0x0400000F RID: 15
		WorkspaceNameDuplicated
	}
}
