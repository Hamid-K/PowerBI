using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A1 RID: 161
	public interface IMobileReportResourceCatalogItem : IResourceCatalogItem, ICatalogItem
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000534 RID: 1332
		// (set) Token: 0x06000535 RID: 1333
		MobileReportResourceType Type { get; set; }
	}
}
