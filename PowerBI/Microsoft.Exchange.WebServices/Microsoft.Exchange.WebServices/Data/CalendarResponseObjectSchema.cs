using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001B1 RID: 433
	internal class CalendarResponseObjectSchema : ServiceObjectSchema
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x00038D94 File Offset: 0x00037D94
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ItemSchema.ItemClass);
			base.RegisterProperty(ItemSchema.Sensitivity);
			base.RegisterProperty(ItemSchema.Body);
			base.RegisterProperty(ItemSchema.Attachments);
			base.RegisterProperty(ItemSchema.InternetMessageHeaders);
			base.RegisterProperty(EmailMessageSchema.Sender);
			base.RegisterProperty(EmailMessageSchema.ToRecipients);
			base.RegisterProperty(EmailMessageSchema.CcRecipients);
			base.RegisterProperty(EmailMessageSchema.BccRecipients);
			base.RegisterProperty(EmailMessageSchema.IsReadReceiptRequested);
			base.RegisterProperty(EmailMessageSchema.IsDeliveryReceiptRequested);
			base.RegisterProperty(ResponseObjectSchema.ReferenceItemId);
		}

		// Token: 0x04000AF8 RID: 2808
		internal static readonly CalendarResponseObjectSchema Instance = new CalendarResponseObjectSchema();
	}
}
