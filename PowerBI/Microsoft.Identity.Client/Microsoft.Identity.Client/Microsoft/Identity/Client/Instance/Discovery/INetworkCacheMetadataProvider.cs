using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x0200027D RID: 637
	internal interface INetworkCacheMetadataProvider
	{
		// Token: 0x060018B8 RID: 6328
		void AddMetadata(string environment, InstanceDiscoveryMetadataEntry entry);

		// Token: 0x060018B9 RID: 6329
		InstanceDiscoveryMetadataEntry GetMetadata(string environment, ILoggerAdapter logger);

		// Token: 0x060018BA RID: 6330
		void Clear();
	}
}
