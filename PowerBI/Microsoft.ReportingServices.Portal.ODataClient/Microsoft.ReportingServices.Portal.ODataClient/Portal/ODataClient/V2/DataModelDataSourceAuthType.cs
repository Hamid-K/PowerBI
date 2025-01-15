using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000098 RID: 152
	[OriginalName("DataModelDataSourceAuthType")]
	public enum DataModelDataSourceAuthType
	{
		// Token: 0x04000320 RID: 800
		[OriginalName("Unknown")]
		Unknown,
		// Token: 0x04000321 RID: 801
		[OriginalName("Anonymous")]
		Anonymous,
		// Token: 0x04000322 RID: 802
		[OriginalName("Integrated")]
		Integrated,
		// Token: 0x04000323 RID: 803
		[OriginalName("Windows")]
		Windows,
		// Token: 0x04000324 RID: 804
		[OriginalName("UsernamePassword")]
		UsernamePassword,
		// Token: 0x04000325 RID: 805
		[OriginalName("Key")]
		Key,
		// Token: 0x04000326 RID: 806
		[OriginalName("Impersonate")]
		Impersonate
	}
}
