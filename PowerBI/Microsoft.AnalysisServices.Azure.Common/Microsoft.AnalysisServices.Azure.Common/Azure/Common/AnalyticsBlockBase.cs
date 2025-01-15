using System;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.Common;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.FabricClient;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Modularization;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Routers;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000034 RID: 52
	public abstract class AnalyticsBlockBase : MonitoredBlock
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000E603 File Offset: 0x0000C803
		protected AnalyticsBlockBase(string name)
			: base(name)
		{
			this.configurationLock = new object();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E618 File Offset: 0x0000C818
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.WindowsFabricRouterDependencies = new WindowsFabricRouterDependencies(this.configurationManagerFactory, this.fabricClientFactory, this.systemModel, this.nodeInfoProvider, base.EventsKitFactory);
			this.configurationManager = this.ConfigurationManagerFactory.GetConfigurationManager();
			this.configurationManager.Subscribe(new Type[] { typeof(AnalyticsBlockBaseConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			return BlockInitializationStatus.Done;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E698 File Offset: 0x0000C898
		protected override void OnStop()
		{
			base.OnStop();
			this.configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			if (this.ShouldDrainWorkTicket && (this.DrainWorkTicketTimeout != null || this.blockBaseConfiguration.DefaultDrainWorkTicketTimeout.TotalMilliseconds > 0.0))
			{
				base.WorkTicketManager.NonFinalStop();
				base.WorkTicketManager.WaitForStopToCompleteOrCrash((int)((this.DrainWorkTicketTimeout != null) ? this.DrainWorkTicketTimeout.Value : this.blockBaseConfiguration.DefaultDrainWorkTicketTimeout).TotalMilliseconds, null);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E744 File Offset: 0x0000C944
		protected virtual TimeSpan? DrainWorkTicketTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A3EB File Offset: 0x000085EB
		protected virtual bool ShouldDrainWorkTicket
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E75A File Offset: 0x0000C95A
		protected IBIAzureServiceModel ServiceModel
		{
			get
			{
				return this.serviceModel;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000E762 File Offset: 0x0000C962
		protected ISystemModel SystemModel
		{
			get
			{
				return this.systemModel;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E76A File Offset: 0x0000C96A
		protected INodeInformationProvider NodeInfoProvider
		{
			get
			{
				return this.nodeInfoProvider;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000E772 File Offset: 0x0000C972
		protected IWindowsFabricRuntime WindowsFabricRuntime
		{
			get
			{
				return this.m_windowsFabricRuntime;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E77A File Offset: 0x0000C97A
		protected IFabricClientFactory FabricClientFactory
		{
			get
			{
				return this.fabricClientFactory;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000E782 File Offset: 0x0000C982
		protected ICommunicationServices CommunicationServices
		{
			get
			{
				return this.communicationServices;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E78A File Offset: 0x0000C98A
		protected IConfigurationManagerFactory ConfigurationManagerFactory
		{
			get
			{
				return this.configurationManagerFactory;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000E792 File Offset: 0x0000C992
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000E79A File Offset: 0x0000C99A
		private protected WindowsFabricRouterDependencies WindowsFabricRouterDependencies { protected get; private set; }

		// Token: 0x0600034A RID: 842 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		protected SyncActivity NewSyncActivity(ActivityType activityType)
		{
			Guid guid = UtilsContext.Current.Activity.RootActivityId;
			if (guid == Guid.Empty)
			{
				guid = Guid.NewGuid();
			}
			return base.ActivityFactory.CreateSyncActivity(Guid.NewGuid(), activityType, guid, UtilsContext.Current.Activity.ClientActivityId);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		private void OnConfigurationChange(IConfigurationContainer configContainer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IConfigurationContainer>(configContainer, "configurationContainer");
			object obj = this.configurationLock;
			lock (obj)
			{
				this.blockBaseConfiguration = configContainer.GetConfiguration<AnalyticsBlockBaseConfiguration>();
				AnalyticsBlockBase.Trace.TraceInformation("OnConfigurationChange is called for block : '{0}'. Updated Configuration : '{1}'.".FormatWithInvariantCulture(new object[]
				{
					base.GetType().FullName,
					this.blockBaseConfiguration.ToString()
				}));
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000E880 File Offset: 0x0000CA80
		private static ANCommonTrace Trace
		{
			get
			{
				return TraceSourceBase<ANCommonTrace>.Tracer;
			}
		}

		// Token: 0x0400009D RID: 157
		private readonly object configurationLock;

		// Token: 0x0400009E RID: 158
		private IConfigurationManager configurationManager;

		// Token: 0x0400009F RID: 159
		private AnalyticsBlockBaseConfiguration blockBaseConfiguration;

		// Token: 0x040000A0 RID: 160
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory configurationManagerFactory;

		// Token: 0x040000A1 RID: 161
		[BlockServiceDependency]
		private readonly IBIAzureServiceModel serviceModel;

		// Token: 0x040000A2 RID: 162
		[BlockServiceDependency]
		private readonly ISystemModel systemModel;

		// Token: 0x040000A3 RID: 163
		[BlockServiceDependency]
		private readonly INodeInformationProvider nodeInfoProvider;

		// Token: 0x040000A4 RID: 164
		[BlockServiceDependency]
		private readonly IFabricClientFactory fabricClientFactory;

		// Token: 0x040000A5 RID: 165
		[BlockServiceDependency]
		private readonly ICommunicationServices communicationServices;

		// Token: 0x040000A6 RID: 166
		[BlockServiceDependency]
		private readonly IWindowsFabricRuntime m_windowsFabricRuntime;
	}
}
