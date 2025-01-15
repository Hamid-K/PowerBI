using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C3 RID: 707
	public static class TimeSpanExtension
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x00041EBC File Offset: 0x000400BC
		public static TimeSpan GenerateRandomTimeSpan(this TimeSpan start, TimeSpan end)
		{
			Random random = new Random();
			double num = ((end > start) ? (end - start).TotalSeconds : (start - end).TotalSeconds);
			int num2 = random.Next((int)num);
			return start.Add(TimeSpan.FromSeconds((double)num2));
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00041F0E File Offset: 0x0004010E
		public static bool IsValidAsTimerPeriod(this TimeSpan value)
		{
			return value.TotalMilliseconds != -1.0;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00041F28 File Offset: 0x00040128
		public static int AsIntTimeoutIfInRange(this TimeSpan timeout, string argName)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException(argName);
			}
			return (int)num;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00041F55 File Offset: 0x00040155
		public static TimeSpan InvalidTimerPeriod
		{
			get
			{
				return TimeSpanExtension.s_invalidTimerPeriod;
			}
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00041F5C File Offset: 0x0004015C
		public static TimeSpan Min(TimeSpan a, TimeSpan b)
		{
			if (!(a > b))
			{
				return a;
			}
			return b;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00041F6A File Offset: 0x0004016A
		public static TimeSpan Max(TimeSpan a, TimeSpan b)
		{
			if (!(a > b))
			{
				return b;
			}
			return a;
		}

		// Token: 0x04000714 RID: 1812
		private static readonly TimeSpan s_invalidTimerPeriod = TimeSpan.FromMilliseconds(-1.0);
	}
}
