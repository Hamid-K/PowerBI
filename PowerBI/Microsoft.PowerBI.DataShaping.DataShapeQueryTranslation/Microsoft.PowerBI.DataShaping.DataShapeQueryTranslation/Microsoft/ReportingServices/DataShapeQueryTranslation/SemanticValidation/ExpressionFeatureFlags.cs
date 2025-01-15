using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.SemanticValidation
{
	// Token: 0x02000064 RID: 100
	[Flags]
	internal enum ExpressionFeatureFlags
	{
		// Token: 0x0400027A RID: 634
		None = 0,
		// Token: 0x0400027B RID: 635
		ModelReferences = 2,
		// Token: 0x0400027C RID: 636
		CalculationReferences = 4,
		// Token: 0x0400027D RID: 637
		ProcessingFunctions = 8,
		// Token: 0x0400027E RID: 638
		AggregateFunctions = 16,
		// Token: 0x0400027F RID: 639
		Evaluate = 32,
		// Token: 0x04000280 RID: 640
		FilterConditionReferences = 64,
		// Token: 0x04000281 RID: 641
		BinaryOrImageFieldReference = 128,
		// Token: 0x04000282 RID: 642
		Subtotal = 256,
		// Token: 0x04000283 RID: 643
		EvaluateWithRollup = 512,
		// Token: 0x04000284 RID: 644
		ScopeReference = 1024,
		// Token: 0x04000285 RID: 645
		DataTableFunctions = 2048,
		// Token: 0x04000286 RID: 646
		EvaluateWithScope = 8192
	}
}
