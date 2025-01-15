using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008E RID: 142
	internal sealed class TimeChangeRecurrence : ComplexProperty
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x00015626 File Offset: 0x00014626
		public TimeChangeRecurrence()
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001562E File Offset: 0x0001462E
		public TimeChangeRecurrence(DayOfTheWeekIndex dayOfTheWeekIndex, DayOfTheWeek dayOfTheWeek, Month month)
			: this()
		{
			this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(dayOfTheWeekIndex);
			this.dayOfTheWeek = new DayOfTheWeek?(dayOfTheWeek);
			this.month = new Month?(month);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001565A File Offset: 0x0001465A
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00015662 File Offset: 0x00014662
		public DayOfTheWeekIndex? DayOfTheWeekIndex
		{
			get
			{
				return this.dayOfTheWeekIndex;
			}
			set
			{
				this.SetFieldValue<DayOfTheWeekIndex?>(ref this.dayOfTheWeekIndex, value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00015671 File Offset: 0x00014671
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00015679 File Offset: 0x00014679
		public DayOfTheWeek? DayOfTheWeek
		{
			get
			{
				return this.dayOfTheWeek;
			}
			set
			{
				this.SetFieldValue<DayOfTheWeek?>(ref this.dayOfTheWeek, value);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00015688 File Offset: 0x00014688
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00015690 File Offset: 0x00014690
		public Month? Month
		{
			get
			{
				return this.month;
			}
			set
			{
				this.SetFieldValue<Month?>(ref this.month, value);
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000156A0 File Offset: 0x000146A0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.DayOfTheWeek != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "DaysOfWeek", this.DayOfTheWeek.Value);
			}
			if (this.dayOfTheWeekIndex != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "DayOfWeekIndex", this.DayOfTheWeekIndex.Value);
			}
			if (this.Month != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Month", this.Month.Value);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00015738 File Offset: 0x00014738
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "DaysOfWeek")
				{
					this.dayOfTheWeek = new DayOfTheWeek?(reader.ReadElementValue<DayOfTheWeek>());
					return true;
				}
				if (localName == "DayOfWeekIndex")
				{
					this.dayOfTheWeekIndex = new DayOfTheWeekIndex?(reader.ReadElementValue<DayOfTheWeekIndex>());
					return true;
				}
				if (localName == "Month")
				{
					this.month = new Month?(reader.ReadElementValue<Month>());
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000210 RID: 528
		private DayOfTheWeek? dayOfTheWeek;

		// Token: 0x04000211 RID: 529
		private DayOfTheWeekIndex? dayOfTheWeekIndex;

		// Token: 0x04000212 RID: 530
		private Month? month;
	}
}
