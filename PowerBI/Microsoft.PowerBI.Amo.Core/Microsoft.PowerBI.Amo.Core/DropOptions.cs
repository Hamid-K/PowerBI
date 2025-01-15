using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000081 RID: 129
	[Flags]
	public enum DropOptions
	{
		// Token: 0x04000443 RID: 1091
		Default = 0,
		// Token: 0x04000444 RID: 1092
		IgnoreFailures = 1,
		// Token: 0x04000445 RID: 1093
		AlterOrDeleteDependents = 2
	}
}
