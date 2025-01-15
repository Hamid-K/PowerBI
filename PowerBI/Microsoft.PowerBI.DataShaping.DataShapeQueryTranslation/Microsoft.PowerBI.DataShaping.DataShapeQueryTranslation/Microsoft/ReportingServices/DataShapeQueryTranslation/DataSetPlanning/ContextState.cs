using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000103 RID: 259
	internal enum ContextState
	{
		// Token: 0x040004ED RID: 1261
		Output,
		// Token: 0x040004EE RID: 1262
		ContextOnly,
		// Token: 0x040004EF RID: 1263
		Context,
		// Token: 0x040004F0 RID: 1264
		Rollup,
		// Token: 0x040004F1 RID: 1265
		OutputRollup,
		// Token: 0x040004F2 RID: 1266
		JoinConstraint,
		// Token: 0x040004F3 RID: 1267
		SynchronizationTarget,
		// Token: 0x040004F4 RID: 1268
		SynchronizationContextOnly
	}
}
