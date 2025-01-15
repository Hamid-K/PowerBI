using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B9 RID: 441
	[Schema]
	public class EmailMessageSchema : ItemSchema
	{
		// Token: 0x060014DF RID: 5343 RVA: 0x0003A288 File Offset: 0x00039288
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(EmailMessageSchema.Sender);
			base.RegisterProperty(EmailMessageSchema.ToRecipients);
			base.RegisterProperty(EmailMessageSchema.CcRecipients);
			base.RegisterProperty(EmailMessageSchema.BccRecipients);
			base.RegisterProperty(EmailMessageSchema.IsReadReceiptRequested);
			base.RegisterProperty(EmailMessageSchema.IsDeliveryReceiptRequested);
			base.RegisterProperty(EmailMessageSchema.ConversationIndex);
			base.RegisterProperty(EmailMessageSchema.ConversationTopic);
			base.RegisterProperty(EmailMessageSchema.From);
			base.RegisterProperty(EmailMessageSchema.InternetMessageId);
			base.RegisterProperty(EmailMessageSchema.IsRead);
			base.RegisterProperty(EmailMessageSchema.IsResponseRequested);
			base.RegisterProperty(EmailMessageSchema.References);
			base.RegisterProperty(EmailMessageSchema.ReplyTo);
			base.RegisterProperty(EmailMessageSchema.ReceivedBy);
			base.RegisterProperty(EmailMessageSchema.ReceivedRepresenting);
			base.RegisterProperty(EmailMessageSchema.ApprovalRequestData);
			base.RegisterProperty(EmailMessageSchema.VotingInformation);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0003A361 File Offset: 0x00039361
		internal EmailMessageSchema()
		{
		}

		// Token: 0x04000BE9 RID: 3049
		public static readonly PropertyDefinition ToRecipients = new ComplexPropertyDefinition<EmailAddressCollection>("ToRecipients", "message:ToRecipients", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new EmailAddressCollection());

		// Token: 0x04000BEA RID: 3050
		public static readonly PropertyDefinition BccRecipients = new ComplexPropertyDefinition<EmailAddressCollection>("BccRecipients", "message:BccRecipients", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new EmailAddressCollection());

		// Token: 0x04000BEB RID: 3051
		public static readonly PropertyDefinition CcRecipients = new ComplexPropertyDefinition<EmailAddressCollection>("CcRecipients", "message:CcRecipients", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new EmailAddressCollection());

		// Token: 0x04000BEC RID: 3052
		public static readonly PropertyDefinition ConversationIndex = new ByteArrayPropertyDefinition("ConversationIndex", "message:ConversationIndex", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BED RID: 3053
		public static readonly PropertyDefinition ConversationTopic = new StringPropertyDefinition("ConversationTopic", "message:ConversationTopic", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BEE RID: 3054
		public static readonly PropertyDefinition From = new ContainedPropertyDefinition<EmailAddress>("From", "message:From", "Mailbox", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new EmailAddress());

		// Token: 0x04000BEF RID: 3055
		public static readonly PropertyDefinition IsDeliveryReceiptRequested = new BoolPropertyDefinition("IsDeliveryReceiptRequested", "message:IsDeliveryReceiptRequested", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BF0 RID: 3056
		public static readonly PropertyDefinition IsRead = new BoolPropertyDefinition("IsRead", "message:IsRead", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BF1 RID: 3057
		public static readonly PropertyDefinition IsReadReceiptRequested = new BoolPropertyDefinition("IsReadReceiptRequested", "message:IsReadReceiptRequested", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BF2 RID: 3058
		public static readonly PropertyDefinition IsResponseRequested = new BoolPropertyDefinition("IsResponseRequested", "message:IsResponseRequested", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, true);

		// Token: 0x04000BF3 RID: 3059
		public static readonly PropertyDefinition InternetMessageId = new StringPropertyDefinition("InternetMessageId", "message:InternetMessageId", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BF4 RID: 3060
		public static readonly PropertyDefinition References = new StringPropertyDefinition("References", "message:References", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1);

		// Token: 0x04000BF5 RID: 3061
		public static readonly PropertyDefinition ReplyTo = new ComplexPropertyDefinition<EmailAddressCollection>("ReplyTo", "message:ReplyTo", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate | PropertyDefinitionFlags.CanDelete, ExchangeVersion.Exchange2007_SP1, () => new EmailAddressCollection());

		// Token: 0x04000BF6 RID: 3062
		public static readonly PropertyDefinition Sender = new ContainedPropertyDefinition<EmailAddress>("Sender", "message:Sender", "Mailbox", PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new EmailAddress());

		// Token: 0x04000BF7 RID: 3063
		public static readonly PropertyDefinition ReceivedBy = new ContainedPropertyDefinition<EmailAddress>("ReceivedBy", "message:ReceivedBy", "Mailbox", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new EmailAddress());

		// Token: 0x04000BF8 RID: 3064
		public static readonly PropertyDefinition ReceivedRepresenting = new ContainedPropertyDefinition<EmailAddress>("ReceivedRepresenting", "message:ReceivedRepresenting", "Mailbox", PropertyDefinitionFlags.CanFind, ExchangeVersion.Exchange2007_SP1, () => new EmailAddress());

		// Token: 0x04000BF9 RID: 3065
		public static readonly PropertyDefinition ApprovalRequestData = new ComplexPropertyDefinition<ApprovalRequestData>("ApprovalRequestData", "message:ApprovalRequestData", ExchangeVersion.Exchange2013, () => new ApprovalRequestData());

		// Token: 0x04000BFA RID: 3066
		public static readonly PropertyDefinition VotingInformation = new ComplexPropertyDefinition<VotingInformation>("VotingInformation", "message:VotingInformation", ExchangeVersion.Exchange2013, () => new VotingInformation());

		// Token: 0x04000BFB RID: 3067
		internal new static readonly EmailMessageSchema Instance = new EmailMessageSchema();

		// Token: 0x020001BA RID: 442
		private static class FieldUris
		{
			// Token: 0x04000C06 RID: 3078
			public const string ConversationIndex = "message:ConversationIndex";

			// Token: 0x04000C07 RID: 3079
			public const string ConversationTopic = "message:ConversationTopic";

			// Token: 0x04000C08 RID: 3080
			public const string InternetMessageId = "message:InternetMessageId";

			// Token: 0x04000C09 RID: 3081
			public const string IsRead = "message:IsRead";

			// Token: 0x04000C0A RID: 3082
			public const string IsResponseRequested = "message:IsResponseRequested";

			// Token: 0x04000C0B RID: 3083
			public const string IsReadReceiptRequested = "message:IsReadReceiptRequested";

			// Token: 0x04000C0C RID: 3084
			public const string IsDeliveryReceiptRequested = "message:IsDeliveryReceiptRequested";

			// Token: 0x04000C0D RID: 3085
			public const string References = "message:References";

			// Token: 0x04000C0E RID: 3086
			public const string ReplyTo = "message:ReplyTo";

			// Token: 0x04000C0F RID: 3087
			public const string From = "message:From";

			// Token: 0x04000C10 RID: 3088
			public const string Sender = "message:Sender";

			// Token: 0x04000C11 RID: 3089
			public const string ToRecipients = "message:ToRecipients";

			// Token: 0x04000C12 RID: 3090
			public const string CcRecipients = "message:CcRecipients";

			// Token: 0x04000C13 RID: 3091
			public const string BccRecipients = "message:BccRecipients";

			// Token: 0x04000C14 RID: 3092
			public const string ReceivedBy = "message:ReceivedBy";

			// Token: 0x04000C15 RID: 3093
			public const string ReceivedRepresenting = "message:ReceivedRepresenting";

			// Token: 0x04000C16 RID: 3094
			public const string ApprovalRequestData = "message:ApprovalRequestData";

			// Token: 0x04000C17 RID: 3095
			public const string VotingInformation = "message:VotingInformation";
		}
	}
}
