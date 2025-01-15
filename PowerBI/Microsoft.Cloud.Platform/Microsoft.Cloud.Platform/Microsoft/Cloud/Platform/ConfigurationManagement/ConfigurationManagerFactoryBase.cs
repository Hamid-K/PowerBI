using System;
using System.Collections.Concurrent;
using System.Configuration;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003FC RID: 1020
	public abstract class ConfigurationManagerFactoryBase : Block, IConfigurationManagerFactory, IConfigurationManagerHost
	{
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0007596D File Offset: 0x00073B6D
		// (set) Token: 0x06001F46 RID: 8006 RVA: 0x00075975 File Offset: 0x00073B75
		public IActivityFactory ActivityFactory { get; private set; }

		// Token: 0x06001F47 RID: 8007 RVA: 0x0007597E File Offset: 0x00073B7E
		public ConfigurationManagerFactoryBase()
			: this(typeof(ConfigurationManagerFactoryBase).Name)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00075995 File Offset: 0x00073B95
		public ConfigurationManagerFactoryBase(string name)
			: base(name)
		{
			this.m_configurationManagerRepository = new ConcurrentDictionary<string, IConfigurationManager>();
			this.ActivityFactory = new ActivityFactory();
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x000759B4 File Offset: 0x00073BB4
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			if (!this.m_published)
			{
				base.BlockHost.PublishService(this, typeof(IConfigurationManagerFactory), BlockServiceProviderIdentity.Implementation, this);
				this.m_published = true;
			}
			this.m_activityFactoryServiceTicket = base.BlockHost.TryGetService(new RequestedBlockService(this, typeof(IActivityFactory)));
			if (this.m_activityFactoryServiceTicket == null)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			IActivityFactory activityFactory = this.m_activityFactoryServiceTicket.GetService() as IActivityFactory;
			this.ActivityFactory = activityFactory;
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00075A38 File Offset: 0x00073C38
		protected override void OnStop()
		{
			foreach (IConfigurationManager configurationManager in this.m_configurationManagerRepository.Values)
			{
				IShuttable shuttable = configurationManager as IShuttable;
				if (shuttable != null)
				{
					shuttable.Stop();
				}
			}
			base.OnStop();
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00075A98 File Offset: 0x00073C98
		protected override void OnWaitForStopToComplete()
		{
			foreach (IConfigurationManager configurationManager in this.m_configurationManagerRepository.Values)
			{
				IShuttable shuttable = configurationManager as IShuttable;
				if (shuttable != null)
				{
					shuttable.WaitForStopToComplete();
				}
			}
			base.OnWaitForStopToComplete();
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00075AF8 File Offset: 0x00073CF8
		protected override void OnShutdown()
		{
			foreach (IConfigurationManager configurationManager in this.m_configurationManagerRepository.Values)
			{
				IShuttable shuttable = configurationManager as IShuttable;
				if (shuttable != null)
				{
					shuttable.Shutdown();
				}
			}
			this.m_configurationManagerRepository.Clear();
			if (this.m_activityFactoryServiceTicket != null)
			{
				this.m_activityFactoryServiceTicket.Dispose();
			}
			this.ActivityFactory = null;
			base.OnShutdown();
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00075B7C File Offset: 0x00073D7C
		public IConfigurationManager GetConfigurationManager([NotNull] string specification)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(specification, "specification");
			return this.m_configurationManagerRepository.GetOrAdd(specification, new Func<string, IConfigurationManager>(this.CreateConfigurationManager));
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00075BA4 File Offset: 0x00073DA4
		public virtual IConfigurationManager GetConfigurationManager()
		{
			string text = ConfigurationManager.AppSettings[ConfigurationManagerConstants.SpecificationSettingName];
			ExtendedDiagnostics.EnsureNotNull<string>(text, "no default configuration package (specification)");
			TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Creating Configuration Manager. Configuration package name: {0}", new object[] { text });
			return this.m_configurationManagerRepository.GetOrAdd(text, new Func<string, IConfigurationManager>(this.CreateConfigurationManager));
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00075BFE File Offset: 0x00073DFE
		public void RequestShutdown()
		{
			base.BlockHost.RequestShutdown(this);
		}

		// Token: 0x06001F50 RID: 8016
		protected abstract IConfigurationManager CreateConfigurationManager(string specification);

		// Token: 0x06001F51 RID: 8017 RVA: 0x00075C0C File Offset: 0x00073E0C
		protected IConfigurationManager GetOrAddToRepository(string key, Func<string, IConfigurationManager> valueFactory)
		{
			return this.m_configurationManagerRepository.GetOrAdd(key, valueFactory);
		}

		// Token: 0x04000AFE RID: 2814
		private readonly ConcurrentDictionary<string, IConfigurationManager> m_configurationManagerRepository;

		// Token: 0x04000AFF RID: 2815
		private BlockServiceTicket m_activityFactoryServiceTicket;

		// Token: 0x04000B00 RID: 2816
		private bool m_published;
	}
}
