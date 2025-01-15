using System;
using System.Management;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.WmiProvider
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	internal sealed class WMIProviderException : Exception
	{
		// Token: 0x0600021F RID: 543 RVA: 0x0000A43E File Offset: 0x0000863E
		public WMIProviderException()
			: base(null)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A44E File Offset: 0x0000864E
		public WMIProviderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A45E File Offset: 0x0000865E
		public WMIProviderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A46F File Offset: 0x0000866F
		public WMIProviderException(ErrorCodes errorCode)
			: base(WMIProviderException.ErrorCodeToMessage(errorCode))
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A48B File Offset: 0x0000868B
		public WMIProviderException(ErrorCodes errorCode, Exception innerException)
			: base(WMIProviderException.ErrorCodeToMessage(errorCode), innerException)
		{
			this.m_errorCode = errorCode;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public WMIProviderException(Exception exc)
			: base(WMIProviderException.ExceptionToMessage(exc), exc)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000A4BE File Offset: 0x000086BE
		public ErrorCodes ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A4C8 File Offset: 0x000086C8
		private static string ExceptionToMessage(Exception exc)
		{
			if (exc is ManagementException)
			{
				ManagementStatus errorCode = ((ManagementException)exc).ErrorCode;
				if (errorCode == ManagementStatus.AccessDenied)
				{
					return WMIProviderExceptionStrings.ManagementException_AccessDenied;
				}
				return WMIProviderExceptionStrings.ManagementException_Generic(exc.Message);
			}
			else
			{
				if (exc is UnauthorizedAccessException)
				{
					return WMIProviderExceptionStrings.ManagementException_AccessDenied;
				}
				return WMIProviderExceptionStrings.Exception_Generic(exc.Message);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A51C File Offset: 0x0000871C
		private static string ErrorCodeToMessage(ErrorCodes errorCode)
		{
			if (errorCode != ErrorCodes.Success)
			{
				switch (errorCode)
				{
				case (ErrorCodes)2147746305U:
					return WMIProviderExceptionStrings.ErrorCodes_CantConnectCatalog;
				case (ErrorCodes)2147746306U:
					return WMIProviderExceptionStrings.ErrorCodes_ServiceNotActivated;
				case (ErrorCodes)2147746307U:
					return WMIProviderExceptionStrings.ErrorCodes_ServiceDisabled;
				case (ErrorCodes)2147746308U:
					return WMIProviderExceptionStrings.ErrorCodes_UnexpectedDatabaseError;
				case (ErrorCodes)2147746309U:
					return WMIProviderExceptionStrings.ErrorCodes_UnexpectedDatabaseResult;
				case (ErrorCodes)2147746310U:
					return WMIProviderExceptionStrings.ErrorCodes_AlreadyActivated;
				case (ErrorCodes)2147746311U:
					return WMIProviderExceptionStrings.ErrorCodes_NotAnnounced;
				case (ErrorCodes)2147746312U:
					return WMIProviderExceptionStrings.ErrorCodes_NotAdmin;
				case (ErrorCodes)2147746313U:
				case (ErrorCodes)2147746314U:
				case (ErrorCodes)2147746315U:
				case (ErrorCodes)2147746350U:
				case (ErrorCodes)2147746351U:
				case (ErrorCodes)2147746361U:
					break;
				case (ErrorCodes)2147746316U:
					return WMIProviderExceptionStrings.ErrorCodes_VirtualDirectoryAlreadyExists;
				case (ErrorCodes)2147746317U:
					return WMIProviderExceptionStrings.ErrorCodes_VirtualDirectoryInvalid;
				case (ErrorCodes)2147746318U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidIISPath;
				case (ErrorCodes)2147746319U:
					return WMIProviderExceptionStrings.ErrorCodes_IISNotInstalled;
				case (ErrorCodes)2147746320U:
					return WMIProviderExceptionStrings.ErrorCodes_ASPNetNotRegistered;
				case (ErrorCodes)2147746321U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidUser;
				case (ErrorCodes)2147746322U:
					return WMIProviderExceptionStrings.ErrorCodes_InsufficientPrivilege;
				case (ErrorCodes)2147746323U:
					return WMIProviderExceptionStrings.ErrorCodes_SqlAdminUserInsufficientPrivilege;
				case (ErrorCodes)2147746324U:
					return WMIProviderExceptionStrings.ErrorCodes_GrantLoginToPasswordRequired;
				case (ErrorCodes)2147746325U:
					return WMIProviderExceptionStrings.ErrorCodes_RsexecRoleDoesNotExist;
				case (ErrorCodes)2147746326U:
					return WMIProviderExceptionStrings.ErrorCodes_WindowsServiceAccountNotSet;
				case (ErrorCodes)2147746327U:
					return WMIProviderExceptionStrings.ErrorCodes_WebServiceAccountNotSet;
				case (ErrorCodes)2147746328U:
					return WMIProviderExceptionStrings.ErrorCodes_SslCertificateNotFound;
				case (ErrorCodes)2147746329U:
					return WMIProviderExceptionStrings.ErrorCodes_ConfigurationFileNotFound;
				case (ErrorCodes)2147746330U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidPortNumber;
				case (ErrorCodes)2147746331U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidUrlParameterSet;
				case (ErrorCodes)2147746332U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidSecureConnectionLevel;
				case (ErrorCodes)2147746333U:
					return WMIProviderExceptionStrings.ErrorCodes_IllformedAccountString;
				case (ErrorCodes)2147746334U:
					return WMIProviderExceptionStrings.ErrorCodes_BadCredentialsType;
				case (ErrorCodes)2147746335U:
					return WMIProviderExceptionStrings.ErrorCodes_BadApplicationPool;
				case (ErrorCodes)2147746336U:
					return WMIProviderExceptionStrings.ErrorCodes_BadVerson;
				case (ErrorCodes)2147746337U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidVersionString;
				case (ErrorCodes)2147746338U:
					return WMIProviderExceptionStrings.ErrorCodes_BadLcid;
				case (ErrorCodes)2147746339U:
					return WMIProviderExceptionStrings.ErrorCodes_ApplicationPoolAlreadyExists;
				case (ErrorCodes)2147746340U:
					return WMIProviderExceptionStrings.ErrorCodes_OperatingSystemNotSupported;
				case (ErrorCodes)2147746341U:
					return WMIProviderExceptionStrings.ErrorCodes_BadWebsiteConfiguration;
				case (ErrorCodes)2147746342U:
					return WMIProviderExceptionStrings.ErrorCodes_BadConfigurationFile;
				case (ErrorCodes)2147746343U:
					return WMIProviderExceptionStrings.ErrorCodes_SharePointNotInstalled;
				case (ErrorCodes)2147746344U:
					return WMIProviderExceptionStrings.ErrorCodes_MustCreateVirtualDirectory;
				case (ErrorCodes)2147746345U:
					return WMIProviderExceptionStrings.ErrorCodes_FailedToLoadResources;
				case (ErrorCodes)2147746346U:
					return WMIProviderExceptionStrings.ErrorCodes_LocalServiceIsLocalOnly;
				case (ErrorCodes)2147746347U:
					return WMIProviderExceptionStrings.ErrorCodes_FailedToEnumerateInstances;
				case (ErrorCodes)2147746348U:
					return WMIProviderExceptionStrings.ErrorCodes_InvalidParameter;
				case (ErrorCodes)2147746349U:
					return WMIProviderExceptionStrings.ErrorCodes_BadApplicationPoolName;
				case (ErrorCodes)2147746352U:
					return WMIProviderExceptionStrings.ErrorCodes_NotSupportedInSharePointMode;
				case (ErrorCodes)2147746353U:
					return WMIProviderExceptionStrings.ErrorCodes_NotSupportedInNativeMode;
				case (ErrorCodes)2147746354U:
					return WMIProviderExceptionStrings.ErrorCodes_RsSharePointObjectModelNotInstalled;
				case (ErrorCodes)2147746355U:
					return WMIProviderExceptionStrings.ErrorCodes_RsSharePointError;
				case (ErrorCodes)2147746356U:
					return WMIProviderExceptionStrings.ErrorCodes_RsServerConfigurationError;
				case (ErrorCodes)2147746357U:
					return WMIProviderExceptionStrings.ErrorCodes_RsUrlAlreadyReservedDifferentName;
				case (ErrorCodes)2147746358U:
					return WMIProviderExceptionStrings.ErrorCodes_RsMustDefineApplicationFirst;
				case (ErrorCodes)2147746359U:
					return WMIProviderExceptionStrings.ErrorCodes_RsIpAddressNotFound;
				case (ErrorCodes)2147746360U:
					return WMIProviderExceptionStrings.ErrorCodes_RsSSLBindingConflict;
				case (ErrorCodes)2147746362U:
					return WMIProviderExceptionStrings.ErrorCodes_RsSSLCertificateNotRegistered;
				case (ErrorCodes)2147746363U:
					return WMIProviderExceptionStrings.ErrorCodes_RsInvalidApplication;
				case (ErrorCodes)2147746364U:
					return WMIProviderExceptionStrings.ErrorCodes_RsURLAlreadyReserved;
				case (ErrorCodes)2147746365U:
					return WMIProviderExceptionStrings.ErrorCodes_RsURLNotReserved;
				case (ErrorCodes)2147746366U:
					return WMIProviderExceptionStrings.ErrorCodes_RsURLMustNotExist;
				case (ErrorCodes)2147746367U:
					return WMIProviderExceptionStrings.ErrorCodes_RsDeliveryExtensionNotFound;
				case (ErrorCodes)2147746368U:
					return WMIProviderExceptionStrings.ErrorCodes_RsLocalServiceNotAllowedXP;
				case (ErrorCodes)2147746369U:
					return WMIProviderExceptionStrings.ErrorCodes_RsInvalidSSLCertificate;
				case (ErrorCodes)2147746370U:
					return WMIProviderExceptionStrings.ErrorCodes_RsBadExtendedProtectionLevelType;
				case (ErrorCodes)2147746371U:
					return WMIProviderExceptionStrings.ErrorCodes_RsBadExtendedProtectionScenarioType;
				case (ErrorCodes)2147746372U:
					return WMIProviderExceptionStrings.ErrorCodes_RsMustDefineAuthenticationFirst;
				default:
					if (errorCode == (ErrorCodes)2147944115U)
					{
						return WMIProviderExceptionStrings.ErrorCodes_RPCServerNotListening;
					}
					break;
				}
				return WMIProviderExceptionStrings.ErrorCodes_UnknownOrUnmappedException((int)errorCode);
			}
			return WMIProviderExceptionStrings.ErrorCodes_Success;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A7E1 File Offset: 0x000089E1
		internal static WMIProviderException GetWMIProviderExceptionForHR(int hresult)
		{
			return new WMIProviderException((ErrorCodes)hresult, Marshal.GetExceptionForHR(hresult));
		}

		// Token: 0x040001D3 RID: 467
		private ErrorCodes m_errorCode = (ErrorCodes)4294967295U;
	}
}
