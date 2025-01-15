using System;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200046A RID: 1130
	public interface IPlatformActivityTracingPolicyEvaluator
	{
		// Token: 0x06002334 RID: 9012
		bool EvaluateForNewIncomingRequestEvent(string clientActivityId, Guid clientRequestId, long requestLength, string requestUri, string requestHeaders, string clientAddress);
	}
}
