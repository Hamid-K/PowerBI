using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000040 RID: 64
	[Flags]
	public enum QueryCompilationOptions
	{
		// Token: 0x0400014B RID: 331
		None = 0,
		// Token: 0x0400014C RID: 332
		ReplaceParameterRefs = 1,
		// Token: 0x0400014D RID: 333
		ResolveStaticFunctions = 2,
		// Token: 0x0400014E RID: 334
		Normalize = 4,
		// Token: 0x0400014F RID: 335
		Subset = 8
	}
}
