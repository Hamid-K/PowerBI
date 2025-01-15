using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A91 RID: 2705
	public enum AutomatonQueueState
	{
		// Token: 0x040042D8 RID: 17112
		Closed,
		// Token: 0x040042D9 RID: 17113
		Opening,
		// Token: 0x040042DA RID: 17114
		DataFlowSend,
		// Token: 0x040042DB RID: 17115
		DataFlowSent,
		// Token: 0x040042DC RID: 17116
		DataFlowSentAsync,
		// Token: 0x040042DD RID: 17117
		ReadAhead,
		// Token: 0x040042DE RID: 17118
		ReadAheadWait,
		// Token: 0x040042DF RID: 17119
		ReadAheadWaited,
		// Token: 0x040042E0 RID: 17120
		ReadAheadReceiving,
		// Token: 0x040042E1 RID: 17121
		DataFlowReceive,
		// Token: 0x040042E2 RID: 17122
		DataFlowWait,
		// Token: 0x040042E3 RID: 17123
		ReceivingAsync,
		// Token: 0x040042E4 RID: 17124
		ReceivedAsync,
		// Token: 0x040042E5 RID: 17125
		DetachingSend,
		// Token: 0x040042E6 RID: 17126
		DetachingReceive,
		// Token: 0x040042E7 RID: 17127
		Closing
	}
}
