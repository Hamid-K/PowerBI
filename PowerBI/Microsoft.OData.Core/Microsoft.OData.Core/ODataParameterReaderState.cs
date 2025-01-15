using System;

namespace Microsoft.OData
{
	// Token: 0x020000A5 RID: 165
	public enum ODataParameterReaderState
	{
		// Token: 0x040002BB RID: 699
		Start,
		// Token: 0x040002BC RID: 700
		Value,
		// Token: 0x040002BD RID: 701
		Collection,
		// Token: 0x040002BE RID: 702
		Exception,
		// Token: 0x040002BF RID: 703
		Completed,
		// Token: 0x040002C0 RID: 704
		Resource,
		// Token: 0x040002C1 RID: 705
		ResourceSet
	}
}
