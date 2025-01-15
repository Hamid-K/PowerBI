using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200014C RID: 332
	internal enum UsageResourceType
	{
		// Token: 0x0400072F RID: 1839
		None,
		// Token: 0x04000730 RID: 1840
		Size = 10000,
		// Token: 0x04000731 RID: 1841
		Transactions,
		// Token: 0x04000732 RID: 1842
		Bandwidth,
		// Token: 0x04000733 RID: 1843
		Connections,
		// Token: 0x04000734 RID: 1844
		SimpleClientConnections,
		// Token: 0x04000735 RID: 1845
		IncomingBandwidth,
		// Token: 0x04000736 RID: 1846
		OutgoingBandwidth,
		// Token: 0x04000737 RID: 1847
		ReadRequests,
		// Token: 0x04000738 RID: 1848
		WriteRequests,
		// Token: 0x04000739 RID: 1849
		RequestMisses,
		// Token: 0x0400073A RID: 1850
		RestTransactions
	}
}
