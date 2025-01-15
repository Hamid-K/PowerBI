using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000019 RID: 25
	public enum ObjectState
	{
		// Token: 0x0400006A RID: 106
		Ready = 1,
		// Token: 0x0400006B RID: 107
		NoData = 3,
		// Token: 0x0400006C RID: 108
		CalculationNeeded,
		// Token: 0x0400006D RID: 109
		SemanticError,
		// Token: 0x0400006E RID: 110
		EvaluationError,
		// Token: 0x0400006F RID: 111
		DependencyError,
		// Token: 0x04000070 RID: 112
		Incomplete,
		// Token: 0x04000071 RID: 113
		[CompatibilityRequirement(Pbi = "1400")]
		ForceCalculationNeeded = 10
	}
}
