using System;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A8 RID: 424
	internal sealed class JsonCommandResult
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000ACEAD File Offset: 0x000AB0AD
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x000ACEB5 File Offset: 0x000AB0B5
		public ModelOperationResult ModelOperationResult { get; internal set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000ACEBE File Offset: 0x000AB0BE
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x000ACEC6 File Offset: 0x000AB0C6
		public XmlaResultCollection XmlaResults { get; internal set; }
	}
}
