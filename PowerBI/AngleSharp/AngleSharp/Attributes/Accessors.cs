using System;

namespace AngleSharp.Attributes
{
	// Token: 0x02000409 RID: 1033
	[Flags]
	public enum Accessors : byte
	{
		// Token: 0x04000D2C RID: 3372
		None = 0,
		// Token: 0x04000D2D RID: 3373
		Getter = 1,
		// Token: 0x04000D2E RID: 3374
		Setter = 2,
		// Token: 0x04000D2F RID: 3375
		Deleter = 4
	}
}
