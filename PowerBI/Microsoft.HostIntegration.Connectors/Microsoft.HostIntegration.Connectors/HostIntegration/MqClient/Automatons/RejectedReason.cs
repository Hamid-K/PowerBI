using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ADF RID: 2783
	public enum RejectedReason
	{
		// Token: 0x0400454F RID: 17743
		Ok,
		// Token: 0x04004550 RID: 17744
		Encoding,
		// Token: 0x04004551 RID: 17745
		Fap,
		// Token: 0x04004552 RID: 17746
		TransmissionSize,
		// Token: 0x04004553 RID: 17747
		Conversations0,
		// Token: 0x04004554 RID: 17748
		ErrorFlag2,
		// Token: 0x04004555 RID: 17749
		Ccsid
	}
}
