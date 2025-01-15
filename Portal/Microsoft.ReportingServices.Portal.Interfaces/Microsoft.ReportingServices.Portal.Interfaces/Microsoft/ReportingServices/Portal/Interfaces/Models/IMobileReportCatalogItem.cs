using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A6 RID: 166
	public interface IMobileReportCatalogItem : ICatalogItem
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000552 RID: 1362
		// (set) Token: 0x06000553 RID: 1363
		bool AllowCaching { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000554 RID: 1364
		IMobileReportManifest Manifest { get; }
	}
}
