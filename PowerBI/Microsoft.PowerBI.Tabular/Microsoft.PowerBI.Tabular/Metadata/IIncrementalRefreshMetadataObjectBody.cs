using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E6 RID: 486
	internal interface IIncrementalRefreshMetadataObjectBody : IRefreshableMetadataObjectBody, IMetadataObjectBody, ITxObjectBody
	{
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001C67 RID: 7271
		// (set) Token: 0x06001C68 RID: 7272
		bool ApplyRefreshPolicyRequested { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001C69 RID: 7273
		// (set) Token: 0x06001C6A RID: 7274
		DateTime? ApplyRefreshPolicyEffectiveDate { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001C6B RID: 7275
		// (set) Token: 0x06001C6C RID: 7276
		bool RefreshAfterApplyRefreshPolicyRequested { get; set; }
	}
}
