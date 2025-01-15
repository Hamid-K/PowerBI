using System;

namespace Microsoft.OData
{
	// Token: 0x02000080 RID: 128
	public enum ODataParameterReaderState
	{
		// Token: 0x04000255 RID: 597
		Start,
		// Token: 0x04000256 RID: 598
		Value,
		// Token: 0x04000257 RID: 599
		Collection,
		// Token: 0x04000258 RID: 600
		Exception,
		// Token: 0x04000259 RID: 601
		Completed,
		// Token: 0x0400025A RID: 602
		Resource,
		// Token: 0x0400025B RID: 603
		ResourceSet
	}
}
