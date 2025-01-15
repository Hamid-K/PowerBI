using System;

namespace Microsoft.OData
{
	// Token: 0x02000094 RID: 148
	public enum ODataReaderState
	{
		// Token: 0x040002AE RID: 686
		Start,
		// Token: 0x040002AF RID: 687
		ResourceSetStart,
		// Token: 0x040002B0 RID: 688
		ResourceSetEnd,
		// Token: 0x040002B1 RID: 689
		ResourceStart,
		// Token: 0x040002B2 RID: 690
		ResourceEnd,
		// Token: 0x040002B3 RID: 691
		NestedResourceInfoStart,
		// Token: 0x040002B4 RID: 692
		NestedResourceInfoEnd,
		// Token: 0x040002B5 RID: 693
		EntityReferenceLink,
		// Token: 0x040002B6 RID: 694
		Exception,
		// Token: 0x040002B7 RID: 695
		Completed,
		// Token: 0x040002B8 RID: 696
		Primitive
	}
}
