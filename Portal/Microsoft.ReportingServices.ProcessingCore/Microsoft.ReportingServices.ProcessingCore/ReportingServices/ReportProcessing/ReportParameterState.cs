using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000612 RID: 1554
	public enum ReportParameterState
	{
		// Token: 0x04002D53 RID: 11603
		HasValidValue,
		// Token: 0x04002D54 RID: 11604
		InvalidValueProvided,
		// Token: 0x04002D55 RID: 11605
		DefaultValueInvalid,
		// Token: 0x04002D56 RID: 11606
		MissingValidValue,
		// Token: 0x04002D57 RID: 11607
		HasOutstandingDependencies,
		// Token: 0x04002D58 RID: 11608
		DynamicValuesUnavailable
	}
}
