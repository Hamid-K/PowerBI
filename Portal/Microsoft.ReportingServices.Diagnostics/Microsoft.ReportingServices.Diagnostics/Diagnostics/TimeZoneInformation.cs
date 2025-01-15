using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000016 RID: 22
	public sealed class TimeZoneInformation
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000028B8 File Offset: 0x00000AB8
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000028C0 File Offset: 0x00000AC0
		public int Bias
		{
			get
			{
				return this.m_bias;
			}
			set
			{
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000028C2 File Offset: 0x00000AC2
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000028C0 File Offset: 0x00000AC0
		public int StandardBias
		{
			get
			{
				return this.m_standardBias;
			}
			set
			{
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000028CA File Offset: 0x00000ACA
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000028C0 File Offset: 0x00000AC0
		public TimeZoneInformation.SYSTEMTIME StandardDate
		{
			get
			{
				return this.m_standardDate;
			}
			set
			{
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000028D2 File Offset: 0x00000AD2
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000028C0 File Offset: 0x00000AC0
		public int DaylightBias
		{
			get
			{
				return this.m_daylightBias;
			}
			set
			{
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000028DA File Offset: 0x00000ADA
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000028C0 File Offset: 0x00000AC0
		public TimeZoneInformation.SYSTEMTIME DaylightDate
		{
			get
			{
				return this.m_daylightDate;
			}
			set
			{
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000028E4 File Offset: 0x00000AE4
		private TimeZoneInformation()
		{
			TIME_ZONE_INFORMATION_Introp time_ZONE_INFORMATION_Introp = default(TIME_ZONE_INFORMATION_Introp);
			TimeZoneUtil.GetTimeZoneInformation(ref time_ZONE_INFORMATION_Introp);
			this.m_bias = time_ZONE_INFORMATION_Introp.bias;
			SYSTEMTIME_Introp systemtime_Introp = time_ZONE_INFORMATION_Introp.standardDate;
			this.m_standardDate = new TimeZoneInformation.SYSTEMTIME
			{
				year = systemtime_Introp.year,
				month = systemtime_Introp.month,
				dayOfWeek = systemtime_Introp.dayOfWeek,
				day = systemtime_Introp.day,
				hour = systemtime_Introp.hour,
				minute = systemtime_Introp.minute,
				second = systemtime_Introp.second,
				milliseconds = systemtime_Introp.milliseconds
			};
			systemtime_Introp = time_ZONE_INFORMATION_Introp.daylightDate;
			this.m_daylightDate = new TimeZoneInformation.SYSTEMTIME
			{
				year = systemtime_Introp.year,
				month = systemtime_Introp.month,
				dayOfWeek = systemtime_Introp.dayOfWeek,
				day = systemtime_Introp.day,
				hour = systemtime_Introp.hour,
				minute = systemtime_Introp.minute,
				second = systemtime_Introp.second,
				milliseconds = systemtime_Introp.milliseconds
			};
			this.m_standardBias = time_ZONE_INFORMATION_Introp.standardBias;
			this.m_daylightBias = time_ZONE_INFORMATION_Introp.daylightBias;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A0E File Offset: 0x00000C0E
		public static TimeZoneInformation Current
		{
			get
			{
				return TimeZoneInformation.current;
			}
		}

		// Token: 0x04000045 RID: 69
		private int m_bias;

		// Token: 0x04000046 RID: 70
		private int m_standardBias;

		// Token: 0x04000047 RID: 71
		private TimeZoneInformation.SYSTEMTIME m_standardDate;

		// Token: 0x04000048 RID: 72
		private int m_daylightBias;

		// Token: 0x04000049 RID: 73
		private TimeZoneInformation.SYSTEMTIME m_daylightDate;

		// Token: 0x0400004A RID: 74
		private static readonly TimeZoneInformation current = new TimeZoneInformation();

		// Token: 0x02000091 RID: 145
		public sealed class SYSTEMTIME
		{
			// Token: 0x04000379 RID: 889
			public short year;

			// Token: 0x0400037A RID: 890
			public short month;

			// Token: 0x0400037B RID: 891
			public short dayOfWeek;

			// Token: 0x0400037C RID: 892
			public short day;

			// Token: 0x0400037D RID: 893
			public short hour;

			// Token: 0x0400037E RID: 894
			public short minute;

			// Token: 0x0400037F RID: 895
			public short second;

			// Token: 0x04000380 RID: 896
			public short milliseconds;
		}
	}
}
