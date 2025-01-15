using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F2 RID: 242
	internal enum DatabaseExistenceState
	{
		// Token: 0x04000906 RID: 2310
		Unknown,
		// Token: 0x04000907 RID: 2311
		DoesNotExist,
		// Token: 0x04000908 RID: 2312
		ExistsConsideredEmpty,
		// Token: 0x04000909 RID: 2313
		Exists
	}
}
