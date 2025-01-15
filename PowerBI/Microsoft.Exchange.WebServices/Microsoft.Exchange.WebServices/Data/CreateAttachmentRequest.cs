using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F2 RID: 242
	internal sealed class CreateAttachmentRequest : MultiResponseServiceRequest<CreateAttachmentResponse>, IJsonSerializable
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0002897C File Offset: 0x0002797C
		internal CreateAttachmentRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00028991 File Offset: 0x00027991
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.ParentItemId, "ParentItemId");
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000289A9 File Offset: 0x000279A9
		internal override CreateAttachmentResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new CreateAttachmentResponse(this.Attachments[responseIndex]);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000289BC File Offset: 0x000279BC
		internal override int GetExpectedResponseMessageCount()
		{
			return this.Attachments.Count;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000289C9 File Offset: 0x000279C9
		internal override string GetXmlElementName()
		{
			return "CreateAttachment";
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000289D0 File Offset: 0x000279D0
		internal override string GetResponseXmlElementName()
		{
			return "CreateAttachmentResponse";
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000289D7 File Offset: 0x000279D7
		internal override string GetResponseMessageXmlElementName()
		{
			return "CreateAttachmentResponseMessage";
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000289E0 File Offset: 0x000279E0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "ParentItemId");
			writer.WriteAttributeValue("Id", this.ParentItemId);
			writer.WriteEndElement();
			writer.WriteStartElement(XmlNamespace.Messages, "Attachments");
			foreach (Attachment attachment in this.Attachments)
			{
				attachment.WriteToXml(writer, attachment.GetXmlElementName());
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00028A70 File Offset: 0x00027A70
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ParentItemId", new ItemId(this.ParentItemId).InternalToJson(service));
			List<object> list = new List<object>();
			foreach (Attachment attachment in this.Attachments)
			{
				list.Add(attachment.InternalToJson(service));
			}
			jsonObject.Add("Attachments", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00028B04 File Offset: 0x00027B04
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00028B08 File Offset: 0x00027B08
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				foreach (ItemAttachment itemAttachment in Enumerable.OfType<ItemAttachment>(this.attachments))
				{
					if (itemAttachment.Item != null && itemAttachment.Item.GetIsTimeZoneHeaderRequired(false))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00028B70 File Offset: 0x00027B70
		public List<Attachment> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x00028B78 File Offset: 0x00027B78
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00028B80 File Offset: 0x00027B80
		public string ParentItemId
		{
			get
			{
				return this.parentItemId;
			}
			set
			{
				this.parentItemId = value;
			}
		}

		// Token: 0x040008C4 RID: 2244
		private string parentItemId;

		// Token: 0x040008C5 RID: 2245
		private List<Attachment> attachments = new List<Attachment>();
	}
}
