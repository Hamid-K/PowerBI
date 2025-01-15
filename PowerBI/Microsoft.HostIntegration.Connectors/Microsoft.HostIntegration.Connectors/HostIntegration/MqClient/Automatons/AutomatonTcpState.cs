using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACF RID: 2767
	public enum AutomatonTcpState
	{
		// Token: 0x04004492 RID: 17554
		UnConnected,
		// Token: 0x04004493 RID: 17555
		FailedConnect,
		// Token: 0x04004494 RID: 17556
		DoSslHandshake,
		// Token: 0x04004495 RID: 17557
		DataTransfer,
		// Token: 0x04004496 RID: 17558
		DataTransferSsl,
		// Token: 0x04004497 RID: 17559
		FailedData
	}
}
