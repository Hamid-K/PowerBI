using System;

namespace Microsoft.OData
{
	// Token: 0x0200005A RID: 90
	public enum ODataBatchReaderState
	{
		// Token: 0x04000156 RID: 342
		Initial,
		// Token: 0x04000157 RID: 343
		Operation,
		// Token: 0x04000158 RID: 344
		ChangesetStart,
		// Token: 0x04000159 RID: 345
		ChangesetEnd,
		// Token: 0x0400015A RID: 346
		Completed,
		// Token: 0x0400015B RID: 347
		Exception
	}
}
