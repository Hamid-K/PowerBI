using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A1 RID: 417
	[EditorBrowsable(1)]
	public abstract class ResponseObject<TMessage> : ServiceObject where TMessage : EmailMessage
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x00036F56 File Offset: 0x00035F56
		internal ResponseObject(Item referenceItem)
			: base(referenceItem.Service)
		{
			EwsUtilities.Assert(referenceItem != null, "ResponseObject.ctor", "referenceItem is null");
			referenceItem.ThrowIfThisIsNew();
			this.referenceItem = referenceItem;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00036F87 File Offset: 0x00035F87
		internal override ServiceObjectSchema GetSchema()
		{
			return ResponseObjectSchema.Instance;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00036F8E File Offset: 0x00035F8E
		internal override void InternalLoad(PropertySet propertySet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00036F95 File Offset: 0x00035F95
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00036F9C File Offset: 0x00035F9C
		internal List<Item> InternalCreate(FolderId destinationFolderId, MessageDisposition messageDisposition)
		{
			((ItemId)base.PropertyBag[ResponseObjectSchema.ReferenceItemId]).Assign(this.referenceItem.Id);
			return base.Service.InternalCreateResponseObject(this, destinationFolderId, new MessageDisposition?(messageDisposition));
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00036FD6 File Offset: 0x00035FD6
		public TMessage Save(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			return this.InternalCreate(destinationFolderId, MessageDisposition.SaveOnly)[0] as TMessage;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00036FFB File Offset: 0x00035FFB
		public TMessage Save(WellKnownFolderName destinationFolderName)
		{
			return this.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SaveOnly)[0] as TMessage;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0003701A File Offset: 0x0003601A
		public TMessage Save()
		{
			return this.InternalCreate(null, MessageDisposition.SaveOnly)[0] as TMessage;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00037034 File Offset: 0x00036034
		public void Send()
		{
			this.InternalCreate(null, MessageDisposition.SendOnly);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003703F File Offset: 0x0003603F
		public void SendAndSaveCopy(FolderId destinationFolderId)
		{
			EwsUtilities.ValidateParam(destinationFolderId, "destinationFolderId");
			this.InternalCreate(destinationFolderId, MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00037055 File Offset: 0x00036055
		public void SendAndSaveCopy(WellKnownFolderName destinationFolderName)
		{
			this.InternalCreate(new FolderId(destinationFolderName), MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00037065 File Offset: 0x00036065
		public void SendAndSaveCopy()
		{
			this.InternalCreate(null, MessageDisposition.SendAndSaveCopy);
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00037070 File Offset: 0x00036070
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00037087 File Offset: 0x00036087
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

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003709F File Offset: 0x0003609F
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x000370B6 File Offset: 0x000360B6
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

		// Token: 0x04000A0A RID: 2570
		private Item referenceItem;
	}
}
