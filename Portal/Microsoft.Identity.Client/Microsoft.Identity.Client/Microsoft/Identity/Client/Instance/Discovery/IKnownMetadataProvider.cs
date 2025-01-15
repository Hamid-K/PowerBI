using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x0200027C RID: 636
	internal interface IKnownMetadataProvider
	{
		// Token: 0x060018B7 RID: 6327
		InstanceDiscoveryMetadataEntry GetMetadata(string environment, IEnumerable<string> existingEnvironmentsInCache, ILoggerAdapter logger);
	}
}
