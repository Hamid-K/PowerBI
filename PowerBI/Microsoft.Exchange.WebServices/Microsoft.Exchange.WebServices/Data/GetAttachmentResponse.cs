using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015E RID: 350
	public sealed class GetAttachmentResponse : ServiceResponse
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x000308EB File Offset: 0x0002F8EB
		internal GetAttachmentResponse(Attachment attachment)
		{
			this.attachment = attachment;
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x000308FC File Offset: 0x0002F8FC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "Attachments");
			if (!reader.IsEmptyElement)
			{
				reader.Read(1);
				if (this.attachment == null)
				{
					if (string.Equals(reader.LocalName, "FileAttachment", 5))
					{
						this.attachment = new FileAttachment(reader.Service);
					}
					else if (string.Equals(reader.LocalName, "ItemAttachment", 5))
					{
						this.attachment = new ItemAttachment(reader.Service);
					}
				}
				if (this.attachment != null)
				{
					this.attachment.LoadFromXml(reader, reader.LocalName);
				}
				reader.ReadEndElement(XmlNamespace.Messages, "Attachments");
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x000309A4 File Offset: 0x0002F9A4
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			object[] array;
			if (responseObject.ContainsKey("Attachments") && (array = responseObject.ReadAsArray("Attachments")).Length > 0)
			{
				JsonObject jsonObject = array[0] as JsonObject;
				if (this.attachment == null && jsonObject != null)
				{
					if (jsonObject.ContainsKey("FileAttachment"))
					{
						this.attachment = new FileAttachment(service);
					}
					else if (jsonObject.ContainsKey("ItemAttachment"))
					{
						this.attachment = new ItemAttachment(service);
					}
				}
				if (this.attachment != null)
				{
					this.attachment.LoadFromJson(jsonObject, service);
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00030A2D File Offset: 0x0002FA2D
		public Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x040009A8 RID: 2472
		private Attachment attachment;
	}
}
