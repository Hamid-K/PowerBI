using System;
using System.Text;
using Microsoft.AnalysisServices.Azure.Gateway;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.CloudBI.Routers;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x0200014C RID: 332
	internal sealed class ExploreToDataExternalRouterBuilder : IRouterBuilder
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x00047C03 File Offset: 0x00045E03
		public ExploreToDataExternalRouterBuilder(IEventsKitFactory eventsKitFactory, IClusterManagementService clusterManagementService, BIAzure.ServiceType serviceType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IClusterManagementService>(clusterManagementService, "clusterManagementService");
			this.clusterManagementService = clusterManagementService;
			this.eventsKitFactory = eventsKitFactory;
			this.serviceType = serviceType;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00047C38 File Offset: 0x00045E38
		public Router Build(BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint)
		{
			TraceSourceBase<ANCommonTrace>.Tracer.TraceVerbose(new StringBuilder().Append('[').Append(base.GetType().Name).Append(']')
				.Append(' ')
				.Append("Build:")
				.Append(' ')
				.Append("serviceType=")
				.Append(this.serviceType)
				.Append("; ")
				.Append("resolvedEndoint=")
				.Append(resolvedEndoint)
				.Append("; ")
				.Append("targetEndpoint=")
				.Append(targetEndpoint)
				.Append("; ")
				.ToString());
			return new ExploreToDataExternalRouter(this.eventsKitFactory, this.clusterManagementService, this.serviceType, resolvedEndoint, targetEndpoint);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public Router BuildAffinitizedRouter(BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint, IPreferredNodeSelector preferredNodeSelector, INodeSorter nodeSorter)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000410 RID: 1040
		private readonly IEventsKitFactory eventsKitFactory;

		// Token: 0x04000411 RID: 1041
		private readonly IClusterManagementService clusterManagementService;

		// Token: 0x04000412 RID: 1042
		private readonly BIAzure.ServiceType serviceType;
	}
}
