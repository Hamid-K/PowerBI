using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE1 RID: 2785
	[Flags]
	public enum ControlFlag2
	{
		// Token: 0x04004561 RID: 17761
		None = 0,
		// Token: 0x04004562 RID: 17762
		HeaderComp = 1,
		// Token: 0x04004563 RID: 17763
		MessageComp = 2,
		// Token: 0x04004564 RID: 17764
		Csh = 4,
		// Token: 0x04004565 RID: 17765
		CommitInterval = 8
	}
}
