using System;

namespace Microsoft.PowerBI.Data.ModelSchemaAnalysis
{
	// Token: 0x0200000B RID: 11
	public enum MeasureCalculationKindResultStatus
	{
		// Token: 0x04000032 RID: 50
		Success,
		// Token: 0x04000033 RID: 51
		MeasureDoesNotExist,
		// Token: 0x04000034 RID: 52
		MeasureIsNotAccessible,
		// Token: 0x04000035 RID: 53
		MeasureHasInvalidExpression,
		// Token: 0x04000036 RID: 54
		MeasureReferencesAreTooDeep,
		// Token: 0x04000037 RID: 55
		FailedToParseExpression,
		// Token: 0x04000038 RID: 56
		Exception,
		// Token: 0x04000039 RID: 57
		MeasureHasUnknownExpression
	}
}
