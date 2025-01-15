using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000BA RID: 186
	internal abstract class RecurrenceRange : ComplexProperty
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0001B931 File Offset: 0x0001A931
		internal RecurrenceRange()
		{
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001B939 File Offset: 0x0001A939
		internal RecurrenceRange(DateTime startDate)
			: this()
		{
			this.startDate = startDate;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001B948 File Offset: 0x0001A948
		internal override void Changed()
		{
			if (this.Recurrence != null)
			{
				this.Recurrence.Changed();
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B95D File Offset: 0x0001A95D
		internal virtual void SetupRecurrence(Recurrence recurrence)
		{
			recurrence.StartDate = this.StartDate;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001B96B File Offset: 0x0001A96B
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "StartDate", EwsUtilities.DateTimeToXSDate(this.StartDate));
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001B984 File Offset: 0x0001A984
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.AddPropertiesToJson(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B9A0 File Offset: 0x0001A9A0
		internal virtual void AddPropertiesToJson(JsonObject jsonProperty, ExchangeService service)
		{
			jsonProperty.AddTypeParameter(this.XmlElementName);
			jsonProperty.Add("StartDate", EwsUtilities.DateTimeToXSDate(this.StartDate));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B9C4 File Offset: 0x0001A9C4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) == null || !(localName == "StartDate"))
			{
				return false;
			}
			DateTime? dateTime = reader.ReadElementValueAsUnspecifiedDate();
			if (dateTime != null)
			{
				this.startDate = dateTime.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001BA0C File Offset: 0x0001AA0C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			base.LoadFromJson(jsonProperty, service);
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null && text2 == "StartDate")
				{
					DateTime? dateTime = service.ConvertStartDateToUnspecifiedDateTime(jsonProperty.ReadAsString(text));
					if (dateTime != null)
					{
						this.startDate = dateTime.Value;
					}
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000839 RID: 2105
		internal abstract string XmlElementName { get; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001BA98 File Offset: 0x0001AA98
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0001BAA0 File Offset: 0x0001AAA0
		internal Recurrence Recurrence
		{
			get
			{
				return this.recurrence;
			}
			set
			{
				this.recurrence = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001BAA9 File Offset: 0x0001AAA9
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001BAB1 File Offset: 0x0001AAB1
		internal DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.SetFieldValue<DateTime>(ref this.startDate, value);
			}
		}

		// Token: 0x0400029F RID: 671
		private DateTime startDate;

		// Token: 0x040002A0 RID: 672
		private Recurrence recurrence;
	}
}
