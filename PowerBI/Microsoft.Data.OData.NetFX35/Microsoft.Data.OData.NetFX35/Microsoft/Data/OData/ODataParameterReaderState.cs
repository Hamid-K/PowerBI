using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E5 RID: 485
	public enum ODataParameterReaderState
	{
		// Token: 0x0400052F RID: 1327
		Start,
		// Token: 0x04000530 RID: 1328
		Value,
		// Token: 0x04000531 RID: 1329
		Collection,
		// Token: 0x04000532 RID: 1330
		Exception,
		// Token: 0x04000533 RID: 1331
		Completed
	}
}
