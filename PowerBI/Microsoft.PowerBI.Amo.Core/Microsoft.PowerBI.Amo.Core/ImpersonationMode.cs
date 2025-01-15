using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000092 RID: 146
	public enum ImpersonationMode
	{
		// Token: 0x0400047D RID: 1149
		Default,
		// Token: 0x0400047E RID: 1150
		ImpersonateServiceAccount,
		// Token: 0x0400047F RID: 1151
		ImpersonateAnonymous,
		// Token: 0x04000480 RID: 1152
		ImpersonateCurrentUser,
		// Token: 0x04000481 RID: 1153
		ImpersonateAccount,
		// Token: 0x04000482 RID: 1154
		ImpersonateUnattendedAccount
	}
}
