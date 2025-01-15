using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x02000007 RID: 7
	public static class CommandUtils
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023A8 File Offset: 0x000005A8
		public static CommandRequestExecutionMetricsKind ToMsolapMetricsKind(this RequestExecutionMetricsKind metricsKind)
		{
			CommandRequestExecutionMetricsKind commandRequestExecutionMetricsKind = CommandRequestExecutionMetricsKind.MSMD_EXECUTIONMETRICS_DEFAULT;
			CommandUtils.MapFlag(ref metricsKind, ref commandRequestExecutionMetricsKind, RequestExecutionMetricsKind.Basic, CommandRequestExecutionMetricsKind.MSMD_EXECUTIONMETRICS_BASIC);
			CommandUtils.MapFlag(ref metricsKind, ref commandRequestExecutionMetricsKind, RequestExecutionMetricsKind.QueryText, CommandRequestExecutionMetricsKind.MSMD_EXECUTIONMETRICS_QUERYTEXT);
			QueryContract.RetailAssert(metricsKind == RequestExecutionMetricsKind.None, "{0} contains unsupported value {1}", "metricsKind", metricsKind);
			return commandRequestExecutionMetricsKind;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023E7 File Offset: 0x000005E7
		private static void MapFlag(ref RequestExecutionMetricsKind flags, ref CommandRequestExecutionMetricsKind msolapFlags, RequestExecutionMetricsKind targetFlag, CommandRequestExecutionMetricsKind targetMsolapFlag)
		{
			if (flags.HasFlag(targetFlag))
			{
				msolapFlags |= targetMsolapFlag;
				flags &= ~targetFlag;
			}
		}
	}
}
