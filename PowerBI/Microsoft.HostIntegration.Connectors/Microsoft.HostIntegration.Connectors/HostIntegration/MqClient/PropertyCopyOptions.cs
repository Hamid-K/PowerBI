using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B40 RID: 2880
	[Flags]
	public enum PropertyCopyOptions
	{
		// Token: 0x040047B7 RID: 18359
		None = 0,
		// Token: 0x040047B8 RID: 18360
		Forward = 1,
		// Token: 0x040047B9 RID: 18361
		Reply = 2,
		// Token: 0x040047BA RID: 18362
		Report = 4,
		// Token: 0x040047BB RID: 18363
		Publish = 8,
		// Token: 0x040047BC RID: 18364
		All = 15,
		// Token: 0x040047BD RID: 18365
		Default = 13
	}
}
