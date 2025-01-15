using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000041 RID: 65
	[Flags]
	public enum ChangeType
	{
		// Token: 0x040000BF RID: 191
		None = 0,
		// Token: 0x040000C0 RID: 192
		Schema = 1,
		// Token: 0x040000C1 RID: 193
		Data = 2,
		// Token: 0x040000C2 RID: 194
		Loaded = 4,
		// Token: 0x040000C3 RID: 195
		Unloaded = 8,
		// Token: 0x040000C4 RID: 196
		Overwritten = 16,
		// Token: 0x040000C5 RID: 197
		Default = 1
	}
}
