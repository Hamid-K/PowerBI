using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000057 RID: 87
	internal sealed class PowerBIReportCatalogItem : CatalogItem, IPowerBIReportCatalogItem, ICatalogItem
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0001294B File Offset: 0x00010B4B
		public PowerBIReportCatalogItem()
			: base(CatalogItemType.PowerBIReport)
		{
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00012955 File Offset: 0x00010B55
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0001295D File Offset: 0x00010B5D
		public byte[] Content { get; set; }
	}
}
