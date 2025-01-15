using System;
using System.Collections.Concurrent;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000284 RID: 644
	internal class NetworkCacheMetadataProvider : INetworkCacheMetadataProvider
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x00051F38 File Offset: 0x00050138
		public InstanceDiscoveryMetadataEntry GetMetadata(string environment, ILoggerAdapter logger)
		{
			InstanceDiscoveryMetadataEntry entry;
			NetworkCacheMetadataProvider.s_cache.TryGetValue(environment, out entry);
			logger.Verbose(() => string.Format("[Instance Discovery] Tried to use network cache provider for {0}. Success? {1}. ", environment, entry != null));
			return entry;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00051F84 File Offset: 0x00050184
		public void AddMetadata(string environment, InstanceDiscoveryMetadataEntry entry)
		{
			NetworkCacheMetadataProvider.s_cache.AddOrUpdate(environment, entry, (string _, InstanceDiscoveryMetadataEntry _) => entry);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00051FBC File Offset: 0x000501BC
		public void Clear()
		{
			NetworkCacheMetadataProvider.s_cache.Clear();
		}

		// Token: 0x04000B3E RID: 2878
		private static readonly ConcurrentDictionary<string, InstanceDiscoveryMetadataEntry> s_cache = new ConcurrentDictionary<string, InstanceDiscoveryMetadataEntry>();
	}
}
