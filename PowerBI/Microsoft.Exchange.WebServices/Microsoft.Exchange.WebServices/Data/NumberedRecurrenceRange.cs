using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BD RID: 189
	internal sealed class NumberedRecurrenceRange : RecurrenceRange
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0001BC46 File Offset: 0x0001AC46
		public NumberedRecurrenceRange()
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001BC4E File Offset: 0x0001AC4E
		public NumberedRecurrenceRange(DateTime startDate, int? numberOfOccurrences)
			: base(startDate)
		{
			this.numberOfOccurrences = numberOfOccurrences;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001BC5E File Offset: 0x0001AC5E
		internal override string XmlElementName
		{
			get
			{
				return "NumberedRecurrence";
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001BC65 File Offset: 0x0001AC65
		internal override void SetupRecurrence(Recurrence recurrence)
		{
			base.SetupRecurrence(recurrence);
			recurrence.NumberOfOccurrences = this.NumberOfOccurrences;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001BC7C File Offset: 0x0001AC7C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			if (this.NumberOfOccurrences != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "NumberOfOccurrences", this.NumberOfOccurrences);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001BCB7 File Offset: 0x0001ACB7
		internal override void AddPropertiesToJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.AddPropertiesToJson(jsonProperty, service);
			jsonProperty.Add("NumberOfOccurrences", this.NumberOfOccurrences);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001BCD8 File Offset: 0x0001ACD8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (base.TryReadElementFromXml(reader))
			{
				return true;
			}
			string localName;
			if ((localName = reader.LocalName) != null && localName == "NumberOfOccurrences")
			{
				this.numberOfOccurrences = new int?(reader.ReadElementValue<int>());
				return true;
			}
			return false;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001BD1C File Offset: 0x0001AD1C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "NumberOfOccurrences")
				{
					this.numberOfOccurrences = new int?(jsonProperty.ReadAsInt(text));
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001BD94 File Offset: 0x0001AD94
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001BD9C File Offset: 0x0001AD9C
		public int? NumberOfOccurrences
		{
			get
			{
				return this.numberOfOccurrences;
			}
			set
			{
				this.SetFieldValue<int?>(ref this.numberOfOccurrences, value);
			}
		}

		// Token: 0x040002A2 RID: 674
		private int? numberOfOccurrences;
	}
}
