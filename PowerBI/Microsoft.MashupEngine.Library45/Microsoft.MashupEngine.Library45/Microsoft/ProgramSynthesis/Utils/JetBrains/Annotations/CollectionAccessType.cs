using System;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000578 RID: 1400
	[Flags]
	public enum CollectionAccessType
	{
		// Token: 0x04000EEC RID: 3820
		None = 0,
		// Token: 0x04000EED RID: 3821
		Read = 1,
		// Token: 0x04000EEE RID: 3822
		ModifyExistingContent = 2,
		// Token: 0x04000EEF RID: 3823
		UpdatedContent = 6
	}
}
