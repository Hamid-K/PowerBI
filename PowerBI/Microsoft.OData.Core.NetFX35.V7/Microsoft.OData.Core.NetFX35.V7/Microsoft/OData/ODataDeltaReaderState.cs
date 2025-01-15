using System;

namespace Microsoft.OData
{
	// Token: 0x02000055 RID: 85
	public enum ODataDeltaReaderState
	{
		// Token: 0x04000185 RID: 389
		Start,
		// Token: 0x04000186 RID: 390
		DeltaResourceSetStart,
		// Token: 0x04000187 RID: 391
		DeltaResourceSetEnd,
		// Token: 0x04000188 RID: 392
		DeltaResourceStart,
		// Token: 0x04000189 RID: 393
		DeltaResourceEnd,
		// Token: 0x0400018A RID: 394
		DeltaDeletedEntry,
		// Token: 0x0400018B RID: 395
		DeltaLink,
		// Token: 0x0400018C RID: 396
		DeltaDeletedLink,
		// Token: 0x0400018D RID: 397
		Exception,
		// Token: 0x0400018E RID: 398
		Completed,
		// Token: 0x0400018F RID: 399
		NestedResource
	}
}
