using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F9 RID: 249
	internal sealed class DeleteAttachmentRequest : MultiResponseServiceRequest<DeleteAttachmentResponse>, IJsonSerializable
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x000290D2 File Offset: 0x000280D2
		internal DeleteAttachmentRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000290E8 File Offset: 0x000280E8
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.Attachments, "Attachments");
			for (int i = 0; i < this.Attachments.Count; i++)
			{
				EwsUtilities.ValidateParam(this.Attachments[i].Id, string.Format("Attachment[{0}].Id", i));
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00029147 File Offset: 0x00028147
		internal override DeleteAttachmentResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new DeleteAttachmentResponse(this.Attachments[responseIndex]);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002915A File Offset: 0x0002815A
		internal override int GetExpectedResponseMessageCount()
		{
			return this.Attachments.Count;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00029167 File Offset: 0x00028167
		internal override string GetXmlElementName()
		{
			return "DeleteAttachment";
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002916E File Offset: 0x0002816E
		internal override string GetResponseXmlElementName()
		{
			return "DeleteAttachmentResponse";
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00029175 File Offset: 0x00028175
		internal override string GetResponseMessageXmlElementName()
		{
			return "DeleteAttachmentResponseMessage";
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002917C File Offset: 0x0002817C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "AttachmentIds");
			foreach (Attachment attachment in this.Attachments)
			{
				writer.WriteStartElement(XmlNamespace.Types, "AttachmentId");
				writer.WriteAttributeValue("Id", attachment.Id);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00029200 File Offset: 0x00028200
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			List<object> list = new List<object>();
			foreach (Attachment attachment in this.Attachments)
			{
				JsonObject jsonObject2 = new JsonObject();
				jsonObject2.AddTypeParameter("AttachmentId");
				jsonObject2.Add("Id", attachment.Id);
				list.Add(jsonObject2);
			}
			jsonObject.Add("AttachmentIds", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00029294 File Offset: 0x00028294
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00029297 File Offset: 0x00028297
		public List<Attachment> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x040008CB RID: 2251
		private List<Attachment> attachments = new List<Attachment>();
	}
}
