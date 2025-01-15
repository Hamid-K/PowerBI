using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000648 RID: 1608
	public enum ConnectionSecurity
	{
		// Token: 0x04002E59 RID: 11865
		UseIntegratedSecurity,
		// Token: 0x04002E5A RID: 11866
		ImpersonateWindowsUser,
		// Token: 0x04002E5B RID: 11867
		UseDataSourceCredentials,
		// Token: 0x04002E5C RID: 11868
		None,
		// Token: 0x04002E5D RID: 11869
		ImpersonateServiceAccount
	}
}
