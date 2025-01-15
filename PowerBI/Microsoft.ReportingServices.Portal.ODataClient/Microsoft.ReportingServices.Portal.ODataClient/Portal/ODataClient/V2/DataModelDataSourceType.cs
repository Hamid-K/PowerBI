using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000096 RID: 150
	[OriginalName("DataModelDataSourceType")]
	public enum DataModelDataSourceType
	{
		// Token: 0x040002FA RID: 762
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x040002FB RID: 763
		[OriginalName("Live")]
		Live,
		// Token: 0x040002FC RID: 764
		[OriginalName("DirectQuery")]
		DirectQuery,
		// Token: 0x040002FD RID: 765
		[OriginalName("Import")]
		Import
	}
}
