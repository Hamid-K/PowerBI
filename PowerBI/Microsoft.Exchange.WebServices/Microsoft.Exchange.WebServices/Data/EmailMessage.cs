using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200019A RID: 410
	[Attachable]
	[ServiceObjectDefinition("Message")]
	public class EmailMessage : Item
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x00035E8D File Offset: 0x00034E8D
		public EmailMessage(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00035E96 File Offset: 0x00034E96
		internal EmailMessage(ItemAttachment parentAttachment)
			: base(parentAttachment)
		{
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00035E9F File Offset: 0x00034E9F
		public new static EmailMessage Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<EmailMessage>(id, propertySet);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00035EA9 File Offset: 0x00034EA9
		public new static EmailMessage Bind(ExchangeService service, ItemId id)
		{
			return EmailMessage.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00035EB7 File Offset: 0x00034EB7
		internal override ServiceObjectSchema GetSchema()
		{
			return EmailMessageSchema.Instance;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00035EBE File Offset: 0x00034EBE
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00035EC4 File Offset: 0x00034EC4
		private void InternalSend(FolderId parentFolderId, MessageDisposition messageDisposition)
		{
			base.ThrowIfThisIsAttachment();
			if (this.IsNew)
			{
				if (base.Attachments.Count == 0 || messageDisposition == MessageDisposition.SaveOnly)
				{
					base.InternalCreate(parentFolderId, new MessageDisposition?(messageDisposition), default(SendInvitationsMode?));
					return;
				}
				base.InternalCreate(null, new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsMode?));
				base.Service.SendItem(this, parentFolderId);
				return;
			}
			else
			{
				if (base.HasUnprocessedAttachmentChanges())
				{
					base.Attachments.Validate();
					base.Attachments.Save();
				}
				if (base.PropertyBag.GetIsUpdateCallNecessary())
				{
					base.InternalUpdate(parentFolderId, ConflictResolutionMode.AutoResolve, new MessageDisposition?(messageDisposition), default(SendInvitationsOrCancellationsMode?));
					return;
				}
				base.Service.SendItem(this, parentFolderId);
				return;
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00035F7B File Offset: 0x00034F7B
		public ResponseMessage CreateReply(bool replyAll)
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, replyAll ? ResponseMessageType.ReplyAll : ResponseMessageType.Reply);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00035F90 File Offset: 0x00034F90
		public ResponseMessage CreateForward()
		{
			base.ThrowIfThisIsNew();
			return new ResponseMessage(this, ResponseMessageType.Forward);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00035FA0 File Offset: 0x00034FA0
		public void Reply(MessageBody bodyPrefix, bool replyAll)
		{
			ResponseMessage responseMessage = this.CreateReply(replyAll);
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00035FC2 File Offset: 0x00034FC2
		public void Forward(MessageBody bodyPrefix, params EmailAddress[] toRecipients)
		{
			this.Forward(bodyPrefix, (IEnumerable<EmailAddress>)toRecipients);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00035FD4 File Offset: 0x00034FD4
		public void Forward(MessageBody bodyPrefix, IEnumerable<EmailAddress> toRecipients)
		{
			ResponseMessage responseMessage = this.CreateForward();
			responseMessage.BodyPrefix = bodyPrefix;
			responseMessage.ToRecipients.AddRange(toRecipients);
			responseMessage.SendAndSaveCopy();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00036001 File Offset: 0x00035001
		public void Send()
		{
			this.InternalSend(null, MessageDisposition.SendOnly);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0003600B File Offset: 0x0003500B
		public void SendAndSaveCopy(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			this.InternalSend(destinationFolderId, MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00036020 File Offset: 0x00035020
		public void SendAndSaveCopy(WellKnownFolderName destinationFolderName)
		{
			this.InternalSend(new FolderId(destinationFolderName), MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003602F File Offset: 0x0003502F
		public void SendAndSaveCopy()
		{
			this.InternalSend(new FolderId(WellKnownFolderName.SentItems), MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00036040 File Offset: 0x00035040
		public void SuppressReadReceipt()
		{
			base.ThrowIfThisIsNew();
			new SuppressReadReceipt(this).InternalCreate(null, default(MessageDisposition?));
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x00036068 File Offset: 0x00035068
		public EmailAddressCollection ToRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.ToRecipients];
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0003607F File Offset: 0x0003507F
		public EmailAddressCollection BccRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.BccRecipients];
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00036096 File Offset: 0x00035096
		public EmailAddressCollection CcRecipients
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.CcRecipients];
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x000360AD File Offset: 0x000350AD
		public string ConversationTopic
		{
			get
			{
				return (string)base.PropertyBag[EmailMessageSchema.ConversationTopic];
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x000360C4 File Offset: 0x000350C4
		public byte[] ConversationIndex
		{
			get
			{
				return (byte[])base.PropertyBag[EmailMessageSchema.ConversationIndex];
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x000360DB File Offset: 0x000350DB
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x000360F2 File Offset: 0x000350F2
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

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00036105 File Offset: 0x00035105
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0003610D File Offset: 0x0003510D
		public new bool IsAssociated
		{
			get
			{
				return base.IsAssociated;
			}
			set
			{
				base.PropertyBag[ItemSchema.IsAssociated] = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00036125 File Offset: 0x00035125
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0003613C File Offset: 0x0003513C
		public bool IsDeliveryReceiptRequested
		{
			get
			{
				return (bool)base.PropertyBag[EmailMessageSchema.IsDeliveryReceiptRequested];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.IsDeliveryReceiptRequested] = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00036154 File Offset: 0x00035154
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0003616B File Offset: 0x0003516B
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

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00036183 File Offset: 0x00035183
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x0003619A File Offset: 0x0003519A
		public bool IsReadReceiptRequested
		{
			get
			{
				return (bool)base.PropertyBag[EmailMessageSchema.IsReadReceiptRequested];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.IsReadReceiptRequested] = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x000361B2 File Offset: 0x000351B2
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x000361C9 File Offset: 0x000351C9
		public bool? IsResponseRequested
		{
			get
			{
				return (bool?)base.PropertyBag[EmailMessageSchema.IsResponseRequested];
			}
			set
			{
				base.PropertyBag[EmailMessageSchema.IsResponseRequested] = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000361E1 File Offset: 0x000351E1
		public string InternetMessageId
		{
			get
			{
				return (string)base.PropertyBag[EmailMessageSchema.InternetMessageId];
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x000361F8 File Offset: 0x000351F8
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0003620F File Offset: 0x0003520F
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

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00036222 File Offset: 0x00035222
		public EmailAddressCollection ReplyTo
		{
			get
			{
				return (EmailAddressCollection)base.PropertyBag[EmailMessageSchema.ReplyTo];
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00036239 File Offset: 0x00035239
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00036250 File Offset: 0x00035250
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00036263 File Offset: 0x00035263
		public EmailAddress ReceivedBy
		{
			get
			{
				return (EmailAddress)base.PropertyBag[EmailMessageSchema.ReceivedBy];
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0003627A File Offset: 0x0003527A
		public EmailAddress ReceivedRepresenting
		{
			get
			{
				return (EmailAddress)base.PropertyBag[EmailMessageSchema.ReceivedRepresenting];
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00036291 File Offset: 0x00035291
		public ApprovalRequestData ApprovalRequestData
		{
			get
			{
				return (ApprovalRequestData)base.PropertyBag[EmailMessageSchema.ApprovalRequestData];
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x000362A8 File Offset: 0x000352A8
		public VotingInformation VotingInformation
		{
			get
			{
				return (VotingInformation)base.PropertyBag[EmailMessageSchema.VotingInformation];
			}
		}
	}
}
