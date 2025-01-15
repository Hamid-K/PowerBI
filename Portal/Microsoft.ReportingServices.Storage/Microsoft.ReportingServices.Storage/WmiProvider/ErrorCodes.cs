using System;

namespace Microsoft.ReportingServices.WmiProvider
{
	// Token: 0x02000043 RID: 67
	internal enum ErrorCodes : uint
	{
		// Token: 0x04000190 RID: 400
		Success,
		// Token: 0x04000191 RID: 401
		CantConnectCatalog = 2147746305U,
		// Token: 0x04000192 RID: 402
		ServiceNotActivated,
		// Token: 0x04000193 RID: 403
		ServiceDisabled,
		// Token: 0x04000194 RID: 404
		UnexpectedDatabaseError,
		// Token: 0x04000195 RID: 405
		UnexpectedDatabaseResult,
		// Token: 0x04000196 RID: 406
		AlreadyActivated,
		// Token: 0x04000197 RID: 407
		NotAnnounced,
		// Token: 0x04000198 RID: 408
		NotAdmin,
		// Token: 0x04000199 RID: 409
		VirtualDirectoryAlreadyExists = 2147746316U,
		// Token: 0x0400019A RID: 410
		VirtualDirectoryInvalid,
		// Token: 0x0400019B RID: 411
		InvalidIISPath,
		// Token: 0x0400019C RID: 412
		IISNotInstalled,
		// Token: 0x0400019D RID: 413
		AspNetNotRegistered,
		// Token: 0x0400019E RID: 414
		InvalidUser,
		// Token: 0x0400019F RID: 415
		InsufficientUserPrivilege,
		// Token: 0x040001A0 RID: 416
		SqlAdminUserInsufficientPrivilege,
		// Token: 0x040001A1 RID: 417
		GrantLoginToPasswordRequired,
		// Token: 0x040001A2 RID: 418
		RsexecRoleDoesNotExist,
		// Token: 0x040001A3 RID: 419
		WindowsServiceAccountNotSet,
		// Token: 0x040001A4 RID: 420
		WebServiceAccountNotSet,
		// Token: 0x040001A5 RID: 421
		SslCertificateNotFound,
		// Token: 0x040001A6 RID: 422
		ConfigurationFileNotFound,
		// Token: 0x040001A7 RID: 423
		InvalidPortNumber,
		// Token: 0x040001A8 RID: 424
		InvalidUrlParameterSet,
		// Token: 0x040001A9 RID: 425
		InvalidSecureConnectionLevel,
		// Token: 0x040001AA RID: 426
		IllformedAccountString,
		// Token: 0x040001AB RID: 427
		BadCredentialsType,
		// Token: 0x040001AC RID: 428
		BadApplicationPool,
		// Token: 0x040001AD RID: 429
		BadVersion,
		// Token: 0x040001AE RID: 430
		IllformedVersionString,
		// Token: 0x040001AF RID: 431
		BadLcid,
		// Token: 0x040001B0 RID: 432
		ApplicationPoolAlreadyExists,
		// Token: 0x040001B1 RID: 433
		OsNotSupported,
		// Token: 0x040001B2 RID: 434
		BadWebsiteConfigruation,
		// Token: 0x040001B3 RID: 435
		BadConfigurationFile,
		// Token: 0x040001B4 RID: 436
		SharePointNotInstalled,
		// Token: 0x040001B5 RID: 437
		MustCreateVirtualDirectory,
		// Token: 0x040001B6 RID: 438
		FailedToLoadResources,
		// Token: 0x040001B7 RID: 439
		LocalServiceIsLocalOnly,
		// Token: 0x040001B8 RID: 440
		FailedToEnumerateInstances,
		// Token: 0x040001B9 RID: 441
		InvalidParameter,
		// Token: 0x040001BA RID: 442
		BadApplicationPoolName,
		// Token: 0x040001BB RID: 443
		MustUseDefaultPort,
		// Token: 0x040001BC RID: 444
		WebsiteNotListeningOnSpecifiedPort,
		// Token: 0x040001BD RID: 445
		NotSupportedInSharePointMode,
		// Token: 0x040001BE RID: 446
		NotSupportedInNativeMode,
		// Token: 0x040001BF RID: 447
		RsSharePointObjectModelNotInstalled,
		// Token: 0x040001C0 RID: 448
		RsSharePointError,
		// Token: 0x040001C1 RID: 449
		RsServerConfigurationError,
		// Token: 0x040001C2 RID: 450
		RsUrlAlreadyReservedDifferentName,
		// Token: 0x040001C3 RID: 451
		RsMustDefineApplicationFirst,
		// Token: 0x040001C4 RID: 452
		RsIpAddressNotFound,
		// Token: 0x040001C5 RID: 453
		RsSSLBindingConflict,
		// Token: 0x040001C6 RID: 454
		RsSSLCertificateNotRegistered = 2147746362U,
		// Token: 0x040001C7 RID: 455
		RsInvalidApplication,
		// Token: 0x040001C8 RID: 456
		RsURLAlreadyReserved,
		// Token: 0x040001C9 RID: 457
		RsURLNotReserved,
		// Token: 0x040001CA RID: 458
		RsURLMustNotExist,
		// Token: 0x040001CB RID: 459
		RsDeliveryExtensionNotFound,
		// Token: 0x040001CC RID: 460
		RsLocalServiceNotAllowedXP,
		// Token: 0x040001CD RID: 461
		RsInvalidSSLCertificate,
		// Token: 0x040001CE RID: 462
		RsBadExtendedProtectionLevelType,
		// Token: 0x040001CF RID: 463
		RsBadExtendedProtectionScenarioType,
		// Token: 0x040001D0 RID: 464
		RsMustDefineAuthenticationFirst,
		// Token: 0x040001D1 RID: 465
		RPCServerNotListening = 2147944115U,
		// Token: 0x040001D2 RID: 466
		UnknownError = 4294967295U
	}
}
