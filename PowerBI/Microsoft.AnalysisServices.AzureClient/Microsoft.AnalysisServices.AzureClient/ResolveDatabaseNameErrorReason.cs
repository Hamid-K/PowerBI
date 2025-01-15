using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000005 RID: 5
	[Guid("A1F358BD-8DF5-42A8-BD25-9810B247FABA")]
	[ComVisible(true)]
	public enum ResolveDatabaseNameErrorReason
	{
		// Token: 0x04000011 RID: 17
		None,
		// Token: 0x04000012 RID: 18
		DatabaseNotFound,
		// Token: 0x04000013 RID: 19
		DatabaseNameDuplicated
	}
}
