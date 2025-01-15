using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000015 RID: 21
	public enum ImpersonationMode
	{
		// Token: 0x04000052 RID: 82
		Default = 1,
		// Token: 0x04000053 RID: 83
		ImpersonateAccount,
		// Token: 0x04000054 RID: 84
		ImpersonateAnonymous,
		// Token: 0x04000055 RID: 85
		ImpersonateCurrentUser,
		// Token: 0x04000056 RID: 86
		ImpersonateServiceAccount,
		// Token: 0x04000057 RID: 87
		ImpersonateUnattendedAccount
	}
}
