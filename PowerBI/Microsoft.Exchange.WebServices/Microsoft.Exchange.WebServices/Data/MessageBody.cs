using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007A RID: 122
	public class MessageBody : ComplexProperty
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001309C File Offset: 0x0001209C
		public MessageBody()
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000130A4 File Offset: 0x000120A4
		public MessageBody(BodyType bodyType, string text)
			: this()
		{
			this.bodyType = bodyType;
			this.text = text;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000130BA File Offset: 0x000120BA
		public MessageBody(string text)
			: this(BodyType.HTML, text)
		{
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000130C4 File Offset: 0x000120C4
		public static implicit operator MessageBody(string textBody)
		{
			return new MessageBody(BodyType.HTML, textBody);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000130CD File Offset: 0x000120CD
		public static implicit operator string(MessageBody messageBody)
		{
			EwsUtilities.ValidateParam(messageBody, "messageBody");
			return messageBody.Text;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000130E0 File Offset: 0x000120E0
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.bodyType = reader.ReadAttributeValue<BodyType>("BodyType");
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000130F3 File Offset: 0x000120F3
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.text = reader.ReadValue();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00013104 File Offset: 0x00012104
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "BodyType"))
					{
						if (text2 == "Value")
						{
							this.text = jsonProperty.ReadAsString(text);
						}
					}
					else
					{
						this.bodyType = jsonProperty.ReadEnumValue<BodyType>(text);
					}
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013190 File Offset: 0x00012190
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("BodyType", this.BodyType);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000131A8 File Offset: 0x000121A8
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				writer.WriteValue(this.Text, "Body");
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000131C8 File Offset: 0x000121C8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("BodyType", this.BodyType);
			if (!string.IsNullOrEmpty(this.Text))
			{
				jsonObject.Add("Value", this.Text);
			}
			return jsonObject;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00013210 File Offset: 0x00012210
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x00013218 File Offset: 0x00012218
		public BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
			set
			{
				this.SetFieldValue<BodyType>(ref this.bodyType, value);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00013227 File Offset: 0x00012227
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0001322F File Offset: 0x0001222F
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.SetFieldValue<string>(ref this.text, value);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001323E File Offset: 0x0001223E
		public override string ToString()
		{
			if (this.Text != null)
			{
				return this.Text;
			}
			return string.Empty;
		}

		// Token: 0x040001DE RID: 478
		private BodyType bodyType;

		// Token: 0x040001DF RID: 479
		private string text;
	}
}
