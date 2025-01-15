using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000013 RID: 19
	[CompilerGenerated]
	internal class AuthenticationSR
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000028E3 File Offset: 0x00000AE3
		protected AuthenticationSR()
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028EB File Offset: 0x00000AEB
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000028F2 File Offset: 0x00000AF2
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028FA File Offset: 0x00000AFA
		public static string Exception_AcquireTokenFailure
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AcquireTokenFailure");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002906 File Offset: 0x00000B06
		public static string Exception_InteractiveLoginRequired
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InteractiveLoginRequired");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002912 File Offset: 0x00000B12
		public static string Exception_MFARequiredAndSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MFARequiredAndSupported");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000291E File Offset: 0x00000B1E
		public static string Exception_UnknownAuthenticationInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UnknownAuthenticationInfo");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000292A File Offset: 0x00000B2A
		public static string Exception_RedirectionInfoNotFound
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionInfoNotFound");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002936 File Offset: 0x00000B36
		public static string Exception_SpnAuthenticationWithoutPassword
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutPassword");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002942 File Offset: 0x00000B42
		public static string Exception_UserAuthenticationWithServicePrincipalProfile
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_UserAuthenticationWithServicePrincipalProfile");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000294E File Offset: 0x00000B4E
		public static string Exception_SpnAuthenticationWithoutTenant
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_SpnAuthenticationWithoutTenant");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000295A File Offset: 0x00000B5A
		public static string Exception_AdalIsNoLongerSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_AdalIsNoLongerSupported");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002966 File Offset: 0x00000B66
		public static string Exception_ADTranslationNotSupportedOnNonWindows
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_ADTranslationNotSupportedOnNonWindows");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002972 File Offset: 0x00000B72
		public static string Exception_MissingDeviceCodeCallback
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_MissingDeviceCodeCallback");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000297E File Offset: 0x00000B7E
		public static string Exception_S2STokenMissing
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_S2STokenMissing");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000298A File Offset: 0x00000B8A
		public static string Exception_InvalidPBIPCapacity
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_InvalidPBIPCapacity");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002996 File Offset: 0x00000B96
		public static string Exception_RedirectionTokenAsPasswordIsNotSupported
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionTokenAsPasswordIsNotSupported");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000029A2 File Offset: 0x00000BA2
		public static string Exception_RedirectionFailWithThrottling
		{
			get
			{
				return AuthenticationSR.Keys.GetString("Exception_RedirectionFailWithThrottling");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000029AE File Offset: 0x00000BAE
		public static string PbiRequest_GetMwcToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetMwcToken");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000029BA File Offset: 0x00000BBA
		public static string PbiRequest_GetRedirectInfo
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetRedirectInfo");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000029C6 File Offset: 0x00000BC6
		public static string PbiRequest_ResolveWorkspace
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_ResolveWorkspace");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000029D2 File Offset: 0x00000BD2
		public static string PbiRequest_GetDatabaseName
		{
			get
			{
				return AuthenticationSR.Keys.GetString("PbiRequest_GetDatabaseName");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000029DE File Offset: 0x00000BDE
		public static string DataverseRequest_GetEmbeddedToken
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetEmbeddedToken");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000029EA File Offset: 0x00000BEA
		public static string DataverseRequest_GetDatasetDetails
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_GetDatasetDetails");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000029F6 File Offset: 0x00000BF6
		public static string DataverseRequest_UnexpectedResponse
		{
			get
			{
				return AuthenticationSR.Keys.GetString("DataverseRequest_UnexpectedResponse");
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A02 File Offset: 0x00000C02
		public static string Exception_AssemblyMissingInEmbeddedResources(string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_AssemblyMissingInEmbeddedResources", assembly);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002A0F File Offset: 0x00000C0F
		public static string Exception_TypeMissingInAdalAssembly(string typeName, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_TypeMissingInAdalAssembly", typeName, assembly);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002A1D File Offset: 0x00000C1D
		public static string Exception_EnumValueMissingInAdalAssembly(string enumName, string enumValue, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_EnumValueMissingInAdalAssembly", enumName, enumValue, assembly);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static string Exception_PropertyMissingInAdalAssembly(string typeName, string property, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_PropertyMissingInAdalAssembly", typeName, property, assembly);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002A3B File Offset: 0x00000C3B
		public static string Exception_MethodMissingInAdalAssembly(string typeName, string method, string args, string assembly)
		{
			return AuthenticationSR.Keys.GetString("Exception_MethodMissingInAdalAssembly", typeName, method, args, assembly);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002A4B File Offset: 0x00000C4B
		public static string Exception_InvalidIdentityProvider(string provider)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidIdentityProvider", provider);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002A58 File Offset: 0x00000C58
		public static string Exception_InvalidAuthorityFormat(string authority)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidAuthorityFormat", authority);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002A65 File Offset: 0x00000C65
		public static string Exception_InvalidEndpointType(string endpoint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidEndpointType", endpoint);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A72 File Offset: 0x00000C72
		public static string Exception_InvalidServiceAppId(string id)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidServiceAppId", id);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002A7F File Offset: 0x00000C7F
		public static string Exception_InvalidCertificateThumbprint(string thumbprint)
		{
			return AuthenticationSR.Keys.GetString("Exception_InvalidCertificateThumbprint", thumbprint);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static string Exception_CantObtainUserInfoOnTokenRefresh(string username)
		{
			return AuthenticationSR.Keys.GetString("Exception_CantObtainUserInfoOnTokenRefresh", username);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002A99 File Offset: 0x00000C99
		public static string Exception_PbiRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_PbiRequestFailed", action, description, details, eol);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002AA9 File Offset: 0x00000CA9
		public static string Exception_DataverseRequestFailed(string action, string description, string details, string eol)
		{
			return AuthenticationSR.Keys.GetString("Exception_DataverseRequestFailed", action, description, details, eol);
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000217 RID: 535 RVA: 0x0000ABF7 File Offset: 0x00008DF7
			private Keys()
			{
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000218 RID: 536 RVA: 0x0000ABFF File Offset: 0x00008DFF
			// (set) Token: 0x06000219 RID: 537 RVA: 0x0000AC06 File Offset: 0x00008E06
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

			// Token: 0x0600021A RID: 538 RVA: 0x0000AC0E File Offset: 0x00008E0E
			public static string GetString(string key)
			{
				return AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture);
			}

			// Token: 0x0600021B RID: 539 RVA: 0x0000AC20 File Offset: 0x00008E20
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0 });
			}

			// Token: 0x0600021C RID: 540 RVA: 0x0000AC46 File Offset: 0x00008E46
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1 });
			}

			// Token: 0x0600021D RID: 541 RVA: 0x0000AC70 File Offset: 0x00008E70
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1, arg2 });
			}

			// Token: 0x0600021E RID: 542 RVA: 0x0000AC9E File Offset: 0x00008E9E
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, AuthenticationSR.Keys.resourceManager.GetString(key, AuthenticationSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x04000164 RID: 356
			private static ResourceManager resourceManager = new ResourceManager(typeof(AuthenticationSR).FullName, typeof(AuthenticationSR).Module.Assembly);

			// Token: 0x04000165 RID: 357
			private static CultureInfo _culture = null;

			// Token: 0x04000166 RID: 358
			public const string Exception_AcquireTokenFailure = "Exception_AcquireTokenFailure";

			// Token: 0x04000167 RID: 359
			public const string Exception_InteractiveLoginRequired = "Exception_InteractiveLoginRequired";

			// Token: 0x04000168 RID: 360
			public const string Exception_MFARequiredAndSupported = "Exception_MFARequiredAndSupported";

			// Token: 0x04000169 RID: 361
			public const string Exception_AssemblyMissingInEmbeddedResources = "Exception_AssemblyMissingInEmbeddedResources";

			// Token: 0x0400016A RID: 362
			public const string Exception_TypeMissingInAdalAssembly = "Exception_TypeMissingInAdalAssembly";

			// Token: 0x0400016B RID: 363
			public const string Exception_EnumValueMissingInAdalAssembly = "Exception_EnumValueMissingInAdalAssembly";

			// Token: 0x0400016C RID: 364
			public const string Exception_PropertyMissingInAdalAssembly = "Exception_PropertyMissingInAdalAssembly";

			// Token: 0x0400016D RID: 365
			public const string Exception_MethodMissingInAdalAssembly = "Exception_MethodMissingInAdalAssembly";

			// Token: 0x0400016E RID: 366
			public const string Exception_UnknownAuthenticationInfo = "Exception_UnknownAuthenticationInfo";

			// Token: 0x0400016F RID: 367
			public const string Exception_InvalidIdentityProvider = "Exception_InvalidIdentityProvider";

			// Token: 0x04000170 RID: 368
			public const string Exception_InvalidAuthorityFormat = "Exception_InvalidAuthorityFormat";

			// Token: 0x04000171 RID: 369
			public const string Exception_RedirectionInfoNotFound = "Exception_RedirectionInfoNotFound";

			// Token: 0x04000172 RID: 370
			public const string Exception_SpnAuthenticationWithoutPassword = "Exception_SpnAuthenticationWithoutPassword";

			// Token: 0x04000173 RID: 371
			public const string Exception_UserAuthenticationWithServicePrincipalProfile = "Exception_UserAuthenticationWithServicePrincipalProfile";

			// Token: 0x04000174 RID: 372
			public const string Exception_SpnAuthenticationWithoutTenant = "Exception_SpnAuthenticationWithoutTenant";

			// Token: 0x04000175 RID: 373
			public const string Exception_AdalIsNoLongerSupported = "Exception_AdalIsNoLongerSupported";

			// Token: 0x04000176 RID: 374
			public const string Exception_InvalidEndpointType = "Exception_InvalidEndpointType";

			// Token: 0x04000177 RID: 375
			public const string Exception_InvalidServiceAppId = "Exception_InvalidServiceAppId";

			// Token: 0x04000178 RID: 376
			public const string Exception_InvalidCertificateThumbprint = "Exception_InvalidCertificateThumbprint";

			// Token: 0x04000179 RID: 377
			public const string Exception_ADTranslationNotSupportedOnNonWindows = "Exception_ADTranslationNotSupportedOnNonWindows";

			// Token: 0x0400017A RID: 378
			public const string Exception_CantObtainUserInfoOnTokenRefresh = "Exception_CantObtainUserInfoOnTokenRefresh";

			// Token: 0x0400017B RID: 379
			public const string Exception_MissingDeviceCodeCallback = "Exception_MissingDeviceCodeCallback";

			// Token: 0x0400017C RID: 380
			public const string Exception_S2STokenMissing = "Exception_S2STokenMissing";

			// Token: 0x0400017D RID: 381
			public const string Exception_InvalidPBIPCapacity = "Exception_InvalidPBIPCapacity";

			// Token: 0x0400017E RID: 382
			public const string Exception_RedirectionTokenAsPasswordIsNotSupported = "Exception_RedirectionTokenAsPasswordIsNotSupported";

			// Token: 0x0400017F RID: 383
			public const string Exception_PbiRequestFailed = "Exception_PbiRequestFailed";

			// Token: 0x04000180 RID: 384
			public const string Exception_RedirectionFailWithThrottling = "Exception_RedirectionFailWithThrottling";

			// Token: 0x04000181 RID: 385
			public const string Exception_DataverseRequestFailed = "Exception_DataverseRequestFailed";

			// Token: 0x04000182 RID: 386
			public const string PbiRequest_GetMwcToken = "PbiRequest_GetMwcToken";

			// Token: 0x04000183 RID: 387
			public const string PbiRequest_GetRedirectInfo = "PbiRequest_GetRedirectInfo";

			// Token: 0x04000184 RID: 388
			public const string PbiRequest_ResolveWorkspace = "PbiRequest_ResolveWorkspace";

			// Token: 0x04000185 RID: 389
			public const string PbiRequest_GetDatabaseName = "PbiRequest_GetDatabaseName";

			// Token: 0x04000186 RID: 390
			public const string DataverseRequest_GetEmbeddedToken = "DataverseRequest_GetEmbeddedToken";

			// Token: 0x04000187 RID: 391
			public const string DataverseRequest_GetDatasetDetails = "DataverseRequest_GetDatasetDetails";

			// Token: 0x04000188 RID: 392
			public const string DataverseRequest_UnexpectedResponse = "DataverseRequest_UnexpectedResponse";
		}
	}
}
