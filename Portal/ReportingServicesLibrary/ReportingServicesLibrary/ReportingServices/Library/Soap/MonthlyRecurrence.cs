using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200033B RID: 827
	public class MonthlyRecurrence : RecurrencePattern
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000707EA File Offset: 0x0006E9EA
		// (set) Token: 0x06001BA8 RID: 7080 RVA: 0x000707F2 File Offset: 0x0006E9F2
		public string Days { get; set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000707FB File Offset: 0x0006E9FB
		// (set) Token: 0x06001BAA RID: 7082 RVA: 0x00070803 File Offset: 0x0006EA03
		public MonthsOfYearSelector MonthsOfYear { get; set; }

		// Token: 0x06001BAB RID: 7083 RVA: 0x0007080C File Offset: 0x0006EA0C
		internal static void WriteToXml(MonthlyRecurrence recurrence, XmlTextWriter xml)
		{
			bool flag = recurrence.MonthsOfYear.January || recurrence.MonthsOfYear.February || recurrence.MonthsOfYear.March || recurrence.MonthsOfYear.April || recurrence.MonthsOfYear.May || recurrence.MonthsOfYear.June || recurrence.MonthsOfYear.July || recurrence.MonthsOfYear.August || recurrence.MonthsOfYear.September || recurrence.MonthsOfYear.October || recurrence.MonthsOfYear.November || recurrence.MonthsOfYear.December;
			if (recurrence == null || !flag)
			{
				return;
			}
			string text = "1";
			if (recurrence.Days != null)
			{
				text = recurrence.Days;
			}
			uint num = ((recurrence.MonthsOfYear != null) ? recurrence.MonthsOfYear.ToUint() : 0U);
			new Monthly(Monthly.GetDayBitMap(text, (Months)num), num).ToXml(xml);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00070903 File Offset: 0x0006EB03
		internal static MonthlyRecurrence TriggerDataToThis(Monthly data)
		{
			return new MonthlyRecurrence
			{
				Days = Monthly.GetDayRange(data.DaysOfMonth),
				MonthsOfYear = MonthsOfYearSelector.UintToThis(data.Months)
			};
		}
	}
}
