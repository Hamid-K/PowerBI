using System;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000081 RID: 129
	[BlockServiceProvider(typeof(IFabricRuntimeFactory))]
	public sealed class ANFabricRuntimeFactory : Block, IFabricRuntimeFactory
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00010B26 File Offset: 0x0000ED26
		public ANFabricRuntimeFactory()
			: this(typeof(ANFabricRuntimeFactory).Name)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00010B3D File Offset: 0x0000ED3D
		public ANFabricRuntimeFactory(string name)
			: base(name)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00010B46 File Offset: 0x0000ED46
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00010B54 File Offset: 0x0000ED54
		public IFabricRuntime Create()
		{
			return new ANFabricRuntime(this.fabricRuntime, this.serviceModel);
		}

		// Token: 0x040001FB RID: 507
		[BlockServiceDependency]
		private readonly IWindowsFabricRuntime fabricRuntime;

		// Token: 0x040001FC RID: 508
		[BlockServiceDependency]
		private readonly IBIAzureServiceModel serviceModel;
	}
}
