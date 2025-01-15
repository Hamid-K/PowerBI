using System;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x02000127 RID: 295
	internal enum MsoIdAuthenticationError
	{
		// Token: 0x04000A4B RID: 2635
		Unknown,
		// Token: 0x04000A4C RID: 2636
		NotInstalled,
		// Token: 0x04000A4D RID: 2637
		LoadFailure,
		// Token: 0x04000A4E RID: 2638
		AlreadyInitialized,
		// Token: 0x04000A4F RID: 2639
		InitFailure,
		// Token: 0x04000A50 RID: 2640
		NotInitialized,
		// Token: 0x04000A51 RID: 2641
		ResetFailure,
		// Token: 0x04000A52 RID: 2642
		SsoWithNonDomainUser,
		// Token: 0x04000A53 RID: 2643
		MissingPassword,
		// Token: 0x04000A54 RID: 2644
		OperationalError,
		// Token: 0x04000A55 RID: 2645
		SsoAuthenticationFailed,
		// Token: 0x04000A56 RID: 2646
		AuthenticationFailed
	}
}
