using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B2 RID: 434
	internal class CancelMeetingMessageSchema : ServiceObjectSchema
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x00038E3F File Offset: 0x00037E3F
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(EmailMessageSchema.IsReadReceiptRequested);
			base.RegisterProperty(EmailMessageSchema.IsDeliveryReceiptRequested);
			base.RegisterProperty(ResponseObjectSchema.ReferenceItemId);
			base.RegisterProperty(CancelMeetingMessageSchema.Body);
		}

		// Token: 0x04000AF9 RID: 2809
		public static readonly PropertyDefinition Body = new ComplexPropertyDefinition<MessageBody>("NewBodyContent", PropertyDefinitionFlags.CanSet, ExchangeVersion.Exchange2007_SP1, () => new MessageBody());

		// Token: 0x04000AFA RID: 2810
		internal static readonly CancelMeetingMessageSchema Instance = new CancelMeetingMessageSchema();
	}
}
