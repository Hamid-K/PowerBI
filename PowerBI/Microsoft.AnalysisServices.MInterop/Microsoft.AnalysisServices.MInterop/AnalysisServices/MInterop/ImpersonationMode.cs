using System;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000008 RID: 8
	public enum ImpersonationMode
	{
		// Token: 0x0400004D RID: 77
		Default = 1,
		// Token: 0x0400004E RID: 78
		Account,
		// Token: 0x0400004F RID: 79
		CurrentUser = 4,
		// Token: 0x04000050 RID: 80
		ServiceAccount,
		// Token: 0x04000051 RID: 81
		UnattendedAccount,
		// Token: 0x04000052 RID: 82
		KerberosS4U
	}
}
