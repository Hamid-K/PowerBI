using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000286 RID: 646
	internal class UserMetadataProvider : IUserMetadataProvider
	{
		// Token: 0x060018E6 RID: 6374 RVA: 0x0005228C File Offset: 0x0005048C
		public UserMetadataProvider(InstanceDiscoveryResponse instanceDiscoveryResponse)
		{
			IEnumerable<InstanceDiscoveryMetadataEntry> enumerable = ((instanceDiscoveryResponse != null) ? instanceDiscoveryResponse.Metadata : null);
			foreach (InstanceDiscoveryMetadataEntry instanceDiscoveryMetadataEntry in (enumerable ?? Enumerable.Empty<InstanceDiscoveryMetadataEntry>()))
			{
				IEnumerable<string> aliases = instanceDiscoveryMetadataEntry.Aliases;
				foreach (string text in (aliases ?? Enumerable.Empty<string>()))
				{
					this._entries.Add(text, instanceDiscoveryMetadataEntry);
				}
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00052348 File Offset: 0x00050548
		public InstanceDiscoveryMetadataEntry GetMetadataOrThrow(string environment, ILoggerAdapter logger)
		{
			InstanceDiscoveryMetadataEntry entry;
			this._entries.TryGetValue(environment ?? "", out entry);
			logger.Verbose(() => string.Format("[Instance Discovery] Tried to use user metadata provider for {0}. Success? {1}. ", environment, entry != null));
			if (entry == null)
			{
				throw new MsalClientException("invalid-custom-instance-metadata", MsalErrorMessage.NoUserInstanceMetadataEntry(environment));
			}
			return entry;
		}

		// Token: 0x04000B42 RID: 2882
		private readonly IDictionary<string, InstanceDiscoveryMetadataEntry> _entries = new Dictionary<string, InstanceDiscoveryMetadataEntry>();
	}
}
