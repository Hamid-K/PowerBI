using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000E9 RID: 233
	internal class ArchiveItemRequest : MultiResponseServiceRequest<ArchiveItemResponse>, IJsonSerializable
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002827C File Offset: 0x0002727C
		internal ArchiveItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00028291 File Offset: 0x00027291
		internal override void Validate()
		{
			EwsUtilities.ValidateParam(this.sourceFolderId, "SourceFolderId");
			this.sourceFolderId.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000282B9 File Offset: 0x000272B9
		internal override ArchiveItemResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ArchiveItemResponse();
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x000282C0 File Offset: 0x000272C0
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x000282C8 File Offset: 0x000272C8
		public FolderId SourceFolderId
		{
			get
			{
				return this.sourceFolderId;
			}
			set
			{
				this.sourceFolderId = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000282D1 File Offset: 0x000272D1
		internal ItemIdWrapperList Ids
		{
			get
			{
				return this.ids;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000282D9 File Offset: 0x000272D9
		internal override int GetExpectedResponseMessageCount()
		{
			return this.ids.Count;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000282E6 File Offset: 0x000272E6
		internal override string GetXmlElementName()
		{
			return "ArchiveItem";
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000282ED File Offset: 0x000272ED
		internal override string GetResponseXmlElementName()
		{
			return "ArchiveItemResponse";
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000282F4 File Offset: 0x000272F4
		internal override string GetResponseMessageXmlElementName()
		{
			return "ArchiveItemResponseMessage";
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000282FB File Offset: 0x000272FB
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000282FE File Offset: 0x000272FE
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "ArchiveSourceFolderId");
			this.SourceFolderId.WriteToXml(writer);
			writer.WriteEndElement();
			this.WriteIdsToXml(writer);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00028325 File Offset: 0x00027325
		internal void WriteIdsToXml(EwsServiceXmlWriter writer)
		{
			this.Ids.WriteToXml(writer, XmlNamespace.Messages, "ItemIds");
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002833C File Offset: 0x0002733C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ArchiveSourceFolderId", this.SourceFolderId.InternalToJson(service));
			this.AddIdsToJson(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002836F File Offset: 0x0002736F
		internal void AddIdsToJson(JsonObject jsonObject, ExchangeService service)
		{
			jsonObject.Add("ItemIds", this.Ids.InternalToJson(service));
		}

		// Token: 0x040008B4 RID: 2228
		private FolderId sourceFolderId;

		// Token: 0x040008B5 RID: 2229
		private ItemIdWrapperList ids = new ItemIdWrapperList();
	}
}
