using System;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000357 RID: 855
	internal enum PlanCompilerPhase
	{
		// Token: 0x04000E4A RID: 3658
		PreProcessor,
		// Token: 0x04000E4B RID: 3659
		AggregatePushdown,
		// Token: 0x04000E4C RID: 3660
		Normalization,
		// Token: 0x04000E4D RID: 3661
		NTE,
		// Token: 0x04000E4E RID: 3662
		ProjectionPruning,
		// Token: 0x04000E4F RID: 3663
		NestPullup,
		// Token: 0x04000E50 RID: 3664
		Transformations,
		// Token: 0x04000E51 RID: 3665
		JoinElimination,
		// Token: 0x04000E52 RID: 3666
		NullSemantics,
		// Token: 0x04000E53 RID: 3667
		CodeGen,
		// Token: 0x04000E54 RID: 3668
		PostCodeGen,
		// Token: 0x04000E55 RID: 3669
		MaxMarker
	}
}
