using System;

namespace Microsoft.OData
{
	// Token: 0x02000036 RID: 54
	public enum ODataBatchReaderState
	{
		// Token: 0x040000ED RID: 237
		Initial,
		// Token: 0x040000EE RID: 238
		Operation,
		// Token: 0x040000EF RID: 239
		ChangesetStart,
		// Token: 0x040000F0 RID: 240
		ChangesetEnd,
		// Token: 0x040000F1 RID: 241
		Completed,
		// Token: 0x040000F2 RID: 242
		Exception
	}
}
