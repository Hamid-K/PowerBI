using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200033C RID: 828
	public class MonthlyDOWRecurrence : RecurrencePattern
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x0007092C File Offset: 0x0006EB2C
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x00070934 File Offset: 0x0006EB34
		public WeekNumberEnum WhichWeek { get; set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0007093D File Offset: 0x0006EB3D
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x00070945 File Offset: 0x0006EB45
		[XmlIgnore]
		public bool WhichWeekSpecified { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x0007094E File Offset: 0x0006EB4E
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x00070956 File Offset: 0x0006EB56
		public DaysOfWeekSelector DaysOfWeek { get; set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x0007095F File Offset: 0x0006EB5F
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x00070967 File Offset: 0x0006EB67
		public MonthsOfYearSelector MonthsOfYear { get; set; }

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00070970 File Offset: 0x0006EB70
		internal static void WriteToXml(MonthlyDOWRecurrence recurrence, XmlTextWriter xml)
		{
			if (recurrence == null)
			{
				return;
			}
			WeekNumberEnum weekNumberEnum = WeekNumberEnum.FirstWeek;
			if (recurrence.WhichWeekSpecified)
			{
				weekNumberEnum = recurrence.WhichWeek;
			}
			uint num = MonthlyDOWRecurrence.WeekNumberToUint(weekNumberEnum);
			uint num2 = ((recurrence.DaysOfWeek != null) ? recurrence.DaysOfWeek.ToUint() : 0U);
			uint num3 = ((recurrence.MonthsOfYear != null) ? recurrence.MonthsOfYear.ToUint() : 0U);
			bool flag = recurrence.MonthsOfYear.January || recurrence.MonthsOfYear.February || recurrence.MonthsOfYear.March || recurrence.MonthsOfYear.April || recurrence.MonthsOfYear.May || recurrence.MonthsOfYear.June || recurrence.MonthsOfYear.July || recurrence.MonthsOfYear.August || recurrence.MonthsOfYear.September || recurrence.MonthsOfYear.October || recurrence.MonthsOfYear.November || recurrence.MonthsOfYear.December;
			bool flag2 = recurrence.DaysOfWeek.Monday || recurrence.DaysOfWeek.Tuesday || recurrence.DaysOfWeek.Wednesday || recurrence.DaysOfWeek.Thursday || recurrence.DaysOfWeek.Friday || recurrence.DaysOfWeek.Saturday || recurrence.DaysOfWeek.Sunday;
			if (!flag || !flag2)
			{
				return;
			}
			new MonthlyDOW(num, num2, num3).ToXml(xml);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		internal static MonthlyDOWRecurrence TriggerDataToThis(MonthlyDOW data)
		{
			return new MonthlyDOWRecurrence
			{
				WhichWeek = MonthlyDOWRecurrence.UintToWeekNumber(data.Week),
				WhichWeekSpecified = true,
				DaysOfWeek = DaysOfWeekSelector.UintToThis(data.DaysOfWeek),
				MonthsOfYear = MonthsOfYearSelector.UintToThis(data.Months)
			};
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00070B2C File Offset: 0x0006ED2C
		internal static uint WeekNumberToUint(WeekNumberEnum n)
		{
			switch (n)
			{
			case WeekNumberEnum.FirstWeek:
				return 1U;
			case WeekNumberEnum.SecondWeek:
				return 2U;
			case WeekNumberEnum.ThirdWeek:
				return 3U;
			case WeekNumberEnum.FourthWeek:
				return 4U;
			case WeekNumberEnum.LastWeek:
				return 5U;
			default:
				return 0U;
			}
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00070B58 File Offset: 0x0006ED58
		internal static WeekNumberEnum UintToWeekNumber(uint n)
		{
			switch (n)
			{
			case 1U:
				return WeekNumberEnum.FirstWeek;
			case 2U:
				return WeekNumberEnum.SecondWeek;
			case 3U:
				return WeekNumberEnum.ThirdWeek;
			case 4U:
				return WeekNumberEnum.FourthWeek;
			case 5U:
				return WeekNumberEnum.LastWeek;
			default:
				return WeekNumberEnum.FirstWeek;
			}
		}
	}
}
