using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200007E RID: 126
	[Flags]
	internal enum QueryExpressionFeatures
	{
		// Token: 0x04000305 RID: 773
		None = 0,
		// Token: 0x04000306 RID: 774
		QueryMeasure = 2,
		// Token: 0x04000307 RID: 775
		ModelMeasure = 4,
		// Token: 0x04000308 RID: 776
		FieldReference = 8,
		// Token: 0x04000309 RID: 777
		RequiresCalculate = 16
	}
}
