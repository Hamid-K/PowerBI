using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.WmiProvider
{
	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	internal class WMIProviderExceptionStrings
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
		protected WMIProviderExceptionStrings()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002178 File Offset: 0x00000378
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000217F File Offset: 0x0000037F
		public static CultureInfo Culture
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.Culture;
			}
			set
			{
				WMIProviderExceptionStrings.Keys.Culture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002187 File Offset: 0x00000387
		public static string GenericWMIError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("GenericWMIError");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002193 File Offset: 0x00000393
		public static string ManagementException_AccessDenied
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ManagementException_AccessDenied");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219F File Offset: 0x0000039F
		public static string ErrorCodes_Success
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_Success");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AB File Offset: 0x000003AB
		public static string ErrorCodes_ServiceNotActivated
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ServiceNotActivated");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public static string ErrorCodes_ServiceDisabled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ServiceDisabled");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C3 File Offset: 0x000003C3
		public static string ErrorCodes_UnexpectedDatabaseError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnexpectedDatabaseError");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021CF File Offset: 0x000003CF
		public static string ErrorCodes_UnexpectedDatabaseResult
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnexpectedDatabaseResult");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021DB File Offset: 0x000003DB
		public static string ErrorCodes_VirtualDirectoryInvalid
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_VirtualDirectoryInvalid");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021E7 File Offset: 0x000003E7
		public static string ErrorCodes_VirtualDirectoryAlreadyExists
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_VirtualDirectoryAlreadyExists");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F3 File Offset: 0x000003F3
		public static string ErrorCodes_CantConnectCatalog
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_CantConnectCatalog");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021FF File Offset: 0x000003FF
		public static string ErrorCodes_AlreadyActivated
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_AlreadyActivated");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000220B File Offset: 0x0000040B
		public static string ErrorCodes_NotAnnounced
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotAnnounced");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002217 File Offset: 0x00000417
		public static string ErrorCodes_NotAdmin
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotAdmin");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002223 File Offset: 0x00000423
		public static string ErrorCodes_InvalidIISPath
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidIISPath");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000222F File Offset: 0x0000042F
		public static string ErrorCodes_IISNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_IISNotInstalled");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000223B File Offset: 0x0000043B
		public static string ErrorCodes_ASPNetNotRegistered
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ASPNetNotRegistered");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002247 File Offset: 0x00000447
		public static string ErrorCodes_InvalidUser
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidUser");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002253 File Offset: 0x00000453
		public static string ErrorCodes_InsufficientPrivilege
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InsufficientPrivilege");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000225F File Offset: 0x0000045F
		public static string ErrorCodes_SqlAdminUserInsufficientPrivilege
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SqlAdminUserInsufficientPrivilege");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000226B File Offset: 0x0000046B
		public static string ErrorCodes_GrantLoginToPasswordRequired
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_GrantLoginToPasswordRequired");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002277 File Offset: 0x00000477
		public static string ErrorCodes_RsexecRoleDoesNotExist
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsexecRoleDoesNotExist");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002283 File Offset: 0x00000483
		public static string ErrorCodes_WindowsServiceAccountNotSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_WindowsServiceAccountNotSet");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000228F File Offset: 0x0000048F
		public static string ErrorCodes_WebServiceAccountNotSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_WebServiceAccountNotSet");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000229B File Offset: 0x0000049B
		public static string ErrorCodes_SslCertificateNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SslCertificateNotFound");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022A7 File Offset: 0x000004A7
		public static string ErrorCodes_ConfigurationFileNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ConfigurationFileNotFound");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022B3 File Offset: 0x000004B3
		public static string ErrorCodes_InvalidPortNumber
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidPortNumber");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022BF File Offset: 0x000004BF
		public static string ErrorCodes_InvalidUrlParameterSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidUrlParameterSet");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022CB File Offset: 0x000004CB
		public static string ErrorCodes_InvalidSecureConnectionLevel
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidSecureConnectionLevel");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022D7 File Offset: 0x000004D7
		public static string ErrorCodes_IllformedAccountString
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_IllformedAccountString");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022E3 File Offset: 0x000004E3
		public static string ErrorCodes_BadCredentialsType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadCredentialsType");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022EF File Offset: 0x000004EF
		public static string ErrorCodes_BadApplicationPool
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadApplicationPool");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022FB File Offset: 0x000004FB
		public static string ErrorCodes_BadApplicationPoolName
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadApplicationPoolName");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002307 File Offset: 0x00000507
		public static string ErrorCodes_BadVerson
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadVerson");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002313 File Offset: 0x00000513
		public static string ErrorCodes_InvalidVersionString
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidVersionString");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000231F File Offset: 0x0000051F
		public static string ErrorCodes_BadLcid
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadLcid");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000232B File Offset: 0x0000052B
		public static string ErrorCodes_ApplicationPoolAlreadyExists
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ApplicationPoolAlreadyExists");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002337 File Offset: 0x00000537
		public static string ErrorCodes_OperatingSystemNotSupported
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_OperatingSystemNotSupported");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002343 File Offset: 0x00000543
		public static string ErrorCodes_BadWebsiteConfiguration
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadWebsiteConfiguration");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000234F File Offset: 0x0000054F
		public static string ErrorCodes_BadConfigurationFile
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadConfigurationFile");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000235B File Offset: 0x0000055B
		public static string ErrorCodes_SharePointNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SharePointNotInstalled");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002367 File Offset: 0x00000567
		public static string ErrorCodes_MustCreateVirtualDirectory
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_MustCreateVirtualDirectory");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002373 File Offset: 0x00000573
		public static string ErrorCodes_FailedToLoadResources
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_FailedToLoadResources");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000237F File Offset: 0x0000057F
		public static string ErrorCodes_RPCServerNotListening
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RPCServerNotListening");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000238B File Offset: 0x0000058B
		public static string ErrorCodes_FailedToEnumerateInstances
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_FailedToEnumerateInstances");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002397 File Offset: 0x00000597
		public static string ErrorCodes_InvalidParameter
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidParameter");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023A3 File Offset: 0x000005A3
		public static string ErrorCodes_LocalServiceIsLocalOnly
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_LocalServiceIsLocalOnly");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023AF File Offset: 0x000005AF
		public static string ErrorCodes_NotSupportedInSharePointMode
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotSupportedInSharePointMode");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023BB File Offset: 0x000005BB
		public static string ErrorCodes_NotSupportedInNativeMode
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotSupportedInNativeMode");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023C7 File Offset: 0x000005C7
		public static string ErrorCodes_RsSharePointObjectModelNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSharePointObjectModelNotInstalled");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000023D3 File Offset: 0x000005D3
		public static string ErrorCodes_RsSharePointError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSharePointError");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023DF File Offset: 0x000005DF
		public static string ErrorCodes_RsServerConfigurationError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsServerConfigurationError");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000023EB File Offset: 0x000005EB
		public static string ErrorCodes_RsUrlAlreadyReservedDifferentName
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsUrlAlreadyReservedDifferentName");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023F7 File Offset: 0x000005F7
		public static string ErrorCodes_RsMustDefineApplicationFirst
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsMustDefineApplicationFirst");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002403 File Offset: 0x00000603
		public static string ErrorCodes_RsIpAddressNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsIpAddressNotFound");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000240F File Offset: 0x0000060F
		public static string ErrorCodes_RsSSLBindingConflict
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSSLBindingConflict");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000241B File Offset: 0x0000061B
		public static string ErrorCodes_RsSSLCertificateNotRegistered
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSSLCertificateNotRegistered");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002427 File Offset: 0x00000627
		public static string ErrorCodes_RsInvalidApplication
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsInvalidApplication");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002433 File Offset: 0x00000633
		public static string ErrorCodes_RsURLAlreadyReserved
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLAlreadyReserved");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000243F File Offset: 0x0000063F
		public static string ErrorCodes_RsURLNotReserved
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLNotReserved");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000244B File Offset: 0x0000064B
		public static string ErrorCodes_RsURLMustNotExist
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLMustNotExist");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002457 File Offset: 0x00000657
		public static string ErrorCodes_RsDeliveryExtensionNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsDeliveryExtensionNotFound");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002463 File Offset: 0x00000663
		public static string ErrorCodes_RsLocalServiceNotAllowedXP
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsLocalServiceNotAllowedXP");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000246F File Offset: 0x0000066F
		public static string ErrorCodes_RsInvalidSSLCertificate
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsInvalidSSLCertificate");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000247B File Offset: 0x0000067B
		public static string ErrorCodes_RsBadExtendedProtectionLevelType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsBadExtendedProtectionLevelType");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002487 File Offset: 0x00000687
		public static string ErrorCodes_RsBadExtendedProtectionScenarioType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsBadExtendedProtectionScenarioType");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002493 File Offset: 0x00000693
		public static string ErrorCodes_RsMustDefineAuthenticationFirst
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsMustDefineAuthenticationFirst");
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000249F File Offset: 0x0000069F
		public static string GenericWMIErrorWithCode(string errorCode)
		{
			return WMIProviderExceptionStrings.Keys.GetString("GenericWMIErrorWithCode", errorCode);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000024AC File Offset: 0x000006AC
		public static string UnableToConnect(string serverName)
		{
			return WMIProviderExceptionStrings.Keys.GetString("UnableToConnect", serverName);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000024B9 File Offset: 0x000006B9
		public static string ManagementException_Generic(string wmiError)
		{
			return WMIProviderExceptionStrings.Keys.GetString("ManagementException_Generic", wmiError);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000024C6 File Offset: 0x000006C6
		public static string Exception_Generic(string genericError)
		{
			return WMIProviderExceptionStrings.Keys.GetString("Exception_Generic", genericError);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000024D3 File Offset: 0x000006D3
		public static string ErrorCodes_UnknownOrUnmappedException(int errorCode)
		{
			return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnknownOrUnmappedException", errorCode);
		}

		// Token: 0x02000009 RID: 9
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000053 RID: 83 RVA: 0x00002170 File Offset: 0x00000370
			private Keys()
			{
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000054 RID: 84 RVA: 0x000025BF File Offset: 0x000007BF
			// (set) Token: 0x06000055 RID: 85 RVA: 0x000025C6 File Offset: 0x000007C6
			public static CultureInfo Culture
			{
				get
				{
					return WMIProviderExceptionStrings.Keys._culture;
				}
				set
				{
					WMIProviderExceptionStrings.Keys._culture = value;
				}
			}

			// Token: 0x06000056 RID: 86 RVA: 0x000025CE File Offset: 0x000007CE
			public static string GetString(string key)
			{
				return WMIProviderExceptionStrings.Keys.resourceManager.GetString(key, WMIProviderExceptionStrings.Keys._culture);
			}

			// Token: 0x06000057 RID: 87 RVA: 0x000025E0 File Offset: 0x000007E0
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, WMIProviderExceptionStrings.Keys.resourceManager.GetString(key, WMIProviderExceptionStrings.Keys._culture), arg0);
			}

			// Token: 0x04000043 RID: 67
			private static ResourceManager resourceManager = new ResourceManager(typeof(WMIProviderExceptionStrings).FullName, typeof(WMIProviderExceptionStrings).Module.Assembly);

			// Token: 0x04000044 RID: 68
			private static CultureInfo _culture = null;

			// Token: 0x04000045 RID: 69
			public const string GenericWMIErrorWithCode = "GenericWMIErrorWithCode";

			// Token: 0x04000046 RID: 70
			public const string GenericWMIError = "GenericWMIError";

			// Token: 0x04000047 RID: 71
			public const string UnableToConnect = "UnableToConnect";

			// Token: 0x04000048 RID: 72
			public const string ManagementException_AccessDenied = "ManagementException_AccessDenied";

			// Token: 0x04000049 RID: 73
			public const string ManagementException_Generic = "ManagementException_Generic";

			// Token: 0x0400004A RID: 74
			public const string Exception_Generic = "Exception_Generic";

			// Token: 0x0400004B RID: 75
			public const string ErrorCodes_UnknownOrUnmappedException = "ErrorCodes_UnknownOrUnmappedException";

			// Token: 0x0400004C RID: 76
			public const string ErrorCodes_Success = "ErrorCodes_Success";

			// Token: 0x0400004D RID: 77
			public const string ErrorCodes_ServiceNotActivated = "ErrorCodes_ServiceNotActivated";

			// Token: 0x0400004E RID: 78
			public const string ErrorCodes_ServiceDisabled = "ErrorCodes_ServiceDisabled";

			// Token: 0x0400004F RID: 79
			public const string ErrorCodes_UnexpectedDatabaseError = "ErrorCodes_UnexpectedDatabaseError";

			// Token: 0x04000050 RID: 80
			public const string ErrorCodes_UnexpectedDatabaseResult = "ErrorCodes_UnexpectedDatabaseResult";

			// Token: 0x04000051 RID: 81
			public const string ErrorCodes_VirtualDirectoryInvalid = "ErrorCodes_VirtualDirectoryInvalid";

			// Token: 0x04000052 RID: 82
			public const string ErrorCodes_VirtualDirectoryAlreadyExists = "ErrorCodes_VirtualDirectoryAlreadyExists";

			// Token: 0x04000053 RID: 83
			public const string ErrorCodes_CantConnectCatalog = "ErrorCodes_CantConnectCatalog";

			// Token: 0x04000054 RID: 84
			public const string ErrorCodes_AlreadyActivated = "ErrorCodes_AlreadyActivated";

			// Token: 0x04000055 RID: 85
			public const string ErrorCodes_NotAnnounced = "ErrorCodes_NotAnnounced";

			// Token: 0x04000056 RID: 86
			public const string ErrorCodes_NotAdmin = "ErrorCodes_NotAdmin";

			// Token: 0x04000057 RID: 87
			public const string ErrorCodes_InvalidIISPath = "ErrorCodes_InvalidIISPath";

			// Token: 0x04000058 RID: 88
			public const string ErrorCodes_IISNotInstalled = "ErrorCodes_IISNotInstalled";

			// Token: 0x04000059 RID: 89
			public const string ErrorCodes_ASPNetNotRegistered = "ErrorCodes_ASPNetNotRegistered";

			// Token: 0x0400005A RID: 90
			public const string ErrorCodes_InvalidUser = "ErrorCodes_InvalidUser";

			// Token: 0x0400005B RID: 91
			public const string ErrorCodes_InsufficientPrivilege = "ErrorCodes_InsufficientPrivilege";

			// Token: 0x0400005C RID: 92
			public const string ErrorCodes_SqlAdminUserInsufficientPrivilege = "ErrorCodes_SqlAdminUserInsufficientPrivilege";

			// Token: 0x0400005D RID: 93
			public const string ErrorCodes_GrantLoginToPasswordRequired = "ErrorCodes_GrantLoginToPasswordRequired";

			// Token: 0x0400005E RID: 94
			public const string ErrorCodes_RsexecRoleDoesNotExist = "ErrorCodes_RsexecRoleDoesNotExist";

			// Token: 0x0400005F RID: 95
			public const string ErrorCodes_WindowsServiceAccountNotSet = "ErrorCodes_WindowsServiceAccountNotSet";

			// Token: 0x04000060 RID: 96
			public const string ErrorCodes_WebServiceAccountNotSet = "ErrorCodes_WebServiceAccountNotSet";

			// Token: 0x04000061 RID: 97
			public const string ErrorCodes_SslCertificateNotFound = "ErrorCodes_SslCertificateNotFound";

			// Token: 0x04000062 RID: 98
			public const string ErrorCodes_ConfigurationFileNotFound = "ErrorCodes_ConfigurationFileNotFound";

			// Token: 0x04000063 RID: 99
			public const string ErrorCodes_InvalidPortNumber = "ErrorCodes_InvalidPortNumber";

			// Token: 0x04000064 RID: 100
			public const string ErrorCodes_InvalidUrlParameterSet = "ErrorCodes_InvalidUrlParameterSet";

			// Token: 0x04000065 RID: 101
			public const string ErrorCodes_InvalidSecureConnectionLevel = "ErrorCodes_InvalidSecureConnectionLevel";

			// Token: 0x04000066 RID: 102
			public const string ErrorCodes_IllformedAccountString = "ErrorCodes_IllformedAccountString";

			// Token: 0x04000067 RID: 103
			public const string ErrorCodes_BadCredentialsType = "ErrorCodes_BadCredentialsType";

			// Token: 0x04000068 RID: 104
			public const string ErrorCodes_BadApplicationPool = "ErrorCodes_BadApplicationPool";

			// Token: 0x04000069 RID: 105
			public const string ErrorCodes_BadApplicationPoolName = "ErrorCodes_BadApplicationPoolName";

			// Token: 0x0400006A RID: 106
			public const string ErrorCodes_BadVerson = "ErrorCodes_BadVerson";

			// Token: 0x0400006B RID: 107
			public const string ErrorCodes_InvalidVersionString = "ErrorCodes_InvalidVersionString";

			// Token: 0x0400006C RID: 108
			public const string ErrorCodes_BadLcid = "ErrorCodes_BadLcid";

			// Token: 0x0400006D RID: 109
			public const string ErrorCodes_ApplicationPoolAlreadyExists = "ErrorCodes_ApplicationPoolAlreadyExists";

			// Token: 0x0400006E RID: 110
			public const string ErrorCodes_OperatingSystemNotSupported = "ErrorCodes_OperatingSystemNotSupported";

			// Token: 0x0400006F RID: 111
			public const string ErrorCodes_BadWebsiteConfiguration = "ErrorCodes_BadWebsiteConfiguration";

			// Token: 0x04000070 RID: 112
			public const string ErrorCodes_BadConfigurationFile = "ErrorCodes_BadConfigurationFile";

			// Token: 0x04000071 RID: 113
			public const string ErrorCodes_SharePointNotInstalled = "ErrorCodes_SharePointNotInstalled";

			// Token: 0x04000072 RID: 114
			public const string ErrorCodes_MustCreateVirtualDirectory = "ErrorCodes_MustCreateVirtualDirectory";

			// Token: 0x04000073 RID: 115
			public const string ErrorCodes_FailedToLoadResources = "ErrorCodes_FailedToLoadResources";

			// Token: 0x04000074 RID: 116
			public const string ErrorCodes_RPCServerNotListening = "ErrorCodes_RPCServerNotListening";

			// Token: 0x04000075 RID: 117
			public const string ErrorCodes_FailedToEnumerateInstances = "ErrorCodes_FailedToEnumerateInstances";

			// Token: 0x04000076 RID: 118
			public const string ErrorCodes_InvalidParameter = "ErrorCodes_InvalidParameter";

			// Token: 0x04000077 RID: 119
			public const string ErrorCodes_LocalServiceIsLocalOnly = "ErrorCodes_LocalServiceIsLocalOnly";

			// Token: 0x04000078 RID: 120
			public const string ErrorCodes_NotSupportedInSharePointMode = "ErrorCodes_NotSupportedInSharePointMode";

			// Token: 0x04000079 RID: 121
			public const string ErrorCodes_NotSupportedInNativeMode = "ErrorCodes_NotSupportedInNativeMode";

			// Token: 0x0400007A RID: 122
			public const string ErrorCodes_RsSharePointObjectModelNotInstalled = "ErrorCodes_RsSharePointObjectModelNotInstalled";

			// Token: 0x0400007B RID: 123
			public const string ErrorCodes_RsSharePointError = "ErrorCodes_RsSharePointError";

			// Token: 0x0400007C RID: 124
			public const string ErrorCodes_RsServerConfigurationError = "ErrorCodes_RsServerConfigurationError";

			// Token: 0x0400007D RID: 125
			public const string ErrorCodes_RsUrlAlreadyReservedDifferentName = "ErrorCodes_RsUrlAlreadyReservedDifferentName";

			// Token: 0x0400007E RID: 126
			public const string ErrorCodes_RsMustDefineApplicationFirst = "ErrorCodes_RsMustDefineApplicationFirst";

			// Token: 0x0400007F RID: 127
			public const string ErrorCodes_RsIpAddressNotFound = "ErrorCodes_RsIpAddressNotFound";

			// Token: 0x04000080 RID: 128
			public const string ErrorCodes_RsSSLBindingConflict = "ErrorCodes_RsSSLBindingConflict";

			// Token: 0x04000081 RID: 129
			public const string ErrorCodes_RsSSLCertificateNotRegistered = "ErrorCodes_RsSSLCertificateNotRegistered";

			// Token: 0x04000082 RID: 130
			public const string ErrorCodes_RsInvalidApplication = "ErrorCodes_RsInvalidApplication";

			// Token: 0x04000083 RID: 131
			public const string ErrorCodes_RsURLAlreadyReserved = "ErrorCodes_RsURLAlreadyReserved";

			// Token: 0x04000084 RID: 132
			public const string ErrorCodes_RsURLNotReserved = "ErrorCodes_RsURLNotReserved";

			// Token: 0x04000085 RID: 133
			public const string ErrorCodes_RsURLMustNotExist = "ErrorCodes_RsURLMustNotExist";

			// Token: 0x04000086 RID: 134
			public const string ErrorCodes_RsDeliveryExtensionNotFound = "ErrorCodes_RsDeliveryExtensionNotFound";

			// Token: 0x04000087 RID: 135
			public const string ErrorCodes_RsLocalServiceNotAllowedXP = "ErrorCodes_RsLocalServiceNotAllowedXP";

			// Token: 0x04000088 RID: 136
			public const string ErrorCodes_RsInvalidSSLCertificate = "ErrorCodes_RsInvalidSSLCertificate";

			// Token: 0x04000089 RID: 137
			public const string ErrorCodes_RsBadExtendedProtectionLevelType = "ErrorCodes_RsBadExtendedProtectionLevelType";

			// Token: 0x0400008A RID: 138
			public const string ErrorCodes_RsBadExtendedProtectionScenarioType = "ErrorCodes_RsBadExtendedProtectionScenarioType";

			// Token: 0x0400008B RID: 139
			public const string ErrorCodes_RsMustDefineAuthenticationFirst = "ErrorCodes_RsMustDefineAuthenticationFirst";
		}
	}
}
