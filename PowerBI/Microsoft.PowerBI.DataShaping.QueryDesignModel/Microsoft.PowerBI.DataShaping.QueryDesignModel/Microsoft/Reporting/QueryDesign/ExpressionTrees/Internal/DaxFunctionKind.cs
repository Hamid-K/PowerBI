using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001D3 RID: 467
	public enum DaxFunctionKind
	{
		// Token: 0x04000C13 RID: 3091
		BinaryMinMax,
		// Token: 0x04000C14 RID: 3092
		GroupBy,
		// Token: 0x04000C15 RID: 3093
		IsEmpty,
		// Token: 0x04000C16 RID: 3094
		SelectColumns,
		// Token: 0x04000C17 RID: 3095
		SummarizeColumns,
		// Token: 0x04000C18 RID: 3096
		Divide,
		// Token: 0x04000C19 RID: 3097
		IsOnOrAfter,
		// Token: 0x04000C1A RID: 3098
		IsAfter,
		// Token: 0x04000C1B RID: 3099
		TreatAs,
		// Token: 0x04000C1C RID: 3100
		StringMinMax,
		// Token: 0x04000C1D RID: 3101
		SampleAxisWithLocalMinMax,
		// Token: 0x04000C1E RID: 3102
		OptimizedNotInOperator,
		// Token: 0x04000C1F RID: 3103
		NonVisual,
		// Token: 0x04000C20 RID: 3104
		FormatByLocale
	}
}
