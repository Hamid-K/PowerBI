using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001A8 RID: 424
	[ServiceObjectDefinition("RemoveItem", ReturnedByServer = false)]
	internal sealed class RemoveFromCalendar : ServiceObject
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x000374B0 File Offset: 0x000364B0
		internal RemoveFromCalendar(Item referenceItem)
			: base(referenceItem.Service)
		{
			EwsUtilities.Assert(referenceItem != null, "RemoveFromCalendar.ctor", "referenceItem is null");
			referenceItem.ThrowIfThisIsNew();
			this.referenceItem = referenceItem;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000374E1 File Offset: 0x000364E1
		internal override ServiceObjectSchema GetSchema()
		{
			return ResponseObjectSchema.Instance;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x000374E8 File Offset: 0x000364E8
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x000374EB File Offset: 0x000364EB
		internal override void InternalLoad(PropertySet propertySet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x000374F2 File Offset: 0x000364F2
		internal override void InternalDelete(DeleteMode deleteMode, SendCancellationsMode? sendCancellationsMode, AffectedTaskOccurrence? affectedTaskOccurrences)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x000374F9 File Offset: 0x000364F9
		internal List<Item> InternalCreate(FolderId parentFolderId, MessageDisposition? messageDisposition)
		{
			((ItemId)base.PropertyBag[ResponseObjectSchema.ReferenceItemId]).Assign(this.referenceItem.Id);
			return base.Service.InternalCreateResponseObject(this, parentFolderId, messageDisposition);
		}

		// Token: 0x04000A0D RID: 2573
		private Item referenceItem;
	}
}
