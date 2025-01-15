using System;

namespace Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing
{
	// Token: 0x02000007 RID: 7
	internal static class ForecastConstants
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
		internal static string ToErrorCode(this ForecastErrorType result)
		{
			return result.GetType().Name + "." + result.ToString();
		}

		// Token: 0x0400004C RID: 76
		internal const uint cMinimalInputSize = 2U;

		// Token: 0x0400004D RID: 77
		internal const double dblDefaultConfidenceLevel = 0.95;

		// Token: 0x0400004E RID: 78
		internal const uint nNoSeasonality = 0U;

		// Token: 0x0400004F RID: 79
		internal const uint nAutoSeasonality = 1U;

		// Token: 0x04000050 RID: 80
		internal const uint nMaxSeasonality = 8784U;

		// Token: 0x04000051 RID: 81
		internal const uint nSeasonalityLimit = 8785U;

		// Token: 0x0200000D RID: 13
		public enum DupAggrPolicy
		{
			// Token: 0x040000A6 RID: 166
			dapNoDuplicated,
			// Token: 0x040000A7 RID: 167
			dapAverage,
			// Token: 0x040000A8 RID: 168
			dapCount,
			// Token: 0x040000A9 RID: 169
			dapCountA,
			// Token: 0x040000AA RID: 170
			dapMax,
			// Token: 0x040000AB RID: 171
			dapMedian,
			// Token: 0x040000AC RID: 172
			dapMin,
			// Token: 0x040000AD RID: 173
			dapSum,
			// Token: 0x040000AE RID: 174
			dapLimit
		}

		// Token: 0x0200000E RID: 14
		public enum CompletionPolicy
		{
			// Token: 0x040000B0 RID: 176
			cpTreatAsZeroes,
			// Token: 0x040000B1 RID: 177
			cpInterpolateCompletionValue,
			// Token: 0x040000B2 RID: 178
			cpLimit
		}
	}
}
