using System;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.CloudBI.Routers;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x0200014E RID: 334
	public interface IExploreToDataRouterFactory
	{
		// Token: 0x0600119D RID: 4509
		Router GetRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType);

		// Token: 0x0600119E RID: 4510
		Router GetRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint);

		// Token: 0x0600119F RID: 4511
		Router GetAffinitizedRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType);

		// Token: 0x060011A0 RID: 4512
		Router GetAffinitizedRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType, IPreferredNodeSelector preferredNodeSelector, INodeSorter nodeSorter);

		// Token: 0x060011A1 RID: 4513
		bool IsDataServiceClusterIsolationEnabled();
	}
}
