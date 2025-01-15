using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000010 RID: 16
	internal interface IExploreClientFactory
	{
		// Token: 0x0600006A RID: 106
		IExploreClient Create(IPowerViewHandler powerViewHandler, IFeatureSwitchesProxy featureSwitchesProxy, bool initRsConfig);
	}
}
