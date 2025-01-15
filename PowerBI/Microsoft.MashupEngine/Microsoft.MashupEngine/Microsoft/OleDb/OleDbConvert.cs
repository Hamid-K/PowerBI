using System;
using Microsoft.OleDb.Marshallers;

namespace Microsoft.OleDb
{
	// Token: 0x02001F14 RID: 7956
	public static class OleDbConvert
	{
		// Token: 0x0600C2CF RID: 49871 RVA: 0x00270C28 File Offset: 0x0026EE28
		public static DBTIMESTAMP GetTimeStamp(DateTime dateTime)
		{
			DBTIMESTAMP dbtimestamp;
			dbtimestamp.year = (short)dateTime.Year;
			dbtimestamp.month = (ushort)dateTime.Month;
			dbtimestamp.day = (ushort)dateTime.Day;
			dbtimestamp.hour = (ushort)dateTime.Hour;
			dbtimestamp.minute = (ushort)dateTime.Minute;
			dbtimestamp.second = (ushort)dateTime.Second;
			dbtimestamp.fraction = (uint)(dateTime.Ticks % 10000000L) * 100U;
			return dbtimestamp;
		}

		// Token: 0x0600C2D0 RID: 49872 RVA: 0x00270CAC File Offset: 0x0026EEAC
		public static DBTIMESTAMPOFFSET GetTimeStampOffset(DateTimeOffset dateTimeOffset)
		{
			DBTIMESTAMPOFFSET dbtimestampoffset;
			dbtimestampoffset.year = (short)dateTimeOffset.Year;
			dbtimestampoffset.month = (ushort)dateTimeOffset.Month;
			dbtimestampoffset.day = (ushort)dateTimeOffset.Day;
			dbtimestampoffset.hour = (ushort)dateTimeOffset.Hour;
			dbtimestampoffset.minute = (ushort)dateTimeOffset.Minute;
			dbtimestampoffset.second = (ushort)dateTimeOffset.Second;
			dbtimestampoffset.fraction = (uint)(dateTimeOffset.Ticks % 10000000L) * 100U;
			dbtimestampoffset.timezone_hour = (short)dateTimeOffset.Offset.Hours;
			dbtimestampoffset.timezone_minute = (short)dateTimeOffset.Offset.Minutes;
			return dbtimestampoffset;
		}

		// Token: 0x0600C2D1 RID: 49873 RVA: 0x00270D5C File Offset: 0x0026EF5C
		public static DateTimeOffset GetDateTimeOffset(DBTIMESTAMPOFFSET timeStampOffset)
		{
			return new DateTimeOffset(new DateTime((int)timeStampOffset.year, (int)timeStampOffset.month, (int)timeStampOffset.day, (int)timeStampOffset.hour, (int)timeStampOffset.minute, (int)timeStampOffset.second) + new TimeSpan((long)((ulong)(timeStampOffset.fraction / 100U))), new TimeSpan((int)timeStampOffset.timezone_hour, (int)timeStampOffset.timezone_minute, 0));
		}

		// Token: 0x0600C2D2 RID: 49874 RVA: 0x00270DC0 File Offset: 0x0026EFC0
		public static DBDATE GetDate(Date date)
		{
			DateTime dateTime = date.DateTime;
			DBDATE dbdate;
			dbdate.year = (short)dateTime.Year;
			dbdate.month = (ushort)dateTime.Month;
			dbdate.day = (ushort)dateTime.Day;
			return dbdate;
		}

		// Token: 0x0600C2D3 RID: 49875 RVA: 0x00270E03 File Offset: 0x0026F003
		public static Date GetDate(DBDATE date)
		{
			return new Date(new DateTime((int)date.year, (int)date.month, (int)date.day));
		}

		// Token: 0x0600C2D4 RID: 49876 RVA: 0x00270E24 File Offset: 0x0026F024
		public static DBTIME2 GetTime(Time time)
		{
			TimeSpan timeSpan = time.TimeSpan;
			DBTIME2 dbtime;
			dbtime.hour = (ushort)timeSpan.Hours;
			dbtime.minute = (ushort)timeSpan.Minutes;
			dbtime.second = (ushort)timeSpan.Seconds;
			dbtime.fraction = (uint)(timeSpan.Ticks % 10000000L) * 100U;
			return dbtime;
		}

		// Token: 0x0600C2D5 RID: 49877 RVA: 0x00270E80 File Offset: 0x0026F080
		public static Time GetTime(DBTIME2 time)
		{
			return new Time(new TimeSpan((int)time.hour, (int)time.minute, (int)time.second) + new TimeSpan((long)((ulong)(time.fraction / 100U))));
		}

		// Token: 0x0600C2D6 RID: 49878 RVA: 0x00270EB2 File Offset: 0x0026F0B2
		public static Time GetTime(DBTIME time)
		{
			return new Time(new TimeSpan((int)time.hour, (int)time.minute, (int)time.second));
		}

		// Token: 0x0600C2D7 RID: 49879 RVA: 0x00270ED0 File Offset: 0x0026F0D0
		public static DateTime GetDateTime(DBTIMESTAMP timeStamp)
		{
			return new DateTime((int)timeStamp.year, (int)timeStamp.month, (int)timeStamp.day, (int)timeStamp.hour, (int)timeStamp.minute, (int)timeStamp.second) + new TimeSpan((long)((ulong)(timeStamp.fraction / 100U)));
		}

		// Token: 0x0600C2D8 RID: 49880 RVA: 0x00270F10 File Offset: 0x0026F110
		public unsafe static object GetObject(IDataConvert dataConvert, DBTYPE wSrcType, DBLENGTH cbSrcLength, void* pSrc, DBSTATUS dbsSrcStatus, byte bPrecision, byte bScale, DBDATACONVERT dwFlags)
		{
			if (wSrcType <= DBTYPE.DBTIMESTAMP)
			{
				if (wSrcType == DBTYPE.DBDATE)
				{
					return new Date(new DateTime((int)((DBDATE*)pSrc)->year, (int)((DBDATE*)pSrc)->month, (int)((DBDATE*)pSrc)->day));
				}
				if (wSrcType == DBTYPE.DBTIMESTAMP)
				{
					return new DateTime((int)((DBTIMESTAMP*)pSrc)->year, (int)((DBTIMESTAMP*)pSrc)->month, (int)((DBTIMESTAMP*)pSrc)->day, (int)((DBTIMESTAMP*)pSrc)->hour, (int)((DBTIMESTAMP*)pSrc)->minute, (int)((DBTIMESTAMP*)pSrc)->second) + new TimeSpan((long)((ulong)(((DBTIMESTAMP*)pSrc)->fraction / 100U)));
				}
			}
			else
			{
				if (wSrcType == DBTYPE.DBTIME2)
				{
					return new Time(new TimeSpan(0, (int)((DBTIME2*)pSrc)->hour, (int)((DBTIME2*)pSrc)->minute, (int)((DBTIME2*)pSrc)->second) + new TimeSpan((long)(((DBTIME2*)pSrc)->fraction / 100U)));
				}
				if (wSrcType == DBTYPE.DBTIMESTAMPOFFSET)
				{
					return new DateTimeOffset((int)((DBTIMESTAMPOFFSET*)pSrc)->year, (int)((DBTIMESTAMPOFFSET*)pSrc)->month, (int)((DBTIMESTAMPOFFSET*)pSrc)->day, (int)((DBTIMESTAMPOFFSET*)pSrc)->hour, (int)((DBTIMESTAMPOFFSET*)pSrc)->minute, (int)((DBTIMESTAMPOFFSET*)pSrc)->second, new TimeSpan((int)((DBTIMESTAMPOFFSET*)pSrc)->timezone_hour, (int)((DBTIMESTAMPOFFSET*)pSrc)->timezone_minute, 0)) + new TimeSpan((long)((ulong)(((DBTIMESTAMPOFFSET*)pSrc)->fraction / 100U)));
				}
				if (wSrcType == DBTYPE.DBDURATION)
				{
					return new TimeSpan(((DBDURATION*)pSrc)->ticks);
				}
			}
			DBLENGTH dblength;
			VARIANT variant;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(wSrcType, DBTYPE.VARIANT, cbSrcLength, out dblength, pSrc, (void*)(&variant), DbLength.MaxValue, dbsSrcStatus, out dbstatus, bPrecision, bScale, dwFlags);
			return VariantMarshaller.Instance.GetManaged((IntPtr)((void*)(&variant)));
		}

		// Token: 0x0600C2D9 RID: 49881 RVA: 0x002710A4 File Offset: 0x0026F2A4
		public static object GetVariantObject(object value)
		{
			if (value is TimeSpan)
			{
				return OleDbConvert.GetVariantValue((TimeSpan)value);
			}
			if (value is Time)
			{
				return OleDbConvert.GetVariantValue((Time)value);
			}
			if (value is Date)
			{
				return OleDbConvert.GetVariantValue(((Date)value).DateTime);
			}
			if (value is DateTimeOffset)
			{
				return OleDbConvert.GetVariantValue((DateTimeOffset)value);
			}
			if (value is DateTime)
			{
				return OleDbConvert.GetVariantValue((DateTime)value);
			}
			return value;
		}

		// Token: 0x0600C2DA RID: 49882 RVA: 0x00271137 File Offset: 0x0026F337
		public static double GetVariantValue(TimeSpan timeSpan)
		{
			return (double)timeSpan.Ticks / 864000000000.0;
		}

		// Token: 0x0600C2DB RID: 49883 RVA: 0x0027114C File Offset: 0x0026F34C
		public static DateTime GetVariantValue(Time time)
		{
			return DateTime.FromOADate((double)time.TimeSpan.Ticks / 864000000000.0);
		}

		// Token: 0x0600C2DC RID: 49884 RVA: 0x00271178 File Offset: 0x0026F378
		public static DateTime? GetVariantValue(DateTimeOffset dateTimeOffset)
		{
			DateTime dateTime = dateTimeOffset.DateTime;
			if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
			{
				return null;
			}
			return OleDbConvert.GetVariantValue(dateTime);
		}

		// Token: 0x0600C2DD RID: 49885 RVA: 0x002711B8 File Offset: 0x0026F3B8
		public static DateTime? GetVariantValue(DateTime dateTime)
		{
			if (!OleDbConvert.IsOleDate(dateTime))
			{
				return null;
			}
			return new DateTime?(dateTime);
		}

		// Token: 0x0600C2DE RID: 49886 RVA: 0x002711E0 File Offset: 0x0026F3E0
		public static double? SafeToOADate(DateTime dateTime)
		{
			if (!OleDbConvert.IsOleDate(dateTime))
			{
				return null;
			}
			return new double?(dateTime.ToOADate());
		}

		// Token: 0x0600C2DF RID: 49887 RVA: 0x0027120C File Offset: 0x0026F40C
		public static DateTime DateTimeFromOADate(double value)
		{
			DateTime dateTime;
			try
			{
				dateTime = DateTime.FromOADate(value);
			}
			catch (ArgumentException)
			{
				throw new OleDbException(-2147352566, Strings.NotLegalOADate, "", null);
			}
			return dateTime;
		}

		// Token: 0x0600C2E0 RID: 49888 RVA: 0x0027124C File Offset: 0x0026F44C
		private static bool IsOleDate(DateTime dateTime)
		{
			return dateTime.Year >= 100;
		}

		// Token: 0x04006457 RID: 25687
		private const int BillionthsPerTick = 100;

		// Token: 0x02001F15 RID: 7957
		private static class HResult
		{
			// Token: 0x0600C2E1 RID: 49889 RVA: 0x0027125C File Offset: 0x0026F45C
			public static int MakeHResult(int severity, int facility, int code)
			{
				return (severity << 31) | (facility << 16) | code;
			}

			// Token: 0x04006458 RID: 25688
			public const int SEVERITY_ERROR = 1;

			// Token: 0x04006459 RID: 25689
			public const int FACILITY_ITF = 4;

			// Token: 0x0400645A RID: 25690
			public const int ITF_VALUEEXCEPTION = 512;
		}
	}
}
