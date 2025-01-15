using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public static class PipelineErrorCodes
	{
		// Token: 0x0400018F RID: 399
		public const string UnknownError = "DM_GWPipeline_UnknownError";

		// Token: 0x04000190 RID: 400
		public const string ClientError_GatewayUnreachable = "DM_GWPipeline_Client_GatewayUnreachable";

		// Token: 0x04000191 RID: 401
		public const string ClientError_AsyncOperationExpired = "DM_GWPipeline_Client_AsyncOperationExpired";

		// Token: 0x04000192 RID: 402
		public const string ClientError_ReceivedUncompressedDataExceedingLimit = "DM_GWPipeline_Client_ReceivedUncompressedDataExceedingLimit";

		// Token: 0x04000193 RID: 403
		public const string ClientError_LoadBalancer_NoCandidateAvailable = "DM_GWPipeline_Client_LoadBalancer_NoCandidateAvailable";

		// Token: 0x04000194 RID: 404
		public const string ClientError_MaxPacketLimitReached = "DM_GWPipeline_Client_MaxPacketLimitReached";

		// Token: 0x04000195 RID: 405
		public const string ClientError_AsyncOperationStreamingRetriesExhausted = "DM_GWPipeline_Client_AsyncOperationStreamingRetriesExhausted";

		// Token: 0x04000196 RID: 406
		public const string ClientError_OAuthTokenLoginFailedError = "DM_GWPipeline_Client_OAuthTokenLoginFailedError";

		// Token: 0x04000197 RID: 407
		public const string ClientError_OAuthTokenLoginFailedUserError = "DM_GWPipeline_Client_OAuthTokenLoginFailedUserError";

		// Token: 0x04000198 RID: 408
		public const string ClientError_OAuthTokenRefreshFailedError = "DM_GWPipeline_Client_OAuthTokenRefreshFailedError";

		// Token: 0x04000199 RID: 409
		public const string ClientError_DiscoverCustomConnectorsOnGatewayError = "DM_GWPipeline_Client_DiscoverCustomConnectorsOnGatewayError";

		// Token: 0x0400019A RID: 410
		public const string ClientError_GetOAuthResourceError = "DM_GWPipeline_Client_GetOAuthResourceError";

		// Token: 0x0400019B RID: 411
		public const string GatewayError_CouldNotUpdateConfiguration = "DM_GWPipeline_Gateway_CouldNotUpdateConfiguration";

		// Token: 0x0400019C RID: 412
		public const string GatewayError_DatabaseConnectError = "DM_GWPipeline_Gateway_DatabaseConnectError";

		// Token: 0x0400019D RID: 413
		public const string GatewayError_DataSourceAccessError = "DM_GWPipeline_Gateway_DataSourceAccessError";

		// Token: 0x0400019E RID: 414
		public const string GatewayError_FeatureNotSupported = "DM_GWPipeline_Gateway_FeatureNotSupported";

		// Token: 0x0400019F RID: 415
		public const string GatewayError_ImpersonationError = "DM_GWPipeline_Gateway_ImpersonationError";

		// Token: 0x040001A0 RID: 416
		public const string GatewayError_BadUsernameFormat = "DM_GWPipeline_Gateway_BadUsernameFormat";

		// Token: 0x040001A1 RID: 417
		public const string GatewayError_AuthenticationTypeNotSupported = "DM_GWPipeline_Gateway_HttpAuthenticationTypeNotSupported";

		// Token: 0x040001A2 RID: 418
		public const string GatewayError_PropertyParseException = "DM_GWPipeline_Gateway_XmlPropertyParseException";

		// Token: 0x040001A3 RID: 419
		public const string GatewayError_InvalidConnectionCredentials = "DM_GWPipeline_Gateway_InvalidConnectionCredentials";

		// Token: 0x040001A4 RID: 420
		public const string GatewayError_ServerUnreachable = "DM_GWPipeline_Gateway_ServerUnreachable";

		// Token: 0x040001A5 RID: 421
		public const string GatewayError_ProviderUnsupported = "DM_GWPipeline_Gateway_ProviderUnsupported";

		// Token: 0x040001A6 RID: 422
		public const string GatewayError_UnsupportedServerVersion = "DM_GWPipeline_Gateway_UnsupportedServerVersion";

		// Token: 0x040001A7 RID: 423
		public const string GatewayError_OleDbProviderNameNotRegistered = "DM_GWPipeline_Gateway_OleDbProviderNameNotRegistered";

		// Token: 0x040001A8 RID: 424
		public const string GatewayError_InvalidMashupConnectionString = "DM_GWPipeline_Gateway_InvalidMashupConnectionString";

		// Token: 0x040001A9 RID: 425
		public const string GatewayError_DatabaseLoginError = "DM_GWPipeline_Gateway_DatabaseLoginError";

		// Token: 0x040001AA RID: 426
		public const string GatewayError_TimeoutException = "DM_GWPipeline_Gateway_TimeoutException";

		// Token: 0x040001AB RID: 427
		public const string GatewayError_InvalidObjectNameException = "DM_GWPipeline_Gateway_InvalidObjectNameException";

		// Token: 0x040001AC RID: 428
		public const string GatewayError_ConnectionBrokenException = "DM_GWPipeline_Gateway_ConnectionBrokenException";

		// Token: 0x040001AD RID: 429
		public const string GatewayError_MemoryError = "DM_GWPipeline_Gateway_MemoryError";

		// Token: 0x040001AE RID: 430
		public const string GatewayError_MissingStructureError = "DM_GWPipeline_Gateway_MissingStructureError";

		// Token: 0x040001AF RID: 431
		public const string GatewayError_UnprocessedStructureError = "DM_GWPipeline_Gateway_UnprocessedStructureError";

		// Token: 0x040001B0 RID: 432
		public const string GatewayError_ModelLoadError = "DM_GWPipeline_Gateway_ModelLoadError";

		// Token: 0x040001B1 RID: 433
		public const string GatewayError_CancellationError = "DM_GWPipeline_Gateway_CancellationError";

		// Token: 0x040001B2 RID: 434
		public const string GatewayError_CanceledError = "DM_GWPipeline_Gateway_CanceledError";

		// Token: 0x040001B3 RID: 435
		public const string GatewayError_TimeoutError = "DM_GWPipeline_Gateway_TimeoutError";

		// Token: 0x040001B4 RID: 436
		public const string GatewayError_QueryExecutionError = "DM_GWPipeline_Gateway_QueryExecutionError";

		// Token: 0x040001B5 RID: 437
		public const string GatewayError_RecalculationRequiredError = "DM_GWPipeline_Gateway_RecalculationRequiredError";

		// Token: 0x040001B6 RID: 438
		public const string GatewayError_AdomdConnectError = "DM_GWPipeline_Gateway_AdomdConnectError";

		// Token: 0x040001B7 RID: 439
		public const string GatewayError_MashupDataAccessError = "DM_GWPipeline_Gateway_MashupDataAccessError";

		// Token: 0x040001B8 RID: 440
		public const string GatewayError_CancellationOrTimeoutError = "DM_GWPipeline_Gateway_CancellationOrTimeoutError";

		// Token: 0x040001B9 RID: 441
		public const string GatewayError_SignatureNotMatchCredentialError = "DM_GWPipeline_Gateway_SignatureNotMatchCredentialError";

		// Token: 0x040001BA RID: 442
		public const string GatewayError_InvalidJwtSecurityTokenError = "DM_GWPipeline_Gateway_InvalidJwtSecurityTokenError";

		// Token: 0x040001BB RID: 443
		public const string GatewayError_ProviderDataAccessArgumentError = "DM_GWPipeline_Gateway_ProviderDataAccessArgumentError";

		// Token: 0x040001BC RID: 444
		public const string GatewayError_SpooledOperationMissing = "DM_GWPipeline_Gateway_SpooledOperationMissing";

		// Token: 0x040001BD RID: 445
		public const string GatewayError_AsyncOperationAlreadyInProgress = "DM_GWPipeline_Gateway_AsyncOperationAlreadyInProgress";

		// Token: 0x040001BE RID: 446
		public const string GatewayError_AsyncOperationCancelled = "DM_GWPipeline_Gateway_AsyncOperationCancelled";

		// Token: 0x040001BF RID: 447
		public const string GatewayError_ClientAsyncOperationCancelled = "DM_GWPipeline_Gateway_ClientAsyncOperationCancelled";

		// Token: 0x040001C0 RID: 448
		public const string GatewayError_ClientAsyncOperationTimeout = "DM_GWPipeline_Gateway_ClientAsyncOperationTimeout";

		// Token: 0x040001C1 RID: 449
		public const string GatewayError_AsyncOperationExpired = "DM_GWPipeline_Gateway_AsyncOperationExpired";

		// Token: 0x040001C2 RID: 450
		public const string GatewayError_AsyncOperationStreamingCancelled = "DM_GWPipeline_Gateway_AsyncOperationStreamingCancelled";

		// Token: 0x040001C3 RID: 451
		public const string GatewayError_GatewayUpgradeRequired = "DM_GWPipeline_Gateway_GatewayUpgradeRequired";

		// Token: 0x040001C4 RID: 452
		public const string GatewayError_SAMLAuthenticationCertificateCanNotBeFound = "DM_GWPipeline_Gateway_SAMLAuthenticationCertificateCanNotBeFound";

		// Token: 0x040001C5 RID: 453
		public const string GatewayError_OutOfMemoryError = "DM_GWPipeline_Gateway_OutOfMemoryError";

		// Token: 0x040001C6 RID: 454
		public const string GatewayError_SSOTestConnectionError = "DM_GWPipeline_Gateway_SSOTestConnectionError";

		// Token: 0x040001C7 RID: 455
		public const string GatewayError_FileLoadDataAccessError = "DM_GWPipeline_Gateway_FileLoadDataAccessError";

		// Token: 0x040001C8 RID: 456
		public const string GatewayError_GatewayOutOfDiskSpaceError = "DM_GWPipeline_Gateway_GatewayOutOfDiskSpaceError";

		// Token: 0x040001C9 RID: 457
		public const string GatewayError_GatewayOutOfSystemResourcesError = "DM_GWPipeline_Gateway_GatewayOutOfSystemResourcesError";

		// Token: 0x040001CA RID: 458
		public const string GatewayError_GatewayDiskQuotaError = "DM_GWPipeline_Gateway_GatewayDiskQuotaError";

		// Token: 0x040001CB RID: 459
		public const string GatewayError_GatewayNotEnoughStorageError = "DM_GWPipeline_Gateway_GatewayNotEnoughStorageError";

		// Token: 0x040001CC RID: 460
		public const string GatewayError_GatewayOverloaded = "DM_GWPipeline_Gateway_GatewayOverloaded";

		// Token: 0x040001CD RID: 461
		public const string GatewayError_GatewayOverloadedMemory = "DM_GWPipeline_Gateway_GatewayOverloadedMemory";

		// Token: 0x040001CE RID: 462
		public const string GatewayError_GatewayOverloadedProcessor = "DM_GWPipeline_Gateway_GatewayOverloadedProcessor";

		// Token: 0x040001CF RID: 463
		public const string GatewayError_GatewayOverloadedConcurrency = "DM_GWPipeline_Gateway_GatewayOverloadedConcurrency";

		// Token: 0x040001D0 RID: 464
		public const string GatewayError_SingleSignOnInformationRequired = "DM_GWPipeline_Gateway_SingleSignOnInformationRequired";

		// Token: 0x040001D1 RID: 465
		public const string GatewayError_ProvisionTenantError = "DM_GWPipeline_Gateway_ProvisionTenantError";

		// Token: 0x040001D2 RID: 466
		public const string GatewayError_InvalidAsyncPacketAcknowledgementError = "DM_GWPipeline_Gateway_InvalidAsyncPacketAcknowledgementError";

		// Token: 0x040001D3 RID: 467
		public const string GatewayError_InvalidConnectionStateError = "DM_GWPipeline_Gateway_InvalidConnectionStateError";

		// Token: 0x040001D4 RID: 468
		public const string GatewayError_InvalidDataSizesInPacketHeaderError = "DM_GWPipeline_Gateway_InvalidDataSizesInPacketHeaderError";

		// Token: 0x040001D5 RID: 469
		public const string GatewayError_MismatchCompressedDataSizeInPacketHeaderError = "DM_GWPipeline_Gateway_MismatchCompressedDataSizeInPacketHeaderError";

		// Token: 0x040001D6 RID: 470
		public const string GatewayError_InvalidMonikerSystemResponseError = "DM_GWPipeline_Gateway_InvalidMonikerSystemResponseError";

		// Token: 0x040001D7 RID: 471
		public const string GatewayError_InvalidObjectArrayArgumentError = "DM_GWPipeline_Gateway_InvalidObjectArrayArgumentError";

		// Token: 0x040001D8 RID: 472
		public const string GatewayError_InvalidOleDbCreateRowsetRequestError = "DM_GWPipeline_Gateway_InvalidOleDbCreateRowsetRequestError";

		// Token: 0x040001D9 RID: 473
		public const string GatewayError_InvalidOleDbProviderSetupError = "DM_GWPipeline_Gateway_InvalidOleDbProviderSetupError";

		// Token: 0x040001DA RID: 474
		public const string GatewayError_InvalidOleDbProviderStringError = "DM_GWPipeline_Gateway_InvalidOleDbProviderStringError";

		// Token: 0x040001DB RID: 475
		public const string GatewayError_InvalidParameterCollectionItemError = "DM_GWPipeline_Gateway_InvalidParameterCollectionItemError";

		// Token: 0x040001DC RID: 476
		public const string GatewayError_InvalidServiceBusBindingError = "DM_GWPipeline_Gateway_InvalidServiceBusBindingError";

		// Token: 0x040001DD RID: 477
		public const string GatewayError_InvalidStateForConnectionStringChangeError = "DM_GWPipeline_Gateway_InvalidStateForConnectionStringChangeError";

		// Token: 0x040001DE RID: 478
		public const string GatewayError_InvalidStateForStatefulConnectionInGatewayChangeError = "DM_GWPipeline_Gateway_InvalidStateForStatefulConnectionInGatewayChangeError";

		// Token: 0x040001DF RID: 479
		public const string GatewayError_OleDbEffectiveUserNameMissingError = "DM_GWPipeline_Gateway_OleDbEffectiveUserNameMissingError";

		// Token: 0x040001E0 RID: 480
		public const string GatewayError_OleDbEffectiveUserNameNotExpectedError = "DM_GWPipeline_Gateway_OleDbEffectiveUserNameNotExpectedError";

		// Token: 0x040001E1 RID: 481
		public const string GatewayError_OleDbGatewayResolutionError = "DM_GWPipeline_Gateway_OleDbGatewayResolutionError";

		// Token: 0x040001E2 RID: 482
		public const string GatewayError_OleDbInvalidEffectiveUserNameError = "DM_GWPipeline_Gateway_OleDbInvalidEffectiveUserNameError";

		// Token: 0x040001E3 RID: 483
		public const string GatewayError_OleDbInvalidGatewayResolutionParametersError = "DM_GWPipeline_Gateway_OleDbInvalidGatewayResolutionParametersError";

		// Token: 0x040001E4 RID: 484
		public const string GatewayError_OleDbInvalidMdDatasetError = "DM_GWPipeline_Gateway_OleDbInvalidMdDatasetError";

		// Token: 0x040001E5 RID: 485
		public const string GatewayError_OleDbInvalidRestrictionsError = "DM_GWPipeline_Gateway_OleDbInvalidRestrictionsError";

		// Token: 0x040001E6 RID: 486
		public const string GatewayError_OleDbMissingGatewayResolutionParametersError = "DM_GWPipeline_Gateway_OleDbMissingGatewayResolutionParametersError";

		// Token: 0x040001E7 RID: 487
		public const string GatewayError_OleDbProviderInstantiationError = "DM_GWPipeline_Gateway_OleDbProviderInstantiationError";

		// Token: 0x040001E8 RID: 488
		public const string GatewayError_AADAuthenticationProviderRequiredError = "DM_GWPipeline_Gateway_AADAuthenticationProviderRequiredError";

		// Token: 0x040001E9 RID: 489
		public const string GatewayError_AdomdEffectiveUserNameMissingError = "DM_GWPipeline_Gateway_AdomdEffectiveUserNameMissingError";

		// Token: 0x040001EA RID: 490
		public const string GatewayError_AdomdInvalidEffectiveUserNameError = "DM_GWPipeline_Gateway_AdomdInvalidEffectiveUserNameError";

		// Token: 0x040001EB RID: 491
		public const string GatewayError_ASDataSourceMissingEffectiveUserNameError = "DM_GWPipeline_Gateway_ASDataSourceMissingEffectiveUserNameError";

		// Token: 0x040001EC RID: 492
		public const string GatewayError_AsyncOperationClosedError = "DM_GWPipeline_Gateway_AsyncOperationClosedError";

		// Token: 0x040001ED RID: 493
		public const string GatewayError_CommandTextMissingError = "DM_GWPipeline_Gateway_CommandTextMissingError";

		// Token: 0x040001EE RID: 494
		public const string GatewayError_ConnectionNotOpenError = "DM_GWPipeline_Gateway_ConnectionNotOpenError";

		// Token: 0x040001EF RID: 495
		public const string GatewayError_ConnectionStringMissingError = "DM_GWPipeline_Gateway_ConnectionStringMissingError";

		// Token: 0x040001F0 RID: 496
		public const string GatewayError_DataflowPipelineSendOrReceiveError = "DM_GWPipeline_Gateway_DataflowPipelineSendOrReceiveError";

		// Token: 0x040001F1 RID: 497
		public const string GatewayError_DataMovementExtensionNotLoadedError = "DM_GWPipeline_Gateway_DataMovementExtensionNotLoadedError";

		// Token: 0x040001F2 RID: 498
		public const string GatewayError_DataMovementExtensionVersionNotLoadedError = "DM_GWPipeline_Gateway_DataMovementExtensionVersionNotLoadedError";

		// Token: 0x040001F3 RID: 499
		public const string GatewayError_OleDbUnsupportedTransferCapabilitiesError = "DM_GWPipeline_Gateway_OleDbUnsupportedTransferCapabilitiesError";

		// Token: 0x040001F4 RID: 500
		public const string GatewayError_DbConnectionProviderFactoryNotRegisteredError = "DM_GWPipeline_Gateway_DbConnectionProviderFactoryNotRegisteredError";

		// Token: 0x040001F5 RID: 501
		public const string GatewayError_DuplicateStatefulPoolObjectReturnError = "DM_GWPipeline_Gateway_DuplicateStatefulPoolObjectReturnError";

		// Token: 0x040001F6 RID: 502
		public const string GatewayError_ErrorCreatingOleDbRowset = "DM_GWPipeline_Gateway_ErrorCreatingOleDbRowset";

		// Token: 0x040001F7 RID: 503
		public const string GatewayError_FailedToExportCredentialStoreError = "DM_GWPipeline_Gateway_FailedToExportCredentialStoreError";

		// Token: 0x040001F8 RID: 504
		public const string GatewayError_FailedToImportCredentialStoreError = "DM_GWPipeline_Gateway_FailedToImportCredentialStoreError";

		// Token: 0x040001F9 RID: 505
		public const string GatewayError_FailedToRetrieveDbConnectionDetailsError = "DM_GWPipeline_Gateway_FailedToRetrieveDbConnectionDetailsError";

		// Token: 0x040001FA RID: 506
		public const string GatewayError_GatewayServiceFailedToStartError = "DM_GWPipeline_Gateway_GatewayServiceFailedToStartError";

		// Token: 0x040001FB RID: 507
		public const string GatewayError_GatewayServiceFailedToStopError = "DM_GWPipeline_Gateway_GatewayServiceFailedToStopError";

		// Token: 0x040001FC RID: 508
		public const string GatewayError_MismatchedConnectionStringPropertyTypeError = "DM_GWPipeline_Gateway_MismatchedConnectionStringPropertyTypeError";

		// Token: 0x040001FD RID: 509
		public const string GatewayError_MissingOleDbProviderStringError = "DM_GWPipeline_Gateway_MissingOleDbProviderStringError";

		// Token: 0x040001FE RID: 510
		public const string GatewayError_MissingRequiredConnectionStringPropertyError = "DM_GWPipeline_Gateway_MissingRequiredConnectionStringPropertyError";

		// Token: 0x040001FF RID: 511
		public const string GatewayError_MultiResourceDatasourceNotSupportedError = "DM_GWPipeline_Gateway_MultiResourceDatasourceNotSupportedError";

		// Token: 0x04000200 RID: 512
		public const string GatewayError_NonSerializablePipelineExceptionSurrogateError = "DM_GWPipeline_Gateway_NonSerializablePipelineExceptionSurrogateError";

		// Token: 0x04000201 RID: 513
		public const string GatewayError_OperationRequiresPBIDataRefreshCertificateError = "DM_GWPipeline_Gateway_OperationRequiresPBIDataRefreshCertificateError";

		// Token: 0x04000202 RID: 514
		public const string GatewayError_PacketsNotReadyToStreamError = "DM_GWPipeline_Gateway_PacketsNotReadyToStreamError";

		// Token: 0x04000203 RID: 515
		public const string GatewayError_PipelineJsonDeserializationError = "DM_GWPipeline_Gateway_PipelineJsonDeserializationError";

		// Token: 0x04000204 RID: 516
		public const string GatewayError_PipelineJsonSerializationError = "DM_GWPipeline_Gateway_PipelineJsonSerializationError";

		// Token: 0x04000205 RID: 517
		public const string GatewayError_PoolStoppingError = "DM_GWPipeline_Gateway_PoolStoppingError";

		// Token: 0x04000206 RID: 518
		public const string GatewayError_RequiredCredentailDetailsParameterMissingError = "DM_GWPipeline_Gateway_RequiredCredentailDetailsParameterMissingError";

		// Token: 0x04000207 RID: 519
		public const string GatewayError_RuntimeCheckFailedError = "DM_GWPipeline_Gateway_RuntimeCheckFailedError";

		// Token: 0x04000208 RID: 520
		public const string GatewayError_SqlServerTransactionDeadlockError = "DM_GWPipeline_Gateway_SqlServerTransactionDeadlockError";

		// Token: 0x04000209 RID: 521
		public const string GatewayError_StatefulPoolCapacityExceededError = "DM_GWPipeline_Gateway_StatefulPoolCapacityExceededError";

		// Token: 0x0400020A RID: 522
		public const string GatewayError_StatefulPoolObjectExpiredError = "DM_GWPipeline_Gateway_StatefulPoolObjectExpiredError";

		// Token: 0x0400020B RID: 523
		public const string GatewayError_UnableToConvertGatewayCredentialToMashupCredentialError = "DM_GWPipeline_Gateway_UnableToConvertGatewayCredentialToMashupCredentialError";

		// Token: 0x0400020C RID: 524
		public const string GatewayError_UnableToStartConfigurationServiceError = "DM_GWPipeline_Gateway_UnableToStartConfigurationServiceError";

		// Token: 0x0400020D RID: 525
		public const string GatewayError_UnableToStartTransferServiceError = "DM_GWPipeline_Gateway_UnableToStartTransferServiceError";

		// Token: 0x0400020E RID: 526
		public const string GatewayError_UnableToStopGatewayServicesError = "DM_GWPipeline_Gateway_UnableToStopGatewayServicesError";

		// Token: 0x0400020F RID: 527
		public const string GatewayError_UncompressedDataSizeForCompressedPacketExceededError = "DM_GWPipeline_Gateway_UncompressedDataSizeForCompressedPacketExceededError";

		// Token: 0x04000210 RID: 528
		public const string GatewayError_UncompressedDataSizeForPacketExceededError = "DM_GWPipeline_Gateway_UncompressedDataSizeForPacketExceededError";

		// Token: 0x04000211 RID: 529
		public const string GatewayError_CompressedDataSizeForPacketExceededError = "DM_GWPipeline_Gateway_CompressedDataSizeForPacketExceededError";

		// Token: 0x04000212 RID: 530
		public const string GatewayError_UnknownGatewayRequestError = "DM_GWPipeline_Gateway_UnknownGatewayRequestError";

		// Token: 0x04000213 RID: 531
		public const string GatewayError_UnsupportedAdomdAuthenticationTypeError = "DM_GWPipeline_Gateway_UnsupportedAdomdAuthenticationTypeError";

		// Token: 0x04000214 RID: 532
		public const string GatewayError_UnsupportedOleDbProviderError = "DM_GWPipeline_Gateway_UnsupportedOleDbProviderError";

		// Token: 0x04000215 RID: 533
		public const string GatewayError_XPress9CompressionError = "DM_GWPipeline_Gateway_XPress9CompressionError";

		// Token: 0x04000216 RID: 534
		public const string GatewayError_XPress9DecompressionError = "DM_GWPipeline_Gateway_XPress9DecompressionError";

		// Token: 0x04000217 RID: 535
		public const string GatewayError_FailedToSpoolPacketToDiskError = "DM_GWPipeline_Gateway_FailedToSpoolPacketToDiskError";

		// Token: 0x04000218 RID: 536
		public const string GatewayError_GatewayIODeviceError = "DM_GWPipeline_Gateway_GatewayIODeviceError";

		// Token: 0x04000219 RID: 537
		public const string GatewayError_GatewayCorruptedFileOrDirectoryError = "DM_GWPipeline_Gateway_GatewayCorruptedFileOrDirectoryError";

		// Token: 0x0400021A RID: 538
		public const string GatewayError_VNetHttpRequestFailedError = "DM_GWPipeline_Gateway_VNetHttpRequestFailedError";

		// Token: 0x0400021B RID: 539
		public const string GatewayError_VNetTransientDataAccessError = "DM_GWPipeline_Gateway_VNetTransientDataAccessError";

		// Token: 0x0400021C RID: 540
		public const string GatewayError_AdoNetProviderConnectionClosedError = "DM_GWPipeline_Gateway_AdoNetProviderConnectionClosedError";

		// Token: 0x0400021D RID: 541
		public const string GatewayError_AdoNetProviderConnectionAlreadyOpenError = "DM_GWPipeline_Gateway_AdoNetProviderConnectionAlreadyOpenError";

		// Token: 0x0400021E RID: 542
		public const string GatewayError_DiagnosticsStorageClientDisconnectedError = "DM_GWPipeline_Gateway_DiagnosticsStorageClientDisconnectedError";

		// Token: 0x0400021F RID: 543
		public const string GatewayError_MonitoredDbConnectionError = "DM_GWPipeline_Gateway_MonitoredDbConnectionError";

		// Token: 0x04000220 RID: 544
		public const string GatewayError_BadRequestDueToInvalidModelStateError = "DM_GWPipeline_Gateway_BadRequestDueToInvalidModelStateError";

		// Token: 0x04000221 RID: 545
		[Obsolete]
		public const string GatewayError_DataSourceConnectionError = "DM_GWPipeline_Gateway_DataSourceConnectionError";
	}
}
