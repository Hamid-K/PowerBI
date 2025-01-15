using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001FC RID: 508
	public enum TopLevelHandlerOption
	{
		// Token: 0x04000546 RID: 1350
		SwallowNothing,
		// Token: 0x04000547 RID: 1351
		SwallowBenign,
		// Token: 0x04000548 RID: 1352
		SwallowNonfatal,
		// Token: 0x04000549 RID: 1353
		PassNonfatal,
		// Token: 0x0400054A RID: 1354
		SwallowBenignPassNonfatal,
		// Token: 0x0400054B RID: 1355
		PassBenign
	}
}
