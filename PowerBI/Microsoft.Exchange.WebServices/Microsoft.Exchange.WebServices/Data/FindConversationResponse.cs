using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200015B RID: 347
	internal sealed class FindConversationResponse : ServiceResponse
	{
		// Token: 0x06001066 RID: 4198 RVA: 0x0002FEA1 File Offset: 0x0002EEA1
		internal FindConversationResponse()
		{
			this.Results = new FindConversationResults();
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0002FEB4 File Offset: 0x0002EEB4
		internal Collection<Conversation> Conversations
		{
			get
			{
				return this.Results.Conversations;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x0002FEC1 File Offset: 0x0002EEC1
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x0002FEC9 File Offset: 0x0002EEC9
		internal FindConversationResults Results { get; private set; }

		// Token: 0x0600106A RID: 4202 RVA: 0x0002FED4 File Offset: 0x0002EED4
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(this.Results.Conversations != null, "FindConversationResponse.ReadElementsFromXml", "conversations is null.");
			EwsUtilities.Assert(this.Results.HighlightTerms != null, "FindConversationResponse.ReadElementsFromXml", "highlightTerms is null.");
			reader.ReadStartElement(XmlNamespace.Messages, "Conversations");
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						Conversation conversation = EwsUtilities.CreateEwsObjectFromXmlElementName<Conversation>(reader.Service, reader.LocalName);
						if (conversation == null)
						{
							reader.SkipCurrentElement();
						}
						else
						{
							conversation.LoadFromXml(reader, true, null, false);
							this.Results.Conversations.Add(conversation);
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "Conversations"));
			}
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "HighlightTerms") && !reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1)
					{
						HighlightTerm highlightTerm = new HighlightTerm();
						highlightTerm.LoadFromXml(reader, XmlNamespace.Types, "Term");
						this.Results.HighlightTerms.Add(highlightTerm);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Messages, "HighlightTerms"));
			}
			if (reader.IsStartElement(XmlNamespace.Messages, "TotalConversationsInView") && !reader.IsEmptyElement)
			{
				this.Results.TotalCount = new int?(reader.ReadElementValue<int>());
				reader.Read();
			}
			if (reader.IsStartElement(XmlNamespace.Messages, "IndexedOffset") && !reader.IsEmptyElement)
			{
				this.Results.IndexedOffset = new int?(reader.ReadElementValue<int>());
				reader.Read();
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003004C File Offset: 0x0002F04C
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			EwsUtilities.Assert(this.Results.Conversations != null, "FindConversationResponse.ReadElementsFromXml", "conversations is null.");
			EwsUtilities.Assert(this.Results.HighlightTerms != null, "FindConversationResponse.ReadElementsFromXml", "highlightTerms is null.");
			foreach (object obj in responseObject.ReadAsArray("Conversations"))
			{
				JsonObject jsonObject = obj as JsonObject;
				Conversation conversation = EwsUtilities.CreateEwsObjectFromXmlElementName<Conversation>(service, "Conversation");
				if (conversation != null)
				{
					conversation.LoadFromJson(jsonObject, service, true, null, false);
					this.Conversations.Add(conversation);
				}
			}
			object[] array2 = responseObject.ReadAsArray("HighlightTerms");
			if (array2 != null)
			{
				foreach (object obj2 in array2)
				{
					JsonObject jsonObject2 = obj2 as JsonObject;
					HighlightTerm highlightTerm = new HighlightTerm();
					highlightTerm.LoadFromJson(jsonObject2, service);
					this.Results.HighlightTerms.Add(highlightTerm);
				}
			}
			if (responseObject.ContainsKey("TotalConversationsInView"))
			{
				this.Results.TotalCount = new int?(responseObject.ReadAsInt("TotalConversationsInView"));
			}
			if (responseObject.ContainsKey("IndexedOffset"))
			{
				this.Results.IndexedOffset = new int?(responseObject.ReadAsInt("IndexedOffset"));
			}
		}
	}
}
