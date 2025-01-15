using System;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200017C RID: 380
	public static class TokenCacheExtensions
	{
		// Token: 0x0600126E RID: 4718 RVA: 0x0003EC24 File Offset: 0x0003CE24
		public static void SetCacheOptions(this ITokenCache tokenCache, CacheOptions options)
		{
			TokenCacheExtensions.ValidatePlatform();
			TokenCache tokenCache2 = (TokenCache)tokenCache;
			ITokenCacheInternal tokenCacheInternal = (ITokenCacheInternal)tokenCache;
			tokenCache2.ServiceBundle.Config.AccessorOptions = options;
			if (tokenCacheInternal.IsAppSubscribedToSerializationEvents())
			{
				throw new MsalClientException("static_cache_with_external_serialization", "You configured MSAL cache serialization at the same time with internal caching options. These are mutually exclusive. Use only one option. Web site and web api scenarios should rely on external cache serialization, as internal cache serialization cannot scale. See https://aka.ms/msal-net-token-cache-serialization .");
			}
			IServiceBundle serviceBundle = tokenCache2.ServiceBundle;
			IPlatformProxy platformProxy = ((serviceBundle != null) ? serviceBundle.PlatformProxy : null) ?? PlatformProxyFactory.CreatePlatformProxy(null);
			tokenCache2.Accessor = platformProxy.CreateTokenCacheAccessor(options, tokenCacheInternal.IsApplicationCache);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003EC9B File Offset: 0x0003CE9B
		private static void ValidatePlatform()
		{
		}
	}
}
