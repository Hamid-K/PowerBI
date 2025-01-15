using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F5 RID: 501
	internal interface IRefreshablePartitionBody : IRefreshableMetadataObjectBody, IMetadataObjectBody, ITxObjectBody
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001CAC RID: 7340
		bool MergePartitionsRequested { get; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001CAD RID: 7341
		// (set) Token: 0x06001CAE RID: 7342
		IEnumerable<Partition> MergePartitionSources { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001CAF RID: 7343
		// (set) Token: 0x06001CB0 RID: 7344
		bool AnalyzeRefreshPolicyImpactRequested { get; set; }
	}
}
