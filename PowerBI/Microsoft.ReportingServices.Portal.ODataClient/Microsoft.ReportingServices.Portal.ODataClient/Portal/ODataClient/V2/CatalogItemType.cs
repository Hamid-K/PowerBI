using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000090 RID: 144
	[OriginalName("CatalogItemType")]
	public enum CatalogItemType
	{
		// Token: 0x040002D6 RID: 726
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x040002D7 RID: 727
		[OriginalName("Folder")]
		Folder,
		// Token: 0x040002D8 RID: 728
		[OriginalName("Report")]
		Report,
		// Token: 0x040002D9 RID: 729
		[OriginalName("DataSource")]
		DataSource,
		// Token: 0x040002DA RID: 730
		[OriginalName("DataSet")]
		DataSet,
		// Token: 0x040002DB RID: 731
		[OriginalName("Component")]
		Component,
		// Token: 0x040002DC RID: 732
		[OriginalName("Resource")]
		Resource,
		// Token: 0x040002DD RID: 733
		[OriginalName("Kpi")]
		Kpi,
		// Token: 0x040002DE RID: 734
		[OriginalName("MobileReport")]
		MobileReport,
		// Token: 0x040002DF RID: 735
		[OriginalName("LinkedReport")]
		LinkedReport,
		// Token: 0x040002E0 RID: 736
		[OriginalName("ReportModel")]
		ReportModel,
		// Token: 0x040002E1 RID: 737
		[OriginalName("PowerBIReport")]
		PowerBIReport,
		// Token: 0x040002E2 RID: 738
		[OriginalName("ExcelWorkbook")]
		ExcelWorkbook
	}
}
