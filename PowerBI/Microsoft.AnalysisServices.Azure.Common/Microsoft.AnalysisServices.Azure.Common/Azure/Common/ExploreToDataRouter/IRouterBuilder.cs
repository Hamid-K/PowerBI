using System;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.CloudBI.Routers;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x0200014F RID: 335
	internal interface IRouterBuilder
	{
		// Token: 0x060011A2 RID: 4514
		Router Build(BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint);

		// Token: 0x060011A3 RID: 4515
		Router BuildAffinitizedRouter(BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint, IPreferredNodeSelector preferredNodeSelector, INodeSorter nodeSorter);
	}
}
