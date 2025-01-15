using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011F RID: 287
	internal interface IClusterHostConfigurationReader
	{
		// Token: 0x06000849 RID: 2121
		void Open(string readerParams, long cacheUserDataSizePerNode);

		// Token: 0x0600084A RID: 2122
		void Close();

		// Token: 0x0600084B RID: 2123
		List<IHostConfiguration> GetListOfHosts();

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600084C RID: 2124
		List<IDomainLayoutConfiguration> DomainLayout { get; }

		// Token: 0x0600084D RID: 2125
		IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName);

		// Token: 0x0600084E RID: 2126
		List<IHostConfiguration> GetListOfNodes(bool seeds);

		// Token: 0x0600084F RID: 2127
		bool TestConnection();
	}
}
