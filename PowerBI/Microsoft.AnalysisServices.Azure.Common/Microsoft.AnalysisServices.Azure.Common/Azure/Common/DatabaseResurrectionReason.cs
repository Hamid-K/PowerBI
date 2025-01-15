using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006C RID: 108
	public enum DatabaseResurrectionReason
	{
		// Token: 0x040001AF RID: 431
		Resolve,
		// Token: 0x040001B0 RID: 432
		ForceResurrect,
		// Token: 0x040001B1 RID: 433
		Test,
		// Token: 0x040001B2 RID: 434
		EvictAndResurrect
	}
}
