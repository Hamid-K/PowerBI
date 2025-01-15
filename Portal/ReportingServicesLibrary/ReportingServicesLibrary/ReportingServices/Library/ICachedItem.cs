using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000266 RID: 614
	internal interface ICachedItem
	{
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600162B RID: 5675
		// (set) Token: 0x0600162C RID: 5676
		string Key { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600162D RID: 5677
		// (set) Token: 0x0600162E RID: 5678
		DateTime ExpirationDate { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600162F RID: 5679
		long SizeEstimateKb { get; }

		// Token: 0x06001630 RID: 5680
		void NotifyItemIsCached();
	}
}
