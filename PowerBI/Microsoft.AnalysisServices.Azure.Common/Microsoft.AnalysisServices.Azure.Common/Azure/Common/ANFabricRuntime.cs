using System;
using System.Fabric;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000080 RID: 128
	public sealed class ANFabricRuntime : IFabricRuntime
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x00010A6E File Offset: 0x0000EC6E
		public ANFabricRuntime(IWindowsFabricRuntime fabricRuntime, IBIAzureServiceModel serviceModel)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IWindowsFabricRuntime>(fabricRuntime, "Fabric Run time");
			ExtendedDiagnostics.EnsureArgumentNotNull<IBIAzureServiceModel>(serviceModel, "serviceModel");
			this.fabricRuntime = fabricRuntime;
			this.serviceModel = serviceModel;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00010A9A File Offset: 0x0000EC9A
		public void RegisterStatefulServiceFactory(string serviceTypeName, IStatefulServiceFactory statefulServiceFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(serviceTypeName, "Service Type");
			ExtendedDiagnostics.EnsureNotNull<IStatefulServiceFactory>(statefulServiceFactory, "Stateful Service Factory");
			this.fabricRuntime.RegisterStatefulServiceFactory(serviceTypeName, new StatefulServiceFactory(new ServiceFactoryInterfaceToDelegate(statefulServiceFactory).StatefulServiceFactory));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00010ACF File Offset: 0x0000ECCF
		public void RegisterStatelessServiceFactory(string serviceTypeName, IStatelessServiceFactory statelessServiceFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(serviceTypeName, "Service Type");
			ExtendedDiagnostics.EnsureNotNull<IStatelessServiceFactory>(statelessServiceFactory, "stateless service factory");
			this.fabricRuntime.RegisterStatelessServiceFactory(serviceTypeName, new StatelessServiceFactory(new ServiceFactoryInterfaceToDelegate(statelessServiceFactory).StatelessServiceFactory));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00010B04 File Offset: 0x0000ED04
		public string GetCodePath(string serviceType)
		{
			return FabricRuntime.GetActivationContext().GetCodePackageObject(this.serviceModel.GetCodePackage(serviceType).FolderName).Path;
		}

		// Token: 0x040001F9 RID: 505
		private IWindowsFabricRuntime fabricRuntime;

		// Token: 0x040001FA RID: 506
		private IBIAzureServiceModel serviceModel;
	}
}
