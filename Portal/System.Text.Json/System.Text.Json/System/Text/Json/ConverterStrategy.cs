using System;

namespace System.Text.Json
{
	// Token: 0x0200004E RID: 78
	internal enum ConverterStrategy : byte
	{
		// Token: 0x040001A5 RID: 421
		None,
		// Token: 0x040001A6 RID: 422
		Object,
		// Token: 0x040001A7 RID: 423
		Value,
		// Token: 0x040001A8 RID: 424
		Enumerable = 8,
		// Token: 0x040001A9 RID: 425
		Dictionary = 16
	}
}
