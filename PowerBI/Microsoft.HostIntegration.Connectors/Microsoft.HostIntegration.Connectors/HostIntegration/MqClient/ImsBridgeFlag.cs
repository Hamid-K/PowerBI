using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B20 RID: 2848
	[Flags]
	public enum ImsBridgeFlag
	{
		// Token: 0x040046F4 RID: 18164
		None = 0,
		// Token: 0x040046F5 RID: 18165
		PassExpiration = 1,
		// Token: 0x040046F6 RID: 18166
		EmptyReplyFormat = 8,
		// Token: 0x040046F7 RID: 18167
		IgnorePurge = 16,
		// Token: 0x040046F8 RID: 18168
		CommitMode0RequestResponse = 32
	}
}
