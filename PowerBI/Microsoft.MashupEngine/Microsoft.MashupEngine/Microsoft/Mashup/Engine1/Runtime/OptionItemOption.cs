using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015B3 RID: 5555
	[Flags]
	internal enum OptionItemOption
	{
		// Token: 0x04004C38 RID: 19512
		None = 0,
		// Token: 0x04004C39 RID: 19513
		ForDsrRoundTripOnly = 1,
		// Token: 0x04004C3A RID: 19514
		RequiresActions = 2,
		// Token: 0x04004C3B RID: 19515
		ForExtensibilityOnly = 4,
		// Token: 0x04004C3C RID: 19516
		ExcludeFromOptionType = 8
	}
}
