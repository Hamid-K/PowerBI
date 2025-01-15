using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x020000A2 RID: 162
	public interface IPowerBIReportCatalogItem : ICatalogItem
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000536 RID: 1334
		// (set) Token: 0x06000537 RID: 1335
		byte[] Content { get; set; }
	}
}
