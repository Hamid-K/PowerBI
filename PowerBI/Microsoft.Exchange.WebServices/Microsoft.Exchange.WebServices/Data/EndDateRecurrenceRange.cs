using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BB RID: 187
	internal sealed class EndDateRecurrenceRange : RecurrenceRange
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0001BAC0 File Offset: 0x0001AAC0
		public EndDateRecurrenceRange()
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001BAC8 File Offset: 0x0001AAC8
		public EndDateRecurrenceRange(DateTime startDate, DateTime endDate)
			: base(startDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001BAD8 File Offset: 0x0001AAD8
		internal override string XmlElementName
		{
			get
			{
				return "EndDateRecurrence";
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001BADF File Offset: 0x0001AADF
		internal override void SetupRecurrence(Recurrence recurrence)
		{
			base.SetupRecurrence(recurrence);
			recurrence.EndDate = new DateTime?(this.EndDate);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001BAF9 File Offset: 0x0001AAF9
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteElementValue(XmlNamespace.Types, "EndDate", EwsUtilities.DateTimeToXSDate(this.EndDate));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001BB19 File Offset: 0x0001AB19
		internal override void AddPropertiesToJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.AddPropertiesToJson(jsonProperty, service);
			jsonProperty.Add("EndDate", EwsUtilities.DateTimeToXSDate(this.EndDate));
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001BB3C File Offset: 0x0001AB3C
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (base.TryReadElementFromXml(reader))
			{
				return true;
			}
			string localName;
			if ((localName = reader.LocalName) != null && localName == "EndDate")
			{
				this.endDate = reader.ReadElementValueAsDateTime().Value;
				return true;
			}
			return false;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001BB84 File Offset: 0x0001AB84
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "EndDate")
				{
					this.endDate = service.ConvertStartDateToUnspecifiedDateTime(jsonProperty.ReadAsString(text)).Value;
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001BC08 File Offset: 0x0001AC08
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0001BC10 File Offset: 0x0001AC10
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.SetFieldValue<DateTime>(ref this.endDate, value);
			}
		}

		// Token: 0x040002A1 RID: 673
		private DateTime endDate;
	}
}
