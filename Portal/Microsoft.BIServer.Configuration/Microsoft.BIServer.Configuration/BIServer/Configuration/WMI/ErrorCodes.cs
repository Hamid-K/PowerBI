using System;

namespace Microsoft.BIServer.Configuration.WMI
{
	// Token: 0x02000031 RID: 49
	internal enum ErrorCodes : uint
	{
		// Token: 0x04000140 RID: 320
		Success,
		// Token: 0x04000141 RID: 321
		CantConnectCatalog = 2147746305U,
		// Token: 0x04000142 RID: 322
		ServiceNotActivated,
		// Token: 0x04000143 RID: 323
		ServiceDisabled,
		// Token: 0x04000144 RID: 324
		UnexpectedDatabaseError,
		// Token: 0x04000145 RID: 325
		UnexpectedDatabaseResult,
		// Token: 0x04000146 RID: 326
		AlreadyActivated,
		// Token: 0x04000147 RID: 327
		NotAnnounced,
		// Token: 0x04000148 RID: 328
		NotAdmin,
		// Token: 0x04000149 RID: 329
		VirtualDirectoryAlreadyExists = 2147746316U,
		// Token: 0x0400014A RID: 330
		VirtualDirectoryInvalid,
		// Token: 0x0400014B RID: 331
		InvalidIISPath,
		// Token: 0x0400014C RID: 332
		IISNotInstalled,
		// Token: 0x0400014D RID: 333
		AspNetNotRegistered,
		// Token: 0x0400014E RID: 334
		InvalidUser,
		// Token: 0x0400014F RID: 335
		InsufficientUserPrivilege,
		// Token: 0x04000150 RID: 336
		SqlAdminUserInsufficientPrivilege,
		// Token: 0x04000151 RID: 337
		GrantLoginToPasswordRequired,
		// Token: 0x04000152 RID: 338
		RsexecRoleDoesNotExist,
		// Token: 0x04000153 RID: 339
		WindowsServiceAccountNotSet,
		// Token: 0x04000154 RID: 340
		WebServiceAccountNotSet,
		// Token: 0x04000155 RID: 341
		SslCertificateNotFound,
		// Token: 0x04000156 RID: 342
		ConfigurationFileNotFound,
		// Token: 0x04000157 RID: 343
		InvalidPortNumber,
		// Token: 0x04000158 RID: 344
		InvalidUrlParameterSet,
		// Token: 0x04000159 RID: 345
		InvalidSecureConnectionLevel,
		// Token: 0x0400015A RID: 346
		IllformedAccountString,
		// Token: 0x0400015B RID: 347
		BadCredentialsType,
		// Token: 0x0400015C RID: 348
		BadApplicationPool,
		// Token: 0x0400015D RID: 349
		BadVersion,
		// Token: 0x0400015E RID: 350
		IllformedVersionString,
		// Token: 0x0400015F RID: 351
		BadLcid,
		// Token: 0x04000160 RID: 352
		ApplicationPoolAlreadyExists,
		// Token: 0x04000161 RID: 353
		OsNotSupported,
		// Token: 0x04000162 RID: 354
		BadWebsiteConfigruation,
		// Token: 0x04000163 RID: 355
		BadConfigurationFile,
		// Token: 0x04000164 RID: 356
		SharePointNotInstalled,
		// Token: 0x04000165 RID: 357
		MustCreateVirtualDirectory,
		// Token: 0x04000166 RID: 358
		FailedToLoadResources,
		// Token: 0x04000167 RID: 359
		LocalServiceIsLocalOnly,
		// Token: 0x04000168 RID: 360
		FailedToEnumerateInstances,
		// Token: 0x04000169 RID: 361
		InvalidParameter,
		// Token: 0x0400016A RID: 362
		BadApplicationPoolName,
		// Token: 0x0400016B RID: 363
		MustUseDefaultPort,
		// Token: 0x0400016C RID: 364
		WebsiteNotListeningOnSpecifiedPort,
		// Token: 0x0400016D RID: 365
		NotSupportedInSharePointMode,
		// Token: 0x0400016E RID: 366
		NotSupportedInNativeMode,
		// Token: 0x0400016F RID: 367
		RsSharePointObjectModelNotInstalled,
		// Token: 0x04000170 RID: 368
		RsSharePointError,
		// Token: 0x04000171 RID: 369
		RsServerConfigurationError,
		// Token: 0x04000172 RID: 370
		RsUrlAlreadyReservedDifferentName,
		// Token: 0x04000173 RID: 371
		RsMustDefineApplicationFirst,
		// Token: 0x04000174 RID: 372
		RsIpAddressNotFound,
		// Token: 0x04000175 RID: 373
		RsSSLBindingConflict,
		// Token: 0x04000176 RID: 374
		RsSSLCertificateNotRegistered = 2147746362U,
		// Token: 0x04000177 RID: 375
		RsInvalidApplication,
		// Token: 0x04000178 RID: 376
		RsURLAlreadyReserved,
		// Token: 0x04000179 RID: 377
		RsURLNotReserved,
		// Token: 0x0400017A RID: 378
		RsURLMustNotExist,
		// Token: 0x0400017B RID: 379
		RsDeliveryExtensionNotFound,
		// Token: 0x0400017C RID: 380
		RsLocalServiceNotAllowedXP,
		// Token: 0x0400017D RID: 381
		RsInvalidSSLCertificate,
		// Token: 0x0400017E RID: 382
		RsBadExtendedProtectionLevelType,
		// Token: 0x0400017F RID: 383
		RsBadExtendedProtectionScenarioType,
		// Token: 0x04000180 RID: 384
		RsMustDefineAuthenticationFirst,
		// Token: 0x04000181 RID: 385
		RPCServerNotListening = 2147944115U,
		// Token: 0x04000182 RID: 386
		UnknownError = 4294967295U
	}
}
