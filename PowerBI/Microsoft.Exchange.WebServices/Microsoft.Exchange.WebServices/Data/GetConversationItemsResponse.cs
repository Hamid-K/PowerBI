using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000163 RID: 355
	public sealed class GetConversationItemsResponse : ServiceResponse
	{
		// Token: 0x06001099 RID: 4249 RVA: 0x00030EAA File Offset: 0x0002FEAA
		internal GetConversationItemsResponse(PropertySet propertySet)
		{
			this.propertySet = propertySet;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00030EB9 File Offset: 0x0002FEB9
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00030EC1 File Offset: 0x0002FEC1
		public ConversationResponse Conversation { get; set; }

		// Token: 0x0600109C RID: 4252 RVA: 0x00030ECA File Offset: 0x0002FECA
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			this.Conversation = new ConversationResponse(this.propertySet);
			reader.ReadStartElement(XmlNamespace.Messages, "Conversation");
			this.Conversation.LoadFromXml(reader, XmlNamespace.Messages, "Conversation");
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00030EFB File Offset: 0x0002FEFB
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			this.Conversation = new ConversationResponse(this.propertySet);
			this.Conversation.LoadFromJson(responseObject.ReadAsJsonObject("Conversation"), service);
		}

		// Token: 0x040009B5 RID: 2485
		private PropertySet propertySet;
	}
}
