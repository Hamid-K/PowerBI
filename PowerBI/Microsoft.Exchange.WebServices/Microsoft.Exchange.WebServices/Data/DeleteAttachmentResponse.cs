using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000156 RID: 342
	public sealed class DeleteAttachmentResponse : ServiceResponse
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x0002FCA1 File Offset: 0x0002ECA1
		internal DeleteAttachmentResponse(Attachment attachment)
		{
			EwsUtilities.Assert(attachment != null, "DeleteAttachmentResponse.ctor", "attachment is null");
			this.attachment = attachment;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0002FCC8 File Offset: 0x0002ECC8
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "RootItemId");
			string text = reader.ReadAttributeValue("RootItemChangeKey");
			if (!string.IsNullOrEmpty(text) && this.attachment.Owner != null)
			{
				this.attachment.Owner.RootItemId.ChangeKey = text;
			}
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "RootItemId");
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0002FD2C File Offset: 0x0002ED2C
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("RootItemId"))
			{
				JsonObject jsonObject = responseObject.ReadAsJsonObject("RootItemId");
				string text;
				if (jsonObject.ContainsKey("RootItemChangeKey") && !string.IsNullOrEmpty(text = jsonObject.ReadAsString("RootItemChangeKey")) && this.attachment.Owner != null)
				{
					this.attachment.Owner.RootItemId.ChangeKey = text;
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0002FD9E File Offset: 0x0002ED9E
		internal Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x0400099D RID: 2461
		private Attachment attachment;
	}
}
