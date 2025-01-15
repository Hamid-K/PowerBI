using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.Platforms.Features.OpenTelemetry;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.TelemetryCore.OpenTelemetry;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001EB RID: 491
	internal abstract class AbstractPlatformProxy : IPlatformProxy
	{
		// Token: 0x060014EF RID: 5359 RVA: 0x000463D0 File Offset: 0x000445D0
		protected AbstractPlatformProxy(ILoggerAdapter logger)
		{
			this.Logger = logger;
			this._deviceModel = new Lazy<string>(new Func<string>(this.InternalGetDeviceModel));
			this._operatingSystem = new Lazy<string>(new Func<string>(this.InternalGetOperatingSystem));
			this._processorArchitecture = new Lazy<string>(new Func<string>(this.InternalGetProcessorArchitecture));
			this._callingApplicationName = new Lazy<string>(new Func<string>(this.InternalGetCallingApplicationName));
			this._callingApplicationVersion = new Lazy<string>(new Func<string>(this.InternalGetCallingApplicationVersion));
			this._deviceId = new Lazy<string>(new Func<string>(this.InternalGetDeviceId));
			this._productName = new Lazy<string>(new Func<string>(this.InternalGetProductName));
			this._cryptographyManager = new Lazy<ICryptographyManager>(new Func<ICryptographyManager>(this.InternalGetCryptographyManager));
			this._platformLogger = new Lazy<IPlatformLogger>(new Func<IPlatformLogger>(this.InternalGetPlatformLogger));
			this._runtimeVersion = new Lazy<string>(new Func<string>(this.InternalGetRuntimeVersion));
			this._otelInstrumentation = new Lazy<IOtelInstrumentation>(new Func<IOtelInstrumentation>(this.InternalGetOtelInstrumentation));
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x000464F4 File Offset: 0x000446F4
		private IOtelInstrumentation InternalGetOtelInstrumentation()
		{
			IOtelInstrumentation otelInstrumentation;
			try
			{
				otelInstrumentation = new OtelInstrumentation();
			}
			catch (FileNotFoundException ex)
			{
				this.Logger.Warning("Failed instantiating OpenTelemetry instrumentation. Exception: " + ex.Message);
				otelInstrumentation = new NullOtelInstrumentation();
			}
			return otelInstrumentation;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00046540 File Offset: 0x00044740
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00046548 File Offset: 0x00044748
		protected IFeatureFlags OverloadFeatureFlags { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00046551 File Offset: 0x00044751
		protected ILoggerAdapter Logger { get; }

		// Token: 0x060014F4 RID: 5364 RVA: 0x00046559 File Offset: 0x00044759
		public IWebUIFactory GetWebUiFactory(ApplicationConfiguration appConfig)
		{
			if (appConfig.WebUiFactoryCreator == null)
			{
				return this.CreateWebUiFactory();
			}
			return appConfig.WebUiFactoryCreator();
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00046575 File Offset: 0x00044775
		public string GetDeviceModel()
		{
			return this._deviceModel.Value;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00046582 File Offset: 0x00044782
		public string GetOperatingSystem()
		{
			return this._operatingSystem.Value;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0004658F File Offset: 0x0004478F
		public string GetProcessorArchitecture()
		{
			return this._processorArchitecture.Value;
		}

		// Token: 0x060014F8 RID: 5368
		public abstract Task<string> GetUserPrincipalNameAsync();

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004659C File Offset: 0x0004479C
		public string GetCallingApplicationName()
		{
			return this._callingApplicationName.Value;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x000465A9 File Offset: 0x000447A9
		public string GetCallingApplicationVersion()
		{
			return this._callingApplicationVersion.Value;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000465B6 File Offset: 0x000447B6
		public string GetDeviceId()
		{
			return this._deviceId.Value;
		}

		// Token: 0x060014FC RID: 5372
		public abstract string GetDefaultRedirectUri(string clientId, bool useRecommendedRedirectUri = false);

		// Token: 0x060014FD RID: 5373 RVA: 0x000465C3 File Offset: 0x000447C3
		public string GetProductName()
		{
			return this._productName.Value;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000465D0 File Offset: 0x000447D0
		public string GetRuntimeVersion()
		{
			return this._runtimeVersion.Value;
		}

		// Token: 0x060014FF RID: 5375
		public abstract ILegacyCachePersistence CreateLegacyCachePersistence();

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000465DD File Offset: 0x000447DD
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x000465E5 File Offset: 0x000447E5
		public ITokenCacheAccessor UserTokenCacheAccessorForTest { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x000465EE File Offset: 0x000447EE
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x000465F6 File Offset: 0x000447F6
		public ITokenCacheAccessor AppTokenCacheAccessorForTest { get; set; }

		// Token: 0x06001504 RID: 5380 RVA: 0x000465FF File Offset: 0x000447FF
		public virtual ITokenCacheAccessor CreateTokenCacheAccessor(CacheOptions tokenCacheAccessorOptions, bool isApplicationTokenCache = false)
		{
			if (isApplicationTokenCache)
			{
				return this.AppTokenCacheAccessorForTest ?? new InMemoryPartitionedAppTokenCacheAccessor(this.Logger, tokenCacheAccessorOptions);
			}
			return this.UserTokenCacheAccessorForTest ?? new InMemoryPartitionedUserTokenCacheAccessor(this.Logger, tokenCacheAccessorOptions);
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00046631 File Offset: 0x00044831
		public ICryptographyManager CryptographyManager
		{
			get
			{
				return this._cryptographyManager.Value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0004663E File Offset: 0x0004483E
		public IPlatformLogger PlatformLogger
		{
			get
			{
				return this._platformLogger.Value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0004664B File Offset: 0x0004484B
		public IOtelInstrumentation OtelInstrumentation
		{
			get
			{
				return this._otelInstrumentation.Value;
			}
		}

		// Token: 0x06001508 RID: 5384
		protected abstract IWebUIFactory CreateWebUiFactory();

		// Token: 0x06001509 RID: 5385
		protected abstract IFeatureFlags CreateFeatureFlags();

		// Token: 0x0600150A RID: 5386
		protected abstract string InternalGetDeviceModel();

		// Token: 0x0600150B RID: 5387
		protected abstract string InternalGetOperatingSystem();

		// Token: 0x0600150C RID: 5388
		protected abstract string InternalGetProcessorArchitecture();

		// Token: 0x0600150D RID: 5389
		protected abstract string InternalGetCallingApplicationName();

		// Token: 0x0600150E RID: 5390
		protected abstract string InternalGetCallingApplicationVersion();

		// Token: 0x0600150F RID: 5391
		protected abstract string InternalGetDeviceId();

		// Token: 0x06001510 RID: 5392
		protected abstract string InternalGetProductName();

		// Token: 0x06001511 RID: 5393
		protected abstract ICryptographyManager InternalGetCryptographyManager();

		// Token: 0x06001512 RID: 5394
		protected abstract IPlatformLogger InternalGetPlatformLogger();

		// Token: 0x06001513 RID: 5395 RVA: 0x00046658 File Offset: 0x00044858
		protected virtual string InternalGetRuntimeVersion()
		{
			return string.Empty;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004665F File Offset: 0x0004485F
		public virtual IFeatureFlags GetFeatureFlags()
		{
			return this.OverloadFeatureFlags ?? this.CreateFeatureFlags();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00046671 File Offset: 0x00044871
		public void SetFeatureFlags(IFeatureFlags featureFlags)
		{
			this.OverloadFeatureFlags = featureFlags;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004667A File Offset: 0x0004487A
		public virtual Task StartDefaultOsBrowserAsync(string url, bool IBrokerConfigured)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00046684 File Offset: 0x00044884
		public virtual IBroker CreateBroker(ApplicationConfiguration appConfig, CoreUIParent uiParent)
		{
			if (appConfig.BrokerCreatorFunc == null)
			{
				return new NullBroker(this.Logger);
			}
			return appConfig.BrokerCreatorFunc(uiParent, appConfig, this.Logger);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000466BA File Offset: 0x000448BA
		public virtual bool CanBrokerSupportSilentAuth()
		{
			return true;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000466BD File Offset: 0x000448BD
		public virtual bool BrokerSupportsWamAccounts
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000466C0 File Offset: 0x000448C0
		public virtual IPoPCryptoProvider GetDefaultPoPCryptoProvider()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000466C7 File Offset: 0x000448C7
		public virtual IDeviceAuthManager CreateDeviceAuthManager()
		{
			return new NullDeviceAuthManager();
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000466CE File Offset: 0x000448CE
		public virtual IMsalHttpClientFactory CreateDefaultHttpClientFactory()
		{
			return new SimpleHttpClientFactory();
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x000466D5 File Offset: 0x000448D5
		public virtual bool LegacyCacheRequiresSerialization
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040008B0 RID: 2224
		public const string MacOsDescriptionForSTS = "MacOS";

		// Token: 0x040008B1 RID: 2225
		public const string LinuxOSDescriptionForSTS = "Linux";

		// Token: 0x040008B2 RID: 2226
		private readonly Lazy<string> _callingApplicationName;

		// Token: 0x040008B3 RID: 2227
		private readonly Lazy<string> _callingApplicationVersion;

		// Token: 0x040008B4 RID: 2228
		private readonly Lazy<ICryptographyManager> _cryptographyManager;

		// Token: 0x040008B5 RID: 2229
		private readonly Lazy<string> _deviceId;

		// Token: 0x040008B6 RID: 2230
		private readonly Lazy<string> _deviceModel;

		// Token: 0x040008B7 RID: 2231
		private readonly Lazy<string> _operatingSystem;

		// Token: 0x040008B8 RID: 2232
		private readonly Lazy<IPlatformLogger> _platformLogger;

		// Token: 0x040008B9 RID: 2233
		private readonly Lazy<string> _processorArchitecture;

		// Token: 0x040008BA RID: 2234
		private readonly Lazy<string> _productName;

		// Token: 0x040008BB RID: 2235
		private readonly Lazy<string> _runtimeVersion;

		// Token: 0x040008BC RID: 2236
		private readonly Lazy<IOtelInstrumentation> _otelInstrumentation;
	}
}
