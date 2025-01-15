using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AppConfig;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.Identity.Client.Http;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.Internal.ClientCredential;
using Microsoft.Identity.Client.Kerberos;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.UI;
using Microsoft.IdentityModel.Abstractions;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200012E RID: 302
	internal sealed class ApplicationConfiguration : IAppConfig
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x00038D94 File Offset: 0x00036F94
		public ApplicationConfiguration(MsalClientType applicationType)
		{
			if (applicationType == MsalClientType.ConfidentialClient)
			{
				this.IsConfidentialClient = true;
				return;
			}
			if (applicationType != MsalClientType.ManagedIdentityClient)
			{
				return;
			}
			this.IsManagedIdentity = true;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00038E18 File Offset: 0x00037018
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00038E20 File Offset: 0x00037020
		public string ClientName
		{
			get
			{
				return this._clientName;
			}
			internal set
			{
				this._clientName = (string.IsNullOrWhiteSpace(value) ? "UnknownClient" : value);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00038E38 File Offset: 0x00037038
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00038E40 File Offset: 0x00037040
		public string ClientVersion
		{
			get
			{
				return this._clientVersion;
			}
			internal set
			{
				this._clientVersion = (string.IsNullOrWhiteSpace(value) ? "0.0.0.0" : value);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00038E58 File Offset: 0x00037058
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00038E60 File Offset: 0x00037060
		public ITelemetryClient[] TelemetryClients { get; internal set; } = Array.Empty<ITelemetryClient>();

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00038E69 File Offset: 0x00037069
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00038E71 File Offset: 0x00037071
		public Func<object> ParentActivityOrWindowFunc { get; internal set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00038E7A File Offset: 0x0003707A
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00038E82 File Offset: 0x00037082
		public string IosKeychainSecurityGroup { get; internal set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00038E8B File Offset: 0x0003708B
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00038E93 File Offset: 0x00037093
		public bool IsBrokerEnabled { get; internal set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00038E9C File Offset: 0x0003709C
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00038EA4 File Offset: 0x000370A4
		public bool IsWebviewSsoPolicyEnabled { get; internal set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00038EAD File Offset: 0x000370AD
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x00038EB5 File Offset: 0x000370B5
		public BrokerOptions BrokerOptions { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00038EBE File Offset: 0x000370BE
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x00038EC6 File Offset: 0x000370C6
		public Func<CoreUIParent, ApplicationConfiguration, ILoggerAdapter, IBroker> BrokerCreatorFunc { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00038ECF File Offset: 0x000370CF
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x00038ED7 File Offset: 0x000370D7
		public Func<IWebUIFactory> WebUiFactoryCreator { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00038EE0 File Offset: 0x000370E0
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x00038EE8 File Offset: 0x000370E8
		public string KerberosServicePrincipalName { get; set; } = string.Empty;

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00038EF1 File Offset: 0x000370F1
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00038EF9 File Offset: 0x000370F9
		public KerberosTicketContainer TicketContainer { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00038F02 File Offset: 0x00037102
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00038F0A File Offset: 0x0003710A
		[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ITelemetryConfig TelemetryConfig { get; internal set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00038F13 File Offset: 0x00037113
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00038F1B File Offset: 0x0003711B
		public IHttpManager HttpManager { get; internal set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00038F24 File Offset: 0x00037124
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00038F2C File Offset: 0x0003712C
		public IPlatformProxy PlatformProxy { get; internal set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00038F35 File Offset: 0x00037135
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00038F3D File Offset: 0x0003713D
		public CacheOptions AccessorOptions { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00038F46 File Offset: 0x00037146
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00038F4E File Offset: 0x0003714E
		public Authority Authority { get; internal set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00038F57 File Offset: 0x00037157
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00038F5F File Offset: 0x0003715F
		public string ClientId { get; internal set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00038F68 File Offset: 0x00037168
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00038F70 File Offset: 0x00037170
		public string RedirectUri { get; internal set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00038F79 File Offset: 0x00037179
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00038F81 File Offset: 0x00037181
		public bool EnablePiiLogging { get; internal set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00038F8A File Offset: 0x0003718A
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00038F92 File Offset: 0x00037192
		public LogLevel LogLevel { get; internal set; } = LogLevel.Info;

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00038F9B File Offset: 0x0003719B
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00038FA3 File Offset: 0x000371A3
		public bool IsDefaultPlatformLoggingEnabled { get; internal set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00038FAC File Offset: 0x000371AC
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00038FB4 File Offset: 0x000371B4
		public IMsalHttpClientFactory HttpClientFactory { get; internal set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00038FBD File Offset: 0x000371BD
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00038FC5 File Offset: 0x000371C5
		public bool IsExtendedTokenLifetimeEnabled { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00038FCE File Offset: 0x000371CE
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00038FD6 File Offset: 0x000371D6
		public LogCallback LoggingCallback { get; internal set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00038FDF File Offset: 0x000371DF
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00038FE7 File Offset: 0x000371E7
		public IIdentityLogger IdentityLogger { get; internal set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00038FF0 File Offset: 0x000371F0
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00038FF8 File Offset: 0x000371F8
		public string Component { get; internal set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00039001 File Offset: 0x00037201
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00039009 File Offset: 0x00037209
		public IDictionary<string, string> ExtraQueryParameters { get; internal set; } = new Dictionary<string, string>();

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00039012 File Offset: 0x00037212
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0003901A File Offset: 0x0003721A
		public bool UseRecommendedDefaultRedirectUri { get; internal set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00039023 File Offset: 0x00037223
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0003902B File Offset: 0x0003722B
		public bool ExperimentalFeaturesEnabled { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00039034 File Offset: 0x00037234
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0003903C File Offset: 0x0003723C
		public IEnumerable<string> ClientCapabilities { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00039045 File Offset: 0x00037245
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x0003904D File Offset: 0x0003724D
		public bool SendX5C { get; internal set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00039056 File Offset: 0x00037256
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x0003905E File Offset: 0x0003725E
		public bool LegacyCacheCompatibilityEnabled { get; internal set; } = true;

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00039067 File Offset: 0x00037267
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0003906F File Offset: 0x0003726F
		public bool CacheSynchronizationEnabled { get; internal set; } = true;

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00039078 File Offset: 0x00037278
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00039080 File Offset: 0x00037280
		public bool MultiCloudSupportEnabled { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00039089 File Offset: 0x00037289
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x00039091 File Offset: 0x00037291
		public bool RetryOnServerErrors { get; set; } = true;

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003909A File Offset: 0x0003729A
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x000390A2 File Offset: 0x000372A2
		public ManagedIdentityId ManagedIdentityId { get; internal set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x000390AB File Offset: 0x000372AB
		public bool IsManagedIdentity { get; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x000390B3 File Offset: 0x000372B3
		public bool IsConfidentialClient { get; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000390BB File Offset: 0x000372BB
		public bool IsPublicClient
		{
			get
			{
				return !this.IsConfidentialClient && !this.IsManagedIdentity;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x000390D0 File Offset: 0x000372D0
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x000390D8 File Offset: 0x000372D8
		public IClientCredential ClientCredential { get; internal set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x000390E4 File Offset: 0x000372E4
		public string ClientSecret
		{
			get
			{
				SecretStringClientCredential secretStringClientCredential = this.ClientCredential as SecretStringClientCredential;
				if (secretStringClientCredential != null)
				{
					return secretStringClientCredential.Secret;
				}
				return null;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00039108 File Offset: 0x00037308
		public X509Certificate2 ClientCredentialCertificate
		{
			get
			{
				CertificateAndClaimsClientCredential certificateAndClaimsClientCredential = this.ClientCredential as CertificateAndClaimsClientCredential;
				if (certificateAndClaimsClientCredential != null)
				{
					return certificateAndClaimsClientCredential.Certificate;
				}
				return null;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0003912C File Offset: 0x0003732C
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x00039134 File Offset: 0x00037334
		public string AzureRegion { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0003913D File Offset: 0x0003733D
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x00039145 File Offset: 0x00037345
		public string TenantId { get; internal set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0003914E File Offset: 0x0003734E
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00039156 File Offset: 0x00037356
		public InstanceDiscoveryResponse CustomInstanceDiscoveryMetadata { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0003915F File Offset: 0x0003735F
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00039167 File Offset: 0x00037367
		public Uri CustomInstanceDiscoveryMetadataUri { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00039170 File Offset: 0x00037370
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00039178 File Offset: 0x00037378
		public AadAuthorityAudience AadAuthorityAudience { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00039181 File Offset: 0x00037381
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00039189 File Offset: 0x00037389
		public AzureCloudInstance AzureCloudInstance { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00039192 File Offset: 0x00037392
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0003919A File Offset: 0x0003739A
		public string Instance { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000391A3 File Offset: 0x000373A3
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x000391AB File Offset: 0x000373AB
		public bool ValidateAuthority { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000391B4 File Offset: 0x000373B4
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x000391BC File Offset: 0x000373BC
		public ILegacyCachePersistence UserTokenLegacyCachePersistenceForTest { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000391C5 File Offset: 0x000373C5
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x000391CD File Offset: 0x000373CD
		public ITokenCacheInternal UserTokenCacheInternalForTest { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000391D6 File Offset: 0x000373D6
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x000391DE File Offset: 0x000373DE
		public ITokenCacheInternal AppTokenCacheInternalForTest { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000391E7 File Offset: 0x000373E7
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x000391EF File Offset: 0x000373EF
		public IDeviceAuthManager DeviceAuthManagerForTest { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000391F8 File Offset: 0x000373F8
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00039200 File Offset: 0x00037400
		public bool IsInstanceDiscoveryEnabled { get; internal set; } = true;

		// Token: 0x04000475 RID: 1141
		public const string DefaultClientName = "UnknownClient";

		// Token: 0x04000476 RID: 1142
		public const string DefaultClientVersion = "0.0.0.0";

		// Token: 0x04000477 RID: 1143
		private string _clientName = "UnknownClient";

		// Token: 0x04000478 RID: 1144
		private string _clientVersion = "0.0.0.0";

		// Token: 0x0400049E RID: 1182
		public Func<AppTokenProviderParameters, Task<AppTokenProviderResult>> AppTokenProvider;
	}
}
