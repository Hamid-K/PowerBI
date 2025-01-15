using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000020 RID: 32
	public static class MetricKinds
	{
		// Token: 0x0400004C RID: 76
		public const string ConfusionMatrix = "ConfusionMatrix";

		// Token: 0x0400004D RID: 77
		public const string OverallMetrics = "OverallMetrics";

		// Token: 0x0400004E RID: 78
		public const string Warnings = "Warnings";

		// Token: 0x02000021 RID: 33
		public sealed class ColumnNames
		{
			// Token: 0x0400004F RID: 79
			public const string WarningText = "WarningText";

			// Token: 0x04000050 RID: 80
			public const string IsWeighted = "IsWeighted";

			// Token: 0x04000051 RID: 81
			public const string Count = "Count";

			// Token: 0x04000052 RID: 82
			public const string Weight = "Weight";

			// Token: 0x04000053 RID: 83
			public const string StratCol = "StratCol";

			// Token: 0x04000054 RID: 84
			public const string StratVal = "StratVal";

			// Token: 0x04000055 RID: 85
			public const string FoldIndex = "Fold Index";
		}
	}
}
