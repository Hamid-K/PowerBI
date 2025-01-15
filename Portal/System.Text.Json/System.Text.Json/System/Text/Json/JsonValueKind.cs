using System;

namespace System.Text.Json
{
	// Token: 0x0200003D RID: 61
	public enum JsonValueKind : byte
	{
		// Token: 0x0400012F RID: 303
		Undefined,
		// Token: 0x04000130 RID: 304
		Object,
		// Token: 0x04000131 RID: 305
		Array,
		// Token: 0x04000132 RID: 306
		String,
		// Token: 0x04000133 RID: 307
		Number,
		// Token: 0x04000134 RID: 308
		True,
		// Token: 0x04000135 RID: 309
		False,
		// Token: 0x04000136 RID: 310
		Null
	}
}
