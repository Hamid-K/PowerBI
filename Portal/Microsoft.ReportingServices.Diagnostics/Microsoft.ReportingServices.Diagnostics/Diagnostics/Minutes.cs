using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000070 RID: 112
	internal class Minutes : BaseTriggerData
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000F9D5 File Offset: 0x0000DBD5
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		public int MinutesInterval
		{
			get
			{
				return this.m_minutesInterval;
			}
			set
			{
				this.m_minutesInterval = value;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000F9E6 File Offset: 0x0000DBE6
		public Minutes(int minutesInterval)
		{
			this.MinutesInterval = minutesInterval;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public override void ToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("MinuteRecurrence", Task.TaskNamespace);
			writer.WriteElementString("MinutesInterval", this.MinutesInterval.ToString(CultureInfo.InvariantCulture));
			writer.WriteEndElement();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public override bool Equals(object trigger)
		{
			bool flag = false;
			if (trigger is Minutes && ((Minutes)trigger).MinutesInterval == this.MinutesInterval)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000355 RID: 853
		private int m_minutesInterval;
	}
}
