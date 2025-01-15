using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F5 RID: 245
	[Flags]
	public enum ParseFrameFeature
	{
		// Token: 0x04000545 RID: 1349
		None = 0,
		// Token: 0x04000546 RID: 1350
		Instance = 1,
		// Token: 0x04000547 RID: 1351
		FirstPersonPronoun = 2,
		// Token: 0x04000548 RID: 1352
		SecondPersonPronoun = 4,
		// Token: 0x04000549 RID: 1353
		ThirdPersonPronoun = 8,
		// Token: 0x0400054A RID: 1354
		DemonstrativePronoun = 16,
		// Token: 0x0400054B RID: 1355
		Possessive = 32,
		// Token: 0x0400054C RID: 1356
		Plural = 64,
		// Token: 0x0400054D RID: 1357
		Aggregated = 128,
		// Token: 0x0400054E RID: 1358
		Collection = 256
	}
}
