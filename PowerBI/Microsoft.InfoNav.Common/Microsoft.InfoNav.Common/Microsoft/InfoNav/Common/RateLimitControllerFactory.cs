using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000067 RID: 103
	internal sealed class RateLimitControllerFactory : IRateLimitControllerFactory
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000A26B File Offset: 0x0000846B
		public IRateLimitController CreateRateLimitControllerInstance(TimeSpan minTimeBetweenRequests, bool exponentialBackOff = false)
		{
			return new RateLimitController(minTimeBetweenRequests, exponentialBackOff, null);
		}
	}
}
