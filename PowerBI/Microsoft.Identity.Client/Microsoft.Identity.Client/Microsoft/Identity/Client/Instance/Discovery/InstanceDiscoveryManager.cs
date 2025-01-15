using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.Region;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x0200027F RID: 639
	internal class InstanceDiscoveryManager : IInstanceDiscoveryManager
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x00051930 File Offset: 0x0004FB30
		public InstanceDiscoveryManager(IHttpManager httpManager, bool shouldClearCaches, InstanceDiscoveryResponse userProvidedInstanceDiscoveryResponse = null, Uri userProvidedInstanceDiscoveryUri = null)
			: this(httpManager, shouldClearCaches, (userProvidedInstanceDiscoveryResponse != null) ? new UserMetadataProvider(userProvidedInstanceDiscoveryResponse) : null, userProvidedInstanceDiscoveryUri, null, null, null, null)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00051958 File Offset: 0x0004FB58
		public InstanceDiscoveryManager(IHttpManager httpManager, bool shouldClearCaches, IUserMetadataProvider userMetadataProvider = null, Uri userProvidedInstanceDiscoveryUri = null, IKnownMetadataProvider knownMetadataProvider = null, INetworkCacheMetadataProvider networkCacheMetadataProvider = null, INetworkMetadataProvider networkMetadataProvider = null, IRegionDiscoveryProvider regionDiscoveryProvider = null)
		{
			if (httpManager == null)
			{
				throw new ArgumentNullException("httpManager");
			}
			this._httpManager = httpManager;
			this._userMetadataProvider = userMetadataProvider;
			this._knownMetadataProvider = knownMetadataProvider ?? new KnownMetadataProvider();
			this._networkCacheMetadataProvider = networkCacheMetadataProvider ?? new NetworkCacheMetadataProvider();
			this._networkMetadataProvider = networkMetadataProvider ?? new NetworkMetadataProvider(this._httpManager, this._networkCacheMetadataProvider, userProvidedInstanceDiscoveryUri);
			this._regionDiscoveryProvider = regionDiscoveryProvider ?? new RegionDiscoveryProvider(this._httpManager, shouldClearCaches);
			if (shouldClearCaches)
			{
				this._networkCacheMetadataProvider.Clear();
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000519F0 File Offset: 0x0004FBF0
		public async Task<InstanceDiscoveryMetadataEntry> GetMetadataEntryTryAvoidNetworkAsync(AuthorityInfo authorityInfo, IEnumerable<string> existingEnvironmentsInCache, RequestContext requestContext)
		{
			string environment = authorityInfo.Host;
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry4;
			if (authorityInfo.IsInstanceDiscoverySupported)
			{
				IUserMetadataProvider userMetadataProvider = this._userMetadataProvider;
				InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = ((userMetadataProvider != null) ? userMetadataProvider.GetMetadataOrThrow(environment, requestContext.Logger) : null);
				if (instanceDiscoveryMetadataEntry == null)
				{
					instanceDiscoveryMetadataEntry = await this._regionDiscoveryProvider.GetMetadataAsync(authorityInfo.CanonicalAuthority, requestContext).ConfigureAwait(false);
				}
				InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry2 = instanceDiscoveryMetadataEntry;
				if (instanceDiscoveryMetadataEntry2 == null && requestContext.ServiceBundle.Config.IsInstanceDiscoveryEnabled)
				{
					instanceDiscoveryMetadataEntry = this._networkCacheMetadataProvider.GetMetadata(environment, requestContext.Logger);
					if (instanceDiscoveryMetadataEntry == null)
					{
						InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry3 = this._knownMetadataProvider.GetMetadata(environment, existingEnvironmentsInCache, requestContext.Logger);
						if (instanceDiscoveryMetadataEntry3 == null)
						{
							instanceDiscoveryMetadataEntry3 = await this.GetMetadataEntryAsync(authorityInfo, requestContext, false).ConfigureAwait(false);
						}
						instanceDiscoveryMetadataEntry = instanceDiscoveryMetadataEntry3;
					}
					instanceDiscoveryMetadataEntry2 = instanceDiscoveryMetadataEntry;
				}
				if (instanceDiscoveryMetadataEntry2 == null)
				{
					requestContext.Logger.Info(() => string.Format("Skipping Instance discovery for {0} authority because it is not enabled.", authorityInfo.AuthorityType));
					instanceDiscoveryMetadataEntry2 = InstanceDiscoveryManager.CreateEntryForSingleAuthority(authorityInfo.CanonicalAuthority);
				}
				instanceDiscoveryMetadataEntry4 = instanceDiscoveryMetadataEntry2;
			}
			else
			{
				requestContext.Logger.Info(() => string.Format("Skipping Instance discovery for {0} authority because it is not supported.", authorityInfo.AuthorityType));
				instanceDiscoveryMetadataEntry4 = await this.GetMetadataEntryAsync(authorityInfo, requestContext, false).ConfigureAwait(false);
			}
			return instanceDiscoveryMetadataEntry4;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00051A4C File Offset: 0x0004FC4C
		public async Task<InstanceDiscoveryMetadataEntry> GetMetadataEntryAsync(AuthorityInfo authorityInfo, RequestContext requestContext, bool forceValidation = false)
		{
			Uri authorityUri = authorityInfo.CanonicalAuthority;
			string environment = authorityInfo.Host;
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry2;
			if (authorityInfo.IsInstanceDiscoverySupported)
			{
				IUserMetadataProvider userMetadataProvider = this._userMetadataProvider;
				InstanceDiscoveryMetadataEntry entry = ((userMetadataProvider != null) ? userMetadataProvider.GetMetadataOrThrow(environment, requestContext.Logger) : null);
				if (entry == null && !requestContext.ServiceBundle.Config.IsInstanceDiscoveryEnabled)
				{
					InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this._regionDiscoveryProvider.GetMetadataAsync(authorityUri, requestContext).ConfigureAwait(false);
					entry = instanceDiscoveryMetadataEntry;
					if (entry == null)
					{
						requestContext.Logger.Info("[Instance Discovery] Skipping Instance discovery because it is disabled. ");
						return InstanceDiscoveryManager.CreateEntryForSingleAuthority(authorityUri);
					}
				}
				if (entry == null && forceValidation)
				{
					await this.FetchNetworkMetadataOrFallbackAsync(requestContext, authorityUri).ConfigureAwait(false);
				}
				requestContext.Logger.Info("[Instance Discovery] Instance discovery is enabled and will be performed");
				if (entry == null)
				{
					InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = await this._regionDiscoveryProvider.GetMetadataAsync(authorityUri, requestContext).ConfigureAwait(false);
					if (instanceDiscoveryMetadataEntry == null)
					{
						instanceDiscoveryMetadataEntry = await this.FetchNetworkMetadataOrFallbackAsync(requestContext, authorityUri).ConfigureAwait(false);
					}
					entry = instanceDiscoveryMetadataEntry;
				}
				if (entry == null)
				{
					string text = "[Instance Discovery] Instance metadata for this authority could neither be fetched nor found. MSAL will continue regardless. SSO might be broken if authority aliases exist. ";
					ILoggerAdapter logger = requestContext.Logger;
					string text2 = "Authority: ";
					Uri canonicalAuthority = authorityInfo.CanonicalAuthority;
					logger.WarningPii(text + text2 + ((canonicalAuthority != null) ? canonicalAuthority.ToString() : null), text);
					entry = InstanceDiscoveryManager.CreateEntryForSingleAuthority(authorityUri);
					this._networkCacheMetadataProvider.AddMetadata(environment, entry);
				}
				instanceDiscoveryMetadataEntry2 = entry;
			}
			else
			{
				requestContext.Logger.Info("[Instance Discovery] Skipping Instance discovery for non-AAD authority. ");
				instanceDiscoveryMetadataEntry2 = InstanceDiscoveryManager.CreateEntryForSingleAuthority(authorityUri);
			}
			return instanceDiscoveryMetadataEntry2;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00051AA8 File Offset: 0x0004FCA8
		private async Task<InstanceDiscoveryMetadataEntry> FetchNetworkMetadataOrFallbackAsync(RequestContext requestContext, Uri authorityUri)
		{
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry;
			try
			{
				instanceDiscoveryMetadataEntry = await this._networkMetadataProvider.GetMetadataAsync(authorityUri, requestContext).ConfigureAwait(false);
			}
			catch (MsalServiceException ex)
			{
				if (!requestContext.ServiceBundle.Config.Authority.AuthorityInfo.ValidateAuthority)
				{
					requestContext.Logger.Info("[Instance Discovery] Skipping Instance discovery as validate authority is set to false. ");
					instanceDiscoveryMetadataEntry = InstanceDiscoveryManager.CreateEntryForSingleAuthority(authorityUri);
				}
				else
				{
					if (ex.ErrorCode == "invalid_instance")
					{
						requestContext.Logger.Error("[Instance Discovery] Instance discovery failed - invalid instance!");
						throw;
					}
					string text = "[Instance Discovery] Instance Discovery failed. Potential cause: no network connection or discovery endpoint is busy. See exception below. MSAL will continue without network instance metadata. ";
					requestContext.Logger.WarningPii(text + " Authority: " + ((authorityUri != null) ? authorityUri.ToString() : null), text);
					requestContext.Logger.WarningPii(ex);
					instanceDiscoveryMetadataEntry = this._knownMetadataProvider.GetMetadata(authorityUri.Host, Enumerable.Empty<string>(), requestContext.Logger);
				}
			}
			return instanceDiscoveryMetadataEntry;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00051AFB File Offset: 0x0004FCFB
		internal void AddTestValueToStaticProvider(string environment, InstanceDiscoveryMetadataEntry entry)
		{
			this._networkCacheMetadataProvider.AddMetadata(environment, entry);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00051B0C File Offset: 0x0004FD0C
		private static InstanceDiscoveryMetadataEntry CreateEntryForSingleAuthority(Uri authority)
		{
			return new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { authority.Host },
				PreferredCache = authority.Host,
				PreferredNetwork = authority.Host
			};
		}

		// Token: 0x04000B30 RID: 2864
		private readonly IHttpManager _httpManager;

		// Token: 0x04000B31 RID: 2865
		private readonly IUserMetadataProvider _userMetadataProvider;

		// Token: 0x04000B32 RID: 2866
		private readonly IKnownMetadataProvider _knownMetadataProvider;

		// Token: 0x04000B33 RID: 2867
		private readonly INetworkCacheMetadataProvider _networkCacheMetadataProvider;

		// Token: 0x04000B34 RID: 2868
		private readonly INetworkMetadataProvider _networkMetadataProvider;

		// Token: 0x04000B35 RID: 2869
		private readonly IRegionDiscoveryProvider _regionDiscoveryProvider;
	}
}
