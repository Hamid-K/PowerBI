using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200005C RID: 92
	internal interface IRateLimitControllerFactory
	{
		// Token: 0x06000395 RID: 917
		IRateLimitController CreateRateLimitControllerInstance(TimeSpan minTimeBetweenRequests, bool exponentialBackOff = false);
	}
}
