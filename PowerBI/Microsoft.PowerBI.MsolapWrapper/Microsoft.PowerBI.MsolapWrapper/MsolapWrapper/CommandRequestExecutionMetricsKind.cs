using System;

namespace MsolapWrapper
{
	// Token: 0x02000022 RID: 34
	[Flags]
	public enum CommandRequestExecutionMetricsKind
	{
		// Token: 0x04000123 RID: 291
		MSMD_EXECUTIONMETRICS_DEFAULT = 0,
		// Token: 0x04000124 RID: 292
		MSMD_EXECUTIONMETRICS_BASIC = 1,
		// Token: 0x04000125 RID: 293
		MSMD_EXECUTIONMETRICS_QUERYTEXT = 2
	}
}
