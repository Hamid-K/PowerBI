using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000283 RID: 643
	internal class KnownMetadataProvider : IKnownMetadataProvider
	{
		// Token: 0x060018D0 RID: 6352 RVA: 0x00051BB4 File Offset: 0x0004FDB4
		static KnownMetadataProvider()
		{
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login.microsoftonline.com", "login.windows.net", "login.microsoft.com", "sts.windows.net" },
				PreferredNetwork = "login.microsoftonline.com",
				PreferredCache = "login.windows.net"
			};
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry2 = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login.partner.microsoftonline.cn", "login.chinacloudapi.cn" },
				PreferredNetwork = "login.partner.microsoftonline.cn",
				PreferredCache = "login.partner.microsoftonline.cn"
			};
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry3 = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login.microsoftonline.de" },
				PreferredNetwork = "login.microsoftonline.de",
				PreferredCache = "login.microsoftonline.de"
			};
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry4 = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login.microsoftonline.us", "login.usgovcloudapi.net" },
				PreferredNetwork = "login.microsoftonline.us",
				PreferredCache = "login.microsoftonline.us"
			};
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry5 = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login-us.microsoftonline.com" },
				PreferredNetwork = "login-us.microsoftonline.com",
				PreferredCache = "login-us.microsoftonline.com"
			};
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry6 = new InstanceDiscoveryMetadataEntry
			{
				Aliases = new string[] { "login.windows-ppe.net", "sts.windows-ppe.net", "login.microsoft-ppe.com" },
				PreferredNetwork = "login.windows-ppe.net",
				PreferredCache = "login.windows-ppe.net"
			};
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry);
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry2);
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry3);
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry4);
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry5);
			KnownMetadataProvider.<.cctor>g__AddToKnownCache|3_0(instanceDiscoveryMetadataEntry6);
			KnownMetadataProvider.<.cctor>g__AddToPublicEnvironment|3_1(instanceDiscoveryMetadataEntry);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00051D95 File Offset: 0x0004FF95
		public static bool IsPublicEnvironment(string environment)
		{
			return KnownMetadataProvider.s_knownPublicEnvironments.Contains(environment);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		public InstanceDiscoveryMetadataEntry GetMetadata(string environment, IEnumerable<string> existingEnvironmentsInCache, ILoggerAdapter logger)
		{
			if (existingEnvironmentsInCache == null)
			{
				existingEnvironmentsInCache = Enumerable.Empty<string>();
			}
			if (existingEnvironmentsInCache.All((string e) => KnownMetadataProvider.s_knownEnvironments.ContainsOrdinalIgnoreCase(e)))
			{
				InstanceDiscoveryMetadataEntry entry;
				KnownMetadataProvider.s_knownEntries.TryGetValue(environment, out entry);
				logger.Verbose(() => string.Format("[Instance Discovery] Tried to use known metadata provider for {0}. Success? {1}. ", environment, entry != null));
				return entry;
			}
			logger.VerbosePii(() => "[Instance Discovery] Could not use known metadata provider because at least one environment in the cache is not known. Environments in cache: " + string.Join(" ", existingEnvironmentsInCache) + " ", () => "[Instance Discovery] Could not use known metadata provider because at least one environment in the cache is not known. ");
			return null;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00051E78 File Offset: 0x00050078
		public static bool IsKnownEnvironment(string environment)
		{
			return KnownMetadataProvider.s_knownEnvironments.Contains(environment);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00051E88 File Offset: 0x00050088
		public static bool TryGetKnownEnviromentPreferredNetwork(string environment, out string preferredNetworkEnvironment)
		{
			InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry;
			if (KnownMetadataProvider.s_knownEntries.TryGetValue(environment, out instanceDiscoveryMetadataEntry))
			{
				preferredNetworkEnvironment = instanceDiscoveryMetadataEntry.PreferredNetwork;
				return true;
			}
			preferredNetworkEnvironment = null;
			return false;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00051EB2 File Offset: 0x000500B2
		public static IDictionary<string, InstanceDiscoveryMetadataEntry> GetAllEntriesForTest()
		{
			return KnownMetadataProvider.s_knownEntries;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00051EC4 File Offset: 0x000500C4
		[CompilerGenerated]
		internal static void <.cctor>g__AddToKnownCache|3_0(InstanceDiscoveryMetadataEntry entry)
		{
			foreach (string text in entry.Aliases)
			{
				KnownMetadataProvider.s_knownEntries[text] = entry;
				KnownMetadataProvider.s_knownEnvironments.Add(text);
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00051F04 File Offset: 0x00050104
		[CompilerGenerated]
		internal static void <.cctor>g__AddToPublicEnvironment|3_1(InstanceDiscoveryMetadataEntry entry)
		{
			foreach (string text in entry.Aliases)
			{
				KnownMetadataProvider.s_knownPublicEnvironments.Add(text);
			}
		}

		// Token: 0x04000B3B RID: 2875
		private static readonly IDictionary<string, InstanceDiscoveryMetadataEntry> s_knownEntries = new Dictionary<string, InstanceDiscoveryMetadataEntry>();

		// Token: 0x04000B3C RID: 2876
		private static readonly ISet<string> s_knownEnvironments = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000B3D RID: 2877
		private static readonly ISet<string> s_knownPublicEnvironments = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
	}
}
