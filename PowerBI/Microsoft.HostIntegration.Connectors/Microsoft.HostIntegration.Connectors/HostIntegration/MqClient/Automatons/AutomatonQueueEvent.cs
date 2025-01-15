using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A90 RID: 2704
	public enum AutomatonQueueEvent
	{
		// Token: 0x040042C1 RID: 17089
		Stop = -1,
		// Token: 0x040042C2 RID: 17090
		Open,
		// Token: 0x040042C3 RID: 17091
		Timer,
		// Token: 0x040042C4 RID: 17092
		ServerData,
		// Token: 0x040042C5 RID: 17093
		UpdateDeterminant,
		// Token: 0x040042C6 RID: 17094
		AttachFailed,
		// Token: 0x040042C7 RID: 17095
		DataToSend,
		// Token: 0x040042C8 RID: 17096
		Quiesced,
		// Token: 0x040042C9 RID: 17097
		QmFailed,
		// Token: 0x040042CA RID: 17098
		Close,
		// Token: 0x040042CB RID: 17099
		QuiesceDetach,
		// Token: 0x040042CC RID: 17100
		AsyncPutDone,
		// Token: 0x040042CD RID: 17101
		Receive,
		// Token: 0x040042CE RID: 17102
		CheckTimer,
		// Token: 0x040042CF RID: 17103
		MakeMessage,
		// Token: 0x040042D0 RID: 17104
		MessageReceived,
		// Token: 0x040042D1 RID: 17105
		QmFailedPut,
		// Token: 0x040042D2 RID: 17106
		InFailedState,
		// Token: 0x040042D3 RID: 17107
		Detached,
		// Token: 0x040042D4 RID: 17108
		DisconnectQm,
		// Token: 0x040042D5 RID: 17109
		Disconnected,
		// Token: 0x040042D6 RID: 17110
		QmFailedGet
	}
}
