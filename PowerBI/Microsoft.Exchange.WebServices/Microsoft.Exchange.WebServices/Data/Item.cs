using System;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000194 RID: 404
	[Attachable]
	[ServiceObjectDefinition("Item")]
	public class Item : ServiceObject
	{
		// Token: 0x060011E9 RID: 4585 RVA: 0x00033854 File Offset: 0x00032854
		internal Item(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003385D File Offset: 0x0003285D
		internal Item(ItemAttachment parentAttachment)
			: this(parentAttachment.Service)
		{
			EwsUtilities.Assert(parentAttachment != null, "Item.ctor", "parentAttachment is null");
			this.parentAttachment = parentAttachment;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00033888 File Offset: 0x00032888
		public static Item Bind(ExchangeService service, ItemId id, PropertySet propertySet)
		{
			return service.BindToItem<Item>(id, propertySet);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00033892 File Offset: 0x00032892
		public static Item Bind(ExchangeService service, ItemId id)
		{
			return Microsoft.Exchange.WebServices.Data.Item.Bind(service, id, PropertySet.FirstClassProperties);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000338A0 File Offset: 0x000328A0
		internal override ServiceObjectSchema GetSchema()
		{
			return ItemSchema.Instance;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000338A7 File Offset: 0x000328A7
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000338AA File Offset: 0x000328AA
		internal void ThrowIfThisIsAttachment()
		{
			if (this.IsAttachment)
			{
				throw new InvalidOperationException(Strings.OperationDoesNotSupportAttachments);
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x000338C4 File Offset: 0x000328C4
		internal override PropertyDefinition GetIdPropertyDefinition()
		{
			return ItemSchema.Id;
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x000338CC File Offset: 0x000328CC
		internal override void InternalLoad(PropertySet propertySet)
		{
			base.ThrowIfThisIsNew();
			this.ThrowIfThisIsAttachment();
			base.Service.InternalLoadPropertiesForItems(new Item[] { this }, propertySet, ServiceErrorHandling.ThrowOnError);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000338FF File Offset: 0x000328FF
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			this.InternalDelete(deleteMode, sendCancellationsMode, affectedTaskOccurrences, false);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003390C File Offset: 0x0003290C
		internal void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences, bool suppressReadReceipts)
		{
			base.ThrowIfThisIsNew();
			this.ThrowIfThisIsAttachment();
			if (sendCancellationsMode == null)
			{
				sendCancellationsMode = this.DefaultSendCancellationsMode;
			}
			if (affectedTaskOccurrences == null)
			{
				affectedTaskOccurrences = this.DefaultAffectedTaskOccurrences;
			}
			base.Service.DeleteItem(this.Id, deleteMode, sendCancellationsMode, affectedTaskOccurrences, suppressReadReceipts);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00033960 File Offset: 0x00032960
		internal void InternalCreate(FolderId parentFolderId, MessageDisposition? messageDisposition, SendInvitationsMode? sendInvitationsMode)
		{
			base.ThrowIfThisIsNotNew();
			this.ThrowIfThisIsAttachment();
			if (this.IsNew || base.IsDirty)
			{
				base.Service.CreateItem(this, parentFolderId, messageDisposition, (sendInvitationsMode != null) ? sendInvitationsMode : this.DefaultSendInvitationsMode);
				this.Attachments.Save();
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000339B4 File Offset: 0x000329B4
		internal Item InternalUpdate(FolderId parentFolderId, ConflictResolutionMode conflictResolutionMode, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode)
		{
			return this.InternalUpdate(parentFolderId, conflictResolutionMode, messageDisposition, sendInvitationsOrCancellationsMode, false);
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000339C4 File Offset: 0x000329C4
		internal Item InternalUpdate(FolderId parentFolderId, ConflictResolutionMode conflictResolutionMode, MessageDisposition? messageDisposition, SendInvitationsOrCancellationsMode? sendInvitationsOrCancellationsMode, bool suppressReadReceipts)
		{
			base.ThrowIfThisIsNew();
			this.ThrowIfThisIsAttachment();
			Item item = null;
			if (base.IsDirty && base.PropertyBag.GetIsUpdateCallNecessary())
			{
				item = base.Service.UpdateItem(this, parentFolderId, conflictResolutionMode, messageDisposition, (sendInvitationsOrCancellationsMode != null) ? sendInvitationsOrCancellationsMode : this.DefaultSendInvitationsOrCancellationsMode, suppressReadReceipts);
			}
			if (this.HasUnprocessedAttachmentChanges())
			{
				this.Attachments.Validate();
				this.Attachments.Save();
			}
			return item;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00033A38 File Offset: 0x00032A38
		internal bool HasUnprocessedAttachmentChanges()
		{
			return this.Attachments.HasUnprocessedChanges();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00033A45 File Offset: 0x00032A45
		internal ItemAttachment ParentAttachment
		{
			get
			{
				return this.parentAttachment;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00033A4D File Offset: 0x00032A4D
		internal ItemId RootItemId
		{
			get
			{
				if (this.IsAttachment && this.ParentAttachment.Owner != null)
				{
					return this.ParentAttachment.Owner.RootItemId;
				}
				return this.Id;
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00033A7B File Offset: 0x00032A7B
		public void Delete(DeleteMode deleteMode)
		{
			this.Delete(deleteMode, false);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00033A88 File Offset: 0x00032A88
		public void Delete(DeleteMode deleteMode, bool suppressReadReceipts)
		{
			this.InternalDelete(deleteMode, default(SendCancellationsMode?), default(AffectedTaskOccurrence?), suppressReadReceipts);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00033AB0 File Offset: 0x00032AB0
		public void Save(FolderId parentFolderId)
		{
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			this.InternalCreate(parentFolderId, new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsMode?));
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00033AE0 File Offset: 0x00032AE0
		public void Save(WellKnownFolderName parentFolderName)
		{
			this.InternalCreate(new FolderId(parentFolderName), new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsMode?));
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00033B08 File Offset: 0x00032B08
		public void Save()
		{
			this.InternalCreate(null, new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsMode?));
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00033B2B File Offset: 0x00032B2B
		public void Update(ConflictResolutionMode conflictResolutionMode)
		{
			this.Update(conflictResolutionMode, false);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00033B38 File Offset: 0x00032B38
		public void Update(ConflictResolutionMode conflictResolutionMode, bool suppressReadReceipts)
		{
			this.InternalUpdate(null, conflictResolutionMode, new MessageDisposition?(MessageDisposition.SaveOnly), default(SendInvitationsOrCancellationsMode?), suppressReadReceipts);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00033B5E File Offset: 0x00032B5E
		public Item Copy(FolderId destinationFolderId)
		{
			base.ThrowIfThisIsNew();
			this.ThrowIfThisIsAttachment();
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return base.Service.CopyItem(this.Id, destinationFolderId);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00033B89 File Offset: 0x00032B89
		public Item Copy(WellKnownFolderName destinationFolderName)
		{
			return this.Copy(new FolderId(destinationFolderName));
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00033B97 File Offset: 0x00032B97
		public Item Move(FolderId destinationFolderId)
		{
			base.ThrowIfThisIsNew();
			this.ThrowIfThisIsAttachment();
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return base.Service.MoveItem(this.Id, destinationFolderId);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00033BC2 File Offset: 0x00032BC2
		public Item Move(WellKnownFolderName destinationFolderName)
		{
			return this.Move(new FolderId(destinationFolderName));
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00033BD0 File Offset: 0x00032BD0
		public void SetExtendedProperty(ExtendedPropertyDefinition extendedPropertyDefinition, object value)
		{
			this.ExtendedProperties.SetExtendedProperty(extendedPropertyDefinition, value);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00033BDF File Offset: 0x00032BDF
		public bool RemoveExtendedProperty(ExtendedPropertyDefinition extendedPropertyDefinition)
		{
			return this.ExtendedProperties.RemoveExtendedProperty(extendedPropertyDefinition);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00033BED File Offset: 0x00032BED
		internal override ExtendedPropertyCollection GetExtendedProperties()
		{
			return this.ExtendedProperties;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00033BF8 File Offset: 0x00032BF8
		internal override void Validate()
		{
			base.Validate();
			this.Attachments.Validate();
			Flag flag;
			if (base.TryGetProperty<Flag>(ItemSchema.Flag, out flag) && flag != null)
			{
				if (base.Service.RequestedServerVersion < ExchangeVersion.Exchange2013)
				{
					throw new ServiceVersionException(string.Format(Strings.ParameterIncompatibleWithRequestVersion, "Flag", ExchangeVersion.Exchange2013));
				}
				flag.Validate();
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00033C5C File Offset: 0x00032C5C
		internal override bool GetIsTimeZoneHeaderRequired(bool isUpdateOperation)
		{
			if (!isUpdateOperation && base.Service.RequestedServerVersion >= ExchangeVersion.Exchange2010_SP2)
			{
				foreach (ItemAttachment itemAttachment in Enumerable.OfType<ItemAttachment>(this.Attachments))
				{
					if (itemAttachment.Item != null && itemAttachment.Item.GetIsTimeZoneHeaderRequired(false))
					{
						return true;
					}
				}
			}
			return base.GetIsTimeZoneHeaderRequired(isUpdateOperation);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00033CDC File Offset: 0x00032CDC
		public bool IsAttachment
		{
			get
			{
				return this.parentAttachment != null;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00033CEA File Offset: 0x00032CEA
		public override bool IsNew
		{
			get
			{
				if (this.IsAttachment)
				{
					return this.ParentAttachment.IsNew;
				}
				return base.IsNew;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00033D06 File Offset: 0x00032D06
		public ItemId Id
		{
			get
			{
				return (ItemId)base.PropertyBag[this.GetIdPropertyDefinition()];
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00033D1E File Offset: 0x00032D1E
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00033D35 File Offset: 0x00032D35
		public MimeContent MimeContent
		{
			get
			{
				return (MimeContent)base.PropertyBag[ItemSchema.MimeContent];
			}
			set
			{
				base.PropertyBag[ItemSchema.MimeContent] = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00033D48 File Offset: 0x00032D48
		public FolderId ParentFolderId
		{
			get
			{
				return (FolderId)base.PropertyBag[ItemSchema.ParentFolderId];
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00033D5F File Offset: 0x00032D5F
		// (set) Token: 0x06001211 RID: 4625 RVA: 0x00033D76 File Offset: 0x00032D76
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

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00033D8E File Offset: 0x00032D8E
		public AttachmentCollection Attachments
		{
			get
			{
				return (AttachmentCollection)base.PropertyBag[ItemSchema.Attachments];
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00033DA5 File Offset: 0x00032DA5
		public DateTime DateTimeReceived
		{
			get
			{
				return (DateTime)base.PropertyBag[ItemSchema.DateTimeReceived];
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00033DBC File Offset: 0x00032DBC
		public int Size
		{
			get
			{
				return (int)base.PropertyBag[ItemSchema.Size];
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00033DD3 File Offset: 0x00032DD3
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00033DEA File Offset: 0x00032DEA
		public StringList Categories
		{
			get
			{
				return (StringList)base.PropertyBag[ItemSchema.Categories];
			}
			set
			{
				base.PropertyBag[ItemSchema.Categories] = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00033DFD File Offset: 0x00032DFD
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x00033E14 File Offset: 0x00032E14
		public string Culture
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.Culture];
			}
			set
			{
				base.PropertyBag[ItemSchema.Culture] = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00033E27 File Offset: 0x00032E27
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00033E3E File Offset: 0x00032E3E
		public Importance Importance
		{
			get
			{
				return (Importance)base.PropertyBag[ItemSchema.Importance];
			}
			set
			{
				base.PropertyBag[ItemSchema.Importance] = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00033E56 File Offset: 0x00032E56
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00033E6D File Offset: 0x00032E6D
		public string InReplyTo
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.InReplyTo];
			}
			set
			{
				base.PropertyBag[ItemSchema.InReplyTo] = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00033E80 File Offset: 0x00032E80
		public bool IsSubmitted
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsSubmitted];
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00033E97 File Offset: 0x00032E97
		public bool IsAssociated
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsAssociated];
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00033EAE File Offset: 0x00032EAE
		public bool IsDraft
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsDraft];
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00033EC5 File Offset: 0x00032EC5
		public bool IsFromMe
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsFromMe];
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00033EDC File Offset: 0x00032EDC
		public bool IsResend
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsResend];
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00033EF3 File Offset: 0x00032EF3
		public bool IsUnmodified
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsUnmodified];
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x00033F0A File Offset: 0x00032F0A
		public InternetMessageHeaderCollection InternetMessageHeaders
		{
			get
			{
				return (InternetMessageHeaderCollection)base.PropertyBag[ItemSchema.InternetMessageHeaders];
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00033F21 File Offset: 0x00032F21
		public DateTime DateTimeSent
		{
			get
			{
				return (DateTime)base.PropertyBag[ItemSchema.DateTimeSent];
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00033F38 File Offset: 0x00032F38
		public DateTime DateTimeCreated
		{
			get
			{
				return (DateTime)base.PropertyBag[ItemSchema.DateTimeCreated];
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00033F4F File Offset: 0x00032F4F
		public ResponseActions AllowedResponseActions
		{
			get
			{
				return (ResponseActions)base.PropertyBag[ItemSchema.AllowedResponseActions];
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00033F66 File Offset: 0x00032F66
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x00033F7D File Offset: 0x00032F7D
		public DateTime ReminderDueBy
		{
			get
			{
				return (DateTime)base.PropertyBag[ItemSchema.ReminderDueBy];
			}
			set
			{
				base.PropertyBag[ItemSchema.ReminderDueBy] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00033F95 File Offset: 0x00032F95
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00033FAC File Offset: 0x00032FAC
		public bool IsReminderSet
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.IsReminderSet];
			}
			set
			{
				base.PropertyBag[ItemSchema.IsReminderSet] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00033FC4 File Offset: 0x00032FC4
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00033FDB File Offset: 0x00032FDB
		public int ReminderMinutesBeforeStart
		{
			get
			{
				return (int)base.PropertyBag[ItemSchema.ReminderMinutesBeforeStart];
			}
			set
			{
				base.PropertyBag[ItemSchema.ReminderMinutesBeforeStart] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00033FF3 File Offset: 0x00032FF3
		public string DisplayCc
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.DisplayCc];
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0003400A File Offset: 0x0003300A
		public string DisplayTo
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.DisplayTo];
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00034021 File Offset: 0x00033021
		public bool HasAttachments
		{
			get
			{
				return (bool)base.PropertyBag[ItemSchema.HasAttachments];
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00034038 File Offset: 0x00033038
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x0003404F File Offset: 0x0003304F
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

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00034062 File Offset: 0x00033062
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00034079 File Offset: 0x00033079
		public string ItemClass
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0003408C File Offset: 0x0003308C
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x000340A3 File Offset: 0x000330A3
		public string Subject
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.Subject];
			}
			set
			{
				this.SetSubject(value);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x000340AC File Offset: 0x000330AC
		public string WebClientReadFormQueryString
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.WebClientReadFormQueryString];
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000340C3 File Offset: 0x000330C3
		public string WebClientEditFormQueryString
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.WebClientEditFormQueryString];
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x000340DA File Offset: 0x000330DA
		public ExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return (ExtendedPropertyCollection)base.PropertyBag[ServiceObjectSchema.ExtendedProperties];
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x000340F1 File Offset: 0x000330F1
		public EffectiveRights EffectiveRights
		{
			get
			{
				return (EffectiveRights)base.PropertyBag[ItemSchema.EffectiveRights];
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00034108 File Offset: 0x00033108
		public string LastModifiedName
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.LastModifiedName];
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0003411F File Offset: 0x0003311F
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)base.PropertyBag[ItemSchema.LastModifiedTime];
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00034136 File Offset: 0x00033136
		public ConversationId ConversationId
		{
			get
			{
				return (ConversationId)base.PropertyBag[ItemSchema.ConversationId];
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0003414D File Offset: 0x0003314D
		public UniqueBody UniqueBody
		{
			get
			{
				return (UniqueBody)base.PropertyBag[ItemSchema.UniqueBody];
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00034164 File Offset: 0x00033164
		public byte[] StoreEntryId
		{
			get
			{
				return (byte[])base.PropertyBag[ItemSchema.StoreEntryId];
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0003417B File Offset: 0x0003317B
		public byte[] InstanceKey
		{
			get
			{
				return (byte[])base.PropertyBag[ItemSchema.InstanceKey];
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00034192 File Offset: 0x00033192
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x000341A9 File Offset: 0x000331A9
		public Flag Flag
		{
			get
			{
				return (Flag)base.PropertyBag[ItemSchema.Flag];
			}
			set
			{
				base.PropertyBag[ItemSchema.Flag] = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000341BC File Offset: 0x000331BC
		public NormalizedBody NormalizedBody
		{
			get
			{
				return (NormalizedBody)base.PropertyBag[ItemSchema.NormalizedBody];
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000341D3 File Offset: 0x000331D3
		public EntityExtractionResult EntityExtractionResult
		{
			get
			{
				return (EntityExtractionResult)base.PropertyBag[ItemSchema.EntityExtractionResult];
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000341EA File Offset: 0x000331EA
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x00034201 File Offset: 0x00033201
		public PolicyTag PolicyTag
		{
			get
			{
				return (PolicyTag)base.PropertyBag[ItemSchema.PolicyTag];
			}
			set
			{
				base.PropertyBag[ItemSchema.PolicyTag] = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00034214 File Offset: 0x00033214
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x0003422B File Offset: 0x0003322B
		public ArchiveTag ArchiveTag
		{
			get
			{
				return (ArchiveTag)base.PropertyBag[ItemSchema.ArchiveTag];
			}
			set
			{
				base.PropertyBag[ItemSchema.ArchiveTag] = value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0003423E File Offset: 0x0003323E
		public DateTime? RetentionDate
		{
			get
			{
				return (DateTime?)base.PropertyBag[ItemSchema.RetentionDate];
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00034255 File Offset: 0x00033255
		public string Preview
		{
			get
			{
				return (string)base.PropertyBag[ItemSchema.Preview];
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0003426C File Offset: 0x0003326C
		public TextBody TextBody
		{
			get
			{
				return (TextBody)base.PropertyBag[ItemSchema.TextBody];
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00034283 File Offset: 0x00033283
		public IconIndex IconIndex
		{
			get
			{
				return (IconIndex)base.PropertyBag[ItemSchema.IconIndex];
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0003429C File Offset: 0x0003329C
		internal virtual AffectedTaskOccurrence? DefaultAffectedTaskOccurrences
		{
			get
			{
				return default(AffectedTaskOccurrence?);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x000342B4 File Offset: 0x000332B4
		internal virtual SendCancellationsMode? DefaultSendCancellationsMode
		{
			get
			{
				return default(SendCancellationsMode?);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x000342CC File Offset: 0x000332CC
		internal virtual SendInvitationsMode? DefaultSendInvitationsMode
		{
			get
			{
				return default(SendInvitationsMode?);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x000342E4 File Offset: 0x000332E4
		internal virtual SendInvitationsOrCancellationsMode? DefaultSendInvitationsOrCancellationsMode
		{
			get
			{
				return default(SendInvitationsOrCancellationsMode?);
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x000342FA File Offset: 0x000332FA
		internal virtual void SetSubject(string subject)
		{
			base.PropertyBag[ItemSchema.Subject] = subject;
		}

		// Token: 0x04000A08 RID: 2568
		private ItemAttachment parentAttachment;
	}
}
