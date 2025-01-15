using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200006D RID: 109
	[Flags]
	public enum TimePart
	{
		// Token: 0x04000166 RID: 358
		None = 0,
		// Token: 0x04000167 RID: 359
		Millisecond = 1,
		// Token: 0x04000168 RID: 360
		Second = 2,
		// Token: 0x04000169 RID: 361
		Minute = 4,
		// Token: 0x0400016A RID: 362
		Hour = 8,
		// Token: 0x0400016B RID: 363
		Day = 16,
		// Token: 0x0400016C RID: 364
		Month = 32,
		// Token: 0x0400016D RID: 365
		Year = 64
	}
}
