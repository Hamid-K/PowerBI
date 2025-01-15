using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.ConfigurationClasses.Tracing;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Tracing
{
	// Token: 0x02000014 RID: 20
	public class TraceSourceConfigurationProvider : Block
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000033C2 File Offset: 0x000015C2
		public TraceSourceConfigurationProvider()
			: base(typeof(TraceSourceConfigurationProvider).Name)
		{
			this.m_configurationLock = new object();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033E4 File Offset: 0x000015E4
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus == BlockInitializationStatus.Done)
			{
				this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
				this.m_configurationManager.Subscribe(new List<Type> { typeof(TraceSourcesConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			}
			return blockInitializationStatus;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003437 File Offset: 0x00001637
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003458 File Offset: 0x00001658
		private void OnConfigurationChange(IConfigurationContainer container)
		{
			object configurationLock = this.m_configurationLock;
			lock (configurationLock)
			{
				TraceSourcesConfiguration configuration = container.GetConfiguration<TraceSourcesConfiguration>();
				Tracing.TraceSourcesRegistrar.UpdateConfig(configuration);
			}
			TraceSourceBase<TracingTrace>.Tracer.Trace(TraceVerbosity.Info, "OnConfigurationChange is called for block {0}", new object[] { typeof(TraceSourceConfigurationProvider).FullName });
		}

		// Token: 0x04000046 RID: 70
		private readonly object m_configurationLock;

		// Token: 0x04000047 RID: 71
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000048 RID: 72
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;
	}
}
