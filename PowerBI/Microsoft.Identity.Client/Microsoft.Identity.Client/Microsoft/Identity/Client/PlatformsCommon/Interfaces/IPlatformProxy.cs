using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.TelemetryCore.OpenTelemetry;
using Microsoft.Identity.Client.UI;

namespace Microsoft.Identity.Client.PlatformsCommon.Interfaces
{
	// Token: 0x020001FF RID: 511
	internal interface IPlatformProxy
	{
		// Token: 0x06001599 RID: 5529
		string GetDeviceModel();

		// Token: 0x0600159A RID: 5530
		string GetOperatingSystem();

		// Token: 0x0600159B RID: 5531
		string GetProcessorArchitecture();

		// Token: 0x0600159C RID: 5532
		Task<string> GetUserPrincipalNameAsync();

		// Token: 0x0600159D RID: 5533
		string GetCallingApplicationName();

		// Token: 0x0600159E RID: 5534
		string GetCallingApplicationVersion();

		// Token: 0x0600159F RID: 5535
		string GetDeviceId();

		// Token: 0x060015A0 RID: 5536
		string GetDefaultRedirectUri(string clientId, bool useRecommendedRedirectUri = false);

		// Token: 0x060015A1 RID: 5537
		string GetProductName();

		// Token: 0x060015A2 RID: 5538
		string GetRuntimeVersion();

		// Token: 0x060015A3 RID: 5539
		ILegacyCachePersistence CreateLegacyCachePersistence();

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060015A4 RID: 5540
		bool LegacyCacheRequiresSerialization { get; }

		// Token: 0x060015A5 RID: 5541
		ITokenCacheAccessor CreateTokenCacheAccessor(CacheOptions accessorOptions, bool isApplicationTokenCache = false);

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060015A6 RID: 5542
		ICryptographyManager CryptographyManager { get; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060015A7 RID: 5543
		IPlatformLogger PlatformLogger { get; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060015A8 RID: 5544
		IOtelInstrumentation OtelInstrumentation { get; }

		// Token: 0x060015A9 RID: 5545
		IWebUIFactory GetWebUiFactory(ApplicationConfiguration appConfig);

		// Token: 0x060015AA RID: 5546
		IPoPCryptoProvider GetDefaultPoPCryptoProvider();

		// Token: 0x060015AB RID: 5547
		IFeatureFlags GetFeatureFlags();

		// Token: 0x060015AC RID: 5548
		void SetFeatureFlags(IFeatureFlags featureFlags);

		// Token: 0x060015AD RID: 5549
		Task StartDefaultOsBrowserAsync(string url, bool isBrokerConfigured);

		// Token: 0x060015AE RID: 5550
		IBroker CreateBroker(ApplicationConfiguration appConfig, CoreUIParent uiParent);

		// Token: 0x060015AF RID: 5551
		IDeviceAuthManager CreateDeviceAuthManager();

		// Token: 0x060015B0 RID: 5552
		bool CanBrokerSupportSilentAuth();

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060015B1 RID: 5553
		bool BrokerSupportsWamAccounts { get; }

		// Token: 0x060015B2 RID: 5554
		IMsalHttpClientFactory CreateDefaultHttpClientFactory();
	}
}
