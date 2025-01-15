using System;
using System.Globalization;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C0 RID: 704
	public static class ExtendedDateTime
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00041989 File Offset: 0x0003FB89
		public static bool IsSimulatingTime
		{
			get
			{
				return ExtendedDateTime.s_simulating;
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00041990 File Offset: 0x0003FB90
		public static void SetSimulatedTime(DateTime utcNow)
		{
			ExtendedDateTime.s_simulating = true;
			ExtendedDateTime.s_simulatedUtcNow = utcNow;
			if (ExtendedDateTime.s_simulatedTimeZoneInfo == null)
			{
				ExtendedDateTime.s_simulatedTimeZoneInfo = TimeZoneInfo.Local;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000419AF File Offset: 0x0003FBAF
		public static void StopSimulatingTime()
		{
			ExtendedDateTime.s_simulating = false;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000419B7 File Offset: 0x0003FBB7
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x000419BE File Offset: 0x0003FBBE
		[NotNull]
		public static TimeZoneInfo SimulatedTimeZoneInfo
		{
			get
			{
				return ExtendedDateTime.s_simulatedTimeZoneInfo;
			}
			set
			{
				ExtendedDiagnostics.EnsureNotNull<TimeZoneInfo>(value, "value (time zone info)");
				ExtendedDateTime.s_simulatedTimeZoneInfo = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x000419D1 File Offset: 0x0003FBD1
		public static DateTime Now
		{
			get
			{
				if (ExtendedDateTime.s_simulating)
				{
					return TimeZoneInfo.ConvertTimeFromUtc(ExtendedDateTime.s_simulatedUtcNow, ExtendedDateTime.s_simulatedTimeZoneInfo);
				}
				return DateTime.Now;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000419EF File Offset: 0x0003FBEF
		public static DateTime UtcNow
		{
			get
			{
				if (ExtendedDateTime.s_simulating)
				{
					return ExtendedDateTime.s_simulatedUtcNow;
				}
				return DateTime.UtcNow;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00041A04 File Offset: 0x0003FC04
		public static DateTime Today
		{
			get
			{
				if (ExtendedDateTime.s_simulating)
				{
					DateTime now = ExtendedDateTime.Now;
					return new DateTime(now.Year, now.Month, now.Day);
				}
				return DateTime.Today;
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00041A3E File Offset: 0x0003FC3E
		public static DateTime Max(DateTime t1, DateTime t2)
		{
			if (!(t1 > t2))
			{
				return t2;
			}
			return t1;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00041A4C File Offset: 0x0003FC4C
		public static DateTime Min(DateTime t1, DateTime t2)
		{
			if (!(t1 < t2))
			{
				return t2;
			}
			return t1;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00041A5C File Offset: 0x0003FC5C
		public static bool RangesIntersect(DateTime firstStart, DateTime firstEnd, DateTime secondStart, DateTime secondEnd)
		{
			return (firstStart >= secondStart && firstStart <= secondEnd) || (firstEnd >= secondStart && firstEnd <= secondEnd) || (secondStart >= firstStart && secondStart <= firstEnd) || (secondEnd >= firstStart && secondEnd <= firstEnd);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		public static DateTime ParseExactUtc(string value, string format)
		{
			string text = "{0}Z".FormatWithInvariantCulture(new object[] { format });
			return DateTime.ParseExact("{0}Z".FormatWithInvariantCulture(new object[] { value }), text, CultureInfo.InvariantCulture).ToUniversalTime();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00041B00 File Offset: 0x0003FD00
		public static DateTime ParseInexactUtc(string value)
		{
			if (string.Equals(value, "now", StringComparison.OrdinalIgnoreCase))
			{
				return ExtendedDateTime.UtcNow;
			}
			switch (value.Length)
			{
			case 4:
				return ExtendedDateTime.ParseExactUtc(value, "yyyy");
			case 6:
				return ExtendedDateTime.ParseExactUtc(value, "yyyyMM");
			case 8:
				return ExtendedDateTime.ParseExactUtc(value, "yyyyMMdd");
			case 10:
				return ExtendedDateTime.ParseExactUtc(value, "yyyyMMddHH");
			case 12:
				return ExtendedDateTime.ParseExactUtc(value, "yyyyMMddHHmm");
			}
			return ExtendedDateTime.ParseExactUtc(value, "yyyyMMddHHmmss");
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		public static TimeSpan ToUnixTimeSpan(DateTime date)
		{
			return date.ToUniversalTime().Subtract(ExtendedDateTime.c_epochStart);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00041BC4 File Offset: 0x0003FDC4
		public static string ToJsonDate(DateTime date)
		{
			return string.Format("/Date({0})/", (date.Ticks - ExtendedDateTime.c_epochStart.Ticks) / 10000L);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00041BFC File Offset: 0x0003FDFC
		public static DateTime FromJsonDate(string date)
		{
			if (string.IsNullOrEmpty(date))
			{
				return DateTime.MinValue;
			}
			string text = date.Replace("/Date(", "").Replace(")/", "");
			if (new TimeSpan(DateTime.MaxValue.Ticks - ExtendedDateTime.c_epochStart.Ticks).TotalMilliseconds.ToString().Equals(text))
			{
				return DateTime.MaxValue;
			}
			double num = 0.0;
			if (double.TryParse(text, out num))
			{
				return ExtendedDateTime.c_epochStart.AddMilliseconds(num);
			}
			return DateTime.MinValue;
		}

		// Token: 0x0400070E RID: 1806
		private static bool s_simulating;

		// Token: 0x0400070F RID: 1807
		private static DateTime s_simulatedUtcNow;

		// Token: 0x04000710 RID: 1808
		private static TimeZoneInfo s_simulatedTimeZoneInfo;

		// Token: 0x04000711 RID: 1809
		private static readonly DateTime c_epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
	}
}
