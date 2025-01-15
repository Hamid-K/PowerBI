using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001C7 RID: 455
	internal class ResponseMessageSchema : ServiceObjectSchema
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x0003B1CC File Offset: 0x0003A1CC
		internal override void RegisterProperties()
		{
			base.RegisterProperties();
			base.RegisterProperty(ItemSchema.Subject);
			base.RegisterProperty(ItemSchema.Body);
			base.RegisterProperty(EmailMessageSchema.ToRecipients);
			base.RegisterProperty(EmailMessageSchema.CcRecipients);
			base.RegisterProperty(EmailMessageSchema.BccRecipients);
			base.RegisterProperty(EmailMessageSchema.IsReadReceiptRequested);
			base.RegisterProperty(EmailMessageSchema.IsDeliveryReceiptRequested);
			base.RegisterProperty(ResponseObjectSchema.ReferenceItemId);
			base.RegisterProperty(ResponseObjectSchema.BodyPrefix);
		}

		// Token: 0x04000C98 RID: 3224
		internal static readonly ResponseMessageSchema Instance = new ResponseMessageSchema();
	}
}
