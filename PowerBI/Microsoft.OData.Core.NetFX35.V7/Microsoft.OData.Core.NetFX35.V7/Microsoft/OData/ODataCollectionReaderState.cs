using System;

namespace Microsoft.OData
{
	// Token: 0x02000041 RID: 65
	public enum ODataCollectionReaderState
	{
		// Token: 0x0400011C RID: 284
		Start,
		// Token: 0x0400011D RID: 285
		CollectionStart,
		// Token: 0x0400011E RID: 286
		Value,
		// Token: 0x0400011F RID: 287
		CollectionEnd,
		// Token: 0x04000120 RID: 288
		Exception,
		// Token: 0x04000121 RID: 289
		Completed
	}
}
