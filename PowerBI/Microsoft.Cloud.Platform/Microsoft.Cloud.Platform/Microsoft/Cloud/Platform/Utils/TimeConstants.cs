using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BE RID: 702
	public static class TimeConstants
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x0004189C File Offset: 0x0003FA9C
		public static bool InRange(int start, int end, int lowerBound, int upperBound, int value)
		{
			if (start > end)
			{
				return (value >= lowerBound && value <= end) || (value >= start && value <= upperBound);
			}
			return value >= start && value <= end;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000418CD File Offset: 0x0003FACD
		public static int Normalize(int value, int upperBound)
		{
			value %= upperBound;
			if (value < 0)
			{
				value = upperBound + value;
			}
			return value;
		}

		// Token: 0x040006FF RID: 1791
		public const int HoursInDay = 24;

		// Token: 0x04000700 RID: 1792
		public const int DaysInWeek = 7;

		// Token: 0x04000701 RID: 1793
		public const int HoursInWeek = 168;

		// Token: 0x04000702 RID: 1794
		public const int MinutesInHour = 60;

		// Token: 0x04000703 RID: 1795
		public const int MinutesInDay = 1440;

		// Token: 0x04000704 RID: 1796
		public const int SecondsInMinute = 60;

		// Token: 0x04000705 RID: 1797
		public const int SecondsInHour = 3600;

		// Token: 0x04000706 RID: 1798
		public const int SecondsInDay = 86400;

		// Token: 0x04000707 RID: 1799
		public const int MillisecondsInSecond = 1000;

		// Token: 0x04000708 RID: 1800
		public const int MillisecondsInMinute = 60000;

		// Token: 0x04000709 RID: 1801
		public const int MillisecondsInDay = 86400000;

		// Token: 0x0400070A RID: 1802
		public const int MinUtcOffset = -720;

		// Token: 0x0400070B RID: 1803
		public const int MaxUtcOffset = 841;

		// Token: 0x0400070C RID: 1804
		public static readonly TimeSpan InfiniteTimeSpan = TimeSpan.FromMilliseconds(-1.0);
	}
}
