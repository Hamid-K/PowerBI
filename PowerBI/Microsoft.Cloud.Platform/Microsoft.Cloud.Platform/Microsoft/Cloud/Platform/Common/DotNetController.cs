using System;
using System.Net;
using System.Threading;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000517 RID: 1303
	public sealed class DotNetController : Block
	{
		// Token: 0x06002862 RID: 10338 RVA: 0x00091A79 File Offset: 0x0008FC79
		public DotNetController()
			: base(typeof(DotNetController).Name)
		{
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00091A9C File Offset: 0x0008FC9C
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.m_configurationManager.Subscribe(new Type[] { typeof(DotNetControllerConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00091AFA File Offset: 0x0008FCFA
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00091B1C File Offset: 0x0008FD1C
		private void OnConfigurationChange(IConfigurationContainer configurationContainer)
		{
			DotNetControllerConfiguration configuration = configurationContainer.GetConfiguration<DotNetControllerConfiguration>();
			object configurationLock = this.m_configurationLock;
			lock (configurationLock)
			{
				if (configuration.ServicePointConnections != 0)
				{
					ServicePointManager.DefaultConnectionLimit = configuration.ServicePointConnections;
				}
			}
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("OnConfigurationChange is called for block {0}. Updating configuration to the following values: ServicePointConnections={1} ", new object[]
			{
				typeof(DotNetController).FullName,
				configuration.ServicePointConnections
			});
			DotNetController.SetThreadCounts(configuration);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00091BAC File Offset: 0x0008FDAC
		internal static void SetThreadCounts(DotNetControllerConfiguration cfg)
		{
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Configuring thread pool defaults and limits.");
			int num;
			int num2;
			ThreadPool.GetMinThreads(out num, out num2);
			int num3;
			int num4;
			ThreadPool.GetMaxThreads(out num3, out num4);
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Existing thread min: worker-{0}, io-{1}", new object[] { num, num2 });
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Existing thread max: worker-{0}, io-{1}", new object[] { num3, num4 });
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Requested thread min: worker-{0}, io-{1}", new object[]
			{
				cfg.WorkerRange.Min,
				cfg.IoRange.Min
			});
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Requested thread max: worker-{0}, io-{1}", new object[]
			{
				cfg.WorkerRange.Max,
				cfg.IoRange.Max
			});
			if (cfg.IoRange.Min > cfg.IoRange.Max && cfg.IoRange.Max > 0)
			{
				throw new CcsMalformedConfigurationException("Invalid IO thread range: Min:{0}, Max:{1}".FormatWithInvariantCulture(new object[]
				{
					cfg.IoRange.Min,
					cfg.IoRange.Max
				}));
			}
			if (cfg.WorkerRange.Min > cfg.WorkerRange.Max && cfg.WorkerRange.Max > 0)
			{
				throw new CcsMalformedConfigurationException("Invalid worker thread range: Min:{0}, Max:{1}".FormatWithInvariantCulture(new object[]
				{
					cfg.WorkerRange.Min,
					cfg.WorkerRange.Max
				}));
			}
			if (cfg.IoRange.Max == 0 && num4 < cfg.IoRange.Min)
			{
				cfg.IoRange.Max = cfg.IoRange.Min;
			}
			if (cfg.WorkerRange.Max == 0 && num3 < cfg.WorkerRange.Min)
			{
				cfg.WorkerRange.Max = cfg.WorkerRange.Min;
			}
			cfg.IoRange.Min = ((cfg.IoRange.Min == 0) ? num2 : cfg.IoRange.Min);
			cfg.IoRange.Max = ((cfg.IoRange.Max == 0) ? num4 : cfg.IoRange.Max);
			cfg.WorkerRange.Min = ((cfg.WorkerRange.Min == 0) ? num : cfg.WorkerRange.Min);
			cfg.WorkerRange.Max = ((cfg.WorkerRange.Max == 0) ? num3 : cfg.WorkerRange.Max);
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Setting thread min: worker-{0}, io-{1}", new object[]
			{
				cfg.WorkerRange.Min,
				cfg.IoRange.Min
			});
			TraceSourceBase<CommonTrace>.Tracer.TraceInformation("Setting thread max: worker-{0}, io-{1}", new object[]
			{
				cfg.WorkerRange.Max,
				cfg.IoRange.Max
			});
			ThreadPool.SetMaxThreads(cfg.WorkerRange.Max, cfg.IoRange.Max);
			ThreadPool.SetMinThreads(cfg.WorkerRange.Min, cfg.IoRange.Min);
		}

		// Token: 0x04000DFB RID: 3579
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000DFC RID: 3580
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000DFD RID: 3581
		private readonly object m_configurationLock = new object();
	}
}
