using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000060 RID: 96
	internal static class KpiAggregation
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0001156C File Offset: 0x0000F76C
		internal static string ToRdlFunctionName(KpiSharedDataItemAggregation aggregation)
		{
			switch (aggregation)
			{
			case KpiSharedDataItemAggregation.First:
				return "First";
			case KpiSharedDataItemAggregation.Last:
				return "Last";
			case KpiSharedDataItemAggregation.Min:
				return "Min";
			case KpiSharedDataItemAggregation.Max:
				return "Max";
			case KpiSharedDataItemAggregation.Average:
				return "Avg";
			case KpiSharedDataItemAggregation.Sum:
				return "Sum";
			default:
				return string.Empty;
			}
		}
	}
}
