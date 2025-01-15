using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000097 RID: 151
	// (Invoke) Token: 0x0600035C RID: 860
	internal delegate DataCache CreateNewCacheDelegate(string name, IClientProtocol protocolToUse, DataCacheFactory parentFactory, NamedCacheConfiguration cacheConfiguration, ClientOperationsSupportProvider supportProvider);
}
