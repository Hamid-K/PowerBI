using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A2 RID: 930
	internal static class CacheStringErrorCode
	{
		// Token: 0x04001458 RID: 5208
		internal const string DefaultIfNotSpecified = "UnspecifiedErrorCode";

		// Token: 0x04001459 RID: 5209
		internal const string DMDuplicateKey = "ERRDM0001";

		// Token: 0x0400145A RID: 5210
		internal const string DMKeyNotFound = "ERRDM0002";

		// Token: 0x0400145B RID: 5211
		internal const string DMVersionMismatchError = "ERRDM0003";

		// Token: 0x0400145C RID: 5212
		internal const string DMObjectLocked = "ERRDM0004";

		// Token: 0x0400145D RID: 5213
		internal const string DMIndexExists = "ERRDM0005";

		// Token: 0x0400145E RID: 5214
		internal const string DMInvalidLock = "ERRDM0006";

		// Token: 0x0400145F RID: 5215
		internal const string DMObjectNotLocked = "ERRDM0007";

		// Token: 0x04001460 RID: 5216
		internal const string DMInvalidEnumerator = "ERRDM0008";

		// Token: 0x04001461 RID: 5217
		internal const string DMKeyLatched = "ERRDM0009";

		// Token: 0x04001462 RID: 5218
		internal const string DMReadThroughObjectLocked = "ERRDM0010";

		// Token: 0x04001463 RID: 5219
		internal const string DMReadThroughDuplicateKey = "ERRDM0011";

		// Token: 0x04001464 RID: 5220
		internal const string DMReadThroughObjectNotLocked = "ERRDM00012";

		// Token: 0x04001465 RID: 5221
		internal const string DMReadThroughInvalidLock = "ERRDM0013";

		// Token: 0x04001466 RID: 5222
		internal const string OMFatalError = "ERROM0001";

		// Token: 0x04001467 RID: 5223
		internal const string OMRegionNotFound = "ERROM0002";

		// Token: 0x04001468 RID: 5224
		internal const string OMDuplicateRegion = "ERROM0003";

		// Token: 0x04001469 RID: 5225
		internal const string OMExceedMaxNamedCacheCount = "ERROM0004";

		// Token: 0x0400146A RID: 5226
		internal const string OMDuplicateNamedCache = "ERROM0005";

		// Token: 0x0400146B RID: 5227
		internal const string OMNamedCacheNotFound = "ERROM0006";

		// Token: 0x0400146C RID: 5228
		internal const string OMReadThroughRegionNotFound = "ERROM0007";

		// Token: 0x0400146D RID: 5229
		internal const string DOMInitializationFailed = "ERRService0001";

		// Token: 0x0400146E RID: 5230
		internal const string ServiceCrashed = "ERRService0002";

		// Token: 0x0400146F RID: 5231
		internal const string ReplicationFailed = "ERRService0003";

		// Token: 0x04001470 RID: 5232
		internal const string CacheItemVersionMismatch = "ERRCA0001";

		// Token: 0x04001471 RID: 5233
		internal const string RegistryKeyOpenFailure = "ERRCA0002";

		// Token: 0x04001472 RID: 5234
		internal const string InvalidArgument = "ERRCA0003";

		// Token: 0x04001473 RID: 5235
		internal const string UndefinedError = "ERRCA0004";

		// Token: 0x04001474 RID: 5236
		internal const string RegionDoesNotExist = "ERRCA0005";

		// Token: 0x04001475 RID: 5237
		internal const string KeyDoesNotExist = "ERRCA0006";

		// Token: 0x04001476 RID: 5238
		internal const string RegionAlreadyExists = "ERRCA0007";

		// Token: 0x04001477 RID: 5239
		internal const string KeyAlreadyExists = "ERRCA0008";

		// Token: 0x04001478 RID: 5240
		internal const string NamedCacheDoesNotExist = "ERRCA0009";

		// Token: 0x04001479 RID: 5241
		internal const string MaxNamedCacheCountExceeded = "ERRCA0010";

		// Token: 0x0400147A RID: 5242
		internal const string ObjectLocked = "ERRCA0011";

		// Token: 0x0400147B RID: 5243
		internal const string ObjectNotLocked = "ERRCA0012";

		// Token: 0x0400147C RID: 5244
		internal const string InvalidCacheLockHandle = "ERRCA0013";

		// Token: 0x0400147D RID: 5245
		internal const string InvalidEnumerator = "ERRCA0014";

		// Token: 0x0400147E RID: 5246
		internal const string NotificationInvalidationNotSupported = "ERRCA0015";

		// Token: 0x0400147F RID: 5247
		internal const string ConnectionTerminated = "ERRCA0016";

		// Token: 0x04001480 RID: 5248
		internal const string RetryLater = "ERRCA0017";

		// Token: 0x04001481 RID: 5249
		internal const string Timeout = "ERRCA0018";

		// Token: 0x04001482 RID: 5250
		internal const string ClientServerVersionMismatch = "ERRCA0019";

		// Token: 0x04001483 RID: 5251
		internal const string SerializationException = "ERRCA0020";

		// Token: 0x04001484 RID: 5252
		internal const string ServerNull = "ERRCA0021";

		// Token: 0x04001485 RID: 5253
		internal const string CAOutofMemory = "ERRCA0022";

		// Token: 0x04001486 RID: 5254
		internal const string OperationNotSupported = "ERRCA0023";

		// Token: 0x04001487 RID: 5255
		internal const string CAPreCallbackFailed = "ERRCA0024";

		// Token: 0x04001488 RID: 5256
		internal const string VersionModified = "VersionValueModified";

		// Token: 0x04001489 RID: 5257
		internal const string UsageResourceNotFound = "ERRCA0032";

		// Token: 0x0400148A RID: 5258
		internal const string CcrUnhandledException = "ERRCA0033";

		// Token: 0x0400148B RID: 5259
		internal const string ReadThroughRegionDoesNotExist = "ERRCA0034";

		// Token: 0x0400148C RID: 5260
		internal const string OverflowException = "ERRCA0035";

		// Token: 0x0400148D RID: 5261
		internal const string DuplicateServersSpecified = "ERRCA0037";

		// Token: 0x0400148E RID: 5262
		internal const string StringTooLarge = "ERRCA0038";

		// Token: 0x0400148F RID: 5263
		internal const string MessageLargerThanConfiguredSize = "ERRCA0039";

		// Token: 0x04001490 RID: 5264
		internal const string UnsupportedMessageAttemptedOnPort = "ERRCA0040";

		// Token: 0x04001491 RID: 5265
		internal const string ChannelAuthenticationFailed = "ERRCA0041";

		// Token: 0x04001492 RID: 5266
		internal const string InvalidAutoDiscoverIdentifier = "ERRCA0042";

		// Token: 0x04001493 RID: 5267
		internal const string SSConfigParameterNull = "ERRSS0001";

		// Token: 0x04001494 RID: 5268
		internal const string SSInvalidSessionState = "ERRSS0002";

		// Token: 0x04001495 RID: 5269
		internal const string SSUnknownConfigurationParamater = "ERRSS0003";

		// Token: 0x04001496 RID: 5270
		internal const string SSUnknownConfigurationError = "ERRSS0004";

		// Token: 0x04001497 RID: 5271
		internal const string SSEmptyCacheName = "ERRSS0005";

		// Token: 0x04001498 RID: 5272
		internal const string SSEmptyApplicationName = "ERRSS0006";

		// Token: 0x04001499 RID: 5273
		internal const string RSLInitializationError = "ERRRSL0001";

		// Token: 0x0400149A RID: 5274
		internal const string RSLFormatStringError = "ERRRSL0002";

		// Token: 0x0400149B RID: 5275
		internal const string ClientConfigFileTagMissing = "ERRCMC0001";

		// Token: 0x0400149C RID: 5276
		internal const string ClientConfigFileInvalidParameterValue = "ERRCMC0002";

		// Token: 0x0400149D RID: 5277
		internal const string ClientConfigFileErrors = "ERRCMC0003";

		// Token: 0x0400149E RID: 5278
		internal const string ClientConfigFileDuplicateElement = "ERRCMC0004";

		// Token: 0x0400149F RID: 5279
		internal const string ConfigErrorDuringAccessingStore = "ERRCMS0001";

		// Token: 0x040014A0 RID: 5280
		internal const string ConfigFileInsufficientPermissions = "ERRCMS0002";

		// Token: 0x040014A1 RID: 5281
		internal const string ConfigFileDCacheSectionNotFound = "ERRCMS0003";

		// Token: 0x040014A2 RID: 5282
		internal const string ConfigFileNotFound = "ERRCMS0004";

		// Token: 0x040014A3 RID: 5283
		internal const string ClusterConfigInvalidParameterValue = "ERRCMS0005";

		// Token: 0x040014A4 RID: 5284
		internal const string CustomProviderLoadError = "ERRCMS0006";

		// Token: 0x040014A5 RID: 5285
		internal const string CustomProviderInstantiationFailed = "ERRCMS0007";

		// Token: 0x040014A6 RID: 5286
		internal const string CustomProviderRegistrationFailed = "ERRCMS0008";

		// Token: 0x040014A7 RID: 5287
		internal const string HighAvailabilityNotSupported = "ERRCMS0009";

		// Token: 0x040014A8 RID: 5288
		internal const string SslCertificateNotSpecified = "ERRCMS0010";

		// Token: 0x040014A9 RID: 5289
		internal const string InvalidCacheHostDataSize = "ERRCMS0011";

		// Token: 0x040014AA RID: 5290
		internal const string MissingOrIncorrectConfiguration = "ERRCMS0012";

		// Token: 0x040014AB RID: 5291
		internal const string MissingOrMultipleSslCertificates = "ERRCMS0013";

		// Token: 0x040014AC RID: 5292
		internal const string CacheAdminHostsRunning = "ERRCAdmin001";

		// Token: 0x040014AD RID: 5293
		internal const string CacheAdminRequestTimeoutResultUnknown = "ERRCAdmin002";

		// Token: 0x040014AE RID: 5294
		internal const string CacheAdminTimeout = "ERRCAdmin003";

		// Token: 0x040014AF RID: 5295
		internal const string CacheAdminNullArgs = "ERRCAdmin004";

		// Token: 0x040014B0 RID: 5296
		internal const string CacheAdminUnknownError = "ERRCAdmin005";

		// Token: 0x040014B1 RID: 5297
		internal const string CacheAdminQuorumNotUp = "ERRCAdmin006";

		// Token: 0x040014B2 RID: 5298
		internal const string CacheAdminStoreAccessFailure = "ERRCAdmin007";

		// Token: 0x040014B3 RID: 5299
		internal const string CacheAdminHostsNotRunning = "ERRCAdmin008";

		// Token: 0x040014B4 RID: 5300
		internal const string CacheAdminCacheNotPresent = "ERRCAdmin009";

		// Token: 0x040014B5 RID: 5301
		internal const string CacheAdminHostNotPresent = "ERRCAdmin010";

		// Token: 0x040014B6 RID: 5302
		internal const string CacheAdminCacheAlreadyPresent = "ERRCAdmin011";

		// Token: 0x040014B7 RID: 5303
		internal const string CacheAdminRegionNotPresent = "ERRCAdmin012";

		// Token: 0x040014B8 RID: 5304
		internal const string CacheAdminNoQuorumIfHostStopped = "ERRCAdmin013";

		// Token: 0x040014B9 RID: 5305
		internal const string CacheAdminNoSeedNodes = "ERRCAdmin014";

		// Token: 0x040014BA RID: 5306
		internal const string CacheAdminHostRunning = "ERRCAdmin015";

		// Token: 0x040014BB RID: 5307
		internal const string CacheAdminHostNotRunning = "ERRCAdmin016";

		// Token: 0x040014BC RID: 5308
		internal const string CacheAdminConfigDeleteHostError = "ERRCAdmin017";

		// Token: 0x040014BD RID: 5309
		internal const string CacheAdminConfigAddHostError = "ERRCAdmin018";

		// Token: 0x040014BE RID: 5310
		internal const string CacheAdminDefaultCacheCreateFailure = "ERRCAdmin019";

		// Token: 0x040014BF RID: 5311
		internal const string CacheAdminDeleteInProgress = "ERRCAdmin020";

		// Token: 0x040014C0 RID: 5312
		internal const string CacheAdminCacheCreationInconsistencyFailure = "ERRCAdmin021";

		// Token: 0x040014C1 RID: 5313
		internal const string CacheAdminHostNameResolveFailure = "ERRCAdmin022";

		// Token: 0x040014C2 RID: 5314
		internal const string CacheAdminNoHosts = "ERRCAdmin023";

		// Token: 0x040014C3 RID: 5315
		internal const string CacheAdminClusterRefreshFailed = "ERRCAdmin024";

		// Token: 0x040014C4 RID: 5316
		internal const string CacheAdminClusterTimeout = "ERRCAdmin025";

		// Token: 0x040014C5 RID: 5317
		internal const string CacheAdminRemoteRegistryAccessFailed = "ERRCAdmin026";

		// Token: 0x040014C6 RID: 5318
		internal const string CacheAdminWindowsAccountInvalid = "ERRCAdmin027";

		// Token: 0x040014C7 RID: 5319
		internal const string CacheAdminWindowsAccountAlreadyPresent = "ERRCAdmin028";

		// Token: 0x040014C8 RID: 5320
		internal const string CacheAdminWindowsAccountNotPresent = "ERRCAdmin029";

		// Token: 0x040014C9 RID: 5321
		internal const string CacheAdminGrantClientAccountErrorFormat = "ERRCAdmin030";

		// Token: 0x040014CA RID: 5322
		internal const string CacheAdminRevokeClientAccountErrorFormat = "ERRCAdmin031";

		// Token: 0x040014CB RID: 5323
		internal const string CacheAdminInvalidOperation = "ERRCAdmin032";

		// Token: 0x040014CC RID: 5324
		internal const string CacheAdminClusterNotReady = "ERRCAdmin033";

		// Token: 0x040014CD RID: 5325
		internal const string CacheAdminHostRefreshFailed = "ERRCAdmin034";

		// Token: 0x040014CE RID: 5326
		internal const string CacheAdminShutdownInProgress = "ERRCAdmin035";

		// Token: 0x040014CF RID: 5327
		internal const string CacheAdminShutdownNotInProgress = "ERRCAdmin036";

		// Token: 0x040014D0 RID: 5328
		internal const string CacheAdminCancelShutdownError = "ERRCAdmin037";

		// Token: 0x040014D1 RID: 5329
		internal const string CacheAdminOperationNotSupported = "ERRCAdmin038";

		// Token: 0x040014D2 RID: 5330
		internal const string CacheAdminHostNotReachable = "ERRCAdmin039";

		// Token: 0x040014D3 RID: 5331
		internal const string CacheAdminHostsNotReachable = "ERRCAdmin040";

		// Token: 0x040014D4 RID: 5332
		internal const string CacheAdminShutdownNotPossible = "ERRCAdmin041";

		// Token: 0x040014D5 RID: 5333
		internal const string CacheAdminClusterSettingsReadError = "ERRPS001";

		// Token: 0x040014D6 RID: 5334
		internal const string CacheAdminInvalidClusterSettings = "ERRPS002";

		// Token: 0x040014D7 RID: 5335
		internal const string CacheAdminMaxCachesCreated = "ERRPS003";

		// Token: 0x040014D8 RID: 5336
		internal const string CacheAdminPortsDuplicated = "ERRPS004";

		// Token: 0x040014D9 RID: 5337
		internal const string CacheAdminClusterDown = "ERRPS005";

		// Token: 0x040014DA RID: 5338
		internal const string CacheAdminStatsIncorrect = "ERRPS006";

		// Token: 0x040014DB RID: 5339
		internal const string CacheAdminInvalidSecuritySettings = "ERRPS007";

		// Token: 0x040014DC RID: 5340
		internal const string CacheAdminInstallPathReadError = "ERRPS008";

		// Token: 0x040014DD RID: 5341
		internal const string CacheAdminOperationError = "ERRPS011";

		// Token: 0x040014DE RID: 5342
		internal const string CacheAdminHostOperationError = "ERRPS012";

		// Token: 0x040014DF RID: 5343
		internal const string CacheAdminClusterSettingsNotProvided = "ERRPS013";

		// Token: 0x040014E0 RID: 5344
		internal const string CacheAdminMinSecondariesOutOfBounds = "ERRPS014";

		// Token: 0x040014E1 RID: 5345
		internal const string CacheAdminNoValuesProvided = "ERRPS015";

		// Token: 0x040014E2 RID: 5346
		internal const string CacheAdminInvalidParameters = "ERRPS016";

		// Token: 0x040014E3 RID: 5347
		internal const string CacheAdminNullProviderSettingsKey = "ERRPS017";

		// Token: 0x040014E4 RID: 5348
		internal const string CacheAdminProviderTypeUnspecified = "ERRPS018";

		// Token: 0x040014E5 RID: 5349
		internal const string CacheAdminIncompleteParameters = "ERRPS019";

		// Token: 0x040014E6 RID: 5350
		internal const string CacheAdminInvalidCacheName = "ERRPS020";

		// Token: 0x040014E7 RID: 5351
		internal const string CacheAdminCacheConfigWriteError = "ERRPS021";

		// Token: 0x040014E8 RID: 5352
		internal const string CacheAdminHostConfigWriteError = "ERRPS022";

		// Token: 0x040014E9 RID: 5353
		internal const string CacheAdminInvalidParametersForHost = "ERRPS023";

		// Token: 0x040014EA RID: 5354
		internal const string CacheAdminCommandNotAllowed = "ERRPS024";

		// Token: 0x040014EB RID: 5355
		internal const string CacheAdminIncompatibleExpirationParameters = "ERRPS025";

		// Token: 0x040014EC RID: 5356
		internal const string NetworkShareAsLocalPathError = "NetworkShareAsLocalPathError";

		// Token: 0x040014ED RID: 5357
		internal const string NetworkShareFolderConnectionError = "NetworkShareFolderConnectionError";

		// Token: 0x040014EE RID: 5358
		internal const string ClusterConfigReadError = "ClusterConfigReadError";

		// Token: 0x040014EF RID: 5359
		internal const string ClusterConfigConnectionError = "ClusterConfigConnectionError";

		// Token: 0x040014F0 RID: 5360
		internal const string NewNetworkShareSetupError = "NewNetworkShareSetupError";

		// Token: 0x040014F1 RID: 5361
		internal const string ConnectionSettingsRegistrySaveError = "ConnectionSettingsRegistrySaveError";

		// Token: 0x040014F2 RID: 5362
		internal const string InstallPathMissingError = "InstallPathMissingError";

		// Token: 0x040014F3 RID: 5363
		internal const string HostAdditionFailureError = "HostAdditionFailureError";

		// Token: 0x040014F4 RID: 5364
		internal const string IncompleteConnectionParameters = "IncompleteConnectionParameters";

		// Token: 0x040014F5 RID: 5365
		internal const string PortAlreadyInUseError = "PortAlreadyInUseError";

		// Token: 0x040014F6 RID: 5366
		internal const string ClusterAlreadyInitialized = "ClusterAlreadyInitialized";

		// Token: 0x040014F7 RID: 5367
		internal const string ClusterNotInitialized = "ClusterNotInitialized";

		// Token: 0x040014F8 RID: 5368
		internal const string PermissionsError = "PermissionsError";

		// Token: 0x040014F9 RID: 5369
		internal const string HostDeletionFailureError = "HostDeletionFailureError";

		// Token: 0x040014FA RID: 5370
		internal const string NonDomainBlockedAccount = "NonDomainBlockedAccount";

		// Token: 0x040014FB RID: 5371
		internal const string DomainBlockedAccount = "DomainBlockedAccount";

		// Token: 0x040014FC RID: 5372
		internal const string OffloadingWithXml = "OffloadingWithXml";

		// Token: 0x040014FD RID: 5373
		internal const string IncompatibleExpirationParameters = "IncompatibleExpirationParameters";

		// Token: 0x040014FE RID: 5374
		internal const string ServiceAccessError = "ServiceAccessError";

		// Token: 0x040014FF RID: 5375
		internal const string ServiceNotStopped = "ServiceNotStopped";

		// Token: 0x04001500 RID: 5376
		internal const string HostEntryNotFound = "HostEntryNotFound";

		// Token: 0x04001501 RID: 5377
		internal const string AdminAlreadyConfigured = "AdminAlreadyConfigured";

		// Token: 0x04001502 RID: 5378
		internal const string ServiceAlreadyConfigured = "ServiceAlreadyConfigured";

		// Token: 0x04001503 RID: 5379
		internal const string AdminNotConfigured = "AdminNotConfigured";

		// Token: 0x04001504 RID: 5380
		internal const string ServiceNotConfigured = "ServiceNotConfigured";

		// Token: 0x04001505 RID: 5381
		internal const string GetComputerDomainError = "GetComputerDomainError";

		// Token: 0x04001506 RID: 5382
		internal const string TestConnectionFailed = "TestConnectionFailed";

		// Token: 0x04001507 RID: 5383
		internal const string RegistryAccessFailed = "RegistryAccessFailed";

		// Token: 0x04001508 RID: 5384
		internal const string ConfigurationStateSaveError = "ConfigurationStateSaveError";

		// Token: 0x04001509 RID: 5385
		internal const string PortDuplicationError = "PortDuplicationError";

		// Token: 0x0400150A RID: 5386
		internal const string NonDomainNWService = "NonDomainNWService";

		// Token: 0x0400150B RID: 5387
		internal const string NetworkShareFilePermissionsError = "NetworkShareFilePermissionsError";

		// Token: 0x0400150C RID: 5388
		internal const string SqlAuthenticationNotSupported = "SqlAuthenticationNotSupported";

		// Token: 0x0400150D RID: 5389
		internal const string HostAlreadyPresent = "HostAlreadyPresent";

		// Token: 0x0400150E RID: 5390
		internal const string MemoryPoolExhausted = "MemoryPoolExhausted";

		// Token: 0x0400150F RID: 5391
		internal const string ReadThroughProviderFailure = "ERRCA0025";

		// Token: 0x04001510 RID: 5392
		internal const string ReadThroughProviderDidNotReturnResult = "ERRCA0026";

		// Token: 0x04001511 RID: 5393
		internal const string ReadThroughProviderNotFound = "ERRCA0027";

		// Token: 0x04001512 RID: 5394
		internal const string AuthorizationTokenNotValid = "ERRCA0029";

		// Token: 0x04001513 RID: 5395
		internal const string AcsTokenRequestFailedAuthError = "ERRCA0030";

		// Token: 0x04001514 RID: 5396
		internal const string CacheDisabled = "ERRCA0031";
	}
}
