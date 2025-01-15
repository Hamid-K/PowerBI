using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014D7 RID: 5335
	public enum LearnConfidenceReason
	{
		// Token: 0x04004232 RID: 16946
		None,
		// Token: 0x04004233 RID: 16947
		NoExamples,
		// Token: 0x04004234 RID: 16948
		CultureNotSupported,
		// Token: 0x04004235 RID: 16949
		OutputType,
		// Token: 0x04004236 RID: 16950
		TooManyColumns,
		// Token: 0x04004237 RID: 16951
		TooManyRows,
		// Token: 0x04004238 RID: 16952
		AllFormattedNumbers,
		// Token: 0x04004239 RID: 16953
		AllFormattedDateTimes
	}
}
