using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A7 RID: 423
	internal enum DiagEventName
	{
		// Token: 0x0400098F RID: 2447
		Started,
		// Token: 0x04000990 RID: 2448
		Completed,
		// Token: 0x04000991 RID: 2449
		RequestRecievedDrm,
		// Token: 0x04000992 RID: 2450
		PutStarted,
		// Token: 0x04000993 RID: 2451
		ReplicationQueued,
		// Token: 0x04000994 RID: 2452
		ReplicationReqRecv,
		// Token: 0x04000995 RID: 2453
		RecievedDom,
		// Token: 0x04000996 RID: 2454
		PacketRead,
		// Token: 0x04000997 RID: 2455
		RequestCreated,
		// Token: 0x04000998 RID: 2456
		DMOperation,
		// Token: 0x04000999 RID: 2457
		RespondedDom,
		// Token: 0x0400099A RID: 2458
		ResponseFromStore,
		// Token: 0x0400099B RID: 2459
		SendingResponse,
		// Token: 0x0400099C RID: 2460
		ReplicationSequenceGenerated,
		// Token: 0x0400099D RID: 2461
		AddReplQueue,
		// Token: 0x0400099E RID: 2462
		RegionCreated,
		// Token: 0x0400099F RID: 2463
		RegionDeleted,
		// Token: 0x040009A0 RID: 2464
		KeyNotFound,
		// Token: 0x040009A1 RID: 2465
		SocketSend,
		// Token: 0x040009A2 RID: 2466
		ReplicationAcked,
		// Token: 0x040009A3 RID: 2467
		PartitionNotFound,
		// Token: 0x040009A4 RID: 2468
		ReplicationCompleted,
		// Token: 0x040009A5 RID: 2469
		DispatchToDrm,
		// Token: 0x040009A6 RID: 2470
		DeferredDrmCallback,
		// Token: 0x040009A7 RID: 2471
		Pending,
		// Token: 0x040009A8 RID: 2472
		Acked,
		// Token: 0x040009A9 RID: 2473
		UpdateRegionStats,
		// Token: 0x040009AA RID: 2474
		Nacked,
		// Token: 0x040009AB RID: 2475
		DuplicateKeyFound,
		// Token: 0x040009AC RID: 2476
		Throttled,
		// Token: 0x040009AD RID: 2477
		Error,
		// Token: 0x040009AE RID: 2478
		ErrorLogging,
		// Token: 0x040009AF RID: 2479
		MemoryStatsUpdated,
		// Token: 0x040009B0 RID: 2480
		ProcessSecondaryRequest,
		// Token: 0x040009B1 RID: 2481
		ResponseProcessing
	}
}
