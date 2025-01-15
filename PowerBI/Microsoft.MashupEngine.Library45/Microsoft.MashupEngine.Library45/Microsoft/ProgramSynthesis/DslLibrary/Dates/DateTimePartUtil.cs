using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200084E RID: 2126
	public static class DateTimePartUtil
	{
		// Token: 0x06002E22 RID: 11810 RVA: 0x00083D69 File Offset: 0x00081F69
		internal static int MonthToQuarter(int month)
		{
			return (month - 1) / 3 + 1;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x00083D72 File Offset: 0x00081F72
		public static int HourInPeriodToHour(int hourInPeriod, Period period)
		{
			if (hourInPeriod != 12)
			{
				return hourInPeriod + ((period == Period.PM) ? 12 : 0);
			}
			if (period != Period.AM)
			{
				return 12;
			}
			return 0;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x00083D8C File Offset: 0x00081F8C
		public static DayOfWeek DateToDayOfWeek(int year, int month, int day)
		{
			year -= ((month < 3) ? 1 : 0);
			return (DayOfWeek)((year + year / 4 - year / 100 + year / 400 + DateTimePartUtil.DayOfWeekMonthTable[month - 1] + day) % 7);
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x00083DB6 File Offset: 0x00081FB6
		public static int DayOfMonthToDayOfWeekInMonth(int day)
		{
			return (day + 6) / 7;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x00083DC0 File Offset: 0x00081FC0
		public static int DayOfWeekInMonthToDayOfMonth(int year, int month, DayOfWeek dayOfWeek, int dayOfWeekInMonth)
		{
			DayOfWeek dayOfWeek2 = DateTimePartUtil.DateToDayOfWeek(year, month, 1);
			return ((dayOfWeek >= dayOfWeek2) ? (dayOfWeek - dayOfWeek2 + 1) : (dayOfWeek - dayOfWeek2 + 8)) + (dayOfWeekInMonth - 1) * 7;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00083DEC File Offset: 0x00081FEC
		public static int GetDaysInMonth(int year, int month)
		{
			if (month == 2 && DateTimePartUtil.IsLeapYear(year))
			{
				return 29;
			}
			return DateTimePartUtil.MonthLengths[month];
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x00083E04 File Offset: 0x00082004
		public static bool IsLeapYear(int year)
		{
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x00083E20 File Offset: 0x00082020
		public static bool DayOfYearToDate(int year, int dayOfYear, out int month, out int day)
		{
			if (dayOfYear > 0 && dayOfYear <= 366)
			{
				Record<byte, byte> record = DateTimePartUtil.DayOfYearToDateLookup[(DateTimePartUtil.IsLeapYear(year) > false) ? 1 : 0, dayOfYear];
				month = (int)record.Item1;
				if (month > 0)
				{
					day = (int)record.Item2;
					return true;
				}
			}
			month = 0;
			day = 0;
			return false;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x00083E6C File Offset: 0x0008206C
		private static Record<byte, byte> _DayOfYearToDate(int year, int dayOfYear)
		{
			int num = dayOfYear;
			for (int i = 1; i <= 12; i++)
			{
				int daysInMonth = DateTimePartUtil.GetDaysInMonth(year, i);
				if (num <= daysInMonth)
				{
					return new Record<byte, byte>((byte)i, (byte)num);
				}
				num -= daysInMonth;
			}
			throw new Exception(FormattableString.Invariant(FormattableStringFactory.Create("There is no day of year {0} in year {1}.", new object[] { dayOfYear, year })));
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x00083ECE File Offset: 0x000820CE
		public static int DateToDayOfYear(int year, int month, int day)
		{
			return DateTimePartUtil.CumulativeDaysBeforeMonth[(DateTimePartUtil.IsLeapYear(year) > false) ? 1 : 0, month] + day;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00083EE8 File Offset: 0x000820E8
		public static void DateToWeekOfYear(int year, int month, int day, out int weekYear, out int weekOfYear, out DayOfWeek dayOfWeek)
		{
			int num = DateTimePartUtil.DateToDayOfYear(year, month, day);
			dayOfWeek = DateTimePartUtil.DateToDayOfWeek(year, month, day);
			int num2 = (int)((dayOfWeek == DayOfWeek.Sunday) ? ((DayOfWeek)7) : dayOfWeek);
			weekOfYear = (num - num2 + 10) / 7;
			int num3 = weekOfYear;
			if (num3 == 0)
			{
				weekYear = year - 1;
				weekOfYear = (DateTimePartUtil.IsLongIsoWeekYear(year - 1) ? 53 : 52);
				return;
			}
			if (num3 != 53)
			{
				weekYear = year;
				return;
			}
			if (DateTimePartUtil.IsLongIsoWeekYear(year))
			{
				weekYear = year;
				weekOfYear = 53;
				return;
			}
			weekYear = year + 1;
			weekOfYear = 1;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x00083F65 File Offset: 0x00082165
		internal static bool IsLongIsoWeekYear(int weekYear)
		{
			return DateTimePartUtil.<IsLongIsoWeekYear>g__p|17_0(weekYear) == 4 || DateTimePartUtil.<IsLongIsoWeekYear>g__p|17_0(weekYear - 1) == 3;
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x00083F80 File Offset: 0x00082180
		public static void WeekOfYearToDate(int weekYear, int weekOfYear, DayOfWeek dayOfWeek, out int year, out int month, out int day)
		{
			int num = (int)((dayOfWeek == DayOfWeek.Sunday) ? ((DayOfWeek)7) : dayOfWeek);
			DayOfWeek dayOfWeek2 = DateTimePartUtil.DateToDayOfWeek(weekYear, 1, 3);
			int num2 = weekOfYear * 7 + num - (int)(dayOfWeek2 + 4);
			if (DateTimePartUtil.DayOfYearToDate(weekYear, num2, out month, out day))
			{
				year = weekYear;
				return;
			}
			if (num2 <= 0)
			{
				year = weekYear - 1;
				month = 12;
				day = 31 + num2;
				return;
			}
			year = weekYear + 1;
			month = 1;
			day = num2 - (DateTimePartUtil.IsLeapYear(weekYear) ? 366 : 365);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x00083FF4 File Offset: 0x000821F4
		public static int GetValue(this DateTime dt, DateTimePart part)
		{
			switch (part)
			{
			case DateTimePart.Year:
				return dt.Year;
			case DateTimePart.Month:
				return dt.Month;
			case DateTimePart.Day:
				return dt.Day;
			case DateTimePart.Hour:
				return dt.Hour;
			case DateTimePart.Minute:
				return dt.Minute;
			case DateTimePart.Second:
				return dt.Second;
			case DateTimePart.Millisecond:
				return dt.Millisecond;
			case DateTimePart.HourInPeriod:
			{
				int num = dt.Hour % 12;
				if (num != 0)
				{
					return num;
				}
				return 12;
			}
			case DateTimePart.Period:
				return (dt.Hour >= 12) ? 1 : 0;
			case DateTimePart.DayOfWeek:
				return (int)dt.DayOfWeek;
			case DateTimePart.Quarter:
				return DateTimePartUtil.MonthToQuarter(dt.Month);
			case DateTimePart.DayOfYear:
				return dt.DayOfYear;
			case DateTimePart.DayOfWeekInMonth:
				return DateTimePartUtil.DayOfMonthToDayOfWeekInMonth(dt.Day);
			case DateTimePart.WeekYear:
			{
				int num2;
				int num3;
				DayOfWeek dayOfWeek;
				DateTimePartUtil.DateToWeekOfYear(dt.Year, dt.Month, dt.Day, out num2, out num3, out dayOfWeek);
				return num2;
			}
			case DateTimePart.WeekOfYear:
			{
				int num3;
				DayOfWeek dayOfWeek;
				int num4;
				DateTimePartUtil.DateToWeekOfYear(dt.Year, dt.Month, dt.Day, out num3, out num4, out dayOfWeek);
				return num4;
			}
			case DateTimePart.TimeZoneOffset:
				return 0;
			default:
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown DateTimePart: {0}.", new object[] { part })));
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x0008413C File Offset: 0x0008233C
		public static int GetValue(this DateTimeOffset dt, DateTimePart part)
		{
			if (part == DateTimePart.TimeZoneOffset)
			{
				return (int)dt.Offset.TotalMinutes;
			}
			return dt.DateTime.GetValue(part);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x0008416C File Offset: 0x0008236C
		public static bool CanExplainFullDate(this DateTimePartSet parts)
		{
			return DateTimePartUtil.FullDateInfoSets.Any(new Func<DateTimePartSet, bool>(parts.Contains));
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0008418C File Offset: 0x0008238C
		public static bool CanExplain(this DateTimePartSet parts, DateTimePart toExplain)
		{
			if (parts.Contains(toExplain))
			{
				return true;
			}
			if (toExplain != DateTimePart.Hour)
			{
				switch (toExplain)
				{
				case DateTimePart.HourInPeriod:
					if (parts.Contains(DateTimePart.Hour))
					{
						return true;
					}
					break;
				case DateTimePart.Period:
					if (parts.Contains(DateTimePart.Hour))
					{
						return true;
					}
					break;
				case DateTimePart.Quarter:
					if (parts.Contains(DateTimePart.Month))
					{
						return true;
					}
					break;
				case DateTimePart.DayOfWeekInMonth:
					if (parts.Contains(DateTimePart.Day))
					{
						return true;
					}
					break;
				}
			}
			else if (parts.Contains(DateTimePart.HourInPeriod) && parts.Contains(DateTimePart.Period))
			{
				return true;
			}
			return toExplain.GetKind() == DateTimePartKind.Date && parts.CanExplainFullDate();
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x00084228 File Offset: 0x00082428
		public static bool CanExplain(this DateTimePartSet parts, DateTimePartSet partsToExplain)
		{
			partsToExplain = partsToExplain.SetDifference(parts);
			return !partsToExplain.Any() || partsToExplain.AsEnumerable().All((DateTimePart toExplain) => parts.CanExplain(toExplain));
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00084274 File Offset: 0x00082474
		public static bool CanExplain(this DateTimePartSet parts, IEnumerable<DateTimePart> partsToExplain)
		{
			return parts.CanExplain(new DateTimePartSet(partsToExplain));
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00084284 File Offset: 0x00082484
		public static DateTimePartKind GetKind(this DateTimePart part)
		{
			switch (part)
			{
			case DateTimePart.Year:
			case DateTimePart.Month:
			case DateTimePart.Day:
			case DateTimePart.DayOfWeek:
			case DateTimePart.Quarter:
			case DateTimePart.DayOfYear:
			case DateTimePart.DayOfWeekInMonth:
			case DateTimePart.WeekYear:
			case DateTimePart.WeekOfYear:
				return DateTimePartKind.Date;
			case DateTimePart.Hour:
			case DateTimePart.Minute:
			case DateTimePart.Second:
			case DateTimePart.Millisecond:
			case DateTimePart.HourInPeriod:
			case DateTimePart.Period:
			case DateTimePart.TimeZoneOffset:
				return DateTimePartKind.Time;
			default:
				throw new NotImplementedException("Unknown DateTimePart: " + part.ToString());
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000842FC File Offset: 0x000824FC
		private static int[] _MinValues()
		{
			int[] array = new int[DateTimePartUtil.PartKindCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((DateTimePart)i)._MinValue();
			}
			return array;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0008432C File Offset: 0x0008252C
		private static int[] _MaxValues()
		{
			int[] array = new int[DateTimePartUtil.PartKindCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((DateTimePart)i).MaxValue();
			}
			return array;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x0008435C File Offset: 0x0008255C
		private static int _MinValue(this DateTimePart part)
		{
			switch (part)
			{
			case DateTimePart.Year:
			case DateTimePart.Month:
			case DateTimePart.Day:
			case DateTimePart.HourInPeriod:
			case DateTimePart.Quarter:
			case DateTimePart.DayOfYear:
			case DateTimePart.DayOfWeekInMonth:
			case DateTimePart.WeekYear:
			case DateTimePart.WeekOfYear:
				return 1;
			case DateTimePart.Hour:
			case DateTimePart.Minute:
			case DateTimePart.Second:
			case DateTimePart.Millisecond:
			case DateTimePart.Period:
			case DateTimePart.DayOfWeek:
				return 0;
			case DateTimePart.TimeZoneOffset:
				return -840;
			default:
				throw new NotImplementedException("Unknown DateTimePart: " + part.ToString());
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000843D8 File Offset: 0x000825D8
		public static int MaxValue(this DateTimePart part)
		{
			switch (part)
			{
			case DateTimePart.Year:
			case DateTimePart.WeekYear:
				return 9999;
			case DateTimePart.Month:
				return 12;
			case DateTimePart.Day:
				return 31;
			case DateTimePart.Hour:
				return 23;
			case DateTimePart.Minute:
			case DateTimePart.Second:
				return 59;
			case DateTimePart.Millisecond:
				return 999;
			case DateTimePart.HourInPeriod:
				return 12;
			case DateTimePart.Period:
				return 1;
			case DateTimePart.DayOfWeek:
				return 6;
			case DateTimePart.Quarter:
				return 4;
			case DateTimePart.DayOfYear:
				return 366;
			case DateTimePart.DayOfWeekInMonth:
				return 5;
			case DateTimePart.WeekOfYear:
				return 53;
			case DateTimePart.TimeZoneOffset:
				return 840;
			default:
				throw new NotImplementedException("Unknown DateTimePart: " + part.ToString());
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0008447B File Offset: 0x0008267B
		public static IEnumerable<int> AllAllowedValues(this DateTimePart part)
		{
			if (part == DateTimePart.TimeZoneOffset)
			{
				int num2;
				for (int hour = -24; hour <= 23; hour = num2 + 1)
				{
					foreach (int num in new int[] { 0, 30, 45 })
					{
						if (hour >= 0 || num != 45)
						{
							yield return 60 * hour + num;
						}
					}
					int[] array = null;
					num2 = hour;
				}
				yield return 1440;
			}
			else
			{
				int max = part.MaxValue();
				int num2;
				for (int hour = part._MinValue(); hour <= max; hour = num2 + 1)
				{
					yield return hour;
					num2 = hour;
				}
			}
			yield break;
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000845C6 File Offset: 0x000827C6
		[CompilerGenerated]
		internal static int <IsLongIsoWeekYear>g__p|17_0(int year)
		{
			return (year + year / 4 - year / 100 + year / 400) % 7;
		}

		// Token: 0x0400165A RID: 5722
		private static readonly int[] DayOfWeekMonthTable = new int[]
		{
			0, 3, 2, 5, 0, 3, 5, 1, 4, 6,
			2, 4
		};

		// Token: 0x0400165B RID: 5723
		private static readonly int[] MonthLengths = new int[]
		{
			0, 31, 28, 31, 30, 31, 30, 31, 31, 30,
			31, 30, 31
		};

		// Token: 0x0400165C RID: 5724
		private const int ExampleLeapYear = 2004;

		// Token: 0x0400165D RID: 5725
		private const int ExampleNonLeapYear = 1999;

		// Token: 0x0400165E RID: 5726
		private static readonly Record<byte, byte>[,] DayOfYearToDateLookup = new bool[]
		{
			default(bool),
			true
		}.Select((bool isLeapYear) => Enumerable.Range(0, 367).Select(delegate(int dayOfYear)
		{
			if (dayOfYear != 0 && (dayOfYear != 366 || isLeapYear))
			{
				return DateTimePartUtil._DayOfYearToDate(isLeapYear ? 2004 : 1999, dayOfYear);
			}
			return default(Record<byte, byte>);
		}).ToArray<Record<byte, byte>>()).ToArray<Record<byte, byte>[]>().ToMultidimensionalArray<Record<byte, byte>>();

		// Token: 0x0400165F RID: 5727
		private static readonly int[,] CumulativeDaysBeforeMonth = new bool[]
		{
			default(bool),
			true
		}.Select((bool isLeapYear) => Enumerable.Range(1, 12).Aggregate(new int[13], delegate(int[] arr, int i)
		{
			arr[i] = arr[i - 1] + DateTimePartUtil.GetDaysInMonth(isLeapYear ? 2004 : 1999, i - 1);
			return arr;
		})).ToArray<int[]>().ToMultidimensionalArray<int>();

		// Token: 0x04001660 RID: 5728
		private static readonly DateTimePartSet[] FullDateInfoSets = new DateTimePartSet[]
		{
			new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Year,
				DateTimePart.Month,
				DateTimePart.Day
			}),
			new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Year,
				DateTimePart.DayOfYear
			}),
			new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.Year,
				DateTimePart.Month,
				DateTimePart.DayOfWeek,
				DateTimePart.DayOfWeekInMonth
			}),
			new DateTimePartSet(new DateTimePart[]
			{
				DateTimePart.WeekYear,
				DateTimePart.WeekOfYear,
				DateTimePart.DayOfWeek
			})
		};

		// Token: 0x04001661 RID: 5729
		public static readonly int PartKindCount = Enum.GetValues(typeof(DateTimePart)).Length;

		// Token: 0x04001662 RID: 5730
		internal static readonly int[] MinValues = DateTimePartUtil._MinValues();

		// Token: 0x04001663 RID: 5731
		internal static readonly int[] MaxValues = DateTimePartUtil._MaxValues();
	}
}
