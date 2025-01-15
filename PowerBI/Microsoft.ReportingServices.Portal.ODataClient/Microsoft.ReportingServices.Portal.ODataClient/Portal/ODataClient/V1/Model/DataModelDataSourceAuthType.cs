using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000126 RID: 294
	[OriginalName("DataModelDataSourceAuthType")]
	public enum DataModelDataSourceAuthType
	{
		// Token: 0x040005E7 RID: 1511
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x040005E8 RID: 1512
		[OriginalName("None")]
		None,
		// Token: 0x040005E9 RID: 1513
		[OriginalName("Integrated")]
		Integrated,
		// Token: 0x040005EA RID: 1514
		[OriginalName("Store")]
		Store,
		// Token: 0x040005EB RID: 1515
		[OriginalName("Basic")]
		Basic,
		// Token: 0x040005EC RID: 1516
		[OriginalName("Key")]
		Key
	}
}
