using System;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000073 RID: 115
	internal class Monthly : BaseTriggerData
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000FBDD File Offset: 0x0000DDDD
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public uint DaysOfMonth
		{
			get
			{
				return this.m_daysOfMonth;
			}
			set
			{
				this.m_daysOfMonth = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000FBEE File Offset: 0x0000DDEE
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000FBF6 File Offset: 0x0000DDF6
		public uint Months
		{
			get
			{
				return this.m_months;
			}
			set
			{
				this.m_months = value;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000FBFF File Offset: 0x0000DDFF
		public Monthly(uint daysOfMonth, uint months)
		{
			this.DaysOfMonth = daysOfMonth;
			this.Months = months;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000FC15 File Offset: 0x0000DE15
		public override void ToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("MonthlyRecurrence", Task.TaskNamespace);
			writer.WriteElementString("Days", Monthly.GetDayRange(this.DaysOfMonth));
			base.WriteMonthsElements(this.Months, writer);
			writer.WriteEndElement();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000FC50 File Offset: 0x0000DE50
		public static uint GetDayBitMap(string daysString, Months months)
		{
			uint num;
			if (ScheduleFieldValidation.TryGetDayBitMap(daysString, months, Localization.ClientPrimaryCulture, out num))
			{
				return num;
			}
			throw new InvalidElementException("Days");
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public static string GetDayRange(uint dayBitMap)
		{
			string text = "";
			string listSeparator = Localization.ClientPrimaryCulture.TextInfo.ListSeparator;
			int num = 1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i < 33; i++)
			{
				if (((long)num & (long)((ulong)dayBitMap)) > 0L && i != 32)
				{
					if (num2 == 0)
					{
						num2 = i;
					}
					num3 = i;
				}
				else if (num2 != 0)
				{
					if (num2 == num3)
					{
						text = text + num2.ToString(CultureInfo.InvariantCulture) + listSeparator;
					}
					else if (num2 + 1 == num3)
					{
						text = string.Concat(new string[]
						{
							text,
							num2.ToString(CultureInfo.InvariantCulture),
							listSeparator,
							num3.ToString(CultureInfo.InvariantCulture),
							listSeparator
						});
					}
					else
					{
						text = string.Concat(new string[]
						{
							text,
							num2.ToString(CultureInfo.InvariantCulture),
							"-",
							num3.ToString(CultureInfo.InvariantCulture),
							listSeparator
						});
					}
					num2 = 0;
				}
				num <<= 1;
			}
			if (text.Length > 0)
			{
				text = text.TrimEnd(listSeparator.ToCharArray());
			}
			return text;
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000FD92 File Offset: 0x0000DF92
		public new bool IsEveryMonth
		{
			get
			{
				return base.IsEveryMonth(this.Months);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		public override bool Equals(object trigger)
		{
			bool flag = false;
			if (trigger is Monthly)
			{
				Monthly monthly = (Monthly)trigger;
				if (monthly.DaysOfMonth == this.DaysOfMonth && monthly.Months == this.Months)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000359 RID: 857
		private uint m_daysOfMonth;

		// Token: 0x0400035A RID: 858
		private uint m_months;
	}
}
