using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C4 RID: 452
	[Schema]
	public sealed class PostItemSchema : ItemSchema
	{
		// Token: 0x06001504 RID: 5380 RVA: 0x0003B09C File Offset: 0x0003A09C
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(PostItemSchema.ConversationIndex);
			base.RegisterProperty(PostItemSchema.ConversationTopic);
			base.RegisterProperty(PostItemSchema.From);
			base.RegisterProperty(PostItemSchema.InternetMessageId);
			base.RegisterProperty(PostItemSchema.IsRead);
			base.RegisterProperty(PostItemSchema.PostedTime);
			base.RegisterProperty(PostItemSchema.References);
			base.RegisterProperty(PostItemSchema.Sender);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0003B107 File Offset: 0x0003A107
		internal PostItemSchema()
		{
		}

		// Token: 0x04000C8D RID: 3213
		public static readonly PropertyDefinition ConversationIndex = EmailMessageSchema.ConversationIndex;

		// Token: 0x04000C8E RID: 3214
		public static readonly PropertyDefinition ConversationTopic = EmailMessageSchema.ConversationTopic;

		// Token: 0x04000C8F RID: 3215
		public static readonly PropertyDefinition From = EmailMessageSchema.From;

		// Token: 0x04000C90 RID: 3216
		public static readonly PropertyDefinition InternetMessageId = EmailMessageSchema.InternetMessageId;

		// Token: 0x04000C91 RID: 3217
		public static readonly PropertyDefinition IsRead = EmailMessageSchema.IsRead;

		// Token: 0x04000C92 RID: 3218
		public static readonly PropertyDefinition PostedTime = new DateTimePropertyDefinition("PostedTime", "postitem:PostedTime", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000C93 RID: 3219
		public static readonly PropertyDefinition References = EmailMessageSchema.References;

		// Token: 0x04000C94 RID: 3220
		public static readonly PropertyDefinition Sender = EmailMessageSchema.Sender;

		// Token: 0x04000C95 RID: 3221
		internal new static readonly PostItemSchema Instance = new PostItemSchema();

		// Token: 0x020001C5 RID: 453
		private static class FieldUris
		{
			// Token: 0x04000C96 RID: 3222
			public const string PostedTime = "postitem:PostedTime";
		}
	}
}
