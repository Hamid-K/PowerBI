using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019F RID: 415
	[Attachable]
	[ServiceObjectDefinition("PostItem")]
	public sealed class PostItem : Item
	{
		// Token: 0x060013D8 RID: 5080 RVA: 0x000369D3 File Offset: 0x000359D3
		public PostItem(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x000369DC File Offset: 0x000359DC
		internal PostItem(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x000369E5 File Offset: 0x000359E5
		public new static PostItem Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<PostItem>(id, propertySet);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x000369EF File Offset: 0x000359EF
		public new static PostItem Bind(ExchangeService service, ItemId id)
		{
			return PostItem.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000369FD File Offset: 0x000359FD
		internal override ServiceObjectSchema GetSchema()
		{
			return PostItemSchema.Instance;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00036A04 File Offset: 0x00035A04
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00036A07 File Offset: 0x00035A07
		public PostReply CreatePostReply()
		{
			base.ThrowIfThisIsNew();
			return new PostReply(this);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00036A18 File Offset: 0x00035A18
		public void PostReply(MessageBody bodyPrefix)
		{
			PostReply postReply = this.CreatePostReply();
			postReply.BodyPrefix = bodyPrefix;
			postReply.Save();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00036A3A File Offset: 0x00035A3A
		public ResponseMessage CreateReply(bool replyAll)
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, replyAll ? ResponseMessageType.ReplyAll : ResponseMessageType.Reply);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00036A50 File Offset: 0x00035A50
		public void Reply(MessageBody bodyPrefix, bool replyAll)
		{
			ResponseMessage responseMessage = this.CreateReply(replyAll);
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00036A72 File Offset: 0x00035A72
		public ResponseMessage CreateForward()
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, ResponseMessageType.Forward);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00036A81 File Offset: 0x00035A81
		public void Forward(MessageBody bodyPrefix, params EmailAddress[] toRecipients)
		{
			this.Forward(bodyPrefix, (IEnumerable<EmailAddress>)toRecipients);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00036A90 File Offset: 0x00035A90
		public void Forward(MessageBody bodyPrefix, IEnumerable<EmailAddress> toRecipients)
		{
			ResponseMessage responseMessage = this.CreateForward();
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.ToRecipients.AddRange(toRecipients);
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x00036ABD File Offset: 0x00035ABD
		public byte[] ConversationIndex
		{
			get
			{
				return (byte[])base.PropertyBag[EmailMessageSchema.ConversationIndex];
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00036AD4 File Offset: 0x00035AD4
		public string ConversationTopic
		{
			get
			{
				return (string)base.PropertyBag[EmailMessageSchema.ConversationTopic];
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00036AEB File Offset: 0x00035AEB
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00036B02 File Offset: 0x00035B02
		public EmailAddress From
		{
			get
			{
				return (EmailAddress)base.PropertyBag[EmailMessageSchema.From];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.From] = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00036B15 File Offset: 0x00035B15
		public string InternetMessageId
		{
			get
			{
				return (string)base.PropertyBag[EmailMessageSchema.InternetMessageId];
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00036B2C File Offset: 0x00035B2C
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00036B43 File Offset: 0x00035B43
		public bool IsRead
		{
			get
			{
				return (bool)base.PropertyBag[EmailMessageSchema.IsRead];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.IsRead] = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00036B5B File Offset: 0x00035B5B
		public DateTime PostedTime
		{
			get
			{
				return (DateTime)base.PropertyBag[PostItemSchema.PostedTime];
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00036B72 File Offset: 0x00035B72
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x00036B89 File Offset: 0x00035B89
		public string References
		{
			get
			{
				return (string)base.PropertyBag[EmailMessageSchema.References];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.References] = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00036B9C File Offset: 0x00035B9C
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00036BB3 File Offset: 0x00035BB3
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
