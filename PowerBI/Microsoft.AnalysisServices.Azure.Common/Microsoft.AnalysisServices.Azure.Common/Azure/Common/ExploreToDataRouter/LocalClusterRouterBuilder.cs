using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Routers;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.CloudBI.Routers;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x02000150 RID: 336
	internal sealed class LocalClusterRouterBuilder<T> : IRouterBuilder where T : WindowsFabricServiceRouter
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x000482FA File Offset: 0x000464FA
		public LocalClusterRouterBuilder(WindowsFabricRouterDependencies windowsFabricRouterDependencies, IBIAzureServiceModel serviceModel, BIAzure.ServiceType serviceType)
		{
			this.windowsFabricRouterDependencies = windowsFabricRouterDependencies;
			this.serviceModel = serviceModel;
			this.serviceType = serviceType;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00048318 File Offset: 0x00046518
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
			return (T)((object)Activator.CreateInstance(typeof(T), new object[]
			{
				this.serviceModel.GetService(this.serviceType),
				resolvedEndoint,
				targetEndpoint,
				this.windowsFabricRouterDependencies
			}));
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00048420 File Offset: 0x00046620
		public Router BuildAffinitizedRouter(BIAzure.EndpointType resolvedEndpoint, BIAzure.EndpointType targetEndpoint, IPreferredNodeSelector preferredNodeSelector, INodeSorter nodeSorter)
		{
			LocalClusterRouterBuilder<T>.affinitySupportedRouterTypes.Contains(typeof(T));
			TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("[" + base.GetType().Name + "] BuildAffinitizedRouter: " + string.Format("serviceType={0}; resolvedEndoint={1}; targetEndpoint={2};", this.serviceType, resolvedEndpoint, targetEndpoint));
			return (T)((object)Activator.CreateInstance(typeof(T), new object[]
			{
				this.serviceModel.GetService(this.serviceType),
				resolvedEndpoint,
				targetEndpoint,
				this.windowsFabricRouterDependencies,
				preferredNodeSelector,
				nodeSorter
			}));
		}

		// Token: 0x0400041C RID: 1052
		private static readonly List<Type> affinitySupportedRouterTypes = new List<Type>
		{
			typeof(PreferOneDynamicNodeRouter),
			typeof(PreferOneNodeRouter)
		};

		// Token: 0x0400041D RID: 1053
		private readonly WindowsFabricRouterDependencies windowsFabricRouterDependencies;

		// Token: 0x0400041E RID: 1054
		private readonly IBIAzureServiceModel serviceModel;

		// Token: 0x0400041F RID: 1055
		private readonly BIAzure.ServiceType serviceType;
	}
}
