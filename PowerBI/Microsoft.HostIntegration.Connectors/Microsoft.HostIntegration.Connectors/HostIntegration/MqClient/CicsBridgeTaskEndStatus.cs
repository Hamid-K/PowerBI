using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B1C RID: 2844
	public enum CicsBridgeTaskEndStatus
	{
		// Token: 0x040046DC RID: 18140
		NoSync,
		// Token: 0x040046DD RID: 18141
		Commit = 256,
		// Token: 0x040046DE RID: 18142
		Backout = 4352,
		// Token: 0x040046DF RID: 18143
		EndTask = 65536
	}
}
