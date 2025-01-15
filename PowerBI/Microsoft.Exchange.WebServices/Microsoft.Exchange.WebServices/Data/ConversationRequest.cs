using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200004A RID: 74
	public sealed class ConversationRequest : ComplexProperty, ISelfValidate, IJsonSerializable
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000C8A2 File Offset: 0x0000B8A2
		public ConversationRequest()
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C8AA File Offset: 0x0000B8AA
		public ConversationRequest(ConversationId conversationId, string syncState)
		{
			this.ConversationId = conversationId;
			this.SyncState = syncState;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000C8C0 File Offset: 0x0000B8C0
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000C8C8 File Offset: 0x0000B8C8
		public ConversationId ConversationId { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C8D1 File Offset: 0x0000B8D1
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000C8D9 File Offset: 0x0000B8D9
		public string SyncState { get; set; }

		// Token: 0x06000351 RID: 849 RVA: 0x0000C8E2 File Offset: 0x0000B8E2
		internal override void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			this.ConversationId.WriteToXml(writer);
			if (this.SyncState != null)
			{
				writer.WriteElementValue(XmlNamespace.Types, "SyncState", this.SyncState);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C918 File Offset: 0x0000B918
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("ConversationId", this.ConversationId.InternalToJson(service));
			if (!string.IsNullOrEmpty(this.SyncState))
			{
				jsonObject.Add("SyncState", this.SyncState);
			}
			return jsonObject;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C961 File Offset: 0x0000B961
		internal override void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.ConversationId, "ConversationId");
		}
	}
}
