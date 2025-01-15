using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000071 RID: 113
	internal class Daily : BaseTriggerData
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000FA69 File Offset: 0x0000DC69
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000FA71 File Offset: 0x0000DC71
		public long DaysInterval
		{
			get
			{
				return this.m_daysInterval;
			}
			set
			{
				this.m_daysInterval = value;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FA7A File Offset: 0x0000DC7A
		public Daily(long daysInterval)
		{
			this.DaysInterval = daysInterval;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public override void ToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("DailyRecurrence", Task.TaskNamespace);
			writer.WriteElementString("DaysInterval", this.DaysInterval.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000FACD File Offset: 0x0000DCCD
		public bool IsEveryDay
		{
			get
			{
				return this.DaysInterval == 1L;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000FADC File Offset: 0x0000DCDC
		public override bool Equals(object trigger)
		{
			bool flag = false;
			if (trigger is Daily && ((Daily)trigger).DaysInterval == this.DaysInterval)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000356 RID: 854
		private long m_daysInterval;
	}
}
