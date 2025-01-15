using System;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000074 RID: 116
	internal class MonthlyDOW : BaseTriggerData
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000FDDD File Offset: 0x0000DFDD
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		public uint Week
		{
			get
			{
				return this.m_week;
			}
			set
			{
				this.m_week = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000FDEE File Offset: 0x0000DFEE
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000FDF6 File Offset: 0x0000DFF6
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000FDFF File Offset: 0x0000DFFF
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000FE07 File Offset: 0x0000E007
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

		// Token: 0x060003BE RID: 958 RVA: 0x0000FE10 File Offset: 0x0000E010
		public MonthlyDOW(uint week, uint daysOfWeek, uint months)
		{
			this.m_week = week;
			this.m_daysOfWeek = daysOfWeek;
			this.m_months = months;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000FE2D File Offset: 0x0000E02D
		public override void ToXml(XmlTextWriter writer)
		{
			writer.WriteStartElement("MonthlyDOWRecurrence", Task.TaskNamespace);
			base.WriteWeeksOfMonth(this.Week, writer);
			base.WriteDaysElements(this.DaysOfWeek, writer);
			base.WriteMonthsElements(this.Months, writer);
			writer.WriteEndElement();
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000FE6C File Offset: 0x0000E06C
		public new bool IsEveryMonth
		{
			get
			{
				return base.IsEveryMonth(this.Months);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public override bool Equals(object trigger)
		{
			bool flag = false;
			if (trigger is MonthlyDOW)
			{
				MonthlyDOW monthlyDOW = (MonthlyDOW)trigger;
				if (monthlyDOW.DaysOfWeek == this.DaysOfWeek && monthlyDOW.Months == this.Months && monthlyDOW.Week == this.Week)
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E75E File Offset: 0x0000C95E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400035B RID: 859
		private uint m_week;

		// Token: 0x0400035C RID: 860
		private uint m_daysOfWeek;

		// Token: 0x0400035D RID: 861
		private uint m_months;
	}
}
