using System;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200006F RID: 111
	internal abstract class BaseTriggerData
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00002E32 File Offset: 0x00001032
		public BaseTriggerData()
		{
		}

		// Token: 0x06000392 RID: 914
		public abstract void ToXml(XmlTextWriter writer);

		// Token: 0x06000393 RID: 915 RVA: 0x0000F75C File Offset: 0x0000D95C
		public void WriteDaysElements(uint days, XmlTextWriter writer)
		{
			writer.WriteStartElement("DaysOfWeek");
			if ((days & 1U) == 1U)
			{
				writer.WriteElementString("Sunday", "true");
			}
			if ((days & 2U) == 2U)
			{
				writer.WriteElementString("Monday", "true");
			}
			if ((days & 4U) == 4U)
			{
				writer.WriteElementString("Tuesday", "true");
			}
			if ((days & 8U) == 8U)
			{
				writer.WriteElementString("Wednesday", "true");
			}
			if ((days & 16U) == 16U)
			{
				writer.WriteElementString("Thursday", "true");
			}
			if ((days & 32U) == 32U)
			{
				writer.WriteElementString("Friday", "true");
			}
			if ((days & 64U) == 64U)
			{
				writer.WriteElementString("Saturday", "true");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public void WriteMonthsElements(uint months, XmlTextWriter writer)
		{
			writer.WriteStartElement("MonthsOfYear");
			if ((months & 1U) == 1U)
			{
				writer.WriteElementString("January", "true");
			}
			if ((months & 2U) == 2U)
			{
				writer.WriteElementString("February", "true");
			}
			if ((months & 4U) == 4U)
			{
				writer.WriteElementString("March", "true");
			}
			if ((months & 8U) == 8U)
			{
				writer.WriteElementString("April", "true");
			}
			if ((months & 16U) == 16U)
			{
				writer.WriteElementString("May", "true");
			}
			if ((months & 32U) == 32U)
			{
				writer.WriteElementString("June", "true");
			}
			if ((months & 64U) == 64U)
			{
				writer.WriteElementString("July", "true");
			}
			if ((months & 128U) == 128U)
			{
				writer.WriteElementString("August", "true");
			}
			if ((months & 256U) == 256U)
			{
				writer.WriteElementString("September", "true");
			}
			if ((months & 512U) == 512U)
			{
				writer.WriteElementString("October", "true");
			}
			if ((months & 1024U) == 1024U)
			{
				writer.WriteElementString("November", "true");
			}
			if ((months & 2048U) == 2048U)
			{
				writer.WriteElementString("December", "true");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F970 File Offset: 0x0000DB70
		public void WriteWeeksOfMonth(uint weeks, XmlTextWriter writer)
		{
			string text = "";
			if (weeks == 1U)
			{
				text = "FirstWeek";
			}
			else if (weeks == 2U)
			{
				text = "SecondWeek";
			}
			else if (weeks == 3U)
			{
				text = "ThirdWeek";
			}
			else if (weeks == 4U)
			{
				text = "FourthWeek";
			}
			else if (weeks == 5U)
			{
				text = "LastWeek";
			}
			writer.WriteElementString("WhichWeek", text);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F9CB File Offset: 0x0000DBCB
		public bool IsEveryMonth(uint months)
		{
			return months == 4095U;
		}
	}
}
