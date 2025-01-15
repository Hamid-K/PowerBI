using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000049 RID: 73
	[EditorBrowsable(1)]
	public sealed class ConversationNodeCollection : ComplexPropertyCollection<ConversationNode>, IJsonCollectionDeserializer
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000C81E File Offset: 0x0000B81E
		internal ConversationNodeCollection(PropertySet propertySet)
		{
			this.propertySet = propertySet;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C82D File Offset: 0x0000B82D
		internal override ConversationNode CreateComplexProperty(string xmlElementName)
		{
			return new ConversationNode(this.propertySet);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C83A File Offset: 0x0000B83A
		internal override ConversationNode CreateDefaultComplexProperty()
		{
			return new ConversationNode(this.propertySet);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C847 File Offset: 0x0000B847
		internal override string GetCollectionItemXmlElementName(ConversationNode complexProperty)
		{
			return complexProperty.GetXmlElementName();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C850 File Offset: 0x0000B850
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				JsonObject jsonObject = obj as JsonObject;
				if (jsonObject != null)
				{
					ConversationNode conversationNode = new ConversationNode(this.propertySet);
					conversationNode.LoadFromJson(jsonObject, service);
					base.InternalAdd(conversationNode);
				}
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C89B File Offset: 0x0000B89B
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000169 RID: 361
		private PropertySet propertySet;
	}
}
