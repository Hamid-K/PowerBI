using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000148 RID: 328
	public enum ODataBatchReaderState
	{
		// Token: 0x04000525 RID: 1317
		Initial,
		// Token: 0x04000526 RID: 1318
		Operation,
		// Token: 0x04000527 RID: 1319
		ChangesetStart,
		// Token: 0x04000528 RID: 1320
		ChangesetEnd,
		// Token: 0x04000529 RID: 1321
		Completed,
		// Token: 0x0400052A RID: 1322
		Exception
	}
}
