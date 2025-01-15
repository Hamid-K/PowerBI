using System;

namespace Microsoft.ReportingServices.Hybrid.OAuth
{
	// Token: 0x0200000C RID: 12
	internal sealed class AadUserProvider : IAadUserProvider
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002C33 File Offset: 0x00000E33
		public AadUserProvider(IAadCache cache)
		{
			this.m_cache = cache;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C44 File Offset: 0x00000E44
		public ServiceIdToken GetLoggedInUserDetails()
		{
			ServiceToken serviceTokenFromCache = this.m_cache.GetServiceTokenFromCache();
			if (serviceTokenFromCache == null)
			{
				return null;
			}
			return AadOAuthHelper.GetIdTokenFromResponseString(serviceTokenFromCache.IdToken);
		}

		// Token: 0x0400004D RID: 77
		private readonly IAadCache m_cache;
	}
}
