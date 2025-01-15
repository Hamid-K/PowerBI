using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000010 RID: 16
	internal enum ConnectionSecurity
	{
		// Token: 0x04000048 RID: 72
		UseIntegratedSecurity,
		// Token: 0x04000049 RID: 73
		ImpersonateWindowsUser,
		// Token: 0x0400004A RID: 74
		UseDataSourceCredentials,
		// Token: 0x0400004B RID: 75
		None,
		// Token: 0x0400004C RID: 76
		ImpersonateServiceAccount
	}
}
