using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C8 RID: 712
	public static class WeekDayConverter
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x000422F1 File Offset: 0x000404F1
		public static WeekDay FromDayOfWeek(DayOfWeek dayOfWeek)
		{
			return WeekDayConverter.s_dayOfWeekToWeekDay[dayOfWeek];
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00042300 File Offset: 0x00040500
		public static WeekDay FromDaysOfWeek(IEnumerable<DayOfWeek> dayOfWeek)
		{
			WeekDay weekDay = WeekDay.None;
			foreach (DayOfWeek dayOfWeek2 in dayOfWeek)
			{
				weekDay |= WeekDayConverter.FromDayOfWeek(dayOfWeek2);
			}
			return weekDay;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00042350 File Offset: 0x00040550
		public static IEnumerable<DayOfWeek> ToDaysOfWeek(WeekDay weekDay)
		{
			List<DayOfWeek> list = new List<DayOfWeek>();
			using (IEnumerator<WeekDay> enumerator = ExtendedEnum.GetFlags<WeekDay>(weekDay).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WeekDay day = enumerator.Current;
					list.Add(WeekDayConverter.s_dayOfWeekToWeekDay.First((KeyValuePair<DayOfWeek, WeekDay> p) => p.Value == day).Key);
				}
			}
			return list;
		}

		// Token: 0x04000727 RID: 1831
		private static readonly Dictionary<DayOfWeek, WeekDay> s_dayOfWeekToWeekDay = new Dictionary<DayOfWeek, WeekDay>
		{
			{
				DayOfWeek.Sunday,
				WeekDay.Sunday
			},
			{
				DayOfWeek.Monday,
				WeekDay.Monday
			},
			{
				DayOfWeek.Tuesday,
				WeekDay.Tuesday
			},
			{
				DayOfWeek.Wednesday,
				WeekDay.Wednesday
			},
			{
				DayOfWeek.Thursday,
				WeekDay.Thursday
			},
			{
				DayOfWeek.Friday,
				WeekDay.Friday
			},
			{
				DayOfWeek.Saturday,
				WeekDay.Saturday
			}
		};
	}
}
