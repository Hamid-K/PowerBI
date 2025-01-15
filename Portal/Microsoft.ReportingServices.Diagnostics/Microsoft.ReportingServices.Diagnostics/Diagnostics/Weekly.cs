using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000072 RID: 114
	internal class Weekly : BaseTriggerData
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000FB09 File Offset: 0x0000DD09
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000FB11 File Offset: 0x0000DD11
		public long WeeksInterval
		{
			get
			{
				return this.m_weeksInterval;
			}
			set
			{
				this.m_weeksInterval = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000FB1A File Offset: 0x0000DD1A
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000FB22 File Offset: 0x0000DD22
		public uint DaysOfWeek
		{
			get
			{
				return this.m_daysOfWeek;
			}
			set
			{
				this.m_daysOfWeek = value;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		public Weekly(long weeksInterval, uint daysOfWeek)
		{
			this.WeeksInterval = weeksInterval;
			this.DaysOfWeek = daysOfWeek;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public override void ToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("WeeklyRecurrence", Task.TaskNamespace);
			writer.WriteElementString("WeeksInterval", this.WeeksInterval.ToString(CultureInfo.InvariantCulture));
			base.WriteDaysElements(this.DaysOfWeek, writer);
			writer.WriteEndElement();
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000FB92 File Offset: 0x0000DD92
		public bool IsWeekly
		{
			get
			{
				return this.WeeksInterval == 1L;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public override bool Equals(object trigger)
		{
			bool flag = false;
			if (trigger is Weekly)
			{
				Weekly weekly = (Weekly)trigger;
				if (weekly.DaysOfWeek == this.DaysOfWeek && weekly.WeeksInterval == this.WeeksInterval)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000357 RID: 855
		private long m_weeksInterval;

		// Token: 0x04000358 RID: 856
		private uint m_daysOfWeek;
	}
}
