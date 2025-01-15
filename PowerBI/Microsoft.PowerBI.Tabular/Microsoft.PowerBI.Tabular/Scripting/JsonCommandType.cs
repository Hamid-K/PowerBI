using System;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A9 RID: 425
	internal enum JsonCommandType
	{
		// Token: 0x040004F4 RID: 1268
		Unknown,
		// Token: 0x040004F5 RID: 1269
		Alter,
		// Token: 0x040004F6 RID: 1270
		CreateOrReplace,
		// Token: 0x040004F7 RID: 1271
		Create,
		// Token: 0x040004F8 RID: 1272
		Delete,
		// Token: 0x040004F9 RID: 1273
		Refresh,
		// Token: 0x040004FA RID: 1274
		Sequence,
		// Token: 0x040004FB RID: 1275
		Backup,
		// Token: 0x040004FC RID: 1276
		Restore,
		// Token: 0x040004FD RID: 1277
		Attach,
		// Token: 0x040004FE RID: 1278
		Detach,
		// Token: 0x040004FF RID: 1279
		Synchronize,
		// Token: 0x04000500 RID: 1280
		MergePartitions,
		// Token: 0x04000501 RID: 1281
		ApplyAutomaticAggregations,
		// Token: 0x04000502 RID: 1282
		Export
	}
}
