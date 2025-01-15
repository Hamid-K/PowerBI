using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000070 RID: 112
	public sealed class UniqueBody : ComplexProperty
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00012192 File Offset: 0x00011192
		internal UniqueBody()
		{
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001219A File Offset: 0x0001119A
		public static implicit operator string(UniqueBody messageBody)
		{
			EwsUtilities.ValidateParam(messageBody, "messageBody");
			return messageBody.Text;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000121B0 File Offset: 0x000111B0
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.bodyType = reader.ReadAttributeValue<BodyType>("BodyType");
			string text = reader.ReadAttributeValue("IsTruncated");
			if (!string.IsNullOrEmpty(text))
			{
				this.isTruncated = bool.Parse(text);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000121EE File Offset: 0x000111EE
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.text = reader.ReadValue();
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000121FC File Offset: 0x000111FC
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

		// Token: 0x06000520 RID: 1312 RVA: 0x000122A4 File Offset: 0x000112A4
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("BodyType", this.BodyType);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000122BC File Offset: 0x000112BC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				writer.WriteValue(this.Text, "UniqueBody");
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000122DC File Offset: 0x000112DC
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00012335 File Offset: 0x00011335
		public BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001233D File Offset: 0x0001133D
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00012345 File Offset: 0x00011345
		public bool IsTruncated
		{
			get
			{
				return this.isTruncated;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001234D File Offset: 0x0001134D
		public override string ToString()
		{
			if (this.Text != null)
			{
				return this.Text;
			}
			return string.Empty;
		}

		// Token: 0x040001B6 RID: 438
		private BodyType bodyType;

		// Token: 0x040001B7 RID: 439
		private string text;

		// Token: 0x040001B8 RID: 440
		private bool isTruncated;
	}
}
