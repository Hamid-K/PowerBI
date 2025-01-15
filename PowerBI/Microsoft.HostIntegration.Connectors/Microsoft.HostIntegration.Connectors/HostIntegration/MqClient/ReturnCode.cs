using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BC9 RID: 3017
	public enum ReturnCode
	{
		// Token: 0x04004F60 RID: 20320
		Ok,
		// Token: 0x04004F61 RID: 20321
		InternalReturnCode = 1000000,
		// Token: 0x04004F62 RID: 20322
		TcpConnectFailed,
		// Token: 0x04004F63 RID: 20323
		TcpSslConnectFailed,
		// Token: 0x04004F64 RID: 20324
		TcpPoolingDifferentSsl,
		// Token: 0x04004F65 RID: 20325
		QmConnectTcpConnectFailed,
		// Token: 0x04004F66 RID: 20326
		QmConnectTcpDisconnected,
		// Token: 0x04004F67 RID: 20327
		QmConnectInitialDataRejectedCcsid,
		// Token: 0x04004F68 RID: 20328
		QmConnectInitialDataRejectedConversations0,
		// Token: 0x04004F69 RID: 20329
		QmConnectInitialDataRejectedEncoding,
		// Token: 0x04004F6A RID: 20330
		QmConnectInitialDataRejectedErrorFlag2,
		// Token: 0x04004F6B RID: 20331
		QmConnectInitialDataRejectedFap,
		// Token: 0x04004F6C RID: 20332
		QmConnectInitialDataRejectedTransmissionSize,
		// Token: 0x04004F6D RID: 20333
		QmConnectUnexpected,
		// Token: 0x04004F6E RID: 20334
		QmConnectMqConnFailed,
		// Token: 0x04004F6F RID: 20335
		QmConnectNoChannel,
		// Token: 0x04004F70 RID: 20336
		QmAsyncStatusFailedQuiescing,
		// Token: 0x04004F71 RID: 20337
		QmAsyncStatusFailedHandle,
		// Token: 0x04004F72 RID: 20338
		QSendMaximumMessageSize,
		// Token: 0x04004F73 RID: 20339
		QOpenFailedQmFailed,
		// Token: 0x04004F74 RID: 20340
		QOpenFailedQmQuiesced,
		// Token: 0x04004F75 RID: 20341
		QOpenFailedTcpFailed,
		// Token: 0x04004F76 RID: 20342
		QOpenFailedFinalizing,
		// Token: 0x04004F77 RID: 20343
		QSendFailedUnknown,
		// Token: 0x04004F78 RID: 20344
		QSendFailedQmFailed,
		// Token: 0x04004F79 RID: 20345
		QSendFailedQmQuiesced,
		// Token: 0x04004F7A RID: 20346
		QReceiveFailedQmQuiesced,
		// Token: 0x04004F7B RID: 20347
		QReceiveFailedQmFailed,
		// Token: 0x04004F7C RID: 20348
		QReceiveFailedUnknown,
		// Token: 0x04004F7D RID: 20349
		XaEndClosing,
		// Token: 0x04004F7E RID: 20350
		XaEndDisconnecting,
		// Token: 0x04004F7F RID: 20351
		XaFinalizeClosing,
		// Token: 0x04004F80 RID: 20352
		XaFinalizeDisconnecting,
		// Token: 0x04004F81 RID: 20353
		XaPrepareClosing,
		// Token: 0x04004F82 RID: 20354
		XaPrepareDisconnecting,
		// Token: 0x04004F83 RID: 20355
		XaStartClosing,
		// Token: 0x04004F84 RID: 20356
		XaStartDisconnecting,
		// Token: 0x04004F85 RID: 20357
		XaRollback,
		// Token: 0x04004F86 RID: 20358
		XaRollbackCommunicationFailure,
		// Token: 0x04004F87 RID: 20359
		XaRollbackDeadlock,
		// Token: 0x04004F88 RID: 20360
		XaRollbackIntegrity,
		// Token: 0x04004F89 RID: 20361
		XaRollbackOther,
		// Token: 0x04004F8A RID: 20362
		XaRollbackProtocol,
		// Token: 0x04004F8B RID: 20363
		XaRollbackTimeout,
		// Token: 0x04004F8C RID: 20364
		XaRollbackTransient,
		// Token: 0x04004F8D RID: 20365
		XaNoMigrate,
		// Token: 0x04004F8E RID: 20366
		XaHeuristicHazard,
		// Token: 0x04004F8F RID: 20367
		XaHeuristicCommit,
		// Token: 0x04004F90 RID: 20368
		XaHeuristicRollback,
		// Token: 0x04004F91 RID: 20369
		XaHeuristicMix,
		// Token: 0x04004F92 RID: 20370
		XaRetry,
		// Token: 0x04004F93 RID: 20371
		XaReadOnly,
		// Token: 0x04004F94 RID: 20372
		XaOutstandingAsynchronousOperation,
		// Token: 0x04004F95 RID: 20373
		XaResourceManagerError,
		// Token: 0x04004F96 RID: 20374
		XaInvalidXid,
		// Token: 0x04004F97 RID: 20375
		XaInvalidArguments,
		// Token: 0x04004F98 RID: 20376
		XaProtocol,
		// Token: 0x04004F99 RID: 20377
		XaResourceManagerUnavailable,
		// Token: 0x04004F9A RID: 20378
		XaDuplicateXid,
		// Token: 0x04004F9B RID: 20379
		XaWorkOutsideTransaction,
		// Token: 0x04004F9C RID: 20380
		XaEnlistmentFailed,
		// Token: 0x04004F9D RID: 20381
		StaleTransaction,
		// Token: 0x04004F9E RID: 20382
		EnlistDataFlowFailed,
		// Token: 0x04004F9F RID: 20383
		EnlistServerClosing,
		// Token: 0x04004FA0 RID: 20384
		EnlistTcpFailed,
		// Token: 0x04004FA1 RID: 20385
		QmConnectCipherSpec,
		// Token: 0x04004FA2 RID: 20386
		QmConnectSslExpected
	}
}
