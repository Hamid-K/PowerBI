using System;

namespace Microsoft.OData
{
	// Token: 0x02000064 RID: 100
	public enum ODataCollectionReaderState
	{
		// Token: 0x0400017A RID: 378
		Start,
		// Token: 0x0400017B RID: 379
		CollectionStart,
		// Token: 0x0400017C RID: 380
		Value,
		// Token: 0x0400017D RID: 381
		CollectionEnd,
		// Token: 0x0400017E RID: 382
		Exception,
		// Token: 0x0400017F RID: 383
		Completed
	}
}
