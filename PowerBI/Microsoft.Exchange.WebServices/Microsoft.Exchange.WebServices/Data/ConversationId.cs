using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006F RID: 111
	public class ConversationId : ServiceId
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001213A File Offset: 0x0001113A
		internal ConversationId()
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00012142 File Offset: 0x00011142
		public static implicit operator ConversationId(string uniqueId)
		{
			return new ConversationId(uniqueId);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001214A File Offset: 0x0001114A
		public static implicit operator string(ConversationId conversationId)
		{
			if (conversationId == null)
			{
				throw new ArgumentNullException("conversationId");
			}
			if (string.IsNullOrEmpty(conversationId.UniqueId))
			{
				return string.Empty;
			}
			return conversationId.UniqueId;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00012173 File Offset: 0x00011173
		internal override string GetXmlElementName()
		{
			return "ConversationId";
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001217A File Offset: 0x0001117A
		internal override string GetJsonTypeName()
		{
			return "ItemId";
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012181 File Offset: 0x00011181
		public ConversationId(string uniqueId)
			: base(uniqueId)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001218A File Offset: 0x0001118A
		public override string ToString()
		{
			return base.UniqueId;
		}
	}
}
