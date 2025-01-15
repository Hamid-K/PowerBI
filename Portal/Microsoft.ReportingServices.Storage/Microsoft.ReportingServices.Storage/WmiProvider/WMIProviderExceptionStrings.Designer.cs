using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.WmiProvider
{
	// Token: 0x02000045 RID: 69
	[CompilerGenerated]
	internal class WMIProviderExceptionStrings
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00002A10 File Offset: 0x00000C10
		protected WMIProviderExceptionStrings()
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000A7EF File Offset: 0x000089EF
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000A7F6 File Offset: 0x000089F6
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A7FE File Offset: 0x000089FE
		public static string GenericWMIError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("GenericWMIError");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A80A File Offset: 0x00008A0A
		public static string ManagementException_AccessDenied
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ManagementException_AccessDenied");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A816 File Offset: 0x00008A16
		public static string ErrorCodes_Success
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_Success");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A822 File Offset: 0x00008A22
		public static string ErrorCodes_ServiceNotActivated
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ServiceNotActivated");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A82E File Offset: 0x00008A2E
		public static string ErrorCodes_ServiceDisabled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ServiceDisabled");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A83A File Offset: 0x00008A3A
		public static string ErrorCodes_UnexpectedDatabaseError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnexpectedDatabaseError");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A846 File Offset: 0x00008A46
		public static string ErrorCodes_UnexpectedDatabaseResult
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnexpectedDatabaseResult");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A852 File Offset: 0x00008A52
		public static string ErrorCodes_VirtualDirectoryInvalid
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_VirtualDirectoryInvalid");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A85E File Offset: 0x00008A5E
		public static string ErrorCodes_VirtualDirectoryAlreadyExists
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_VirtualDirectoryAlreadyExists");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A86A File Offset: 0x00008A6A
		public static string ErrorCodes_CantConnectCatalog
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_CantConnectCatalog");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A876 File Offset: 0x00008A76
		public static string ErrorCodes_AlreadyActivated
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_AlreadyActivated");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A882 File Offset: 0x00008A82
		public static string ErrorCodes_NotAnnounced
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotAnnounced");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A88E File Offset: 0x00008A8E
		public static string ErrorCodes_NotAdmin
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotAdmin");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A89A File Offset: 0x00008A9A
		public static string ErrorCodes_InvalidIISPath
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidIISPath");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public static string ErrorCodes_IISNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_IISNotInstalled");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A8B2 File Offset: 0x00008AB2
		public static string ErrorCodes_ASPNetNotRegistered
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ASPNetNotRegistered");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A8BE File Offset: 0x00008ABE
		public static string ErrorCodes_InvalidUser
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidUser");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A8CA File Offset: 0x00008ACA
		public static string ErrorCodes_InsufficientPrivilege
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InsufficientPrivilege");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A8D6 File Offset: 0x00008AD6
		public static string ErrorCodes_SqlAdminUserInsufficientPrivilege
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SqlAdminUserInsufficientPrivilege");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A8E2 File Offset: 0x00008AE2
		public static string ErrorCodes_GrantLoginToPasswordRequired
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_GrantLoginToPasswordRequired");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public static string ErrorCodes_RsexecRoleDoesNotExist
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsexecRoleDoesNotExist");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public static string ErrorCodes_WindowsServiceAccountNotSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_WindowsServiceAccountNotSet");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A906 File Offset: 0x00008B06
		public static string ErrorCodes_WebServiceAccountNotSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_WebServiceAccountNotSet");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A912 File Offset: 0x00008B12
		public static string ErrorCodes_SslCertificateNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SslCertificateNotFound");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A91E File Offset: 0x00008B1E
		public static string ErrorCodes_ConfigurationFileNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ConfigurationFileNotFound");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A92A File Offset: 0x00008B2A
		public static string ErrorCodes_InvalidPortNumber
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidPortNumber");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A936 File Offset: 0x00008B36
		public static string ErrorCodes_InvalidUrlParameterSet
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidUrlParameterSet");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A942 File Offset: 0x00008B42
		public static string ErrorCodes_InvalidSecureConnectionLevel
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidSecureConnectionLevel");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A94E File Offset: 0x00008B4E
		public static string ErrorCodes_IllformedAccountString
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_IllformedAccountString");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A95A File Offset: 0x00008B5A
		public static string ErrorCodes_BadCredentialsType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadCredentialsType");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A966 File Offset: 0x00008B66
		public static string ErrorCodes_BadApplicationPool
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadApplicationPool");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A972 File Offset: 0x00008B72
		public static string ErrorCodes_BadApplicationPoolName
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadApplicationPoolName");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A97E File Offset: 0x00008B7E
		public static string ErrorCodes_BadVerson
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadVerson");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A98A File Offset: 0x00008B8A
		public static string ErrorCodes_InvalidVersionString
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidVersionString");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A996 File Offset: 0x00008B96
		public static string ErrorCodes_BadLcid
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadLcid");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		public static string ErrorCodes_ApplicationPoolAlreadyExists
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_ApplicationPoolAlreadyExists");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A9AE File Offset: 0x00008BAE
		public static string ErrorCodes_OperatingSystemNotSupported
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_OperatingSystemNotSupported");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A9BA File Offset: 0x00008BBA
		public static string ErrorCodes_BadWebsiteConfiguration
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadWebsiteConfiguration");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A9C6 File Offset: 0x00008BC6
		public static string ErrorCodes_BadConfigurationFile
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_BadConfigurationFile");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A9D2 File Offset: 0x00008BD2
		public static string ErrorCodes_SharePointNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_SharePointNotInstalled");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A9DE File Offset: 0x00008BDE
		public static string ErrorCodes_MustCreateVirtualDirectory
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_MustCreateVirtualDirectory");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A9EA File Offset: 0x00008BEA
		public static string ErrorCodes_FailedToLoadResources
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_FailedToLoadResources");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A9F6 File Offset: 0x00008BF6
		public static string ErrorCodes_RPCServerNotListening
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RPCServerNotListening");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000AA02 File Offset: 0x00008C02
		public static string ErrorCodes_FailedToEnumerateInstances
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_FailedToEnumerateInstances");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000AA0E File Offset: 0x00008C0E
		public static string ErrorCodes_InvalidParameter
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_InvalidParameter");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000AA1A File Offset: 0x00008C1A
		public static string ErrorCodes_LocalServiceIsLocalOnly
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_LocalServiceIsLocalOnly");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000AA26 File Offset: 0x00008C26
		public static string ErrorCodes_NotSupportedInSharePointMode
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotSupportedInSharePointMode");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000AA32 File Offset: 0x00008C32
		public static string ErrorCodes_NotSupportedInNativeMode
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_NotSupportedInNativeMode");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000AA3E File Offset: 0x00008C3E
		public static string ErrorCodes_RsSharePointObjectModelNotInstalled
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSharePointObjectModelNotInstalled");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000AA4A File Offset: 0x00008C4A
		public static string ErrorCodes_RsSharePointError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSharePointError");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000AA56 File Offset: 0x00008C56
		public static string ErrorCodes_RsServerConfigurationError
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsServerConfigurationError");
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000AA62 File Offset: 0x00008C62
		public static string ErrorCodes_RsUrlAlreadyReservedDifferentName
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsUrlAlreadyReservedDifferentName");
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000AA6E File Offset: 0x00008C6E
		public static string ErrorCodes_RsMustDefineApplicationFirst
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsMustDefineApplicationFirst");
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000AA7A File Offset: 0x00008C7A
		public static string ErrorCodes_RsIpAddressNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsIpAddressNotFound");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000AA86 File Offset: 0x00008C86
		public static string ErrorCodes_RsSSLBindingConflict
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSSLBindingConflict");
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000AA92 File Offset: 0x00008C92
		public static string ErrorCodes_RsSSLCertificateNotRegistered
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsSSLCertificateNotRegistered");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000AA9E File Offset: 0x00008C9E
		public static string ErrorCodes_RsInvalidApplication
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsInvalidApplication");
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000AAAA File Offset: 0x00008CAA
		public static string ErrorCodes_RsURLAlreadyReserved
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLAlreadyReserved");
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000AAB6 File Offset: 0x00008CB6
		public static string ErrorCodes_RsURLNotReserved
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLNotReserved");
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		public static string ErrorCodes_RsURLMustNotExist
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsURLMustNotExist");
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000AACE File Offset: 0x00008CCE
		public static string ErrorCodes_RsDeliveryExtensionNotFound
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsDeliveryExtensionNotFound");
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000AADA File Offset: 0x00008CDA
		public static string ErrorCodes_RsLocalServiceNotAllowedXP
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsLocalServiceNotAllowedXP");
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		public static string ErrorCodes_RsInvalidSSLCertificate
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsInvalidSSLCertificate");
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000AAF2 File Offset: 0x00008CF2
		public static string ErrorCodes_RsBadExtendedProtectionLevelType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsBadExtendedProtectionLevelType");
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000AAFE File Offset: 0x00008CFE
		public static string ErrorCodes_RsBadExtendedProtectionScenarioType
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsBadExtendedProtectionScenarioType");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public static string ErrorCodes_RsMustDefineAuthenticationFirst
		{
			get
			{
				return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_RsMustDefineAuthenticationFirst");
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AB16 File Offset: 0x00008D16
		public static string GenericWMIErrorWithCode(string errorCode)
		{
			return WMIProviderExceptionStrings.Keys.GetString("GenericWMIErrorWithCode", errorCode);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000AB23 File Offset: 0x00008D23
		public static string UnableToConnect(string serverName)
		{
			return WMIProviderExceptionStrings.Keys.GetString("UnableToConnect", serverName);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AB30 File Offset: 0x00008D30
		public static string ManagementException_Generic(string wmiError)
		{
			return WMIProviderExceptionStrings.Keys.GetString("ManagementException_Generic", wmiError);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public static string Exception_Generic(string genericError)
		{
			return WMIProviderExceptionStrings.Keys.GetString("Exception_Generic", genericError);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000AB4A File Offset: 0x00008D4A
		public static string ErrorCodes_UnknownOrUnmappedException(int errorCode)
		{
			return WMIProviderExceptionStrings.Keys.GetString("ErrorCodes_UnknownOrUnmappedException", errorCode);
		}

		// Token: 0x0200005B RID: 91
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x060002C2 RID: 706 RVA: 0x00002A10 File Offset: 0x00000C10
			private Keys()
			{
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B398 File Offset: 0x00009598
			// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000B39F File Offset: 0x0000959F
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

			// Token: 0x060002C5 RID: 709 RVA: 0x0000B3A7 File Offset: 0x000095A7
			public static string GetString(string key)
			{
				return WMIProviderExceptionStrings.Keys.resourceManager.GetString(key, WMIProviderExceptionStrings.Keys._culture);
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000B3B9 File Offset: 0x000095B9
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, WMIProviderExceptionStrings.Keys.resourceManager.GetString(key, WMIProviderExceptionStrings.Keys._culture), arg0);
			}

			// Token: 0x04000267 RID: 615
			private static ResourceManager resourceManager = new ResourceManager(typeof(WMIProviderExceptionStrings).FullName, typeof(WMIProviderExceptionStrings).Module.Assembly);

			// Token: 0x04000268 RID: 616
			private static CultureInfo _culture = null;

			// Token: 0x04000269 RID: 617
			public const string GenericWMIErrorWithCode = "GenericWMIErrorWithCode";

			// Token: 0x0400026A RID: 618
			public const string GenericWMIError = "GenericWMIError";

			// Token: 0x0400026B RID: 619
			public const string UnableToConnect = "UnableToConnect";

			// Token: 0x0400026C RID: 620
			public const string ManagementException_AccessDenied = "ManagementException_AccessDenied";

			// Token: 0x0400026D RID: 621
			public const string ManagementException_Generic = "ManagementException_Generic";

			// Token: 0x0400026E RID: 622
			public const string Exception_Generic = "Exception_Generic";

			// Token: 0x0400026F RID: 623
			public const string ErrorCodes_UnknownOrUnmappedException = "ErrorCodes_UnknownOrUnmappedException";

			// Token: 0x04000270 RID: 624
			public const string ErrorCodes_Success = "ErrorCodes_Success";

			// Token: 0x04000271 RID: 625
			public const string ErrorCodes_ServiceNotActivated = "ErrorCodes_ServiceNotActivated";

			// Token: 0x04000272 RID: 626
			public const string ErrorCodes_ServiceDisabled = "ErrorCodes_ServiceDisabled";

			// Token: 0x04000273 RID: 627
			public const string ErrorCodes_UnexpectedDatabaseError = "ErrorCodes_UnexpectedDatabaseError";

			// Token: 0x04000274 RID: 628
			public const string ErrorCodes_UnexpectedDatabaseResult = "ErrorCodes_UnexpectedDatabaseResult";

			// Token: 0x04000275 RID: 629
			public const string ErrorCodes_VirtualDirectoryInvalid = "ErrorCodes_VirtualDirectoryInvalid";

			// Token: 0x04000276 RID: 630
			public const string ErrorCodes_VirtualDirectoryAlreadyExists = "ErrorCodes_VirtualDirectoryAlreadyExists";

			// Token: 0x04000277 RID: 631
			public const string ErrorCodes_CantConnectCatalog = "ErrorCodes_CantConnectCatalog";

			// Token: 0x04000278 RID: 632
			public const string ErrorCodes_AlreadyActivated = "ErrorCodes_AlreadyActivated";

			// Token: 0x04000279 RID: 633
			public const string ErrorCodes_NotAnnounced = "ErrorCodes_NotAnnounced";

			// Token: 0x0400027A RID: 634
			public const string ErrorCodes_NotAdmin = "ErrorCodes_NotAdmin";

			// Token: 0x0400027B RID: 635
			public const string ErrorCodes_InvalidIISPath = "ErrorCodes_InvalidIISPath";

			// Token: 0x0400027C RID: 636
			public const string ErrorCodes_IISNotInstalled = "ErrorCodes_IISNotInstalled";

			// Token: 0x0400027D RID: 637
			public const string ErrorCodes_ASPNetNotRegistered = "ErrorCodes_ASPNetNotRegistered";

			// Token: 0x0400027E RID: 638
			public const string ErrorCodes_InvalidUser = "ErrorCodes_InvalidUser";

			// Token: 0x0400027F RID: 639
			public const string ErrorCodes_InsufficientPrivilege = "ErrorCodes_InsufficientPrivilege";

			// Token: 0x04000280 RID: 640
			public const string ErrorCodes_SqlAdminUserInsufficientPrivilege = "ErrorCodes_SqlAdminUserInsufficientPrivilege";

			// Token: 0x04000281 RID: 641
			public const string ErrorCodes_GrantLoginToPasswordRequired = "ErrorCodes_GrantLoginToPasswordRequired";

			// Token: 0x04000282 RID: 642
			public const string ErrorCodes_RsexecRoleDoesNotExist = "ErrorCodes_RsexecRoleDoesNotExist";

			// Token: 0x04000283 RID: 643
			public const string ErrorCodes_WindowsServiceAccountNotSet = "ErrorCodes_WindowsServiceAccountNotSet";

			// Token: 0x04000284 RID: 644
			public const string ErrorCodes_WebServiceAccountNotSet = "ErrorCodes_WebServiceAccountNotSet";

			// Token: 0x04000285 RID: 645
			public const string ErrorCodes_SslCertificateNotFound = "ErrorCodes_SslCertificateNotFound";

			// Token: 0x04000286 RID: 646
			public const string ErrorCodes_ConfigurationFileNotFound = "ErrorCodes_ConfigurationFileNotFound";

			// Token: 0x04000287 RID: 647
			public const string ErrorCodes_InvalidPortNumber = "ErrorCodes_InvalidPortNumber";

			// Token: 0x04000288 RID: 648
			public const string ErrorCodes_InvalidUrlParameterSet = "ErrorCodes_InvalidUrlParameterSet";

			// Token: 0x04000289 RID: 649
			public const string ErrorCodes_InvalidSecureConnectionLevel = "ErrorCodes_InvalidSecureConnectionLevel";

			// Token: 0x0400028A RID: 650
			public const string ErrorCodes_IllformedAccountString = "ErrorCodes_IllformedAccountString";

			// Token: 0x0400028B RID: 651
			public const string ErrorCodes_BadCredentialsType = "ErrorCodes_BadCredentialsType";

			// Token: 0x0400028C RID: 652
			public const string ErrorCodes_BadApplicationPool = "ErrorCodes_BadApplicationPool";

			// Token: 0x0400028D RID: 653
			public const string ErrorCodes_BadApplicationPoolName = "ErrorCodes_BadApplicationPoolName";

			// Token: 0x0400028E RID: 654
			public const string ErrorCodes_BadVerson = "ErrorCodes_BadVerson";

			// Token: 0x0400028F RID: 655
			public const string ErrorCodes_InvalidVersionString = "ErrorCodes_InvalidVersionString";

			// Token: 0x04000290 RID: 656
			public const string ErrorCodes_BadLcid = "ErrorCodes_BadLcid";

			// Token: 0x04000291 RID: 657
			public const string ErrorCodes_ApplicationPoolAlreadyExists = "ErrorCodes_ApplicationPoolAlreadyExists";

			// Token: 0x04000292 RID: 658
			public const string ErrorCodes_OperatingSystemNotSupported = "ErrorCodes_OperatingSystemNotSupported";

			// Token: 0x04000293 RID: 659
			public const string ErrorCodes_BadWebsiteConfiguration = "ErrorCodes_BadWebsiteConfiguration";

			// Token: 0x04000294 RID: 660
			public const string ErrorCodes_BadConfigurationFile = "ErrorCodes_BadConfigurationFile";

			// Token: 0x04000295 RID: 661
			public const string ErrorCodes_SharePointNotInstalled = "ErrorCodes_SharePointNotInstalled";

			// Token: 0x04000296 RID: 662
			public const string ErrorCodes_MustCreateVirtualDirectory = "ErrorCodes_MustCreateVirtualDirectory";

			// Token: 0x04000297 RID: 663
			public const string ErrorCodes_FailedToLoadResources = "ErrorCodes_FailedToLoadResources";

			// Token: 0x04000298 RID: 664
			public const string ErrorCodes_RPCServerNotListening = "ErrorCodes_RPCServerNotListening";

			// Token: 0x04000299 RID: 665
			public const string ErrorCodes_FailedToEnumerateInstances = "ErrorCodes_FailedToEnumerateInstances";

			// Token: 0x0400029A RID: 666
			public const string ErrorCodes_InvalidParameter = "ErrorCodes_InvalidParameter";

			// Token: 0x0400029B RID: 667
			public const string ErrorCodes_LocalServiceIsLocalOnly = "ErrorCodes_LocalServiceIsLocalOnly";

			// Token: 0x0400029C RID: 668
			public const string ErrorCodes_NotSupportedInSharePointMode = "ErrorCodes_NotSupportedInSharePointMode";

			// Token: 0x0400029D RID: 669
			public const string ErrorCodes_NotSupportedInNativeMode = "ErrorCodes_NotSupportedInNativeMode";

			// Token: 0x0400029E RID: 670
			public const string ErrorCodes_RsSharePointObjectModelNotInstalled = "ErrorCodes_RsSharePointObjectModelNotInstalled";

			// Token: 0x0400029F RID: 671
			public const string ErrorCodes_RsSharePointError = "ErrorCodes_RsSharePointError";

			// Token: 0x040002A0 RID: 672
			public const string ErrorCodes_RsServerConfigurationError = "ErrorCodes_RsServerConfigurationError";

			// Token: 0x040002A1 RID: 673
			public const string ErrorCodes_RsUrlAlreadyReservedDifferentName = "ErrorCodes_RsUrlAlreadyReservedDifferentName";

			// Token: 0x040002A2 RID: 674
			public const string ErrorCodes_RsMustDefineApplicationFirst = "ErrorCodes_RsMustDefineApplicationFirst";

			// Token: 0x040002A3 RID: 675
			public const string ErrorCodes_RsIpAddressNotFound = "ErrorCodes_RsIpAddressNotFound";

			// Token: 0x040002A4 RID: 676
			public const string ErrorCodes_RsSSLBindingConflict = "ErrorCodes_RsSSLBindingConflict";

			// Token: 0x040002A5 RID: 677
			public const string ErrorCodes_RsSSLCertificateNotRegistered = "ErrorCodes_RsSSLCertificateNotRegistered";

			// Token: 0x040002A6 RID: 678
			public const string ErrorCodes_RsInvalidApplication = "ErrorCodes_RsInvalidApplication";

			// Token: 0x040002A7 RID: 679
			public const string ErrorCodes_RsURLAlreadyReserved = "ErrorCodes_RsURLAlreadyReserved";

			// Token: 0x040002A8 RID: 680
			public const string ErrorCodes_RsURLNotReserved = "ErrorCodes_RsURLNotReserved";

			// Token: 0x040002A9 RID: 681
			public const string ErrorCodes_RsURLMustNotExist = "ErrorCodes_RsURLMustNotExist";

			// Token: 0x040002AA RID: 682
			public const string ErrorCodes_RsDeliveryExtensionNotFound = "ErrorCodes_RsDeliveryExtensionNotFound";

			// Token: 0x040002AB RID: 683
			public const string ErrorCodes_RsLocalServiceNotAllowedXP = "ErrorCodes_RsLocalServiceNotAllowedXP";

			// Token: 0x040002AC RID: 684
			public const string ErrorCodes_RsInvalidSSLCertificate = "ErrorCodes_RsInvalidSSLCertificate";

			// Token: 0x040002AD RID: 685
			public const string ErrorCodes_RsBadExtendedProtectionLevelType = "ErrorCodes_RsBadExtendedProtectionLevelType";

			// Token: 0x040002AE RID: 686
			public const string ErrorCodes_RsBadExtendedProtectionScenarioType = "ErrorCodes_RsBadExtendedProtectionScenarioType";

			// Token: 0x040002AF RID: 687
			public const string ErrorCodes_RsMustDefineAuthenticationFirst = "ErrorCodes_RsMustDefineAuthenticationFirst";
		}
	}
}
