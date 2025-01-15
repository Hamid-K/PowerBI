using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006B RID: 107
	public sealed class InternetMessageHeader : ComplexProperty
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x00011DEF File Offset: 0x00010DEF
		internal InternetMessageHeader()
		{
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011DF7 File Offset: 0x00010DF7
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.name = reader.ReadAttributeValue("HeaderName");
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011E0A File Offset: 0x00010E0A
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.value = reader.ReadValue();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011E18 File Offset: 0x00010E18
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "HeaderName"))
					{
						if (text2 == "Value")
						{
							this.value = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.name = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00011EA4 File Offset: 0x00010EA4
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("HeaderName", this.Name);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00011EB7 File Offset: 0x00010EB7
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteValue(this.Value, this.Name);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00011ECC File Offset: 0x00010ECC
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("HeaderName", this.Name);
			jsonObject.Add("Value", this.Value);
			return jsonObject;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00011F02 File Offset: 0x00010F02
		public override string ToString()
		{
			return string.Format("{0}={1}", this.Name, this.Value);
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00011F1A File Offset: 0x00010F1A
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00011F22 File Offset: 0x00010F22
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.SetFieldValue<string>(ref this.name, value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00011F31 File Offset: 0x00010F31
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x00011F39 File Offset: 0x00010F39
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.SetFieldValue<string>(ref this.value, value);
			}
		}

		// Token: 0x040001B3 RID: 435
		private string name;

		// Token: 0x040001B4 RID: 436
		private string value;
	}
}
