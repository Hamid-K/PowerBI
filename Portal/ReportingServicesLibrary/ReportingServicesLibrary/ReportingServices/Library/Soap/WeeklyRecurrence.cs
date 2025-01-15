using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200033A RID: 826
	public class WeeklyRecurrence : RecurrencePattern
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x000706DC File Offset: 0x0006E8DC
		// (set) Token: 0x06001B9E RID: 7070 RVA: 0x000706E4 File Offset: 0x0006E8E4
		public int WeeksInterval { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000706ED File Offset: 0x0006E8ED
		// (set) Token: 0x06001BA0 RID: 7072 RVA: 0x000706F5 File Offset: 0x0006E8F5
		[XmlIgnore]
		public bool WeeksIntervalSpecified { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000706FE File Offset: 0x0006E8FE
		// (set) Token: 0x06001BA2 RID: 7074 RVA: 0x00070706 File Offset: 0x0006E906
		public DaysOfWeekSelector DaysOfWeek { get; set; }

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00070710 File Offset: 0x0006E910
		internal static void WriteToXml(WeeklyRecurrence recurrence, XmlTextWriter xml)
		{
			bool flag = recurrence.DaysOfWeek.Monday || recurrence.DaysOfWeek.Tuesday || recurrence.DaysOfWeek.Wednesday || recurrence.DaysOfWeek.Thursday || recurrence.DaysOfWeek.Friday || recurrence.DaysOfWeek.Saturday || recurrence.DaysOfWeek.Sunday;
			if (recurrence == null || recurrence.DaysOfWeek == null || !flag)
			{
				return;
			}
			int num = WeeklyRecurrence.DefaultWeeksInterval;
			if (recurrence.WeeksIntervalSpecified)
			{
				num = recurrence.WeeksInterval;
			}
			new Weekly((long)num, recurrence.DaysOfWeek.ToUint()).ToXml(xml);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000707B6 File Offset: 0x0006E9B6
		internal static WeeklyRecurrence TriggerDataToThis(Weekly data)
		{
			return new WeeklyRecurrence
			{
				WeeksInterval = (int)data.WeeksInterval,
				WeeksIntervalSpecified = true,
				DaysOfWeek = DaysOfWeekSelector.UintToThis(data.DaysOfWeek)
			};
		}

		// Token: 0x04000B21 RID: 2849
		internal static int DefaultWeeksInterval = 1;
	}
}
