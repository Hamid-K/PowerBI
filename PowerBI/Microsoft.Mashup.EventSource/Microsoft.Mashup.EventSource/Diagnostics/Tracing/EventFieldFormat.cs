using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000031 RID: 49
	public enum EventFieldFormat
	{
		// Token: 0x040000CB RID: 203
		Default,
		// Token: 0x040000CC RID: 204
		String = 2,
		// Token: 0x040000CD RID: 205
		Boolean,
		// Token: 0x040000CE RID: 206
		Hexadecimal,
		// Token: 0x040000CF RID: 207
		Xml = 11,
		// Token: 0x040000D0 RID: 208
		Json,
		// Token: 0x040000D1 RID: 209
		HResult = 15
	}
}
