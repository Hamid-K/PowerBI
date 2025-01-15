using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200022E RID: 558
	[Flags]
	internal enum PropertyDefinitionFlags
	{
		// Token: 0x04000F32 RID: 3890
		None = 0,
		// Token: 0x04000F33 RID: 3891
		AutoInstantiateOnRead = 1,
		// Token: 0x04000F34 RID: 3892
		ReuseInstance = 2,
		// Token: 0x04000F35 RID: 3893
		CanSet = 4,
		// Token: 0x04000F36 RID: 3894
		CanUpdate = 8,
		// Token: 0x04000F37 RID: 3895
		CanDelete = 16,
		// Token: 0x04000F38 RID: 3896
		CanFind = 32,
		// Token: 0x04000F39 RID: 3897
		MustBeExplicitlyLoaded = 64,
		// Token: 0x04000F3A RID: 3898
		UpdateCollectionItems = 128
	}
}
