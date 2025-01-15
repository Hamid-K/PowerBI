using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000121 RID: 289
	[OriginalName("CatalogItemType")]
	public enum CatalogItemType
	{
		// Token: 0x040005AA RID: 1450
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x040005AB RID: 1451
		[OriginalName("Folder")]
		Folder,
		// Token: 0x040005AC RID: 1452
		[OriginalName("Report")]
		Report,
		// Token: 0x040005AD RID: 1453
		[OriginalName("DataSource")]
		DataSource,
		// Token: 0x040005AE RID: 1454
		[OriginalName("DataSet")]
		DataSet,
		// Token: 0x040005AF RID: 1455
		[OriginalName("Component")]
		Component,
		// Token: 0x040005B0 RID: 1456
		[OriginalName("Resource")]
		Resource,
		// Token: 0x040005B1 RID: 1457
		[OriginalName("Kpi")]
		Kpi,
		// Token: 0x040005B2 RID: 1458
		[OriginalName("MobileReport")]
		MobileReport,
		// Token: 0x040005B3 RID: 1459
		[OriginalName("LinkedReport")]
		LinkedReport,
		// Token: 0x040005B4 RID: 1460
		[OriginalName("ReportModel")]
		ReportModel,
		// Token: 0x040005B5 RID: 1461
		[OriginalName("PowerBIReport")]
		PowerBIReport,
		// Token: 0x040005B6 RID: 1462
		[OriginalName("ExcelWorkbook")]
		ExcelWorkbook
	}
}
