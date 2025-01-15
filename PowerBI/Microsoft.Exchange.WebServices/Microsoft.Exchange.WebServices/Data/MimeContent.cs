using System;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200007B RID: 123
	public sealed class MimeContent : ComplexProperty
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00013254 File Offset: 0x00012254
		public MimeContent()
		{
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001325C File Offset: 0x0001225C
		public MimeContent(string characterSet, byte[] content)
			: this()
		{
			this.characterSet = characterSet;
			this.content = content;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013272 File Offset: 0x00012272
		internal override void ReadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.characterSet = reader.ReadAttributeValue<string>("CharacterSet");
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013285 File Offset: 0x00012285
		internal override void ReadTextValueFromXml(EwsServiceXmlReader reader)
		{
			this.content = Convert.FromBase64String(reader.ReadValue());
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00013298 File Offset: 0x00012298
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "CharacterSet"))
					{
						if (text2 == "Value")
						{
							this.content = jsonProperty.ReadAsBase64Content(text);
						}
					}
					else
					{
						this.characterSet = jsonProperty.ReadAsString(text);
					}
				}
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00013324 File Offset: 0x00012324
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("CharacterSet", this.CharacterSet);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00013337 File Offset: 0x00012337
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.Content != null && this.Content.Length > 0)
			{
				writer.WriteBase64ElementValue(this.Content);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00013358 File Offset: 0x00012358
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ChangeKey", this.CharacterSet);
			if (this.Content != null && this.Content.Length > 0)
			{
				jsonObject.AddBase64("Value", this.Content);
			}
			return jsonObject;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000133A1 File Offset: 0x000123A1
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000133A9 File Offset: 0x000123A9
		public string CharacterSet
		{
			get
			{
				return this.characterSet;
			}
			set
			{
				this.SetFieldValue<string>(ref this.characterSet, value);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000133B8 File Offset: 0x000123B8
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000133C0 File Offset: 0x000123C0
		public byte[] Content
		{
			get
			{
				return this.content;
			}
			set
			{
				this.SetFieldValue<byte[]>(ref this.content, value);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000133D0 File Offset: 0x000123D0
		public override string ToString()
		{
			if (this.Content == null)
			{
				return string.Empty;
			}
			string text2;
			try
			{
				string text = (string.IsNullOrEmpty(this.CharacterSet) ? Encoding.UTF8.EncodingName : this.CharacterSet);
				Encoding encoding = Encoding.GetEncoding(text);
				text2 = encoding.GetString(this.Content);
			}
			catch (ArgumentException)
			{
				text2 = Convert.ToBase64String(this.Content);
			}
			return text2;
		}

		// Token: 0x040001E0 RID: 480
		private string characterSet;

		// Token: 0x040001E1 RID: 481
		private byte[] content;
	}
}
