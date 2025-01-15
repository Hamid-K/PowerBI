using System;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000076 RID: 118
	public enum SecurityLogOnType
	{
		// Token: 0x04000215 RID: 533
		Interactive = 2,
		// Token: 0x04000216 RID: 534
		Network,
		// Token: 0x04000217 RID: 535
		Batch,
		// Token: 0x04000218 RID: 536
		Service,
		// Token: 0x04000219 RID: 537
		NetworkClearText = 8,
		// Token: 0x0400021A RID: 538
		NewCredentials
	}
}
