using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001031 RID: 4145
	internal static class CacheConsistencyService
	{
		// Token: 0x06006C39 RID: 27705 RVA: 0x001750E8 File Offset: 0x001732E8
		public static bool VerifyCacheConsistency(IEngineHost host)
		{
			ICacheConsistencyService cacheConsistencyService = host.QueryService<ICacheConsistencyService>();
			return cacheConsistencyService == null || cacheConsistencyService.VerifyCacheConsistency;
		}
	}
}
