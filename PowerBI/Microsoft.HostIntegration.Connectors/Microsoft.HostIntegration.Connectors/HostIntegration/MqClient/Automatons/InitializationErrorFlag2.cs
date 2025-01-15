using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AE6 RID: 2790
	[Flags]
	public enum InitializationErrorFlag2
	{
		// Token: 0x04004589 RID: 17801
		None = 0,
		// Token: 0x0400458A RID: 17802
		HeaderCompList = 1,
		// Token: 0x0400458B RID: 17803
		MessageCompList = 2,
		// Token: 0x0400458C RID: 17804
		SslReset = 4
	}
}
