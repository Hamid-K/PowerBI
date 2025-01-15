using System;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x0200011C RID: 284
	internal enum MsoIdAuthenticationError
	{
		// Token: 0x04000A11 RID: 2577
		Unknown,
		// Token: 0x04000A12 RID: 2578
		NotInstalled,
		// Token: 0x04000A13 RID: 2579
		LoadFailure,
		// Token: 0x04000A14 RID: 2580
		AlreadyInitialized,
		// Token: 0x04000A15 RID: 2581
		InitFailure,
		// Token: 0x04000A16 RID: 2582
		NotInitialized,
		// Token: 0x04000A17 RID: 2583
		ResetFailure,
		// Token: 0x04000A18 RID: 2584
		SsoWithNonDomainUser,
		// Token: 0x04000A19 RID: 2585
		MissingPassword,
		// Token: 0x04000A1A RID: 2586
		OperationalError,
		// Token: 0x04000A1B RID: 2587
		SsoAuthenticationFailed,
		// Token: 0x04000A1C RID: 2588
		AuthenticationFailed
	}
}
