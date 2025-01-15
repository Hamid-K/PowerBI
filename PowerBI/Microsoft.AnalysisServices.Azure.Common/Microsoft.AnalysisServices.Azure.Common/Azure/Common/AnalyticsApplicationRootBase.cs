using System;
using System.Collections.Generic;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Application;
using Microsoft.Cloud.Platform.Azure.Common;
using Microsoft.Cloud.Platform.Azure.Eventing;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.CloudBI.ServiceCommon;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000033 RID: 51
	public abstract class AnalyticsApplicationRootBase : ElementApplicationRoot
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000E53F File Offset: 0x0000C73F
		protected AnalyticsApplicationRootBase(string name)
			: base(name)
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E548 File Offset: 0x0000C748
		protected ISystemModel SystemModel
		{
			get
			{
				return this.systemModel;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E550 File Offset: 0x0000C750
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.systemModel = new BIAzureSystemModel();
			base.AddBlock(new ServiceDiagnosticAgentBlock());
			base.AddBlock(new AnalyticsConfigurationManagerFactory());
			base.AddBlock(new WindowsFabricRuntimeBlock());
			base.AddBlock(new FabricClientFactory());
			base.AddBlock(new DataContractEntitySerializer());
			base.AddBlock(new BIAzureServiceModel());
			base.AddBlock(new BIAzureNodeInformationProvider());
			base.AddBlock((IBlock)this.systemModel);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		protected override IEnumerable<IBlock> InterceptAddBlock(IBlock blockToAdd)
		{
			if (blockToAdd is LocalDirectoriesManager)
			{
				return new IBlock[]
				{
					new AzureEventingDirectoriesManager()
				};
			}
			if (blockToAdd is ElementInstanceIdProvider)
			{
				return new IBlock[]
				{
					new AzureElementInstanceIdProvider()
				};
			}
			return base.InterceptAddBlock(blockToAdd);
		}

		// Token: 0x0400009C RID: 156
		private ISystemModel systemModel;
	}
}
