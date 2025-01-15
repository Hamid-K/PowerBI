using System;

namespace Microsoft.OData
{
	// Token: 0x020000B6 RID: 182
	public enum ODataReaderState
	{
		// Token: 0x0400030E RID: 782
		Start,
		// Token: 0x0400030F RID: 783
		ResourceSetStart,
		// Token: 0x04000310 RID: 784
		ResourceSetEnd,
		// Token: 0x04000311 RID: 785
		ResourceStart,
		// Token: 0x04000312 RID: 786
		ResourceEnd,
		// Token: 0x04000313 RID: 787
		NestedResourceInfoStart,
		// Token: 0x04000314 RID: 788
		NestedResourceInfoEnd,
		// Token: 0x04000315 RID: 789
		EntityReferenceLink,
		// Token: 0x04000316 RID: 790
		Exception,
		// Token: 0x04000317 RID: 791
		Completed,
		// Token: 0x04000318 RID: 792
		Primitive,
		// Token: 0x04000319 RID: 793
		DeltaResourceSetStart,
		// Token: 0x0400031A RID: 794
		DeltaResourceSetEnd,
		// Token: 0x0400031B RID: 795
		DeletedResourceStart,
		// Token: 0x0400031C RID: 796
		DeletedResourceEnd,
		// Token: 0x0400031D RID: 797
		DeltaLink,
		// Token: 0x0400031E RID: 798
		DeltaDeletedLink,
		// Token: 0x0400031F RID: 799
		NestedProperty,
		// Token: 0x04000320 RID: 800
		Stream
	}
}
