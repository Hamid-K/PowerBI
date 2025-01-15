using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x02000098 RID: 152
	public interface IMobileReportManifest
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000501 RID: 1281
		IMobileReportManifestItem Definition { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000502 RID: 1282
		IEnumerable<IMobileReportManifestResourceGroup> Resources { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000503 RID: 1283
		IEnumerable<IMobileReportManifestDataSetItem> DataSets { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000504 RID: 1284
		IEnumerable<IMobileReportManifestThumbnailItem> Thumbnails { get; }
	}
}
