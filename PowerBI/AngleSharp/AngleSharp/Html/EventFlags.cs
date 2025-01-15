using System;

namespace AngleSharp.Html
{
	// Token: 0x020000B1 RID: 177
	[Flags]
	internal enum EventFlags : byte
	{
		// Token: 0x04000481 RID: 1153
		None = 0,
		// Token: 0x04000482 RID: 1154
		StopPropagation = 1,
		// Token: 0x04000483 RID: 1155
		StopImmediatePropagation = 2,
		// Token: 0x04000484 RID: 1156
		Canceled = 4,
		// Token: 0x04000485 RID: 1157
		Initialized = 8,
		// Token: 0x04000486 RID: 1158
		Dispatch = 16
	}
}
