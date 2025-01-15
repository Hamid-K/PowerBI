using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000613 RID: 1555
	internal enum ReportParameterDependencyState
	{
		// Token: 0x04002D5A RID: 11610
		AllDependenciesSpecified,
		// Token: 0x04002D5B RID: 11611
		HasOutstandingDependencies,
		// Token: 0x04002D5C RID: 11612
		MissingUpstreamDataSourcePrompt
	}
}
