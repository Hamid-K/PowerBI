using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001AA RID: 426
	[ServiceObjectDefinition("SuppressReadReceipt", ReturnedByServer = false)]
	internal sealed class SuppressReadReceipt : ServiceObject
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0003765E File Offset: 0x0003665E
		internal SuppressReadReceipt(Item referenceItem)
			: base(referenceItem.Service)
		{
			EwsUtilities.Assert(referenceItem != null, "SuppressReadReceipt.ctor", "referenceItem is null");
			referenceItem.ThrowIfThisIsNew();
			this.referenceItem = referenceItem;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0003768F File Offset: 0x0003668F
		internal override ServiceObjectSchema GetSchema()
		{
			return ResponseObjectSchema.Instance;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00037696 File Offset: 0x00036696
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00037699 File Offset: 0x00036699
		internal override void InternalLoad(PropertySet propertySet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000376A0 File Offset: 0x000366A0
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000376A7 File Offset: 0x000366A7
		internal void InternalCreate(FolderId parentFolderId, MessageDisposition? messageDisposition)
		{
			((ItemId)base.PropertyBag[ResponseObjectSchema.ReferenceItemId]).Assign(this.referenceItem.Id);
			base.Service.InternalCreateResponseObject(this, parentFolderId, messageDisposition);
		}

		// Token: 0x04000A0F RID: 2575
		private Item referenceItem;
	}
}
