using System;
using Microsoft.DataShaping.Engine;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreHost;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000017 RID: 23
	internal class RSExploreClientFactory : IExploreClientFactory
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003199 File Offset: 0x00001399
		public IExploreClient Create(IPowerViewHandler powerViewHandler, IFeatureSwitchesProxy featureSwitchesProxy, bool initRsConfig)
		{
			return new ExploreClient(powerViewHandler, featureSwitchesProxy, NoOpQueryCancellationManager.Instance);
		}

		// Token: 0x04000056 RID: 86
		internal static readonly RSExploreClientFactory Instance = new RSExploreClientFactory();
	}
}
