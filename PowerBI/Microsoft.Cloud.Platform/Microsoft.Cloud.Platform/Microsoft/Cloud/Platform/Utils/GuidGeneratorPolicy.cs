using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000220 RID: 544
	[Flags]
	public enum GuidGeneratorPolicy
	{
		// Token: 0x04000591 RID: 1425
		None = 0,
		// Token: 0x04000592 RID: 1426
		Faking = 1,
		// Token: 0x04000593 RID: 1427
		AllowOverflow = 2,
		// Token: 0x04000594 RID: 1428
		NoRandomBits = 4
	}
}
