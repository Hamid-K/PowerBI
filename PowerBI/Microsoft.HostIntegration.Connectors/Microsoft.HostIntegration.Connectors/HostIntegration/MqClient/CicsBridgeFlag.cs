using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B18 RID: 2840
	[Flags]
	public enum CicsBridgeFlag
	{
		// Token: 0x040046C7 RID: 18119
		None = 0,
		// Token: 0x040046C8 RID: 18120
		PassExpiration = 1,
		// Token: 0x040046C9 RID: 18121
		TruncateNulls = 2,
		// Token: 0x040046CA RID: 18122
		SyncpointOnReturn = 4
	}
}
