using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200014F RID: 335
	public sealed class CreateAttachmentResponse : ServiceResponse
	{
		// Token: 0x0600103E RID: 4158 RVA: 0x0002F97F File Offset: 0x0002E97F
		internal CreateAttachmentResponse(Attachment attachment)
		{
			EwsUtilities.Assert(attachment != null, "CreateAttachmentResponse.ctor", "attachment is null");
			this.attachment = attachment;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002F9A4 File Offset: 0x0002E9A4
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.ReadStartElement(XmlNamespace.Messages, "Attachments");
			reader.Read(1);
			this.attachment.LoadFromXml(reader, reader.LocalName);
			reader.ReadEndElement(XmlNamespace.Messages, "Attachments");
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002F9E0 File Offset: 0x0002E9E0
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			object[] array = responseObject.ReadAsArray("Attachments");
			if (array != null && array.Length > 0)
			{
				this.attachment.LoadFromJson(array[0] as JsonObject, service);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0002FA16 File Offset: 0x0002EA16
		internal Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x04000994 RID: 2452
		private Attachment attachment;
	}
}
