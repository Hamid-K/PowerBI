using System;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.OAuth2.Throttling;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore;
using Microsoft.Identity.Client.TelemetryCore.Http;
using Microsoft.Identity.Client.WsTrust;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200023E RID: 574
	internal class ServiceBundle : IServiceBundle
	{
		// Token: 0x0600173E RID: 5950 RVA: 0x0004D0D0 File Offset: 0x0004B2D0
		internal ServiceBundle(ApplicationConfiguration config, bool shouldClearCaches = false)
		{
			this.Config = config;
			this.ApplicationLogger = LoggerHelper.CreateLogger(Guid.Empty, config);
			this.PlatformProxy = config.PlatformProxy ?? PlatformProxyFactory.CreatePlatformProxy(this.ApplicationLogger);
			IHttpManager httpManager;
			if ((httpManager = config.HttpManager) == null)
			{
				httpManager = HttpManagerFactory.GetHttpManager(config.HttpClientFactory ?? this.PlatformProxy.CreateDefaultHttpClientFactory(), config.RetryOnServerErrors, config.IsManagedIdentity);
			}
			this.HttpManager = httpManager;
			this.HttpTelemetryManager = new HttpTelemetryManager();
			this.InstanceDiscoveryManager = new InstanceDiscoveryManager(this.HttpManager, shouldClearCaches, config.CustomInstanceDiscoveryMetadata, config.CustomInstanceDiscoveryMetadataUri);
			this.WsTrustWebRequestManager = new WsTrustWebRequestManager(this.HttpManager);
			this.ThrottlingManager = SingletonThrottlingManager.GetInstance();
			this.DeviceAuthManager = config.DeviceAuthManagerForTest ?? this.PlatformProxy.CreateDeviceAuthManager();
			if (shouldClearCaches)
			{
				AuthorityManager.ClearValidationCache();
				PoPProviderFactory.Reset();
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0004D1B9 File Offset: 0x0004B3B9
		public ILoggerAdapter ApplicationLogger { get; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0004D1C1 File Offset: 0x0004B3C1
		public IHttpManager HttpManager { get; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0004D1C9 File Offset: 0x0004B3C9
		public IInstanceDiscoveryManager InstanceDiscoveryManager { get; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0004D1D1 File Offset: 0x0004B3D1
		public IWsTrustWebRequestManager WsTrustWebRequestManager { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0004D1D9 File Offset: 0x0004B3D9
		// (set) Token: 0x06001744 RID: 5956 RVA: 0x0004D1E1 File Offset: 0x0004B3E1
		public IPlatformProxy PlatformProxy { get; private set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0004D1EA File Offset: 0x0004B3EA
		public ApplicationConfiguration Config { get; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0004D1F2 File Offset: 0x0004B3F2
		public IDeviceAuthManager DeviceAuthManager { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0004D1FA File Offset: 0x0004B3FA
		public IHttpTelemetryManager HttpTelemetryManager { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0004D202 File Offset: 0x0004B402
		public IThrottlingProvider ThrottlingManager { get; }

		// Token: 0x06001749 RID: 5961 RVA: 0x0004D20A File Offset: 0x0004B40A
		public static ServiceBundle Create(ApplicationConfiguration config)
		{
			return new ServiceBundle(config, false);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0004D213 File Offset: 0x0004B413
		public void SetPlatformProxyForTest(IPlatformProxy platformProxy)
		{
			this.PlatformProxy = platformProxy;
		}
	}
}
