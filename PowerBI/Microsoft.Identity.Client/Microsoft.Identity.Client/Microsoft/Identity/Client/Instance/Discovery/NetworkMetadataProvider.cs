using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000285 RID: 645
	internal class NetworkMetadataProvider : INetworkMetadataProvider
	{
		// Token: 0x060018DE RID: 6366 RVA: 0x00051FDC File Offset: 0x000501DC
		public NetworkMetadataProvider(IHttpManager httpManager, INetworkCacheMetadataProvider networkCacheMetadataProvider, Uri userProvidedInstanceDiscoveryUri = null)
		{
			if (httpManager == null)
			{
				throw new ArgumentNullException("httpManager");
			}
			this._httpManager = httpManager;
			if (networkCacheMetadataProvider == null)
			{
				throw new ArgumentNullException("networkCacheMetadataProvider");
			}
			this._networkCacheMetadataProvider = networkCacheMetadataProvider;
			this._userProvidedInstanceDiscoveryUri = userProvidedInstanceDiscoveryUri;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00052018 File Offset: 0x00050218
		public async Task<InstanceDiscoveryMetadataEntry> GetMetadataAsync(Uri authority, RequestContext requestContext)
		{
			ILoggerAdapter logger = requestContext.Logger;
			string environment = authority.Host;
			InstanceDiscoveryMetadataEntry cachedEntry = this._networkCacheMetadataProvider.GetMetadata(environment, logger);
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry;
			if (cachedEntry != null)
			{
				logger.Verbose(() => "[Instance Discovery] The network provider found an entry for " + environment + ". ");
				instanceDiscoveryMetadataEntry = cachedEntry;
			}
			else
			{
				InstanceDiscoveryResponse instanceDiscoveryResponse = await this.FetchAllDiscoveryMetadataAsync(authority, requestContext).ConfigureAwait(false);
				this.CacheInstanceDiscoveryMetadata(instanceDiscoveryResponse);
				cachedEntry = this._networkCacheMetadataProvider.GetMetadata(environment, logger);
				logger.Verbose(() => string.Format("[Instance Discovery] After hitting the discovery endpoint, the network provider found an entry for {0} ? {1}. ", environment, cachedEntry != null));
				instanceDiscoveryMetadataEntry = cachedEntry;
			}
			return instanceDiscoveryMetadataEntry;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005206C File Offset: 0x0005026C
		private void CacheInstanceDiscoveryMetadata(InstanceDiscoveryResponse instanceDiscoveryResponse)
		{
			IEnumerable<InstanceDiscoveryMetadataEntry> metadata = instanceDiscoveryResponse.Metadata;
			foreach (InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry in (metadata ?? Enumerable.Empty<InstanceDiscoveryMetadataEntry>()))
			{
				IEnumerable<string> aliases = instanceDiscoveryMetadataEntry.Aliases;
				foreach (string text in (aliases ?? Enumerable.Empty<string>()))
				{
					this._networkCacheMetadataProvider.AddMetadata(text, instanceDiscoveryMetadataEntry);
				}
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00052110 File Offset: 0x00050310
		private async Task<InstanceDiscoveryResponse> FetchAllDiscoveryMetadataAsync(Uri authority, RequestContext requestContext)
		{
			return await this.SendInstanceDiscoveryRequestAsync(authority, requestContext).ConfigureAwait(false);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00052164 File Offset: 0x00050364
		private async Task<InstanceDiscoveryResponse> SendInstanceDiscoveryRequestAsync(Uri authority, RequestContext requestContext)
		{
			OAuth2Client oauth2Client = new OAuth2Client(requestContext.Logger, this._httpManager);
			oauth2Client.AddQueryParameter("api-version", "1.1");
			oauth2Client.AddQueryParameter("authorization_endpoint", NetworkMetadataProvider.BuildAuthorizeEndpoint(authority));
			Uri uri = this.ComputeHttpEndpoint(authority, requestContext);
			return await oauth2Client.DiscoverAadInstanceAsync(uri, requestContext).ConfigureAwait(false);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000521B8 File Offset: 0x000503B8
		private Uri ComputeHttpEndpoint(Uri authority, RequestContext requestContext)
		{
			if (this._userProvidedInstanceDiscoveryUri != null)
			{
				return this._userProvidedInstanceDiscoveryUri;
			}
			string discoveryHost = (KnownMetadataProvider.IsKnownEnvironment(authority.Host) ? authority.Host : "login.microsoftonline.com");
			string instanceDiscoveryEndpoint = UriBuilderExtensions.GetHttpsUriWithOptionalPort("https://" + discoveryHost + "/common/discovery/instance", authority.Port);
			requestContext.Logger.InfoPii(() => string.Concat(new string[] { "Fetching instance discovery from the network from host ", discoveryHost, ". Endpoint ", instanceDiscoveryEndpoint, ". " }), () => "Fetching instance discovery from the network from host " + discoveryHost + ". ");
			return new Uri(instanceDiscoveryEndpoint);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00052254 File Offset: 0x00050454
		private static string BuildAuthorizeEndpoint(Uri authority)
		{
			return UriBuilderExtensions.GetHttpsUriWithOptionalPort(authority.Host, NetworkMetadataProvider.GetTenant(authority), "oauth2/v2.0/authorize", authority.Port);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00052272 File Offset: 0x00050472
		private static string GetTenant(Uri uri)
		{
			return uri.AbsolutePath.Split(new char[] { '/' })[1];
		}

		// Token: 0x04000B3F RID: 2879
		private readonly IHttpManager _httpManager;

		// Token: 0x04000B40 RID: 2880
		private readonly INetworkCacheMetadataProvider _networkCacheMetadataProvider;

		// Token: 0x04000B41 RID: 2881
		private readonly Uri _userProvidedInstanceDiscoveryUri;
	}
}
