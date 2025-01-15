using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F6 RID: 246
	[CompilerGenerated]
	internal class AuthenticationSR
	{
		// Token: 0x06000D78 RID: 3448 RVA: 0x000308B8 File Offset: 0x0002EAB8
		protected AuthenticationSR()
		{
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x000308C0 File Offset: 0x0002EAC0
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x000308C7 File Offset: 0x0002EAC7
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

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x000308CF File Offset: 0x0002EACF
		public static string Exception_AcquireTokenFailure
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AcquireTokenFailure");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000308DB File Offset: 0x0002EADB
		public static string Exception_InteractiveLoginRequired
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InteractiveLoginRequired");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000308E7 File Offset: 0x0002EAE7
		public static string Exception_MFARequiredAndSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MFARequiredAndSupported");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000308F3 File Offset: 0x0002EAF3
		public static string Exception_UnknownAuthenticationInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UnknownAuthenticationInfo");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000308FF File Offset: 0x0002EAFF
		public static string Exception_RedirectionInfoNotFound
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionInfoNotFound");
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0003090B File Offset: 0x0002EB0B
		public static string Exception_SpnAuthenticationWithoutPassword
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutPassword");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00030917 File Offset: 0x0002EB17
		public static string Exception_UserAuthenticationWithServicePrincipalProfile
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UserAuthenticationWithServicePrincipalProfile");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00030923 File Offset: 0x0002EB23
		public static string Exception_SpnAuthenticationWithoutTenant
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutTenant");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003092F File Offset: 0x0002EB2F
		public static string Exception_AdalIsNoLongerSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AdalIsNoLongerSupported");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0003093B File Offset: 0x0002EB3B
		public static string Exception_ADTranslationNotSupportedOnNonWindows
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_ADTranslationNotSupportedOnNonWindows");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00030947 File Offset: 0x0002EB47
		public static string Exception_MissingDeviceCodeCallback
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MissingDeviceCodeCallback");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00030953 File Offset: 0x0002EB53
		public static string Exception_S2STokenMissing
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_S2STokenMissing");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003095F File Offset: 0x0002EB5F
		public static string Exception_InvalidPBIPCapacity
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InvalidPBIPCapacity");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0003096B File Offset: 0x0002EB6B
		public static string Exception_RedirectionTokenAsPasswordIsNotSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionTokenAsPasswordIsNotSupported");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00030977 File Offset: 0x0002EB77
		public static string Exception_RedirectionFailWithThrottling
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionFailWithThrottling");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00030983 File Offset: 0x0002EB83
		public static string PbiRequest_GetMwcToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetMwcToken");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0003098F File Offset: 0x0002EB8F
		public static string PbiRequest_GetRedirectInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetRedirectInfo");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0003099B File Offset: 0x0002EB9B
		public static string PbiRequest_ResolveWorkspace
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_ResolveWorkspace");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000309A7 File Offset: 0x0002EBA7
		public static string PbiRequest_GetDatabaseName
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetDatabaseName");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000309B3 File Offset: 0x0002EBB3
		public static string DataverseRequest_GetEmbeddedToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetEmbeddedToken");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000309BF File Offset: 0x0002EBBF
		public static string DataverseRequest_GetDatasetDetails
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetDatasetDetails");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000309CB File Offset: 0x0002EBCB
		public static string DataverseRequest_UnexpectedResponse
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_UnexpectedResponse");
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000309D7 File Offset: 0x0002EBD7
		public static string Exception_AssemblyMissingInEmbeddedResources(string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_AssemblyMissingInEmbeddedResources", assembly);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000309E4 File Offset: 0x0002EBE4
		public static string Exception_TypeMissingInAdalAssembly(string typeName, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_TypeMissingInAdalAssembly", typeName, assembly);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000309F2 File Offset: 0x0002EBF2
		public static string Exception_EnumValueMissingInAdalAssembly(string enumName, string enumValue, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_EnumValueMissingInAdalAssembly", enumName, enumValue, assembly);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00030A01 File Offset: 0x0002EC01
		public static string Exception_PropertyMissingInAdalAssembly(string typeName, string property, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_PropertyMissingInAdalAssembly", typeName, property, assembly);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00030A10 File Offset: 0x0002EC10
		public static string Exception_MethodMissingInAdalAssembly(string typeName, string method, string args, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_MethodMissingInAdalAssembly", typeName, method, args, assembly);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00030A20 File Offset: 0x0002EC20
		public static string Exception_InvalidIdentityProvider(string provider)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidIdentityProvider", provider);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00030A2D File Offset: 0x0002EC2D
		public static string Exception_InvalidAuthorityFormat(string authority)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidAuthorityFormat", authority);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00030A3A File Offset: 0x0002EC3A
		public static string Exception_InvalidEndpointType(string endpoint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidEndpointType", endpoint);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00030A47 File Offset: 0x0002EC47
		public static string Exception_InvalidServiceAppId(string id)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidServiceAppId", id);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00030A54 File Offset: 0x0002EC54
		public static string Exception_InvalidCertificateThumbprint(string thumbprint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidCertificateThumbprint", thumbprint);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00030A61 File Offset: 0x0002EC61
		public static string Exception_CantObtainUserInfoOnTokenRefresh(string username)
		{
			return AuthenticationSR.Keys.GetString("Exception_CantObtainUserInfoOnTokenRefresh", username);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00030A6E File Offset: 0x0002EC6E
		public static string Exception_PbiRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_PbiRequestFailed", action, description, details, eol);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00030A7E File Offset: 0x0002EC7E
		public static string Exception_DataverseRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_DataverseRequestFailed", action, description, details, eol);
		}

		// Token: 0x020001CC RID: 460
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013BC RID: 5052 RVA: 0x00044D37 File Offset: 0x00042F37
			private Keys()
			{
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x060013BD RID: 5053 RVA: 0x00044D3F File Offset: 0x00042F3F
			// (set) Token: 0x060013BE RID: 5054 RVA: 0x00044D46 File Offset: 0x00042F46
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

			// Token: 0x060013BF RID: 5055 RVA: 0x00044D4E File Offset: 0x00042F4E
			public static string GetString(string key)
			{
				return AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture);
			}

			// Token: 0x060013C0 RID: 5056 RVA: 0x00044D60 File Offset: 0x00042F60
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0);
			}

			// Token: 0x060013C1 RID: 5057 RVA: 0x00044D7D File Offset: 0x00042F7D
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013C2 RID: 5058 RVA: 0x00044D9B File Offset: 0x00042F9B
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x060013C3 RID: 5059 RVA: 0x00044DBA File Offset: 0x00042FBA
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x04000D02 RID: 3330
			private static ResourceManager resourceManager = new ResourceManager(typeof(AuthenticationSR).FullName, typeof(AuthenticationSR).Module.Assembly);

			// Token: 0x04000D03 RID: 3331
			private static CultureInfo _culture = null;

			// Token: 0x04000D04 RID: 3332
			public const string Exception_AcquireTokenFailure = "Exception_AcquireTokenFailure";

			// Token: 0x04000D05 RID: 3333
			public const string Exception_InteractiveLoginRequired = "Exception_InteractiveLoginRequired";

			// Token: 0x04000D06 RID: 3334
			public const string Exception_MFARequiredAndSupported = "Exception_MFARequiredAndSupported";

			// Token: 0x04000D07 RID: 3335
			public const string Exception_AssemblyMissingInEmbeddedResources = "Exception_AssemblyMissingInEmbeddedResources";

			// Token: 0x04000D08 RID: 3336
			public const string Exception_TypeMissingInAdalAssembly = "Exception_TypeMissingInAdalAssembly";

			// Token: 0x04000D09 RID: 3337
			public const string Exception_EnumValueMissingInAdalAssembly = "Exception_EnumValueMissingInAdalAssembly";

			// Token: 0x04000D0A RID: 3338
			public const string Exception_PropertyMissingInAdalAssembly = "Exception_PropertyMissingInAdalAssembly";

			// Token: 0x04000D0B RID: 3339
			public const string Exception_MethodMissingInAdalAssembly = "Exception_MethodMissingInAdalAssembly";

			// Token: 0x04000D0C RID: 3340
			public const string Exception_UnknownAuthenticationInfo = "Exception_UnknownAuthenticationInfo";

			// Token: 0x04000D0D RID: 3341
			public const string Exception_InvalidIdentityProvider = "Exception_InvalidIdentityProvider";

			// Token: 0x04000D0E RID: 3342
			public const string Exception_InvalidAuthorityFormat = "Exception_InvalidAuthorityFormat";

			// Token: 0x04000D0F RID: 3343
			public const string Exception_RedirectionInfoNotFound = "Exception_RedirectionInfoNotFound";

			// Token: 0x04000D10 RID: 3344
			public const string Exception_SpnAuthenticationWithoutPassword = "Exception_SpnAuthenticationWithoutPassword";

			// Token: 0x04000D11 RID: 3345
			public const string Exception_UserAuthenticationWithServicePrincipalProfile = "Exception_UserAuthenticationWithServicePrincipalProfile";

			// Token: 0x04000D12 RID: 3346
			public const string Exception_SpnAuthenticationWithoutTenant = "Exception_SpnAuthenticationWithoutTenant";

			// Token: 0x04000D13 RID: 3347
			public const string Exception_AdalIsNoLongerSupported = "Exception_AdalIsNoLongerSupported";

			// Token: 0x04000D14 RID: 3348
			public const string Exception_InvalidEndpointType = "Exception_InvalidEndpointType";

			// Token: 0x04000D15 RID: 3349
			public const string Exception_InvalidServiceAppId = "Exception_InvalidServiceAppId";

			// Token: 0x04000D16 RID: 3350
			public const string Exception_InvalidCertificateThumbprint = "Exception_InvalidCertificateThumbprint";

			// Token: 0x04000D17 RID: 3351
			public const string Exception_ADTranslationNotSupportedOnNonWindows = "Exception_ADTranslationNotSupportedOnNonWindows";

			// Token: 0x04000D18 RID: 3352
			public const string Exception_CantObtainUserInfoOnTokenRefresh = "Exception_CantObtainUserInfoOnTokenRefresh";

			// Token: 0x04000D19 RID: 3353
			public const string Exception_MissingDeviceCodeCallback = "Exception_MissingDeviceCodeCallback";

			// Token: 0x04000D1A RID: 3354
			public const string Exception_S2STokenMissing = "Exception_S2STokenMissing";

			// Token: 0x04000D1B RID: 3355
			public const string Exception_InvalidPBIPCapacity = "Exception_InvalidPBIPCapacity";

			// Token: 0x04000D1C RID: 3356
			public const string Exception_RedirectionTokenAsPasswordIsNotSupported = "Exception_RedirectionTokenAsPasswordIsNotSupported";

			// Token: 0x04000D1D RID: 3357
			public const string Exception_PbiRequestFailed = "Exception_PbiRequestFailed";

			// Token: 0x04000D1E RID: 3358
			public const string Exception_RedirectionFailWithThrottling = "Exception_RedirectionFailWithThrottling";

			// Token: 0x04000D1F RID: 3359
			public const string Exception_DataverseRequestFailed = "Exception_DataverseRequestFailed";

			// Token: 0x04000D20 RID: 3360
			public const string PbiRequest_GetMwcToken = "PbiRequest_GetMwcToken";

			// Token: 0x04000D21 RID: 3361
			public const string PbiRequest_GetRedirectInfo = "PbiRequest_GetRedirectInfo";

			// Token: 0x04000D22 RID: 3362
			public const string PbiRequest_ResolveWorkspace = "PbiRequest_ResolveWorkspace";

			// Token: 0x04000D23 RID: 3363
			public const string PbiRequest_GetDatabaseName = "PbiRequest_GetDatabaseName";

			// Token: 0x04000D24 RID: 3364
			public const string DataverseRequest_GetEmbeddedToken = "DataverseRequest_GetEmbeddedToken";

			// Token: 0x04000D25 RID: 3365
			public const string DataverseRequest_GetDatasetDetails = "DataverseRequest_GetDatasetDetails";

			// Token: 0x04000D26 RID: 3366
			public const string DataverseRequest_UnexpectedResponse = "DataverseRequest_UnexpectedResponse";
		}
	}
}
