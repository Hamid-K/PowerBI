using System;

namespace Microsoft.OData
{
	// Token: 0x02000078 RID: 120
	public enum ODataDeltaReaderState
	{
		// Token: 0x040001E7 RID: 487
		Start,
		// Token: 0x040001E8 RID: 488
		DeltaResourceSetStart,
		// Token: 0x040001E9 RID: 489
		DeltaResourceSetEnd,
		// Token: 0x040001EA RID: 490
		DeltaResourceStart,
		// Token: 0x040001EB RID: 491
		DeltaResourceEnd,
		// Token: 0x040001EC RID: 492
		DeltaDeletedEntry,
		// Token: 0x040001ED RID: 493
		DeltaLink,
		// Token: 0x040001EE RID: 494
		DeltaDeletedLink,
		// Token: 0x040001EF RID: 495
		Exception,
		// Token: 0x040001F0 RID: 496
		Completed,
		// Token: 0x040001F1 RID: 497
		NestedResource
	}
}
