using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000086 RID: 134
	internal enum TransactionType
	{
		// Token: 0x040002C2 RID: 706
		LocalFromTSQL = 1,
		// Token: 0x040002C3 RID: 707
		LocalFromAPI,
		// Token: 0x040002C4 RID: 708
		Delegated,
		// Token: 0x040002C5 RID: 709
		Distributed,
		// Token: 0x040002C6 RID: 710
		Context
	}
}
