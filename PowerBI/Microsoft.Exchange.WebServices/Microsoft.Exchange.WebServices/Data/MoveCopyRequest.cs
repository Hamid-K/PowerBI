using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000ED RID: 237
	internal abstract class MoveCopyRequest<TServiceObject, TResponse> : MultiResponseServiceRequest<TResponse>, IJsonSerializable where TServiceObject : ServiceObject where TResponse : ServiceResponse
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x000286F4 File Offset: 0x000276F4
		internal override void Validate()
		{
			EwsUtilities.ValidateParam(this.DestinationFolderId, "DestinationFolderId");
			this.DestinationFolderId.Validate(base.Service.RequestedServerVersion);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002871C File Offset: 0x0002771C
		internal MoveCopyRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C15 RID: 3093
		internal abstract void WriteIdsToXml(EwsServiceXmlWriter writer);

		// Token: 0x06000C16 RID: 3094 RVA: 0x00028726 File Offset: 0x00027726
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "ToFolderId");
			this.DestinationFolderId.WriteToXml(writer);
			writer.WriteEndElement();
			this.WriteIdsToXml(writer);
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002874D File Offset: 0x0002774D
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00028755 File Offset: 0x00027755
		public FolderId DestinationFolderId
		{
			get
			{
				return this.destinationFolderId;
			}
			set
			{
				this.destinationFolderId = value;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00028760 File Offset: 0x00027760
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			JsonObject jsonObject2 = new JsonObject();
			jsonObject2.Add("BaseFolderId", this.DestinationFolderId.InternalToJson(service));
			jsonObject.Add("ToFolderId", jsonObject2);
			this.AddIdsToJson(jsonObject, service);
			return jsonObject;
		}

		// Token: 0x06000C1A RID: 3098
		internal abstract void AddIdsToJson(JsonObject jsonObject, ExchangeService service);

		// Token: 0x040008C0 RID: 2240
		private FolderId destinationFolderId;
	}
}
