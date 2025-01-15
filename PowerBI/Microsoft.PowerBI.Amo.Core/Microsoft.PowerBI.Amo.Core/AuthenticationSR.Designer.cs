using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000D9 RID: 217
	[CompilerGenerated]
	internal class AuthenticationSR
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0002B0D9 File Offset: 0x000292D9
		protected AuthenticationSR()
		{
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0002B0E1 File Offset: 0x000292E1
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0002B0E8 File Offset: 0x000292E8
		public static CultureInfo Culture
		{
			get
			{
				return AuthenticationSR.Keys.Culture;
			}
			set
			{
				AuthenticationSR.Keys.Culture = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x0002B0F0 File Offset: 0x000292F0
		public static string Exception_AcquireTokenFailure
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AcquireTokenFailure");
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002B0FC File Offset: 0x000292FC
		public static string Exception_InteractiveLoginRequired
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InteractiveLoginRequired");
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002B108 File Offset: 0x00029308
		public static string Exception_MFARequiredAndSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MFARequiredAndSupported");
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002B114 File Offset: 0x00029314
		public static string Exception_UnknownAuthenticationInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UnknownAuthenticationInfo");
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x0002B120 File Offset: 0x00029320
		public static string Exception_RedirectionInfoNotFound
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionInfoNotFound");
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002B12C File Offset: 0x0002932C
		public static string Exception_SpnAuthenticationWithoutPassword
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutPassword");
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x0002B138 File Offset: 0x00029338
		public static string Exception_UserAuthenticationWithServicePrincipalProfile
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UserAuthenticationWithServicePrincipalProfile");
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002B144 File Offset: 0x00029344
		public static string Exception_SpnAuthenticationWithoutTenant
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutTenant");
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002B150 File Offset: 0x00029350
		public static string Exception_AdalIsNoLongerSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AdalIsNoLongerSupported");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0002B15C File Offset: 0x0002935C
		public static string Exception_ADTranslationNotSupportedOnNonWindows
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_ADTranslationNotSupportedOnNonWindows");
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002B168 File Offset: 0x00029368
		public static string Exception_MissingDeviceCodeCallback
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MissingDeviceCodeCallback");
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002B174 File Offset: 0x00029374
		public static string Exception_S2STokenMissing
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_S2STokenMissing");
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0002B180 File Offset: 0x00029380
		public static string Exception_InvalidPBIPCapacity
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InvalidPBIPCapacity");
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0002B18C File Offset: 0x0002938C
		public static string Exception_RedirectionTokenAsPasswordIsNotSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionTokenAsPasswordIsNotSupported");
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002B198 File Offset: 0x00029398
		public static string Exception_RedirectionFailWithThrottling
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionFailWithThrottling");
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002B1A4 File Offset: 0x000293A4
		public static string PbiRequest_GetMwcToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetMwcToken");
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002B1B0 File Offset: 0x000293B0
		public static string PbiRequest_GetRedirectInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetRedirectInfo");
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002B1BC File Offset: 0x000293BC
		public static string PbiRequest_ResolveWorkspace
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_ResolveWorkspace");
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002B1C8 File Offset: 0x000293C8
		public static string PbiRequest_GetDatabaseName
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetDatabaseName");
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002B1D4 File Offset: 0x000293D4
		public static string DataverseRequest_GetEmbeddedToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetEmbeddedToken");
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002B1E0 File Offset: 0x000293E0
		public static string DataverseRequest_GetDatasetDetails
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetDatasetDetails");
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0002B1EC File Offset: 0x000293EC
		public static string DataverseRequest_UnexpectedResponse
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_UnexpectedResponse");
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002B1F8 File Offset: 0x000293F8
		public static string Exception_AssemblyMissingInEmbeddedResources(string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_AssemblyMissingInEmbeddedResources", assembly);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002B205 File Offset: 0x00029405
		public static string Exception_TypeMissingInAdalAssembly(string typeName, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_TypeMissingInAdalAssembly", typeName, assembly);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002B213 File Offset: 0x00029413
		public static string Exception_EnumValueMissingInAdalAssembly(string enumName, string enumValue, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_EnumValueMissingInAdalAssembly", enumName, enumValue, assembly);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002B222 File Offset: 0x00029422
		public static string Exception_PropertyMissingInAdalAssembly(string typeName, string property, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_PropertyMissingInAdalAssembly", typeName, property, assembly);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002B231 File Offset: 0x00029431
		public static string Exception_MethodMissingInAdalAssembly(string typeName, string method, string args, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_MethodMissingInAdalAssembly", typeName, method, args, assembly);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002B241 File Offset: 0x00029441
		public static string Exception_InvalidIdentityProvider(string provider)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidIdentityProvider", provider);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002B24E File Offset: 0x0002944E
		public static string Exception_InvalidAuthorityFormat(string authority)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidAuthorityFormat", authority);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002B25B File Offset: 0x0002945B
		public static string Exception_InvalidEndpointType(string endpoint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidEndpointType", endpoint);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002B268 File Offset: 0x00029468
		public static string Exception_InvalidServiceAppId(string id)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidServiceAppId", id);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002B275 File Offset: 0x00029475
		public static string Exception_InvalidCertificateThumbprint(string thumbprint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidCertificateThumbprint", thumbprint);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002B282 File Offset: 0x00029482
		public static string Exception_CantObtainUserInfoOnTokenRefresh(string username)
		{
			return AuthenticationSR.Keys.GetString("Exception_CantObtainUserInfoOnTokenRefresh", username);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002B28F File Offset: 0x0002948F
		public static string Exception_PbiRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_PbiRequestFailed", action, description, details, eol);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002B29F File Offset: 0x0002949F
		public static string Exception_DataverseRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_DataverseRequestFailed", action, description, details, eol);
		}

		// Token: 0x0200019E RID: 414
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001316 RID: 4886 RVA: 0x0004325F File Offset: 0x0004145F
			private Keys()
			{
			}

			// Token: 0x1700062E RID: 1582
			// (get) Token: 0x06001317 RID: 4887 RVA: 0x00043267 File Offset: 0x00041467
			// (set) Token: 0x06001318 RID: 4888 RVA: 0x0004326E File Offset: 0x0004146E
			public static CultureInfo Culture
			{
				get
				{
					return AuthenticationSR.Keys._culture;
				}
				set
				{
					AuthenticationSR.Keys._culture = value;
				}
			}

			// Token: 0x06001319 RID: 4889 RVA: 0x00043276 File Offset: 0x00041476
			public static string GetString(string key)
			{
				return AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture);
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x00043288 File Offset: 0x00041488
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0);
			}

			// Token: 0x0600131B RID: 4891 RVA: 0x000432A5 File Offset: 0x000414A5
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x0600131C RID: 4892 RVA: 0x000432C3 File Offset: 0x000414C3
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x000432E2 File Offset: 0x000414E2
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x04000C57 RID: 3159
			private static ResourceManager resourceManager = new ResourceManager(typeof(AuthenticationSR).FullName, typeof(AuthenticationSR).Module.Assembly);

			// Token: 0x04000C58 RID: 3160
			private static CultureInfo _culture = null;

			// Token: 0x04000C59 RID: 3161
			public const string Exception_AcquireTokenFailure = "Exception_AcquireTokenFailure";

			// Token: 0x04000C5A RID: 3162
			public const string Exception_InteractiveLoginRequired = "Exception_InteractiveLoginRequired";

			// Token: 0x04000C5B RID: 3163
			public const string Exception_MFARequiredAndSupported = "Exception_MFARequiredAndSupported";

			// Token: 0x04000C5C RID: 3164
			public const string Exception_AssemblyMissingInEmbeddedResources = "Exception_AssemblyMissingInEmbeddedResources";

			// Token: 0x04000C5D RID: 3165
			public const string Exception_TypeMissingInAdalAssembly = "Exception_TypeMissingInAdalAssembly";

			// Token: 0x04000C5E RID: 3166
			public const string Exception_EnumValueMissingInAdalAssembly = "Exception_EnumValueMissingInAdalAssembly";

			// Token: 0x04000C5F RID: 3167
			public const string Exception_PropertyMissingInAdalAssembly = "Exception_PropertyMissingInAdalAssembly";

			// Token: 0x04000C60 RID: 3168
			public const string Exception_MethodMissingInAdalAssembly = "Exception_MethodMissingInAdalAssembly";

			// Token: 0x04000C61 RID: 3169
			public const string Exception_UnknownAuthenticationInfo = "Exception_UnknownAuthenticationInfo";

			// Token: 0x04000C62 RID: 3170
			public const string Exception_InvalidIdentityProvider = "Exception_InvalidIdentityProvider";

			// Token: 0x04000C63 RID: 3171
			public const string Exception_InvalidAuthorityFormat = "Exception_InvalidAuthorityFormat";

			// Token: 0x04000C64 RID: 3172
			public const string Exception_RedirectionInfoNotFound = "Exception_RedirectionInfoNotFound";

			// Token: 0x04000C65 RID: 3173
			public const string Exception_SpnAuthenticationWithoutPassword = "Exception_SpnAuthenticationWithoutPassword";

			// Token: 0x04000C66 RID: 3174
			public const string Exception_UserAuthenticationWithServicePrincipalProfile = "Exception_UserAuthenticationWithServicePrincipalProfile";

			// Token: 0x04000C67 RID: 3175
			public const string Exception_SpnAuthenticationWithoutTenant = "Exception_SpnAuthenticationWithoutTenant";

			// Token: 0x04000C68 RID: 3176
			public const string Exception_AdalIsNoLongerSupported = "Exception_AdalIsNoLongerSupported";

			// Token: 0x04000C69 RID: 3177
			public const string Exception_InvalidEndpointType = "Exception_InvalidEndpointType";

			// Token: 0x04000C6A RID: 3178
			public const string Exception_InvalidServiceAppId = "Exception_InvalidServiceAppId";

			// Token: 0x04000C6B RID: 3179
			public const string Exception_InvalidCertificateThumbprint = "Exception_InvalidCertificateThumbprint";

			// Token: 0x04000C6C RID: 3180
			public const string Exception_ADTranslationNotSupportedOnNonWindows = "Exception_ADTranslationNotSupportedOnNonWindows";

			// Token: 0x04000C6D RID: 3181
			public const string Exception_CantObtainUserInfoOnTokenRefresh = "Exception_CantObtainUserInfoOnTokenRefresh";

			// Token: 0x04000C6E RID: 3182
			public const string Exception_MissingDeviceCodeCallback = "Exception_MissingDeviceCodeCallback";

			// Token: 0x04000C6F RID: 3183
			public const string Exception_S2STokenMissing = "Exception_S2STokenMissing";

			// Token: 0x04000C70 RID: 3184
			public const string Exception_InvalidPBIPCapacity = "Exception_InvalidPBIPCapacity";

			// Token: 0x04000C71 RID: 3185
			public const string Exception_RedirectionTokenAsPasswordIsNotSupported = "Exception_RedirectionTokenAsPasswordIsNotSupported";

			// Token: 0x04000C72 RID: 3186
			public const string Exception_PbiRequestFailed = "Exception_PbiRequestFailed";

			// Token: 0x04000C73 RID: 3187
			public const string Exception_RedirectionFailWithThrottling = "Exception_RedirectionFailWithThrottling";

			// Token: 0x04000C74 RID: 3188
			public const string Exception_DataverseRequestFailed = "Exception_DataverseRequestFailed";

			// Token: 0x04000C75 RID: 3189
			public const string PbiRequest_GetMwcToken = "PbiRequest_GetMwcToken";

			// Token: 0x04000C76 RID: 3190
			public const string PbiRequest_GetRedirectInfo = "PbiRequest_GetRedirectInfo";

			// Token: 0x04000C77 RID: 3191
			public const string PbiRequest_ResolveWorkspace = "PbiRequest_ResolveWorkspace";

			// Token: 0x04000C78 RID: 3192
			public const string PbiRequest_GetDatabaseName = "PbiRequest_GetDatabaseName";

			// Token: 0x04000C79 RID: 3193
			public const string DataverseRequest_GetEmbeddedToken = "DataverseRequest_GetEmbeddedToken";

			// Token: 0x04000C7A RID: 3194
			public const string DataverseRequest_GetDatasetDetails = "DataverseRequest_GetDatasetDetails";

			// Token: 0x04000C7B RID: 3195
			public const string DataverseRequest_UnexpectedResponse = "DataverseRequest_UnexpectedResponse";
		}
	}
}
