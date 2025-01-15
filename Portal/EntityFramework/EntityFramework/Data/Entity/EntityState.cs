using System;

namespace System.Data.Entity
{
	// Token: 0x02000066 RID: 102
	[Flags]
	public enum EntityState
	{
		// Token: 0x040000C5 RID: 197
		Detached = 1,
		// Token: 0x040000C6 RID: 198
		Unchanged = 2,
		// Token: 0x040000C7 RID: 199
		Added = 4,
		// Token: 0x040000C8 RID: 200
		Deleted = 8,
		// Token: 0x040000C9 RID: 201
		Modified = 16
	}
}
