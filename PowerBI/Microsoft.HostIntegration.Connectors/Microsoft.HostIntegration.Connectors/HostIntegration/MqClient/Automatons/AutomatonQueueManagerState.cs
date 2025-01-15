using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A95 RID: 2709
	public enum AutomatonQueueManagerState
	{
		// Token: 0x0400435E RID: 17246
		UnConnected,
		// Token: 0x0400435F RID: 17247
		ConnectingTcp,
		// Token: 0x04004360 RID: 17248
		Handshake,
		// Token: 0x04004361 RID: 17249
		Authorization,
		// Token: 0x04004362 RID: 17250
		HandshakeFailed,
		// Token: 0x04004363 RID: 17251
		AttachingTcp,
		// Token: 0x04004364 RID: 17252
		SocketAction,
		// Token: 0x04004365 RID: 17253
		ConnectingQm,
		// Token: 0x04004366 RID: 17254
		DataFlow,
		// Token: 0x04004367 RID: 17255
		TxnOpening,
		// Token: 0x04004368 RID: 17256
		TxnOpen,
		// Token: 0x04004369 RID: 17257
		TxnStarting,
		// Token: 0x0400436A RID: 17258
		InTxn,
		// Token: 0x0400436B RID: 17259
		TxnEnding,
		// Token: 0x0400436C RID: 17260
		TxnEnded,
		// Token: 0x0400436D RID: 17261
		TxnPreparing,
		// Token: 0x0400436E RID: 17262
		TxnPrepared,
		// Token: 0x0400436F RID: 17263
		TxnFinalizing,
		// Token: 0x04004370 RID: 17264
		TxnCatchup,
		// Token: 0x04004371 RID: 17265
		TxnClosing,
		// Token: 0x04004372 RID: 17266
		RecoveryNoTxn,
		// Token: 0x04004373 RID: 17267
		RecoveryStarting,
		// Token: 0x04004374 RID: 17268
		RecoveryActive,
		// Token: 0x04004375 RID: 17269
		RecoveryEnding,
		// Token: 0x04004376 RID: 17270
		RecoveryIdle,
		// Token: 0x04004377 RID: 17271
		RecoveryPreparing,
		// Token: 0x04004378 RID: 17272
		RecoveryPrepared,
		// Token: 0x04004379 RID: 17273
		RecoveryFinalizing,
		// Token: 0x0400437A RID: 17274
		RecoveryRollbackOnly,
		// Token: 0x0400437B RID: 17275
		RecoveryHeuristic,
		// Token: 0x0400437C RID: 17276
		RecoveryForgetting,
		// Token: 0x0400437D RID: 17277
		Disconnecting,
		// Token: 0x0400437E RID: 17278
		DetachingTcp,
		// Token: 0x0400437F RID: 17279
		OpeningClosing,
		// Token: 0x04004380 RID: 17280
		OpenCloseTxnOpen,
		// Token: 0x04004381 RID: 17281
		OpenCloseInTxn,
		// Token: 0x04004382 RID: 17282
		ServerClosing,
		// Token: 0x04004383 RID: 17283
		DataFlowFailed
	}
}
