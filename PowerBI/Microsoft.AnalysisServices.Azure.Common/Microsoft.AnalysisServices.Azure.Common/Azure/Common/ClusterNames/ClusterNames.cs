using System;
using Microsoft.Cloud.Platform.Azure.Dns;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.CloudBI.ConfigurationCommon;

namespace Microsoft.AnalysisServices.Azure.Common.ClusterNames
{
	// Token: 0x0200015A RID: 346
	[BlockServiceProvider(typeof(IClusterNames))]
	public sealed class ClusterNames : AnalyticsBlockBase, IClusterNames
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004968C File Offset: 0x0004788C
		public ClusterNames()
			: base(typeof(ClusterNames).Name)
		{
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000496A4 File Offset: 0x000478A4
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus == BlockInitializationStatus.Done)
			{
				this.m_configurationManager = base.ConfigurationManagerFactory.GetConfigurationManager();
				this.m_configurationManager.Subscribe(new Type[] { typeof(ClusterNamesConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
				this.m_eventsKit = base.EventsKitFactory.CreateEventsKit<IClusterNamesEventsKit>();
			}
			return blockInitializationStatus;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00049708 File Offset: 0x00047908
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00049728 File Offset: 0x00047928
		public DnsName DeploymentClusterName
		{
			get
			{
				ClusterNamesConfiguration configuration = this.m_configuration;
				this.VerifyEnabled(configuration.DeploymentClusterPrefix);
				return new DnsName(configuration.DeploymentClusterPrefix, configuration.DnsZone);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0004975C File Offset: 0x0004795C
		public DnsName RedirectClusterName
		{
			get
			{
				ClusterNamesConfiguration configuration = this.m_configuration;
				this.VerifyEnabled(configuration.RedirectClusterPrefix);
				return new DnsName(configuration.RedirectClusterPrefix, configuration.DnsZone);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00049790 File Offset: 0x00047990
		public DnsName CurrentClusterName
		{
			get
			{
				ClusterNamesConfiguration configuration = this.m_configuration;
				this.VerifyEnabled(configuration.CurrentClusterName);
				return new DnsName(configuration.CurrentClusterName, configuration.DnsZone);
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x000497C3 File Offset: 0x000479C3
		private void OnConfigurationChange(IConfigurationContainer container)
		{
			this.m_configuration = container.GetConfiguration<ClusterNamesConfiguration>();
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000497D4 File Offset: 0x000479D4
		private void VerifyEnabled(string prefix)
		{
			if (string.IsNullOrWhiteSpace(prefix))
			{
				ClusterNamesDisabledException ex = new ClusterNamesDisabledException();
				this.m_eventsKit.NotifyClusterNamesDisabled(ex);
				throw ex;
			}
		}

		// Token: 0x0400043B RID: 1083
		private IConfigurationManager m_configurationManager;

		// Token: 0x0400043C RID: 1084
		private volatile ClusterNamesConfiguration m_configuration;

		// Token: 0x0400043D RID: 1085
		private IClusterNamesEventsKit m_eventsKit;
	}
}
