using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200007A RID: 122
	[Flags]
	public enum VirtualServerNameGenerationOptions
	{
		// Token: 0x040001F1 RID: 497
		BySubscriptionAndAuthority = 0,
		// Token: 0x040001F2 RID: 498
		Random = 1,
		// Token: 0x040001F3 RID: 499
		BySuffix = 2
	}
}
