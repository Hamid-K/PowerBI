using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000095 RID: 149
	internal static class DiagnoisticsEventThrottlingDefaults
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001442A File Offset: 0x0001262A
		internal static bool IsInRangeThrottleAfterCount(this int throttleAfterCount)
		{
			return throttleAfterCount >= 1 && throttleAfterCount <= 10;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001443A File Offset: 0x0001263A
		internal static bool IsInRangeThrottlingRecycleInterval(this uint throttlingRecycleIntervalInMinutes)
		{
			return throttlingRecycleIntervalInMinutes >= 1U && throttlingRecycleIntervalInMinutes <= 60U;
		}

		// Token: 0x040001D6 RID: 470
		internal const int MinimalThrottleAfterCount = 1;

		// Token: 0x040001D7 RID: 471
		internal const int DefaultThrottleAfterCount = 5;

		// Token: 0x040001D8 RID: 472
		internal const int MaxThrottleAfterCount = 10;

		// Token: 0x040001D9 RID: 473
		internal const uint MinimalThrottlingRecycleIntervalInMinutes = 1U;

		// Token: 0x040001DA RID: 474
		internal const uint DefaultThrottlingRecycleIntervalInMinutes = 5U;

		// Token: 0x040001DB RID: 475
		internal const uint MaxThrottlingRecycleIntervalInMinutes = 60U;

		// Token: 0x040001DC RID: 476
		internal const int KeywordsExcludedFromEventThrottling = 2;
	}
}
