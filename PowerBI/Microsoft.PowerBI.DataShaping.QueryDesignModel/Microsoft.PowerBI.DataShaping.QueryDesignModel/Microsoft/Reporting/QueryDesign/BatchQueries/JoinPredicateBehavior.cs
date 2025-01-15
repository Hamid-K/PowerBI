using System;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026B RID: 619
	[Flags]
	internal enum JoinPredicateBehavior
	{
		// Token: 0x04000ED4 RID: 3796
		None = 0,
		// Token: 0x04000ED5 RID: 3797
		AutoPredicates = 2,
		// Token: 0x04000ED6 RID: 3798
		ExistsPredicates = 4,
		// Token: 0x04000ED7 RID: 3799
		AutoPredicatesForFilters = 8
	}
}
