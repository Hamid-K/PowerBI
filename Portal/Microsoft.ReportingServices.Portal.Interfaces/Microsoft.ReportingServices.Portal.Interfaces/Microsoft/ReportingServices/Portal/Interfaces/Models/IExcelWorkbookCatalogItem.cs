using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Models
{
	// Token: 0x02000097 RID: 151
	public interface IExcelWorkbookCatalogItem : ICatalogItem
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004FF RID: 1279
		// (set) Token: 0x06000500 RID: 1280
		byte[] Content { get; set; }
	}
}
