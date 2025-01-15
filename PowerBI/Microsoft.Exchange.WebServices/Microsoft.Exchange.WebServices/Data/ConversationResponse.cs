using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200004B RID: 75
	public sealed class ConversationResponse : ComplexProperty
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000C973 File Offset: 0x0000B973
		internal ConversationResponse(PropertySet propertySet)
		{
			this.propertySet = propertySet;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C982 File Offset: 0x0000B982
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000C98A File Offset: 0x0000B98A
		public ConversationId ConversationId { get; internal set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C993 File Offset: 0x0000B993
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000C99B File Offset: 0x0000B99B
		public string SyncState { get; internal set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C9A4 File Offset: 0x0000B9A4
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000C9AC File Offset: 0x0000B9AC
		public ConversationNodeCollection ConversationNodes { get; internal set; }

		// Token: 0x0600035B RID: 859 RVA: 0x0000C9B8 File Offset: 0x0000B9B8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "ConversationId")
				{
					this.ConversationId = new ConversationId();
					this.ConversationId.LoadFromXml(reader, "ConversationId");
					return true;
				}
				if (localName == "SyncState")
				{
					this.SyncState = reader.ReadElementValue();
					return true;
				}
				if (localName == "ConversationNodes")
				{
					this.ConversationNodes = new ConversationNodeCollection(this.propertySet);
					this.ConversationNodes.LoadFromXml(reader, "ConversationNodes");
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CA4C File Offset: 0x0000BA4C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			this.ConversationId = new ConversationId();
			this.ConversationId.LoadFromJson(jsonProperty.ReadAsJsonObject("ConversationId"), service);
			if (jsonProperty.ContainsKey("SyncState"))
			{
				this.SyncState = jsonProperty.ReadAsString("SyncState");
			}
			this.ConversationNodes = new ConversationNodeCollection(this.propertySet);
			((IJsonCollectionDeserializer)this.ConversationNodes).CreateFromJsonCollection(jsonProperty.ReadAsArray("ConversationNodes"), service);
		}

		// Token: 0x0400016C RID: 364
		private PropertySet propertySet;
	}
}
