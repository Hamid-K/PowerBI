using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000AB RID: 171
	[Guid("64D709A8-D8E4-4711-ABF8-59156178A14A")]
	public enum ProcessType
	{
		// Token: 0x040004B9 RID: 1209
		ProcessFull,
		// Token: 0x040004BA RID: 1210
		ProcessAdd,
		// Token: 0x040004BB RID: 1211
		ProcessUpdate,
		// Token: 0x040004BC RID: 1212
		ProcessIndexes,
		// Token: 0x040004BD RID: 1213
		ProcessData,
		// Token: 0x040004BE RID: 1214
		ProcessDefault,
		// Token: 0x040004BF RID: 1215
		ProcessClear,
		// Token: 0x040004C0 RID: 1216
		ProcessStructure,
		// Token: 0x040004C1 RID: 1217
		ProcessClearStructureOnly,
		// Token: 0x040004C2 RID: 1218
		ProcessScriptCache,
		// Token: 0x040004C3 RID: 1219
		ProcessRecalc,
		// Token: 0x040004C4 RID: 1220
		ProcessDefrag
	}
}
