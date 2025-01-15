using System;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x0200009D RID: 157
	public interface IMobileReportManifestThumbnailItem : IMobileReportManifestItem
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600051C RID: 1308
		// (set) Token: 0x0600051D RID: 1309
		MobileReportThumbnailType Type { get; set; }
	}
}
