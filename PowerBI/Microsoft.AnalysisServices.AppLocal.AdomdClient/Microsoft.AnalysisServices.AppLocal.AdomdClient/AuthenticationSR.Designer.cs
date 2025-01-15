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
		// Token: 0x06000D85 RID: 3461 RVA: 0x00030BE8 File Offset: 0x0002EDE8
		protected AuthenticationSR()
		{
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00030BF7 File Offset: 0x0002EDF7
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

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00030BFF File Offset: 0x0002EDFF
		public static string Exception_AcquireTokenFailure
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AcquireTokenFailure");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00030C0B File Offset: 0x0002EE0B
		public static string Exception_InteractiveLoginRequired
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InteractiveLoginRequired");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00030C17 File Offset: 0x0002EE17
		public static string Exception_MFARequiredAndSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MFARequiredAndSupported");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00030C23 File Offset: 0x0002EE23
		public static string Exception_UnknownAuthenticationInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UnknownAuthenticationInfo");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00030C2F File Offset: 0x0002EE2F
		public static string Exception_RedirectionInfoNotFound
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionInfoNotFound");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00030C3B File Offset: 0x0002EE3B
		public static string Exception_SpnAuthenticationWithoutPassword
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutPassword");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00030C47 File Offset: 0x0002EE47
		public static string Exception_UserAuthenticationWithServicePrincipalProfile
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UserAuthenticationWithServicePrincipalProfile");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00030C53 File Offset: 0x0002EE53
		public static string Exception_SpnAuthenticationWithoutTenant
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutTenant");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00030C5F File Offset: 0x0002EE5F
		public static string Exception_AdalIsNoLongerSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AdalIsNoLongerSupported");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00030C6B File Offset: 0x0002EE6B
		public static string Exception_ADTranslationNotSupportedOnNonWindows
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_ADTranslationNotSupportedOnNonWindows");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00030C77 File Offset: 0x0002EE77
		public static string Exception_MissingDeviceCodeCallback
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MissingDeviceCodeCallback");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00030C83 File Offset: 0x0002EE83
		public static string Exception_S2STokenMissing
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_S2STokenMissing");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00030C8F File Offset: 0x0002EE8F
		public static string Exception_InvalidPBIPCapacity
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InvalidPBIPCapacity");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00030C9B File Offset: 0x0002EE9B
		public static string Exception_RedirectionTokenAsPasswordIsNotSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionTokenAsPasswordIsNotSupported");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00030CA7 File Offset: 0x0002EEA7
		public static string Exception_RedirectionFailWithThrottling
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionFailWithThrottling");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00030CB3 File Offset: 0x0002EEB3
		public static string PbiRequest_GetMwcToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetMwcToken");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00030CBF File Offset: 0x0002EEBF
		public static string PbiRequest_GetRedirectInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetRedirectInfo");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x00030CCB File Offset: 0x0002EECB
		public static string PbiRequest_ResolveWorkspace
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_ResolveWorkspace");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00030CD7 File Offset: 0x0002EED7
		public static string PbiRequest_GetDatabaseName
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetDatabaseName");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00030CE3 File Offset: 0x0002EEE3
		public static string DataverseRequest_GetEmbeddedToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetEmbeddedToken");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00030CEF File Offset: 0x0002EEEF
		public static string DataverseRequest_GetDatasetDetails
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetDatasetDetails");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00030CFB File Offset: 0x0002EEFB
		public static string DataverseRequest_UnexpectedResponse
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_UnexpectedResponse");
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00030D07 File Offset: 0x0002EF07
		public static string Exception_AssemblyMissingInEmbeddedResources(string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_AssemblyMissingInEmbeddedResources", assembly);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00030D14 File Offset: 0x0002EF14
		public static string Exception_TypeMissingInAdalAssembly(string typeName, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_TypeMissingInAdalAssembly", typeName, assembly);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00030D22 File Offset: 0x0002EF22
		public static string Exception_EnumValueMissingInAdalAssembly(string enumName, string enumValue, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_EnumValueMissingInAdalAssembly", enumName, enumValue, assembly);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00030D31 File Offset: 0x0002EF31
		public static string Exception_PropertyMissingInAdalAssembly(string typeName, string property, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_PropertyMissingInAdalAssembly", typeName, property, assembly);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00030D40 File Offset: 0x0002EF40
		public static string Exception_MethodMissingInAdalAssembly(string typeName, string method, string args, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_MethodMissingInAdalAssembly", typeName, method, args, assembly);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00030D50 File Offset: 0x0002EF50
		public static string Exception_InvalidIdentityProvider(string provider)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidIdentityProvider", provider);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00030D5D File Offset: 0x0002EF5D
		public static string Exception_InvalidAuthorityFormat(string authority)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidAuthorityFormat", authority);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00030D6A File Offset: 0x0002EF6A
		public static string Exception_InvalidEndpointType(string endpoint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidEndpointType", endpoint);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00030D77 File Offset: 0x0002EF77
		public static string Exception_InvalidServiceAppId(string id)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidServiceAppId", id);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00030D84 File Offset: 0x0002EF84
		public static string Exception_InvalidCertificateThumbprint(string thumbprint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidCertificateThumbprint", thumbprint);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00030D91 File Offset: 0x0002EF91
		public static string Exception_CantObtainUserInfoOnTokenRefresh(string username)
		{
			return AuthenticationSR.Keys.GetString("Exception_CantObtainUserInfoOnTokenRefresh", username);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00030D9E File Offset: 0x0002EF9E
		public static string Exception_PbiRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_PbiRequestFailed", action, description, details, eol);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00030DAE File Offset: 0x0002EFAE
		public static string Exception_DataverseRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_DataverseRequestFailed", action, description, details, eol);
		}

		// Token: 0x020001CC RID: 460
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060013C9 RID: 5065 RVA: 0x00045273 File Offset: 0x00043473
			private Keys()
			{
			}

			// Token: 0x170006EA RID: 1770
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x0004527B File Offset: 0x0004347B
			// (set) Token: 0x060013CB RID: 5067 RVA: 0x00045282 File Offset: 0x00043482
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

			// Token: 0x060013CC RID: 5068 RVA: 0x0004528A File Offset: 0x0004348A
			public static string GetString(string key)
			{
				return AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture);
			}

			// Token: 0x060013CD RID: 5069 RVA: 0x0004529C File Offset: 0x0004349C
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0);
			}

			// Token: 0x060013CE RID: 5070 RVA: 0x000452B9 File Offset: 0x000434B9
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x060013CF RID: 5071 RVA: 0x000452D7 File Offset: 0x000434D7
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x060013D0 RID: 5072 RVA: 0x000452F6 File Offset: 0x000434F6
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x04000D13 RID: 3347
			private static ResourceManager resourceManager = new ResourceManager(typeof(AuthenticationSR).FullName, typeof(AuthenticationSR).Module.Assembly);

			// Token: 0x04000D14 RID: 3348
			private static CultureInfo _culture = null;

			// Token: 0x04000D15 RID: 3349
			public const string Exception_AcquireTokenFailure = "Exception_AcquireTokenFailure";

			// Token: 0x04000D16 RID: 3350
			public const string Exception_InteractiveLoginRequired = "Exception_InteractiveLoginRequired";

			// Token: 0x04000D17 RID: 3351
			public const string Exception_MFARequiredAndSupported = "Exception_MFARequiredAndSupported";

			// Token: 0x04000D18 RID: 3352
			public const string Exception_AssemblyMissingInEmbeddedResources = "Exception_AssemblyMissingInEmbeddedResources";

			// Token: 0x04000D19 RID: 3353
			public const string Exception_TypeMissingInAdalAssembly = "Exception_TypeMissingInAdalAssembly";

			// Token: 0x04000D1A RID: 3354
			public const string Exception_EnumValueMissingInAdalAssembly = "Exception_EnumValueMissingInAdalAssembly";

			// Token: 0x04000D1B RID: 3355
			public const string Exception_PropertyMissingInAdalAssembly = "Exception_PropertyMissingInAdalAssembly";

			// Token: 0x04000D1C RID: 3356
			public const string Exception_MethodMissingInAdalAssembly = "Exception_MethodMissingInAdalAssembly";

			// Token: 0x04000D1D RID: 3357
			public const string Exception_UnknownAuthenticationInfo = "Exception_UnknownAuthenticationInfo";

			// Token: 0x04000D1E RID: 3358
			public const string Exception_InvalidIdentityProvider = "Exception_InvalidIdentityProvider";

			// Token: 0x04000D1F RID: 3359
			public const string Exception_InvalidAuthorityFormat = "Exception_InvalidAuthorityFormat";

			// Token: 0x04000D20 RID: 3360
			public const string Exception_RedirectionInfoNotFound = "Exception_RedirectionInfoNotFound";

			// Token: 0x04000D21 RID: 3361
			public const string Exception_SpnAuthenticationWithoutPassword = "Exception_SpnAuthenticationWithoutPassword";

			// Token: 0x04000D22 RID: 3362
			public const string Exception_UserAuthenticationWithServicePrincipalProfile = "Exception_UserAuthenticationWithServicePrincipalProfile";

			// Token: 0x04000D23 RID: 3363
			public const string Exception_SpnAuthenticationWithoutTenant = "Exception_SpnAuthenticationWithoutTenant";

			// Token: 0x04000D24 RID: 3364
			public const string Exception_AdalIsNoLongerSupported = "Exception_AdalIsNoLongerSupported";

			// Token: 0x04000D25 RID: 3365
			public const string Exception_InvalidEndpointType = "Exception_InvalidEndpointType";

			// Token: 0x04000D26 RID: 3366
			public const string Exception_InvalidServiceAppId = "Exception_InvalidServiceAppId";

			// Token: 0x04000D27 RID: 3367
			public const string Exception_InvalidCertificateThumbprint = "Exception_InvalidCertificateThumbprint";

			// Token: 0x04000D28 RID: 3368
			public const string Exception_ADTranslationNotSupportedOnNonWindows = "Exception_ADTranslationNotSupportedOnNonWindows";

			// Token: 0x04000D29 RID: 3369
			public const string Exception_CantObtainUserInfoOnTokenRefresh = "Exception_CantObtainUserInfoOnTokenRefresh";

			// Token: 0x04000D2A RID: 3370
			public const string Exception_MissingDeviceCodeCallback = "Exception_MissingDeviceCodeCallback";

			// Token: 0x04000D2B RID: 3371
			public const string Exception_S2STokenMissing = "Exception_S2STokenMissing";

			// Token: 0x04000D2C RID: 3372
			public const string Exception_InvalidPBIPCapacity = "Exception_InvalidPBIPCapacity";

			// Token: 0x04000D2D RID: 3373
			public const string Exception_RedirectionTokenAsPasswordIsNotSupported = "Exception_RedirectionTokenAsPasswordIsNotSupported";

			// Token: 0x04000D2E RID: 3374
			public const string Exception_PbiRequestFailed = "Exception_PbiRequestFailed";

			// Token: 0x04000D2F RID: 3375
			public const string Exception_RedirectionFailWithThrottling = "Exception_RedirectionFailWithThrottling";

			// Token: 0x04000D30 RID: 3376
			public const string Exception_DataverseRequestFailed = "Exception_DataverseRequestFailed";

			// Token: 0x04000D31 RID: 3377
			public const string PbiRequest_GetMwcToken = "PbiRequest_GetMwcToken";

			// Token: 0x04000D32 RID: 3378
			public const string PbiRequest_GetRedirectInfo = "PbiRequest_GetRedirectInfo";

			// Token: 0x04000D33 RID: 3379
			public const string PbiRequest_ResolveWorkspace = "PbiRequest_ResolveWorkspace";

			// Token: 0x04000D34 RID: 3380
			public const string PbiRequest_GetDatabaseName = "PbiRequest_GetDatabaseName";

			// Token: 0x04000D35 RID: 3381
			public const string DataverseRequest_GetEmbeddedToken = "DataverseRequest_GetEmbeddedToken";

			// Token: 0x04000D36 RID: 3382
			public const string DataverseRequest_GetDatasetDetails = "DataverseRequest_GetDatasetDetails";

			// Token: 0x04000D37 RID: 3383
			public const string DataverseRequest_UnexpectedResponse = "DataverseRequest_UnexpectedResponse";
		}
	}
}
