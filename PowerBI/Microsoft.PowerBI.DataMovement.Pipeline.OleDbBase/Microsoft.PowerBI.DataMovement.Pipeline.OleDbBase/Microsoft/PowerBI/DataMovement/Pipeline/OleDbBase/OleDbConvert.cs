using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BB RID: 187
	public static class OleDbConvert
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00008FD0 File Offset: 0x000071D0
		public static DBTIMESTAMP GetTimeStamp(DateTime dateTime)
		{
			DBTIMESTAMP dbtimestamp;
			dbtimestamp.Year = (short)dateTime.Year;
			dbtimestamp.Month = (ushort)dateTime.Month;
			dbtimestamp.Day = (ushort)dateTime.Day;
			dbtimestamp.Hour = (ushort)dateTime.Hour;
			dbtimestamp.Minute = (ushort)dateTime.Minute;
			dbtimestamp.Second = (ushort)dateTime.Second;
			dbtimestamp.Fraction = (uint)(dateTime.Ticks % 10000000L) * 100U;
			return dbtimestamp;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00009054 File Offset: 0x00007254
		public static DBTIMESTAMPOFFSET GetTimeStampOffset(DateTimeOffset dateTimeOffset)
		{
			DBTIMESTAMPOFFSET dbtimestampoffset;
			dbtimestampoffset.Year = (short)dateTimeOffset.Year;
			dbtimestampoffset.Month = (ushort)dateTimeOffset.Month;
			dbtimestampoffset.Day = (ushort)dateTimeOffset.Day;
			dbtimestampoffset.Hour = (ushort)dateTimeOffset.Hour;
			dbtimestampoffset.Minute = (ushort)dateTimeOffset.Minute;
			dbtimestampoffset.Second = (ushort)dateTimeOffset.Second;
			dbtimestampoffset.Fraction = (uint)(dateTimeOffset.Ticks % 10000000L) * 100U;
			dbtimestampoffset.Timezone_Hour = (short)dateTimeOffset.Offset.Hours;
			dbtimestampoffset.Timezone_Minute = (short)dateTimeOffset.Offset.Minutes;
			return dbtimestampoffset;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009104 File Offset: 0x00007304
		public static DateTimeOffset GetDateTimeOffset(DBTIMESTAMPOFFSET timeStampOffset)
		{
			return new DateTimeOffset(new DateTime((int)timeStampOffset.Year, (int)timeStampOffset.Month, (int)timeStampOffset.Day, (int)timeStampOffset.Hour, (int)timeStampOffset.Minute, (int)timeStampOffset.Second) + new TimeSpan((long)((ulong)(timeStampOffset.Fraction / 100U))), new TimeSpan((int)timeStampOffset.Timezone_Hour, (int)timeStampOffset.Timezone_Minute, 0));
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009168 File Offset: 0x00007368
		public static DBDATE GetDate(Date date)
		{
			DateTime dateTime = date.DateTime;
			DBDATE dbdate;
			dbdate.Year = (short)dateTime.Year;
			dbdate.Month = (ushort)dateTime.Month;
			dbdate.Day = (ushort)dateTime.Day;
			return dbdate;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000091AB File Offset: 0x000073AB
		public static Date GetDate(DBDATE date)
		{
			return new Date(new DateTime((int)date.Year, (int)date.Month, (int)date.Day));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000091CC File Offset: 0x000073CC
		public static DBTIME2 GetTime(Time time)
		{
			TimeSpan timeSpan = time.TimeSpan;
			DBTIME2 dbtime;
			dbtime.Hour = (ushort)timeSpan.Hours;
			dbtime.Minute = (ushort)timeSpan.Minutes;
			dbtime.Second = (ushort)timeSpan.Seconds;
			dbtime.Fraction = (uint)(timeSpan.Ticks % 10000000L) * 100U;
			return dbtime;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009228 File Offset: 0x00007428
		public static Time GetTime(DBTIME2 time)
		{
			return new Time(new TimeSpan((int)time.Hour, (int)time.Minute, (int)time.Second) + new TimeSpan((long)((ulong)(time.Fraction / 100U))));
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000925A File Offset: 0x0000745A
		public static Time GetTime(DBTIME time)
		{
			return new Time(new TimeSpan((int)time.Hour, (int)time.Minute, (int)time.Second));
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009278 File Offset: 0x00007478
		public static DateTime GetDateTime(DBTIMESTAMP timeStamp)
		{
			return new DateTime((int)timeStamp.Year, (int)timeStamp.Month, (int)timeStamp.Day, (int)timeStamp.Hour, (int)timeStamp.Minute, (int)timeStamp.Second) + new TimeSpan((long)((ulong)(timeStamp.Fraction / 100U)));
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000092B8 File Offset: 0x000074B8
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public unsafe static object GetObject(DBTYPE srcType, DBLENGTH srcLength, void* src, DBSTATUS srcStatus, byte precision, byte scale, DBDATACONVERT flags)
		{
			if (srcType <= DBTYPE.DBTIMESTAMP)
			{
				if (srcType == DBTYPE.DBDATE)
				{
					return new Date(new DateTime((int)((DBDATE*)src)->Year, (int)((DBDATE*)src)->Month, (int)((DBDATE*)src)->Day));
				}
				if (srcType == DBTYPE.DBTIMESTAMP)
				{
					return new DateTime((int)((DBTIMESTAMP*)src)->Year, (int)((DBTIMESTAMP*)src)->Month, (int)((DBTIMESTAMP*)src)->Day, (int)((DBTIMESTAMP*)src)->Hour, (int)((DBTIMESTAMP*)src)->Minute, (int)((DBTIMESTAMP*)src)->Second) + new TimeSpan((long)((ulong)(((DBTIMESTAMP*)src)->Fraction / 100U)));
				}
			}
			else
			{
				if (srcType == DBTYPE.DBTIME2)
				{
					return new Time(new TimeSpan(0, (int)((DBTIME2*)src)->Hour, (int)((DBTIME2*)src)->Minute, (int)((DBTIME2*)src)->Second) + new TimeSpan((long)(((DBTIME2*)src)->Fraction / 100U)));
				}
				if (srcType == DBTYPE.DBTIMESTAMPOFFSET)
				{
					return new DateTimeOffset((int)((DBTIMESTAMPOFFSET*)src)->Year, (int)((DBTIMESTAMPOFFSET*)src)->Month, (int)((DBTIMESTAMPOFFSET*)src)->Day, (int)((DBTIMESTAMPOFFSET*)src)->Hour, (int)((DBTIMESTAMPOFFSET*)src)->Minute, (int)((DBTIMESTAMPOFFSET*)src)->Second, new TimeSpan((int)((DBTIMESTAMPOFFSET*)src)->Timezone_Hour, (int)((DBTIMESTAMPOFFSET*)src)->Timezone_Minute, 0)) + new TimeSpan((long)((ulong)(((DBTIMESTAMPOFFSET*)src)->Fraction / 100U)));
				}
				if (srcType == DBTYPE.DBDURATION)
				{
					return new TimeSpan(((DBDURATION*)src)->Ticks);
				}
			}
			DBLENGTH dblength;
			VARIANT variant;
			DBSTATUS dbstatus;
			DataConvert.GetInstance().DataConvert(srcType, DBTYPE.VARIANT, srcLength, out dblength, src, (void*)(&variant), DbLength.MaxValue, srcStatus, out dbstatus, precision, scale, flags);
			return Variant.GetObject(&variant);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009444 File Offset: 0x00007644
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static object GetVariantObject(object value)
		{
			if (value is TimeSpan)
			{
				return (double)((TimeSpan)value).Ticks / 864000000000.0;
			}
			if (value is Time)
			{
				return DateTime.FromOADate((double)((Time)value).TimeSpan.Ticks / 864000000000.0);
			}
			if (value is Date)
			{
				return OleDbConvert.GetVariantObject(((Date)value).DateTime);
			}
			if (value is DateTimeOffset)
			{
				return OleDbConvert.GetVariantObject((DateTimeOffset)value);
			}
			if (value is DateTime)
			{
				return OleDbConvert.GetVariantObject((DateTime)value);
			}
			return value;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000094F4 File Offset: 0x000076F4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static object GetVariantObject(DateTimeOffset dateTimeOffset)
		{
			DateTime dateTime = dateTimeOffset.DateTime;
			if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
			{
				return null;
			}
			return OleDbConvert.GetVariantObject(dateTime);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000952B File Offset: 0x0000772B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static object GetVariantObject(DateTime dateTime)
		{
			if (!OleDbConvert.IsOleDate(dateTime))
			{
				return null;
			}
			return dateTime;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00009540 File Offset: 0x00007740
		public static double? SafeToOADate(DateTime dateTime)
		{
			if (!OleDbConvert.IsOleDate(dateTime))
			{
				return null;
			}
			return new double?(dateTime.ToOADate());
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000956B File Offset: 0x0000776B
		private static bool IsOleDate(DateTime dateTime)
		{
			return dateTime.Year >= 100;
		}

		// Token: 0x0400036D RID: 877
		private const int BillionthsPerTick = 100;

		// Token: 0x020000F0 RID: 240
		internal static class HResult
		{
			// Token: 0x060004C3 RID: 1219 RVA: 0x0000E5C2 File Offset: 0x0000C7C2
			public static int MakeHResult(int severity, int facility, int code)
			{
				return (severity << 31) | (facility << 16) | code;
			}

			// Token: 0x0400040C RID: 1036
			internal const int SEVERITY_ERROR = 1;

			// Token: 0x0400040D RID: 1037
			internal const int FACILITY_ITF = 4;

			// Token: 0x0400040E RID: 1038
			internal const int ITF_VALUEEXCEPTION = 512;
		}
	}
}
