using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200087C RID: 2172
	internal class ItemHolder
	{
		// Token: 0x060077B3 RID: 30643 RVA: 0x001EE3ED File Offset: 0x001EC5ED
		internal ItemHolder()
		{
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x001EE3F5 File Offset: 0x001EC5F5
		internal virtual int ComputeSizeForReference()
		{
			return this.BaseSize() + ItemSizes.ReferenceSize;
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x001EE403 File Offset: 0x001EC603
		internal int BaseSize()
		{
			return ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 1 + ItemSizes.ReferenceSize;
		}

		// Token: 0x04003C5E RID: 15454
		internal ItemHolder Previous;

		// Token: 0x04003C5F RID: 15455
		internal ItemHolder Next;

		// Token: 0x04003C60 RID: 15456
		internal IStorable Item;

		// Token: 0x04003C61 RID: 15457
		[NonSerialized]
		internal BaseReference Reference;

		// Token: 0x04003C62 RID: 15458
		[NonSerialized]
		internal InQueueState InQueue;
	}
}
