using System;
using System.Linq;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.Configuration;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.CloudBI.ConfigurationCommon;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000039 RID: 57
	public sealed class AnalyticsConfigurationManagerFactory : ConfigurationManagerFactoryBase
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public AnalyticsConfigurationManagerFactory()
			: base(typeof(AnalyticsConfigurationManagerFactory).Name)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		protected override IConfigurationManager CreateConfigurationManager(string specification)
		{
			BIAzure.ConfigPackage configPackage = this.serviceModel.GetAllServices().SelectMany((BIAzure.Service s) => s.ConfigPackages).Single((BIAzure.ConfigPackage p) => p.FolderName.Equals(specification, StringComparison.OrdinalIgnoreCase));
			return new AnalyticsConfigurationManagerFactory.Manager(specification, configPackage, this, this.fabricRuntime, this.serviceModel.Application.Path, new GetVersionedConfigurationTableUriFunc(BIAzureConfigurationManagerUtils.GetVersionedConfigurationTableUriForNodeNumber));
		}

		// Token: 0x040000A9 RID: 169
		[BlockServiceDependency]
		private readonly IWindowsFabricRuntime fabricRuntime;

		// Token: 0x040000AA RID: 170
		[BlockServiceDependency]
		private readonly IBIAzureServiceModel serviceModel;

		// Token: 0x02000161 RID: 353
		private sealed class Manager : ConfigurationManagerBase
		{
			// Token: 0x0600122F RID: 4655 RVA: 0x00049D1C File Offset: 0x00047F1C
			public Manager(string specification, BIAzure.ConfigPackage package, IConfigurationManagerHost host, IWindowsFabricRuntime windowsFabricRuntime, string applicationPath, GetVersionedConfigurationTableUriFunc getNameUri)
				: base(host, new IConfigurationProvider[]
				{
					new WindowsFabricConfigurationProvider(specification, windowsFabricRuntime)
				})
			{
			}
		}
	}
}
