using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C4 RID: 708
	public static class DateTimeExtension
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x00041F90 File Offset: 0x00040190
		public static DateTime Round(this DateTime dateTime, RoundTo roundTo)
		{
			switch (roundTo)
			{
			case RoundTo.Second:
				return dateTime.Round(TimeSpan.FromSeconds(1.0));
			case RoundTo.Minute:
				return dateTime.Round(TimeSpan.FromMinutes(1.0));
			case RoundTo.Hour:
				return dateTime.Round(TimeSpan.FromHours(1.0));
			case RoundTo.Day:
				return dateTime.Round(TimeSpan.FromDays(1.0));
			default:
				return dateTime;
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004200C File Offset: 0x0004020C
		public static DateTime Round(this DateTime dateTime, TimeSpan roundTo)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(roundTo.Ticks, "roundTo");
			if (roundTo.Ticks == 1L)
			{
				return dateTime;
			}
			if (roundTo.Ticks == 2L && dateTime.Ticks % 2L == 0L)
			{
				return dateTime;
			}
			return new DateTime((dateTime.Ticks + roundTo.Ticks / 2L + 1L) / roundTo.Ticks * roundTo.Ticks, dateTime.Kind);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00042081 File Offset: 0x00040281
		public static DateTime Floor(this DateTime dateTime, TimeSpan timeSpan)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(timeSpan.Ticks, "timeSpan");
			return new DateTime(dateTime.Ticks / timeSpan.Ticks * timeSpan.Ticks, dateTime.Kind);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000420B8 File Offset: 0x000402B8
		public static DateTime Ceiling(this DateTime dateTime, TimeSpan timeSpan)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(timeSpan.Ticks, "timeSpan");
			return new DateTime((dateTime.Ticks + timeSpan.Ticks - 1L) / timeSpan.Ticks * timeSpan.Ticks, dateTime.Kind);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00042104 File Offset: 0x00040304
		public static DateTimeOffset FromUnixTimeSeconds(this long seconds)
		{
			return new DateTimeOffset(DateTimeExtension.epoch.AddSeconds((double)seconds));
		}

		// Token: 0x04000715 RID: 1813
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
