using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A9 RID: 425
	public sealed class ResponseMessage : ResponseObject<EmailMessage>
	{
		// Token: 0x06001464 RID: 5220 RVA: 0x0003752E File Offset: 0x0003652E
		internal ResponseMessage(Item referenceItem, ResponseMessageType responseType)
			: base(referenceItem)
		{
			this.responseType = responseType;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0003753E File Offset: 0x0003653E
		internal override ServiceObjectSchema GetSchema()
		{
			return ResponseMessageSchema.Instance;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00037545 File Offset: 0x00036545
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00037548 File Offset: 0x00036548
		internal override string GetXmlElementNameOverride()
		{
			switch (this.responseType)
			{
			case ResponseMessageType.Reply:
				return "ReplyToItem";
			case ResponseMessageType.ReplyAll:
				return "ReplyAllToItem";
			case ResponseMessageType.Forward:
				return "ForwardItem";
			default:
				EwsUtilities.Assert(false, "ResponseMessage.GetXmlElementNameOverride", "An unexpected value for responseType could not be handled.");
				return null;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00037593 File Offset: 0x00036593
		public ResponseMessageType ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003759B File Offset: 0x0003659B
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x000375B2 File Offset: 0x000365B2
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

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x000375C5 File Offset: 0x000365C5
		public EmailAddressCollection ToRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.ToRecipients];
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x000375DC File Offset: 0x000365DC
		public EmailAddressCollection CcRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.CcRecipients];
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x000375F3 File Offset: 0x000365F3
		public EmailAddressCollection BccRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.BccRecipients];
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0003760A File Offset: 0x0003660A
		// (set) Token: 0x0600146F RID: 5231 RVA: 0x00037621 File Offset: 0x00036621
		public string Subject
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.Subject];
			}
			set
			{
				base.PropertyBag[ItemSchema.Subject] = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00037634 File Offset: 0x00036634
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x0003764B File Offset: 0x0003664B
		public MessageBody BodyPrefix
		{
			get
			{
				return (MessageBody)base.PropertyBag[ResponseObjectSchema.BodyPrefix];
			}
			set
			{
				base.PropertyBag[ResponseObjectSchema.BodyPrefix] = value;
			}
		}

		// Token: 0x04000A0E RID: 2574
		private ResponseMessageType responseType;
	}
}
