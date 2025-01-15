using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A9 RID: 425
	internal class DiagConfigManager
	{
		// Token: 0x040009B8 RID: 2488
		public const int DiagBufferSize = 300;

		// Token: 0x040009B9 RID: 2489
		public const int AvgNumEventsForRequests = 20;

		// Token: 0x040009BA RID: 2490
		public static VelocityDiagMode DiagMode = VelocityDiagMode.WarningWithFailedReq;

		// Token: 0x040009BB RID: 2491
		public static string[] EventNames = new string[]
		{
			"Started", "Completed", "RequestRecievedDrm", "PutStarted", "ReplicationQueued", "ReplicationReqRecv", "RecievedDom", "PacketRead", "RequestCreated", "DMOperation",
			"RespondedDom", "ResponseFromStore", "SendingResponse", "ReplicationSequenceGenerated", "AddReplQueue", "RegionCreated", "RegionDeleted", "KeyNotFound", "SocketSend", "ReplicationAcked",
			"PartitionNotFound", "ReplicationCompleted", "DispatchToDrm", "DeferredDrmCallback", "Pending", "Acked", "UpdateRegionStats", "Nacked", "DuplicateKeyFound", "Throttled",
			"Error", "ErrorLogging", "MemoryStatsUpdated", "ProcessSecondaryRequest", "ResponseProcessing"
		};

		// Token: 0x040009BC RID: 2492
		public static int MaxTimePerRequest = 10000;
	}
}
