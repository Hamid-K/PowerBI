using System;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000127 RID: 295
	internal enum MsoIdAuthenticationError
	{
		// Token: 0x04000A58 RID: 2648
		Unknown,
		// Token: 0x04000A59 RID: 2649
		NotInstalled,
		// Token: 0x04000A5A RID: 2650
		LoadFailure,
		// Token: 0x04000A5B RID: 2651
		AlreadyInitialized,
		// Token: 0x04000A5C RID: 2652
		InitFailure,
		// Token: 0x04000A5D RID: 2653
		NotInitialized,
		// Token: 0x04000A5E RID: 2654
		ResetFailure,
		// Token: 0x04000A5F RID: 2655
		SsoWithNonDomainUser,
		// Token: 0x04000A60 RID: 2656
		MissingPassword,
		// Token: 0x04000A61 RID: 2657
		OperationalError,
		// Token: 0x04000A62 RID: 2658
		SsoAuthenticationFailed,
		// Token: 0x04000A63 RID: 2659
		AuthenticationFailed
	}
}
