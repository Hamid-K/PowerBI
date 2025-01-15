using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000058 RID: 88
	internal interface IRoutingTokenFactory
	{
		// Token: 0x060002BD RID: 701
		string GenerateRoutingToken(string sessionId);

		// Token: 0x060002BE RID: 702
		bool IsTokenForCurrentNode(string routingToken, string sessionid);
	}
}
