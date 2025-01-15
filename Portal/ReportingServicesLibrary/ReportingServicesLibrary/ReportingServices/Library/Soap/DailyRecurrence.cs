using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000339 RID: 825
	public class DailyRecurrence : RecurrencePattern
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00070696 File Offset: 0x0006E896
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x0007069E File Offset: 0x0006E89E
		public int DaysInterval { get; set; }

		// Token: 0x06001B9A RID: 7066 RVA: 0x000706A7 File Offset: 0x0006E8A7
		internal static void WriteToXml(DailyRecurrence recurrence, XmlTextWriter xml)
		{
			if (recurrence == null || recurrence.DaysInterval <= 0)
			{
				return;
			}
			new Daily((long)recurrence.DaysInterval).ToXml(xml);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000706C8 File Offset: 0x0006E8C8
		internal static DailyRecurrence TriggerDataToThis(Daily data)
		{
			return new DailyRecurrence
			{
				DaysInterval = (int)data.DaysInterval
			};
		}
	}
}
