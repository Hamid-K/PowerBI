using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000085 RID: 133
	[Flags]
	internal enum ExpressionContent
	{
		// Token: 0x040002E3 RID: 739
		None = 0,
		// Token: 0x040002E4 RID: 740
		ModelReference = 2,
		// Token: 0x040002E5 RID: 741
		SubqueryReference = 4,
		// Token: 0x040002E6 RID: 742
		ScopedEval = 8
	}
}
