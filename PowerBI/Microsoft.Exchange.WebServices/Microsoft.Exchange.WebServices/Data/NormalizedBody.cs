using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007C RID: 124
	public sealed class NormalizedBody : ComplexProperty
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00013444 File Offset: 0x00012444
		internal NormalizedBody()
		{
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001344C File Offset: 0x0001244C
		public static implicit operator string(NormalizedBody messageBody)
		{
			EwsUtilities.ValidateParam(messageBody, "messageBody");
			return messageBody.Text;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00013460 File Offset: 0x00012460
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.bodyType = reader.ReadAttributeValue<BodyType>("BodyType");
			string text = reader.ReadAttributeValue("IsTruncated");
			if (!string.IsNullOrEmpty(text))
			{
				this.isTruncated = bool.Parse(text);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001349E File Offset: 0x0001249E
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.text = reader.ReadValue();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000134AC File Offset: 0x000124AC
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "BodyType"))
					{
						if (!(text2 == "IsTruncated"))
						{
							if (text2 == "Value")
							{
								this.text = jsonProperty.ReadAsString(text);
							}
						}
						else
						{
							this.isTruncated = jsonProperty.ReadAsBool(text);
						}
					}
					else
					{
						this.bodyType = jsonProperty.ReadEnumValue<BodyType>(text);
					}
				}
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00013554 File Offset: 0x00012554
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("BodyType", this.BodyType);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001356C File Offset: 0x0001256C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				writer.WriteValue(this.Text, "NormalizedBody");
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001358C File Offset: 0x0001258C
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("BodyType", this.BodyType);
			jsonObject.Add("IsTruncated", this.IsTruncated);
			if (!string.IsNullOrEmpty(this.Text))
			{
				jsonObject.Add("Value", this.Text);
			}
			return jsonObject;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000135E5 File Offset: 0x000125E5
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x000135ED File Offset: 0x000125ED
		public BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
			internal set
			{
				this.bodyType = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000135F6 File Offset: 0x000125F6
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x000135FE File Offset: 0x000125FE
		public string Text
		{
			get
			{
				return this.text;
			}
			internal set
			{
				this.text = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00013607 File Offset: 0x00012607
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001360F File Offset: 0x0001260F
		public bool IsTruncated
		{
			get
			{
				return this.isTruncated;
			}
			internal set
			{
				this.isTruncated = value;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00013618 File Offset: 0x00012618
		public override string ToString()
		{
			if (this.Text != null)
			{
				return this.Text;
			}
			return string.Empty;
		}

		// Token: 0x040001E2 RID: 482
		private BodyType bodyType;

		// Token: 0x040001E3 RID: 483
		private string text;

		// Token: 0x040001E4 RID: 484
		private bool isTruncated;
	}
}
