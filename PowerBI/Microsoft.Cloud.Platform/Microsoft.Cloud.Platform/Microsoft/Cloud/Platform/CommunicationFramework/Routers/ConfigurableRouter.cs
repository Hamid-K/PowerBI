using System;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Routers
{
	// Token: 0x02000475 RID: 1141
	public abstract class ConfigurableRouter<T> : Router, IShuttable where T : ConfigurationClass
	{
		// Token: 0x06002382 RID: 9090 RVA: 0x0008076C File Offset: 0x0007E96C
		protected ConfigurableRouter(bool isUnicast, IRetryPolicy retryPolicy, IConfigurationManagerFactory configurationManagerFactory, IEventsKitFactory eventsKitFactory)
			: base(isUnicast, retryPolicy, eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IConfigurationManagerFactory>(configurationManagerFactory, "configurationManagerFactory");
			this.m_ticketManager = new WorkTicketManager(base.GetType().Name);
			this.m_ticketManager.Start();
			this.m_configurationManager = configurationManagerFactory.GetConfigurationManager();
			this.m_configurationManager.Subscribe(new Type[] { typeof(T) }, new CcsEventHandler(this.OnConfigurationChange));
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000807E5 File Offset: 0x0007E9E5
		public IWorkTicketFactory WorkTicketFactory
		{
			get
			{
				return this.m_ticketManager;
			}
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000807ED File Offset: 0x0007E9ED
		public T GetConfiguration()
		{
			return this.m_configuration;
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000807F7 File Offset: 0x0007E9F7
		public void Stop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			this.m_ticketManager.Stop();
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0008081B File Offset: 0x0007EA1B
		public void WaitForStopToComplete()
		{
			this.m_ticketManager.WaitForStopToComplete();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00080828 File Offset: 0x0007EA28
		public void Shutdown()
		{
			this.m_ticketManager.Shutdown();
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00080835 File Offset: 0x0007EA35
		private void OnConfigurationChange(IConfigurationContainer configContainer)
		{
			this.m_configuration = configContainer.GetConfiguration<T>();
		}

		// Token: 0x04000C63 RID: 3171
		private readonly WorkTicketManager m_ticketManager;

		// Token: 0x04000C64 RID: 3172
		private readonly IConfigurationManager m_configurationManager;

		// Token: 0x04000C65 RID: 3173
		private volatile T m_configuration;
	}
}
