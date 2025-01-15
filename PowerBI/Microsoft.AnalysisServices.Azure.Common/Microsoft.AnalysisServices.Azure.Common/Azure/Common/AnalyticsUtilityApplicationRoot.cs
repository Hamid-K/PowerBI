using System;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.Eventing;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Cloud.Platform.Eventing.Implementation;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Security;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000036 RID: 54
	public abstract class AnalyticsUtilityApplicationRoot<T> : UtilityApplicationRoot<T> where T : UtilityBlock, new()
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			base.AddBlock(new AnalyticsConfigurationManagerFactory());
			base.AddBlock(new WindowsFabricRuntimeBlock());
			base.AddBlock(new FabricClientFactory());
			base.AddBlock(new BIAzureSystemModel());
			base.AddBlock(new ElementInstanceIdProvider());
			base.AddBlock(new PackageManager());
			base.AddBlock(new EventingServer());
			base.AddBlock(new EtwSessionsManager());
			base.AddBlock(new AzureEventingDirectoriesManager());
			base.AddBlock(new EventsKitFactory());
			base.AddBlock(new EventsKitExplorerFactory());
			base.AddBlock(new OnBehalfOfEventService());
			base.AddBlock(new MonitoredActivityCompletionModelFactory());
			base.AddBlock(new SecretManager());
			base.AddBlock(new MonitoredTaskScheduler());
			base.AddBlock(new BIAzureServiceModel());
			base.AddBlock(new BIAzureNodeInformationProvider());
		}
	}
}
