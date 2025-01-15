using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007B RID: 123
	internal enum Event
	{
		// Token: 0x0400019C RID: 412
		SqlAgentNotRunning = 106,
		// Token: 0x0400019D RID: 413
		CouldNotCommunicateToCatalog,
		// Token: 0x0400019E RID: 414
		CouldNotLoadExtension,
		// Token: 0x0400019F RID: 415
		ConfigFileChanged,
		// Token: 0x040001A0 RID: 416
		InvalidConfigEntry,
		// Token: 0x040001A1 RID: 417
		CouldNotCreateTraceFile,
		// Token: 0x040001A2 RID: 418
		DenialOfService,
		// Token: 0x040001A3 RID: 419
		CantCreatePerfCounters,
		// Token: 0x040001A4 RID: 420
		CantCommunicateToReportServer,
		// Token: 0x040001A5 RID: 421
		ScheduleUpdated,
		// Token: 0x040001A6 RID: 422
		InternalError,
		// Token: 0x040001A7 RID: 423
		InvalidDBVersion,
		// Token: 0x040001A8 RID: 424
		TraceNotDefaultLocation,
		// Token: 0x040001A9 RID: 425
		NotActivated,
		// Token: 0x040001AA RID: 426
		IsDisabled,
		// Token: 0x040001AB RID: 427
		RPCFailedStart,
		// Token: 0x040001AC RID: 428
		InvalidSMTP,
		// Token: 0x040001AD RID: 429
		FailedTraceWrite,
		// Token: 0x040001AE RID: 430
		ActivationSuccessful,
		// Token: 0x040001AF RID: 431
		KeyExtractionSuccessful,
		// Token: 0x040001B0 RID: 432
		KeyImportSuccessful,
		// Token: 0x040001B1 RID: 433
		EncryptDataCleaned,
		// Token: 0x040001B2 RID: 434
		SKUMismatch,
		// Token: 0x040001B3 RID: 435
		FailedToDecryptDSN,
		// Token: 0x040001B4 RID: 436
		ConfigFileNotFound,
		// Token: 0x040001B5 RID: 437
		FailureToDecryptData,
		// Token: 0x040001B6 RID: 438
		FailureToEncryptData,
		// Token: 0x040001B7 RID: 439
		FailureToLoadConfigFile,
		// Token: 0x040001B8 RID: 440
		FailureToEncryptConfigData,
		// Token: 0x040001B9 RID: 441
		KeyDeleteSuccessful,
		// Token: 0x040001BA RID: 442
		EvaluationPeriodExpired,
		// Token: 0x040001BB RID: 443
		SetExtensionConfigFailed,
		// Token: 0x040001BC RID: 444
		WebFarmNodeActivated,
		// Token: 0x040001BD RID: 445
		AppDomainFailedToStart,
		// Token: 0x040001BE RID: 446
		AppDomainFailedToInitialize,
		// Token: 0x040001BF RID: 447
		AppDomainMaxMemoryLimitReached,
		// Token: 0x040001C0 RID: 448
		PollQueueFull,
		// Token: 0x040001C1 RID: 449
		ServerStarted,
		// Token: 0x040001C2 RID: 450
		ServerFailedToStart,
		// Token: 0x040001C3 RID: 451
		ServerStopped,
		// Token: 0x040001C4 RID: 452
		ServerFailedToStop
	}
}
