using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A3 RID: 419
	[EditorBrowsable(1)]
	public abstract class CalendarResponseMessage<TMessage> : CalendarResponseMessageBase<TMessage> where TMessage : EmailMessage
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x00037160 File Offset: 0x00036160
		internal CalendarResponseMessage(Item referenceItem)
			: base(referenceItem)
		{
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00037169 File Offset: 0x00036169
		internal override ServiceObjectSchema GetSchema()
		{
			return CalendarResponseObjectSchema.Instance;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00037170 File Offset: 0x00036170
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00037187 File Offset: 0x00036187
		public MessageBody Body
		{
			get
			{
				return (MessageBody)base.PropertyBag[ItemSchema.Body];
			}
			set
			{
				base.PropertyBag[ItemSchema.Body] = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003719A File Offset: 0x0003619A
		public EmailAddressCollection ToRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.ToRecipients];
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x000371B1 File Offset: 0x000361B1
		public EmailAddressCollection CcRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.CcRecipients];
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x000371C8 File Offset: 0x000361C8
		public EmailAddressCollection BccRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.BccRecipients];
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000371DF File Offset: 0x000361DF
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x000371F6 File Offset: 0x000361F6
		internal string ItemClass
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.ItemClass];
			}
			set
			{
				base.PropertyBag[ItemSchema.ItemClass] = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00037209 File Offset: 0x00036209
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00037220 File Offset: 0x00036220
		public Sensitivity Sensitivity
		{
			get
			{
				return (Sensitivity)base.PropertyBag[ItemSchema.Sensitivity];
			}
			set
			{
				base.PropertyBag[ItemSchema.Sensitivity] = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00037238 File Offset: 0x00036238
		public AttachmentCollection Attachments
		{
			get
			{
				return (AttachmentCollection)base.PropertyBag[ItemSchema.Attachments];
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003724F File Offset: 0x0003624F
		internal InternetMessageHeaderCollection InternetMessageHeaders
		{
			get
			{
				return (InternetMessageHeaderCollection)base.PropertyBag[ItemSchema.InternetMessageHeaders];
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00037266 File Offset: 0x00036266
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x0003727D File Offset: 0x0003627D
		public EmailAddress Sender
		{
			get
			{
				return (EmailAddress)base.PropertyBag[EmailMessageSchema.Sender];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.Sender] = value;
			}
		}
	}
}
