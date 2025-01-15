using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F7 RID: 503
	internal interface ITxObjectBody
	{
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001CB5 RID: 7349
		ITxObject Owner { get; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001CB6 RID: 7350
		// (set) Token: 0x06001CB7 RID: 7351
		ITxObjectBody CreatedFrom { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001CB8 RID: 7352
		// (set) Token: 0x06001CB9 RID: 7353
		TxSavepoint Savepoint { get; set; }

		// Token: 0x06001CBA RID: 7354
		void CopyFrom(ITxObjectBody other, CopyContext context);
	}
}
