using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x020007AA RID: 1962
	public enum SecurityMechanism
	{
		// Token: 0x04002970 RID: 10608
		Unknown,
		// Token: 0x04002971 RID: 10609
		DCESEC,
		// Token: 0x04002972 RID: 10610
		USRIDPWD = 3,
		// Token: 0x04002973 RID: 10611
		USRIDONL,
		// Token: 0x04002974 RID: 10612
		USRIDNWPWD,
		// Token: 0x04002975 RID: 10613
		USRSBSPWD,
		// Token: 0x04002976 RID: 10614
		USRENCPWD,
		// Token: 0x04002977 RID: 10615
		EUSRIDPWD = 9,
		// Token: 0x04002978 RID: 10616
		EUSRIDNWPWD,
		// Token: 0x04002979 RID: 10617
		KERSEC,
		// Token: 0x0400297A RID: 10618
		NotSupported = -1
	}
}
