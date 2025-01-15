using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F6 RID: 502
	internal interface ITxObject
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001CB1 RID: 7345
		// (set) Token: 0x06001CB2 RID: 7346
		ITxObjectBody Body { get; set; }

		// Token: 0x06001CB3 RID: 7347
		ITxObjectBody CreateBody();

		// Token: 0x06001CB4 RID: 7348
		void NotifyBodyReverted();
	}
}
