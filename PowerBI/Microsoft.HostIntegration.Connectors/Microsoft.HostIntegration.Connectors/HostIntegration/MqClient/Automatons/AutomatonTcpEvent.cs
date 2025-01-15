using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACE RID: 2766
	public enum AutomatonTcpEvent
	{
		// Token: 0x0400447C RID: 17532
		Stop = -1,
		// Token: 0x0400447D RID: 17533
		Connect,
		// Token: 0x0400447E RID: 17534
		Connected,
		// Token: 0x0400447F RID: 17535
		DataReceived,
		// Token: 0x04004480 RID: 17536
		Failed,
		// Token: 0x04004481 RID: 17537
		SslFailed,
		// Token: 0x04004482 RID: 17538
		StartHandshake,
		// Token: 0x04004483 RID: 17539
		StartTransfer,
		// Token: 0x04004484 RID: 17540
		DataToSend,
		// Token: 0x04004485 RID: 17541
		AsyncDataToSend,
		// Token: 0x04004486 RID: 17542
		SendToQm,
		// Token: 0x04004487 RID: 17543
		AsyncSendDone,
		// Token: 0x04004488 RID: 17544
		InitialDataInfo,
		// Token: 0x04004489 RID: 17545
		SendTimer,
		// Token: 0x0400448A RID: 17546
		ResetSendTime,
		// Token: 0x0400448B RID: 17547
		ReceiveTimer,
		// Token: 0x0400448C RID: 17548
		ReceiveTime60,
		// Token: 0x0400448D RID: 17549
		QmAttach,
		// Token: 0x0400448E RID: 17550
		QmDetach,
		// Token: 0x0400448F RID: 17551
		Disconnect,
		// Token: 0x04004490 RID: 17552
		TcpFailed
	}
}
