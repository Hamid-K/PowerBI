using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A7 RID: 423
	[ServiceObjectDefinition("PostReplyItem", ReturnedByServer = false)]
	public sealed class PostReply : ServiceObject
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x00037309 File Offset: 0x00036309
		internal PostReply(Item referenceItem)
			: base(referenceItem.Service)
		{
			EwsUtilities.Assert(referenceItem != null, "PostReply.ctor", "referenceItem is null");
			referenceItem.ThrowIfThisIsNew();
			this.referenceItem = referenceItem;
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003733A File Offset: 0x0003633A
		internal override ServiceObjectSchema GetSchema()
		{
			return PostReplySchema.Instance;
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00037341 File Offset: 0x00036341
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00037344 File Offset: 0x00036344
		internal PostItem InternalCreate(FolderId parentFolderId, MessageDisposition? messageDisposition)
		{
			((ItemId)base.PropertyBag[ResponseObjectSchema.ReferenceItemId]).Assign(this.referenceItem.Id);
			List<Item> list = base.Service.InternalCreateResponseObject(this, parentFolderId, messageDisposition);
			PostItem postItem = EwsUtilities.FindFirstItemOfType<PostItem>(list);
			EwsUtilities.Assert(postItem != null, "PostReply.InternalCreate", "postItem is null. The CreateItem call did not return the expected PostItem.");
			return postItem;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x000373A3 File Offset: 0x000363A3
		internal override void InternalLoad(PropertySet propertySet)
		{
			throw new InvalidOperationException(Strings.LoadingThisObjectTypeNotSupported);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000373B4 File Offset: 0x000363B4
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			throw new InvalidOperationException(Strings.DeletingThisObjectTypeNotAuthorized);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000373C8 File Offset: 0x000363C8
		public PostItem Save()
		{
			return this.InternalCreate(null, default(MessageDisposition?));
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x000373E8 File Offset: 0x000363E8
		public PostItem Save(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return this.InternalCreate(destinationFolderId, default(MessageDisposition?));
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00037410 File Offset: 0x00036410
		public PostItem Save(WellKnownFolderName destinationFolderName)
		{
			return this.InternalCreate(new FolderId(destinationFolderName), default(MessageDisposition?));
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00037432 File Offset: 0x00036432
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00037449 File Offset: 0x00036449
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0003745C File Offset: 0x0003645C
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00037473 File Offset: 0x00036473
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

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00037486 File Offset: 0x00036486
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x0003749D File Offset: 0x0003649D
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

		// Token: 0x04000A0C RID: 2572
		private Item referenceItem;
	}
}
