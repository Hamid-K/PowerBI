using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000124 RID: 292
	[OriginalName("DataModelDataSourceType")]
	public enum DataModelDataSourceType
	{
		// Token: 0x040005C3 RID: 1475
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x040005C4 RID: 1476
		[OriginalName("Live")]
		Live,
		// Token: 0x040005C5 RID: 1477
		[OriginalName("DirectQuery")]
		DirectQuery,
		// Token: 0x040005C6 RID: 1478
		[OriginalName("Import")]
		Import
	}
}
