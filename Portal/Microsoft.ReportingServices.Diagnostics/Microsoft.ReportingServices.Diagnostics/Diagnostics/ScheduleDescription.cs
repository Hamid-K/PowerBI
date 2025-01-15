using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000060 RID: 96
	internal class ScheduleDescription
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public ScheduleDescription()
			: this(Thread.CurrentThread.CurrentCulture)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CC56 File Offset: 0x0000AE56
		public ScheduleDescription(CultureInfo culture)
		{
			this.m_culture = culture;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public string OnceDescription(DateTime startDate, DateTime endDate)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4 = ScheduleStringsWrapper.OnceScheduleDescription(text2, text);
			if (text3 != null)
			{
				text4 = text4 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text4;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public string MinutesDescription(DateTime startDate, DateTime endDate, int minutes)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4 = ScheduleStringsWrapper.MinutesScheduleDescription(minutes / 60, minutes % 60, text, text2);
			if (text3 != null)
			{
				text4 = text4 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text4;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public string DailyDescription(DateTime startDate, DateTime endDate, long daysInterval)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4;
			if (daysInterval == 1L)
			{
				text4 = ScheduleStringsWrapper.DailyScheduleDescription(text2, text);
			}
			else
			{
				text4 = ScheduleStringsWrapper.DailyWithIntervalScheduleDescription(text2, daysInterval, text);
			}
			if (text3 != null)
			{
				text4 = text4 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text4;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CD34 File Offset: 0x0000AF34
		public string WeeklyDescription(DateTime startDate, DateTime endDate, uint daysOfWeek, long weeksInterval)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4 = this.DaysOfWeekString(daysOfWeek);
			string text5;
			if (weeksInterval == 1L)
			{
				text5 = ScheduleStringsWrapper.WeeklyScheduleDescription(text2, text4, text);
			}
			else
			{
				text5 = ScheduleStringsWrapper.WeeklyWithIntervalScheduleDescription(text2, text4, weeksInterval, text);
			}
			if (text3 != null)
			{
				text5 = text5 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text5;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CD90 File Offset: 0x0000AF90
		public string MonthlyDescription(DateTime startDate, DateTime endDate, string daysOfMonth, uint months)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4;
			if (months == 4095U)
			{
				text4 = ScheduleStringsWrapper.MonthlyEveryMonthScheduleDescription(text2, daysOfMonth, text);
			}
			else
			{
				text4 = ScheduleStringsWrapper.MontlyScheduleDescription(text2, daysOfMonth, this.MonthsOfYearString(months), text);
			}
			if (text3 != null)
			{
				text4 = text4 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text4;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public string MonthlyDOWDescription(DateTime startDate, DateTime endDate, uint daysOfWeek, WeeksOfMonth weeks, uint months)
		{
			string text;
			string text2;
			string text3;
			this.GetBoundaryStrings(startDate, endDate, out text, out text2, out text3);
			string text4 = this.DaysOfWeekString(daysOfWeek);
			string text5 = this.WeeksString(weeks);
			string text6;
			if (months == 4095U)
			{
				text6 = ScheduleStringsWrapper.MonthlyDOWEveryMonthScheduleDescription(text2, text5, text4, text);
			}
			else
			{
				text6 = ScheduleStringsWrapper.MontlyDOWScheduleDescription(text2, text5, text4, this.MonthsOfYearString(months), text);
			}
			if (text3 != null)
			{
				text6 = text6 + " " + ScheduleStringsWrapper.EndScheduleDescription(text3);
			}
			return text6;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CE5C File Offset: 0x0000B05C
		private void GetBoundaryStrings(DateTime startDate, DateTime endDate, out string startDateString, out string startTimeString, out string endDateString)
		{
			startTimeString = startDate.ToString("t", this.m_culture);
			startDateString = startDate.ToString("d", this.m_culture);
			if (endDate != DateTime.MinValue)
			{
				endDateString = endDate.ToString("d", this.m_culture);
				return;
			}
			endDateString = null;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		private string DaysOfWeekString(uint days)
		{
			string text = "";
			string listSeparator = this.m_culture.TextInfo.ListSeparator;
			for (int i = 1; i <= 64; i *= 2)
			{
				if (((ulong)days & (ulong)((long)i)) > 0UL)
				{
					string text2 = text;
					string text3 = " ";
					DaysOfWeek daysOfWeek = (DaysOfWeek)i;
					text = text2 + text3 + ScheduleStringsWrapper.Keys.GetString(daysOfWeek.ToString()) + listSeparator;
				}
			}
			return text.TrimEnd(listSeparator.ToCharArray());
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000CF25 File Offset: 0x0000B125
		private string WeeksString(WeeksOfMonth weeksOfMonth)
		{
			return ScheduleStringsWrapper.Keys.GetString(weeksOfMonth.ToString());
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000CF3C File Offset: 0x0000B13C
		private string MonthsOfYearString(uint months)
		{
			string text = "";
			string listSeparator = this.m_culture.TextInfo.ListSeparator;
			for (int i = 1; i <= 2048; i *= 2)
			{
				if (((ulong)months & (ulong)((long)i)) > 0UL)
				{
					string text2 = text;
					string text3 = " ";
					Months months2 = (Months)i;
					text = text2 + text3 + ScheduleStringsWrapper.Keys.GetString(months2.ToString()) + listSeparator;
				}
			}
			return text.TrimEnd(listSeparator.ToCharArray());
		}

		// Token: 0x040002F5 RID: 757
		private CultureInfo m_culture;
	}
}
