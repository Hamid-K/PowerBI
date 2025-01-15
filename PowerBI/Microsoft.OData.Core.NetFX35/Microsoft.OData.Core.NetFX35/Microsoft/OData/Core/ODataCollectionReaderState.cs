using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000152 RID: 338
	public enum ODataCollectionReaderState
	{
		// Token: 0x0400055B RID: 1371
		Start,
		// Token: 0x0400055C RID: 1372
		CollectionStart,
		// Token: 0x0400055D RID: 1373
		Value,
		// Token: 0x0400055E RID: 1374
		CollectionEnd,
		// Token: 0x0400055F RID: 1375
		Exception,
		// Token: 0x04000560 RID: 1376
		Completed
	}
}
