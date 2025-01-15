using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE9 RID: 2793
	public enum SocketActionType
	{
		// Token: 0x04004593 RID: 17811
		Unknown,
		// Token: 0x04004594 RID: 17812
		NewQmRequest,
		// Token: 0x04004595 RID: 17813
		QmDisconnected,
		// Token: 0x04004596 RID: 17814
		QuiescedChannel,
		// Token: 0x04004597 RID: 17815
		QuiescedQueueManager = 7,
		// Token: 0x04004598 RID: 17816
		NewQmResponse,
		// Token: 0x04004599 RID: 17817
		Quiescing
	}
}
