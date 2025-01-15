using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010E RID: 270
	internal sealed class GetConversationItemsRequest : MultiResponseServiceRequest<GetConversationItemsResponse>, IJsonSerializable
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0002AB74 File Offset: 0x00029B74
		internal GetConversationItemsRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0002AB7E File Offset: 0x00029B7E
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x0002AB86 File Offset: 0x00029B86
		internal List<ConversationRequest> Conversations { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002AB8F File Offset: 0x00029B8F
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x0002AB97 File Offset: 0x00029B97
		internal PropertySet ItemProperties { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002ABA0 File Offset: 0x00029BA0
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0002ABA8 File Offset: 0x00029BA8
		internal FolderIdCollection FoldersToIgnore { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002ABB1 File Offset: 0x00029BB1
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0002ABB9 File Offset: 0x00029BB9
		internal int? MaxItemsToReturn { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0002ABC2 File Offset: 0x00029BC2
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x0002ABCA File Offset: 0x00029BCA
		internal ConversationSortOrder? SortOrder { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002ABD3 File Offset: 0x00029BD3
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0002ABDB File Offset: 0x00029BDB
		internal MailboxSearchLocation? MailboxScope { get; set; }

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002ABE4 File Offset: 0x00029BE4
		internal override void Validate()
		{
			base.Validate();
			if (this.MailboxScope != null && base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
			{
				throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "MailboxScope", ExchangeVersion.Exchange2013));
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002AC35 File Offset: 0x00029C35
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002AC5C File Offset: 0x00029C5C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.ItemProperties.WriteToXml(writer, ServiceObjectType.Item);
			this.FoldersToIgnore.WriteToXml(writer, XmlNamespace.Messages, "FoldersToIgnore");
			if (this.MaxItemsToReturn != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "MaxItemsToReturn", this.MaxItemsToReturn.Value);
			}
			if (this.SortOrder != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "SortOrder", this.SortOrder.Value);
			}
			if (this.MailboxScope != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "MailboxScope", this.MailboxScope.Value);
			}
			writer.WriteStartElement(XmlNamespace.Messages, "Conversations");
			this.Conversations.ForEach(delegate(ConversationRequest conversation)
			{
				conversation.WriteToXml(writer, "Conversation");
			});
			writer.WriteEndElement();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002AD94 File Offset: 0x00029D94
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.ItemProperties.WriteGetShapeToJson(jsonObject, service, ServiceObjectType.Item);
			if (this.FoldersToIgnore.Count > 0)
			{
				jsonObject.Add("FoldersToIgnore", this.FoldersToIgnore.InternalToJson(service));
			}
			if (this.MaxItemsToReturn != null)
			{
				jsonObject.Add("MaxItemsToReturn", this.MaxItemsToReturn.Value);
			}
			if (this.SortOrder != null)
			{
				jsonObject.Add("SortOrder", this.SortOrder.Value);
			}
			if (this.MailboxScope != null)
			{
				jsonObject.Add("MailboxScope", this.MailboxScope.Value);
			}
			List<object> jsonPropertyCollection = new List<object>();
			this.Conversations.ForEach(delegate(ConversationRequest conversation)
			{
				jsonPropertyCollection.Add(conversation.InternalToJson(service));
			});
			jsonObject.Add("Conversations", jsonPropertyCollection.ToArray());
			return jsonObject;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002AEB3 File Offset: 0x00029EB3
		internal override GetConversationItemsResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetConversationItemsResponse(this.ItemProperties);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002AEC0 File Offset: 0x00029EC0
		internal override string GetXmlElementName()
		{
			return "GetConversationItems";
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002AEC7 File Offset: 0x00029EC7
		internal override string GetResponseXmlElementName()
		{
			return "GetConversationItemsResponse";
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002AECE File Offset: 0x00029ECE
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetConversationItemsResponseMessage";
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002AED5 File Offset: 0x00029ED5
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002AED8 File Offset: 0x00029ED8
		internal override int GetExpectedResponseMessageCount()
		{
			return this.Conversations.Count;
		}
	}
}
