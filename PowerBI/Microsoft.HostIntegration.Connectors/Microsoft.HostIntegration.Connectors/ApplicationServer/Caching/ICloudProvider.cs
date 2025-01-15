using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000326 RID: 806
	internal interface ICloudProvider
	{
		// Token: 0x06001D24 RID: 7460
		string GetLogLocationForCurrentInstance();

		// Token: 0x06001D25 RID: 7461
		int GetLogLevel();

		// Token: 0x06001D26 RID: 7462
		string GetCurrentEndpointAddress();

		// Token: 0x06001D27 RID: 7463
		Uri GetCurrentInternalEndpointUri();

		// Token: 0x06001D28 RID: 7464
		string GetCurrentInstanceId();

		// Token: 0x06001D29 RID: 7465
		IEnumerable<CacheHostConfiguration> GetAllHosts();

		// Token: 0x06001D2A RID: 7466
		IEnumerable<IDomainLayoutConfiguration> GetAllDomains();

		// Token: 0x06001D2B RID: 7467
		CacheHostConfiguration GetHost(string hostName, int servicePort);

		// Token: 0x06001D2C RID: 7468
		CacheHostConfiguration GetHost(string hostName, string serviceName);

		// Token: 0x06001D2D RID: 7469
		string GetConfigurationValue(string configuration);

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001D2E RID: 7470
		// (remove) Token: 0x06001D2F RID: 7471
		event EventHandler<NodeModificationHandlerEventArgs> NodesModifiedEventHandler;
	}
}
