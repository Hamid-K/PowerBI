using System;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005CD RID: 1485
	[Flags]
	internal enum PropagatorFlags : byte
	{
		// Token: 0x0400197C RID: 6524
		NoFlags = 0,
		// Token: 0x0400197D RID: 6525
		Preserve = 1,
		// Token: 0x0400197E RID: 6526
		ConcurrencyValue = 2,
		// Token: 0x0400197F RID: 6527
		Unknown = 8,
		// Token: 0x04001980 RID: 6528
		Key = 16,
		// Token: 0x04001981 RID: 6529
		ForeignKey = 32
	}
}
