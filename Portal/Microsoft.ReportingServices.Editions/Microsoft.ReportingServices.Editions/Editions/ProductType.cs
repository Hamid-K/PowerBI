using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000013 RID: 19
	[CLSCompliant(true)]
	public enum ProductType
	{
		// Token: 0x0400005A RID: 90
		[SkuStrings]
		[ProductDetails]
		None,
		// Token: 0x0400005B RID: 91
		[SkuStrings(FullName = "SQL Server Reporting Services", ShortName = "SSRS")]
		[ProductDetails(DefaultInstanceName = "SSRS")]
		SqlServerReportingServices,
		// Token: 0x0400005C RID: 92
		[SkuStrings(FullName = "Power BI Report Server", ShortName = "PBIRS")]
		[ProductDetails(DefaultInstanceName = "PBIRS")]
		PowerBiReportServer
	}
}
