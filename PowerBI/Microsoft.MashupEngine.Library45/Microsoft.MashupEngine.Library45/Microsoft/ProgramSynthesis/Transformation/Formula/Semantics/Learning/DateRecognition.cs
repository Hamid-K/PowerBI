using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001606 RID: 5638
	internal class DateRecognition
	{
		// Token: 0x0600BB87 RID: 48007 RVA: 0x002853F0 File Offset: 0x002835F0
		public DateTime? DayEnd(DateTime date)
		{
			return DateRecognition.AddDays(this.DayStart(date), 1);
		}

		// Token: 0x0600BB88 RID: 48008 RVA: 0x002853FF File Offset: 0x002835FF
		public DateTime DayStart(DateTime date)
		{
			return date.Date;
		}

		// Token: 0x0600BB89 RID: 48009 RVA: 0x00285408 File Offset: 0x00283608
		public int Hour(DateTime date)
		{
			return date.Hour;
		}

		// Token: 0x0600BB8A RID: 48010 RVA: 0x00285411 File Offset: 0x00283611
		public DateTime? HourEnd(DateTime date)
		{
			return DateRecognition.AddHours(this.HourStart(date), 1);
		}

		// Token: 0x0600BB8B RID: 48011 RVA: 0x00285420 File Offset: 0x00283620
		public DateTime HourStart(DateTime date)
		{
			return this.ReplaceElements(date, new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Minute,
				DateTimePart.Second,
				DateTimePart.Millisecond
			}), 0, 1, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BB8C RID: 48012 RVA: 0x00285451 File Offset: 0x00283651
		public int Minute(DateTime date)
		{
			return date.Minute;
		}

		// Token: 0x0600BB8D RID: 48013 RVA: 0x0028545A File Offset: 0x0028365A
		public DateTime? MinuteEnd(DateTime date)
		{
			return DateRecognition.AddMinutes(this.MinuteStart(date), 1);
		}

		// Token: 0x0600BB8E RID: 48014 RVA: 0x0028546C File Offset: 0x0028366C
		public DateTime MinuteStart(DateTime date)
		{
			return this.ReplaceElements(date, new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Second,
				DateTimePart.Millisecond
			}), 0, 1, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BB8F RID: 48015 RVA: 0x0028549A File Offset: 0x0028369A
		public int Month(DateTime date)
		{
			return date.Month;
		}

		// Token: 0x0600BB90 RID: 48016 RVA: 0x002854A3 File Offset: 0x002836A3
		public int MonthDay(DateTime date)
		{
			return date.Day;
		}

		// Token: 0x0600BB91 RID: 48017 RVA: 0x002854AC File Offset: 0x002836AC
		public int MonthDays(DateTime date)
		{
			return this._calendar.GetDaysInMonth(date.Year, date.Month);
		}

		// Token: 0x0600BB92 RID: 48018 RVA: 0x002854C7 File Offset: 0x002836C7
		public DateTime? MonthEnd(DateTime date)
		{
			return DateRecognition.AddMonths(this.MonthStart(date), 1);
		}

		// Token: 0x0600BB93 RID: 48019 RVA: 0x002854D8 File Offset: 0x002836D8
		public DateTime? MonthEndDay(DateTime date)
		{
			DateTime? dateTime = this.MonthEnd(date);
			if (dateTime != null)
			{
				return DateRecognition.AddDays(dateTime.Value, -1);
			}
			return null;
		}

		// Token: 0x0600BB94 RID: 48020 RVA: 0x00285510 File Offset: 0x00283710
		public DateTime MonthStart(DateTime date)
		{
			return this.ReplaceElements(date, DateTimePartSet.StandardTimeParts.Set(DateTimePart.Day), 0, 1, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BB95 RID: 48021 RVA: 0x00285539 File Offset: 0x00283739
		public int MonthWeek(DateTime date)
		{
			return this.Weeks(date, this.MonthStart(date));
		}

		// Token: 0x0600BB96 RID: 48022 RVA: 0x00285549 File Offset: 0x00283749
		public int Quarter(DateTime date)
		{
			return Convert.ToInt32(Math.Floor((double)(date.Month - 1) / 3.0) + 1.0);
		}

		// Token: 0x0600BB97 RID: 48023 RVA: 0x00285574 File Offset: 0x00283774
		public int QuarterDay(DateTime date)
		{
			return (date - this.QuarterStart(date)).Days + 1;
		}

		// Token: 0x0600BB98 RID: 48024 RVA: 0x00285598 File Offset: 0x00283798
		public int? QuarterDays(DateTime date)
		{
			DateTime? dateTime = this.QuarterEnd(date);
			if (dateTime == null)
			{
				return null;
			}
			DateTime dateTime2 = this.QuarterStart(date);
			return new int?((dateTime.Value - dateTime2).Days);
		}

		// Token: 0x0600BB99 RID: 48025 RVA: 0x002855E2 File Offset: 0x002837E2
		public DateTime? QuarterEnd(DateTime date)
		{
			return DateRecognition.AddMonths(this.QuarterStart(date), 3);
		}

		// Token: 0x0600BB9A RID: 48026 RVA: 0x002855F4 File Offset: 0x002837F4
		public DateTime? QuarterEndDay(DateTime date)
		{
			DateTime? dateTime = this.QuarterEnd(date);
			if (dateTime != null)
			{
				return DateRecognition.AddDays(dateTime.Value, -1);
			}
			return null;
		}

		// Token: 0x0600BB9B RID: 48027 RVA: 0x0028562C File Offset: 0x0028382C
		public DateTime QuarterStart(DateTime date)
		{
			int num = this.Quarter(date);
			return this.ReplaceElements(date, DateTimePartSet.StandardTimeParts.Set(new DateTimePart[]
			{
				DateTimePart.Month,
				DateTimePart.Day
			}), 0, 3 * num - 2, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BB9C RID: 48028 RVA: 0x0028566E File Offset: 0x0028386E
		public int QuarterWeek(DateTime date)
		{
			return this.Weeks(date, this.QuarterStart(date));
		}

		// Token: 0x0600BB9D RID: 48029 RVA: 0x0028567E File Offset: 0x0028387E
		public int Second(DateTime date)
		{
			return date.Second;
		}

		// Token: 0x0600BB9E RID: 48030 RVA: 0x00285687 File Offset: 0x00283887
		public DateTime? SecondEnd(DateTime date)
		{
			return DateRecognition.AddSeconds(this.SecondStart(date), 1);
		}

		// Token: 0x0600BB9F RID: 48031 RVA: 0x00285698 File Offset: 0x00283898
		public DateTime SecondStart(DateTime date)
		{
			return this.ReplaceElements(date, new DateTimePartSet(new DateTimePart[] { DateTimePart.Millisecond }), 0, 1, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BBA0 RID: 48032 RVA: 0x002856C2 File Offset: 0x002838C2
		public int WeekDay(DateTime date)
		{
			return (int)this._calendar.GetDayOfWeek(date);
		}

		// Token: 0x0600BBA1 RID: 48033 RVA: 0x0001B319 File Offset: 0x00019519
		public int WeekDays(DateTime date)
		{
			return 7;
		}

		// Token: 0x0600BBA2 RID: 48034 RVA: 0x002856D0 File Offset: 0x002838D0
		public DateTime? WeekEnd(DateTime date)
		{
			DateTime? dateTime = this.WeekStart(date);
			if (dateTime != null)
			{
				return DateRecognition.AddDays(dateTime.Value, this.WeekDays(date));
			}
			return null;
		}

		// Token: 0x0600BBA3 RID: 48035 RVA: 0x0028570C File Offset: 0x0028390C
		public DateTime? WeekEndDay(DateTime date)
		{
			DateTime? dateTime = this.WeekEnd(date);
			if (dateTime != null)
			{
				return DateRecognition.AddDays(dateTime.Value, -1);
			}
			return null;
		}

		// Token: 0x0600BBA4 RID: 48036 RVA: 0x00285744 File Offset: 0x00283944
		public int Weeks(DateTime subject, DateTime periodStart)
		{
			double days = (double)(subject - periodStart).Days;
			int num = this.WeekDay(periodStart) + 1;
			return Convert.ToInt32(Math.Ceiling((days + (double)num) / 7.0));
		}

		// Token: 0x0600BBA5 RID: 48037 RVA: 0x00285784 File Offset: 0x00283984
		public DateTime? WeekStart(DateTime date)
		{
			DateTime? dateTime = DateRecognition.AddDays(date, -this.WeekDay(date));
			if (dateTime != null)
			{
				return new DateTime?(this.ReplaceElements(dateTime.Value, DateTimePartSet.StandardTimeParts, 0, 1, 1, 0, 0, 0, 0));
			}
			return null;
		}

		// Token: 0x0600BBA6 RID: 48038 RVA: 0x002857D1 File Offset: 0x002839D1
		public int Year(DateTime date)
		{
			return date.Year;
		}

		// Token: 0x0600BBA7 RID: 48039 RVA: 0x002857DA File Offset: 0x002839DA
		public int YearDay(DateTime date)
		{
			return this._calendar.GetDayOfYear(date);
		}

		// Token: 0x0600BBA8 RID: 48040 RVA: 0x002857E8 File Offset: 0x002839E8
		public int YearDays(DateTime date)
		{
			if (!this._calendar.IsLeapYear(date.Year))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x0600BBA9 RID: 48041 RVA: 0x00285809 File Offset: 0x00283A09
		public DateTime? YearEnd(DateTime date)
		{
			return DateRecognition.AddYears(this.YearStart(date), 1);
		}

		// Token: 0x0600BBAA RID: 48042 RVA: 0x00285818 File Offset: 0x00283A18
		public DateTime? YearEndDay(DateTime date)
		{
			DateTime? dateTime = this.YearEnd(date);
			if (dateTime != null)
			{
				return DateRecognition.AddDays(dateTime.Value, -1);
			}
			return null;
		}

		// Token: 0x0600BBAB RID: 48043 RVA: 0x00285850 File Offset: 0x00283A50
		public DateTime YearStart(DateTime date)
		{
			return this.ReplaceElements(date, DateTimePartSet.StandardTimeParts.Set(new DateTimePart[]
			{
				DateTimePart.Month,
				DateTimePart.Day
			}), 0, 1, 1, 0, 0, 0, 0);
		}

		// Token: 0x0600BBAC RID: 48044 RVA: 0x00285886 File Offset: 0x00283A86
		public int YearWeek(DateTime date)
		{
			return this.Weeks(date, this.YearStart(date));
		}

		// Token: 0x0600BBAD RID: 48045 RVA: 0x00285898 File Offset: 0x00283A98
		private DateTime ReplaceElements(DateTime date, DateTimePartSet elements, int year = 0, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
		{
			return new DateTime(elements.Contains(DateTimePart.Year) ? year : date.Year, elements.Contains(DateTimePart.Month) ? month : date.Month, elements.Contains(DateTimePart.Day) ? day : date.Day, elements.Contains(DateTimePart.Hour) ? hour : date.Hour, elements.Contains(DateTimePart.Minute) ? minute : date.Minute, elements.Contains(DateTimePart.Second) ? second : date.Second, elements.Contains(DateTimePart.Millisecond) ? millisecond : date.Millisecond, this._calendar, date.Kind);
		}

		// Token: 0x0600BBAE RID: 48046 RVA: 0x0028594C File Offset: 0x00283B4C
		private static DateTime? AddDays(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddDays((double)interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x0600BBAF RID: 48047 RVA: 0x00285988 File Offset: 0x00283B88
		private static DateTime? AddHours(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddHours((double)interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x0600BBB0 RID: 48048 RVA: 0x002859C4 File Offset: 0x00283BC4
		private static DateTime? AddMinutes(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddMinutes((double)interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x0600BBB1 RID: 48049 RVA: 0x00285A00 File Offset: 0x00283C00
		private static DateTime? AddMonths(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddMonths(interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x0600BBB2 RID: 48050 RVA: 0x00285A3C File Offset: 0x00283C3C
		private static DateTime? AddSeconds(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddSeconds((double)interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x0600BBB3 RID: 48051 RVA: 0x00285A78 File Offset: 0x00283C78
		private static DateTime? AddYears(DateTime date, int interval)
		{
			DateTime? dateTime;
			try
			{
				dateTime = new DateTime?(date.AddYears(interval));
			}
			catch (ArgumentOutOfRangeException)
			{
				dateTime = null;
			}
			return dateTime;
		}

		// Token: 0x040046F2 RID: 18162
		private readonly Calendar _calendar = new CultureInfo("en-US").Calendar;
	}
}
