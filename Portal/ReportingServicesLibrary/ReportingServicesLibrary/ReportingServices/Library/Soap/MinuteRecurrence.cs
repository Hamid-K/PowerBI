using System;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000338 RID: 824
	public class MinuteRecurrence : RecurrencePattern
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0007064A File Offset: 0x0006E84A
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x00070652 File Offset: 0x0006E852
		public int MinutesInterval { get; set; }

		// Token: 0x06001B95 RID: 7061 RVA: 0x0007065B File Offset: 0x0006E85B
		internal static void WriteToXml(MinuteRecurrence recurrence, XmlTextWriter xml)
		{
			if (recurrence == null || recurrence.MinutesInterval <= 0)
			{
				return;
			}
			new Minutes(recurrence.MinutesInterval).ToXml(xml);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0007067B File Offset: 0x0006E87B
		internal static MinuteRecurrence TriggerDataToThis(Minutes data)
		{
			return new MinuteRecurrence
			{
				MinutesInterval = data.MinutesInterval
			};
		}
	}
}
