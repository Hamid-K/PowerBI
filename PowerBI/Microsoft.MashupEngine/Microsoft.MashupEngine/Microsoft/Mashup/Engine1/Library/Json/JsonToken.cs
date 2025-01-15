using System;

namespace Microsoft.Mashup.Engine1.Library.Json
{
	// Token: 0x02000A42 RID: 2626
	public enum JsonToken
	{
		// Token: 0x0400270D RID: 9997
		End,
		// Token: 0x0400270E RID: 9998
		RecordStart,
		// Token: 0x0400270F RID: 9999
		RecordKey,
		// Token: 0x04002710 RID: 10000
		RecordEnd,
		// Token: 0x04002711 RID: 10001
		ListStart,
		// Token: 0x04002712 RID: 10002
		ListEnd,
		// Token: 0x04002713 RID: 10003
		String,
		// Token: 0x04002714 RID: 10004
		True,
		// Token: 0x04002715 RID: 10005
		False,
		// Token: 0x04002716 RID: 10006
		Null,
		// Token: 0x04002717 RID: 10007
		Number,
		// Token: 0x04002718 RID: 10008
		Error
	}
}
