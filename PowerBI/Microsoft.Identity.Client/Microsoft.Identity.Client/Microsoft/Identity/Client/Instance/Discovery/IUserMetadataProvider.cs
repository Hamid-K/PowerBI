using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000282 RID: 642
	internal interface IUserMetadataProvider
	{
		// Token: 0x060018CF RID: 6351
		InstanceDiscoveryMetadataEntry GetMetadataOrThrow(string environment, ILoggerAdapter logger);
	}
}
