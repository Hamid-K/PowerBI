using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000264 RID: 612
	internal class RegionDiscoveryProvider : IRegionDiscoveryProvider
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x00050B24 File Offset: 0x0004ED24
		public RegionDiscoveryProvider(IHttpManager httpManager, bool clearCache)
		{
			this._regionManager = new RegionManager(httpManager, 2000, clearCache);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00050B40 File Offset: 0x0004ED40
		public async Task<InstanceDiscoveryMetadataEntry> GetMetadataAsync(Uri authority, RequestContext requestContext)
		{
			string text = null;
			ApiEvent apiEvent = requestContext.ApiEvent;
			if (apiEvent != null && apiEvent.ApiId == ApiEvent.ApiIds.AcquireTokenForClient)
			{
				text = await this._regionManager.GetAzureRegionAsync(requestContext).ConfigureAwait(false);
			}
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry;
			if (string.IsNullOrEmpty(text))
			{
				requestContext.Logger.Info("[Region discovery] Not using a regional authority. ");
				instanceDiscoveryMetadataEntry = null;
			}
			else if (authority.Host.StartsWith(text + "."))
			{
				instanceDiscoveryMetadataEntry = RegionDiscoveryProvider.CreateEntry(requestContext.ServiceBundle.Config.Authority.AuthorityInfo.Host, authority.Host);
			}
			else
			{
				string regionalizedEnvironment = RegionDiscoveryProvider.GetRegionalizedEnvironment(authority, text, requestContext);
				instanceDiscoveryMetadataEntry = RegionDiscoveryProvider.CreateEntry(authority.Host, regionalizedEnvironment);
			}
			return instanceDiscoveryMetadataEntry;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00050B94 File Offset: 0x0004ED94
		private static InstanceDiscoveryMetadataEntry CreateEntry(string originalEnv, string regionalEnv)
		{
			return new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { regionalEnv, originalEnv },
				PreferredCache = originalEnv,
				PreferredNetwork = regionalEnv
			};
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00050BCC File Offset: 0x0004EDCC
		private static string GetRegionalizedEnvironment(Uri authority, string region, RequestContext requestContext)
		{
			string host = authority.Host;
			if (KnownMetadataProvider.IsPublicEnvironment(host))
			{
				requestContext.Logger.Info(() => "[Region discovery] Regionalized Environment is : " + region + ".login.microsoft.com. ");
				return region + ".login.microsoft.com";
			}
			string text;
			if (KnownMetadataProvider.TryGetKnownEnviromentPreferredNetwork(host, out text))
			{
				host = text;
			}
			requestContext.Logger.Info(() => string.Concat(new string[] { "[Region discovery] Regionalized Environment is : ", region, ".", host, ". " }));
			return region + "." + host;
		}

		// Token: 0x04000AF0 RID: 2800
		private readonly IRegionManager _regionManager;

		// Token: 0x04000AF1 RID: 2801
		public const string PublicEnvForRegional = "login.microsoft.com";
	}
}
