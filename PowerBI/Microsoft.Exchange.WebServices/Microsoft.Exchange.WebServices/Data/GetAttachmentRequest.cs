using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000108 RID: 264
	internal sealed class GetAttachmentRequest : MultiResponseServiceRequest<GetAttachmentResponse>, IJsonSerializable
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x0002A1B3 File Offset: 0x000291B3
		internal GetAttachmentRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002A1E0 File Offset: 0x000291E0
		internal override void Validate()
		{
			base.Validate();
			if (this.Attachments.Count > 0)
			{
				EwsUtilities.ValidateParamCollection(this.Attachments, "Attachments");
			}
			if (this.AttachmentIds.Count > 0)
			{
				EwsUtilities.ValidateParamCollection(this.AttachmentIds, "AttachmentIds");
			}
			if (this.AttachmentIds.Count == 0 && this.Attachments.Count == 0)
			{
				throw new ArgumentException(Strings.CollectionIsEmpty, "Attachments/AttachmentIds");
			}
			for (int i = 0; i < this.AdditionalProperties.Count; i++)
			{
				EwsUtilities.ValidateParam(this.AdditionalProperties[i], string.Format("AdditionalProperties[{0}]", i));
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0002A295 File Offset: 0x00029295
		internal override GetAttachmentResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetAttachmentResponse((this.Attachments.Count > 0) ? this.Attachments[responseIndex] : null);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002A2B9 File Offset: 0x000292B9
		internal override int GetExpectedResponseMessageCount()
		{
			return this.Attachments.Count + this.AttachmentIds.Count;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0002A2D2 File Offset: 0x000292D2
		internal override string GetXmlElementName()
		{
			return "GetAttachment";
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002A2D9 File Offset: 0x000292D9
		internal override string GetResponseXmlElementName()
		{
			return "GetAttachmentResponse";
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002A2E0 File Offset: 0x000292E0
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetAttachmentResponseMessage";
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002A2E8 File Offset: 0x000292E8
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.BodyType != null || this.AdditionalProperties.Count > 0)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "AttachmentShape");
				if (this.BodyType != null)
				{
					writer.WriteElementValue(XmlNamespace.Types, "BodyType", this.BodyType.Value);
				}
				if (this.AdditionalProperties.Count > 0)
				{
					PropertySet.WriteAdditionalPropertiesToXml(writer, this.AdditionalProperties);
				}
				writer.WriteEndElement();
			}
			writer.WriteStartElement(XmlNamespace.Messages, "AttachmentIds");
			foreach (Attachment attachment in this.Attachments)
			{
				this.WriteAttachmentIdXml(writer, attachment.Id);
			}
			foreach (string text in this.AttachmentIds)
			{
				this.WriteAttachmentIdXml(writer, text);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002A414 File Offset: 0x00029414
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.BodyType != null || this.AdditionalProperties.Count > 0)
			{
				JsonObject jsonObject2 = new JsonObject();
				if (this.BodyType != null)
				{
					jsonObject2.Add("BodyType", this.BodyType.Value);
				}
				if (this.AdditionalProperties.Count > 0)
				{
					PropertySet.WriteAdditionalPropertiesToJson(jsonObject2, service, this.AdditionalProperties);
				}
				jsonObject.Add("AttachmentShape", jsonObject2);
			}
			List<object> list = new List<object>();
			foreach (Attachment attachment in this.Attachments)
			{
				this.AddJsonAttachmentIdToList(list, attachment.Id);
			}
			foreach (string text in this.AttachmentIds)
			{
				this.AddJsonAttachmentIdToList(list, text);
			}
			jsonObject.Add("AttachmentIds", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0002A550 File Offset: 0x00029550
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002A553 File Offset: 0x00029553
		public List<Attachment> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0002A55B File Offset: 0x0002955B
		public List<string> AttachmentIds
		{
			get
			{
				return this.attachmentIds;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002A563 File Offset: 0x00029563
		public List<PropertyDefinitionBase> AdditionalProperties
		{
			get
			{
				return this.additionalProperties;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0002A56B File Offset: 0x0002956B
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0002A573 File Offset: 0x00029573
		public BodyType? BodyType
		{
			get
			{
				return this.bodyType;
			}
			set
			{
				this.bodyType = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0002A57C File Offset: 0x0002957C
		internal override bool EmitTimeZoneHeader
		{
			get
			{
				return this.additionalProperties.Contains(ItemSchema.MimeContent);
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002A58E File Offset: 0x0002958E
		private void WriteAttachmentIdXml(EwsServiceXmlWriter writer, string attachmentId)
		{
			writer.WriteStartElement(XmlNamespace.Types, "AttachmentId");
			writer.WriteAttributeValue("Id", attachmentId);
			writer.WriteEndElement();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002A5B0 File Offset: 0x000295B0
		private void AddJsonAttachmentIdToList(List<object> attachmentIds, string attachmentId)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Id", attachmentId);
			attachmentIds.Add(jsonObject);
		}

		// Token: 0x040008EC RID: 2284
		private List<Attachment> attachments = new List<Attachment>();

		// Token: 0x040008ED RID: 2285
		private List<string> attachmentIds = new List<string>();

		// Token: 0x040008EE RID: 2286
		private List<PropertyDefinitionBase> additionalProperties = new List<PropertyDefinitionBase>();

		// Token: 0x040008EF RID: 2287
		private BodyType? bodyType;
	}
}
