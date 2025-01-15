using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000063 RID: 99
	internal class ScheduleStringsWrapper
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00002E32 File Offset: 0x00001032
		protected ScheduleStringsWrapper()
		{
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D143 File Offset: 0x0000B343
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000D14A File Offset: 0x0000B34A
		public static CultureInfo Culture
		{
			get
			{
				return ScheduleStringsWrapper.Keys.Culture;
			}
			set
			{
				ScheduleStringsWrapper.Keys.Culture = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000D152 File Offset: 0x0000B352
		public static string Sunday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Sunday");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D15E File Offset: 0x0000B35E
		public static string Monday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Monday");
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000D16A File Offset: 0x0000B36A
		public static string Tuesday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Tuesday");
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000D176 File Offset: 0x0000B376
		public static string Wednesday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Wednesday");
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000D182 File Offset: 0x0000B382
		public static string Thursday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Thursday");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000D18E File Offset: 0x0000B38E
		public static string Friday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Friday");
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000D19A File Offset: 0x0000B39A
		public static string Saturday
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("Saturday");
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		public static string January
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("January");
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000D1B2 File Offset: 0x0000B3B2
		public static string February
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("February");
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000D1BE File Offset: 0x0000B3BE
		public static string March
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("March");
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000D1CA File Offset: 0x0000B3CA
		public static string April
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("April");
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000D1D6 File Offset: 0x0000B3D6
		public static string May
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("May");
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000D1E2 File Offset: 0x0000B3E2
		public static string June
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("June");
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		public static string July
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("July");
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000D1FA File Offset: 0x0000B3FA
		public static string August
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("August");
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000D206 File Offset: 0x0000B406
		public static string September
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("September");
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D212 File Offset: 0x0000B412
		public static string October
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("October");
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000D21E File Offset: 0x0000B41E
		public static string November
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("November");
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000D22A File Offset: 0x0000B42A
		public static string December
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("December");
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000D236 File Offset: 0x0000B436
		public static string FirstWeek
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("FirstWeek");
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000D242 File Offset: 0x0000B442
		public static string SecondWeek
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("SecondWeek");
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000D24E File Offset: 0x0000B44E
		public static string ThirdWeek
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("ThirdWeek");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D25A File Offset: 0x0000B45A
		public static string FourthWeek
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("FourthWeek");
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000D266 File Offset: 0x0000B466
		public static string LastWeek
		{
			get
			{
				return ScheduleStringsWrapper.Keys.GetString("LastWeek");
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D272 File Offset: 0x0000B472
		public static string OnceScheduleDescription(string time, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("OnceScheduleDescription", time, date);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D280 File Offset: 0x0000B480
		public static string MinutesScheduleDescription(int hours, int minutes, string date, string time)
		{
			return ScheduleStringsWrapper.Keys.GetString("MinutesScheduleDescription", hours, minutes, date, time);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D29A File Offset: 0x0000B49A
		public static string DailyScheduleDescription(string time, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("DailyScheduleDescription", time, date);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public static string DailyWithIntervalScheduleDescription(string time, long days, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("DailyWithIntervalScheduleDescription", time, days, date);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public static string WeeklyScheduleDescription(string time, string days, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("WeeklyScheduleDescription", time, days, date);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		public static string WeeklyWithIntervalScheduleDescription(string time, string days, long interval, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("WeeklyWithIntervalScheduleDescription", time, days, interval, date);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D2E0 File Offset: 0x0000B4E0
		public static string MonthlyEveryMonthScheduleDescription(string time, string days, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("MonthlyEveryMonthScheduleDescription", time, days, date);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		public static string MontlyScheduleDescription(string time, string days, string months, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("MontlyScheduleDescription", time, days, months, date);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D2FF File Offset: 0x0000B4FF
		public static string MontlyDOWScheduleDescription(string time, string week, string days, string months, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("MontlyDOWScheduleDescription", time, week, days, months, date);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D311 File Offset: 0x0000B511
		public static string MonthlyDOWEveryMonthScheduleDescription(string time, string week, string days, string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("MonthlyDOWEveryMonthScheduleDescription", time, week, days, date);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D321 File Offset: 0x0000B521
		public static string EndScheduleDescription(string date)
		{
			return ScheduleStringsWrapper.Keys.GetString("EndScheduleDescription", date);
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x00002E32 File Offset: 0x00001032
			private Keys()
			{
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001105C File Offset: 0x0000F25C
			// (set) Token: 0x0600045A RID: 1114 RVA: 0x00011063 File Offset: 0x0000F263
			public static CultureInfo Culture
			{
				get
				{
					return ScheduleStringsWrapper.Keys._culture;
				}
				set
				{
					ScheduleStringsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x0001106B File Offset: 0x0000F26B
			public static string GetString(string key)
			{
				return ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture);
			}

			// Token: 0x0600045C RID: 1116 RVA: 0x0001107D File Offset: 0x0000F27D
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture), arg0);
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x0001109A File Offset: 0x0000F29A
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture), arg0, arg1);
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x000110B8 File Offset: 0x0000F2B8
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x000110D7 File Offset: 0x0000F2D7
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x0001110A File Offset: 0x0000F30A
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, ScheduleStringsWrapper.Keys.resourceManager.GetString(key, ScheduleStringsWrapper.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x040003AA RID: 938
			private static ResourceManager resourceManager = new ResourceManager(typeof(ScheduleStrings).FullName, typeof(ScheduleStrings).Module.Assembly);

			// Token: 0x040003AB RID: 939
			private static CultureInfo _culture = null;

			// Token: 0x040003AC RID: 940
			public const string Sunday = "Sunday";

			// Token: 0x040003AD RID: 941
			public const string Monday = "Monday";

			// Token: 0x040003AE RID: 942
			public const string Tuesday = "Tuesday";

			// Token: 0x040003AF RID: 943
			public const string Wednesday = "Wednesday";

			// Token: 0x040003B0 RID: 944
			public const string Thursday = "Thursday";

			// Token: 0x040003B1 RID: 945
			public const string Friday = "Friday";

			// Token: 0x040003B2 RID: 946
			public const string Saturday = "Saturday";

			// Token: 0x040003B3 RID: 947
			public const string January = "January";

			// Token: 0x040003B4 RID: 948
			public const string February = "February";

			// Token: 0x040003B5 RID: 949
			public const string March = "March";

			// Token: 0x040003B6 RID: 950
			public const string April = "April";

			// Token: 0x040003B7 RID: 951
			public const string May = "May";

			// Token: 0x040003B8 RID: 952
			public const string June = "June";

			// Token: 0x040003B9 RID: 953
			public const string July = "July";

			// Token: 0x040003BA RID: 954
			public const string August = "August";

			// Token: 0x040003BB RID: 955
			public const string September = "September";

			// Token: 0x040003BC RID: 956
			public const string October = "October";

			// Token: 0x040003BD RID: 957
			public const string November = "November";

			// Token: 0x040003BE RID: 958
			public const string December = "December";

			// Token: 0x040003BF RID: 959
			public const string FirstWeek = "FirstWeek";

			// Token: 0x040003C0 RID: 960
			public const string SecondWeek = "SecondWeek";

			// Token: 0x040003C1 RID: 961
			public const string ThirdWeek = "ThirdWeek";

			// Token: 0x040003C2 RID: 962
			public const string FourthWeek = "FourthWeek";

			// Token: 0x040003C3 RID: 963
			public const string LastWeek = "LastWeek";

			// Token: 0x040003C4 RID: 964
			public const string OnceScheduleDescription = "OnceScheduleDescription";

			// Token: 0x040003C5 RID: 965
			public const string MinutesScheduleDescription = "MinutesScheduleDescription";

			// Token: 0x040003C6 RID: 966
			public const string DailyScheduleDescription = "DailyScheduleDescription";

			// Token: 0x040003C7 RID: 967
			public const string DailyWithIntervalScheduleDescription = "DailyWithIntervalScheduleDescription";

			// Token: 0x040003C8 RID: 968
			public const string WeeklyScheduleDescription = "WeeklyScheduleDescription";

			// Token: 0x040003C9 RID: 969
			public const string WeeklyWithIntervalScheduleDescription = "WeeklyWithIntervalScheduleDescription";

			// Token: 0x040003CA RID: 970
			public const string MonthlyEveryMonthScheduleDescription = "MonthlyEveryMonthScheduleDescription";

			// Token: 0x040003CB RID: 971
			public const string MontlyScheduleDescription = "MontlyScheduleDescription";

			// Token: 0x040003CC RID: 972
			public const string MontlyDOWScheduleDescription = "MontlyDOWScheduleDescription";

			// Token: 0x040003CD RID: 973
			public const string MonthlyDOWEveryMonthScheduleDescription = "MonthlyDOWEveryMonthScheduleDescription";

			// Token: 0x040003CE RID: 974
			public const string EndScheduleDescription = "EndScheduleDescription";
		}
	}
}
