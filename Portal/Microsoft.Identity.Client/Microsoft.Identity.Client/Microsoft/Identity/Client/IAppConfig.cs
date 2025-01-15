using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000139 RID: 313
	public interface IAppConfig
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000FD4 RID: 4052
		string ClientId { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000FD5 RID: 4053
		bool EnablePiiLogging { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000FD6 RID: 4054
		IMsalHttpClientFactory HttpClientFactory { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000FD7 RID: 4055
		LogLevel LogLevel { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000FD8 RID: 4056
		bool IsDefaultPlatformLoggingEnabled { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000FD9 RID: 4057
		string RedirectUri { get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000FDA RID: 4058
		string TenantId { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000FDB RID: 4059
		LogCallback LoggingCallback { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000FDC RID: 4060
		IDictionary<string, string> ExtraQueryParameters { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000FDD RID: 4061
		bool IsBrokerEnabled { get; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000FDE RID: 4062
		string ClientName { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000FDF RID: 4063
		string ClientVersion { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000FE0 RID: 4064
		[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		ITelemetryConfig TelemetryConfig { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000FE1 RID: 4065
		bool ExperimentalFeaturesEnabled { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000FE2 RID: 4066
		IEnumerable<string> ClientCapabilities { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000FE3 RID: 4067
		bool LegacyCacheCompatibilityEnabled { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000FE4 RID: 4068
		string ClientSecret { get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000FE5 RID: 4069
		X509Certificate2 ClientCredentialCertificate { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000FE6 RID: 4070
		Func<object> ParentActivityOrWindowFunc { get; }
	}
}
