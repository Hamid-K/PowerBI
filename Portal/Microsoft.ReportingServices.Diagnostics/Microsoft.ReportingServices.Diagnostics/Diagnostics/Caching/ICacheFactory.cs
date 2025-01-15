using System;

namespace Microsoft.ReportingServices.Diagnostics.Caching
{
	// Token: 0x0200007F RID: 127
	internal interface ICacheFactory
	{
		// Token: 0x0600041A RID: 1050
		ICache CreateCache(CacheIdentifier cacheId);
	}
}
