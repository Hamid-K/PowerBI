using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000635 RID: 1589
	[Flags]
	public enum UserProfileState
	{
		// Token: 0x04002DEE RID: 11758
		None = 0,
		// Token: 0x04002DEF RID: 11759
		InQuery = 1,
		// Token: 0x04002DF0 RID: 11760
		InReport = 2,
		// Token: 0x04002DF1 RID: 11761
		Both = 3,
		// Token: 0x04002DF2 RID: 11762
		OnDemandExpressions = 8
	}
}
