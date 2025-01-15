using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Client.Instance.Oidc
{
	// Token: 0x0200027A RID: 634
	internal static class OidcRetrieverWithCache
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000518C0 File Offset: 0x0004FAC0
		public static async Task<OidcMetadata> GetOidcAsync(string authority, RequestContext requestContext)
		{
			OidcMetadata oidcMetadata;
			OidcMetadata oidcMetadata2;
			if (OidcRetrieverWithCache.s_cache.TryGetValue(authority, out oidcMetadata))
			{
				requestContext.Logger.Verbose(() => "[OIDC Discovery] OIDC discovery found a cached entry for " + authority);
				oidcMetadata2 = oidcMetadata;
			}
			else
			{
				await OidcRetrieverWithCache.s_lockOidcRetrieval.WaitAsync().ConfigureAwait(false);
				Uri oidcMetadataEndpoint = null;
				try
				{
					if (OidcRetrieverWithCache.s_cache.TryGetValue(authority, out oidcMetadata))
					{
						requestContext.Logger.Verbose(() => "[OIDC Discovery] OIDC discovery found a cached entry for " + authority);
						oidcMetadata2 = oidcMetadata;
					}
					else
					{
						UriBuilder uriBuilder = new UriBuilder(authority);
						uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' }) + "/.well-known/openid-configuration";
						oidcMetadataEndpoint = uriBuilder.Uri;
						oidcMetadata = await new OAuth2Client(requestContext.Logger, requestContext.ServiceBundle.HttpManager).DiscoverOidcMetadataAsync(oidcMetadataEndpoint, requestContext).ConfigureAwait(false);
						OidcRetrieverWithCache.s_cache[authority] = oidcMetadata;
						requestContext.Logger.Verbose(() => "[OIDC Discovery] OIDC discovery retrieved metadata from the network for " + authority);
						oidcMetadata2 = oidcMetadata;
					}
				}
				catch (Exception ex)
				{
					requestContext.Logger.Error(string.Format("[OIDC Discovery] Failed to retrieve OpenID configuration from the OpenID endpoint {0} due to {1}", authority + ".well-known/openid-configuration", ex));
					if (ex is MsalServiceException)
					{
						throw;
					}
					throw new MsalServiceException("oidc_failure", string.Format("Failed to retrieve OIDC configuration from {0}. See inner exception. ", oidcMetadataEndpoint), ex);
				}
				finally
				{
					OidcRetrieverWithCache.s_lockOidcRetrieval.Release();
				}
			}
			return oidcMetadata2;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005190B File Offset: 0x0004FB0B
		public static void ResetCacheForTest()
		{
			OidcRetrieverWithCache.s_cache.Clear();
		}

		// Token: 0x04000B2E RID: 2862
		private static readonly ConcurrentDictionary<string, OidcMetadata> s_cache = new ConcurrentDictionary<string, OidcMetadata>();

		// Token: 0x04000B2F RID: 2863
		private static readonly SemaphoreSlim s_lockOidcRetrieval = new SemaphoreSlim(1);
	}
}
