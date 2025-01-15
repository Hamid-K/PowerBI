using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.DataRefresh;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001F4 RID: 500
	internal interface IRefreshableMetadataObjectBody : IMetadataObjectBody, ITxObjectBody
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001CA6 RID: 7334
		// (set) Token: 0x06001CA7 RID: 7335
		bool RefreshRequested { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001CA8 RID: 7336
		// (set) Token: 0x06001CA9 RID: 7337
		RefreshTypeMask RequestedRefreshMask { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001CAA RID: 7338
		// (set) Token: 0x06001CAB RID: 7339
		ICollection<OverrideCollection> Overrides { get; set; }
	}
}
