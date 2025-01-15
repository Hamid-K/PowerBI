using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000085 RID: 133
	internal enum TransactionState
	{
		// Token: 0x040002BC RID: 700
		Pending,
		// Token: 0x040002BD RID: 701
		Active,
		// Token: 0x040002BE RID: 702
		Aborted,
		// Token: 0x040002BF RID: 703
		Committed,
		// Token: 0x040002C0 RID: 704
		Unknown
	}
}
