using System;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x0200053E RID: 1342
	[Flags]
	public enum TextTableBorder
	{
		// Token: 0x04000E9D RID: 3741
		None = 0,
		// Token: 0x04000E9E RID: 3742
		Column = 4,
		// Token: 0x04000E9F RID: 3743
		Row = 8,
		// Token: 0x04000EA0 RID: 3744
		Outer = 16,
		// Token: 0x04000EA1 RID: 3745
		Inner = 12,
		// Token: 0x04000EA2 RID: 3746
		Default = 0,
		// Token: 0x04000EA3 RID: 3747
		All = 2147483647
	}
}
