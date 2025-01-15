using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001EF RID: 495
	[Flags]
	internal enum CollectionAccessType
	{
		// Token: 0x04000588 RID: 1416
		None = 0,
		// Token: 0x04000589 RID: 1417
		Read = 1,
		// Token: 0x0400058A RID: 1418
		ModifyExistingContent = 2,
		// Token: 0x0400058B RID: 1419
		UpdatedContent = 6
	}
}
