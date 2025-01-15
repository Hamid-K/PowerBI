using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200023D RID: 573
	public enum ODataCollectionReaderState
	{
		// Token: 0x0400069C RID: 1692
		Start,
		// Token: 0x0400069D RID: 1693
		CollectionStart,
		// Token: 0x0400069E RID: 1694
		Value,
		// Token: 0x0400069F RID: 1695
		CollectionEnd,
		// Token: 0x040006A0 RID: 1696
		Exception,
		// Token: 0x040006A1 RID: 1697
		Completed
	}
}
