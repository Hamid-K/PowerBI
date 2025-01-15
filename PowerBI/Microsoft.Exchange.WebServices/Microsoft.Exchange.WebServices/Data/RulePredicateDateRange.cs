using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000097 RID: 151
	public sealed class RulePredicateDateRange : ComplexProperty
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x000186E7 File Offset: 0x000176E7
		internal RulePredicateDateRange()
		{
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000186EF File Offset: 0x000176EF
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000186F7 File Offset: 0x000176F7
		public DateTime? Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.SetFieldValue<DateTime?>(ref this.start, value);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00018706 File Offset: 0x00017706
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001870E File Offset: 0x0001770E
		public DateTime? End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.SetFieldValue<DateTime?>(ref this.end, value);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00018720 File Offset: 0x00017720
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "StartDateTime")
				{
					this.start = reader.ReadElementValueAsDateTime();
					return true;
				}
				if (localName == "EndDateTime")
				{
					this.end = reader.ReadElementValueAsDateTime();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00018770 File Offset: 0x00017770
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "StartDateTime"))
					{
						if (text2 == "EndDateTime")
						{
							this.end = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text));
						}
					}
					else
					{
						this.start = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text));
					}
				}
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00018808 File Offset: 0x00017808
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.Start != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "StartDateTime", this.Start.Value);
			}
			if (this.End != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "EndDateTime", this.End.Value);
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00018874 File Offset: 0x00017874
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.Start != null)
			{
				jsonObject.Add("StartDateTime", service.ConvertDateTimeToUniversalDateTimeString(this.Start.Value));
			}
			if (this.End != null)
			{
				jsonObject.Add("EndDateTime", service.ConvertDateTimeToUniversalDateTimeString(this.End.Value));
			}
			return jsonObject;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000188E8 File Offset: 0x000178E8
		internal override void InternalValidate()
		{
			base.InternalValidate();
			if (this.start != null && this.end != null && this.start.Value > this.end.Value)
			{
				throw new ServiceValidationException("Start date time cannot be bigger than end date time.");
			}
		}

		// Token: 0x04000258 RID: 600
		private DateTime? start;

		// Token: 0x04000259 RID: 601
		private DateTime? end;
	}
}
