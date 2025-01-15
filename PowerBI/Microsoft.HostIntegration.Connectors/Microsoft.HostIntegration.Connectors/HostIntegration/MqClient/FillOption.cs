using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B08 RID: 2824
	[Flags]
	public enum FillOption
	{
		// Token: 0x04004650 RID: 18000
		None = 0,
		// Token: 0x04004651 RID: 18001
		Headers = 1,
		// Token: 0x04004652 RID: 18002
		Data = 4,
		// Token: 0x04004653 RID: 18003
		AllContext = 16,
		// Token: 0x04004654 RID: 18004
		IdentityContext = 64,
		// Token: 0x04004655 RID: 18005
		DeepCopy = 256
	}
}
