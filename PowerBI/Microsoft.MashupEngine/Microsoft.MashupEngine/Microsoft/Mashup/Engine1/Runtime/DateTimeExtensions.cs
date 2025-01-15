using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C5 RID: 4805
	public static class DateTimeExtensions
	{
		// Token: 0x06007E5B RID: 32347 RVA: 0x001B0E43 File Offset: 0x001AF043
		public static DateTime StartOfDay(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x001B0E60 File Offset: 0x001AF060
		public static DateTime EndOfDay(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59).AddTicks(9999999L);
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x001B0E9B File Offset: 0x001AF09B
		public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
		{
			return DateTimeValue.New(dateTime).StartOfWeek(firstDayOfWeek).AsDateTime.AsClrDateTime;
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x001B0EB4 File Offset: 0x001AF0B4
		public static DateTime EndOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
		{
			DayOfWeek dayOfWeek = (firstDayOfWeek - 1) % (DayOfWeek)7;
			return DateTimeValue.New(dateTime).EndOfWeek(dayOfWeek).AsDateTime.AsClrDateTime;
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x001B0EDD File Offset: 0x001AF0DD
		public static DateTime StartOfMonth(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, 1);
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x001B0EF4 File Offset: 0x001AF0F4
		public static DateTime EndOfMonth(this DateTime dateTime)
		{
			int year = dateTime.Year;
			int month = dateTime.Month;
			return new DateTime(year, month, DateTime.DaysInMonth(year, month)).EndOfDay();
		}

		// Token: 0x06007E61 RID: 32353 RVA: 0x001B0F24 File Offset: 0x001AF124
		public static DateTime StartOfQuarter(this DateTime dateTime)
		{
			int num = (dateTime.Month - 1) % 3;
			return dateTime.AddMonths(-num).StartOfMonth();
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x001B0F4C File Offset: 0x001AF14C
		public static DateTime EndOfQuarter(this DateTime dateTime)
		{
			int num = (dateTime.Month - 1) % 3;
			return dateTime.AddMonths(2 - num).EndOfMonth();
		}

		// Token: 0x06007E63 RID: 32355 RVA: 0x001B0F74 File Offset: 0x001AF174
		public static DateTime StartOfYear(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, 1, 1);
		}

		// Token: 0x06007E64 RID: 32356 RVA: 0x001B0F84 File Offset: 0x001AF184
		public static DateTime EndOfYear(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, 12, 1).EndOfMonth().EndOfDay();
		}

		// Token: 0x06007E65 RID: 32357 RVA: 0x001B0FA0 File Offset: 0x001AF1A0
		public static DateTime AddWeeks(this DateTime dateTime, int numberofWeeks)
		{
			DateTime dateTime2;
			try
			{
				dateTime2 = dateTime + new TimeSpan(checked(7 * numberofWeeks), 0, 0, 0);
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return dateTime2;
		}

		// Token: 0x06007E66 RID: 32358 RVA: 0x001B0FE4 File Offset: 0x001AF1E4
		public static DateTime AddQuarters(this DateTime dateTime, int numberofQuarters)
		{
			DateTime dateTime2;
			try
			{
				dateTime2 = dateTime.AddMonths(checked(3 * numberofQuarters));
			}
			catch (OverflowException ex)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.DateTime_OutOfRangeError, Value.Null, ex);
			}
			return dateTime2;
		}
	}
}
