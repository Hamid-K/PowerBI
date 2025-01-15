using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006B RID: 107
	public enum DatabaseEvictionReason
	{
		// Token: 0x040001A5 RID: 421
		ServiceMove,
		// Token: 0x040001A6 RID: 422
		Inactive,
		// Token: 0x040001A7 RID: 423
		Test,
		// Token: 0x040001A8 RID: 424
		EphemeralForceDeletion,
		// Token: 0x040001A9 RID: 425
		RevertingRoutingToBe,
		// Token: 0x040001AA RID: 426
		ForceEviction,
		// Token: 0x040001AB RID: 427
		ForceEvictionAutoImageSaveFailed,
		// Token: 0x040001AC RID: 428
		EvictFromUnresponsiveNode,
		// Token: 0x040001AD RID: 429
		ETagMismatch
	}
}
