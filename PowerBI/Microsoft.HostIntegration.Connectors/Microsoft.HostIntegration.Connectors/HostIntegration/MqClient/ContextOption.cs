using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B06 RID: 2822
	public enum ContextOption
	{
		// Token: 0x04004645 RID: 17989
		Default = 32,
		// Token: 0x04004646 RID: 17990
		PassIdentity = 256,
		// Token: 0x04004647 RID: 17991
		PassAll = 512,
		// Token: 0x04004648 RID: 17992
		SetIdentity = 1024,
		// Token: 0x04004649 RID: 17993
		SetAll = 2048,
		// Token: 0x0400464A RID: 17994
		None = 16384
	}
}
