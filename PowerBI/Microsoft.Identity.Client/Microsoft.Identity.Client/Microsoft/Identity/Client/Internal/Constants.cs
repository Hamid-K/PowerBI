using System;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000230 RID: 560
	internal static class Constants
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x0004B84C File Offset: 0x00049A4C
		public static string FormatEnterpriseRegistrationOnPremiseUri(string domain)
		{
			return "https://enterpriseregistration." + domain + "/enrollmentserver/contract";
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0004B85E File Offset: 0x00049A5E
		public static string FormatEnterpriseRegistrationInternetUri(string domain)
		{
			return "https://enterpriseregistration.windows.net/" + domain + "/enrollmentserver/contract";
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0004B870 File Offset: 0x00049A70
		public static string FormatAdfsWebFingerUrl(string host, string resource)
		{
			return "https://" + host + "/.well-known/webfinger?rel=http://schemas.microsoft.com/rel/trusted-realm&resource=" + resource;
		}

		// Token: 0x040009C3 RID: 2499
		public const string MsAppScheme = "ms-app";

		// Token: 0x040009C4 RID: 2500
		public const int ExpirationMarginInMinutes = 5;

		// Token: 0x040009C5 RID: 2501
		public const int CodeVerifierLength = 128;

		// Token: 0x040009C6 RID: 2502
		public const int CodeVerifierByteSize = 96;

		// Token: 0x040009C7 RID: 2503
		public const string DefaultRedirectUri = "urn:ietf:wg:oauth:2.0:oob";

		// Token: 0x040009C8 RID: 2504
		public const string NativeClientRedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		// Token: 0x040009C9 RID: 2505
		public const string LocalHostRedirectUri = "http://localhost";

		// Token: 0x040009CA RID: 2506
		public const string DefaultConfidentialClientRedirectUri = "https://replyUrlNotSet";

		// Token: 0x040009CB RID: 2507
		public const string DefaultRealm = "http://schemas.microsoft.com/rel/trusted-realm";

		// Token: 0x040009CC RID: 2508
		public const string MsaTenantId = "9188040d-6c67-4c5b-b112-36a304b66dad";

		// Token: 0x040009CD RID: 2509
		public const string ConsumerTenant = "consumers";

		// Token: 0x040009CE RID: 2510
		public const string OrganizationsTenant = "organizations";

		// Token: 0x040009CF RID: 2511
		public const string CommonTenant = "common";

		// Token: 0x040009D0 RID: 2512
		public const string UserRealmMsaDomainName = "live.com";

		// Token: 0x040009D1 RID: 2513
		public const string CcsRoutingHintHeader = "x-anchormailbox";

		// Token: 0x040009D2 RID: 2514
		public const string AadThrottledErrorCode = "AADSTS50196";

		// Token: 0x040009D3 RID: 2515
		public const int DefaultJitterRangeInSeconds = 300;

		// Token: 0x040009D4 RID: 2516
		public static readonly TimeSpan AccessTokenExpirationBuffer = TimeSpan.FromMinutes(5.0);

		// Token: 0x040009D5 RID: 2517
		public const string EnableSpaAuthCode = "1";

		// Token: 0x040009D6 RID: 2518
		public const string PoPTokenType = "pop";

		// Token: 0x040009D7 RID: 2519
		public const string PoPAuthHeaderPrefix = "PoP";

		// Token: 0x040009D8 RID: 2520
		public const string RequestConfirmation = "req_cnf";

		// Token: 0x040009D9 RID: 2521
		public const string BearerAuthHeaderPrefix = "Bearer";

		// Token: 0x040009DA RID: 2522
		public const string ManagedIdentityClientId = "client_id";

		// Token: 0x040009DB RID: 2523
		public const string ManagedIdentityObjectId = "object_id";

		// Token: 0x040009DC RID: 2524
		public const string ManagedIdentityResourceId = "mi_res_id";

		// Token: 0x040009DD RID: 2525
		public const string ManagedIdentityDefaultClientId = "system_assigned_managed_identity";

		// Token: 0x040009DE RID: 2526
		public const string ManagedIdentityDefaultTenant = "managed_identity";

		// Token: 0x040009DF RID: 2527
		public const string CiamAuthorityHostSuffix = ".ciamlogin.com";

		// Token: 0x040009E0 RID: 2528
		public const string WellKnownOpenIdConfigurationPath = ".well-known/openid-configuration";

		// Token: 0x040009E1 RID: 2529
		public const string OpenIdConfigurationEndpoint = "v2.0/.well-known/openid-configuration";

		// Token: 0x040009E2 RID: 2530
		public const string Tenant = "{tenant}";

		// Token: 0x040009E3 RID: 2531
		public const string TenantId = "{tenantid}";
	}
}
