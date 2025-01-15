using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200001A RID: 26
	public enum PartitionSourceType
	{
		// Token: 0x04000073 RID: 115
		Query = 1,
		// Token: 0x04000074 RID: 116
		Calculated,
		// Token: 0x04000075 RID: 117
		None,
		// Token: 0x04000076 RID: 118
		[CompatibilityRequirement("1400")]
		M,
		// Token: 0x04000077 RID: 119
		[CompatibilityRequirement("1400")]
		Entity,
		// Token: 0x04000078 RID: 120
		[CompatibilityRequirement("1450")]
		PolicyRange,
		// Token: 0x04000079 RID: 121
		[CompatibilityRequirement("1470")]
		CalculationGroup,
		// Token: 0x0400007A RID: 122
		[CompatibilityRequirement("1563")]
		Inferred,
		// Token: 0x0400007B RID: 123
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Internal")]
		Parquet
	}
}
