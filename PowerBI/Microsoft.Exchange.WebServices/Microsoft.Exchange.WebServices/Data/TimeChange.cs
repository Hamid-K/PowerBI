using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008D RID: 141
	internal sealed class TimeChange : ComplexProperty
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x000153B9 File Offset: 0x000143B9
		public TimeChange()
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000153C1 File Offset: 0x000143C1
		public TimeChange(TimeSpan offset)
			: this()
		{
			this.offset = new TimeSpan?(offset);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000153D5 File Offset: 0x000143D5
		public TimeChange(TimeSpan offset, Time time)
			: this(offset)
		{
			this.time = time;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000153E5 File Offset: 0x000143E5
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000153ED File Offset: 0x000143ED
		public string TimeZoneName
		{
			get
			{
				return this.timeZoneName;
			}
			set
			{
				this.SetFieldValue<string>(ref this.timeZoneName, value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000153FC File Offset: 0x000143FC
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00015404 File Offset: 0x00014404
		public TimeSpan? Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.SetFieldValue<TimeSpan?>(ref this.offset, value);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00015413 File Offset: 0x00014413
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0001541B File Offset: 0x0001441B
		public Time Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.SetFieldValue<Time>(ref this.time, value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001542A File Offset: 0x0001442A
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00015432 File Offset: 0x00014432
		public DateTime? AbsoluteDate
		{
			get
			{
				return this.absoluteDate;
			}
			set
			{
				this.SetFieldValue<DateTime?>(ref this.absoluteDate, value);
				if (this.absoluteDate != null)
				{
					this.recurrence = null;
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00015455 File Offset: 0x00014455
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001545D File Offset: 0x0001445D
		public TimeChangeRecurrence Recurrence
		{
			get
			{
				return this.recurrence;
			}
			set
			{
				this.SetFieldValue<TimeChangeRecurrence>(ref this.recurrence, value);
				if (this.recurrence != null)
				{
					this.absoluteDate = default(DateTime?);
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00015480 File Offset: 0x00014480
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "Offset")
				{
					this.offset = new TimeSpan?(EwsUtilities.XSDurationToTimeSpan(reader.ReadElementValue()));
					return true;
				}
				if (localName == "RelativeYearlyRecurrence")
				{
					this.Recurrence = new TimeChangeRecurrence();
					this.Recurrence.LoadFromXml(reader, reader.LocalName);
					return true;
				}
				if (localName == "AbsoluteDate")
				{
					this.absoluteDate = new DateTime?(new DateTime(DateTime.Parse(reader.ReadElementValue()).ToUniversalTime().Ticks, 0));
					return true;
				}
				if (localName == "Time")
				{
					this.time = new Time(DateTime.Parse(reader.ReadElementValue()));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001554E File Offset: 0x0001454E
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.timeZoneName = reader.ReadAttributeValue("TimeZoneName");
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00015561 File Offset: 0x00014561
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("TimeZoneName", this.TimeZoneName);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00015574 File Offset: 0x00014574
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.Offset != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Offset", EwsUtilities.TimeSpanToXSDuration(this.Offset.Value));
			}
			if (this.Recurrence != null)
			{
				this.Recurrence.WriteToXml(writer, "RelativeYearlyRecurrence");
			}
			if (this.AbsoluteDate != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "AbsoluteDate", EwsUtilities.DateTimeToXSDate(new DateTime(this.AbsoluteDate.Value.Ticks, 0)));
			}
			if (this.Time != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "Time", this.Time.ToXSTime());
			}
		}

		// Token: 0x0400020B RID: 523
		private string timeZoneName;

		// Token: 0x0400020C RID: 524
		private TimeSpan? offset;

		// Token: 0x0400020D RID: 525
		private Time time;

		// Token: 0x0400020E RID: 526
		private DateTime? absoluteDate;

		// Token: 0x0400020F RID: 527
		private TimeChangeRecurrence recurrence;
	}
}
