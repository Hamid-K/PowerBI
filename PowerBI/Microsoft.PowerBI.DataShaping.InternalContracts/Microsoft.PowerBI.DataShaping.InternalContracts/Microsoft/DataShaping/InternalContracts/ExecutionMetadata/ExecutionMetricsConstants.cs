using System;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x0200002A RID: 42
	public static class ExecutionMetricsConstants
	{
		// Token: 0x0400007A RID: 122
		public const string DSE = "DSE";

		// Token: 0x0400007B RID: 123
		public const string ExecuteSemanticQuery = "Execute Semantic Query";

		// Token: 0x0400007C RID: 124
		public const string ExecuteDaxQuery = "Execute DAX Query";

		// Token: 0x0400007D RID: 125
		public const string MetricsTruncated = "Metrics Truncated";

		// Token: 0x0400007E RID: 126
		public const string OpenConnection = "Open Connection";

		// Token: 0x0400007F RID: 127
		public const string QueryText = "QueryText";

		// Token: 0x04000080 RID: 128
		public const string RowCount = "RowCount";

		// Token: 0x04000081 RID: 129
		public const string Error = "Error";

		// Token: 0x04000082 RID: 130
		public const string Canceled = "Canceled";

		// Token: 0x04000083 RID: 131
		public const int DefaultMaxExecutionEventsPerQuery = 200;

		// Token: 0x04000084 RID: 132
		public const int DefaultMaxExecutionEvents = 300;
	}
}
